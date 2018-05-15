using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiotVietBO
{
   
    public class HangHoaAttributeBO
    {
        public int productId { get; set; }
        public string attributeName { get; set; }
        public string attributeValue { get; set; }
    }

    public class HangHoaBO
    {
        public int id { get; set; }
        public int retailerId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string fullName { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public bool allowsSale { get; set; }
        public int type { get; set; }
        public bool hasVariants { get; set; }
        public int basePrice { get; set; }
        public string unit { get; set; }
        public int conversionValue { get; set; }
        public DateTime modifiedDate { get; set; }
        public bool isActive { get; set; }
        public List<string> images { get; set; }
        public List<HangHoaAttributeBO> attributes { get; set; }
        public string description { get; set; }
    }

    public class RootHangHoaBO
    {
        public int total { get; set; }
        public int pageSize { get; set; }
        public List<HangHoaBO> data { get; set; }
        public DateTime timestamp { get; set; }   
    }
    public class HangHoaInventoryBO
    {
        public int productId { get; set; }
        public int branchId { get; set; }
        public string branchName { get; set; }
        public int cost { get; set; }
        public int onHand { get; set; }
        public int reserved { get; set; }
    }

    public class HangHoaPriceBookBO
    {
        public int productId { get; set; }
        public int priceBookId { get; set; }
        public string priceBookName { get; set; }
        public int price { get; set; }
        public bool isActive { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }

    public class ChiTietHangHoaBO
    {
        [Key]
        public int id { get; set; }
        public int retailerId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string fullName { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public bool allowsSale { get; set; }
        public int type { get; set; }
        public bool hasVariants { get; set; }
        public int basePrice { get; set; }
        public string unit { get; set; }
        public int masterProductId { get; set; }
        public int conversionValue { get; set; }
        public string description { get; set; }
        public DateTime modifiedDate { get; set; }
        public bool isActive { get; set; }
        public List<HangHoaInventoryBO> inventories { get; set; }
        public List<HangHoaPriceBookBO> priceBooks { get; set; }
        public List<HangHoaAttributeBO> attributes { get; set; }
        public List<string> images { get; set; }

        public int ConHang
        {
            get {
                int conhang = 0;
                if (inventories!=null)
                {
                    foreach (var item in inventories)
                    {
                        conhang += item.onHand;
                    }
                }
                return conhang; 
            }
        }
      
    }
    public class RootChiTietHangHoaBO
    {
        public int total { get; set; }
        public int pageSize { get; set; }
        public List<ChiTietHangHoaBO> data { get; set; }
        public DateTime timestamp { get; set; }
    }
}
