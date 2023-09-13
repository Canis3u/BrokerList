using System;

namespace BrokerListService.ServiceModel
{
    public class BrokerQueryServiceModel
    {
        public string HeadquarterCode { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
