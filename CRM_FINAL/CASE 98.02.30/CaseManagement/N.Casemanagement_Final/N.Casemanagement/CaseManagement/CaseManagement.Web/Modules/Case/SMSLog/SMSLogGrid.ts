
namespace CaseManagement.Case {
    
    @Serenity.Decorators.registerClass()
    export class SMSLogGrid extends Serenity.EntityGrid<SMSLogRow, any> {
        protected getColumnsKey() { return 'Case.SMSLog'; }
        protected getDialogType() { return SMSLogDialog; }
        protected getIdProperty() { return SMSLogRow.idProperty; }
        protected getLocalTextPrefix() { return SMSLogRow.localTextPrefix; }
        protected getService() { return SMSLogService.baseUrl; }

        constructor(container: JQuery) {
            super(container);
        }


        protected PersiaNumber(value) {
            var arabicNumbers = ['۰', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩'];

            //console.log(value);
            var chars = value.split('');

            for (var i = 0; i < chars.length; i++) {
                if (/\d/.test(chars[i])) {
                    chars[i] = arabicNumbers[chars[i]];
                }
            }

            return chars.join('');

        }
		  protected getColumns(): Slick.Column[] {
            var columns = super.getColumns();

            var fld = Case.SMSLogRow.Fields;

            Q.first(columns, x => x.field == fld.ActivityRequestId).format =
                ctx => `<a href="javascript:;" class="ActivityRequest-link">${Q.htmlEncode(this.PersiaNumber(ctx.value.toString()))}</a>`;

            return columns;
        }

             
        
          protected onClick(e: JQueryEventObject, row: number, cell: number): void {

              // let base grid handle clicks for its edit links
              super.onClick(e, row, cell);

              // if base grid already handled, we shouldn"t handle it again
              if (e.isDefaultPrevented()) {
                  return;
              }

              // get reference to current item
              var item = this.itemAt(row);

              // get reference to clicked element
              var target = $(e.target);

             if (target.hasClass("ActivityRequest-link")) {

                  e.preventDefault();

                  var ActivityRequest = Q.first(Case.ActivityRequestPenddingRow.getLookup().items,  
                      x => x.Id  == item.ActivityRequestId);

                  new Case.ActivityRequestPenddingDialog().loadByIdAndOpenDialog(ActivityRequest.Id);
              }


          }

        protected getButtons() {
            var buttons = super.getButtons();

            buttons.splice(Q.indexOf(buttons, x => x.cssClass == "add-button"), 1);

            return buttons;
        }
    }
}