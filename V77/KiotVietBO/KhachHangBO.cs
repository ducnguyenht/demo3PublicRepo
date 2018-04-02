using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiotVietBO
{
    public class KhachHangBO
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public bool gender { get; set; }
        public DateTime birthDate { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string comments { get; set; }
    }
    public class RootKhachHangBO
    {
        public int total { get; set; }
        public int pageSize { get; set; }
        public List<KhachHangBO> data { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class RetKhachHangBO
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public bool gender { get; set; }
        public DateTime birthDate { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public int retailerId { get; set; }
        public int branchId { get; set; }
        public string locationName { get; set; }
        public string email { get; set; }
        public DateTime createdDate { get; set; }
        public int type { get; set; }
        public string taxCode { get; set; }
        public string comments { get; set; }
        public int debt { get; set; }
    }

    public class RootRetKhachHangBO
    {
        public int total { get; set; }
        public int pageSize { get; set; }
        public List<RetKhachHangBO> data { get; set; }
        public DateTime timestamp { get; set; }
    }
}
