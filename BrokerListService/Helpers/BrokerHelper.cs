using BrokerListService.Helpers.Interface;
using BrokerListService.OpenAPIClients;
using BrokerListService.ServiceModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerListService.Helpers
{
    public class BrokerHelper : IBrokerHelper
    {
        private IBrokerClient _brokerClient;
        public BrokerHelper(IBrokerClient brokerClient) 
        {
            _brokerClient = brokerClient;
        }
        public async Task<IEnumerable<BranchBrokerViewModel>> GetBranchBrokerAsync()
        {
            return await _brokerClient.BRK02Async();
        }

        public async Task<IEnumerable<HeadquarterBrokerViewModel>> GetHeadquarterBrokerAsync()
        {
            return await _brokerClient.BrokerListAsync();
        }
    }
}
