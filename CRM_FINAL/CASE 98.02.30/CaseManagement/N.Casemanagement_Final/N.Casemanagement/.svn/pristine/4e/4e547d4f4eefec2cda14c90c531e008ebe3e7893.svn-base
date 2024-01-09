/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace CaseManagement.Case {
    @Serenity.Decorators.registerEditor()
    export class ActivityRequestCommentEditor
        extends Common.GridEditorBase<ActivityRequestCommentRow> {
        protected getColumnsKey() { return "Case.ActivityRequestComment"; }
        protected getDialogType() { return ActivityRequestCommentEditDialog; }
        protected getLocalTextPrefix() { return ActivityRequestCommentRow.localTextPrefix; }
        constructor(container: JQuery) {
            super(container);
        }
        protected getAddButtonCaption() {
            return "افزودن";
        }
    }
}  