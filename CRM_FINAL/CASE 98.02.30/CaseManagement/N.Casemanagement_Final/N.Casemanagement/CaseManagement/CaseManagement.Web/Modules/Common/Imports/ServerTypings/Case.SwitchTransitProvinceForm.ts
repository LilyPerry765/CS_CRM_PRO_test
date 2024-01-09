namespace CaseManagement.Case {
    export class SwitchTransitProvinceForm extends Serenity.PrefixedContext {
        static formKey = 'Case.SwitchTransitProvince';

    }

    export interface SwitchTransitProvinceForm {
        ProvinceId: Serenity.LookupEditor;
        SwitchTransitId: Serenity.LookupEditor;
    }

    [['ProvinceId', () => Serenity.LookupEditor], ['SwitchTransitId', () => Serenity.LookupEditor]].forEach(x => Object.defineProperty(SwitchTransitProvinceForm.prototype, <string>x[0], { get: function () { return this.w(x[0], (x[1] as any)()); }, enumerable: true, configurable: true }));
}

