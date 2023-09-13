using AutoMapper;
using BrokerListService.Models;
using BrokerListService.ServiceModel;
using BrokerListService.Utils;
using BrokerListService.ViewModel;

namespace BrokerListService.Profiles
{
    public class BrokerProfile:Profile
    {
        public BrokerProfile() 
        {
            CreateMap<BrokerQueryViewModel, BrokerQueryServiceModel>()
                .ForMember(
                    member => member.StartDate,
                    opt => opt.MapFrom(src => DatetimeTools.GetDatetimeFromZHTWFormat(src.StartDate))
                ).ForMember(
                    member => member.EndDate,
                    opt => opt.MapFrom(src => DatetimeTools.GetDatetimeFromZHTWFormat(src.EndDate))
                );
            CreateMap<BrokerRespServiceModel, BrokerRespViewModel>()
                .ForMember(
                    member => member.EstablishmentDate,
                    opt => opt.MapFrom(src => DatetimeTools.GetZHTWDatetimeString(src.EstablishmentDate))
                );
            CreateMap<HeadquarterBroker, BrokerRespServiceModel>();
            CreateMap<BranchBroker, BrokerRespServiceModel>();
        }
    }
}
