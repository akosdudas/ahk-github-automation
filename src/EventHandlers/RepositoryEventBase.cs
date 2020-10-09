﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Ahk.GitHub.Monitor.Services;
using Octokit;
using Octokit.Internal;

namespace Ahk.GitHub.Monitor.EventHandlers
{
    public abstract class RepositoryEventBase<TPayload> : IGitHubEventHandler
        where TPayload : ActivityPayload
    {
        private static readonly YamlDotNet.Serialization.IDeserializer YamlDeserializer = CreateYamlDeserializer();

        private readonly IGitHubClientFactory gitHubClientFactory;

        protected GitHubClient GitHubClient { get; private set; }

        protected RepositoryEventBase(IGitHubClientFactory gitHubClientFactory)
        {
            this.gitHubClientFactory = gitHubClientFactory;
        }

        public async Task Execute(string requestBody, WebhookResult webhookResult)
        {
            if (!tryParsePayload(requestBody, webhookResult, out var webhookPayload))
                return;

            GitHubClient = await gitHubClientFactory.CreateGitHubClient(webhookPayload.Installation.Id);
            var repoSettings = await tryGetRepositorySettings(webhookPayload, webhookResult);

            if (repoSettings == null)
                return;

            if (!repoSettings.Enabled)
            {
                webhookResult.LogInfo($"ahk-monitor.yml disabled app for repository {webhookPayload.Repository.FullName}");
                return;
            }

            await execute(webhookPayload, repoSettings, webhookResult);
        }

        protected abstract Task execute(TPayload webhookPayload, RepositorySettings repoSettings, WebhookResult webhookResult);

        protected bool tryParsePayload(string requestBody, WebhookResult webhookResult, out TPayload payload)
        {
            payload = null;
            if (string.IsNullOrEmpty(requestBody))
            {
                webhookResult.LogError("request body was empty");
                return false;
            }

            try
            {
                payload = new SimpleJsonSerializer().Deserialize<TPayload>(requestBody);
            }
            catch (Exception ex)
            {
                webhookResult.LogError(ex, "request body deserialization failed");
                return false;
            }

            if (payload == null)
            {
                webhookResult.LogError("parsed payload was null or empty");
                return false;
            }

            if (payload.Repository == null)
            {
                webhookResult.LogError("no repository information in webhook payload");
                return false;
            }

            return true;
        }

        private static YamlDotNet.Serialization.IDeserializer CreateYamlDeserializer()
            => new YamlDotNet.Serialization.DeserializerBuilder()
                    .WithNamingConvention(YamlDotNet.Serialization.NamingConventions.CamelCaseNamingConvention.Instance)
                    .IgnoreUnmatchedProperties()
                    .Build();

        private async Task<RepositorySettings> tryGetRepositorySettings(TPayload webhookPayload, WebhookResult webhookResult)
        {
            try
            {
                var contents = await GitHubClient.Repository.Content.GetAllContentsByRef(webhookPayload.Repository.Id, ".github/ahk-monitor.yml", webhookPayload.Repository.DefaultBranch);
                var settingsString = contents.FirstOrDefault()?.Content;

                if (settingsString == null)
                {
                    webhookResult.LogInfo($"repository {webhookPayload.Repository.FullName} has no ahk-monitor.yml");
                    return null;
                }

                try
                {
                    return YamlDeserializer.Deserialize<RepositorySettings>(settingsString);
                }
                catch (Exception ex)
                {
                    webhookResult.LogError("Config yaml parse error: " + ex.Message);
                    return null;
                }
            }
            catch (NotFoundException)
            {
                webhookResult.LogInfo($"repository {webhookPayload.Repository.FullName} has no ahk-monitor.yml");
                return null;
            }
        }
    }
}
