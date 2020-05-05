using Ahk.GitHub.Monitor.Extensions;
using Ahk.GitHub.Monitor.Helpers;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Octokit;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ahk.GitHub.Monitor
{
    public static class GitHubClientHelper
    {
        public static async Task<GitHubClient> CreateGitHubClient()
        {
            var gitHubClient = new GitHubClient(new Octokit.ProductHeaderValue("Ahk"))
            {
                Credentials = new Credentials(await GetInstallationToken())
            };
            gitHubClient.SetRequestTimeout(TimeSpan.FromSeconds(5));
            return gitHubClient;
        }

        public static string GetApplicationToken()
        {
            var parameters = CryptoHelper.GetRsaParameters(Environment.GetEnvironmentVariable("AHK_GITHUB_APP_PRIVATE_KEY", EnvironmentVariableTarget.Process));
            var key = new RsaSecurityKey(parameters);
            var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(claims: new[]
            {
                new Claim("iat", now.ToUnixTimeStamp().ToString(), ClaimValueTypes.Integer),
                new Claim("exp", now.AddMinutes(10).ToUnixTimeStamp().ToString(), ClaimValueTypes.Integer),
                new Claim("iss", Environment.GetEnvironmentVariable("AHK_GITHUB_APP_ID", EnvironmentVariableTarget.Process), ClaimValueTypes.Integer)
            },
            signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public static async Task<string> GetInstallationToken()
        {
            var installationId = Environment.GetEnvironmentVariable("AHK_GITHUB_APP_INSTALLATION_ID", EnvironmentVariableTarget.Process);
            var applicationToken = GetApplicationToken();
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.github.com/installations/{installationId}/access_tokens")
                {
                    Headers =
                    {
                        Authorization = new AuthenticationHeaderValue("Bearer", applicationToken),
                        UserAgent =
                        {
                            ProductInfoHeaderValue.Parse("Ahk"),
                        },
                        Accept =
                        {
                            MediaTypeWithQualityHeaderValue.Parse("application/vnd.github.machine-man-preview+json")
                        }
                    }
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var obj = JObject.Parse(json);
                    return obj["token"]?.Value<string>();
                }
            }
        }
    }
}
