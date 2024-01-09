/// <reference path="../../Common/Helpers/GridEditorBase.ts" />

namespace CaseManagement.Case {
    @Serenity.Decorators.registerEditor()
    export class ActivityCorrectionOperationEditor
        extends Common.GridEditorBase<ActivityMainReasonRow> {
        protected getColumnsKey() { return "Case.ActivityCorrectionOperation"; }
        protected getDialogType() { return ActivityCorrectionOperationEditDialog; }
        protected getLocalTextPrefix() { return ActivityCorrectionOperationRow.localTextPrefix; }
        constructor(container: JQuery) {
            super(container);
        }
        protected getAddButtonCaption() {
            return "افزودن";
        }
    }
}    