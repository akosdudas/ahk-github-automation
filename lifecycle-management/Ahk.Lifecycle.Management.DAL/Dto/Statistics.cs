using Ahk.Lifecycle.Management.DAL.Entities;

namespace Ahk.Lifecycle.Management.DAL.Dto
{
    public class Statistics
    {
        public Statistics(int count, string repository, ICollection<LifecycleEvent> events)
        {
            this.Count = count;
            this.Repository = repository;
            this.Events = events;
        }

        public int Count { get; }
        public string Repository { get; }
        public ICollection<LifecycleEvent> Events { get; }
    }
}
