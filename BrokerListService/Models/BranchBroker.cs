using System;
using System.Collections.Generic;

namespace BrokerListService.Models
{
    public partial class BranchBroker
    {
        public int BranchBrokerId { get; set; }
        public int HeadquarterBrokerId { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? EstablishmentDate { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual HeadquarterBroker HeadquarterBroker { get; set; }
    }
}
