using NASDMS.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using db = NASDMS.RDS.NASDMSContext;
namespace NASDMS.RDS.ORM.Helper
{
    public static class RDSAuditTrail
    {
        internal static List<AuditTrail> GetAuditTrails(Guid Oid)
        {
            try
            {
                string oid = Oid.ToString();
                if (oid == "1e01c667-8f46-4e83-83dd-6b4d8aef701f")
                {
                    return db.AuditTrails.All(orderBy: "ChangedOn DESC").ToList();
                }
                var auditTrails = db.AuditTrails.All(where: "Oid=@0", parms: new object[] { oid }, orderBy: "ChangedOn DESC");
                return auditTrails.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        internal static void AddAuditTrail(Guid Oid, string ChangedBy, string Data, CategoryAudit category, ActionAudit action)
        {
            try
            {
                AuditTrail h = new AuditTrail();
                h.Oid = Oid.ToString();
                h.ChangedBy = ChangedBy;
                h.Data = Data;
                h.ChangedOn = DateTime.Now;
                h.CategoryAudit = category;
                h.ActionAudit = action;
                db.AuditTrails.Insert(h);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
