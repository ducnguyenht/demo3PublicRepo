using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiotVietBO
{
   public class ChiNhanhBO
    {
        public int id { get; set; }
        public string branchName { get; set; }
        public string address { get; set; }
        public string locationName { get; set; }
        public string wardName { get; set; }
        public string contactNumber { get; set; }
        public int retailerId { get; set; }
       
        public DateTime createdDate { get; set; }
        public DateTime? modifiedDate { get; set; }
    }
   public class RootChiNhanhBO
   {
       public int total { get; set; }
       public int pageSize { get; set; }
       public List<ChiNhanhBO> data { get; set; }
       public DateTime timestamp { get; set; }
   }
}
