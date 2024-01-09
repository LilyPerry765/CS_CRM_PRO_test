namespace CaseManagement.WorkFlow {
    export class WorkFlowStepForm extends Serenity.PrefixedContext {
        static formKey = 'WorkFlow.WorkFlowStep';

    }

    export interface WorkFlowStepForm {
        Name: Serenity.StringEditor;
    }

    [['Name', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(WorkFlowStepForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

