namespace CaseManagement.Administration {
    export class CalendarEventForm extends Serenity.PrefixedContext {
        static formKey = 'Administration.CalendarEvent';

    }

    export interface CalendarEventForm {
        Title: Serenity.StringEditor;
        AllDay: Serenity.BooleanEditor;
        StartDate: Serenity.DateEditor;
        EndDate: Serenity.DateEditor;
        Url: Serenity.StringEditor;
        ClassName: Serenity.StringEditor;
        IsEditable: Serenity.BooleanEditor;
        IsOverlap: Serenity.BooleanEditor;
        Color: Serenity.StringEditor;
        BackgroundColor: Serenity.StringEditor;
        TextColor: Serenity.StringEditor;
    }

    [['Title', () => Serenity.StringEditor], ['AllDay', () => Serenity.BooleanEditor], ['StartDate', () => Serenity.DateEditor], ['EndDate', () => Serenity.DateEditor], ['Url', () => Serenity.StringEditor], ['ClassName', () => Serenity.StringEditor], ['IsEditable', () => Serenity.BooleanEditor], ['IsOverlap', () => Serenity.BooleanEditor], ['Color', () => Serenity.StringEditor], ['BackgroundColor', () => Serenity.StringEditor], ['TextColor', () => Serenity.StringEditor]].forEach(x => Object.defineProperty(CalendarEventForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

