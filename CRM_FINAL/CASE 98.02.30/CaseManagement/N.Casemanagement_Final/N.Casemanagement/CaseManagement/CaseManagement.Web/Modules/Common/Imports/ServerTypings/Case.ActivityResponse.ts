namespace CaseManagement.Case {
    export interface ActivityResponse extends Serenity.ServiceResponse {
        Values?: { [key: string]: any }[]
    }
}

