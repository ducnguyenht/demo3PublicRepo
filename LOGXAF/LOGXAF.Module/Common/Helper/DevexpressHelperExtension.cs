using DevExpress.Persistent.AuditTrail;
using DevExpress.Xpo;
using NASDMS.Module.Common.Helper;
using NASDMS.RDS;
using NASDMS.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class DevexpressHelperExtension
{

    public static List<NASDMS.Module.Common.NonPersistents.History> LoadHistory(Guid Oid, Session session)
    {
        NASDMS.RDS.Services.AuditTrailServices.IAuditTrailService AuditTrailService = new NASDMS.RDS.Services.AuditTrailServices.AuditTrailService();
        List<NASDMS.Module.Common.NonPersistents.History> lstH = new List<NASDMS.Module.Common.NonPersistents.History>();
        try
        {
            List<NASDMS.RDS.AuditTrail> auditTrails = new List<NASDMS.RDS.AuditTrail>();
            AuditTrailService.GetAuditTrails(ref auditTrails, Oid);
            foreach (var item in auditTrails)
            {
                NASDMS.Module.Common.NonPersistents.History h = new NASDMS.Module.Common.NonPersistents.History(session);
                h.ChangedBy = item.ChangedBy;
                h.Data = item.Data;
                h.ChangedOn = item.ChangedOn.Value;
                h.Action = item.ActionAudit;
                h.Category = item.CategoryAudit;
                lstH.Add(h);
            }
        }
        catch (Exception)
        {
            lstH = new List<NASDMS.Module.Common.NonPersistents.History>();
        }
        return lstH;
    }
    public static object GetPropValue(this object src, string propName)
    {
        return src.GetType().GetProperty(propName).GetValue(src, null);
    }
    public static void AddDeletedToHistory(this object obj,ref Master helper, CategoryAudit category, object currentObj)
    {
        if (obj != null)
        {
            var find = helper.deleteds.Find(x => x.Item1.ToString() == obj.GetPropValue("Oid").ToString());
            if (find == null)
            {
                helper.deleteds.Add(new Tuple<Guid, Enum, object>(new Guid(obj.GetPropValue("Oid").ToString()), NASDMS.Systems.CategoryAudit.DomainObject1, currentObj));
            }
        }
    }

    public static AuditTrail GetAuditTrailByGuid(this Master HistoryHelper, ref AuditTrail audit, Guid Myself)
    {
        NASDMS.RDS.Services.AuditTrailServices.IAuditTrailService AuditTrailService = new NASDMS.RDS.Services.AuditTrailServices.AuditTrailService();
        AuditTrailService.GetAuditTrailByGuid(ref audit, Myself);
        return audit;
    }

    public static void ToHistory(this Master HistoryHelper, Guid Oid,  Guid Myself,string ObjToString, string ChangedBy, CategoryAudit Category, bool IsNewObject = false, string NameObjectDeleted = null)
    {
        NASDMS.RDS.Services.AuditTrailServices.IAuditTrailService AuditTrailService = new NASDMS.RDS.Services.AuditTrailServices.AuditTrailService();
        if (IsNewObject)
        {//ActionAudit.Created
            var groupNew = HistoryHelper.list.Where(o => o.action == NASDMS.Module.Common.Helper.Action.Created).GroupBy(o => o.Oid).ToDictionary(o => o.Key, o => o.ToList<Detail>());
            var select1 = groupNew.Where(o => o.Key == Myself).FirstOrDefault();
            AuditTrailService.AddAuditTrail(Oid, Myself, ChangedBy, select1.Value.ToDescriptionHistory(), Category, ActionAudit.Created);
            groupNew.Remove(select1.Key);
        }
        else
        {
            if (NameObjectDeleted == null)
            {//ActionAudit.Updated             
                var groupUpdate = HistoryHelper.list.Where(o => o.action == NASDMS.Module.Common.Helper.Action.Updated && o.Oid == HistoryHelper.Oid).GroupBy(o => o.Oid).ToDictionary(o => o.Key, o => o.ToList<Detail>());
                foreach (var item in groupUpdate.Values)
                {
                    AuditTrailService.AddAuditTrail(Oid, Myself, ChangedBy, ObjToString + Environment.NewLine + item.ToDescriptionHistory(), Category, ActionAudit.Updated);
                }
                foreach (var items in groupUpdate.Values)
                {
                    foreach (var item in items)
                    {
                        HistoryHelper.list.Remove(item);
                    }
                }
            }
            else
            {//ActionAudit.Deleted
                AuditTrailService.AddAuditTrail(Oid, Myself, ChangedBy, NameObjectDeleted, Category, ActionAudit.Deleted);
            }
        }
        return;
    }

    public static string ToOldDescriptionHistory(this List<Detail> lst)
    {
        string log = "";
        if (lst != null)
        {
            foreach (var listHistory in lst)
            {
                log += listHistory.propertyName + ": " + listHistory.oldValue + " ;";
            }
            return log;
        }
        return "";
    }

    public static string ToDescriptionHistory(this List<Detail> lst)
    {
        string log = "";
        if (lst != null)
        {
            foreach (var listHistory in lst)
            {
                log += listHistory.propertyName + ": " + listHistory.oldValue + " => " + listHistory.newValue + Environment.NewLine;
            }
            return log;
        }
        return "";
    }



    public static string ToCustomString(this object obj)
    {
        string ret = "N/A";
        if (obj == null)
        {
            return ret;
        }
        switch (Type.GetTypeCode(obj.GetType()))
        {
            case TypeCode.DateTime:
                var date = obj.ToObject<DateTime>();
                if (date != default(DateTime))
                    ret = date.ToDateTime();
                break;
            case TypeCode.Decimal:
                ret = obj.ToObject<Decimal>().ToQuantity();
                break;
            default:
                if (obj.GetType().IsEnum)
                {
                    ret = obj.ToObject<Enum>().ToEnumLocalization();
                }
                else
                {
                    ret = obj.ToString();
                }
                break;
        }
        return ret;
    }
    /// <summary>
    /// Localize Property Field(Name -> Tên).
    /// </summary>
    public static string ToLocalization(this string str, object T)
    {
        return DevExpress.ExpressApp.Utils.CaptionHelper.GetMemberCaption(T.GetType(), str);
    }
    /// <summary>
    /// Localize Enum Field( Enum.Status -> Trạng thái).
    /// </summary>
    public static string ToEnumLocalization(this Enum str)
    {
        return DevExpress.ExpressApp.Utils.CaptionHelper.GetDisplayText(str);
    }
    public static string ToChildHistory(this string str)
    {
        return "\t\t" + str;
    }
    public static string WithChildTable(this string str, object T)
    {
        return T.GetType().Name + Environment.NewLine + str;
    }
    /// <summary>
    /// dd/MM/yyyy hh:mm:ss
    /// </summary>
    /// <param name="dt">The dt.</param>
    /// <returns>dd/MM/yyyy hh:mm:ss</returns>
    public static string ToDateTime(this DateTime dt)
    {
        return dt.ToString("dd/MM/yyyy hh:mm:ss");
    }
    public static string ToDateTime(this DateTime? dt)
    {
        return dt.Value.ToString("dd/MM/yyyy hh:mm:ss");
    }
    /// <summary>
    /// To the price #,##0 : 30,000
    /// </summary>
    public static string ToPrice(this decimal? de)
    {
        return de.Value.ToString("#,##0");
    }
    /// <summary>
    /// To the price? #,##0 : 30,000
    /// </summary>
    public static string ToPrice(this decimal de)
    {
        return de.ToString("#,##0");
    }
    /// <summary>
    /// To the quantity #,##0.00
    /// </summary>
    public static string ToQuantity(this decimal? de)
    {
        return de.Value.ToString("#,##0.00");
    }
    /// <summary>
    /// To the quantity? #,##0.00
    /// </summary>
    public static string ToQuantity(this decimal de)
    {
        return de.ToString("#,##0.00");
    }
    public static T ToObject<T>(this object obj)
    {
        return (T)obj;
    }
}

