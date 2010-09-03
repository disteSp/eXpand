﻿using System;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using eXpand.ExpressApp.AdditionalViewControlsProvider.Logic;
using eXpand.ExpressApp.Attributes;
using FeatureCenter.Base;

namespace FeatureCenter.Module.WorldCreator.ExistentAssemblyMasterDetail
{
    [AdditionalViewControlsRule(Captions.ViewMessage + " " + Captions.HeaderExistentAssemblyMasterDetail, "1=1", "1=1",
        Captions.ViewMessageExistentAssemblyMasterDetail, Position.Bottom, ViewType = ViewType.DetailView)]
    [AdditionalViewControlsRule(Captions.Header + " " + Captions.HeaderExistentAssemblyMasterDetail, "1=1", "1=1",
        Captions.HeaderExistentAssemblyMasterDetail, Position.Top, ViewType = ViewType.DetailView)]
    [XpandNavigationItem("WorldCreator/Existent Assembly Master Detail", "EAMDCustomer_ListView")]
    [DisplayFeatureModel("EAMDCustomer_ListView", "ExistentAssemblyMasterDetailModelStore")]
    public class EAMDCustomer:BaseObject,ICustomer
    {
        public EAMDCustomer(Session session) : base(session) {
        }


        string ICustomer.Name {
            get { return GetMemberValue("Name") as string; }
            set { SetMemberValue("Name",value); }
        }

        string ICustomer.City {
            get { return GetMemberValue("City") as string; }
            set { SetMemberValue("City",value); }
        }

        string ICustomer.Description {
            get { return GetMemberValue("Description") as string; }
            set { SetMemberValue("Description",value); }
        }
    }
}
