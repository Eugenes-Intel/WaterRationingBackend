using System.Collections.Generic;
using System.Threading.Tasks;
using WaterRationingBackend.Entities;

namespace WaterRationingBackend.Services.Interfaces
{
    public interface ISupervisor
    {
        Task<IEnumerable<IData>> Get();
        Task<IEnumerable<IData>> GetWith();
        Task<IData> Get(int id);
        Task<IData> GetWith(int id);
        Task<string> Post(object data);
        Task<string> Edit(object data);
        Task<string> Delete(int id);
    }
}