using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NASDMS.Systems;

namespace NASDMS.RDS
{
    // Generated 03/23/2017 09:57:49

    // Add custom code inside partial class

    public partial class AuditTrail : Entity<AuditTrail>
    {
        [EnumDataType(typeof(CategoryAudit))]
        public CategoryAudit CategoryAudit
        {
            get
            {
                return (CategoryAudit)this.Category;
            }
            set
            {
                this.Category = (int)value;
            }
        }

        [EnumDataType(typeof(ActionAudit))]
        public ActionAudit ActionAudit
        {
            get
            {
                return (ActionAudit)this.Action;
            }
            set
            {
                this.Action = (int)value;
            }
        }
    }
}
