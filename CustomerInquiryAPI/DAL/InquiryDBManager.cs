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
            decimal CustomerId;
            if (!Decimal.TryParse(inquiry.CustomerId, out CustomerId))
            {

            }
                    
            var Master_Customer = (from c in context.Master_Customer
                                   where (c.CustomerId == CustomerId && c.ContactEmail == inquiry.Email)
                                    || (c.CustomerId == CustomerId && inquiry.Email == null)
                                    || (CustomerId == 0 && c.ContactEmail == inquiry.Email)

                                   select new
                                   {
                                       CustomerId = c.CustomerId,
                                       CustomerName = c.CustomerName,
                                       ContactEmail = c.ContactEmail,
                                       MobileNo = c.MobileNo
                                   }).ToList();

            var inquiryResponse = (from c in Master_Customer
                                   select new CustomerInquiryEntity
                                   {
                                       customerId = (Int64)c.CustomerId,
                                       name = c.CustomerName,
                                       email = c.ContactEmail,
                                       mobile = (Int64)c.MobileNo
                                   }).FirstOrDefault();

            if (inquiryResponse != null)
            {
                var Process_Transaction = (from c in context.Process_Transaction
                                           where c.Master_Customer.CustomerId == inquiryResponse.customerId
                                           select new
                                                {
                                                    CustomerId = c.Master_Customer.CustomerId,
                                                    TransactionId = c.TransactionId,
                                                    TransactionDateTime = c.TransactionDateTime,
                                                    CurrencyCode = c.CurrencyCode,
                                                    Status = c.Status,
                                                    Amount = c.Amount

                                                }).ToList();

                inquiryResponse.transactions = (from c in Process_Transaction
                                                select new RecentTransaction
                                                {
                                                    id = (int)c.TransactionId,
                                                    date = c.TransactionDateTime.Value.ToString("dd/MM/yyyy HH:MM"),
                                                    currency = c.CurrencyCode,
                                                    status = c.Status,
                                                    amount = c.Amount

                                                }).ToList();
            }

            return inquiryResponse;
        }
    }
}