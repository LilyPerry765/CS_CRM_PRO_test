namespace CaseManagement.WorkFlow {
    export class WorkFlowStatusForm extends Serenity.PrefixedContext {
        static formKey = 'WorkFlow.WorkFlowStatus';

    }

    export interface WorkFlowStatusForm {
        StepId: Serenity.LookupEditor;
        StatusTypeId: Serenity.LookupEditor;
    }

    [['StepId', () => Serenity.LookupEditor], ['StatusTypeId', () => Serenity.LookupEditor]].forEach(x => Object.defineProperty(WorkFlowStatusForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

