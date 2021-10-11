using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WaterRationingBackend.DataAccess;
using WaterRationingBackend.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WaterRationingBackend.Services
{
    public class EntityFetcher
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EntityFetcher(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<City> GetCityId(string title)
        {
            return _applicationDbContext.Cities.AsNoTracking().Where((c) => c.Name == title).FirstOrDefaultAsync();
        }

        public Task<Suburb> GetSuburbId(string title)
        {
            return _applicationDbContext.Suburbs.AsNoTracking().Where((c) => c.Name == title).FirstOrDefaultAsync();
        }
    }
}
