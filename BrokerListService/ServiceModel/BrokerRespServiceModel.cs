using System;

namespace BrokerListService.ServiceModel
{
    public class BrokerRespServiceModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime EstablishmentDate { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
    }
}
