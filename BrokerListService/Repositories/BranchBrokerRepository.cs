using BrokerListService.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System;
using Microsoft.EntityFrameworkCore;
using BrokerListService.Repositories.Interface;

namespace BrokerListService.Repositories
{
    public class BranchBrokerRepository: IBranchBrokerRepository
    {
        private BrokerListContext _brokerListContext;
        public BranchBrokerRepository(BrokerListContext brokerListContext)
        {
            _brokerListContext = brokerListContext;
        }
        public async Task<IEnumerable<BranchBroker>> GetListAsync(Expression<Func<BranchBroker, bool>> branchPredicate)
        {
            var branchBrokers = await _brokerListContext.BranchBrokers.Where(branchPredicate).ToListAsync();
            return branchBrokers;
        }
        public async Task<int> DeleteListAsync(IEnumerable<BranchBroker> branchBroker)
        {
            var rowschanges = 0;
            using (TransactionScope ts = new TransactionScope())
            {
                _brokerListContext.BranchBrokers.RemoveRange(branchBroker);
                rowschanges = _brokerListContext.SaveChanges();
                ts.Complete();
            }
            return rowschanges;
        }
    }
}
