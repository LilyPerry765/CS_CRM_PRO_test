
namespace CaseManagement.Case {

    @Serenity.Decorators.registerClass()
    export class ProvinceSwitchGrid extends Serenity.EntityGrid<SwitchProvinceRow, any> {
        protected getColumnsKey() { return 'Case.SwitchProvince'; }
        //protected getDialogType() { return SwitchProvinceDialog; }
        protected getIdProperty() { return SwitchProvinceRow.idProperty; }
        protected getLocalTextPrefix() { return SwitchProvinceRow.localTextPrefix; }
        protected getService() { return SwitchProvinceService.baseUrl; }

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
                this.setEquality(SwitchProvinceRow.Fields.ProvinceId, value);
                this.refresh();
            }
        }
    }
} 