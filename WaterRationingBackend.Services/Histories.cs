using Helper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterRationingBackend.DataAccess;
using WaterRationingBackend.Entities;
using WaterRationingBackend.Services.Interfaces;

namespace WaterRationingBackend.Services
{
    public class Histories : IEntities
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Histories(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<IData>> GetAsync()
        {
            return await _applicationDbContext.UsageHistories.AsNoTracking().ToListAsync<UsageHistory>();
        }

        public async Task<IData> GetAsync(int id)
        {
            return await _applicationDbContext.UsageHistories.AsNoTracking().Where((usageHistory) => usageHistory.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> AddAsync(object data)
        {
            var response = string.Empty;

            var usageHistory = await Serializer.GetDeserializedClientModelAsync<UsageHistory>(data);
            var usageHistories = await GetAsync();

            if (usageHistories.Cast<UsageHistory>().Any((u) => (u.SuburbId == usageHistory.SuburbId && u.Day == usageHistory.Day)))
            {
                response = ClientResponse.Add(nameof(UsageHistory), ResponseInfo.Exist);
            }
            else
            {
                await _applicationDbContext.AddAsync<UsageHistory>(usageHistory);
                var result = await _applicationDbContext.SaveChangesAsync();
                response = result >= 1 ? ClientResponse.Add(nameof(UsageHistory), ResponseInfo.Success) : ClientResponse.Add(nameof(UsageHistory), ResponseInfo.Error);
            }
            return response;
        }

        public async Task<string> UpdateAsync(object data)
        {
            var response = string.Empty;

            var usageHistory = await Serializer.GetDeserializedClientModelAsync<UsageHistory>(data);
            var singleUsageHistory = await GetAsync(usageHistory.Id);

            if (singleUsageHistory != null)
            {
                var usageHistories = await GetAsync();
                var filteredCities = usageHistories.Cast<UsageHistory>().SkipWhile<UsageHistory>((u) => (u.SuburbId == usageHistory.SuburbId && u.Day == usageHistory.Day));

                if (filteredCities.Any((u) => (u.SuburbId == usageHistory.SuburbId && u.Day == usageHistory.Day)))
                {
                    response = ClientResponse.Add(nameof(UsageHistory), ResponseInfo.Exist);
                }
                else
                {
                    _applicationDbContext.Entry<UsageHistory>(usageHistory).State = EntityState.Modified;
                    var result = await _applicationDbContext.SaveChangesAsync();
                    response = result >= 1 ? ClientResponse.Update(nameof(UsageHistory), ResponseInfo.Success) : ClientResponse.Update(nameof(UsageHistory), ResponseInfo.Error);
                }
            }
            else
            {
                response = ClientResponse.Update(nameof(UsageHistory), ResponseInfo.Exist);
            }
            return response;
        }

        public async Task<string> DeleteAsync(int id)
        {
            var response = string.Empty;

            var singleUsageHistory = await GetAsync(id);
            var usageHistories = await Serializer.GetDeserializedModelAsync<UsageHistory>(singleUsageHistory);

            if (usageHistories != null)
            {
                _applicationDbContext.Entry<UsageHistory>(usageHistories).State = EntityState.Deleted;
                var result = await _applicationDbContext.SaveChangesAsync();
                response = result >= 1 ? ClientResponse.Delete(nameof(UsageHistory), ResponseInfo.Success) : ClientResponse.Delete(nameof(UsageHistory), ResponseInfo.Error);
            }
            else
            {
                response = ClientResponse.Delete(nameof(UsageHistory), ResponseInfo.Exist);
            }
            return response;
        }
    }
}
