using BrokerListService.ServiceModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerListService.Service.Interface
{
    public interface IBrokerService
    {
        Task<int> DeleteBrokerAsync(string deleteCode);
        Task<IEnumerable<BrokerRespServiceModel>> GetBrokersAsync(BrokerQueryServiceModel queryServiceModel);
    }
}
