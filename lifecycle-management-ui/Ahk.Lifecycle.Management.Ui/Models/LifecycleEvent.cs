using System.Text.Json.Serialization;

namespace Ahk.Lifecycle.Management.Ui.Models
{
    public class LifecycleEvent
    {
        public LifecycleEvent(string id, string repository, string username, string type, DateTime timestamp)
        {
            this.Id = id;
            this.Repository = repository;
            this.Username = username;
            this.Type = type;
            this.Timestamp = timestamp;
        }

        [JsonPropertyName("id")]
        public string Id { get; }
        public string Repository { get; }
        public string Username { get; }

        [JsonPropertyName("$type")]
        public string Type { get; }
        public DateTime Timestamp { get; }
    }
}
