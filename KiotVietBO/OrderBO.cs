using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiotVietBO
{
    public class OrderDetailBO
    {
        public int productId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
        public int discount { get; set; }
        public int discountRatio { get; set; }
    }

    public class PartnerDeliveryBO
    {
        public string code { get; set; }
        public string name { get; set; }
        public string contactNumber { get; set; }
    }

    public class OrderDeliveryBO
    {
        public int type { get; set; }
        public int price { get; set; }
        public string receiver { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public int weight { get; set; }
        public int length { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int partnerDeliveryId { get; set; }
        public PartnerDeliveryBO partnerDelivery { get; set; }
    }

    public class RootOrderBO
    {
        public int branchId { get; set; }
        public int soldById { get; set; }
        public int customerId { get; set; }
        public int total { get; set; }
        public int discount { get; set; }
        public bool makeInvoice { get; set; }
        public int status { get; set; }
        public int retailerId { get; set; }
        public string statusValue { get; set; }
        public string description { get; set; }
        public string method { get; set; }
        public List<OrderDetailBO> orderDetails { get; set; }
        public OrderDeliveryBO orderDelivery { get; set; }
    }
}
