
namespace CaseManagement.Administration {

    @Serenity.Decorators.registerClass()
    export class RoleStepDialog extends Serenity.TemplatedDialog<RoleStepDialogOptions> {

        private steps: StepCheckEditor;

        constructor(opt: RoleStepDialogOptions) {
            super(opt);

            this.steps = new StepCheckEditor(this.byId('Steps'));

            RoleStepService.List({
                RoleID: this.options.roleID
            }, response => {
                this.steps.value = response.Entities.map(x => x.toString());
            });
        }

        protected getDialogOptions() {
            var opt = super.getDialogOptions();

            opt.buttons = [{
                text: Q.text('Dialogs.OkButton'),
                click: () => {
                    Q.serviceRequest('Administration/RoleStep/Update', {
                        RoleID: this.options.roleID,
                        Steps: this.steps.value.map(x => parseInt(x, 10))
                    }, response => {
                        this.dialogClose();
                        Q.notifySuccess(Q.text('Site.RoleStepDialog.SaveSuccess'));
                    });
                }
            }, {
                    text: Q.text('Dialogs.CancelButton'),
                    click: () => this.dialogClose()
                }];

            opt.title = Q.format(Q.text('Site.RoleStepDialog.DialogTitle'), this.options.title);
            return opt;
        }

        protected getTemplate(): string {
            return '<div id="~_Steps"></div>';
        }
    }

    export interface RoleStepDialogOptions {
        roleID?: number;
        title?: string;
    }
}