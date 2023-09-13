using AutoMapper;
using BrokerListService.Helpers.Interface;
using BrokerListService.Models;
using BrokerListService.Service.Interface;
using BrokerListService.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BrokerListService.Service
{
    public class BrokerListGenerateService:IBrokerListGenrateService
    {
        private readonly IMapper _mapper;
        private IBrokerHelper _brokerHelper;
        private BrokerListContext _brokerListContext;
        public BrokerListGenerateService(IMapper mapper,IBrokerHelper brokerHelper, BrokerListContext brokerListContext)
        {
            _mapper = mapper;
            _brokerHelper = brokerHelper;
            _brokerListContext = brokerListContext;
        }
        public async Task<int> GetDataFromApiAsync()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                _brokerListContext.BranchBrokers.RemoveRange(_brokerListContext.BranchBrokers.ToList());
                _brokerListContext.HeadquarterBrokers.RemoveRange(_brokerListContext.HeadquarterBrokers.ToList());
                _ = _brokerListContext.SaveChanges();
                ts.Complete();
            }
            var headquarterBrokerServiceModels = await _brokerHelper.GetHeadquarterBrokerAsync();
            var headquarterBrokers = _mapper.Map<List<HeadquarterBroker>>(headquarterBrokerServiceModels);
            var branchBrokersServiceModels = await _brokerHelper.GetBranchBrokerAsync();
            var branchBrokers = _mapper.Map<List<BranchBroker>>(branchBrokersServiceModels);
            var rowschange = await InsertToDBAsync(headquarterBrokers, branchBrokers, "admin");
            return rowschange;
        }
        private async Task<int> InsertToDBAsync(List<HeadquarterBroker> headquarterBrokers, List<BranchBroker> branchBrokers, string userName)
        {
            int rowschanges = 0;
            headquarterBrokers.ForEach(x => {
                x.CreateUser = userName;
                x.CreateDate = DateTime.Now;
            });
            branchBrokers.ForEach(x => {
                x.CreateUser = userName;
                x.CreateDate = DateTime.Now;
            });
            foreach (var branchBroker in branchBrokers)
            {
                headquarterBrokers.Where(x => BrokerCodeTools.GetHeadCode(x.Code) == BrokerCodeTools.GetHeadCode(branchBroker.Code)).FirstOrDefault().BranchBrokers.Add(branchBroker);
            }
            using (TransactionScope ts = new TransactionScope())
            {
                await _brokerListContext.HeadquarterBrokers.AddRangeAsync(headquarterBrokers);
                rowschanges = _brokerListContext.SaveChanges();
                ts.Complete();
            }
            return rowschanges;
        }
    }
}
