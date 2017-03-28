using DevExpress.Xpo;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System;
using NASDMS.Module.Common.Helper;
using System.Linq;
using System.ComponentModel;
using LOGXAF.Module.BusinessObjects;
using System.Collections.Generic;
namespace TestLogXAF.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("Code")]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    public class DomainObject3 : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public DomainObject3(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (http://documentation.devexpress.com/#Xaf/CustomDocument2834).
        }
        // Fields...
        private DomainObject2 _DomainObject2;
        private DomainObject1 _DomainObject1;
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
        // Fields...
        private string _Name;

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
        [Browsable(false)]
        [Association("DomainObject1-DomainObject3s")]
        public DomainObject1 DomainObject1
        {
            get
            {
                return _DomainObject1;
            }
            set
            {
                SetPropertyValue("DomainObject1", ref _DomainObject1, value);
            }
        }


        [Association("DomainObject2-DomainObject3s")]
        [Browsable(false)]
        public DomainObject2 DomainObject2
        {
            get
            {
                return _DomainObject2;
            }
            set
            {
                SetPropertyValue("DomainObject2", ref _DomainObject2, value);
            }
        }


        private static Master helper = new Master();
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            if (!IsSaving && !IsLoading)
            {
                base.OnChanged(propertyName, oldValue, newValue);
                try
                {
                    string[] IgnoreProperty = { "DomainObject1", "GCRecord", "OptimisticLockField", "OptimisticLockFieldInDataLayer" };
                    if (oldValue != newValue && !IgnoreProperty.Contains(propertyName))
                    {
                        helper.Oid = this.Oid;
                        helper.UpdateDetail(propertyName.ToLocalization(this).ToChildHistory(), oldValue.ToCustomString(), newValue.ToCustomString(), this.Session.IsNewObject(this));

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
                bool IsNewObject = Session.IsNewObject(this);
                if (this.DomainObject1 != null)
                {
                    helper.Oid = this.Oid;
                    helper.ToHistory(this.DomainObject1.Oid, this.ToString(), "user A", NASDMS.Systems.CategoryAudit.DomainObject1, IsNewObject);
                }
                if (helper.deleteds.Count > 0 && IsDeleted)
                {
                    foreach (var item in helper.deleteds)
                    {
                        if (item.Item2.Equals(NASDMS.Systems.CategoryAudit.DomainObject1))
                        {
                            var deletedItem = item.Item3.ToObject<DomainObject3>();
                            helper.ToHistory(item.Item1, "", "user A", NASDMS.Systems.CategoryAudit.DomainObject1, IsNewObject, deletedItem.ToString());
                        }
                        if (item.Item2.Equals(NASDMS.Systems.CategoryAudit.DomainObject2))
                        {
                            var deletedItem = item.Item3.ToObject<DomainObject3>();

                            helper.ToHistory(item.Item1, "", "user A", NASDMS.Systems.CategoryAudit.DomainObject2, IsNewObject, deletedItem.ToString());
                        }

                    }
                    helper.deleteds.Clear();
                }
            }
            catch (Exception)
            {
            }
            #endregion AuditTrail
        }
        protected override void OnDeleting()
        {
            base.OnDeleting();

            if (DomainObject1 != null)
            {
                //List<Tuple<Guid, Enum, object>> list = new List<Tuple<Guid, Enum, object>>();
                //list.Add(new Tuple<Guid, Enum, object>(DomainObject1.Oid, NASDMS.Systems.CategoryAudit.DomainObject1, this));
                helper.deleteds.Add(new Tuple<Guid, Enum, object>(DomainObject1.Oid, NASDMS.Systems.CategoryAudit.DomainObject1, this));// = list;
            }
            if (DomainObject2 != null)
            {
                //List<Tuple<Guid, Enum, object>> list = new List<Tuple<Guid, Enum, object>>();
                //list.Add(new Tuple<Guid, Enum, object>(DomainObject1.Oid, NASDMS.Systems.CategoryAudit.DomainObject1, this));
                helper.deleteds.Add(new Tuple<Guid, Enum, object>(DomainObject2.Oid, NASDMS.Systems.CategoryAudit.DomainObject2, this));// = list;
            }
        }
    }
}
//private string _PersistentProperty;
//[XafDisplayName("My display name"), ToolTip("My hint message")]
//[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
//[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
//public string PersistentProperty {
//    get { return _PersistentProperty; }
//    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
//}

//[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
//public void ActionMethod() {
//    // Trigger a custom business logic for the current record in the UI (http://documentation.devexpress.com/#Xaf/CustomDocument2619).
//    this.PersistentProperty = "Paid";
//}