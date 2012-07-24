﻿using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using Xpand.ExpressApp.Logic;
using Xpand.ExpressApp.Logic.Conditional.Logic;
using Xpand.ExpressApp.Logic.Model;
using Xpand.ExpressApp.MasterDetail.Model;

namespace Xpand.ExpressApp.MasterDetail.Logic {
    public class MasterDetailRuleInfo {
        public MasterDetailRuleInfo(IModelListView childListView, IModelMember collectionMember, ITypeInfo typeInfo) {
            ChildListView = childListView;
            CollectionMember = collectionMember;
            TypeInfo = typeInfo;
        }

        public IModelListView ChildListView { get; set; }
        public IModelMember CollectionMember { get; set; }
        public ITypeInfo TypeInfo { get; set; }
    }
    public class MasterDetailRuleController : ConditionalLogicRuleViewController<IMasterDetailRule> {
        readonly List<IMasterDetailRule> _masterDetailRules = new List<IMasterDetailRule>();

        protected override IModelLogic GetModelLogic() {
            return ((IModelApplicationMasterDetail)Application.Model).MasterDetail;
        }

        protected override void OnActivated() {
            base.OnActivated();
            Frame.GetController<MasterDetailViewControllerBase>().NeedsRule += OnNeedsRule;
        }
        protected override void OnDeactivated() {
            base.OnDeactivated();
            Frame.GetController<MasterDetailViewControllerBase>().NeedsRule -= OnNeedsRule;
        }
        void OnNeedsRule(object sender, NeedsRuleArgs needsRuleArgs) {
            var controller = needsRuleArgs.Frame.GetController<MasterDetailRuleController>();
            var ruleInfos = controller.MasterDetailRules.Select(rule => new MasterDetailRuleInfo(rule.ChildListView, rule.CollectionMember, rule.TypeInfo));
            needsRuleArgs.Rules.AddRange(ruleInfos);
        }

        public override void ExecuteRule(LogicRuleInfo<IMasterDetailRule> info, ExecutionContext executionContext) {
            if (info.Active) {
                if (!(_masterDetailRules.Contains(info.Rule))) {
                    _masterDetailRules.Add(info.Rule);
                }
            } else {
                _masterDetailRules.Remove(info.Rule);
            }
        }

        public List<IMasterDetailRule> MasterDetailRules {
            get { return _masterDetailRules; }
        }

    }

}