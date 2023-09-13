using AutoMapper;
using BrokerListService.Extensions;
using BrokerListService.Models;
using BrokerListService.Service.Interface;
using BrokerListService.ServiceModel;
using BrokerListService.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace BrokerListService.Service
{
    public class BrokerService:IBrokerService
    {
        private readonly IMapper _mapper;
        private BrokerListContext _brokerListContext;
        public BrokerService(IMapper mapper, BrokerListContext brokerListContext) 
        {
            _mapper = mapper;
            _brokerListContext = brokerListContext;
        }

        public async Task<int> DeleteBrokerAsync(string deleteCode)
        {
            int rowschanges = 0;
            if (deleteCode == "All")
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var branchsBrokers = _brokerListContext.BranchBrokers.ToList();
                    var headquarterBrokers = _brokerListContext.HeadquarterBrokers.ToList();
                    _brokerListContext.BranchBrokers.RemoveRange(branchsBrokers);
                    _brokerListContext.HeadquarterBrokers.RemoveRange(headquarterBrokers);
                    rowschanges = _brokerListContext.SaveChanges();
                    ts.Complete();
                }
            }
            else 
            {
                var headquarterBroker = _brokerListContext.HeadquarterBrokers.Include(c => c.BranchBrokers).Where(x => x.Code == deleteCode).FirstOrDefault();
                if (headquarterBroker != null) 
                {
                    var branchBrokers = headquarterBroker.BranchBrokers;
                    using (TransactionScope ts = new TransactionScope())
                    {
                        _brokerListContext.BranchBrokers.RemoveRange(branchBrokers);
                        _brokerListContext.HeadquarterBrokers.RemoveRange(headquarterBroker);
                        rowschanges = _brokerListContext.SaveChanges();
                        ts.Complete();
                    }
                }
            }
            return rowschanges;
        }

        public async Task<IEnumerable<BrokerRespServiceModel>> GetBrokersAsync(BrokerQueryServiceModel queryServiceModel)
        {
            List<BrokerRespServiceModel> brokerRespServiceModels = new List<BrokerRespServiceModel>();
            Expression<Func<HeadquarterBroker, bool>> headquarterPredicate = x => true;
            Expression<Func<BranchBroker, bool>> branchPredicate = x => true;
            if (!string.IsNullOrWhiteSpace(queryServiceModel.HeadquarterCode))
            {
                headquarterPredicate = headquarterPredicate.And(x => x.Code == queryServiceModel.HeadquarterCode);
                var headquarterId = (await _brokerListContext.HeadquarterBrokers.Where(headquarterPredicate).FirstOrDefaultAsync())?.HeadquarterBrokerId;
                if (headquarterId == null) return brokerRespServiceModels;
                else
                {
                    headquarterPredicate = headquarterPredicate.And(x => x.Code == queryServiceModel.HeadquarterCode);
                    branchPredicate = branchPredicate.And(x => x.HeadquarterBrokerId == headquarterId);
                }
            }
            if (queryServiceModel.StartDate != null)
            {
                headquarterPredicate = headquarterPredicate.And(x => x.EstablishmentDate >= queryServiceModel.StartDate);
                branchPredicate = branchPredicate.And(x => x.EstablishmentDate >= queryServiceModel.StartDate);
            }

            if (queryServiceModel.EndDate != null)
            {
                headquarterPredicate = headquarterPredicate.And(x => x.EstablishmentDate <= queryServiceModel.EndDate);
                branchPredicate = branchPredicate.And(x => x.EstablishmentDate <= queryServiceModel.EndDate);
            }
            var headquarterBrokers = await _brokerListContext.HeadquarterBrokers.Where(headquarterPredicate).ToListAsync();
            var branchBrokers =  await _brokerListContext.BranchBrokers.Where(branchPredicate).ToListAsync();
            brokerRespServiceModels.AddRange(_mapper.Map<List<BrokerRespServiceModel>>(headquarterBrokers));
            brokerRespServiceModels.AddRange(_mapper.Map<List<BrokerRespServiceModel>>(branchBrokers));
            return brokerRespServiceModels;
        }

    }
}
