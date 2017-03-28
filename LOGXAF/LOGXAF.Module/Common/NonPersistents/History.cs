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
using DevExpress.ExpressApp.ConditionalAppearance;
using NASDMS.Systems;
using DevExpress.ExpressApp.Editors;

namespace NASDMS.Module.Common.NonPersistents
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [Appearance("NewDisabled", AppearanceItemType.Action, "true", TargetItems = "New,Edit,Delete", Visibility = ViewItemVisibility.Hide)]
    [NonPersistent]
    public class History : BaseObject
    {
        public History(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private ActionAudit _Action;
        private CategoryAudit _Category;
        private DateTime _ChangedOn;
        private string _ChangedBy;
        private string _Data;
        [ModelDefault("DisplayFormat", "{0: dd/MM/yyyy hh:mm tt}"), ModelDefault("EditMask", "dd/MM/yyyy hh:mm tt")]

        public DateTime ChangedOn
        {
            get
            {
                return _ChangedOn;
            }
            set
            {
                SetPropertyValue("ChangedOn", ref _ChangedOn, value);
            }
        }
        public string ChangedBy
        {
            get
            {
                return _ChangedBy;
            }
            set
            {
                SetPropertyValue("ChangedBy", ref _ChangedBy, value);
            }
        }
        public string Data
        {
            get
            {
                return _Data;
            }
            set
            {
                SetPropertyValue("Data", ref _Data, value);
            }
        }
        public ActionAudit Action
        {
            get
            {
                return _Action;
            }
            set
            {
                SetPropertyValue("Action", ref _Action, value);
            }
        }
        [Browsable(false)]
        public CategoryAudit Category
        {
            get
            {
                return _Category;
            }
            set
            {
                SetPropertyValue("Category", ref _Category, value);
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