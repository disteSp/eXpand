﻿using System;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.DashboardWin;
using Xpand.ExpressApp.Dashboard;
using Xpand.ExpressApp.Dashboard.Filter;
using Xpand.ExpressApp.Dashboard.PropertyEditors;
using Xpand.ExpressApp.Dashboard.BusinessObjects;

namespace Xpand.ExpressApp.XtraDashboard.Win.PropertyEditors {
    public class DashboardViewerModelAdapter : Dashboard.PropertyEditors.DashboardViewerModelAdapter {

        protected override Type GetControlType(){
            return typeof (DashboardViewer);
        }
    }

    [PropertyEditor(typeof(String), false)]
    public class DashboardViewEditor : WinPropertyEditor, IComplexViewItem,IDashboardViewEditor {
        XafApplication _application;
        IObjectSpace _objectSpace;

        public DashboardViewEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model) {
        }

        public DashboardViewer DashboardViewer => (DashboardViewer)Control;

        public IObjectSpace ObjectSpace => _objectSpace;

        public XafApplication Application => _application;

        public void Setup(IObjectSpace objectSpace, XafApplication application) {
            _objectSpace = objectSpace;
            _application = application;
        }

        protected override object CreateControlCore() {
            return new DashboardViewer { Margin = new Padding(0), Padding = new Padding(0) };
        }

        protected override void ReadValueCore() {
            Control.HandleCreated+=ControlOnHandleCreated;
        }

        void ControlOnHandleCreated(object sender, EventArgs eventArgs) {
            var template = CurrentObject as IDashboardDefinition;
            if (template != null) {
                Control.HandleCreated -= ControlOnHandleCreated;
                Control.BeginInvoke(new Action(() =>DashboardViewer.Dashboard =template.CreateDashBoard(RuleMode.Runtime,
                                    type => ObjectSpace.CreateDashboardDataSource(type),_application)));
            }
        }
    }

}