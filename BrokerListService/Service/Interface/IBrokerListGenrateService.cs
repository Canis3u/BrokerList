using System.Threading.Tasks;

namespace BrokerListService.Service.Interface
{
    public interface IBrokerListGenrateService
    {
        Task<int> GetDataFromApiAsync();
    }
}
