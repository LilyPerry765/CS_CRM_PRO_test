﻿
namespace CaseManagement.Case {

    @Serenity.Decorators.maximizable()
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ActivityRequestFinancialDialog extends Serenity.EntityDialog<ActivityRequestFinancialRow, any> {
        protected getFormKey() { return ActivityRequestFinancialForm.formKey; }
        protected getIdProperty() { return ActivityRequestFinancialRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestFinancialRow.localTextPrefix; }
        protected getService() { return ActivityRequestFinancialService.baseUrl; }

        protected form = new ActivityRequestFinancialForm(this.idPrefix);

      //  private activityRequestHistoryPropertyGrid: Serenity.PropertyGrid;
      //  private activityRequestHistoryForm: ActivityRequestHistoryForm;
      //  private historyValidator: JQueryValidation.Validator;

        private logsGrid: ActivityRequestLogGrid;
        constructor() {
            super();

         //   this.activityRequestHistoryPropertyGrid = new Serenity.PropertyGrid(this.byId("ActivityRequestHistoryPropertyGrid"), {
         //       items: Q.getForm(ActivityRequestHistoryForm.formKey).filter(x => x.Id != 'Id'),
         //       useCategories: true
         //   });
         //
         //   // this is just a helper to access editors if needed
         //   this.activityRequestHistoryForm = new ActivityRequestHistoryForm((this.activityRequestHistoryPropertyGrid as any).idPrefix);
         //   this.historyValidator = this.byId("ActivityRequestHistoryForm").validate(Q.validateOptions({}));

            this.logsGrid = new ActivityRequestLogGrid(this.byId("LogsGrid"));
            this.tabs.on('tabsactivate', (e, i) => {
                this.arrange();
            });

            this.form = new ActivityRequestFinancialForm(this.idPrefix);

            this.form.ActivityId.changeSelect2(e => {
                var ActivityId = Q.toId(this.form.ActivityId.value);
                if (ActivityId != null) {
                    var RequiredYearRepeatCOUNT = ActivityRow.getLookup().itemById[ActivityId].RequiredYearRepeatCount;
                    this.form.Factor.value = RequiredYearRepeatCOUNT.toString();
                }
            });


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

            this.form.ActivityId.changeSelect2(e => {
                var activityID = Q.toId(this.form.ActivityId.value);
                if (activityID != null) {
                    this.form.EventDescription.value = ActivityRow.getLookup().itemById[activityID].EventDescription;
                    //this.form.EventDescription.value = ActivityRow.getLookup().itemById[activityID].EventDescription;
                }
            });
        }

       

      // loadEntity(entity: ActivityRequestFinancialRow) {
      //     super.loadEntity(entity);
      //
      //     Serenity.TabsExtensions.setDisabled(this.tabs, 'ActivityRequestHistory',
      //         !this.form.Id.value);
      //
      //     this.activityRequestHistoryPropertyGrid.load({});
      // }

        protected updateInterface(): void {

            super.updateInterface();
            this.deleteButton.hide();
        }

        protected afterLoadEntity() {
            super.afterLoadEntity();
            this.logsGrid.ActivityRequestID = this.entityId;
        }
    }
}