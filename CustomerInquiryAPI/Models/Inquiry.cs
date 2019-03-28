using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerInquiryAPI.Models
{
    public class Inquiry
    {       
        public decimal CustomerId { get; set; }

        public string Email { get; set; }
    }
}