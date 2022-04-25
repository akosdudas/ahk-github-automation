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

        private static readonly SemaphoreSlim lockObject = new SemaphoreSlim(1, 1);

        public CosmosDbRepository(CosmosClient client)
        {
            this.database = client.GetDatabase(databaseName);
            this.eventsContainer = database.GetContainer(eventsContainerName);
            this.statisticsContainer = database.GetContainer(statisticsContainerName);
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

        private static RepositoryStatistics createRepositoryStatistics(string repository = "", string username = "", string branchName = "", string action = "", string[] assignees = null!, string neptun = "", string conclusion = "", bool isGraded = false, int workflowRunCount = 0, int eventCount = 0, LifecycleEvent firstEvent = null!, LifecycleEvent lastEvent = null!)
        {
            return new RepositoryStatistics()
            {
                Id = repository,
                Username = username,
                BranchName = branchName,
                Action = action,
                Assignees = assignees,
                Neptun = neptun,
                Conclusion = conclusion,
                IsGraded = isGraded,
                WorkflowRunCount = workflowRunCount,
                EventCount = eventCount,
                FirstEventType = firstEvent.EventType,
                FirstEventDate = firstEvent.Timestamp,
                FirstEventId = firstEvent.Id,
                LastEventType = lastEvent.EventType,
                LastEventDate = lastEvent.Timestamp,
                LastEventId = lastEvent.Id,
            };
        }

        public Task<ICollection> GetMany(string repository = "", string username = "", int page = 1, int limit = 10) => throw new NotImplementedException();


        public async Task Insert(RepositoryCreateEvent data)
        {
            await this.ensureCreated();
            await eventsContainer.CreateItemAsync<RepositoryCreateEvent>(data, new PartitionKey(data.EventType));
            await this.Upsert(createRepositoryStatistics(
                repository: data.Repository,
                username: data.Username,
                eventCount: 1,
                firstEvent: data,
                lastEvent: data));
        }

        public async Task Insert(BranchCreateEvent data)
        {
            await this.ensureCreated();
            await eventsContainer.CreateItemAsync<BranchCreateEvent>(data, new PartitionKey(data.EventType));
            await this.Upsert(createRepositoryStatistics(
                repository: data.Repository,
                username: data.Username,
                branchName: data.Branch,
                eventCount: 1,
                firstEvent: data,
                lastEvent: data));
        }

        public async Task Insert(PullRequestEvent data)
        {
            await this.ensureCreated();
            await eventsContainer.CreateItemAsync<PullRequestEvent>(data, new PartitionKey(data.EventType));
            await this.Upsert(createRepositoryStatistics(
                repository: data.Repository,
                username: data.Username,
                action: data.Action,
                assignees: data.Assignees,
                neptun: data.Neptun,
                eventCount: 1,
                firstEvent: data,
                lastEvent: data));
        }

        public async Task Insert(WorkflowRunEvent data)
        {
            await this.ensureCreated();
            await eventsContainer.CreateItemAsync<WorkflowRunEvent>(data, new PartitionKey(data.EventType));
            await this.Upsert(createRepositoryStatistics(
                repository: data.Repository,
                username: data.Username,
                conclusion: data.Conclusion,
                eventCount: 1,
                firstEvent: data,
                lastEvent: data));
        }

        public async Task Insert(SetGradeEvent data)
        {
            await this.ensureCreated();
            await eventsContainer.CreateItemAsync<SetGradeEvent>(data, new PartitionKey(data.EventType));
            await this.Upsert(createRepositoryStatistics(
                repository: data.Repository,
                username: data.Username,
                isGraded: true,
                eventCount: 1,
                firstEvent: data,
                lastEvent: data));
        }

        private static string getCorrectValue(string newer, string older) => (string.IsNullOrEmpty(newer) && !string.IsNullOrEmpty(older)) ? older : newer;

        public async Task Upsert(RepositoryStatistics data)
        {
            await this.ensureCreated();

            await lockObject.WaitAsync();

            try
            {
                await statisticsContainer.CreateItemAsync(data, new PartitionKey(data.Id));
            }
            catch (Exception)
            {
                var result = await statisticsContainer.ReadItemAsync<RepositoryStatistics>(data.Id, new PartitionKey(data.Id));

                data.BranchName = getCorrectValue(data.BranchName, result.Resource.BranchName);
                data.Action = getCorrectValue(data.Action, result.Resource.Action);

                if(result.Resource.Assignees != null && result.Resource.Assignees.Length > 0 && (data.Assignees == null))
                    data.Assignees = result.Resource.Assignees;

                data.Neptun = getCorrectValue(data.Neptun, result.Resource.Neptun);

                if(result.Resource.WorkflowRunCount > 0)
                    data.WorkflowRunCount = result.Resource.WorkflowRunCount;

                if (!string.IsNullOrEmpty(data.Conclusion))
                    data.WorkflowRunCount++;

                data.Conclusion = getCorrectValue(data.Conclusion, result.Resource.Conclusion);

                if (result.Resource.IsGraded)
                    data.IsGraded = true;

                data.EventCount = result.Resource.EventCount + 1;

                if (data.LastEventDate < result.Resource.LastEventDate)
                {
                    data.LastEventId = result.Resource.LastEventId;
                    data.LastEventType = result.Resource.LastEventType;
                    data.LastEventDate = result.Resource.LastEventDate;
                }

                if (data.FirstEventDate > result.Resource.FirstEventDate)
                {
                    data.FirstEventId = result.Resource.FirstEventId;
                    data.FirstEventType = result.Resource.FirstEventType;
                    data.FirstEventDate = result.Resource.FirstEventDate;
                }

                await statisticsContainer.UpsertItemAsync(data, new PartitionKey(data.Id));
            }
            finally
            {
                lockObject.Release();
            }
        }
    }
}
