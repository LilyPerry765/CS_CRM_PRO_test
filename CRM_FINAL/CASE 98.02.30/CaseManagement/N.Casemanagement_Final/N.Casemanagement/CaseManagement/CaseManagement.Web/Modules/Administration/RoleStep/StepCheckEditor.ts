namespace CaseManagement.Administration {

    @Serenity.Decorators.registerEditor()
    export class StepCheckEditor extends Serenity.CheckTreeEditor<Serenity.CheckTreeItem<any>, any> {

        private searchText: string;

        constructor(div: JQuery) {
            super(div);
        }

        protected createToolbarExtensions() {
            super.createToolbarExtensions();

            Serenity.GridUtils.addQuickSearchInputCustom(this.toolbar.element, (field, text) => {
                this.searchText = Select2.util.stripDiacritics(text || '').toUpperCase();
                this.view.setItems(this.view.getItems(), true);
            });
        }

        protected getButtons() {
            return [];
        }

        protected getTreeItems() {
            return WorkFlow.WorkFlowStepRow.getLookup().items.map(step => <Serenity.CheckTreeItem<any>>{
                id: step.Id.toString(),
                text: step.Name
            });
        }

        protected onViewFilter(item) {
            return super.onViewFilter(item) &&
                (Q.isEmptyOrNull(this.searchText) ||
                    Select2.util.stripDiacritics(item.text || '')
                        .toUpperCase().indexOf(this.searchText) >= 0);
        }
    }
}