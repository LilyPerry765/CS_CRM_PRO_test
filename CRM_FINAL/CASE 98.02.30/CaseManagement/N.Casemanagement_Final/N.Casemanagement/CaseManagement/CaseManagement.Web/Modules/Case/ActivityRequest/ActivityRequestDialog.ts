
namespace CaseManagement.Case {

    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    @Serenity.Decorators.maximizable()
    export class ActivityRequestDialog extends Serenity.EntityDialog<ActivityRequestRow, any> {
        protected getFormKey() { return ActivityRequestForm.formKey; }
        protected getIdProperty() { return ActivityRequestRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestRow.localTextPrefix; }
        protected getService() { return ActivityRequestService.baseUrl; }

        protected form = new ActivityRequestForm(this.idPrefix);       

        private logsGrid: ActivityRequestLogGrid;
        constructor() {
            super();
            this.logsGrid = new ActivityRequestLogGrid(this.byId("LogsGrid"));
            this.tabs.on('tabsactivate', (e, i) => {
                this.arrange();
            });

            this.form = new ActivityRequestForm(this.idPrefix);

            this.form.CycleCost.change(e => {
                var cycle = Q.toId(this.form.CycleCost.value);

                var factor = Q.toId(this.form.Factor.value);                
                if (cycle != null && factor != null) {
                    var year = cycle * factor;
                    this.form.YearCost.value = year.toString();
                }

                var delay = Q.toId(this.form.DelayedCost.value);
                if (cycle != null && delay != null) {
                    var total = cycle + delay;
                    this.form.TotalLeakage.value = total.toString();
                }

                var accessible = Q.toId(this.form.AccessibleCost.value);
                if (cycle != null && accessible != null) {
                    var recoverableLeakage = cycle + accessible;
                    this.form.RecoverableLeakage.value = recoverableLeakage.toString();
                    this.form.Recovered.value = recoverableLeakage.toString();
                }
            });

            this.form.DelayedCost.change(e => {
                var cycle = Q.toId(this.form.CycleCost.value);
                var delay = Q.toId(this.form.DelayedCost.value);
                if (cycle != null && delay != null) {
                    var total = cycle + delay;
                    this.form.TotalLeakage.value = total.toString();
                }
            });  

            this.form.AccessibleCost.change(e => {
                var cycle = Q.toId(this.form.CycleCost.value);
                var accessibleCost = Q.toId(this.form.AccessibleCost.value);
                if (cycle != null && accessibleCost != null) {
                    var recoverableLeakage = cycle + accessibleCost;
                    this.form.RecoverableLeakage.value = recoverableLeakage.toString();
                    this.form.Recovered.value = recoverableLeakage.toString();
                }
            }); 

            this.form.Factor.change(e => {
                var cycle = Q.toId(this.form.CycleCost.value);
                var factor = Q.toId(this.form.Factor.value);
                if (cycle != null && factor != null) {
                    var year = cycle * factor;
                    this.form.YearCost.value = year.toString();
                }
            });            

            this.form.ActivityId.changeSelect2(e => {                
                var activityID = Q.toId(this.form.ActivityId.value);                
                if (activityID != null){
                    this.form.EventDescription.value = ActivityRow.getLookup().itemById[activityID].EventDescription;
                    //this.form.EventDescription.value = ActivityRow.getLookup().itemById[activityID].EventDescription;
                }                
            });
        }

        protected afterLoadEntity() {
            super.afterLoadEntity();
            this.logsGrid.ActivityRequestID = this.entityId;
        }       
    }
}