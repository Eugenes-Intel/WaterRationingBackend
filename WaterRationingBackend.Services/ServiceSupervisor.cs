using System.Collections.Generic;
using System.Threading.Tasks;
using WaterRationingBackend.Entities;
using WaterRationingBackend.Services.Interfaces;

namespace WaterRationingBackend.Services
{
    public class ServiceSupervisor : ISupervisor
    {
        private readonly IEntityDecider _entityDecider;

        public ServiceSupervisor(IEntityDecider entityDecider)
        {
            _entityDecider = entityDecider;
        }

        public Task<IEnumerable<IData>> Get()
        {
            return  _entityDecider.Decided().GetAsync();
        }

        public Task<IData> Get(int id)
        {
            return _entityDecider.Decided().GetAsync(id);
        }

        public Task<string> Post(object data)
        {
            return _entityDecider.Decided().AddAsync(data);
        }

        public Task<string> Edit(object data)
        {
            return _entityDecider.Decided().UpdateAsync(data);
        }

        public Task<string> Delete(int id)
        {
            return _entityDecider.Decided().DeleteAsync(id);
        }
    }
}
