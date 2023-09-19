using AutoMapper;
using BrokerListService.Models;
using BrokerListService.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace BrokerListService.Repositories
{
    public class HeadquarterBrokerRepository: IHeadquarterBrokerRepository
    {
        private BrokerListContext _brokerListContext;
        public HeadquarterBrokerRepository(BrokerListContext brokerListContext)
        {
            _brokerListContext = brokerListContext;
        }
        public async Task<int?> GetHeadquarterIdByCodeAsync(Expression<Func<HeadquarterBroker, bool>> headquarterPredicate)
        {
            var headquarterBrokerId = (await _brokerListContext.HeadquarterBrokers.Where(headquarterPredicate).FirstOrDefaultAsync())?.HeadquarterBrokerId;
            return headquarterBrokerId;
        }
        public async Task<HeadquarterBroker> GetIncludeBranchAsync(Expression<Func<HeadquarterBroker, bool>> headquarterPredicate)
        {
            var headquarterBroker = await _brokerListContext.HeadquarterBrokers.Include(c => c.BranchBrokers).Where(headquarterPredicate).FirstOrDefaultAsync();
            return headquarterBroker;
        }
        public async Task<IEnumerable<HeadquarterBroker>> GetListAsync(Expression<Func<HeadquarterBroker, bool>> headquarterPredicate)
        {
            var headquarterBrokers = await _brokerListContext.HeadquarterBrokers.Where(headquarterPredicate).ToListAsync();
            return headquarterBrokers;
        }
        public async Task<int> InsertListAsync(IEnumerable<HeadquarterBroker> headquarterBrokers)
        {
            var rowschanges = 0;
            using (TransactionScope ts = new TransactionScope())
            {
                await _brokerListContext.HeadquarterBrokers.AddRangeAsync(headquarterBrokers);
                rowschanges = _brokerListContext.SaveChanges();
                ts.Complete();
            }
            return rowschanges;
        }
        public async Task<int> DeleteAsync(HeadquarterBroker headquarterBroker)
        {
            var rowschanges = 0;
            using (TransactionScope ts = new TransactionScope())
            {
                _brokerListContext.HeadquarterBrokers.Remove(headquarterBroker);
                rowschanges = _brokerListContext.SaveChanges();
                ts.Complete();
            }
            return rowschanges;
        }
        public async Task<int> DeleteListAsync(IEnumerable<HeadquarterBroker> headquarterBrokers)
        {
            var rowschanges = 0;
            using (TransactionScope ts = new TransactionScope())
            {
                _brokerListContext.HeadquarterBrokers.RemoveRange(headquarterBrokers);
                rowschanges = _brokerListContext.SaveChanges();
                ts.Complete();
            }
            return rowschanges;
        }
    }
}
