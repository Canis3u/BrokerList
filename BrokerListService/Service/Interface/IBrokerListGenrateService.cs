using System.Threading.Tasks;

namespace BrokerListService.Service.Interface
{
    public interface IBrokerListGenrateService
    {
        /// <summary>
        /// 打總公司與子公司API並將資料存入DB
        /// </summary>
        /// <returns></returns>
        Task<int> GetDataFromApiAsync();
    }
}
