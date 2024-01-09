
namespace CaseManagement.Case {

    @Serenity.Decorators.registerClass()
    export class ActivityRequestLogGrid extends Serenity.EntityGrid<ActivityRequestLogRow, any> {
        protected getColumnsKey() { return 'Case.ActivityRequestLog'; }
        //protected getDialogType() { return ActivityRequestLogDialog; }
        protected getIdProperty() { return ActivityRequestLogRow.idProperty; }
        protected getLocalTextPrefix() { return ActivityRequestLogRow.localTextPrefix; }
        protected getService() { return ActivityRequestLogService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getButtons() {
            return null;
        }
        protected getInitialTitle() {
            return null;
        }
        protected usePager() {
            return false;
        }
        protected getGridCanLoad() {
            return this.ActivityRequestID != null;
        }
        private _ActivityRequestID: number;
        get ActivityRequestID() {
            return this._ActivityRequestID;
        }
        set ActivityRequestID(value: number) {
            if (this._ActivityRequestID != value) {
                this._ActivityRequestID = value;
                this.setEquality(ActivityRequestLogRow.Fields.ActivityRequestId, value);
                this.refresh();
            }
        }
    }
} 