using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using TestLogXAF.Module.BusinessObjects;
using NASDMS.Module.Common.Helper;
using NASDMS.Module.Common.NonPersistents;

namespace LOGXAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    public class DomainObject2 : BaseObject
    {
        public DomainObject2(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private string _Name;
        private string _Code;

        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                SetPropertyValue("Code", ref _Code, value);
            }
        }

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue("Name", ref _Name, value);
            }
        }

        [Association("DomainObject2-DomainObject3s")]
        public XPCollection<DomainObject3> DomainObject3s
        {
            get
            {
                return GetCollection<DomainObject3>("DomainObject3s");
            }
        }
        public List<History> History
        {
            get
            {
                return DevexpressHelperExtension.LoadHistory(this.Oid, Session);
            }
        }
        private Master helper = new Master();
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            if (!IsSaving && !IsLoading)
            {
                base.OnChanged(propertyName, oldValue, newValue);
                try
                {
                    string[] IgnoreProperty = { "OptimisticLockField", "OptimisticLockFieldInDataLayer" };
                    if (oldValue != newValue && !IgnoreProperty.Contains(propertyName))
                    {
                        helper.UpdateDetail(propertyName.ToLocalization(this), oldValue.ToCustomString(), newValue.ToCustomString(), this.Session.IsNewObject(this));
                    }
                }
                catch (Exception) { }
            }
        }
        public override string ToString()
        {
            return "Code".ToLocalization(this) + " : " + this.Code + " ; "
                 + "Name".ToLocalization(this) + " : " + this.Name;
        }
        protected override void OnSaving()
        {
            base.OnSaving();
            #region AuditTrail
            try
            {
                if (!IsDeleted)
                {
                    helper.ToHistory(this.Oid, this.ToString(), "user A", NASDMS.Systems.CategoryAudit.DomainObject1, Session.IsNewObject(this));
                }
                else
                {
                    helper.ToHistory(this.Oid, "", "user A", NASDMS.Systems.CategoryAudit.DomainObject1, false, this.ToString());
                }
                OnChanged("History");
            }
            catch (Exception) { }
            #endregion AuditTrail
        }
    }
}
