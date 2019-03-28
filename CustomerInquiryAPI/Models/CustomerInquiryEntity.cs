using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerInquiryAPI.Models
{
    public class CustomerInquiryEntity
    {
        public Int64 customerId { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public Int64? mobile { get; set; }                       

        public List<RecentTransaction> transactions { get; set; }
    }
}