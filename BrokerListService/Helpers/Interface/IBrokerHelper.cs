using BrokerListService.OpenAPIClients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerListService.Helpers.Interface
{
    public interface IBrokerHelper
    {
        Task<IEnumerable<HeadquarterBrokerViewModel>> GetHeadquarterBrokerAsync();
        Task<IEnumerable<BranchBrokerViewModel>> GetBranchBrokerAsync();
    }
}
