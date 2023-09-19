using BrokerListService.OpenAPIClients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerListService.Helpers.Interface
{
    public interface IBrokerHelper
    {
        /// <summary>
        /// 打api獲取總公司資料
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<HeadquarterBrokerViewModel>> GetHeadquarterBrokerAsync();
        /// <summary>
        /// 打api獲取子公司資料
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<BranchBrokerViewModel>> GetBranchBrokerAsync();
    }
}
