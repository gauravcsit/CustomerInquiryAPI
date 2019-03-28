using CustomerInquiryAPI.DAL;
using CustomerInquiryAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace CustomerInquiryAPI.Controllers
{
    public class InquiryController : ApiController
    {
        InquiryDBManager dbManager = new InquiryDBManager();

        // POST api/Inquiry
        [AcceptVerbs("GET", "POST")]
        //[HttpPost]
        public HttpResponseMessage Post([FromBody]Inquiry inquiry)
        {
            string strError = string.Empty;
            bool validCustomerId = false;
            bool validEmail = false;

            try
            {
                if (inquiry == null || (inquiry.CustomerId == null && inquiry.Email == null))
                {
                    return Request.CreateResponse<string>(HttpStatusCode.Created, "Bad Request! No inquiry criteria");
                }

                decimal CustomerId;
                if (!Decimal.TryParse(inquiry.CustomerId, out CustomerId))
                {
                    
                }
                               
                // Validations
                if (inquiry.Email != null && inquiry.Email != string.Empty && IsValidEmail(inquiry.Email))
                {
                    validEmail = true;
                }
                else if (inquiry.Email != null)
                {
                    strError = "Bad Request! Invalid Email";
                }


                if (inquiry.CustomerId != null && CustomerId > 0 && CustomerId <= 9999999999 && CustomerId== (int)CustomerId)   // Valid CustomerID
                {
                    validCustomerId = true;
                }
                else if (inquiry.CustomerId != null)
                {
                    strError = "Bad Request! Invalid Customer Id";
                }

                if ((validCustomerId || validEmail) && strError == string.Empty)
                {
                    CustomerInquiryEntity customer = this.InquiryCustomer(inquiry);

                    if (customer != null)
                    {
                        var result = Newtonsoft.Json.JsonConvert.SerializeObject(customer, Formatting.Indented);
                        var temp = Request.CreateResponse(HttpStatusCode.Created, result);
                        return temp;
                    }
                    else
                    {
                        strError = "Not Found";
                    }
                }
                else
                {
                    if (strError == string.Empty)
                    {
                        strError = "Bad Request!";
                    }
                }

                return Request.CreateResponse<string>(HttpStatusCode.Created, strError);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse<string>(HttpStatusCode.Created, "Bad Request");
            }
        }

        private bool IsValidEmail(string email)
        {
            Regex rx = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
            return rx.IsMatch(email);
        }

        private CustomerInquiryEntity InquiryCustomer(Inquiry inquiry)
        {
            return dbManager.InquiryCustomerByCustomerId(inquiry);
        }
    }
}
