
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    @Serenity.Decorators.responsive()
    export class ProvinceDialog extends Serenity.EntityDialog<ProvinceRow, any> {
        protected getFormKey() { return ProvinceForm.formKey; }
        protected getIdProperty() { return ProvinceRow.idProperty; }
        protected getLocalTextPrefix() { return ProvinceRow.localTextPrefix; }
        protected getNameProperty() { return ProvinceRow.nameProperty; }
        protected getService() { return ProvinceService.baseUrl; }

        protected form = new ProvinceForm(this.idPrefix);

        private switchsGrid: ProvinceSwitchGrid;
        private switchDSLAMsGrid: ProvinceSwitchDSLAMGrid;
        private switchTransitsGrid: ProvinceSwitchTransitGrid;
        private softwaresGrid: ProvinceCompanySoftwareGrid;
        constructor() {
            super();
      
            this.switchsGrid = new ProvinceSwitchGrid(this.byId("SwitchsGrid"));
            this.tabs.on('tabsactivate', (e, i) => {
                this.arrange();
            });
      
            this.switchDSLAMsGrid = new ProvinceSwitchDSLAMGrid(this.byId("SwitchDSLAMsGrid"));
            this.tabs.on('tabsactivate', (e, i) => {
                this.arrange();
            });
      
            this.switchTransitsGrid = new ProvinceSwitchTransitGrid(this.byId("SwitchTransitsGrid"));
            this.tabs.on('tabsactivate', (e, i) => {
                this.arrange();
            });
      
            this.softwaresGrid = new ProvinceCompanySoftwareGrid(this.byId("SoftwaresGrid"));
            this.tabs.on('tabsactivate', (e, i) => {
                this.arrange();
            });
        }
      
        protected afterLoadEntity() {
            super.afterLoadEntity();
            Serenity.TabsExtensions.setDisabled(this.tabs, 'Orders', this.isNewOrDeleted());
      
            this.switchsGrid.ProvinceID = this.entityId;
            this.switchDSLAMsGrid.ProvinceID = this.entityId;
            this.switchTransitsGrid.ProvinceID = this.entityId;
            this.softwaresGrid.ProvinceID = this.entityId;
        }
    }
}