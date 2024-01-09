
namespace CaseManagement.Case {

    @Serenity.Decorators.registerClass()
    export class ProvinceSwitchDSLAMGrid extends Serenity.EntityGrid<SwitchDslamProvinceRow, any> {
        protected getColumnsKey() { return 'Case.SwitchDslamProvince'; }
        //protected getDialogType() { return SwitchProvinceDialog; }
        protected getIdProperty() { return SwitchDslamProvinceRow.idProperty; }
        protected getLocalTextPrefix() { return SwitchDslamProvinceRow.localTextPrefix; }
        protected getService() { return SwitchDslamProvinceService.baseUrl; }

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
                this.setEquality(SwitchDslamProvinceRow.Fields.ProvinceId, value);
                this.refresh();
            }
        }
    }
}  