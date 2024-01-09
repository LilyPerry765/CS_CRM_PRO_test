
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class CommentReasonGrid extends Serenity.EntityGrid<CommentReasonRow, any> {
        protected getColumnsKey() { return 'Case.CommentReason'; }
        protected getDialogType() { return CommentReasonDialog; }
        protected getIdProperty() { return CommentReasonRow.idProperty; }
        protected getLocalTextPrefix() { return CommentReasonRow.localTextPrefix; }
        protected getService() { return CommentReasonService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }
    }
}