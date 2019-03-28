using CustomerInquiryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerInquiryAPI.DAL
{
    public class InquiryDBManager
    {
        Test_DBEntities context;

        public InquiryDBManager()
        {
            context = SharedObjectContext.Instance.Context;
        }

        internal CustomerInquiryEntity InquiryCustomerByCustomerId(Inquiry inquiry)
        {
            var inquiryResponse = (from c in context.Master_Customer
                                   where c.CustomerId == inquiry.CustomerId || c.ContactEmail == inquiry.Email
                                   select new CustomerInquiryEntity
                                   {
                                       CustomerId = c.CustomerId,
                                       CustomerName = c.CustomerName,
                                       ContactEmail = c.ContactEmail,
                                       MobileNo = c.MobileNo
                                   }).FirstOrDefault();

            if (inquiryResponse != null)
            {
                inquiryResponse.RecentTransactions = (from c in context.Process_Transaction
                                                      where c.Master_Customer.CustomerId == inquiryResponse.CustomerId
                                                      select new Process_Transaction
                                                      {
                                                          CustomerId = c.Master_Customer.CustomerId,
                                                          TransactionId = c.TransactionId,
                                                          TransactionDateTime = c.TransactionDateTime,
                                                          CurrencyCode = c.CurrencyCode,
                                                          Status = c.Status,
                                                          Amount = c.Amount

                                                      }).ToList();
            }

            return inquiryResponse;
        }
    }
}