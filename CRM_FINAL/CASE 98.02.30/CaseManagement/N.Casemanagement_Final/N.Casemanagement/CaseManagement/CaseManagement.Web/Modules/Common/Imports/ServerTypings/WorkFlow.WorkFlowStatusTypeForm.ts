namespace CaseManagement.WorkFlow {
    export class WorkFlowStatusTypeForm extends Serenity.PrefixedContext {
        static formKey = 'WorkFlow.WorkFlowStatusType';

    }

    export interface WorkFlowStatusTypeForm {
        Name: Serenity.StringEditor;
    }

    [['Name', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(WorkFlowStatusTypeForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

