using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiotVietBO
{
    public class DatHangSoldBy
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public int Type { get; set; }
        public string UserName { get; set; }
        public bool isDeleted { get; set; }
    }

    public class DatHangSeller
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Email { get; set; }
        public string GivenName { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public int Type { get; set; }
        public string UserName { get; set; }
        public bool isDeleted { get; set; }
    }

    public class DatHangOrderDetail
    {
        public int BasePrice { get; set; }
        public bool IsLotSerialControl { get; set; }
        public bool IsRewardPoint { get; set; }
        public int Price { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public int DiscountRatio { get; set; }
        public string ProductCode { get; set; }
    }

    public class DatHangDeliveryDetail
    {
        public int Type { get; set; }
        public string TypeName { get; set; }
        public int Status { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string Receiver { get; set; }
        public int DeliveryBy { get; set; }
        public string LocationName { get; set; }
        public string WardName { get; set; }
        public string LastLocation { get; set; }
        public string LastWard { get; set; }
        public int Weight { get; set; }
        public int UsingPriceCod { get; set; }
    }

    public class DatHangOrder
    {
        public int BranchId { get; set; }
        public int RetailerId { get; set; }
        public int CustomerId { get; set; }
        public int SoldById { get; set; }
        public DatHangSoldBy SoldBy { get; set; }
        public int SaleChannelId { get; set; }
        public DatHangSeller Seller { get; set; }
        public string Code { get; set; }
        public List<DatHangOrderDetail> OrderDetails { get; set; }
        public List<object> InvoiceOrderSurcharges { get; set; }
        public List<object> Payments { get; set; }
        public DatHangDeliveryDetail DeliveryDetail { get; set; }
        public int UsingCod { get; set; }
        public int Status { get; set; }
        public int Total { get; set; }
        public string Extra { get; set; }
        public int Surcharge { get; set; }
        public int Type { get; set; }
        public string Uuid { get; set; }
        public string addToAccount { get; set; }
        public int PayingAmount { get; set; }
        public int TotalBeforeDiscount { get; set; }
        public int ProductDiscount { get; set; }
        public string Description { get; set; }
    }

    public class RootDatHangBO
    {
        public DatHangOrder Order { get; set; }
    }
}
