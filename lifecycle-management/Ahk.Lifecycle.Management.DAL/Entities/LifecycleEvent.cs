using Newtonsoft.Json;

namespace Ahk.Lifecycle.Management.DAL.Entities
{
    public abstract class LifecycleEvent
    {
        [JsonProperty("id")]
        public abstract string Id { get; }
        public abstract string Repository { get; }
        public abstract string Username { get; }

        [JsonProperty("$type")]
        public abstract string Type { get; }
        public abstract DateTime Timestamp { get; }
    }
}
