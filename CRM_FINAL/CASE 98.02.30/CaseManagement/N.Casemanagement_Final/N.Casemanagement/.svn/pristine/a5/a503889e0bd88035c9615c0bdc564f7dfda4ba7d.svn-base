/// <reference path="../../Common/Helpers/GridEditorDialog.ts" />
namespace CaseManagement.Case {
    @Serenity.Decorators.registerClass()
    export class ActivityRequestCommentEditDialog extends
        Common.GridEditorDialog<ActivityRequestCommentRow> {
        protected getFormKey() { return ActivityRequestCommentForm.formKey; }
        protected getNameProperty() { return ActivityRequestCommentRow.nameProperty; }
        protected getLocalTextPrefix() { return ActivityRequestCommentRow.localTextPrefix; }
        protected form: ActivityRequestCommentForm;
        constructor() {
            super();
            this.form = new ActivityRequestCommentForm(this.idPrefix);
        }
    }
} 