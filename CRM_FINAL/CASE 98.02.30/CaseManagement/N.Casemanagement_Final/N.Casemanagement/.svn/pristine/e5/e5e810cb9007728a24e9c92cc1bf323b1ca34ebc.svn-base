
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityRequestPenddingDialog extends Serenity.EntityDialog<ActivityRequestPenddingRow, any> {
        protected getFormKey() { return ActivityRequestPenddingForm.formKey; }
        protected getIdProperty() { return ActivityRequestPenddingRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestPenddingRow.localTextPrefix; }
        protected getService() { return ActivityRequestPenddingService.baseUrl; }

        protected form = new ActivityRequestPenddingForm(this.idPrefix);

        private logsGrid: ActivityRequestLogGrid;
        constructor() {
            super();
            this.logsGrid = new ActivityRequestLogGrid(this.byId("LogsGrid"));
            this.tabs.on('tabsactivate', (e, i) => {
                this.arrange();
            });
        }

        protected getToolbarButtons(): Serenity.ToolButton[] {
            let buttons = super.getToolbarButtons();

            buttons.push({
                title: Q.text('چاپ فعالیت'),
                cssClass: 'print-preview-button',
                onClick: () => {
                    var activityID = this.form.Id.value;
                    window.location.href = "../Common/ActivityRequestPendingInfoTOPrint?ActivityId=" + activityID;
                }
            });

            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "save-and-close-button"), 1);
            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "apply-changes-button"), 1);

            // We could also remove delete button here, but for demonstration 
            // purposes we'll hide it in another method (updateInterface)
            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "delete-button"), 1);

            return buttons;
        }

       
        protected updateInterface(): void {

            super.updateInterface();
            Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);

            
            this.element.find('sup').hide();
            this.deleteButton.hide();
        }

        protected afterLoadEntity() {
            super.afterLoadEntity();
            this.logsGrid.ActivityRequestID = this.entityId;
        }
    }
}