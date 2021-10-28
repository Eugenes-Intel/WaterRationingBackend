using Helper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaterRationingBackend.DataAccess;
using WaterRationingBackend.Entities;
using WaterRationingBackend.Services.Interfaces;
//using System.Data.Entity;

namespace WaterRationingBackend.Services
{
    public class Cities : IEntities
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Cities(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<IData>> GetAsync()
        {
            //return await _applicationDbContext.Cities.AsNoTracking().ToListAsync<City>();
            return await _applicationDbContext.Cities.Include((c) => c.Suburbs).ThenInclude((s) => s.UsageHistory).ToListAsync();
        }
        public async Task<IEnumerable<IData>> GetWithAsync()
        {
            return await _applicationDbContext.Cities.Include((c) => c.Suburbs).ThenInclude((s) => s.UsageHistory).ToListAsync();
        }

        public async Task<IData> GetAsync(int id)
        {
            return await _applicationDbContext.Cities.AsNoTracking().Where((city) => city.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IData> GetWithAsync(int id)
        {
            var city = await _applicationDbContext.Cities.AsNoTracking().Where((city) => city.Id == id).FirstOrDefaultAsync();
            return city;
        }

        public async Task<string> AddAsync(object data)
        {
            var response = string.Empty;

            var city = await Serializer.GetDeserializedClientModelAsync<City>(data);
            var cities = await GetAsync();

            if (cities.Cast<City>().Any((c) => c.Name == city.Name))
            {
                response = ClientResponse.Add(city.Name, ResponseInfo.Exist);
            }
            else
            {
                await _applicationDbContext.AddAsync<City>(city);
                var result = await _applicationDbContext.SaveChangesAsync();
                response = result >= 1 ? ClientResponse.Add(city.Name, ResponseInfo.Success) : ClientResponse.Add(city.Name, ResponseInfo.Error);
            }
            return response;
        }

        public async Task<string> UpdateAsync(object data)
        {
            var response = string.Empty;

            var city = await Serializer.GetDeserializedClientModelAsync<City>(data);
            var singleCity = await GetAsync(city.Id);

            if (singleCity != null)
            {
                var cities = await GetAsync();
                var filteredCities = cities.Cast<City>().SkipWhile<City>((c) => c.Id == singleCity.Id);

                if (filteredCities.Any((c) => c.Name == city.Name))
                {
                    response = ClientResponse.Add(city.Name, ResponseInfo.Exist);
                }
                else
                {
                    _applicationDbContext.Entry<City>(city).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
                    var result = await _applicationDbContext.SaveChangesAsync();
                    response = result >= 1 ? ClientResponse.Update(city.Name, ResponseInfo.Success) : ClientResponse.Update(city.Name, ResponseInfo.Error);
                }
            }
            else
            {
                response = ClientResponse.Update(nameof(City), ResponseInfo.Exist);
            }
            return response;
        }

        public async Task<string> DeleteAsync(int id)
        {
            var response = string.Empty;

            var singleCity = await GetAsync(id);
            var city = await Serializer.GetDeserializedModelAsync<City>(singleCity);

            if (city != null)
            {
                _applicationDbContext.Entry<City>(city).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Deleted;
                var result = await _applicationDbContext.SaveChangesAsync();
                response = result >= 1 ? ClientResponse.Delete(city.Name, ResponseInfo.Success) : ClientResponse.Delete(city.Name, ResponseInfo.Error);
            }
            else
            {
                response = ClientResponse.Delete(nameof(City), ResponseInfo.Exist);
            }
            return response;
        }
    }
}
