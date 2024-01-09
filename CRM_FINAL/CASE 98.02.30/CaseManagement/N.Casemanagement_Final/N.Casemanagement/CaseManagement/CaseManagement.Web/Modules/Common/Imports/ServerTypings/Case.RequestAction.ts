namespace CaseManagement.Case {
    export enum RequestAction {
        Save = 1,
        Forward = 2,
        Deny = 3,
        Delete = 4
    }
    Serenity.Decorators.registerEnum(RequestAction, 'Case.RequestAction');
}

