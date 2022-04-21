using System;
using Microsoft.EntityFrameworkCore;

namespace Ahk.Lifecycle.Management
{
    public class CosmosDbContext : DbContext
    {
        internal const string DatabaseName = "ahk-test";
        internal const string EventsContainerName = "events";
        internal const string StatisticsContainerName = "statistics";

        public DbSet<RepositoryStatistics> Statistics { get; set; }
        public DbSet<LifecycleEvent> LifecycleEvents { get; set; }
        public DbSet<RepositoryCreateEvent> RepositoryCreateEvents { get; set; }
        public DbSet<BranchCreateEvent> BranchCreateEvents { get; set; }
        public DbSet<PullRequestEvent> PullRequestEvents { get; set; }
        public DbSet<WorkflowRunEvent> WorkflowRunEvents { get; set; }
        public DbSet<SetGradeEvent> SetGradeEvents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseCosmos(connectionString: Environment.GetEnvironmentVariable("AHK_CosmosDbConnectionString"), databaseName: DatabaseName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepositoryStatistics>()
                .ToContainer(StatisticsContainerName)
                .HasPartitionKey(o => o.Repository)
                .HasNoDiscriminator();

            modelBuilder.Entity<LifecycleEvent>()
                .ToContainer(EventsContainerName)
                .HasPartitionKey(o => o.EventType)
                .HasKey(o => o.LifecycleEventId);
            modelBuilder.Entity<RepositoryCreateEvent>().HasPartitionKey(o => o.EventType);
            modelBuilder.Entity<BranchCreateEvent>().HasPartitionKey(o => o.EventType);
            modelBuilder.Entity<PullRequestEvent>().HasPartitionKey(o => o.EventType);
            modelBuilder.Entity<WorkflowRunEvent>().HasPartitionKey(o => o.EventType);
            modelBuilder.Entity<SetGradeEvent>().HasPartitionKey(o => o.EventType);
        }
    }
}
