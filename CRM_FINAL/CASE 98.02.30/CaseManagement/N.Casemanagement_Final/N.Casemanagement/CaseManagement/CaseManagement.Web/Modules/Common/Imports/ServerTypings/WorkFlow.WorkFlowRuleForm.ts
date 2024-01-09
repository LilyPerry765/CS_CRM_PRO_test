namespace CaseManagement.WorkFlow {
    export class WorkFlowRuleForm extends Serenity.PrefixedContext {
        static formKey = 'WorkFlow.WorkFlowRule';

    }

    export interface WorkFlowRuleForm {
        CurrentStatusId: Serenity.LookupEditor;
        ActionId: Serenity.LookupEditor;
        NextStatusId: Serenity.LookupEditor;
        Version: Serenity.IntegerEditor;
    }

    [['CurrentStatusId', () => Serenity.LookupEditor], ['ActionId', () => Serenity.LookupEditor], ['NextStatusId', () => Serenity.LookupEditor], ['Version', () => Serenity.IntegerEditor]].forEach(x => Object.defineProperty(WorkFlowRuleForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

