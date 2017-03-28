using NASDMS.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASDMS.RDS.Services.AuditTrailServices
{
    public interface IAuditTrailService
    {

        ErrorCode GetAuditTrails(ref List<AuditTrail> auditTrails, Guid Oid);
        ErrorCode AddAuditTrail(Guid Oid, string ChangedBy, string Data, CategoryAudit category, ActionAudit action);
    }
}
