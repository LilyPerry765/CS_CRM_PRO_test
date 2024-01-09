
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityGroupDialog extends Serenity.EntityDialog<ActivityGroupRow, any> {
        protected getFormKey() { return ActivityGroupForm.formKey; }
        protected getIdProperty() { return ActivityGroupRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityGroupRow.localTextPrefix; }
        protected getNameProperty() { return ActivityGroupRow.nameProperty; }
        protected getService() { return ActivityGroupService.baseUrl; }

        protected form = new ActivityGroupForm(this.idPrefix);
    }
}