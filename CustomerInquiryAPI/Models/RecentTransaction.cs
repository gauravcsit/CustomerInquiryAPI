using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerInquiryAPI.Models
{
    public class RecentTransaction
    {

        public Int64 id { get; set; }

        public string date { get; set; }

        public decimal? amount { get; set; }

        public string currency { get; set; }

        public string status { get; set; }        
    }
}
