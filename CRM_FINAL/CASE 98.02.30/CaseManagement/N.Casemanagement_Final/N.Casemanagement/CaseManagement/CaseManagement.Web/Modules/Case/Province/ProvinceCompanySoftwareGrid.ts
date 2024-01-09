
namespace CaseManagement.Case {

    @Serenity.Decorators.registerClass()
    export class ProvinceCompanySoftwareGrid extends Serenity.EntityGrid<ProvinceCompanySoftwareRow, any> {
        protected getColumnsKey() { return 'Case.ProvinceCompanySoftware'; }
        //protected getDialogType() { return ProvinceCompanySoftwareDialog; }
        protected getIdProperty() { return ProvinceCompanySoftwareRow.idProperty; }
        protected getLocalTextPrefix() { return ProvinceCompanySoftwareRow.localTextPrefix; }
        protected getService() { return ProvinceCompanySoftwareService.baseUrl; }

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
            return this.ProvinceID != null;
        }
        private _ProvinceID: number;
        get ProvinceID() {
            return this._ProvinceID;
        }
        set ProvinceID(value: number) {
            if (this._ProvinceID != value) {
                this._ProvinceID = value;
                this.setEquality(ProvinceCompanySoftwareRow.Fields.ProvinveId, value);
                this.refresh();
            }
        }
    }
} 