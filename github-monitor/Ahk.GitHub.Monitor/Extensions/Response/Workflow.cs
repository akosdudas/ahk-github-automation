using System;

namespace Octokit
{
    public class Workflow
    {
        public Workflow()
        {
        }

        public Workflow(string badgeUrl, DateTimeOffset createdAt, string htmlUrl, long id, string name, string nodeId, string path, string state, DateTimeOffset updatedAt, string url)
        {
            this.BadgeUrl = badgeUrl;
            this.CreatedAt = createdAt;
            this.HtmlUrl = htmlUrl;
            this.Id = id;
            this.Name = name;
            this.NodeId = nodeId;
            this.Path = path;
            this.State = state;
            this.UpdatedAt = updatedAt;
            this.Url = url;
        }

        public string BadgeUrl { get; protected set; }
        public DateTimeOffset CreatedAt { get; protected set; }
        public string HtmlUrl { get; protected set; }
        public long Id { get; protected set; }
        public string Name { get; protected set; }
        public string NodeId { get; protected set; }
        public string Path { get; protected set; }
        public string State { get; protected set; }
        public DateTimeOffset UpdatedAt { get; protected set; }
        public string Url { get; protected set; }
    }
}
