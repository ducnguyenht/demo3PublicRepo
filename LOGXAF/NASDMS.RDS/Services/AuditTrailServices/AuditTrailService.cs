using NASDMS.RDS.ORM.Helper;
using NASDMS.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASDMS.RDS.Services.AuditTrailServices
{
    public class AuditTrailService : IAuditTrailService
    {
        public static bool IsReadyForLogging = true;
        public NASDMS.Systems.ErrorCode GetAuditTrails(ref List<AuditTrail> auditTrails, Guid Oid)
        {
            ErrorCode err = ErrorCode.ERROR_SUCCESS;
            if (IsReadyForLogging == false)
            {
                return err;
            }
            try
            {
                auditTrails = RDSAuditTrail.GetAuditTrails(Oid);
                return err;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public ErrorCode AddAuditTrail(Guid Oid, Guid MySelf, string ChangedBy, string Data, CategoryAudit category, ActionAudit action)
        {
            ErrorCode err = ErrorCode.ERROR_SUCCESS;
            if (IsReadyForLogging == false)
            {
                return err;
            }
            try
            {
                RDSAuditTrail.AddAuditTrail(Oid, MySelf, ChangedBy, Data, category, action);
                return err;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public ErrorCode GetAuditTrailByGuid(ref AuditTrail auditTrail, Guid MySelf)
        {
            ErrorCode err = ErrorCode.ERROR_SUCCESS;
            if (IsReadyForLogging == false)
            {
                return err;
            }
            try
            {
                auditTrail = RDSAuditTrail.GetAuditTrailByGuid(MySelf);
                return err;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
