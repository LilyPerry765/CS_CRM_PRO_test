
namespace CaseManagement.Case {

    @Serenity.Decorators.registerClass()
    export class ProvinceSwitchTransitGrid extends Serenity.EntityGrid<SwitchTransitProvinceRow, any> {
        protected getColumnsKey() { return 'Case.SwitchTransitProvince'; }
        protected getDialogType() { return SwitchTransitProvinceDialog; }
        protected getIdProperty() { return SwitchTransitProvinceRow.idProperty; }
        protected getLocalTextPrefix() { return SwitchTransitProvinceRow.localTextPrefix; }
        protected getService() { return SwitchTransitProvinceService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }

        protected getColumns(): Slick.Column[] {
            let fld = SwitchTransitProvinceRow.Fields;
            return super.getColumns().filter(x => x.field !== fld.ProvinceName);
        }

        protected initEntityDialog(itemType, dialog) {
            super.initEntityDialog(itemType, dialog);
            Serenity.SubDialogHelper.cascade(dialog, this.element.closest('.ui-dialog'));
        }

        protected addButtonClick() {
            this.editItem({ ProvinceID: this.ProvinceID });
        }

        protected getInitialTitle() {
            return null;
        }
        protected usePager() {
            return false;
        }

        protected getGridCanLoad() {
            return super.getGridCanLoad() && !!this.ProvinceID;
        }

        private _ProvinceID: number;

        get ProvinceID() {
            return this._ProvinceID;
        }
        set ProvinceID(value: number) {
            if (this._ProvinceID != value) {
                this._ProvinceID = value;
                //this.setEquality(SwitchTransitProvinceRow.Fields.ProvinceId, value);
                this.setEquality('ProvinceID', value);
                this.refresh();
            }
        }
    }
}   