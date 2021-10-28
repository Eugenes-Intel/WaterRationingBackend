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
    public class Suburbs : IEntities
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Suburbs(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<IData>> GetAsync()
        {
            return await _applicationDbContext.Suburbs.AsNoTracking().ToListAsync<Suburb>();
        }
        public async Task<IEnumerable<IData>> GetWithAsync()
        {
            return await _applicationDbContext.Suburbs.Include((s) => s.UsageHistory).ToListAsync();
        }


        public async Task<IData> GetAsync(int id)
        {
            return await _applicationDbContext.Suburbs.AsNoTracking().Where((suburb) => suburb.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IData> GetWithAsync(int id)
        {
            return await _applicationDbContext.Suburbs.Where((s) => s.Id == id).Include((s) => s.UsageHistory).SingleAsync();
        }

        public async Task<string> AddAsync(object data)
        {
            var response = string.Empty;

            var suburb = await Serializer.GetDeserializedClientModelAsync<Suburb>(data);
            var suburbs = await GetAsync();

            if (suburbs.Cast<Suburb>().Any((s) => s.Name == suburb.Name))
            {
                response = ClientResponse.Add(suburb.Name, ResponseInfo.Exist);
            }
            else
            {
                await _applicationDbContext.AddAsync<Suburb>(suburb);
                var result = await _applicationDbContext.SaveChangesAsync();
                response = result >= 1 ? ClientResponse.Add(suburb.Name, ResponseInfo.Success) : ClientResponse.Add(suburb.Name, ResponseInfo.Error);
            }
            return response;
        }

        public async Task<string> UpdateAsync(object data)
        {
            var response = string.Empty;
            
            var suburb = await Serializer.GetDeserializedClientModelAsync<Suburb>(data);
            var singleSuburb = await GetAsync(suburb.Id);

            if (singleSuburb != null)
            {
                var suburbs = await GetAsync();
                var filteredCities = suburbs.Cast<Suburb>().SkipWhile<Suburb>((c) => c.Id == singleSuburb.Id);

                if (filteredCities.Any((c) => c.Name == suburb.Name))
                {
                    response = ClientResponse.Add(suburb.Name, ResponseInfo.Exist);
                }
                else
                {
                    _applicationDbContext.Entry<Suburb>(suburb).State = EntityState.Modified;
                    var result = await _applicationDbContext.SaveChangesAsync();
                    response = result >= 1 ? ClientResponse.Update(suburb.Name, ResponseInfo.Success) : ClientResponse.Update(suburb.Name, ResponseInfo.Error);
                }
            }
            else
            {
                response = ClientResponse.Update(nameof(Suburb), ResponseInfo.Exist);
            }
            return response;
        }

        public async Task<string> DeleteAsync(int id)
        {
            var response = string.Empty;

            var singleSuburb = await GetAsync(id);
            var suburb = await Serializer.GetDeserializedModelAsync<Suburb>(singleSuburb);

            if (suburb != null)
            {
                _applicationDbContext.Entry<Suburb>(suburb).State = EntityState.Deleted;
                var result = await _applicationDbContext.SaveChangesAsync();
                response = result >= 1 ? ClientResponse.Delete(suburb.Name, ResponseInfo.Success) : ClientResponse.Delete(suburb.Name, ResponseInfo.Error);
            }
            else
            {
                response = ClientResponse.Delete(nameof(Suburb), ResponseInfo.Exist);
            }
            return response;
        }

        
    }
}
