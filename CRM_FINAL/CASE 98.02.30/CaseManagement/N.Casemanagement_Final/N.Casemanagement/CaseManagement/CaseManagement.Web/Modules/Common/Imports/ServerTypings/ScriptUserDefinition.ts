namespace CaseManagement {
    export interface ScriptUserDefinition {
        Username?: string
        DisplayName?: string
        Permissions?: { [key: string]: boolean }
    }
}

