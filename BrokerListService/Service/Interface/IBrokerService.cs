using BrokerListService.ServiceModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BrokerListService.Service.Interface
{
    public interface IBrokerService
    {   
        /// <summary>
        /// 以總公司代號(Code)刪除該總公司與其所有子公司之資料
        /// </summary>
        /// <param name="deleteCode"></param>
        /// <returns></returns>
        Task<int> DeleteBrokerAsync(string deleteCode);
        /// <summary>
        /// 以條件查詢總公司與子公司資料
        /// </summary>
        /// <param name="queryServiceModel"></param>
        /// <returns></returns>
        Task<IEnumerable<BrokerRespServiceModel>> GetBrokersAsync(BrokerQueryServiceModel queryServiceModel);
    }
}
