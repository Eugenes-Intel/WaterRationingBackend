using System;
using System.Linq;
using System.Threading.Tasks;
using WaterRationingBackend.Entities;
using WaterRationingBackend.Services.Interfaces;

namespace WaterRationingBackend.Services
{
    public class Collator : ICollator
    {
        private readonly ISupervisor _supervisor;
        private readonly IScopeHelper _scopeHelper;

        #region constants
        private const string _loadCities = "suburbs";
        private const string _loadSuburbs = "density";
        private const string _loadHistory = "day";
        #endregion

        public Collator(ISupervisor supervisor, IScopeHelper scopeHelper)
        {
            _supervisor = supervisor;
            _scopeHelper = scopeHelper;
        }

        public async Task<IData> Collate(int id)
        {
            var entityScope = _scopeHelper.GetEntity();

            if (entityScope == Entity.City)
            {
                throw new NotImplementedException(
                    "this method is not callable since this version of Water Rationing system is limited to Harare");
            }
            else if (entityScope == Entity.Suburb)
            {
                var city = await _supervisor.Get(id);
                var suburbs = await _supervisor.Get();
                var selectedSuburbs = suburbs.Cast<Suburb>().Where((s) => s.CityId == id).ToList();
                (city as City).Suburbs = selectedSuburbs;
                return city;
            }
            else if (entityScope == Entity.History)
            {
                var suburb = await _supervisor.Get(id);
                var history = await _supervisor.Get();
                var usageHistories = history.Cast<UsageHistory>().Where((h) => h.SuburbId == id).ToList();
                (suburb as Suburb).UsageHistory = usageHistories;
                return suburb;
            }
            else
            {
                throw new NotImplementedException("entity scope could not be determined");
            }
        }
    }
}
