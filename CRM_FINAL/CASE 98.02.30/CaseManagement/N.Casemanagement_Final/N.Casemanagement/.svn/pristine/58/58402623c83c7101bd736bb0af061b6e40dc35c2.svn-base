namespace CaseManagement.WorkFlow {
    export class WorkFlowActionForm extends Serenity.PrefixedContext {
        static formKey = 'WorkFlow.WorkFlowAction';

    }

    export interface WorkFlowActionForm {
        Name: Serenity.StringEditor;
    }

    [['Name', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(WorkFlowActionForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

