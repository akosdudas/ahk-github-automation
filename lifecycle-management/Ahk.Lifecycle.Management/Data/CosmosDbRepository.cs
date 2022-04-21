using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Ahk.Lifecycle.Management
{
    public class CosmosDbRepository : IRepository
    {
        private readonly CosmosDbContext _context;
        private volatile bool created;
        public CosmosDbRepository(CosmosDbContext context) => _context = context;

        private async Task createDB()
        {
            if(!created)
            {
                created = true;
                await _context.Database.EnsureCreatedAsync();
            }
        }

        public async Task Insert(LifecycleEvent data)
        {
            await this.createDB();

            var newStatistics = new RepositoryStatistics()
            {
                Repository = data.Repository,
                Username = data.Username,
                EventCount = 1,
                LastEventType = data.EventType,
                LastEventDate = data.Timestamp,
                LastEventId = data.LifecycleEventId
            };

            await _context.LifecycleEvents.AddAsync(data);
            await _context.SaveChangesAsync();

            await this.Insert(newStatistics);
        }

        public async Task<ICollection> GetOne(string repo)
        {
            await this.createDB();

            return await Task.FromResult<ICollection>(_context.LifecycleEvents.Where(e => e.Repository.Contains(repo)).ToList());
        }
        public async Task Insert(RepositoryStatistics data)
        {
            await this.createDB();

            try
            {
                await _context.Statistics.AddAsync(data);
                await _context.SaveChangesAsync();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                Console.WriteLine(ex.Message);
                await this.Update(data);
            }
        }

        public async Task Update(RepositoryStatistics data)
        {
            await this.createDB();

            _context.Statistics.Remove(data);
            var oldStat = _context.Statistics.FirstOrDefault(o => o.Repository == data.Repository);

            if (oldStat != null)
            {
                if (oldStat.LastEventDate < data.LastEventDate)
                {
                    oldStat.LastEventDate = data.LastEventDate;
                    oldStat.LastEventId = data.LastEventId;
                    oldStat.LastEventType = data.LastEventType;
                }
                
                oldStat.EventCount++;

                _context.Statistics.Update(oldStat);
                await _context.SaveChangesAsync(true);
            }
        }

        public async Task<ICollection> GetMany(string repository = "", string username = "", int page = 1, int limit = 10)
        {
            await this.createDB();

            if(page < 1)
                page = 1;

            if(limit < 1 || limit > 25)
                limit = 25;

            var q = _context.Statistics
                .Where(o => o.Repository.Contains(repository) && o.Username.Contains(username));

            int count = q.Count();

            var repos = q
                .Skip(limit * (page - 1))
                .Take(limit);

            JObject result = new JObject
            {
                { "TotalCount", count },
                { "Page", page },
                { "Limit", limit },
                {
                    "repositories", JArray.FromObject(repos)
                }
            };

            return result;
        }
    }
}
