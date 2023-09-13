using AutoMapper;
using BrokerListService.Models;
using BrokerListService.OpenAPIClients;
using BrokerListService.Utils;

namespace BrokerListService.Profiles
{
    public class BranchBrokerProfile:Profile
    {
        public BranchBrokerProfile() 
        {
            CreateMap<BranchBrokerViewModel, BranchBroker>()
                .ForMember(
                    member => member.PublicationDate,
                    opt => opt.MapFrom(src => DatetimeTools.GetDatetimeFromZHTWFormat(src.PublicationDate))
                )
                .ForMember(
                    member => member.EstablishmentDate,
                    opt => opt.MapFrom(src => DatetimeTools.GetDatetimeFromZHTWFormat(src.EstablishmentDate))
                );
        }
    }
}
