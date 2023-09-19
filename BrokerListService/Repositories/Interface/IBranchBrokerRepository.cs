using BrokerListService.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace BrokerListService.Repositories.Interface
{
    public interface IBranchBrokerRepository
    {
        /// <summary>
        /// 查詢DB子公司List資料
        /// </summary>
        /// <param name="branchPredicate"></param>
        /// <returns></returns>
        Task<IEnumerable<BranchBroker>> GetListAsync(Expression<Func<BranchBroker, bool>> branchPredicate);
        /// <summary>
        /// 刪除DB子公司List資料
        /// </summary>
        /// <param name="branchBroker"></param>
        /// <returns></returns>
        Task<int> DeleteListAsync(IEnumerable<BranchBroker> branchBroker);
    }
}
