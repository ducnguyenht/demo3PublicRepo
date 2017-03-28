using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASDMS.Module.Common.Helper
{
    public class Master
    {
        public Master()
        {
            list = new List<Detail>();
            deleted = new List<object>();
            deleteds = new List<Tuple<Guid, Enum, object>>();
        }
        public List<Detail> list { get; set; }
        public List<object> deleted { get; set; }
        public List<Tuple<Guid, Enum, object>> deleteds { get; set; }
        public Guid Oid { get; set; }
        public Guid OidDeleted { get; set; }
        public void UpdateDetail(string propertyName, string oldValue, string newValue, bool IsNewObject)
        {
            if (IsNewObject)
            {
                Detail detail = new Detail();
                detail.propertyName = propertyName;
                detail.oldValue = oldValue;
                detail.newValue = newValue;
                detail.Oid = this.Oid;
                detail.action = Action.Created;
                list.Add(detail);
            }
            else
            {
                Detail detail = new Detail();
                detail.propertyName = propertyName;
                detail.oldValue = oldValue;
                detail.newValue = newValue;
                detail.Oid = this.Oid;
                detail.action = Action.Updated;
                list.Add(detail);
            }
        }
        public string DescriptionTemp { get; set; }
        public string DescriptionHistory()
        {
            DescriptionTemp = "";
            if (list != null)
            {
                foreach (var listHistory in list)
                {
                    DescriptionTemp += listHistory.propertyName + ": " + listHistory.oldValue + " => " + listHistory.newValue + Environment.NewLine;
                }
                return DescriptionTemp;
            }
            return @"";
        }
    }
    public class Detail
    {
        public override string ToString()
        {
            return this.propertyName + ": " + this.oldValue + " => " + this.newValue + Environment.NewLine; ;
        }
        public string propertyName { get; set; }
        public string oldValue { get; set; }
        public string newValue { get; set; }
        public Guid Oid { get; set; }
        public Action action { get; set; }
    }
    public enum Action
    {
        Created,
        Updated,
        Deleted
    }
}
