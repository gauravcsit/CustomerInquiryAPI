using CustomerInquiryAPI.DAL;
using CustomerInquiryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomerInquiryAPI.Controllers
{
    public class InquiryController : ApiController
    {
        InquiryDBManager dbManager = new InquiryDBManager();

        // POST api/Inquiry/InquiryCustomer/inquiry
        public HttpResponseMessage InquiryCustomer([FromBody]Inquiry inquiry)
        {
            this.InquiryCustomer1(inquiry);

            var response = Request.CreateResponse<Inquiry>(HttpStatusCode.Created, inquiry);

            return response;
        }

        // POST api/Inquiry/InquiryCustomer/inquiry
        [AcceptVerbs("GET", "POST")]
        public CustomerInquiryEntity InquiryCustomer1([FromBody]Inquiry inquiry)
        {
            return dbManager.InquiryCustomerByCustomerId(inquiry);
        }

        // POST api/Inquiry/InquiryCustomer/customerId,email
        [AcceptVerbs("GET", "POST")]
        public string InquiryCustomer()
        {
            return "Bad Request";
        }

    }
}
