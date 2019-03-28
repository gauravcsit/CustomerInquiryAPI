using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerInquiryAPI.Models
{
    public class CustomerInquiryEntity
    {
        public decimal CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string ContactEmail { get; set; }
        public Nullable<decimal> MobileNo { get; set; }

        public List<Process_Transaction> RecentTransactions { get; set; }


    }
}