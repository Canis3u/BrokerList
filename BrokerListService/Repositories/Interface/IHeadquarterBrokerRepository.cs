using BrokerListService.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace BrokerListService.Repositories.Interface
{
    public interface IHeadquarterBrokerRepository
    {
        /// <summary>
        /// 查詢DB獲取總公司id
        /// </summary>
        /// <param name="headquarterPredicate"></param>
        /// <returns></returns>
        Task<int?> GetHeadquarterIdByCodeAsync(Expression<Func<HeadquarterBroker, bool>> headquarterPredicate);
        /// <summary>
        /// 查詢DB獲取總公司及其子公司資料
        /// </summary>
        /// <param name="headquarterPredicate"></param>
        /// <returns></returns>
        Task<HeadquarterBroker> GetIncludeBranchAsync(Expression<Func<HeadquarterBroker, bool>> headquarterPredicate);
        /// <summary>
        /// 查詢DB獲取總公司List資料
        /// </summary>
        /// <param name="headquarterPredicate"></param>
        /// <returns></returns>
        Task<IEnumerable<HeadquarterBroker>> GetListAsync(Expression<Func<HeadquarterBroker, bool>> headquarterPredicate);
        /// <summary>
        /// 新增DB總公司資料
        /// </summary>
        /// <param name="headquarterBrokers"></param>
        /// <returns></returns>
        Task<int> InsertListAsync(IEnumerable<HeadquarterBroker> headquarterBrokers);
        /// <summary>
        /// 刪除DB總公司資料
        /// </summary>
        /// <param name="headquarterBroker"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(HeadquarterBroker headquarterBroker);
        /// <summary>
        /// 刪除總公司List資料
        /// </summary>
        /// <param name="headquarterBrokers"></param>
        /// <returns></returns>
        Task<int> DeleteListAsync(IEnumerable<HeadquarterBroker> headquarterBrokers);
    }
}
