/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />
namespace CaseManagement.Case {
    @Serenity.Decorators.registerClass()
    export class ActivityMainReasonEditDialog extends
        Common.GridEditorDialog<ActivityMainReasonRow> {
        protected getFormKey() { return ActivityMainReasonForm.formKey; }
        protected getNameProperty() { return ActivityMainReasonRow.nameProperty; }
        protected getLocalTextPrefix() { return ActivityMainReasonRow.localTextPrefix; }
        protected form: ActivityMainReasonForm;
        constructor() {
            super();
            this.form = new ActivityMainReasonForm(this.idPrefix);
        }
    }
}  