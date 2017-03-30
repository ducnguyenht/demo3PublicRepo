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
using NASDMS.Module.Common.NonPersistents;
using NASDMS.RDS;
using NASDMS.Module.Common.Helper;

namespace TestLogXAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Test1")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    public class DomainObject1 : BaseObject
    {
        public DomainObject1(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private string _Test2;
        private string _Test1;
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Test1
        {
            get
            {
                return _Test1;
            }
            set
            {
                SetPropertyValue("Test1", ref _Test1, value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Test2
        {
            get
            {
                return _Test2;
            }
            set
            {
                SetPropertyValue("Test2", ref _Test2, value);
            }
        }

        [Association("DomainObject1-DomainObject3s")]
        public XPCollection<DomainObject3> DomainObject3s
        {
            get
            {
                return GetCollection<DomainObject3>("DomainObject3s");
            }
        }

        //[Delayed]
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
                        helper.Oid = this.Oid;
                        helper.UpdateDetail(propertyName.ToLocalization(this), oldValue.ToCustomString(), newValue.ToCustomString(), this.Session.IsNewObject(this));
                    }
                }
                catch (Exception) { }
            }
        }

        public override string ToString()
        {
            return "Test1".ToLocalization(this) + " : " + this.Test1 + " ; "
                 + "Test2".ToLocalization(this) + " : " + this.Test2;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            #region AuditTrail
            try
            {
                if (!IsDeleted)
                {
                    helper.ToHistory(this.Oid, this.Oid, this.ToString(), "user A", NASDMS.Systems.CategoryAudit.DomainObject1, Session.IsNewObject(this));
                }
                else
                {
                    helper.ToHistory(this.Oid, this.Oid, "", "user A", NASDMS.Systems.CategoryAudit.DomainObject1, false, this.ToString());
                }
                OnChanged("History");
            }
            catch (Exception) { }
            #endregion AuditTrail
        }


    }
}

