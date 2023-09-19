using AutoMapper;
using BrokerListService.Helpers.Interface;
using BrokerListService.Models;
using BrokerListService.Repositories.Interface;
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
        private readonly IBrokerHelper _brokerHelper;
        private readonly IHeadquarterBrokerRepository _headquarterBrokerRepository;
        private readonly IBranchBrokerRepository _branchBrokerRepository;
        public BrokerListGenerateService(IMapper mapper, IBrokerHelper brokerHelper, IHeadquarterBrokerRepository headquarterBrokerRepository, IBranchBrokerRepository branchBrokerRepository)
        {
            _mapper = mapper;
            _brokerHelper = brokerHelper;
            _headquarterBrokerRepository = headquarterBrokerRepository;
            _branchBrokerRepository = branchBrokerRepository;
        }

        public async Task<int> GetDataFromApiAsync()
        {
            var oldBranchBrokers = await _branchBrokerRepository.GetListAsync(x => true);
            var oldHeadquarterBrokers = await _headquarterBrokerRepository.GetListAsync(x => true);
            _ = await _branchBrokerRepository.DeleteListAsync(oldBranchBrokers);
            _ = await _headquarterBrokerRepository.DeleteListAsync(oldHeadquarterBrokers);
            var headquarterBrokerServiceModels = await _brokerHelper.GetHeadquarterBrokerAsync();
            var headquarterBrokers = _mapper.Map<List<HeadquarterBroker>>(headquarterBrokerServiceModels);
            var branchBrokersServiceModels = await _brokerHelper.GetBranchBrokerAsync();
            var branchBrokers = _mapper.Map<List<BranchBroker>>(branchBrokersServiceModels);
            var rowschanges = await InsertToDBAsync(headquarterBrokers, branchBrokers, "admin");
            return rowschanges;
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
            rowschanges = await _headquarterBrokerRepository.InsertListAsync(headquarterBrokers);
            return rowschanges;
        }
    }
}
