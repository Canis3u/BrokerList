using AutoMapper;
using BrokerListService.Models;
using BrokerListService.OpenAPIClients;
using BrokerListService.Utils;

namespace BrokerListService.Profiles
{
    public class HeadquarterBrokerProfile : Profile
    {
        public HeadquarterBrokerProfile()
        {
            CreateMap<HeadquarterBrokerViewModel, HeadquarterBroker>()
                .ForMember(
                    member => member.EstablishmentDate,
                    opt => opt.MapFrom(src => DatetimeTools.GetDatetimeFromZHTWFormat(src.EstablishmentDate))
                );
        }
    }
}
