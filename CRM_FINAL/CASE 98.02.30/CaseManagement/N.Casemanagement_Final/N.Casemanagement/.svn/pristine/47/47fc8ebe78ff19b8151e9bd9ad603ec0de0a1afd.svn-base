/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace CaseManagement.Case {
    @Serenity.Decorators.registerEditor()
    export class ActivityMainReasonEditor
        extends Common.GridEditorBase<ActivityMainReasonRow> {
        protected getColumnsKey() { return "Case.ActivityMainReason"; }
        protected getDialogType() { return ActivityMainReasonEditDialog; }
        protected getLocalTextPrefix() { return ActivityMainReasonRow.localTextPrefix; }
        constructor(container: JQuery) {
            super(container);
        }
        protected getAddButtonCaption() {
            return "افزودن";
        }
    }
}   