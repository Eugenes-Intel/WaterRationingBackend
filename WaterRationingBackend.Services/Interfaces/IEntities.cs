using System.Collections.Generic;
using System.Threading.Tasks;
using WaterRationingBackend.Entities;

namespace WaterRationingBackend.Services.Interfaces
{
    public interface IEntities
    {
        Task<IEnumerable<IData>> GetAsync();
        Task<IEnumerable<IData>> GetWithAsync();
        Task<IData> GetAsync(int id);
        Task<IData> GetWithAsync(int id);
        Task<string> AddAsync(object data);
        Task<string> UpdateAsync(object data);
        Task<string> DeleteAsync(int id);
    }
}