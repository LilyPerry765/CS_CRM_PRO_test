﻿namespace CaseManagement.Administration {
    export interface TranslationListRequest extends Serenity.ListRequest {
        SourceLanguageID?: string
        TargetLanguageID?: string
    }
}

