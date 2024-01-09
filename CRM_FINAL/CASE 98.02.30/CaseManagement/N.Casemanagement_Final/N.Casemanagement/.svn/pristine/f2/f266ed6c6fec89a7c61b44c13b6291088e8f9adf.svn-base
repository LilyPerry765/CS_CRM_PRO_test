
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class CommentReasonDialog extends Serenity.EntityDialog<CommentReasonRow, any> {
        protected getFormKey() { return CommentReasonForm.formKey; }
        protected getIdProperty() { return CommentReasonRow.idProperty; }
        protected getLocalTextPrefix() { return CommentReasonRow.localTextPrefix; }
        protected getNameProperty() { return CommentReasonRow.nameProperty; }
        protected getService() { return CommentReasonService.baseUrl; }

        protected form = new CommentReasonForm(this.idPrefix);
    }
}