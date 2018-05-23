using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVer1.BLL.ParseInvoice
{
    public class Properties
    {
    }

    public class InvoiceDetail
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public object DiscountRatio { get; set; }
        public object ProductFormulaHistoryId { get; set; }
    }

    public class Payment
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Amount { get; set; }
        public object AccountId { get; set; }
        public object BankAccount { get; set; }
        public object Description { get; set; }
        public string Method { get; set; }
        public int Status { get; set; }
        public string StatusValue { get; set; }
        public DateTime TransDate { get; set; }
    }

    public class Datum
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int SoldById { get; set; }
        public string SoldByName { get; set; }
        public object CustomerId { get; set; }
        public object CustomerCode { get; set; }
        public object CustomerName { get; set; }
        public double Total { get; set; }
        public double TotalPayment { get; set; }
        public object Discount { get; set; }
        public object DiscountRatio { get; set; }
        public int Status { get; set; }
        public string StatusValue { get; set; }
        public object Description { get; set; }
        public bool UsingCod { get; set; }
        public object ModifiedDate { get; set; }
        public object InvoiceDelivery { get; set; }
        public List<InvoiceDetail> InvoiceDetails { get; set; }
        public List<Payment> Payments { get; set; }
    }

    public class Notification
    {
        public string Action { get; set; }
        public List<Datum> Data { get; set; }
    }

    public class RootInvoice
    {
        public string Id { get; set; }
        public int Attempt { get; set; }
        public Properties Properties { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}