namespace CaseManagement.Case {
    export enum SoftwareStatus {
        Yes = 1,
        No = 2,
        Pendding = 3
    }
    Serenity.Decorators.registerEnum(SoftwareStatus, 'Case.SoftwareStatus');
}

