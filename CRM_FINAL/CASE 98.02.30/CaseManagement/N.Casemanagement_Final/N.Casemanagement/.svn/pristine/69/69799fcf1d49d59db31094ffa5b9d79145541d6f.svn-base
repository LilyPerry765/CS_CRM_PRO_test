
/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />
namespace CaseManagement.Case {
    @Serenity.Decorators.registerClass()
    export class ActivityCorrectionOperationEditDialog extends
        Common.GridEditorDialog<ActivityCorrectionOperationRow> {
        protected getFormKey() { return ActivityCorrectionOperationForm.formKey; }
        protected getNameProperty() { return ActivityCorrectionOperationRow.nameProperty; }
        protected getLocalTextPrefix() { return ActivityCorrectionOperationRow.localTextPrefix; }
        protected form: ActivityCorrectionOperationForm;
        constructor() {
            super();
            this.form = new ActivityCorrectionOperationForm(this.idPrefix);
        }
    }
}  