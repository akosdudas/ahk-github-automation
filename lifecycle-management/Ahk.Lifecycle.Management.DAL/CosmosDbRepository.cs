using System.Collections;
using Ahk.Lifecycle.Management.DAL.Entities;
using Microsoft.Azure.Cosmos;

namespace Ahk.Lifecycle.Management.DAL
{
    public class CosmosDbRepository : IRepository
    {
        private const string databaseName = "ahk";
        private const string eventsContainerName = "events";
        private const string eventsContainerPartitionKey = "/eventType";
        private const string statisticsContainerName = "statistics";
        private const string statisticsContainerPartitionKey = "/id";

        private readonly Database database;
        private readonly Container eventsContainer;
        private readonly Container statisticsContainer;
        private volatile bool created;

        public CosmosDbRepository(CosmosClient client)
        {
            this.database = client.GetDatabase(databaseName);
            this.eventsContainer = database.GetContainer("events");
            this.statisticsContainer = database.GetContainer("statistics");
        }

        private async Task ensureCreated()
        {
            if (created)
                return;

            await this.database.Client.CreateDatabaseIfNotExistsAsync(databaseName);
            await this.database.CreateContainerIfNotExistsAsync(new ContainerProperties() { Id = eventsContainerName, PartitionKeyPath = eventsContainerPartitionKey });
            await this.database.CreateContainerIfNotExistsAsync(new ContainerProperties() { Id = statisticsContainerName, PartitionKeyPath = statisticsContainerPartitionKey });

            created = true;
        }

        public async Task Insert(LifecycleEvent data)
        {
            await this.ensureCreated();
            await eventsContainer.CreateItemAsync<LifecycleEvent>(data, new PartitionKey(data.EventType));
            await this.Upsert(new RepositoryStatistics() {
                Id = data.Repository,
                Username = data.Username,
                EventCount = 1,
                LastEventId = data.Id,
                LastEventType = data.EventType,
                LastEventDate = data.Timestamp,
            });
        }

        public Task<ICollection> GetOne(string repo) => throw new NotImplementedException();

        public Task<ICollection> GetMany(string repository = "", string username = "", int page = 1, int limit = 10) => throw new NotImplementedException();

        public async Task Upsert(RepositoryStatistics data)
        {
            await this.ensureCreated();

            try
            {
                await statisticsContainer.CreateItemAsync(data, new PartitionKey(data.Id));
            }
            catch (Exception)
            {
                var result = await statisticsContainer.ReadItemAsync<RepositoryStatistics>(data.Id, new PartitionKey(data.Id));

                data.EventCount = result.Resource.EventCount + 1;

                if (data.LastEventDate < result.Resource.LastEventDate)
                {
                    data.LastEventId = result.Resource.LastEventId;
                    data.LastEventType = result.Resource.LastEventType;
                    data.LastEventDate = result.Resource.LastEventDate;
                }

                await statisticsContainer.UpsertItemAsync(data, new PartitionKey(data.Id));
            }
        }
    }
}
