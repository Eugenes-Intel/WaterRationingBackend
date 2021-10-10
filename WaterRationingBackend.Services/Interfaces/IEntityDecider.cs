using System.Threading.Tasks;

namespace WaterRationingBackend.Services.Interfaces
{
    public interface IEntityDecider
    {
        IEntities Decided();
    }
}