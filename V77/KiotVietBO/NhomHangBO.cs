using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiotVietBO
{
    public class NhomHang2BO
    {
        public int categoryId { get; set; }
        public int parentId { get; set; }
        public string categoryName { get; set; }
        public int retailerId { get; set; }
        public bool hasChild { get; set; }
        public DateTime createdDate { get; set; }
    }
    public class NhomHangBO
    {
        public int categoryId { get; set; }
        public int parentId { get; set; }
        public string categoryName { get; set; }
        public int retailerId { get; set; }
        public bool hasChild { get; set; }
        public DateTime modifiedDate { get; set; }
        public DateTime createdDate { get; set; }
        public int level { get; set; }
        public List<NhomHangBO> children { get; set; }
    }
    public class DSNhomHangBo
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public int retailerId { get; set; }
        public bool hasChild { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
        public List<NhomHangBO> children { get; set; }
    }
    public class RootNhomHangBO
    {
        public List<object> removedIds { get; set; }
        public int total { get; set; }
        public int pageSize { get; set; }
        public List<NhomHangBO> data { get; set; }
        public DateTime timestamp { get; set; }
    }
}
