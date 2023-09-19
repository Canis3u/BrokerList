using AutoMapper;
using BrokerListService.Extensions;
using BrokerListService.Models;
using BrokerListService.Repositories;
using BrokerListService.Repositories.Interface;
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
        private readonly IHeadquarterBrokerRepository _headquarterBrokerRepository;
        private readonly IBranchBrokerRepository _branchBrokerRepository;

        public BrokerService(IMapper mapper, IHeadquarterBrokerRepository headquarterBrokerRepository, IBranchBrokerRepository branchBrokerRepository)
        {
            _mapper = mapper;
            _headquarterBrokerRepository = headquarterBrokerRepository;
            _branchBrokerRepository = branchBrokerRepository;
        }

        public async Task<int> DeleteBrokerAsync(string deleteCode)
        {
            int rowschanges = 0;
            if (deleteCode == "All")
            {
                var branchBrokers = await _branchBrokerRepository.GetListAsync(x => true);
                var headquarterBrokers = await _headquarterBrokerRepository.GetListAsync(x => true);
                rowschanges += await _branchBrokerRepository.DeleteListAsync(branchBrokers);
                rowschanges += await _headquarterBrokerRepository.DeleteListAsync(headquarterBrokers);
            }
            else 
            {
                var headquarterBroker = await _headquarterBrokerRepository.GetIncludeBranchAsync(x => x.Code == deleteCode);
                if (headquarterBroker != null) 
                {
                    var branchBrokers = headquarterBroker.BranchBrokers;
                    rowschanges += await _branchBrokerRepository.DeleteListAsync(branchBrokers);
                    rowschanges += await _headquarterBrokerRepository.DeleteAsync(headquarterBroker);
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
                var headquarterId = await _headquarterBrokerRepository.GetHeadquarterIdByCodeAsync(headquarterPredicate);
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
            var branchBrokers = await _branchBrokerRepository.GetListAsync(branchPredicate);
            var headquarterBrokers = await _headquarterBrokerRepository.GetListAsync(headquarterPredicate);
            brokerRespServiceModels.AddRange(_mapper.Map<List<BrokerRespServiceModel>>(headquarterBrokers));
            brokerRespServiceModels.AddRange(_mapper.Map<List<BrokerRespServiceModel>>(branchBrokers));
            return brokerRespServiceModels;
        }

    }
}
