﻿
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityRequestConfirmAdminDialog extends Serenity.EntityDialog<ActivityRequestConfirmAdminRow, any> {
        protected getFormKey() { return ActivityRequestConfirmAdminForm.formKey; }
        protected getIdProperty() { return ActivityRequestConfirmAdminRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestConfirmAdminRow.localTextPrefix; }
        protected getService() { return ActivityRequestConfirmAdminService.baseUrl; }

        protected form = new ActivityRequestConfirmAdminForm(this.idPrefix);

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

            //buttons.splice(Q.indexOf(buttons, x => x.cssClass == "save-and-close-button"), 1);
            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "apply-changes-button"), 1);
            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "delete-button"), 1);
            buttons.push({
                title: Q.text('چاپ'),
                cssClass: 'print-preview-button',
                onClick: () => {
                    var activityID = this.form.Id.value;
                    window.location.href = "../Common/ActivityRequestTechnicalInfoTOPrint?ActivityId=" + activityID;
                }
            });

            return buttons;
        }

     //  protected updateInterface(): void {
     //
     //      super.updateInterface();
     //
     //      Serenity.EditorUtils.setReadonly(this.element.find('.editor'), true);
     //      this.element.find('sup').hide();
     //      this.deleteButton.hide();
     //  }

        protected afterLoadEntity() {
            super.afterLoadEntity();
            this.logsGrid.ActivityRequestID = this.entityId;
        } 
    }
}