namespace CaseManagement.Case {
    export enum RequestActionAdmin {
        Deny = 3,
        Delete = 4
    }
    Serenity.Decorators.registerEnum(RequestActionAdmin, 'Case.RequestActionAdmin');
}

