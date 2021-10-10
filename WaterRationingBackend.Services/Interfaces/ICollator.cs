using System.Threading.Tasks;
using WaterRationingBackend.Entities;

namespace WaterRationingBackend.Services.Interfaces
{
    public interface ICollator
    {
        Task<IData> Collate(int id);
    }
}