declare var Morris: any;
declare namespace CaseManagement.Common {
    class DashboardIndex extends Serenity.TemplatedDialog<any> {
        private areaChart;
        static ProvinceProgram95(): void;
    }
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowStepDialog extends Serenity.EntityDialog<WorkFlowStepRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: WorkFlowStepForm;
    }
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowStepGrid extends Serenity.EntityGrid<WorkFlowStepRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof WorkFlowStepDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowStatusTypeDialog extends Serenity.EntityDialog<WorkFlowStatusTypeRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: WorkFlowStatusTypeForm;
    }
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowStatusTypeGrid extends Serenity.EntityGrid<WorkFlowStatusTypeRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof WorkFlowStatusTypeDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowStatusDialog extends Serenity.EntityDialog<WorkFlowStatusRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: WorkFlowStatusForm;
    }
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowStatusGrid extends Serenity.EntityGrid<WorkFlowStatusRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof WorkFlowStatusDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowRuleDialog extends Serenity.EntityDialog<WorkFlowRuleRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: WorkFlowRuleForm;
    }
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowRuleGrid extends Serenity.EntityGrid<WorkFlowRuleRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof WorkFlowRuleDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowActionDialog extends Serenity.EntityDialog<WorkFlowActionRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: WorkFlowActionForm;
    }
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowActionGrid extends Serenity.EntityGrid<WorkFlowActionRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof WorkFlowActionDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare var Morris: any;
declare namespace CaseManagement.StimulReport {
    class UserProvinceActivityDetail extends Serenity.TemplatedDialog<any> {
        private areaChart;
        static initializePage(): void;
    }
}
declare var Morris: any;
declare namespace CaseManagement.StimulReport {
    class UserMonthActivityDetail extends Serenity.TemplatedDialog<any> {
        private areaChart;
        static initializePage(): void;
    }
}
declare var Morris: any;
declare namespace CaseManagement.StimulReport {
    class UserLeaderActivityDetail extends Serenity.TemplatedDialog<any> {
        private areaChart;
        static initializePage(): void;
    }
}
declare var Morris: any;
declare namespace CaseManagement.StimulReport {
    class ProvinceLineChart extends Serenity.TemplatedDialog<any> {
        private areaChart;
        static ProvinceProgram96(): void;
        static ProvinceProgram95(): void;
        static ProvinceProgram94(): void;
        static ProvinceProgram93(): void;
        static ProvinceProgram92(): void;
    }
}
declare var Morris: any;
declare namespace CaseManagement.StimulReport {
    class ProvinceActivityReport extends Serenity.TemplatedDialog<any> {
        private areaChart;
        static ProvinceActivityGroupList(): void;
        static ProvinceActivityList(GID: any): void;
    }
}
declare namespace CaseManagement.Messaging {
    class VwMessagesDialog extends Serenity.EntityDialog<VwMessagesRow, any> {
        protected getFormKey(): any;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: any;
    }
}
declare namespace CaseManagement.Messaging {
    class VwMessagesGrid extends Serenity.EntityGrid<VwMessagesRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof VwMessagesDialog;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Messaging {
    class SentDialog extends Serenity.EntityDialog<SentRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: SentForm;
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
    }
}
declare namespace CaseManagement.Messaging {
    class SentGrid extends Serenity.EntityGrid<SentRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof SentDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Messaging {
    class NewMessageDialog extends Serenity.EntityDialog<NewMessageRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: NewMessageForm;
    }
}
declare namespace CaseManagement.Messaging {
    class NewMessageGrid extends Serenity.EntityGrid<NewMessageRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof NewMessageDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Messaging {
    class InboxDialog extends Serenity.EntityDialog<InboxRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: InboxForm;
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
    }
}
declare namespace CaseManagement.Messaging {
    class InboxGrid extends Serenity.EntityGrid<InboxRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof InboxDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
        protected getItemCssClass(item: Messaging.InboxRow, index: number): string;
    }
}
declare namespace CaseManagement.StimulReport {
    class ActivityRequestReportGrid extends Serenity.EntityGrid<ActivityRequestReportRow, any> {
        protected getColumnsKey(): string;
        protected getIdProperty(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected createSlickGrid(): Slick.Grid;
        protected getSlickOptions(): Slick.GridOptions;
        protected usePager(): boolean;
        protected getButtons(): Serenity.ToolButton[];
        protected getQuickFilters(): Serenity.QuickFilter<Serenity.Widget<any>, any>[];
    }
}
declare namespace CaseManagement.StimulReport {
    class ActivityRequestDetailDialog extends Serenity.EntityDialog<ActivityRequestDetailRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ActivityRequestDetailForm;
    }
}
declare namespace CaseManagement.StimulReport {
    class ActivityRequestDetailGrid extends Serenity.EntityGrid<ActivityRequestDetailRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestDetailDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected createSlickGrid(): Slick.Grid;
        protected getSlickOptions(): Slick.GridOptions;
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Messaging {
    class VwMessagesDialog extends Serenity.EntityDialog<VwMessagesRow, any> {
        protected getFormKey(): any;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: any;
    }
}
declare namespace CaseManagement.Messaging {
    class VwMessagesGrid extends Serenity.EntityGrid<VwMessagesRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof VwMessagesDialog;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Messaging {
    class SentDialog extends Serenity.EntityDialog<SentRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: SentForm;
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
    }
}
declare namespace CaseManagement.Messaging {
    class SentGrid extends Serenity.EntityGrid<SentRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof SentDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Messaging {
    class NewMessageDialog extends Serenity.EntityDialog<NewMessageRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: NewMessageForm;
    }
}
declare namespace CaseManagement.Messaging {
    class NewMessageGrid extends Serenity.EntityGrid<NewMessageRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof NewMessageDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Messaging {
    class InboxDialog extends Serenity.EntityDialog<InboxRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: InboxForm;
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
    }
}
declare namespace CaseManagement.Messaging {
    class InboxGrid extends Serenity.EntityGrid<InboxRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof InboxDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
        protected getItemCssClass(item: Messaging.InboxRow, index: number): string;
    }
}
declare namespace CaseManagement.Membership {
    class LoginPanel extends Serenity.PropertyPanel<LoginRequest, any> {
        protected getFormKey(): string;
        private form;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Membership {
    class SignUpPanel extends Serenity.PropertyPanel<SignUpRequest, any> {
        protected getFormKey(): string;
        private form;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Membership {
    class ResetPasswordPanel extends Serenity.PropertyPanel<ResetPasswordRequest, any> {
        protected getFormKey(): string;
        private form;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Membership {
    class ForgotPasswordPanel extends Serenity.PropertyPanel<ForgotPasswordRequest, any> {
        protected getFormKey(): string;
        private form;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Membership {
    class ChangePasswordPanel extends Serenity.PropertyPanel<ChangePasswordRequest, any> {
        protected getFormKey(): string;
        private form;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.ScriptInitialization {
}
declare namespace CaseManagement.Common {
    class UserPreferenceStorage implements Serenity.SettingStorage {
        getItem(key: string): string;
        setItem(key: string, data: string): void;
    }
}
declare namespace CaseManagement.Common {
    interface PdfExportOptions {
        grid: Serenity.DataGrid<any, any>;
        onViewSubmit: () => boolean;
        title?: string;
        hint?: string;
        separator?: boolean;
        reportTitle?: string;
        titleTop?: number;
        titleFontSize?: number;
        fileName?: string;
        pageNumbers?: boolean;
        columnTitles?: {
            [key: string]: string;
        };
        tableOptions?: jsPDF.AutoTableOptions;
        output?: string;
        autoPrint?: boolean;
    }
    namespace PdfExportHelper {
        function exportToPdf(options: PdfExportOptions): void;
        function createToolButton(options: PdfExportOptions): Serenity.ToolButton;
    }
}
declare namespace CaseManagement.Common {
    class LanguageSelection extends Serenity.Widget<any> {
        constructor(select: JQuery, currentLanguage: string);
    }
}
declare namespace CaseManagement.Common {
    class SidebarSearch extends Serenity.Widget<any> {
        private menuUL;
        constructor(input: JQuery, menuUL: JQuery);
        protected updateMatchFlags(text: string): void;
    }
}
declare namespace CaseManagement.Common {
    class ThemeSelection extends Serenity.Widget<any> {
        constructor(select: JQuery);
        protected getCurrentTheme(): string;
    }
}
declare namespace CaseManagement.Administration {
    enum ActionLog {
        View = 1,
        Insert = 2,
        Update = 3,
        Delete = 4,
        Login = 5,
        Logout = 6,
    }
}
declare namespace CaseManagement.Administration {
}
declare namespace CaseManagement.Administration {
    class CalendarEventForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface CalendarEventForm {
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
}
declare namespace CaseManagement.Administration {
    interface CalendarEventRow {
        Id?: number;
        Title?: string;
        AllDay?: boolean;
        StartDate?: string;
        EndDate?: string;
        Url?: string;
        ClassName?: string;
        IsEditable?: boolean;
        IsOverlap?: boolean;
        Color?: string;
        BackgroundColor?: string;
        TextColor?: string;
    }
    namespace CalendarEventRow {
        const idProperty = "Id";
        const nameProperty = "Title";
        const localTextPrefix = "Administration.CalendarEvent";
        namespace Fields {
            const Id: string;
            const Title: string;
            const AllDay: string;
            const StartDate: string;
            const EndDate: string;
            const Url: string;
            const ClassName: string;
            const IsEditable: string;
            const IsOverlap: string;
            const Color: string;
            const BackgroundColor: string;
            const TextColor: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace CalendarEventService {
        const baseUrl = "Administration/CalendarEvent";
        function Create(request: Serenity.SaveRequest<CalendarEventRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<CalendarEventRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CalendarEventRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CalendarEventRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Administration {
}
declare namespace CaseManagement.Administration {
    class LanguageForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface LanguageForm {
        LanguageId: Serenity.StringEditor;
        LanguageName: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Administration {
    interface LanguageRow {
        Id?: number;
        LanguageId?: string;
        LanguageName?: string;
    }
    namespace LanguageRow {
        const idProperty = "Id";
        const nameProperty = "LanguageName";
        const localTextPrefix = "Administration.Language";
        const lookupKey = "Administration.Language";
        function getLookup(): Q.Lookup<LanguageRow>;
        namespace Fields {
            const Id: string;
            const LanguageId: string;
            const LanguageName: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace LanguageService {
        const baseUrl = "Administration/Language";
        function Create(request: Serenity.SaveRequest<LanguageRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<LanguageRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<LanguageRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<LanguageRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Administration {
}
declare namespace CaseManagement.Administration {
    class LogForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface LogForm {
        TableName: Serenity.StringEditor;
        PersianTableName: Serenity.StringEditor;
        RecordId: Serenity.StringEditor;
        RecordName: Serenity.IntegerEditor;
        ActionId: Serenity.EnumEditor;
        UserId: Serenity.LookupEditor;
        InsertDate: Serenity.DateEditor;
    }
}
declare namespace CaseManagement.Administration {
    interface LogRequest extends Serenity.ServiceRequest {
    }
}
declare namespace CaseManagement.Administration {
    interface LogResponse extends Serenity.ServiceResponse {
        Values?: {
            [key: string]: any;
        }[];
        ProvinceKey?: string[];
        Keys?: string[];
        Labels?: string;
    }
}
declare namespace CaseManagement.Administration {
    interface LogRow {
        Id?: number;
        TableName?: string;
        PersianTableName?: string;
        RecordId?: number;
        RecordName?: string;
        IP?: string;
        ActionID?: ActionLog;
        UserId?: number;
        InsertDate?: string;
        DisplayName?: string;
        ProvinceId?: number;
        ProvinceName?: string;
        OldData?: string;
    }
    namespace LogRow {
        const idProperty = "Id";
        const nameProperty = "TableName";
        const localTextPrefix = "Administration.Log";
        namespace Fields {
            const Id: string;
            const TableName: string;
            const PersianTableName: string;
            const RecordId: string;
            const RecordName: string;
            const IP: string;
            const ActionID: string;
            const UserId: string;
            const InsertDate: string;
            const DisplayName: string;
            const ProvinceId: string;
            const ProvinceName: string;
            const OldData: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace LogService {
        const baseUrl = "Administration/Log";
        function Create(request: Serenity.SaveRequest<LogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<LogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<LogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<LogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function LogProvinceReport(request: LogRequest, onSuccess?: (response: LogResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function LogLeaderReport(request: LogRequest, onSuccess?: (response: LogResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
            const LogProvinceReport: string;
            const LogLeaderReport: string;
        }
    }
}
declare namespace CaseManagement.Administration {
}
declare namespace CaseManagement.Administration {
    class NotificationForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface NotificationForm {
        GroupId: Serenity.IntegerEditor;
        UserId: Serenity.IntegerEditor;
        Message: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Administration {
}
declare namespace CaseManagement.Administration {
    class NotificationGroupForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface NotificationGroupForm {
        Name: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Administration {
    interface NotificationGroupRow {
        Id?: number;
        Name?: string;
    }
    namespace NotificationGroupRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Administration.NotificationGroup";
        namespace Fields {
            const Id: string;
            const Name: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace NotificationGroupService {
        const baseUrl = "Administration/NotificationGroup";
        function Create(request: Serenity.SaveRequest<NotificationGroupRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<NotificationGroupRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<NotificationGroupRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<NotificationGroupRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    interface NotificationRow {
        Id?: number;
        GroupId?: number;
        UserId?: number;
        Message?: string;
        InsertDate?: string;
    }
    namespace NotificationRow {
        const idProperty = "Id";
        const nameProperty = "Message";
        const localTextPrefix = "Administration.Notification";
        namespace Fields {
            const Id: string;
            const GroupId: string;
            const UserId: string;
            const Message: string;
            const InsertDate: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace NotificationService {
        const baseUrl = "Administration/Notification";
        function Create(request: Serenity.SaveRequest<NotificationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<NotificationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<NotificationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<NotificationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Administration {
}
declare namespace CaseManagement.Administration {
    class RoleForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface RoleForm {
        RoleName: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Administration {
    interface RolePermissionListRequest extends Serenity.ServiceRequest {
        RoleID?: number;
        Module?: string;
        Submodule?: string;
    }
}
declare namespace CaseManagement.Administration {
    interface RolePermissionListResponse extends Serenity.ListResponse<string> {
    }
}
declare namespace CaseManagement.Administration {
    interface RolePermissionRow {
        RolePermissionId?: number;
        RoleId?: number;
        PermissionKey?: string;
        RoleRoleName?: string;
    }
    namespace RolePermissionRow {
        const idProperty = "RolePermissionId";
        const nameProperty = "PermissionKey";
        const localTextPrefix = "Administration.RolePermission";
        namespace Fields {
            const RolePermissionId: string;
            const RoleId: string;
            const PermissionKey: string;
            const RoleRoleName: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace RolePermissionService {
        const baseUrl = "Administration/RolePermission";
        function Update(request: RolePermissionUpdateRequest, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: RolePermissionListRequest, onSuccess?: (response: RolePermissionListResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Update: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    interface RolePermissionUpdateRequest extends Serenity.ServiceRequest {
        RoleID?: number;
        Module?: string;
        Submodule?: string;
        Permissions?: string[];
    }
}
declare namespace CaseManagement.Administration {
    interface RoleRow {
        RoleId?: number;
        RoleName?: string;
    }
    namespace RoleRow {
        const idProperty = "RoleId";
        const nameProperty = "RoleName";
        const localTextPrefix = "Administration.Role";
        const lookupKey = "Administration.Role";
        function getLookup(): Q.Lookup<RoleRow>;
        namespace Fields {
            const RoleId: string;
            const RoleName: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace RoleService {
        const baseUrl = "Administration/Role";
        function Create(request: Serenity.SaveRequest<RoleRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<RoleRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<RoleRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<RoleRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    interface RoleStepListRequest extends Serenity.ServiceRequest {
        RoleID?: number;
    }
}
declare namespace CaseManagement.Administration {
    interface RoleStepListResponse extends Serenity.ListResponse<number> {
    }
}
declare namespace CaseManagement.Administration {
    interface RoleStepRow {
        Id?: number;
        RoleId?: number;
        StepId?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        RoleRoleName?: string;
        StepName?: string;
        StepCreatedUserId?: number;
        StepCreatedDate?: string;
        StepModifiedUserId?: number;
        StepModifiedDate?: string;
        StepIsDeleted?: boolean;
        StepDeletedUserId?: number;
        StepDeletedDate?: string;
    }
    namespace RoleStepRow {
        const idProperty = "Id";
        const localTextPrefix = "Administration.RoleStep";
        namespace Fields {
            const Id: string;
            const RoleId: string;
            const StepId: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const RoleRoleName: string;
            const StepName: string;
            const StepCreatedUserId: string;
            const StepCreatedDate: string;
            const StepModifiedUserId: string;
            const StepModifiedDate: string;
            const StepIsDeleted: string;
            const StepDeletedUserId: string;
            const StepDeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace RoleStepService {
        const baseUrl = "Administration/RoleStep";
        function Update(request: RoleStepUpdateRequest, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: RoleStepListRequest, onSuccess?: (response: RoleStepListResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Update: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    interface RoleStepUpdateRequest extends Serenity.ServiceRequest {
        RoleID?: number;
        Steps?: number[];
    }
}
declare namespace CaseManagement.Administration {
    interface TranslationItem {
        Key?: string;
        SourceText?: string;
        TargetText?: string;
        CustomText?: string;
    }
}
declare namespace CaseManagement.Administration {
    interface TranslationListRequest extends Serenity.ListRequest {
        SourceLanguageID?: string;
        TargetLanguageID?: string;
    }
}
declare namespace CaseManagement.Administration {
    namespace TranslationService {
        const baseUrl = "Administration/Translation";
        function List(request: TranslationListRequest, onSuccess?: (response: Serenity.ListResponse<TranslationItem>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: TranslationUpdateRequest, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const List: string;
            const Update: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    interface TranslationUpdateRequest extends Serenity.ServiceRequest {
        TargetLanguageID?: string;
        Translations?: {
            [key: string]: string;
        };
    }
}
declare namespace CaseManagement.Administration {
}
declare namespace CaseManagement.Administration {
    class UserForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface UserForm {
        Username: Serenity.StringEditor;
        DisplayName: Serenity.StringEditor;
        EmployeeID: Serenity.StringEditor;
        Email: Serenity.StringEditor;
        Rank: Serenity.StringEditor;
        Password: Serenity.PasswordEditor;
        PasswordConfirm: Serenity.PasswordEditor;
        TelephoneNo1: Serenity.StringEditor;
        MobileNo: Serenity.StringEditor;
        Degree: Serenity.StringEditor;
        ProvinceId: Serenity.LookupEditor;
        ProvinceList: Serenity.LookupEditor;
        IsActive: Serenity.BooleanEditor;
        IsDeleted: Serenity.BooleanEditor;
        ImagePath: Serenity.ImageUploadEditor;
    }
}
declare namespace CaseManagement.Administration {
    interface UserPermissionListRequest extends Serenity.ServiceRequest {
        UserID?: number;
        Module?: string;
        Submodule?: string;
    }
}
declare namespace CaseManagement.Administration {
    interface UserPermissionRow {
        UserPermissionId?: number;
        UserId?: number;
        PermissionKey?: string;
        Granted?: boolean;
        Username?: string;
        User?: string;
    }
    namespace UserPermissionRow {
        const idProperty = "UserPermissionId";
        const nameProperty = "PermissionKey";
        const localTextPrefix = "Administration.UserPermission";
        namespace Fields {
            const UserPermissionId: string;
            const UserId: string;
            const PermissionKey: string;
            const Granted: string;
            const Username: string;
            const User: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace UserPermissionService {
        const baseUrl = "Administration/UserPermission";
        function Update(request: UserPermissionUpdateRequest, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: UserPermissionListRequest, onSuccess?: (response: Serenity.ListResponse<UserPermissionRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ListRolePermissions(request: UserPermissionListRequest, onSuccess?: (response: Serenity.ListResponse<string>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ListPermissionKeys(request: Serenity.ServiceRequest, onSuccess?: (response: Serenity.ListResponse<string>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Update: string;
            const List: string;
            const ListRolePermissions: string;
            const ListPermissionKeys: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    interface UserPermissionUpdateRequest extends Serenity.ServiceRequest {
        UserID?: number;
        Module?: string;
        Submodule?: string;
        Permissions?: UserPermissionRow[];
    }
}
declare namespace CaseManagement.Administration {
    interface UserRoleListRequest extends Serenity.ServiceRequest {
        UserID?: number;
    }
}
declare namespace CaseManagement.Administration {
    interface UserRoleListResponse extends Serenity.ListResponse<number> {
    }
}
declare namespace CaseManagement.Administration {
    interface UserRoleRow {
        UserRoleId?: number;
        UserId?: number;
        RoleId?: number;
        Username?: string;
        User?: string;
    }
    namespace UserRoleRow {
        const idProperty = "UserRoleId";
        const localTextPrefix = "Administration.UserRole";
        namespace Fields {
            const UserRoleId: string;
            const UserId: string;
            const RoleId: string;
            const Username: string;
            const User: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace UserRoleService {
        const baseUrl = "Administration/UserRole";
        function Update(request: UserRoleUpdateRequest, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: UserRoleListRequest, onSuccess?: (response: UserRoleListResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Update: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    interface UserRoleUpdateRequest extends Serenity.ServiceRequest {
        UserID?: number;
        Roles?: number[];
    }
}
declare namespace CaseManagement.Administration {
    interface UserRow {
        UserId?: number;
        Username?: string;
        Source?: string;
        PasswordHash?: string;
        PasswordSalt?: string;
        DisplayName?: string;
        EmployeeID?: string;
        Rank?: string;
        Email?: string;
        LastDirectoryUpdate?: string;
        IsActive?: number;
        Password?: string;
        PasswordConfirm?: string;
        TelephoneNo1?: string;
        TelephoneNo2?: string;
        MobileNo?: string;
        Degree?: string;
        ProvinceId?: number;
        ProvinceName?: string;
        IsIranTCI?: UserTCI;
        ProvinceList?: number[];
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        LastLoginDate?: string;
        ImagePath?: string;
        InsertUserId?: number;
        InsertDate?: string;
        UpdateUserId?: number;
        UpdateDate?: string;
    }
    namespace UserRow {
        const idProperty = "UserId";
        const isActiveProperty = "IsActive";
        const nameProperty = "DisplayName";
        const localTextPrefix = "Administration.User";
        const lookupKey = "Administration.User";
        function getLookup(): Q.Lookup<UserRow>;
        namespace Fields {
            const UserId: string;
            const Username: string;
            const Source: string;
            const PasswordHash: string;
            const PasswordSalt: string;
            const DisplayName: string;
            const EmployeeID: string;
            const Rank: string;
            const Email: string;
            const LastDirectoryUpdate: string;
            const IsActive: string;
            const Password: string;
            const PasswordConfirm: string;
            const TelephoneNo1: string;
            const TelephoneNo2: string;
            const MobileNo: string;
            const Degree: string;
            const ProvinceId: string;
            const ProvinceName: string;
            const IsIranTCI: string;
            const ProvinceList: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const LastLoginDate: string;
            const ImagePath: string;
            const InsertUserId: string;
            const InsertDate: string;
            const UpdateUserId: string;
            const UpdateDate: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace UserService {
        const baseUrl = "Administration/User";
        function Create(request: Serenity.SaveRequest<UserRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<UserRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Undelete(request: Serenity.UndeleteRequest, onSuccess?: (response: Serenity.UndeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<UserRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<UserRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Undelete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Administration {
}
declare namespace CaseManagement.Administration {
    class UserSupportGroupForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface UserSupportGroupForm {
        UserId: Serenity.IntegerEditor;
        GroupId: Serenity.IntegerEditor;
    }
}
declare namespace CaseManagement.Administration {
    interface UserSupportGroupRow {
        Id?: number;
        UserId?: number;
        GroupId?: number;
        UserOldcaseId?: number;
        UserUsername?: string;
        UserDisplayName?: string;
        UserEmployeeId?: string;
        UserEmail?: string;
        UserRank?: string;
        UserSource?: string;
        UserPassword?: string;
        UserPasswordHash?: string;
        UserPasswordSalt?: string;
        UserInsertDate?: string;
        UserInsertUserId?: number;
        UserUpdateDate?: string;
        UserUpdateUserId?: number;
        UserIsActive?: number;
        UserLastDirectoryUpdate?: string;
        UserRoleId?: number;
        UserTelephoneNo1?: string;
        UserTelephoneNo2?: string;
        UserMobileNo?: string;
        UserDegree?: string;
        UserProvinceId?: number;
    }
    namespace UserSupportGroupRow {
        const idProperty = "Id";
        const localTextPrefix = "Administration.UserSupportGroup";
        namespace Fields {
            const Id: string;
            const UserId: string;
            const GroupId: string;
            const UserOldcaseId: string;
            const UserUsername: string;
            const UserDisplayName: string;
            const UserEmployeeId: string;
            const UserEmail: string;
            const UserRank: string;
            const UserSource: string;
            const UserPassword: string;
            const UserPasswordHash: string;
            const UserPasswordSalt: string;
            const UserInsertDate: string;
            const UserInsertUserId: string;
            const UserUpdateDate: string;
            const UserUpdateUserId: string;
            const UserIsActive: string;
            const UserLastDirectoryUpdate: string;
            const UserRoleId: string;
            const UserTelephoneNo1: string;
            const UserTelephoneNo2: string;
            const UserMobileNo: string;
            const UserDegree: string;
            const UserProvinceId: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    namespace UserSupportGroupService {
        const baseUrl = "Administration/UserSupportGroup";
        function Create(request: Serenity.SaveRequest<UserSupportGroupRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<UserSupportGroupRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<UserSupportGroupRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<UserSupportGroupRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Administration {
    enum UserTCI {
        Iran = 1,
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityCorrectionOperationForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityCorrectionOperationForm {
        Body: Serenity.TextAreaEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityCorrectionOperationRow {
        Id?: number;
        ActivityId?: number;
        Body?: string;
    }
    namespace ActivityCorrectionOperationRow {
        const idProperty = "Id";
        const nameProperty = "Body";
        const localTextPrefix = "Case.ActivityCorrectionOperation";
        const lookupKey = "Case.ActivityCorrectionOperation";
        function getLookup(): Q.Lookup<ActivityCorrectionOperationRow>;
        namespace Fields {
            const Id: string;
            const ActivityId: string;
            const Body: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityCorrectionOperationService {
        const baseUrl = "Case/ActivityCorrectionOperation";
        function Create(request: Serenity.SaveRequest<ActivityCorrectionOperationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityCorrectionOperationRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityCorrectionOperationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityCorrectionOperationRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    class ActivityForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityForm {
        Code: Serenity.IntegerEditor;
        Name: Serenity.TextAreaEditor;
        EnglishName: Serenity.TextAreaEditor;
        Objective: Serenity.TextAreaEditor;
        EnglishObjective: Serenity.TextAreaEditor;
        GroupId: Serenity.LookupEditor;
        RepeatTermId: Serenity.LookupEditor;
        RequiredYearRepeatCount: Serenity.LookupEditor;
        Factor: Serenity.StringEditor;
        KeyCheckArea: Serenity.TextAreaEditor;
        DataSource: Serenity.TextAreaEditor;
        Methodology: Serenity.TextAreaEditor;
        KeyFocus: Serenity.TextAreaEditor;
        Action: Serenity.TextAreaEditor;
        KPI: Serenity.TextAreaEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReasonList: ActivityMainReasonEditor;
        CorrectionOperationList: ActivityCorrectionOperationEditor;
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityGroupForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityGroupForm {
        Name: Serenity.StringEditor;
        EnglishName: Serenity.StringEditor;
        Code: Serenity.IntegerEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityGroupRow {
        Id?: number;
        Name?: string;
        EnglishName?: string;
        Code?: number;
    }
    namespace ActivityGroupRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.ActivityGroup";
        const lookupKey = "Case.ActivityGroup";
        function getLookup(): Q.Lookup<ActivityGroupRow>;
        namespace Fields {
            const Id: string;
            const Name: string;
            const EnglishName: string;
            const Code: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityGroupService {
        const baseUrl = "Case/ActivityGroup";
        function Create(request: Serenity.SaveRequest<ActivityGroupRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityGroupRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityGroupRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityGroupRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityHelpForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityHelpForm {
        Code: Serenity.IntegerEditor;
        Name: Serenity.TextAreaEditor;
        EnglishName: Serenity.TextAreaEditor;
        Objective: Serenity.TextAreaEditor;
        EnglishObjective: Serenity.TextAreaEditor;
        GroupId: Serenity.LookupEditor;
        RepeatTermId: Serenity.LookupEditor;
        KeyCheckArea: Serenity.TextAreaEditor;
        DataSource: Serenity.TextAreaEditor;
        Methodology: Serenity.TextAreaEditor;
        KeyFocus: Serenity.TextAreaEditor;
        Action: Serenity.TextAreaEditor;
        KPI: Serenity.TextAreaEditor;
        EventDescription: Serenity.TextAreaEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityHelpRow {
        Id?: number;
        Code?: string;
        Name?: string;
        CodeName?: string;
        EnglishName?: string;
        Objective?: string;
        EnglishObjective?: string;
        GroupId?: number;
        RepeatTermId?: number;
        GroupName?: string;
        RepeatTermName?: string;
        KeyCheckArea?: string;
        DataSource?: string;
        Methodology?: string;
        KeyFocus?: string;
        Action?: string;
        KPI?: string;
        EventDescription?: string;
    }
    namespace ActivityHelpRow {
        const idProperty = "Id";
        const nameProperty = "CodeName";
        const localTextPrefix = "Case.ActivityHelp";
        const lookupKey = "Case.ActivityHelp";
        function getLookup(): Q.Lookup<ActivityHelpRow>;
        namespace Fields {
            const Id: string;
            const Code: string;
            const Name: string;
            const CodeName: string;
            const EnglishName: string;
            const Objective: string;
            const EnglishObjective: string;
            const GroupId: string;
            const RepeatTermId: string;
            const GroupName: string;
            const RepeatTermName: string;
            const KeyCheckArea: string;
            const DataSource: string;
            const Methodology: string;
            const KeyFocus: string;
            const Action: string;
            const KPI: string;
            const EventDescription: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityHelpService {
        const baseUrl = "Case/ActivityHelp";
        function Create(request: Serenity.SaveRequest<ActivityHelpRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityHelpRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityHelpRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityHelpRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityMainReasonForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityMainReasonForm {
        Body: Serenity.TextAreaEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityMainReasonRow {
        Id?: number;
        ActivityId?: number;
        Body?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace ActivityMainReasonRow {
        const idProperty = "Id";
        const nameProperty = "Body";
        const localTextPrefix = "Case.ActivityMainReason";
        const lookupKey = "Case.ActivityMainReason";
        function getLookup(): Q.Lookup<ActivityMainReasonRow>;
        namespace Fields {
            const Id: string;
            const ActivityId: string;
            const Body: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityMainReasonService {
        const baseUrl = "Case/ActivityMainReason";
        function Create(request: Serenity.SaveRequest<ActivityMainReasonRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityMainReasonRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityMainReasonRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityMainReasonRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequest extends Serenity.ServiceRequest {
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestCommentForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestCommentForm {
        Comment: Serenity.TextAreaEditor;
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestCommentReasonForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestCommentReasonForm {
        CommentReasonId: Serenity.IntegerEditor;
        ActivityRequestId: Serenity.IntegerEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestCommentReasonRow {
        Id?: number;
        CommentReasonId?: number;
        ActivityRequestId?: number;
        CommentReasonComment?: string;
        CommentReasonCreatedUserId?: number;
        CommentReasonCreatedDate?: string;
        ActivityRequestOldCaseId?: number;
        ActivityRequestProvinceId?: number;
        ActivityRequestActivityId?: number;
        ActivityRequestCycleId?: number;
        ActivityRequestCustomerEffectId?: number;
        ActivityRequestRiskLevelId?: number;
        ActivityRequestIncomeFlowId?: number;
        ActivityRequestCount?: number;
        ActivityRequestCycleCost?: number;
        ActivityRequestFactor?: number;
        ActivityRequestDelayedCost?: number;
        ActivityRequestYearCost?: number;
        ActivityRequestAccessibleCost?: number;
        ActivityRequestInaccessibleCost?: number;
        ActivityRequestFinancial?: number;
        ActivityRequestLeakDate?: string;
        ActivityRequestDiscoverLeakDate?: string;
        ActivityRequestDiscoverLeakDateShamsi?: string;
        ActivityRequestEventDescription?: string;
        ActivityRequestMainReason?: string;
        ActivityRequestCorrectionOperation?: string;
        ActivityRequestAvoidRepeatingOperation?: string;
        ActivityRequestCreatedUserId?: number;
        ActivityRequestCreatedDate?: string;
        ActivityRequestCreatedDateShamsi?: string;
        ActivityRequestModifiedUserId?: number;
        ActivityRequestModifiedDate?: string;
        ActivityRequestIsDeleted?: boolean;
        ActivityRequestDeletedUserId?: number;
        ActivityRequestDeletedDate?: string;
        ActivityRequestEndDate?: string;
        ActivityRequestStatusId?: number;
        ActivityRequestActionId?: number;
    }
    namespace ActivityRequestCommentReasonRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ActivityRequestCommentReason";
        namespace Fields {
            const Id: string;
            const CommentReasonId: string;
            const ActivityRequestId: string;
            const CommentReasonComment: string;
            const CommentReasonCreatedUserId: string;
            const CommentReasonCreatedDate: string;
            const ActivityRequestOldCaseId: string;
            const ActivityRequestProvinceId: string;
            const ActivityRequestActivityId: string;
            const ActivityRequestCycleId: string;
            const ActivityRequestCustomerEffectId: string;
            const ActivityRequestRiskLevelId: string;
            const ActivityRequestIncomeFlowId: string;
            const ActivityRequestCount: string;
            const ActivityRequestCycleCost: string;
            const ActivityRequestFactor: string;
            const ActivityRequestDelayedCost: string;
            const ActivityRequestYearCost: string;
            const ActivityRequestAccessibleCost: string;
            const ActivityRequestInaccessibleCost: string;
            const ActivityRequestFinancial: string;
            const ActivityRequestLeakDate: string;
            const ActivityRequestDiscoverLeakDate: string;
            const ActivityRequestDiscoverLeakDateShamsi: string;
            const ActivityRequestEventDescription: string;
            const ActivityRequestMainReason: string;
            const ActivityRequestCorrectionOperation: string;
            const ActivityRequestAvoidRepeatingOperation: string;
            const ActivityRequestCreatedUserId: string;
            const ActivityRequestCreatedDate: string;
            const ActivityRequestCreatedDateShamsi: string;
            const ActivityRequestModifiedUserId: string;
            const ActivityRequestModifiedDate: string;
            const ActivityRequestIsDeleted: string;
            const ActivityRequestDeletedUserId: string;
            const ActivityRequestDeletedDate: string;
            const ActivityRequestEndDate: string;
            const ActivityRequestStatusId: string;
            const ActivityRequestActionId: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestCommentReasonService {
        const baseUrl = "Case/ActivityRequestCommentReason";
        function Create(request: Serenity.SaveRequest<ActivityRequestCommentReasonRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestCommentReasonRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestCommentReasonRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestCommentReasonRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestCommentRow {
        Id?: number;
        Comment?: string;
        ActivityRequestId?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        CreatedUserName?: string;
    }
    namespace ActivityRequestCommentRow {
        const idProperty = "Id";
        const nameProperty = "Comment";
        const localTextPrefix = "Case.ActivityRequestComment";
        const lookupKey = "Case.ActivityRequestComment";
        function getLookup(): Q.Lookup<ActivityRequestCommentRow>;
        namespace Fields {
            const Id: string;
            const Comment: string;
            const ActivityRequestId: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const CreatedUserName: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestCommentService {
        const baseUrl = "Case/ActivityRequestComment";
        function Create(request: Serenity.SaveRequest<ActivityRequestCommentRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestCommentRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestCommentRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestCommentRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestConfirmAdminForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestConfirmAdminForm {
        ProvinceName: Serenity.StringEditor;
        Id: Serenity.StringEditor;
        ActivityId: Serenity.LookupEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.DecimalEditor;
        Factor: Serenity.DecimalEditor;
        DelayedCost: Serenity.DecimalEditor;
        AccessibleCost: Serenity.DecimalEditor;
        InaccessibleCost: Serenity.DecimalEditor;
        YearCost: Serenity.DecimalEditor;
        TotalLeakage: Serenity.DecimalEditor;
        RecoverableLeakage: Serenity.DecimalEditor;
        Recovered: Serenity.DecimalEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReason: Serenity.TextAreaEditor;
        CommnetList: ActivityRequestCommentEditor;
        File1: Serenity.ImageUploadEditor;
        File2: Serenity.ImageUploadEditor;
        ActionID: Serenity.EnumEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestConfirmAdminRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        SendDate?: string;
        SendUserId?: number;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
        ActionID?: RequestActionAdmin;
        ConfirmTypeID?: ConfirmType;
        IsRejected?: boolean;
        RejectCount?: number;
        CommentReasonList?: number[];
        CommnetList?: ActivityRequestCommentRow[];
        File1?: string;
        File2?: string;
        File3?: string;
        FinancialControllerConfirm?: boolean;
    }
    namespace ActivityRequestConfirmAdminRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ActivityRequestConfirmAdmin";
        namespace Fields {
            const Id: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const Financial: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const EventDescription: string;
            const MainReason: string;
            const CorrectionOperation: string;
            const AvoidRepeatingOperation: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const SendDate: string;
            const SendUserId: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const ActivityId: string;
            const ProvinceId: string;
            const CycleId: string;
            const CustomerEffectId: string;
            const IncomeFlowId: string;
            const RiskLevelId: string;
            const StatusID: string;
            const LeakDate: string;
            const DiscoverLeakDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityObjective: string;
            const ActivityGroupId: string;
            const ProvinceName: string;
            const CycleName: string;
            const CustomerEffectName: string;
            const IncomeFlowName: string;
            const RiskLevelName: string;
            const StatusName: string;
            const CreatedUserName: string;
            const ModifiedUserName: string;
            const DeletedUserName: string;
            const ActionID: string;
            const ConfirmTypeID: string;
            const IsRejected: string;
            const RejectCount: string;
            const CommentReasonList: string;
            const CommnetList: string;
            const File1: string;
            const File2: string;
            const File3: string;
            const FinancialControllerConfirm: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestConfirmAdminService {
        const baseUrl = "Case/ActivityRequestConfirmAdmin";
        function Create(request: Serenity.SaveRequest<ActivityRequestConfirmAdminRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestConfirmAdminRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestConfirmAdminRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestConfirmAdminRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestConfirmForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestConfirmForm {
        ProvinceName: Serenity.StringEditor;
        Id: Serenity.StringEditor;
        ActivityId: Serenity.LookupEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        CycleCostHistory: Serenity.DecimalEditor;
        DelayedCostHistory: Serenity.DecimalEditor;
        AccessibleCostHistory: Serenity.DecimalEditor;
        InaccessibleCostHistory: Serenity.DecimalEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.DecimalEditor;
        Factor: Serenity.DecimalEditor;
        DelayedCost: Serenity.DecimalEditor;
        AccessibleCost: Serenity.DecimalEditor;
        InaccessibleCost: Serenity.DecimalEditor;
        YearCost: Serenity.DecimalEditor;
        TotalLeakage: Serenity.DecimalEditor;
        RecoverableLeakage: Serenity.DecimalEditor;
        Recovered: Serenity.DecimalEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReason: Serenity.TextAreaEditor;
        CommnetList: ActivityRequestCommentEditor;
        File1: Serenity.ImageUploadEditor;
        File2: Serenity.ImageUploadEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestConfirmRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
        ActionID?: RequestAction;
        ConfirmTypeID?: ConfirmType;
        IsRejected?: boolean;
        CommentReasonList?: number[];
        CommnetList?: ActivityRequestCommentRow[];
        File1?: string;
        File2?: string;
        File3?: string;
        FinancialControllerConfirm?: boolean;
        CycleCostHistory?: number;
        DelayedCostHistory?: number;
        YearCostHistory?: number;
        AccessibleCostHistory?: number;
        InaccessibleCostHistory?: number;
        TotalLeakageHistory?: number;
        RecoverableLeakageHistory?: number;
        RecoveredHistory?: number;
    }
    namespace ActivityRequestConfirmRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ActivityRequestConfirm";
        namespace Fields {
            const Id: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const Financial: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const EventDescription: string;
            const MainReason: string;
            const CorrectionOperation: string;
            const AvoidRepeatingOperation: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const ActivityId: string;
            const ProvinceId: string;
            const CycleId: string;
            const CustomerEffectId: string;
            const IncomeFlowId: string;
            const RiskLevelId: string;
            const StatusID: string;
            const LeakDate: string;
            const DiscoverLeakDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityObjective: string;
            const ActivityGroupId: string;
            const ProvinceName: string;
            const CycleName: string;
            const CustomerEffectName: string;
            const IncomeFlowName: string;
            const RiskLevelName: string;
            const StatusName: string;
            const CreatedUserName: string;
            const ModifiedUserName: string;
            const DeletedUserName: string;
            const ActionID: string;
            const ConfirmTypeID: string;
            const IsRejected: string;
            const CommentReasonList: string;
            const CommnetList: string;
            const File1: string;
            const File2: string;
            const File3: string;
            const FinancialControllerConfirm: string;
            const CycleCostHistory: string;
            const DelayedCostHistory: string;
            const YearCostHistory: string;
            const AccessibleCostHistory: string;
            const InaccessibleCostHistory: string;
            const TotalLeakageHistory: string;
            const RecoverableLeakageHistory: string;
            const RecoveredHistory: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestConfirmService {
        const baseUrl = "Case/ActivityRequestConfirm";
        function Create(request: Serenity.SaveRequest<ActivityRequestConfirmRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestConfirmRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestConfirmRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestConfirmRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestDeleteForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestDeleteForm {
        ProvinceName: Serenity.StringEditor;
        Id: Serenity.StringEditor;
        ActivityId: Serenity.LookupEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.DecimalEditor;
        Factor: Serenity.StringEditor;
        DelayedCost: Serenity.DecimalEditor;
        AccessibleCost: Serenity.DecimalEditor;
        InaccessibleCost: Serenity.DecimalEditor;
        YearCost: Serenity.DecimalEditor;
        TotalLeakage: Serenity.DecimalEditor;
        RecoverableLeakage: Serenity.DecimalEditor;
        Recovered: Serenity.DecimalEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReason: Serenity.TextAreaEditor;
        CommnetList: ActivityRequestCommentEditor;
        File1: Serenity.ImageUploadEditor;
        File2: Serenity.ImageUploadEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestDeleteRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        SendDate?: string;
        SendUserId?: number;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
        SendUserName?: string;
        ActionID?: RequestAction;
        ConfirmTypeID?: ConfirmType;
        IsRejected?: boolean;
        RejectCount?: number;
        CommentReasonList?: number[];
        CommnetList?: ActivityRequestCommentRow[];
        File1?: string;
        File2?: string;
        File3?: string;
        FinancialControllerConfirm?: boolean;
    }
    namespace ActivityRequestDeleteRow {
        const idProperty = "Id";
        const nameProperty = "ActivityCode";
        const localTextPrefix = "Case.ActivityRequestDelete";
        namespace Fields {
            const Id: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const Financial: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const EventDescription: string;
            const MainReason: string;
            const CorrectionOperation: string;
            const AvoidRepeatingOperation: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const SendDate: string;
            const SendUserId: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const ActivityId: string;
            const ProvinceId: string;
            const CycleId: string;
            const CustomerEffectId: string;
            const IncomeFlowId: string;
            const RiskLevelId: string;
            const StatusID: string;
            const LeakDate: string;
            const DiscoverLeakDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityObjective: string;
            const ActivityGroupId: string;
            const ProvinceName: string;
            const CycleName: string;
            const CustomerEffectName: string;
            const IncomeFlowName: string;
            const RiskLevelName: string;
            const StatusName: string;
            const CreatedUserName: string;
            const ModifiedUserName: string;
            const DeletedUserName: string;
            const SendUserName: string;
            const ActionID: string;
            const ConfirmTypeID: string;
            const IsRejected: string;
            const RejectCount: string;
            const CommentReasonList: string;
            const CommnetList: string;
            const File1: string;
            const File2: string;
            const File3: string;
            const FinancialControllerConfirm: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestDeleteService {
        const baseUrl = "Case/ActivityRequestDelete";
        function Create(request: Serenity.SaveRequest<ActivityRequestDeleteRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestDeleteRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestDeleteRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestDeleteRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestDenyForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestDenyForm {
        ProvinceName: Serenity.StringEditor;
        Id: Serenity.StringEditor;
        ActivityId: Serenity.LookupEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        CycleCostHistory: Serenity.DecimalEditor;
        DelayedCostHistory: Serenity.DecimalEditor;
        AccessibleCostHistory: Serenity.DecimalEditor;
        InaccessibleCostHistory: Serenity.DecimalEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.DecimalEditor;
        Factor: Serenity.DecimalEditor;
        DelayedCost: Serenity.DecimalEditor;
        AccessibleCost: Serenity.DecimalEditor;
        InaccessibleCost: Serenity.DecimalEditor;
        YearCost: Serenity.DecimalEditor;
        TotalLeakage: Serenity.DecimalEditor;
        RecoverableLeakage: Serenity.DecimalEditor;
        Recovered: Serenity.DecimalEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReason: Serenity.TextAreaEditor;
        CommnetList: ActivityRequestCommentEditor;
        File1: Serenity.ImageUploadEditor;
        File2: Serenity.ImageUploadEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestDenyRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        SendDate?: string;
        SendUserId?: number;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
        SendUserName?: string;
        ActionID?: RequestAction;
        ConfirmTypeID?: ConfirmType;
        IsRejected?: boolean;
        CommentReasonList?: number[];
        CommnetList?: ActivityRequestCommentRow[];
        File1?: string;
        File2?: string;
        File3?: string;
        CycleCostHistory?: number;
        DelayedCostHistory?: number;
        YearCostHistory?: number;
        AccessibleCostHistory?: number;
        InaccessibleCostHistory?: number;
        TotalLeakageHistory?: number;
        RecoverableLeakageHistory?: number;
        RecoveredHistory?: number;
    }
    namespace ActivityRequestDenyRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ActivityRequestDeny";
        namespace Fields {
            const Id: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const Financial: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const EventDescription: string;
            const MainReason: string;
            const CorrectionOperation: string;
            const AvoidRepeatingOperation: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const SendDate: string;
            const SendUserId: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const ActivityId: string;
            const ProvinceId: string;
            const CycleId: string;
            const CustomerEffectId: string;
            const IncomeFlowId: string;
            const RiskLevelId: string;
            const StatusID: string;
            const LeakDate: string;
            const DiscoverLeakDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityObjective: string;
            const ActivityGroupId: string;
            const ProvinceName: string;
            const CycleName: string;
            const CustomerEffectName: string;
            const IncomeFlowName: string;
            const RiskLevelName: string;
            const StatusName: string;
            const CreatedUserName: string;
            const ModifiedUserName: string;
            const DeletedUserName: string;
            const SendUserName: string;
            const ActionID: string;
            const ConfirmTypeID: string;
            const IsRejected: string;
            const CommentReasonList: string;
            const CommnetList: string;
            const File1: string;
            const File2: string;
            const File3: string;
            const CycleCostHistory: string;
            const DelayedCostHistory: string;
            const YearCostHistory: string;
            const AccessibleCostHistory: string;
            const InaccessibleCostHistory: string;
            const TotalLeakageHistory: string;
            const RecoverableLeakageHistory: string;
            const RecoveredHistory: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestDenyService {
        const baseUrl = "Case/ActivityRequestDeny";
        function Create(request: Serenity.SaveRequest<ActivityRequestDenyRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestDenyRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestDenyRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestDenyRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestDetailsInfoForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestDetailsInfoForm {
        Id: Serenity.StringEditor;
        ProvinceId: Serenity.IntegerEditor;
        ActivityId: Serenity.IntegerEditor;
        CycleId: Serenity.IntegerEditor;
        IncomeFlowId: Serenity.IntegerEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.StringEditor;
        Factor: Serenity.StringEditor;
        DelayedCost: Serenity.StringEditor;
        YearCost: Serenity.StringEditor;
        AccessibleCost: Serenity.StringEditor;
        InaccessibleCost: Serenity.StringEditor;
        TotalLeakage: Serenity.StringEditor;
        RecoverableLeakage: Serenity.StringEditor;
        Recovered: Serenity.StringEditor;
        DelayedCostHistory: Serenity.StringEditor;
        YearCostHistory: Serenity.StringEditor;
        AccessibleCostHistory: Serenity.StringEditor;
        InaccessibleCostHistory: Serenity.StringEditor;
        RejectCount: Serenity.IntegerEditor;
        EventDescription: Serenity.StringEditor;
        MainReason: Serenity.StringEditor;
        CycleName: Serenity.StringEditor;
        Name: Serenity.StringEditor;
        Expr1: Serenity.StringEditor;
        CodeName: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestDetailsInfoRow {
        Id?: number;
        ProvinceId?: number;
        ActivityId?: number;
        CycleId?: number;
        IncomeFlowId?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        DelayedCostHistory?: number;
        YearCostHistory?: number;
        AccessibleCostHistory?: number;
        InaccessibleCostHistory?: number;
        RejectCount?: number;
        EventDescription?: string;
        MainReason?: string;
        CycleName?: string;
        Name?: string;
        Expr1?: string;
        CodeName?: string;
        DiscoverLeakDate?: string;
    }
    namespace ActivityRequestDetailsInfoRow {
        const idProperty = "Id";
        const nameProperty = "EventDescription";
        const localTextPrefix = "Case.ActivityRequestDetailsInfo";
        namespace Fields {
            const Id: string;
            const ProvinceId: string;
            const ActivityId: string;
            const CycleId: string;
            const IncomeFlowId: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const DelayedCostHistory: string;
            const YearCostHistory: string;
            const AccessibleCostHistory: string;
            const InaccessibleCostHistory: string;
            const RejectCount: string;
            const EventDescription: string;
            const MainReason: string;
            const CycleName: string;
            const Name: string;
            const Expr1: string;
            const CodeName: string;
            const DiscoverLeakDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestDetailsInfoService {
        const baseUrl = "Case/ActivityRequestDetailsInfo";
        function Create(request: Serenity.SaveRequest<ActivityRequestDetailsInfoRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestDetailsInfoRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestDetailsInfoRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestDetailsInfoRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestFinancialForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestFinancialForm {
        ProvinceName: Serenity.StringEditor;
        Id: Serenity.StringEditor;
        ActivityId: Serenity.LookupEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        CycleCostHistory: Serenity.DecimalEditor;
        DelayedCostHistory: Serenity.DecimalEditor;
        AccessibleCostHistory: Serenity.DecimalEditor;
        InaccessibleCostHistory: Serenity.DecimalEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.DecimalEditor;
        Factor: Serenity.StringEditor;
        DelayedCost: Serenity.DecimalEditor;
        AccessibleCost: Serenity.DecimalEditor;
        InaccessibleCost: Serenity.DecimalEditor;
        YearCost: Serenity.DecimalEditor;
        TotalLeakage: Serenity.DecimalEditor;
        RecoverableLeakage: Serenity.DecimalEditor;
        Recovered: Serenity.DecimalEditor;
        RejectCount: Serenity.StringEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReason: Serenity.TextAreaEditor;
        CommnetList: ActivityRequestCommentEditor;
        File1: Serenity.ImageUploadEditor;
        File2: Serenity.ImageUploadEditor;
        ConfirmTypeID: Serenity.EnumEditor;
        ActionID: Serenity.EnumEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestFinancialRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        SendDate?: string;
        SendUserId?: number;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
        SendUserName?: string;
        ActionID?: RequestAction;
        ConfirmTypeID?: ConfirmType;
        IsRejected?: boolean;
        RejectCount?: number;
        CommentReasonList?: number[];
        CommnetList?: ActivityRequestCommentRow[];
        File1?: string;
        File2?: string;
        File3?: string;
        FinancialControllerConfirm?: boolean;
        CycleCostHistory?: number;
        DelayedCostHistory?: number;
        YearCostHistory?: number;
        AccessibleCostHistory?: number;
        InaccessibleCostHistory?: number;
        TotalLeakageHistory?: number;
        RecoverableLeakageHistory?: number;
        RecoveredHistory?: number;
    }
    namespace ActivityRequestFinancialRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ActivityRequestFinancial";
        namespace Fields {
            const Id: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const Financial: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const EventDescription: string;
            const MainReason: string;
            const CorrectionOperation: string;
            const AvoidRepeatingOperation: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const SendDate: string;
            const SendUserId: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const ActivityId: string;
            const ProvinceId: string;
            const CycleId: string;
            const CustomerEffectId: string;
            const IncomeFlowId: string;
            const RiskLevelId: string;
            const StatusID: string;
            const LeakDate: string;
            const DiscoverLeakDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityObjective: string;
            const ActivityGroupId: string;
            const ProvinceName: string;
            const CycleName: string;
            const CustomerEffectName: string;
            const IncomeFlowName: string;
            const RiskLevelName: string;
            const StatusName: string;
            const CreatedUserName: string;
            const ModifiedUserName: string;
            const DeletedUserName: string;
            const SendUserName: string;
            const ActionID: string;
            const ConfirmTypeID: string;
            const IsRejected: string;
            const RejectCount: string;
            const CommentReasonList: string;
            const CommnetList: string;
            const File1: string;
            const File2: string;
            const File3: string;
            const FinancialControllerConfirm: string;
            const CycleCostHistory: string;
            const DelayedCostHistory: string;
            const YearCostHistory: string;
            const AccessibleCostHistory: string;
            const InaccessibleCostHistory: string;
            const TotalLeakageHistory: string;
            const RecoverableLeakageHistory: string;
            const RecoveredHistory: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestFinancialService {
        const baseUrl = "Case/ActivityRequestFinancial";
        function Create(request: Serenity.SaveRequest<ActivityRequestFinancialRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestFinancialRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestFinancialRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestFinancialRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestForm {
        ActivityId: Serenity.LookupEditor;
        ProvinceName: Serenity.StringEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.DecimalEditor;
        Factor: Serenity.StringEditor;
        DelayedCost: Serenity.DecimalEditor;
        AccessibleCost: Serenity.DecimalEditor;
        InaccessibleCost: Serenity.DecimalEditor;
        YearCost: Serenity.DecimalEditor;
        TotalLeakage: Serenity.DecimalEditor;
        RecoverableLeakage: Serenity.DecimalEditor;
        Recovered: Serenity.DecimalEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReason: Serenity.TextAreaEditor;
        CommnetList: ActivityRequestCommentEditor;
        File1: Serenity.ImageUploadEditor;
        File2: Serenity.ImageUploadEditor;
        ConfirmTypeID: Serenity.EnumEditor;
        ActionID: Serenity.EnumEditor;
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestHistoryForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestHistoryForm {
        CycleCostHistory: Serenity.DecimalEditor;
        DelayedCostHistory: Serenity.DecimalEditor;
        YearCostHistory: Serenity.DecimalEditor;
        AccessibleCostHistory: Serenity.DecimalEditor;
        InaccessibleCostHistory: Serenity.DecimalEditor;
        TotalLeakageHistory: Serenity.DecimalEditor;
        RecoverableLeakageHistory: Serenity.DecimalEditor;
        RecoveredHistory: Serenity.DecimalEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestHistoryRow {
        Id?: number;
        CycleCostHistory?: number;
        DelayedCostHistory?: number;
        YearCostHistory?: number;
        AccessibleCostHistory?: number;
        InaccessibleCostHistory?: number;
        TotalLeakageHistory?: number;
        RecoverableLeakageHistory?: number;
        RecoveredHistory?: number;
    }
    namespace ActivityRequestHistoryRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ActivityRequestHistory";
        const lookupKey = "Case.ActivityRequestHistory";
        function getLookup(): Q.Lookup<ActivityRequestHistoryRow>;
        namespace Fields {
            const Id: string;
            const CycleCostHistory: string;
            const DelayedCostHistory: string;
            const YearCostHistory: string;
            const AccessibleCostHistory: string;
            const InaccessibleCostHistory: string;
            const TotalLeakageHistory: string;
            const RecoverableLeakageHistory: string;
            const RecoveredHistory: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestHistoryService {
        const baseUrl = "Case/ActivityRequestHistory";
        function Create(request: Serenity.SaveRequest<ActivityRequestHistoryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestHistoryRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestHistoryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestHistoryRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestLeaderForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestLeaderForm {
        ProvinceName: Serenity.StringEditor;
        Id: Serenity.StringEditor;
        ActivityId: Serenity.LookupEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.DecimalEditor;
        Factor: Serenity.StringEditor;
        DelayedCost: Serenity.DecimalEditor;
        AccessibleCost: Serenity.DecimalEditor;
        InaccessibleCost: Serenity.DecimalEditor;
        YearCost: Serenity.DecimalEditor;
        TotalLeakage: Serenity.DecimalEditor;
        RecoverableLeakage: Serenity.DecimalEditor;
        Recovered: Serenity.DecimalEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReason: Serenity.TextAreaEditor;
        CommnetList: ActivityRequestCommentEditor;
        File1: Serenity.ImageUploadEditor;
        File2: Serenity.ImageUploadEditor;
        ConfirmTypeID: Serenity.EnumEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestLeaderRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        SendDate?: string;
        SendUserId?: number;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
        SendUserName?: string;
        ActionID?: RequestAction;
        ConfirmTypeID?: ConfirmType;
        IsRejected?: boolean;
        RejectCount?: number;
        CommentReasonList?: number[];
        CommnetList?: ActivityRequestCommentRow[];
        File1?: string;
        File2?: string;
        File3?: string;
        FinancialControllerConfirm?: boolean;
    }
    namespace ActivityRequestLeaderRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ActivityRequestLeader";
        namespace Fields {
            const Id: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const Financial: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const EventDescription: string;
            const MainReason: string;
            const CorrectionOperation: string;
            const AvoidRepeatingOperation: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const SendDate: string;
            const SendUserId: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const ActivityId: string;
            const ProvinceId: string;
            const CycleId: string;
            const CustomerEffectId: string;
            const IncomeFlowId: string;
            const RiskLevelId: string;
            const StatusID: string;
            const LeakDate: string;
            const DiscoverLeakDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityObjective: string;
            const ActivityGroupId: string;
            const ProvinceName: string;
            const CycleName: string;
            const CustomerEffectName: string;
            const IncomeFlowName: string;
            const RiskLevelName: string;
            const StatusName: string;
            const CreatedUserName: string;
            const ModifiedUserName: string;
            const DeletedUserName: string;
            const SendUserName: string;
            const ActionID: string;
            const ConfirmTypeID: string;
            const IsRejected: string;
            const RejectCount: string;
            const CommentReasonList: string;
            const CommnetList: string;
            const File1: string;
            const File2: string;
            const File3: string;
            const FinancialControllerConfirm: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestLeaderService {
        const baseUrl = "Case/ActivityRequestLeader";
        function Create(request: Serenity.SaveRequest<ActivityRequestLeaderRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestLeaderRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestLeaderRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestLeaderRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestListRequest extends Serenity.ListRequest {
        ActivityRequests?: number[];
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestLogForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestLogForm {
        StatusId: Serenity.IntegerEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestLogRow {
        Id?: number;
        ActivityRequestId?: number;
        StatusId?: number;
        ActionID?: RequestAction;
        UserId?: number;
        InsertDate?: string;
        StatusName?: string;
        UserDisplayName?: string;
    }
    namespace ActivityRequestLogRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ActivityRequestLog";
        namespace Fields {
            const Id: string;
            const ActivityRequestId: string;
            const StatusId: string;
            const ActionID: string;
            const UserId: string;
            const InsertDate: string;
            const StatusName: string;
            const UserDisplayName: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestLogService {
        const baseUrl = "Case/ActivityRequestLog";
        function Create(request: Serenity.SaveRequest<ActivityRequestLogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestLogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestLogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestLogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestPenddingForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestPenddingForm {
        ProvinceName: Serenity.StringEditor;
        Id: Serenity.StringEditor;
        ActivityId: Serenity.LookupEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        CycleCostHistory: Serenity.DecimalEditor;
        DelayedCostHistory: Serenity.DecimalEditor;
        AccessibleCostHistory: Serenity.DecimalEditor;
        InaccessibleCostHistory: Serenity.DecimalEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.DecimalEditor;
        Factor: Serenity.DecimalEditor;
        DelayedCost: Serenity.DecimalEditor;
        AccessibleCost: Serenity.DecimalEditor;
        InaccessibleCost: Serenity.DecimalEditor;
        YearCost: Serenity.DecimalEditor;
        TotalLeakage: Serenity.DecimalEditor;
        RecoverableLeakage: Serenity.DecimalEditor;
        Recovered: Serenity.DecimalEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReason: Serenity.TextAreaEditor;
        CommnetList: ActivityRequestCommentEditor;
        File1: Serenity.ImageUploadEditor;
        File2: Serenity.ImageUploadEditor;
        ConfirmTypeID: Serenity.EnumEditor;
        ActionID: Serenity.EnumEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestPenddingRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        SendDate?: string;
        SendUserId?: number;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
        SendUserName?: string;
        ActionID?: RequestAction;
        ConfirmTypeID?: ConfirmType;
        IsRejected?: boolean;
        CommentReasonList?: number[];
        CommnetList?: ActivityRequestCommentRow[];
        File1?: string;
        File2?: string;
        File3?: string;
        FinancialControllerConfirm?: boolean;
        CycleCostHistory?: number;
        DelayedCostHistory?: number;
        YearCostHistory?: number;
        AccessibleCostHistory?: number;
        InaccessibleCostHistory?: number;
        TotalLeakageHistory?: number;
        RecoverableLeakageHistory?: number;
        RecoveredHistory?: number;
    }
    namespace ActivityRequestPenddingRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ActivityRequestPendding";
        const lookupKey = "Case.ActivityRequestPenddingRow";
        function getLookup(): Q.Lookup<ActivityRequestPenddingRow>;
        namespace Fields {
            const Id: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const Financial: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const EventDescription: string;
            const MainReason: string;
            const CorrectionOperation: string;
            const AvoidRepeatingOperation: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const SendDate: string;
            const SendUserId: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const ActivityId: string;
            const ProvinceId: string;
            const CycleId: string;
            const CustomerEffectId: string;
            const IncomeFlowId: string;
            const RiskLevelId: string;
            const StatusID: string;
            const LeakDate: string;
            const DiscoverLeakDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityObjective: string;
            const ActivityGroupId: string;
            const ProvinceName: string;
            const CycleName: string;
            const CustomerEffectName: string;
            const IncomeFlowName: string;
            const RiskLevelName: string;
            const StatusName: string;
            const CreatedUserName: string;
            const ModifiedUserName: string;
            const DeletedUserName: string;
            const SendUserName: string;
            const ActionID: string;
            const ConfirmTypeID: string;
            const IsRejected: string;
            const CommentReasonList: string;
            const CommnetList: string;
            const File1: string;
            const File2: string;
            const File3: string;
            const FinancialControllerConfirm: string;
            const CycleCostHistory: string;
            const DelayedCostHistory: string;
            const YearCostHistory: string;
            const AccessibleCostHistory: string;
            const InaccessibleCostHistory: string;
            const TotalLeakageHistory: string;
            const RecoverableLeakageHistory: string;
            const RecoveredHistory: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestPenddingService {
        const baseUrl = "Case/ActivityRequestPendding";
        function Create(request: Serenity.SaveRequest<ActivityRequestPenddingRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestPenddingRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestPenddingRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestPenddingRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestRequest extends Serenity.ServiceRequest {
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestResponse extends Serenity.ServiceResponse {
        Values?: {
            [key: string]: any;
        }[];
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        SendDate?: string;
        SendUserId?: number;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
        SendUserName?: string;
        ActionID?: RequestAction;
        ConfirmTypeID?: ConfirmType;
        IsRejected?: boolean;
        RejectCount?: number;
        CommentReasonList?: number[];
        CommnetList?: ActivityRequestCommentRow[];
        File1?: string;
        File2?: string;
        File3?: string;
        FinancialControllerConfirm?: boolean;
    }
    namespace ActivityRequestRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ActivityRequest";
        namespace Fields {
            const Id: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const Financial: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const EventDescription: string;
            const MainReason: string;
            const CorrectionOperation: string;
            const AvoidRepeatingOperation: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const SendDate: string;
            const SendUserId: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const ActivityId: string;
            const ProvinceId: string;
            const CycleId: string;
            const CustomerEffectId: string;
            const IncomeFlowId: string;
            const RiskLevelId: string;
            const StatusID: string;
            const LeakDate: string;
            const DiscoverLeakDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityObjective: string;
            const ActivityGroupId: string;
            const ProvinceName: string;
            const CycleName: string;
            const CustomerEffectName: string;
            const IncomeFlowName: string;
            const RiskLevelName: string;
            const StatusName: string;
            const CreatedUserName: string;
            const ModifiedUserName: string;
            const DeletedUserName: string;
            const SendUserName: string;
            const ActionID: string;
            const ConfirmTypeID: string;
            const IsRejected: string;
            const RejectCount: string;
            const CommentReasonList: string;
            const CommnetList: string;
            const File1: string;
            const File2: string;
            const File3: string;
            const FinancialControllerConfirm: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestService {
        const baseUrl = "Case/ActivityRequest";
        function Create(request: Serenity.SaveRequest<ActivityRequestRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: ActivityRequestListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ActivityRequestTechnicalForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestTechnicalForm {
        ProvinceName: Serenity.StringEditor;
        Id: Serenity.StringEditor;
        ActivityId: Serenity.LookupEditor;
        CycleId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.DecimalEditor;
        Factor: Serenity.StringEditor;
        DelayedCost: Serenity.DecimalEditor;
        AccessibleCost: Serenity.DecimalEditor;
        InaccessibleCost: Serenity.DecimalEditor;
        YearCost: Serenity.DecimalEditor;
        TotalLeakage: Serenity.DecimalEditor;
        RecoverableLeakage: Serenity.DecimalEditor;
        Recovered: Serenity.DecimalEditor;
        RejectCount: Serenity.StringEditor;
        EventDescription: Serenity.TextAreaEditor;
        MainReason: Serenity.TextAreaEditor;
        CommnetList: ActivityRequestCommentEditor;
        File1: Serenity.ImageUploadEditor;
        File2: Serenity.ImageUploadEditor;
        ConfirmTypeID: Serenity.EnumEditor;
        ActionID: Serenity.EnumEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRequestTechnicalRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        SendDate?: string;
        SendUserId?: number;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
        SendUserName?: string;
        ActionID?: RequestAction;
        ConfirmTypeID?: ConfirmType;
        IsRejected?: boolean;
        RejectCount?: number;
        CommentReasonList?: number[];
        CommnetList?: ActivityRequestCommentRow[];
        File1?: string;
        File2?: string;
        File3?: string;
        FinancialControllerConfirm?: boolean;
        CycleCostHistory?: number;
        DelayedCostHistory?: number;
        YearCostHistory?: number;
        AccessibleCostHistory?: number;
        InaccessibleCostHistory?: number;
        TotalLeakageHistory?: number;
        RecoverableLeakageHistory?: number;
        RecoveredHistory?: number;
    }
    namespace ActivityRequestTechnicalRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ActivityRequestTechnical";
        namespace Fields {
            const Id: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const Financial: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const EventDescription: string;
            const MainReason: string;
            const CorrectionOperation: string;
            const AvoidRepeatingOperation: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const SendDate: string;
            const SendUserId: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const ActivityId: string;
            const ProvinceId: string;
            const CycleId: string;
            const CustomerEffectId: string;
            const IncomeFlowId: string;
            const RiskLevelId: string;
            const StatusID: string;
            const LeakDate: string;
            const DiscoverLeakDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityObjective: string;
            const ActivityGroupId: string;
            const ProvinceName: string;
            const CycleName: string;
            const CustomerEffectName: string;
            const IncomeFlowName: string;
            const RiskLevelName: string;
            const StatusName: string;
            const CreatedUserName: string;
            const ModifiedUserName: string;
            const DeletedUserName: string;
            const SendUserName: string;
            const ActionID: string;
            const ConfirmTypeID: string;
            const IsRejected: string;
            const RejectCount: string;
            const CommentReasonList: string;
            const CommnetList: string;
            const File1: string;
            const File2: string;
            const File3: string;
            const FinancialControllerConfirm: string;
            const CycleCostHistory: string;
            const DelayedCostHistory: string;
            const YearCostHistory: string;
            const AccessibleCostHistory: string;
            const InaccessibleCostHistory: string;
            const TotalLeakageHistory: string;
            const RecoverableLeakageHistory: string;
            const RecoveredHistory: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityRequestTechnicalService {
        const baseUrl = "Case/ActivityRequestTechnical";
        function Create(request: Serenity.SaveRequest<ActivityRequestTechnicalRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestTechnicalRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestTechnicalRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestTechnicalRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    interface ActivityResponse extends Serenity.ServiceResponse {
        Values?: {
            [key: string]: any;
        }[];
    }
}
declare namespace CaseManagement.Case {
    interface ActivityRow {
        Id?: number;
        Code?: string;
        Name?: string;
        CodeName?: string;
        EnglishName?: string;
        Objective?: string;
        EnglishObjective?: string;
        GroupId?: number;
        RepeatTermId?: number;
        RequiredYearRepeatCount?: number;
        GroupName?: string;
        RepeatTermName?: string;
        KeyCheckArea?: string;
        DataSource?: string;
        Methodology?: string;
        KeyFocus?: string;
        Action?: string;
        KPI?: string;
        EventDescription?: string;
        MainReasonList?: ActivityMainReasonRow[];
        CorrectionOperationList?: ActivityCorrectionOperationRow[];
        Factor?: number;
    }
    namespace ActivityRow {
        const idProperty = "Id";
        const nameProperty = "CodeName";
        const localTextPrefix = "Case.Activity";
        const lookupKey = "Case.Activity";
        function getLookup(): Q.Lookup<ActivityRow>;
        namespace Fields {
            const Id: string;
            const Code: string;
            const Name: string;
            const CodeName: string;
            const EnglishName: string;
            const Objective: string;
            const EnglishObjective: string;
            const GroupId: string;
            const RepeatTermId: string;
            const RequiredYearRepeatCount: string;
            const GroupName: string;
            const RepeatTermName: string;
            const KeyCheckArea: string;
            const DataSource: string;
            const Methodology: string;
            const KeyFocus: string;
            const Action: string;
            const KPI: string;
            const EventDescription: string;
            const MainReasonList: string;
            const CorrectionOperationList: string;
            const Factor: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ActivityService {
        const baseUrl = "Case/Activity";
        function Create(request: Serenity.SaveRequest<ActivityRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ActivitybyGroupList(request: ActivityRequest, onSuccess?: (response: ActivityResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
            const ActivitybyGroupList: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class CommentReasonForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface CommentReasonForm {
        Comment: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Case {
    interface CommentReasonRow {
        Id?: number;
        Comment?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
    }
    namespace CommentReasonRow {
        const idProperty = "Id";
        const nameProperty = "Comment";
        const localTextPrefix = "Case.CommentReason";
        const lookupKey = "Case.CommentReason";
        function getLookup(): Q.Lookup<CommentReasonRow>;
        namespace Fields {
            const Id: string;
            const Comment: string;
            const CreatedUserId: string;
            const CreatedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace CommentReasonService {
        const baseUrl = "Case/CommentReason";
        function Create(request: Serenity.SaveRequest<CommentReasonRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<CommentReasonRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CommentReasonRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CommentReasonRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class CompanyForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface CompanyForm {
        Name: Serenity.StringEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        ModifiedUserId: Serenity.IntegerEditor;
        ModifiedDate: Serenity.DateEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.IntegerEditor;
        DeletedDate: Serenity.DateEditor;
    }
}
declare namespace CaseManagement.Case {
    interface CompanyRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace CompanyRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.Company";
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace CompanyService {
        const baseUrl = "Case/Company";
        function Create(request: Serenity.SaveRequest<CompanyRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<CompanyRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CompanyRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CompanyRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    enum ConfirmType {
        Technical = 1,
        Financial = 2,
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class CustomerEffectForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface CustomerEffectForm {
        Name: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Case {
    interface CustomerEffectRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace CustomerEffectRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.CustomerEffect";
        const lookupKey = "Case.CustomerEffect";
        function getLookup(): Q.Lookup<CustomerEffectRow>;
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace CustomerEffectService {
        const baseUrl = "Case/CustomerEffect";
        function Create(request: Serenity.SaveRequest<CustomerEffectRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<CustomerEffectRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CustomerEffectRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CustomerEffectRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class CycleForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface CycleForm {
        YearId: Serenity.LookupEditor;
        Cycle: Serenity.IntegerEditor;
        IsEnabled: Serenity.BooleanEditor;
    }
}
declare namespace CaseManagement.Case {
    interface CycleRow {
        Id?: number;
        YearId?: number;
        Cycle?: number;
        CycleName?: string;
        IsEnabled?: boolean;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        Year?: string;
    }
    namespace CycleRow {
        const idProperty = "Id";
        const nameProperty = "CycleName";
        const localTextPrefix = "Case.Cycle";
        const lookupKey = "Case.Cycle";
        function getLookup(): Q.Lookup<CycleRow>;
        namespace Fields {
            const Id: string;
            const YearId: string;
            const Cycle: string;
            const CycleName: string;
            const IsEnabled: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const Year: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace CycleService {
        const baseUrl = "Case/Cycle";
        function Create(request: Serenity.SaveRequest<CycleRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<CycleRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<CycleRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<CycleRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class IncomeFlowForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface IncomeFlowForm {
        Name: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Case {
    interface IncomeFlowRow {
        Id?: number;
        Name?: string;
        Code?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace IncomeFlowRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.IncomeFlow";
        const lookupKey = "Case.IncomeFlow";
        function getLookup(): Q.Lookup<IncomeFlowRow>;
        namespace Fields {
            const Id: string;
            const Name: string;
            const Code: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace IncomeFlowService {
        const baseUrl = "Case/IncomeFlow";
        function Create(request: Serenity.SaveRequest<IncomeFlowRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<IncomeFlowRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<IncomeFlowRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<IncomeFlowRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class PmoLevelForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface PmoLevelForm {
        Name: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Case {
    interface PmoLevelRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace PmoLevelRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.PmoLevel";
        const lookupKey = "Case.PmoLevel";
        function getLookup(): Q.Lookup<PmoLevelRow>;
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace PmoLevelService {
        const baseUrl = "Case/PmoLevel";
        function Create(request: Serenity.SaveRequest<PmoLevelRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<PmoLevelRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<PmoLevelRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<PmoLevelRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ProvinceCompanySoftwareForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ProvinceCompanySoftwareForm {
        ProvinveId: Serenity.IntegerEditor;
        CompanyId: Serenity.IntegerEditor;
        SoftwareId: Serenity.IntegerEditor;
        StatusId: Serenity.EnumEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        ModifiedUserId: Serenity.IntegerEditor;
        ModifiedDate: Serenity.DateEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.IntegerEditor;
        DeletedDate: Serenity.DateEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ProvinceCompanySoftwareRow {
        Id?: number;
        ProvinveId?: number;
        CompanyId?: number;
        SoftwareId?: number;
        StatusID?: SoftwareStatus;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        ProvinveName?: string;
        CompanyName?: string;
        SoftwareName?: string;
    }
    namespace ProvinceCompanySoftwareRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ProvinceCompanySoftware";
        namespace Fields {
            const Id: string;
            const ProvinveId: string;
            const CompanyId: string;
            const SoftwareId: string;
            const StatusID: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const ProvinveName: string;
            const CompanyName: string;
            const SoftwareName: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ProvinceCompanySoftwareService {
        const baseUrl = "Case/ProvinceCompanySoftware";
        function Create(request: Serenity.SaveRequest<ProvinceCompanySoftwareRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ProvinceCompanySoftwareRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ProvinceCompanySoftwareRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ProvinceCompanySoftwareRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    class ProvinceForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ProvinceForm {
        Name: Serenity.TextAreaEditor;
        ManagerName: Serenity.StringEditor;
        Code: Serenity.IntegerEditor;
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ProvinceProgramForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ProvinceProgramForm {
        Program: Serenity.StringEditor;
        YearId: Serenity.LookupEditor;
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class ProvinceProgramLogForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ProvinceProgramLogForm {
        ProvinceId: Serenity.IntegerEditor;
        YearId: Serenity.IntegerEditor;
        OldTotalLeakage: Serenity.StringEditor;
        NewTotalLeakage: Serenity.StringEditor;
        OldRecoverableLeakage: Serenity.StringEditor;
        NewRecoverableLeakage: Serenity.StringEditor;
        OldRecovered: Serenity.StringEditor;
        NewRecovered: Serenity.StringEditor;
        UserId: Serenity.IntegerEditor;
        InsertDate: Serenity.DateEditor;
    }
}
declare namespace CaseManagement.Case {
    interface ProvinceProgramLogRow {
        Id?: number;
        ActivityRequestID?: number;
        ProvinceId?: number;
        YearId?: number;
        OldTotalLeakage?: number;
        NewTotalLeakage?: number;
        OldRecoverableLeakage?: number;
        NewRecoverableLeakage?: number;
        OldRecovered?: number;
        NewRecovered?: number;
        UserId?: number;
        InsertDate?: string;
    }
    namespace ProvinceProgramLogRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ProvinceProgramLog";
        namespace Fields {
            const Id: string;
            const ActivityRequestID: string;
            const ProvinceId: string;
            const YearId: string;
            const OldTotalLeakage: string;
            const NewTotalLeakage: string;
            const OldRecoverableLeakage: string;
            const NewRecoverableLeakage: string;
            const OldRecovered: string;
            const NewRecovered: string;
            const UserId: string;
            const InsertDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ProvinceProgramLogService {
        const baseUrl = "Case/ProvinceProgramLog";
        function Create(request: Serenity.SaveRequest<ProvinceProgramLogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ProvinceProgramLogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ProvinceProgramLogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ProvinceProgramLogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    interface ProvinceProgramRequest extends Serenity.ServiceRequest {
    }
}
declare namespace CaseManagement.Case {
    interface ProvinceProgramResponse extends Serenity.ServiceResponse {
        Values?: {
            [key: string]: any;
        }[];
        ProvinceKey?: string[];
        Keys?: string[];
        Labels?: string;
    }
}
declare namespace CaseManagement.Case {
    interface ProvinceProgramRow {
        Id?: number;
        Program?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        PercentTotalLeakage?: string;
        PercentRecoverableLeakage?: string;
        PercentRecovered?: string;
        PercentRecoveredonTotal?: string;
        PercentTotal94to95?: string;
        PercentRecovered94to95?: string;
        ActivityCount?: number;
        ActivityNonRepeatCount?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        LastActivityDate?: string;
        ProvinceId?: number;
        YearId?: number;
        ProvinceName?: string;
        Year?: string;
    }
    namespace ProvinceProgramRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.ProvinceProgram";
        namespace Fields {
            const Id: string;
            const Program: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const PercentTotalLeakage: string;
            const PercentRecoverableLeakage: string;
            const PercentRecovered: string;
            const PercentRecoveredonTotal: string;
            const PercentTotal94to95: string;
            const PercentRecovered94to95: string;
            const ActivityCount: string;
            const ActivityNonRepeatCount: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const LastActivityDate: string;
            const ProvinceId: string;
            const YearId: string;
            const ProvinceName: string;
            const Year: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ProvinceProgramService {
        const baseUrl = "Case/ProvinceProgram";
        function Create(request: Serenity.SaveRequest<ProvinceProgramRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ProvinceProgramRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ProvinceProgramRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ProvinceProgramRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ProvinceProgramLineReport96(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ProvinceProgramLineReport(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ProvinceProgramLineReport94(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ProvinceProgramLineReport93(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ProvinceProgramLineReport92(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function LeakProgramReport95(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function ConfirmProgramReport95(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function LeakConfirmReport95(request: ProvinceProgramRequest, onSuccess?: (response: ProvinceProgramResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
            const ProvinceProgramLineReport96: string;
            const ProvinceProgramLineReport: string;
            const ProvinceProgramLineReport94: string;
            const ProvinceProgramLineReport93: string;
            const ProvinceProgramLineReport92: string;
            const LeakProgramReport95: string;
            const ConfirmProgramReport95: string;
            const LeakConfirmReport95: string;
        }
    }
}
declare namespace CaseManagement.Case {
    interface ProvinceRow {
        Id?: number;
        LeaderID?: number;
        Name?: string;
        Code?: number;
        EnglishName?: string;
        ManagerName?: string;
        LetterNo?: string;
        PmoLevelId?: number;
        InstallDate?: string;
        PmoLevelName?: string;
        LeaderName?: string;
    }
    namespace ProvinceRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.Province";
        const lookupKey = "Case.Province";
        function getLookup(): Q.Lookup<ProvinceRow>;
        namespace Fields {
            const Id: string;
            const LeaderID: string;
            const Name: string;
            const Code: string;
            const EnglishName: string;
            const ManagerName: string;
            const LetterNo: string;
            const PmoLevelId: string;
            const InstallDate: string;
            const PmoLevelName: string;
            const LeaderName: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace ProvinceService {
        const baseUrl = "Case/Province";
        function Create(request: Serenity.SaveRequest<ProvinceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ProvinceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ProvinceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ProvinceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class RepeatTermForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface RepeatTermForm {
        Name: Serenity.StringEditor;
        RequiredYearRepeatCount: Serenity.IntegerEditor;
    }
}
declare namespace CaseManagement.Case {
    interface RepeatTermRow {
        Id?: number;
        Name?: string;
        DayValue?: number;
        RequiredYearRepeatCount?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace RepeatTermRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.RepeatTerm";
        const lookupKey = "Case.RepeatTerm";
        function getLookup(): Q.Lookup<RepeatTermRow>;
        namespace Fields {
            const Id: string;
            const Name: string;
            const DayValue: string;
            const RequiredYearRepeatCount: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace RepeatTermService {
        const baseUrl = "Case/RepeatTerm";
        function Create(request: Serenity.SaveRequest<RepeatTermRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<RepeatTermRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<RepeatTermRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<RepeatTermRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    enum RequestAction {
        Save = 1,
        Forward = 2,
        Deny = 3,
        Delete = 4,
    }
}
declare namespace CaseManagement.Case {
    enum RequestActionAdmin {
        Deny = 3,
        Delete = 4,
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class RiskLevelForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface RiskLevelForm {
        Name: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Case {
    interface RiskLevelRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace RiskLevelRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.RiskLevel";
        const lookupKey = "Case.RiskLevel";
        function getLookup(): Q.Lookup<RiskLevelRow>;
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace RiskLevelService {
        const baseUrl = "Case/RiskLevel";
        function Create(request: Serenity.SaveRequest<RiskLevelRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<RiskLevelRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<RiskLevelRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<RiskLevelRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class SMSLogForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface SMSLogForm {
        ActivityRequestId: Serenity.StringEditor;
        ReceiverProvinceId: Serenity.LookupEditor;
        ReceiverUserName: Serenity.StringEditor;
        TextSent: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Case {
    interface SMSLogRow {
        Id?: number;
        Is_modified?: boolean;
        InsertDate?: string;
        ActivityRequestId?: number;
        MessageId?: number;
        SenderUserId?: number;
        SenderUserName?: string;
        ReceiverProvinceId?: number;
        ReceiverUserId?: number;
        ReceiverUserName?: string;
        MobileNumber?: string;
        IsSent?: boolean;
        IsDelivered?: boolean;
        ReceiverRoleId?: number;
        ReceiverProvinceLeaderId?: number;
        ReceiverProvinceName?: string;
        ReceiverProvinceCode?: number;
        ReceiverProvinceEnglishName?: string;
        ReceiverProvinceManagerName?: string;
        ReceiverProvinceLetterNo?: string;
        ReceiverProvincePmoLevelId?: number;
        ReceiverProvinceInstallDate?: string;
        ModifiedDate?: string;
        ReceiverRoleRoleName?: string;
    }
    namespace SMSLogRow {
        const idProperty = "Id";
        const nameProperty = "SenderUserName";
        const localTextPrefix = "Case.SMSLog";
        const lookupKey = "Case.SMSLogRow";
        function getLookup(): Q.Lookup<SMSLogRow>;
        namespace Fields {
            const Id: string;
            const Is_modified: string;
            const InsertDate: string;
            const ActivityRequestId: string;
            const MessageId: string;
            const SenderUserId: string;
            const SenderUserName: string;
            const ReceiverProvinceId: string;
            const ReceiverUserId: string;
            const ReceiverUserName: string;
            const MobileNumber: string;
            const IsSent: string;
            const IsDelivered: string;
            const ReceiverRoleId: string;
            const ReceiverProvinceLeaderId: string;
            const ReceiverProvinceName: string;
            const ReceiverProvinceCode: string;
            const ReceiverProvinceEnglishName: string;
            const ReceiverProvinceManagerName: string;
            const ReceiverProvinceLetterNo: string;
            const ReceiverProvincePmoLevelId: string;
            const ReceiverProvinceInstallDate: string;
            const ModifiedDate: string;
            const ReceiverRoleRoleName: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace SMSLogService {
        const baseUrl = "Case/SMSLog";
        function Create(request: Serenity.SaveRequest<SMSLogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<SMSLogRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SMSLogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SMSLogRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class SoftwareForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface SoftwareForm {
        Name: Serenity.StringEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        ModifiedUserId: Serenity.IntegerEditor;
        ModifiedDate: Serenity.DateEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.IntegerEditor;
        DeletedDate: Serenity.DateEditor;
    }
}
declare namespace CaseManagement.Case {
    interface SoftwareRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace SoftwareRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.Software";
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace SoftwareService {
        const baseUrl = "Case/Software";
        function Create(request: Serenity.SaveRequest<SoftwareRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<SoftwareRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SoftwareRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SoftwareRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    enum SoftwareStatus {
        Yes = 1,
        No = 2,
        Pendding = 3,
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class SwitchDslamForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface SwitchDslamForm {
        Name: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Case {
    class SwitchDslamProvinceForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface SwitchDslamProvinceForm {
        ProvinceId: Serenity.IntegerEditor;
        SwitchDslamid: Serenity.IntegerEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        ModifiedUserId: Serenity.IntegerEditor;
        ModifiedDate: Serenity.DateEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.IntegerEditor;
        DeletedDate: Serenity.DateEditor;
    }
}
declare namespace CaseManagement.Case {
    interface SwitchDslamProvinceRow {
        Id?: number;
        ProvinceId?: number;
        SwitchDslamid?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        ProvinceName?: string;
        SwitchDslamidName?: string;
    }
    namespace SwitchDslamProvinceRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.SwitchDslamProvince";
        namespace Fields {
            const Id: string;
            const ProvinceId: string;
            const SwitchDslamid: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const ProvinceName: string;
            const SwitchDslamidName: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace SwitchDslamProvinceService {
        const baseUrl = "Case/SwitchDslamProvince";
        function Create(request: Serenity.SaveRequest<SwitchDslamProvinceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<SwitchDslamProvinceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SwitchDslamProvinceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SwitchDslamProvinceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    interface SwitchDslamRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace SwitchDslamRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.SwitchDslam";
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace SwitchDslamService {
        const baseUrl = "Case/SwitchDslam";
        function Create(request: Serenity.SaveRequest<SwitchDslamRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<SwitchDslamRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SwitchDslamRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SwitchDslamRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    class SwitchForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface SwitchForm {
        Name: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Case {
    interface SwitchProvinceRow {
        Id?: number;
        ProvinceId?: number;
        SwitchId?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        ProvinceName?: string;
        SwitchName?: string;
    }
    namespace SwitchProvinceRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.SwitchProvince";
        namespace Fields {
            const Id: string;
            const ProvinceId: string;
            const SwitchId: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const ProvinceName: string;
            const SwitchName: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace SwitchProvinceService {
        const baseUrl = "Case/SwitchProvince";
        function Create(request: Serenity.SaveRequest<SwitchProvinceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<SwitchProvinceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SwitchProvinceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SwitchProvinceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    interface SwitchRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace SwitchRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.Switch";
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace SwitchService {
        const baseUrl = "Case/Switch";
        function Create(request: Serenity.SaveRequest<SwitchRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<SwitchRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SwitchRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SwitchRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class SwitchTransitForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface SwitchTransitForm {
        Name: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.Case {
    class SwitchTransitProvinceForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface SwitchTransitProvinceForm {
        ProvinceId: Serenity.LookupEditor;
        SwitchTransitId: Serenity.LookupEditor;
    }
}
declare namespace CaseManagement.Case {
    interface SwitchTransitProvinceRow {
        Id?: number;
        ProvinceId?: number;
        SwitchTransitId?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        ProvinceName?: string;
        SwitchTransitName?: string;
    }
    namespace SwitchTransitProvinceRow {
        const idProperty = "Id";
        const nameProperty = "ProvinceName";
        const localTextPrefix = "Case.SwitchTransitProvince";
        namespace Fields {
            const Id: string;
            const ProvinceId: string;
            const SwitchTransitId: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const ProvinceName: string;
            const SwitchTransitName: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace SwitchTransitProvinceService {
        const baseUrl = "Case/SwitchTransitProvince";
        function Create(request: Serenity.SaveRequest<SwitchTransitProvinceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<SwitchTransitProvinceRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SwitchTransitProvinceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SwitchTransitProvinceRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    interface SwitchTransitRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace SwitchTransitRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "Case.SwitchTransit";
        const lookupKey = "Case.SwitchTransit";
        function getLookup(): Q.Lookup<SwitchTransitRow>;
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace SwitchTransitService {
        const baseUrl = "Case/SwitchTransit";
        function Create(request: Serenity.SaveRequest<SwitchTransitRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<SwitchTransitRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SwitchTransitRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SwitchTransitRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Case {
    interface UserProvinceRow {
        Id?: number;
        UserId?: number;
        ProvinceId?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        UserUsername?: string;
        UserDisplayName?: string;
        UserEmail?: string;
        UserSource?: string;
        UserPasswordHash?: string;
        UserPasswordSalt?: string;
        UserInsertDate?: string;
        UserInsertUserId?: number;
        UserUpdateDate?: string;
        UserUpdateUserId?: number;
        UserIsActive?: number;
        UserLastDirectoryUpdate?: string;
        ProvinceName?: string;
        ProvinceEnglishName?: string;
        ProvinceManagerName?: string;
        ProvinceLetterNo?: string;
        ProvincePmoLevelId?: number;
        ProvinceInstallDate?: string;
        ProvinceCreatedUserId?: number;
        ProvinceCreatedDate?: string;
        ProvinceModifiedUserId?: number;
        ProvinceModifiedDate?: string;
        ProvinceIsDeleted?: boolean;
        ProvinceDeletedUserId?: number;
        ProvinceDeletedDate?: string;
    }
    namespace UserProvinceRow {
        const idProperty = "Id";
        const localTextPrefix = "Case.UserProvince";
        namespace Fields {
            const Id: string;
            const UserId: string;
            const ProvinceId: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const UserUsername: string;
            const UserDisplayName: string;
            const UserEmail: string;
            const UserSource: string;
            const UserPasswordHash: string;
            const UserPasswordSalt: string;
            const UserInsertDate: string;
            const UserInsertUserId: string;
            const UserUpdateDate: string;
            const UserUpdateUserId: string;
            const UserIsActive: string;
            const UserLastDirectoryUpdate: string;
            const ProvinceName: string;
            const ProvinceEnglishName: string;
            const ProvinceManagerName: string;
            const ProvinceLetterNo: string;
            const ProvincePmoLevelId: string;
            const ProvinceInstallDate: string;
            const ProvinceCreatedUserId: string;
            const ProvinceCreatedDate: string;
            const ProvinceModifiedUserId: string;
            const ProvinceModifiedDate: string;
            const ProvinceIsDeleted: string;
            const ProvinceDeletedUserId: string;
            const ProvinceDeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
}
declare namespace CaseManagement.Case {
    class YearForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface YearForm {
        Year: Serenity.IntegerEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        ModifiedUserId: Serenity.IntegerEditor;
        ModifiedDate: Serenity.DateEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.IntegerEditor;
        DeletedDate: Serenity.DateEditor;
    }
}
declare namespace CaseManagement.Case {
    interface YearRow {
        Id?: number;
        Year?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace YearRow {
        const idProperty = "Id";
        const nameProperty = "Year";
        const localTextPrefix = "Case.Year";
        const lookupKey = "Case.Year";
        function getLookup(): Q.Lookup<YearRow>;
        namespace Fields {
            const Id: string;
            const Year: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.Case {
    namespace YearService {
        const baseUrl = "Case/Year";
        function Create(request: Serenity.SaveRequest<YearRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<YearRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<YearRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<YearRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Common.Pages {
    interface UploadResponse extends Serenity.ServiceResponse {
        TemporaryFile?: string;
        Size?: number;
        IsImage?: boolean;
        Width?: number;
        Height?: number;
    }
}
declare namespace CaseManagement.Common {
    interface UserPreferenceRetrieveRequest extends Serenity.ServiceRequest {
        PreferenceType?: string;
        Name?: string;
    }
}
declare namespace CaseManagement.Common {
    interface UserPreferenceRetrieveResponse extends Serenity.ServiceResponse {
        Value?: string;
    }
}
declare namespace CaseManagement.Common {
    interface UserPreferenceRow {
        UserPreferenceId?: number;
        UserId?: number;
        PreferenceType?: string;
        Name?: string;
        Value?: string;
    }
    namespace UserPreferenceRow {
        const idProperty = "UserPreferenceId";
        const nameProperty = "Name";
        const localTextPrefix = "Common.UserPreference";
        namespace Fields {
            const UserPreferenceId: string;
            const UserId: string;
            const PreferenceType: string;
            const Name: string;
            const Value: string;
        }
    }
}
declare namespace CaseManagement.Common {
    namespace UserPreferenceService {
        const baseUrl = "Common/UserPreference";
        function Update(request: UserPreferenceUpdateRequest, onSuccess?: (response: Serenity.ServiceResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: UserPreferenceRetrieveRequest, onSuccess?: (response: UserPreferenceRetrieveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Update: string;
            const Retrieve: string;
        }
    }
}
declare namespace CaseManagement.Common {
    interface UserPreferenceUpdateRequest extends Serenity.ServiceRequest {
        PreferenceType?: string;
        Name?: string;
        Value?: string;
    }
}
declare namespace CaseManagement {
    interface ExcelImportRequest extends Serenity.ServiceRequest {
        FileName?: string;
    }
}
declare namespace CaseManagement {
    interface ExcelImportResponse extends Serenity.ServiceResponse {
        Inserted?: number;
        Updated?: number;
        ErrorList?: string[];
    }
}
declare namespace CaseManagement {
    interface GetNextNumberRequest extends Serenity.ServiceRequest {
        Prefix?: string;
        Length?: number;
    }
}
declare namespace CaseManagement {
    interface GetNextNumberResponse extends Serenity.ServiceResponse {
        Number?: number;
        Serial?: string;
    }
}
declare namespace CaseManagement.Membership {
    class ChangePasswordForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ChangePasswordForm {
        OldPassword: Serenity.PasswordEditor;
        NewPassword: Serenity.PasswordEditor;
        ConfirmPassword: Serenity.PasswordEditor;
    }
}
declare namespace CaseManagement.Membership {
    interface ChangePasswordRequest extends Serenity.ServiceRequest {
        OldPassword?: string;
        NewPassword?: string;
        ConfirmPassword?: string;
    }
}
declare namespace CaseManagement.Membership {
    class ForgotPasswordForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ForgotPasswordForm {
        Email: Serenity.EmailEditor;
    }
}
declare namespace CaseManagement.Membership {
    interface ForgotPasswordRequest extends Serenity.ServiceRequest {
        Email?: string;
    }
}
declare namespace CaseManagement.Membership {
    class LoginForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface LoginForm {
        Username: Serenity.StringEditor;
        Password: Serenity.PasswordEditor;
    }
}
declare namespace CaseManagement.Membership {
    interface LoginRequest extends Serenity.ServiceRequest {
        Username?: string;
        Password?: string;
    }
}
declare namespace CaseManagement.Membership {
    class ResetPasswordForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ResetPasswordForm {
        NewPassword: Serenity.PasswordEditor;
        ConfirmPassword: Serenity.PasswordEditor;
    }
}
declare namespace CaseManagement.Membership {
    interface ResetPasswordRequest extends Serenity.ServiceRequest {
        Token?: string;
        NewPassword?: string;
        ConfirmPassword?: string;
    }
}
declare namespace CaseManagement.Membership {
    class SignUpForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface SignUpForm {
        DisplayName: Serenity.StringEditor;
        Email: Serenity.EmailEditor;
        ConfirmEmail: Serenity.EmailEditor;
        Password: Serenity.PasswordEditor;
        ConfirmPassword: Serenity.PasswordEditor;
    }
}
declare namespace CaseManagement.Membership {
    interface SignUpRequest extends Serenity.ServiceRequest {
        DisplayName?: string;
        Email?: string;
        Password?: string;
    }
}
declare namespace CaseManagement.Messaging {
}
declare namespace CaseManagement.Messaging {
    class InboxForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface InboxForm {
        SenderDisplayName: Serenity.StringEditor;
        MessageSubject: Serenity.TextAreaEditor;
        MessageBody: Serenity.TextAreaEditor;
        MessageFile: Serenity.ImageUploadEditor;
    }
}
declare namespace CaseManagement.Messaging {
    interface InboxRow {
        Id?: number;
        MessageId?: number;
        RecieverId?: number;
        SenderId?: number;
        Seen?: boolean;
        SeenDate?: string;
        MessageSubject?: string;
        MessageBody?: string;
        MessageFile?: string;
        MessageInsertedDate?: string;
        RecieverDisplayName?: string;
        SenderDisplayName?: string;
    }
    namespace InboxRow {
        const idProperty = "Id";
        const localTextPrefix = "Messaging.Inbox";
        namespace Fields {
            const Id: string;
            const MessageId: string;
            const RecieverId: string;
            const SenderId: string;
            const Seen: string;
            const SeenDate: string;
            const MessageSubject: string;
            const MessageBody: string;
            const MessageFile: string;
            const MessageInsertedDate: string;
            const RecieverDisplayName: string;
            const SenderDisplayName: string;
        }
    }
}
declare namespace CaseManagement.Messaging {
    namespace InboxService {
        const baseUrl = "Messaging/Inbox";
        function Create(request: Serenity.SaveRequest<InboxRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<InboxRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<InboxRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<InboxRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Messaging {
    interface MessagesReceiversRow {
        Id?: number;
        MessageId?: number;
        RecieverId?: number;
        SenderId?: number;
        Seen?: boolean;
        SeenDate?: string;
        MessageSenderId?: number;
        MessageSubject?: string;
        MessageBody?: string;
        MessageFile?: string;
        MessageInsertedDate?: string;
        SenderDisplayName?: string;
        RecieverOldcaseId?: number;
        RecieverUsername?: string;
        RecieverDisplayName?: string;
        RecieverEmployeeId?: string;
        RecieverEmail?: string;
        RecieverRank?: string;
        RecieverSource?: string;
        RecieverPassword?: string;
        RecieverPasswordHash?: string;
        RecieverPasswordSalt?: string;
        RecieverLastLoginDate?: string;
        RecieverInsertDate?: string;
        RecieverInsertUserId?: number;
        RecieverUpdateDate?: string;
        RecieverUpdateUserId?: number;
        RecieverIsActive?: number;
        RecieverLastDirectoryUpdate?: string;
        RecieverRoleId?: number;
        RecieverTelephoneNo1?: string;
        RecieverTelephoneNo2?: string;
        RecieverMobileNo?: string;
        RecieverDegree?: string;
        RecieverProvinceId?: number;
        RecieverIsIranTci?: number;
        RecieverIsDeleted?: boolean;
        RecieverDeletedUserId?: number;
        RecieverDeletedDate?: string;
        RecieverImagePath?: string;
    }
    namespace MessagesReceiversRow {
        const idProperty = "Id";
        const localTextPrefix = "Messaging.MessagesReceivers";
        namespace Fields {
            const Id: string;
            const MessageId: string;
            const RecieverId: string;
            const SenderId: string;
            const Seen: string;
            const SeenDate: string;
            const MessageSenderId: string;
            const MessageSubject: string;
            const MessageBody: string;
            const MessageFile: string;
            const MessageInsertedDate: string;
            const SenderDisplayName: string;
            const RecieverOldcaseId: string;
            const RecieverUsername: string;
            const RecieverDisplayName: string;
            const RecieverEmployeeId: string;
            const RecieverEmail: string;
            const RecieverRank: string;
            const RecieverSource: string;
            const RecieverPassword: string;
            const RecieverPasswordHash: string;
            const RecieverPasswordSalt: string;
            const RecieverLastLoginDate: string;
            const RecieverInsertDate: string;
            const RecieverInsertUserId: string;
            const RecieverUpdateDate: string;
            const RecieverUpdateUserId: string;
            const RecieverIsActive: string;
            const RecieverLastDirectoryUpdate: string;
            const RecieverRoleId: string;
            const RecieverTelephoneNo1: string;
            const RecieverTelephoneNo2: string;
            const RecieverMobileNo: string;
            const RecieverDegree: string;
            const RecieverProvinceId: string;
            const RecieverIsIranTci: string;
            const RecieverIsDeleted: string;
            const RecieverDeletedUserId: string;
            const RecieverDeletedDate: string;
            const RecieverImagePath: string;
        }
    }
}
declare namespace CaseManagement.Messaging {
}
declare namespace CaseManagement.Messaging {
    class NewMessageForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface NewMessageForm {
        ReceiverList: Serenity.LookupEditor;
        Subject: Serenity.TextAreaEditor;
        Body: Serenity.TextAreaEditor;
        File: Serenity.ImageUploadEditor;
    }
}
declare namespace CaseManagement.Messaging {
    interface NewMessageRow {
        Id?: number;
        SenderId?: number;
        Subject?: string;
        Body?: string;
        File?: string;
        InsertedDate?: string;
        SenderDisplayName?: string;
        ReceiverList?: number[];
    }
    namespace NewMessageRow {
        const idProperty = "Id";
        const nameProperty = "Subject";
        const localTextPrefix = "Messaging.NewMessage";
        const lookupKey = "Messaging.NewMessage";
        function getLookup(): Q.Lookup<NewMessageRow>;
        namespace Fields {
            const Id: string;
            const SenderId: string;
            const Subject: string;
            const Body: string;
            const File: string;
            const InsertedDate: string;
            const SenderDisplayName: string;
            const ReceiverList: string;
        }
    }
}
declare namespace CaseManagement.Messaging {
    namespace NewMessageService {
        const baseUrl = "Messaging/NewMessage";
        function Create(request: Serenity.SaveRequest<NewMessageRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<NewMessageRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<NewMessageRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<NewMessageRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Messaging {
}
declare namespace CaseManagement.Messaging {
    class SentForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface SentForm {
        RecieverDisplayName: Serenity.StringEditor;
        MessageSubject: Serenity.TextAreaEditor;
        MessageBody: Serenity.TextAreaEditor;
        MessageFile: Serenity.ImageUploadEditor;
    }
}
declare namespace CaseManagement.Messaging {
    interface SentRow {
        Id?: number;
        MessageId?: number;
        RecieverId?: number;
        SenderId?: number;
        Seen?: boolean;
        SeenDate?: string;
        MessageSubject?: string;
        MessageBody?: string;
        MessageFile?: string;
        MessageInsertedDate?: string;
        RecieverDisplayName?: string;
        SenderDisplayName?: string;
    }
    namespace SentRow {
        const idProperty = "Id";
        const localTextPrefix = "Messaging.Sent";
        namespace Fields {
            const Id: string;
            const MessageId: string;
            const RecieverId: string;
            const SenderId: string;
            const Seen: string;
            const SeenDate: string;
            const MessageSubject: string;
            const MessageBody: string;
            const MessageFile: string;
            const MessageInsertedDate: string;
            const RecieverDisplayName: string;
            const SenderDisplayName: string;
        }
    }
}
declare namespace CaseManagement.Messaging {
    namespace SentService {
        const baseUrl = "Messaging/Sent";
        function Create(request: Serenity.SaveRequest<SentRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<SentRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<SentRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<SentRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.Messaging {
}
declare namespace CaseManagement.Messaging {
    interface VwMessagesRow {
        Id?: number;
        SenderId?: number;
        Seen?: boolean;
        SeenDate?: string;
        Subject?: string;
        Body?: string;
        InsertedDate?: string;
        RecieverId?: number;
        MessageId?: number;
        SenderName?: string;
        ReceiverName?: string;
    }
    namespace VwMessagesRow {
        const idProperty = "Id";
        const nameProperty = "Subject";
        const localTextPrefix = "Messaging.VwMessages";
        namespace Fields {
            const Id: string;
            const SenderId: string;
            const Seen: string;
            const SeenDate: string;
            const Subject: string;
            const Body: string;
            const InsertedDate: string;
            const RecieverId: string;
            const MessageId: string;
            const SenderName: string;
            const ReceiverName: string;
        }
    }
}
declare namespace CaseManagement.Messaging {
    namespace VwMessagesService {
        const baseUrl = "Messaging/VwMessages";
        function Create(request: Serenity.SaveRequest<VwMessagesRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<VwMessagesRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<VwMessagesRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<VwMessagesRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace ReportTemplateDB.Common {
}
declare namespace ReportTemplateDB.Common {
    interface ReportTemplateRow {
        Id?: number;
        Template?: number[];
        Title?: string;
    }
    namespace ReportTemplateRow {
        const idProperty = "Id";
        const nameProperty = "Title";
        const localTextPrefix = "Common.ReportTemplate";
        namespace Fields {
            const Id: string;
            const Template: string;
            const Title: string;
        }
    }
}
declare namespace ReportTemplateDB.Common {
    namespace ReportTemplateService {
        const baseUrl = "Common/ReportTemplate";
        function Create(request: Serenity.SaveRequest<ReportTemplateRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ReportTemplateRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ReportTemplateRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ReportTemplateRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement {
    interface ScriptUserDefinition {
        Username?: string;
        DisplayName?: string;
        Permissions?: {
            [key: string]: boolean;
        };
    }
}
declare namespace CaseManagement.StimulReport {
    class ActivityReportForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityReportForm {
        RequestId: Serenity.IntegerEditor;
        ProvinceId: Serenity.IntegerEditor;
        ActivityId: Serenity.IntegerEditor;
        DelayCost: Serenity.IntegerEditor;
        YearCost: Serenity.IntegerEditor;
        LeakCost: Serenity.IntegerEditor;
        ConfirmDelayCost: Serenity.IntegerEditor;
        ConfirmYearCost: Serenity.IntegerEditor;
        ConfirmCost: Serenity.IntegerEditor;
        ProgramCost: Serenity.IntegerEditor;
        Percent: Serenity.StringEditor;
        CreatedUserId: Serenity.IntegerEditor;
        CreatedDate: Serenity.DateEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        DiscoverLeakDateShamsi: Serenity.StringEditor;
        EndDate: Serenity.DateEditor;
        ConfirmUserId: Serenity.IntegerEditor;
        ActionId: Serenity.IntegerEditor;
    }
}
declare namespace CaseManagement.StimulReport {
    interface ActivityReportRow {
        Id?: number;
        RequestId?: number;
        ProvinceId?: number;
        ActivityId?: number;
        DelayCost?: number;
        YearCost?: number;
        LeakCost?: number;
        ConfirmDelayCost?: number;
        ConfirmYearCost?: number;
        ConfirmCost?: number;
        ProgramCost?: number;
        Percent?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        DiscoverLeakDate?: string;
        DiscoverLeakDateShamsi?: string;
        EndDate?: string;
        ConfirmUserId?: number;
        ActionId?: number;
        RequestId2?: number;
        RequestProvinceId?: number;
        RequestActivityId?: number;
        RequestCycleId?: number;
        RequestCustomerEffectId?: number;
        RequestOrganizationEffectId?: number;
        RequestRiskLevelId?: number;
        RequestIncomeFlowId?: number;
        RequestCount?: number;
        RequestCycleCost?: number;
        RequestFactor?: number;
        RequestDelayedCost?: number;
        RequestYearCost?: number;
        RequestAccessibleCost?: number;
        RequestInaccessibleCost?: number;
        RequestFinancial?: number;
        RequestLeakDate?: string;
        RequestDiscoverLeakDate?: string;
        RequestDiscoverLeakDateShamsi?: string;
        RequestEventDescription?: string;
        RequestMainReason?: string;
        RequestCorrectionOperation?: string;
        RequestAvoidRepeatingOperation?: string;
        RequestCreatedUserId?: number;
        RequestCreatedDate?: string;
        RequestCreatedDateShamsi?: string;
        RequestModifiedUserId?: number;
        RequestModifiedDate?: string;
        RequestIsDeleted?: boolean;
        RequestDeletedUserId?: number;
        RequestDeletedDate?: string;
        RequestEndDate?: string;
        RequestStatusId?: number;
        RequestActionId?: number;
        ProvinceName?: string;
        ProvinceEnglishName?: string;
        ProvinceManagerName?: string;
        ProvinceLetterNo?: string;
        ProvincePmoLevelId?: number;
        ProvinceInstallDate?: string;
        ProvinceCreatedUserId?: number;
        ProvinceCreatedDate?: string;
        ProvinceModifiedUserId?: number;
        ProvinceModifiedDate?: string;
        ProvinceIsDeleted?: boolean;
        ProvinceDeletedUserId?: number;
        ProvinceDeletedDate?: string;
        ActivityCode?: number;
        ActivityName?: string;
        ActivityEnglishName?: string;
        ActivityObjective?: string;
        ActivityEnglishObjective?: string;
        ActivityCreatedUserId?: number;
        ActivityCreatedDate?: string;
        ActivityModifiedUserId?: number;
        ActivityModifiedDate?: string;
        ActivityIsDeleted?: boolean;
        ActivityDeletedUserId?: number;
        ActivityDeletedDate?: string;
        ActivityGroupId?: number;
        ActivityRepeatTermId?: number;
        ActivityKeyCheckArea?: string;
        ActivityDataSource?: string;
        ActivityMethodology?: string;
        ActivityKeyFocus?: string;
        ActivityAction?: string;
        ActivityKpi?: string;
    }
    namespace ActivityReportRow {
        const idProperty = "Id";
        const nameProperty = "Percent";
        const localTextPrefix = "StimulReport.ActivityReport";
        namespace Fields {
            const Id: any;
            const RequestId: any;
            const ProvinceId: any;
            const ActivityId: any;
            const DelayCost: any;
            const YearCost: any;
            const LeakCost: any;
            const ConfirmDelayCost: any;
            const ConfirmYearCost: any;
            const ConfirmCost: any;
            const ProgramCost: any;
            const Percent: any;
            const CreatedUserId: any;
            const CreatedDate: any;
            const DiscoverLeakDate: any;
            const DiscoverLeakDateShamsi: any;
            const EndDate: any;
            const ConfirmUserId: any;
            const ActionId: any;
            const RequestId2: string;
            const RequestProvinceId: string;
            const RequestActivityId: string;
            const RequestCycleId: string;
            const RequestCustomerEffectId: string;
            const RequestOrganizationEffectId: string;
            const RequestRiskLevelId: string;
            const RequestIncomeFlowId: string;
            const RequestCount: string;
            const RequestCycleCost: string;
            const RequestFactor: string;
            const RequestDelayedCost: string;
            const RequestYearCost: string;
            const RequestAccessibleCost: string;
            const RequestInaccessibleCost: string;
            const RequestFinancial: string;
            const RequestLeakDate: string;
            const RequestDiscoverLeakDate: string;
            const RequestDiscoverLeakDateShamsi: string;
            const RequestEventDescription: string;
            const RequestMainReason: string;
            const RequestCorrectionOperation: string;
            const RequestAvoidRepeatingOperation: string;
            const RequestCreatedUserId: string;
            const RequestCreatedDate: string;
            const RequestCreatedDateShamsi: string;
            const RequestModifiedUserId: string;
            const RequestModifiedDate: string;
            const RequestIsDeleted: string;
            const RequestDeletedUserId: string;
            const RequestDeletedDate: string;
            const RequestEndDate: string;
            const RequestStatusId: string;
            const RequestActionId: string;
            const ProvinceName: string;
            const ProvinceEnglishName: string;
            const ProvinceManagerName: string;
            const ProvinceLetterNo: string;
            const ProvincePmoLevelId: string;
            const ProvinceInstallDate: string;
            const ProvinceCreatedUserId: string;
            const ProvinceCreatedDate: string;
            const ProvinceModifiedUserId: string;
            const ProvinceModifiedDate: string;
            const ProvinceIsDeleted: string;
            const ProvinceDeletedUserId: string;
            const ProvinceDeletedDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityEnglishName: string;
            const ActivityObjective: string;
            const ActivityEnglishObjective: string;
            const ActivityCreatedUserId: string;
            const ActivityCreatedDate: string;
            const ActivityModifiedUserId: string;
            const ActivityModifiedDate: string;
            const ActivityIsDeleted: string;
            const ActivityDeletedUserId: string;
            const ActivityDeletedDate: string;
            const ActivityGroupId: string;
            const ActivityRepeatTermId: string;
            const ActivityKeyCheckArea: string;
            const ActivityDataSource: string;
            const ActivityMethodology: string;
            const ActivityKeyFocus: string;
            const ActivityAction: string;
            const ActivityKpi: string;
        }
    }
}
declare namespace CaseManagement.StimulReport {
    namespace ActivityReportService {
        const baseUrl = "StimulReport/ActivityReport";
        function Create(request: Serenity.SaveRequest<ActivityReportRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Serenity.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityReportRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Serenity.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Serenity.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityReportRow>) => void, opt?: Serenity.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityReportRow>) => void, opt?: Serenity.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.StimulReport {
}
declare namespace CaseManagement.StimulReport {
    class ActivityRequestDetailForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestDetailForm {
        Id2: Serenity.IntegerEditor;
        ProvinceId: Serenity.LookupEditor;
        ActivityId: Serenity.LookupEditor;
        CycleId: Serenity.LookupEditor;
        CustomerEffectId: Serenity.LookupEditor;
        RiskLevelId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.StringEditor;
        Factor: Serenity.StringEditor;
        DelayedCost: Serenity.StringEditor;
        YearCost: Serenity.StringEditor;
        AccessibleCost: Serenity.StringEditor;
        InaccessibleCost: Serenity.StringEditor;
        TotalLeakage: Serenity.StringEditor;
        RecoverableLeakage: Serenity.StringEditor;
        Recovered: Serenity.StringEditor;
        Financial: Serenity.StringEditor;
        LeakDate: Serenity.DateEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        DiscoverLeakDateShamsi: Serenity.StringEditor;
        EventDescription: Serenity.StringEditor;
        MainReason: Serenity.StringEditor;
        CorrectionOperation: Serenity.StringEditor;
        AvoidRepeatingOperation: Serenity.StringEditor;
        CreatedUserId: Serenity.LookupEditor;
        CreatedDate: Serenity.DateEditor;
        CreatedDateShamsi: Serenity.StringEditor;
        ModifiedUserId: Serenity.LookupEditor;
        ModifiedDate: Serenity.DateEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.LookupEditor;
        DeletedDate: Serenity.DateEditor;
        EndDate: Serenity.DateEditor;
        StatusId: Serenity.LookupEditor;
        ActionId: Serenity.IntegerEditor;
    }
}
declare namespace CaseManagement.StimulReport {
    interface ActivityRequestDetailRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
    }
    namespace ActivityRequestDetailRow {
        const idProperty = "Id";
        const localTextPrefix = "StimulReport.ActivityRequestDetail";
        namespace Fields {
            const Id: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const Financial: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const EventDescription: string;
            const MainReason: string;
            const CorrectionOperation: string;
            const AvoidRepeatingOperation: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const ActivityId: string;
            const ProvinceId: string;
            const CycleId: string;
            const CustomerEffectId: string;
            const IncomeFlowId: string;
            const RiskLevelId: string;
            const StatusID: string;
            const LeakDate: string;
            const DiscoverLeakDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityObjective: string;
            const ActivityGroupId: string;
            const ProvinceName: string;
            const CycleName: string;
            const CustomerEffectName: string;
            const IncomeFlowName: string;
            const RiskLevelName: string;
            const StatusName: string;
            const CreatedUserName: string;
            const ModifiedUserName: string;
            const DeletedUserName: string;
        }
    }
}
declare namespace CaseManagement.StimulReport {
    namespace ActivityRequestDetailService {
        const baseUrl = "StimulReport/ActivityRequestDetail";
        function Create(request: Serenity.SaveRequest<ActivityRequestDetailRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestDetailRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestDetailRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestDetailRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.StimulReport {
}
declare namespace CaseManagement.StimulReport {
    class ActivityRequestReportForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface ActivityRequestReportForm {
        Id2: Serenity.IntegerEditor;
        ProvinceId: Serenity.LookupEditor;
        ActivityId: Serenity.LookupEditor;
        ActivityCode: Serenity.StringEditor;
        CycleId: Serenity.LookupEditor;
        CustomerEffectId: Serenity.LookupEditor;
        RiskLevelId: Serenity.LookupEditor;
        IncomeFlowId: Serenity.LookupEditor;
        Count: Serenity.IntegerEditor;
        CycleCost: Serenity.StringEditor;
        Factor: Serenity.StringEditor;
        DelayedCost: Serenity.StringEditor;
        YearCost: Serenity.StringEditor;
        AccessibleCost: Serenity.StringEditor;
        InaccessibleCost: Serenity.StringEditor;
        TotalLeakage: Serenity.StringEditor;
        RecoverableLeakage: Serenity.StringEditor;
        Recovered: Serenity.StringEditor;
        Financial: Serenity.StringEditor;
        LeakDate: Serenity.DateEditor;
        DiscoverLeakDate: Serenity.DateEditor;
        DiscoverLeakDateShamsi: Serenity.StringEditor;
        EventDescription: Serenity.StringEditor;
        MainReason: Serenity.StringEditor;
        CorrectionOperation: Serenity.StringEditor;
        AvoidRepeatingOperation: Serenity.StringEditor;
        CreatedUserId: Serenity.LookupEditor;
        CreatedDate: Serenity.DateEditor;
        CreatedDateShamsi: Serenity.StringEditor;
        ModifiedUserId: Serenity.LookupEditor;
        ModifiedDate: Serenity.DateEditor;
        SendDate: Serenity.DateEditor;
        SendUserId: Serenity.LookupEditor;
        IsDeleted: Serenity.BooleanEditor;
        DeletedUserId: Serenity.LookupEditor;
        DeletedDate: Serenity.DateEditor;
        EndDate: Serenity.DateEditor;
        StatusId: Serenity.LookupEditor;
        ActionId: Serenity.IntegerEditor;
        File1: Serenity.StringEditor;
        File2: Serenity.StringEditor;
        File3: Serenity.StringEditor;
        ConfirmTypeId: Serenity.EnumEditor;
        IsRejected: Serenity.BooleanEditor;
        FinancialControllerConfirm: Serenity.BooleanEditor;
    }
}
declare namespace CaseManagement.StimulReport {
    interface ActivityRequestReportRow {
        Id?: number;
        Count?: number;
        CycleCost?: number;
        Factor?: number;
        DelayedCost?: number;
        YearCost?: number;
        AccessibleCost?: number;
        InaccessibleCost?: number;
        Financial?: number;
        TotalLeakage?: number;
        RecoverableLeakage?: number;
        Recovered?: number;
        EventDescription?: string;
        MainReason?: string;
        CorrectionOperation?: string;
        AvoidRepeatingOperation?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        SendDate?: string;
        SendUserId?: number;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        EndDate?: string;
        ActivityId?: number;
        ProvinceId?: number;
        CycleId?: number;
        CustomerEffectId?: number;
        IncomeFlowId?: number;
        RiskLevelId?: number;
        StatusID?: number;
        LeakDate?: string;
        DiscoverLeakDate?: string;
        ActivityCode?: string;
        ActivityName?: string;
        ActivityObjective?: string;
        ActivityGroupId?: number;
        ProvinceName?: string;
        CycleName?: string;
        CustomerEffectName?: string;
        IncomeFlowName?: string;
        RiskLevelName?: string;
        StatusName?: string;
        CreatedUserName?: string;
        ModifiedUserName?: string;
        DeletedUserName?: string;
        SendUserName?: string;
        ConfirmTypeID?: Case.ConfirmType;
        IsRejected?: boolean;
        FinancialControllerConfirm?: boolean;
    }
    namespace ActivityRequestReportRow {
        const idProperty = "Id";
        const localTextPrefix = "StimulReport.ActivityRequestReport";
        namespace Fields {
            const Id: string;
            const Count: string;
            const CycleCost: string;
            const Factor: string;
            const DelayedCost: string;
            const YearCost: string;
            const AccessibleCost: string;
            const InaccessibleCost: string;
            const Financial: string;
            const TotalLeakage: string;
            const RecoverableLeakage: string;
            const Recovered: string;
            const EventDescription: string;
            const MainReason: string;
            const CorrectionOperation: string;
            const AvoidRepeatingOperation: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const SendDate: string;
            const SendUserId: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const EndDate: string;
            const ActivityId: string;
            const ProvinceId: string;
            const CycleId: string;
            const CustomerEffectId: string;
            const IncomeFlowId: string;
            const RiskLevelId: string;
            const StatusID: string;
            const LeakDate: string;
            const DiscoverLeakDate: string;
            const ActivityCode: string;
            const ActivityName: string;
            const ActivityObjective: string;
            const ActivityGroupId: string;
            const ProvinceName: string;
            const CycleName: string;
            const CustomerEffectName: string;
            const IncomeFlowName: string;
            const RiskLevelName: string;
            const StatusName: string;
            const CreatedUserName: string;
            const ModifiedUserName: string;
            const DeletedUserName: string;
            const SendUserName: string;
            const ConfirmTypeID: string;
            const IsRejected: string;
            const FinancialControllerConfirm: string;
        }
    }
}
declare namespace CaseManagement.StimulReport {
    namespace ActivityRequestReportService {
        const baseUrl = "StimulReport/ActivityRequestReport";
        function Create(request: Serenity.SaveRequest<ActivityRequestReportRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<ActivityRequestReportRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<ActivityRequestReportRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<ActivityRequestReportRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.WorkFlow {
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowActionForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface WorkFlowActionForm {
        Name: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.WorkFlow {
    interface WorkFlowActionRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace WorkFlowActionRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "WorkFlow.WorkFlowAction";
        const lookupKey = "WorkFlow.WorkFlowAction";
        function getLookup(): Q.Lookup<WorkFlowActionRow>;
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.WorkFlow {
    namespace WorkFlowActionService {
        const baseUrl = "WorkFlow/WorkFlowAction";
        function Create(request: Serenity.SaveRequest<WorkFlowActionRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<WorkFlowActionRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<WorkFlowActionRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<WorkFlowActionRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.WorkFlow {
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowRuleForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface WorkFlowRuleForm {
        CurrentStatusId: Serenity.LookupEditor;
        ActionId: Serenity.LookupEditor;
        NextStatusId: Serenity.LookupEditor;
        Version: Serenity.IntegerEditor;
    }
}
declare namespace CaseManagement.WorkFlow {
    interface WorkFlowRuleRow {
        Id?: number;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        ActionId?: number;
        CurrentStatusId?: number;
        NextStatusId?: number;
        Version?: number;
        ActionName?: string;
        CurrentStatusName?: string;
        NextStatusName?: string;
    }
    namespace WorkFlowRuleRow {
        const idProperty = "Id";
        const localTextPrefix = "WorkFlow.WorkFlowRule";
        namespace Fields {
            const Id: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const ActionId: string;
            const CurrentStatusId: string;
            const NextStatusId: string;
            const Version: string;
            const ActionName: string;
            const CurrentStatusName: string;
            const NextStatusName: string;
        }
    }
}
declare namespace CaseManagement.WorkFlow {
    namespace WorkFlowRuleService {
        const baseUrl = "WorkFlow/WorkFlowRule";
        function Create(request: Serenity.SaveRequest<WorkFlowRuleRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<WorkFlowRuleRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<WorkFlowRuleRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<WorkFlowRuleRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.WorkFlow {
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowStatusForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface WorkFlowStatusForm {
        StepId: Serenity.LookupEditor;
        StatusTypeId: Serenity.LookupEditor;
    }
}
declare namespace CaseManagement.WorkFlow {
    interface WorkFlowStatusRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
        StepId?: number;
        StatusTypeId?: number;
        StepName?: string;
        StatusTypeName?: string;
        FullName?: string;
    }
    namespace WorkFlowStatusRow {
        const idProperty = "Id";
        const nameProperty = "FullName";
        const localTextPrefix = "WorkFlow.WorkFlowStatus";
        const lookupKey = "WorkFlow.WorkFlowStatus";
        function getLookup(): Q.Lookup<WorkFlowStatusRow>;
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
            const StepId: string;
            const StatusTypeId: string;
            const StepName: string;
            const StatusTypeName: string;
            const FullName: string;
        }
    }
}
declare namespace CaseManagement.WorkFlow {
    namespace WorkFlowStatusService {
        const baseUrl = "WorkFlow/WorkFlowStatus";
        function Create(request: Serenity.SaveRequest<WorkFlowStatusRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<WorkFlowStatusRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<WorkFlowStatusRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<WorkFlowStatusRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.WorkFlow {
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowStatusTypeForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface WorkFlowStatusTypeForm {
        Name: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.WorkFlow {
    interface WorkFlowStatusTypeRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace WorkFlowStatusTypeRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "WorkFlow.WorkFlowStatusType";
        const lookupKey = "WorkFlow.WorkFlowStatusType";
        function getLookup(): Q.Lookup<WorkFlowStatusTypeRow>;
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.WorkFlow {
    namespace WorkFlowStatusTypeService {
        const baseUrl = "WorkFlow/WorkFlowStatusType";
        function Create(request: Serenity.SaveRequest<WorkFlowStatusTypeRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<WorkFlowStatusTypeRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<WorkFlowStatusTypeRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<WorkFlowStatusTypeRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement.WorkFlow {
}
declare namespace CaseManagement.WorkFlow {
    class WorkFlowStepForm extends Serenity.PrefixedContext {
        static formKey: string;
    }
    interface WorkFlowStepForm {
        Name: Serenity.StringEditor;
    }
}
declare namespace CaseManagement.WorkFlow {
    interface WorkFlowStepRow {
        Id?: number;
        Name?: string;
        CreatedUserId?: number;
        CreatedDate?: string;
        ModifiedUserId?: number;
        ModifiedDate?: string;
        IsDeleted?: boolean;
        DeletedUserId?: number;
        DeletedDate?: string;
    }
    namespace WorkFlowStepRow {
        const idProperty = "Id";
        const nameProperty = "Name";
        const localTextPrefix = "WorkFlow.WorkFlowStep";
        const lookupKey = "WorkFlow.WorkFlowStep";
        function getLookup(): Q.Lookup<WorkFlowStepRow>;
        namespace Fields {
            const Id: string;
            const Name: string;
            const CreatedUserId: string;
            const CreatedDate: string;
            const ModifiedUserId: string;
            const ModifiedDate: string;
            const IsDeleted: string;
            const DeletedUserId: string;
            const DeletedDate: string;
        }
    }
}
declare namespace CaseManagement.WorkFlow {
    namespace WorkFlowStepService {
        const baseUrl = "WorkFlow/WorkFlowStep";
        function Create(request: Serenity.SaveRequest<WorkFlowStepRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Update(request: Serenity.SaveRequest<WorkFlowStepRow>, onSuccess?: (response: Serenity.SaveResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Delete(request: Serenity.DeleteRequest, onSuccess?: (response: Serenity.DeleteResponse) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function Retrieve(request: Serenity.RetrieveRequest, onSuccess?: (response: Serenity.RetrieveResponse<WorkFlowStepRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        function List(request: Serenity.ListRequest, onSuccess?: (response: Serenity.ListResponse<WorkFlowStepRow>) => void, opt?: Q.ServiceOptions<any>): JQueryXHR;
        namespace Methods {
            const Create: string;
            const Update: string;
            const Delete: string;
            const Retrieve: string;
            const List: string;
        }
    }
}
declare namespace CaseManagement {
    class BasicProgressDialog extends Serenity.TemplatedDialog<any> {
        constructor();
        cancelled: boolean;
        max: number;
        value: number;
        title: string;
        cancelTitle: string;
        getDialogOptions(): JQueryUI.DialogOptions;
        initDialog(): void;
        getTemplate(): string;
    }
}
declare namespace CaseManagement.Common {
    class BulkServiceAction {
        protected keys: string[];
        protected queue: string[];
        protected queueIndex: number;
        protected progressDialog: BasicProgressDialog;
        protected pendingRequests: number;
        protected completedRequests: number;
        protected errorByKey: Q.Dictionary<Serenity.ServiceError>;
        private successCount;
        private errorCount;
        done: () => void;
        protected createProgressDialog(): void;
        protected getConfirmationFormat(): string;
        protected getConfirmationMessage(targetCount: any): string;
        protected confirm(targetCount: any, action: any): void;
        protected getNothingToProcessMessage(): string;
        protected nothingToProcess(): void;
        protected getParallelRequests(): number;
        protected getBatchSize(): number;
        protected startParallelExecution(): void;
        protected serviceCallCleanup(): void;
        protected executeForBatch(batch: string[]): void;
        protected executeNextBatch(): void;
        protected getAllHadErrorsFormat(): string;
        protected showAllHadErrors(): void;
        protected getSomeHadErrorsFormat(): string;
        protected showSomeHadErrors(): void;
        protected getAllSuccessFormat(): string;
        protected showAllSuccess(): void;
        protected showResults(): void;
        execute(keys: string[]): void;
        get_successCount(): any;
        set_successCount(value: number): void;
        get_errorCount(): any;
        set_errorCount(value: number): void;
    }
}
declare namespace CaseManagement.DialogUtils {
    function pendingChangesConfirmation(element: JQuery, hasPendingChanges: () => boolean): void;
}
declare namespace CaseManagement.Common {
    interface ExcelExportOptions {
        grid: Serenity.DataGrid<any, any>;
        service: string;
        onViewSubmit: () => boolean;
        title?: string;
        hint?: string;
        separator?: boolean;
    }
    namespace ExcelExportHelper {
        function createToolButton(options: ExcelExportOptions): Serenity.ToolButton;
    }
}
declare namespace CaseManagement.Common {
    class GridEditorBase<TEntity> extends Serenity.EntityGrid<TEntity, any> implements Serenity.IGetEditValue, Serenity.ISetEditValue {
        protected getIdProperty(): string;
        private nextId;
        constructor(container: JQuery);
        protected id(entity: TEntity): any;
        protected save(opt: Serenity.ServiceOptions<any>, callback: (r: Serenity.ServiceResponse) => void): void;
        protected deleteEntity(id: number): boolean;
        protected validateEntity(row: TEntity, id: number): boolean;
        protected setEntities(items: TEntity[]): void;
        protected getNewEntity(): TEntity;
        protected getButtons(): Serenity.ToolButton[];
        protected editItem(entityOrId: any): void;
        getEditValue(property: any, target: any): void;
        setEditValue(source: any, property: any): void;
        value: TEntity[];
        protected getGridCanLoad(): boolean;
        protected usePager(): boolean;
        protected getInitialTitle(): any;
        protected createQuickSearchInput(): void;
    }
}
declare namespace CaseManagement.Common {
    class GridEditorDialog<TEntity> extends Serenity.EntityDialog<TEntity, any> {
        protected getIdProperty(): string;
        onSave: (options: Serenity.ServiceOptions<Serenity.SaveResponse>, callback: (response: Serenity.SaveResponse) => void) => void;
        onDelete: (options: Serenity.ServiceOptions<Serenity.DeleteResponse>, callback: (response: Serenity.DeleteResponse) => void) => void;
        destroy(): void;
        protected updateInterface(): void;
        protected saveHandler(options: Serenity.ServiceOptions<Serenity.SaveResponse>, callback: (response: Serenity.SaveResponse) => void): void;
        protected deleteHandler(options: Serenity.ServiceOptions<Serenity.DeleteResponse>, callback: (response: Serenity.DeleteResponse) => void): void;
    }
}
declare namespace CaseManagement.LanguageList {
    function getValue(): string[][];
}
declare namespace CaseManagement.Common {
    interface ReportButtonOptions {
        title?: string;
        cssClass?: string;
        icon?: string;
        download?: boolean;
        reportKey: string;
        extension?: string;
        getParams?: () => any;
        target?: string;
    }
    namespace ReportHelper {
        function createToolButton(options: ReportButtonOptions): Serenity.ToolButton;
    }
}
declare namespace CaseManagement.Common.DashboardUser {
    function initializePage(): void;
}
declare var Morris: any;
declare namespace CaseManagement.Common {
    class DashboardIndex extends Serenity.TemplatedDialog<any> {
        private areaChart;
        static ProvinceProgram95(): void;
    }
}
declare namespace CaseManagement.Case {
    class YearDialog extends Serenity.EntityDialog<YearRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: YearForm;
    }
}
declare namespace CaseManagement.Case {
    class YearGrid extends Serenity.EntityGrid<YearRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof YearDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class SwitchTransitDialog extends Serenity.EntityDialog<SwitchTransitRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: SwitchTransitForm;
    }
}
declare namespace CaseManagement.Case {
    class SwitchTransitGrid extends Serenity.EntityGrid<SwitchTransitRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof SwitchTransitDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class SwitchDslamProvinceDialog extends Serenity.EntityDialog<SwitchDslamProvinceRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: SwitchDslamProvinceForm;
    }
}
declare namespace CaseManagement.Case {
    class SwitchDslamDialog extends Serenity.EntityDialog<SwitchDslamRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: SwitchDslamForm;
    }
}
declare namespace CaseManagement.Case {
    class SwitchDslamGrid extends Serenity.EntityGrid<SwitchDslamRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof SwitchDslamDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class SwitchDialog extends Serenity.EntityDialog<SwitchRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: SwitchForm;
    }
}
declare namespace CaseManagement.Case {
    class SwitchGrid extends Serenity.EntityGrid<SwitchRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof SwitchDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class SoftwareDialog extends Serenity.EntityDialog<SoftwareRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: SoftwareForm;
    }
}
declare namespace CaseManagement.Case {
    class SoftwareGrid extends Serenity.EntityGrid<SoftwareRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof SoftwareDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class SMSLogDialog extends Serenity.EntityDialog<SMSLogRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: SMSLogForm;
    }
}
declare namespace CaseManagement.Case {
    class SMSLogGrid extends Serenity.EntityGrid<SMSLogRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof SMSLogDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected PersiaNumber(value: any): any;
        protected getColumns(): Slick.Column[];
        protected onClick(e: JQueryEventObject, row: number, cell: number): void;
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class RiskLevelDialog extends Serenity.EntityDialog<RiskLevelRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: RiskLevelForm;
    }
}
declare namespace CaseManagement.Case {
    class RiskLevelGrid extends Serenity.EntityGrid<RiskLevelRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof RiskLevelDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class RepeatTermDialog extends Serenity.EntityDialog<RepeatTermRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: RepeatTermForm;
    }
}
declare namespace CaseManagement.Case {
    class RepeatTermGrid extends Serenity.EntityGrid<RepeatTermRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof RepeatTermDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class ProvinceProgramLogDialog extends Serenity.EntityDialog<ProvinceProgramLogRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ProvinceProgramLogForm;
    }
}
declare namespace CaseManagement.Case {
    class ProvinceProgramLogGrid extends Serenity.EntityGrid<ProvinceProgramLogRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ProvinceProgramLogDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class ProvinceProgramDialog extends Serenity.EntityDialog<ProvinceProgramRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ProvinceProgramForm;
    }
}
declare namespace CaseManagement.Case {
    class ProvinceProgramGrid extends Serenity.EntityGrid<ProvinceProgramRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ProvinceProgramDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected createSlickGrid(): Slick.Grid;
        protected getSlickOptions(): Slick.GridOptions;
        protected usePager(): boolean;
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class ProvinceCompanySoftwareDialog extends Serenity.EntityDialog<ProvinceCompanySoftwareRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ProvinceCompanySoftwareForm;
    }
}
declare namespace CaseManagement.Case {
    class ProvinceCompanySoftwareGrid extends Serenity.EntityGrid<ProvinceCompanySoftwareRow, any> {
        protected getColumnsKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): any;
        protected getInitialTitle(): any;
        protected usePager(): boolean;
        protected getGridCanLoad(): boolean;
        private _ProvinceID;
        ProvinceID: number;
    }
}
declare namespace CaseManagement.Case {
    class ProvinceDialog extends Serenity.EntityDialog<ProvinceRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: ProvinceForm;
        private switchsGrid;
        private switchDSLAMsGrid;
        private switchTransitsGrid;
        private softwaresGrid;
        constructor();
        protected afterLoadEntity(): void;
    }
}
declare namespace CaseManagement.Case {
    class ProvinceGrid extends Serenity.EntityGrid<ProvinceRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ProvinceDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): {
            title: string;
            cssClass: string;
            onClick: () => void;
        }[];
    }
}
declare namespace CaseManagement.Case {
    class ProvinceSwitchDSLAMGrid extends Serenity.EntityGrid<SwitchDslamProvinceRow, any> {
        protected getColumnsKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): any;
        protected getInitialTitle(): any;
        protected usePager(): boolean;
        protected getGridCanLoad(): boolean;
        private _ProvinceID;
        ProvinceID: number;
    }
}
declare namespace CaseManagement.Case {
    class ProvinceSwitchGrid extends Serenity.EntityGrid<SwitchProvinceRow, any> {
        protected getColumnsKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): any;
        protected getInitialTitle(): any;
        protected usePager(): boolean;
        protected getGridCanLoad(): boolean;
        private _ProvinceID;
        ProvinceID: number;
    }
}
declare namespace CaseManagement.Case {
    class ProvinceSwitchTransitGrid extends Serenity.EntityGrid<SwitchTransitProvinceRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof SwitchTransitProvinceDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getColumns(): Slick.Column[];
        protected initEntityDialog(itemType: any, dialog: any): void;
        protected addButtonClick(): void;
        protected getInitialTitle(): any;
        protected usePager(): boolean;
        protected getGridCanLoad(): boolean;
        private _ProvinceID;
        ProvinceID: number;
    }
}
declare namespace CaseManagement.Case {
    class SwitchTransitProvinceDialog extends Serenity.EntityDialog<SwitchTransitProvinceRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: SwitchTransitProvinceForm;
        updateInterface(): void;
    }
}
declare namespace CaseManagement.Case {
    class PmoLevelDialog extends Serenity.EntityDialog<PmoLevelRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: PmoLevelForm;
    }
}
declare namespace CaseManagement.Case {
    class PmoLevelGrid extends Serenity.EntityGrid<PmoLevelRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof PmoLevelDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class IncomeFlowDialog extends Serenity.EntityDialog<IncomeFlowRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: IncomeFlowForm;
    }
}
declare namespace CaseManagement.Case {
    class IncomeFlowGrid extends Serenity.EntityGrid<IncomeFlowRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof IncomeFlowDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class CycleDialog extends Serenity.EntityDialog<CycleRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: CycleForm;
    }
}
declare namespace CaseManagement.Case {
    class CycleGrid extends Serenity.EntityGrid<CycleRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof CycleDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class CustomerEffectDialog extends Serenity.EntityDialog<CustomerEffectRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: CustomerEffectForm;
    }
}
declare namespace CaseManagement.Case {
    class CustomerEffectGrid extends Serenity.EntityGrid<CustomerEffectRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof CustomerEffectDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class CompanyDialog extends Serenity.EntityDialog<CompanyRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: CompanyForm;
    }
}
declare namespace CaseManagement.Case {
    class CompanyGrid extends Serenity.EntityGrid<CompanyRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof CompanyDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class CommentReasonDialog extends Serenity.EntityDialog<CommentReasonRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: CommentReasonForm;
    }
}
declare namespace CaseManagement.Case {
    class CommentReasonGrid extends Serenity.EntityGrid<CommentReasonRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof CommentReasonDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestTechnicalDialog extends Serenity.EntityDialog<ActivityRequestTechnicalRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ActivityRequestTechnicalForm;
        private logsGrid;
        constructor();
        protected updateInterface(): void;
        protected afterLoadEntity(): void;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestTechnicalGrid extends Serenity.EntityGrid<ActivityRequestTechnicalRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestTechnicalDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected createSlickGrid(): Slick.Grid;
        protected getSlickOptions(): Slick.GridOptions;
        protected getButtons(): Serenity.ToolButton[];
        protected getItemCssClass(item: Case.ActivityRequestRow, index: number): string;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestPenddingDialog extends Serenity.EntityDialog<ActivityRequestPenddingRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ActivityRequestPenddingForm;
        private logsGrid;
        constructor();
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
        protected afterLoadEntity(): void;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestPenddingGrid extends Serenity.EntityGrid<ActivityRequestPenddingRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestPenddingDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected createSlickGrid(): Slick.Grid;
        protected getSlickOptions(): Slick.GridOptions;
        protected getButtons(): Serenity.ToolButton[];
        protected getItemCssClass(item: Case.ActivityRequestPenddingRow, index: number): string;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestLogDialog extends Serenity.EntityDialog<ActivityRequestLogRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ActivityRequestLogForm;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestLeaderDialog extends Serenity.EntityDialog<ActivityRequestLeaderRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ActivityRequestLeaderForm;
        private logsGrid;
        constructor();
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
        protected afterLoadEntity(): void;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestLeaderGrid extends Serenity.EntityGrid<ActivityRequestLeaderRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestLeaderDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected createSlickGrid(): Slick.Grid;
        protected getSlickOptions(): Slick.GridOptions;
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestHistoryDialog extends Serenity.EntityDialog<ActivityRequestHistoryRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): any;
        protected getService(): string;
        protected form: ActivityRequestHistoryForm;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestHistoryGrid extends Serenity.EntityGrid<ActivityRequestHistoryRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestHistoryDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestFinancialDialog extends Serenity.EntityDialog<ActivityRequestFinancialRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ActivityRequestFinancialForm;
        private logsGrid;
        constructor();
        protected updateInterface(): void;
        protected afterLoadEntity(): void;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestFinancialGrid extends Serenity.EntityGrid<ActivityRequestFinancialRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestFinancialDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected createSlickGrid(): Slick.Grid;
        protected getSlickOptions(): Slick.GridOptions;
        protected getButtons(): Serenity.ToolButton[];
        protected getItemCssClass(item: Case.ActivityRequestRow, index: number): string;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestDetailsInfoDialog extends Serenity.EntityDialog<ActivityRequestDetailsInfoRow, any> {
        protected getFormKey(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: ActivityRequestDetailsInfoForm;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestDetailsInfoGrid extends Serenity.EntityGrid<ActivityRequestDetailsInfoRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestDetailsInfoDialog;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestDenyDialog extends Serenity.EntityDialog<ActivityRequestDenyRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ActivityRequestDenyForm;
        private logsGrid;
        constructor();
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
        protected afterLoadEntity(): void;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestDenyGrid extends Serenity.EntityGrid<ActivityRequestDenyRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestDenyDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected createSlickGrid(): Slick.Grid;
        protected getSlickOptions(): Slick.GridOptions;
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestDeleteDialog extends Serenity.EntityDialog<ActivityRequestDeleteRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: ActivityRequestDeleteForm;
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestDeleteGrid extends Serenity.EntityGrid<ActivityRequestDeleteRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestDeleteDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestConfirmAdminDialog extends Serenity.EntityDialog<ActivityRequestConfirmAdminRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ActivityRequestConfirmAdminForm;
        private logsGrid;
        constructor();
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected afterLoadEntity(): void;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestConfirmAdminGrid extends Serenity.EntityGrid<ActivityRequestConfirmAdminRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestConfirmAdminDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected createSlickGrid(): Slick.Grid;
        protected getSlickOptions(): Slick.GridOptions;
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestConfirmDialog extends Serenity.EntityDialog<ActivityRequestConfirmRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ActivityRequestConfirmForm;
        private logsGrid;
        constructor();
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
        protected afterLoadEntity(): void;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestConfirmGrid extends Serenity.EntityGrid<ActivityRequestConfirmRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestConfirmDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected createSlickGrid(): Slick.Grid;
        protected getSlickOptions(): Slick.GridOptions;
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestCommentReasonDialog extends Serenity.EntityDialog<ActivityRequestCommentReasonRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ActivityRequestCommentReasonForm;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestCommentReasonGrid extends Serenity.EntityGrid<ActivityRequestCommentReasonRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestCommentReasonDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestCommentEditDialog extends Common.GridEditorDialog<ActivityRequestCommentRow> {
        protected getFormKey(): string;
        protected getNameProperty(): string;
        protected getLocalTextPrefix(): string;
        protected form: ActivityRequestCommentForm;
        constructor();
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestCommentEditor extends Common.GridEditorBase<ActivityRequestCommentRow> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestCommentEditDialog;
        protected getLocalTextPrefix(): string;
        constructor(container: JQuery);
        protected getAddButtonCaption(): string;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestDialog extends Serenity.EntityDialog<ActivityRequestRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: ActivityRequestForm;
        private logsGrid;
        constructor();
        protected afterLoadEntity(): void;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestGrid extends Serenity.EntityGrid<ActivityRequestRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityRequestDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
        protected getItemCssClass(item: Case.ActivityRequestRow, index: number): string;
    }
}
declare namespace CaseManagement.Case {
    class ActivityRequestLogGrid extends Serenity.EntityGrid<ActivityRequestLogRow, any> {
        protected getColumnsKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): any;
        protected getInitialTitle(): any;
        protected usePager(): boolean;
        protected getGridCanLoad(): boolean;
        private _ActivityRequestID;
        ActivityRequestID: number;
    }
}
declare namespace CaseManagement.Case {
    class ActivityMainReasonDialog extends Serenity.EntityDialog<ActivityMainReasonRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: ActivityMainReasonForm;
    }
}
declare namespace CaseManagement.Case {
    class ActivityMainReasonEditDialog extends Common.GridEditorDialog<ActivityMainReasonRow> {
        protected getFormKey(): string;
        protected getNameProperty(): string;
        protected getLocalTextPrefix(): string;
        protected form: ActivityMainReasonForm;
        constructor();
    }
}
declare namespace CaseManagement.Case {
    class ActivityMainReasonEditor extends Common.GridEditorBase<ActivityMainReasonRow> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityMainReasonEditDialog;
        protected getLocalTextPrefix(): string;
        constructor(container: JQuery);
        protected getAddButtonCaption(): string;
    }
}
declare namespace CaseManagement.Case {
    class ActivityHelpDialog extends Serenity.EntityDialog<ActivityHelpRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: ActivityHelpForm;
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
    }
    interface ActivityHelpDialogOptions {
        activityID?: number;
    }
}
declare namespace CaseManagement.Case {
    class ActivityHelpGrid extends Serenity.EntityGrid<ActivityHelpRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityHelpDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class ActivityGroupDialog extends Serenity.EntityDialog<ActivityGroupRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: ActivityGroupForm;
    }
}
declare namespace CaseManagement.Case {
    class ActivityGroupGrid extends Serenity.EntityGrid<ActivityGroupRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityGroupDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Case {
    class ActivityCorrectionOperationDialog extends Serenity.EntityDialog<ActivityCorrectionOperationRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: ActivityCorrectionOperationForm;
    }
}
declare namespace CaseManagement.Case {
    class ActivityCorrectionOperationEditDialog extends Common.GridEditorDialog<ActivityCorrectionOperationRow> {
        protected getFormKey(): string;
        protected getNameProperty(): string;
        protected getLocalTextPrefix(): string;
        protected form: ActivityCorrectionOperationForm;
        constructor();
    }
}
declare namespace CaseManagement.Case {
    class ActivityCorrectionOperationEditor extends Common.GridEditorBase<ActivityMainReasonRow> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityCorrectionOperationEditDialog;
        protected getLocalTextPrefix(): string;
        constructor(container: JQuery);
        protected getAddButtonCaption(): string;
    }
}
declare namespace CaseManagement.Case {
    class ActivityDialog extends Serenity.EntityDialog<ActivityRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: ActivityForm;
    }
}
declare namespace CaseManagement.Case {
    class ActivityGrid extends Serenity.EntityGrid<ActivityRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof ActivityDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Administration {
    class UserSupportGroupDialog extends Serenity.EntityDialog<UserSupportGroupRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        protected form: UserSupportGroupForm;
    }
}
declare namespace CaseManagement.Administration {
    class UserSupportGroupGrid extends Serenity.EntityGrid<UserSupportGroupRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof UserSupportGroupDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Administration {
    class RoleCheckEditor extends Serenity.CheckTreeEditor<Serenity.CheckTreeItem<any>, any> {
        private searchText;
        constructor(div: JQuery);
        protected createToolbarExtensions(): void;
        protected getButtons(): any[];
        protected getTreeItems(): Serenity.CheckTreeItem<any>[];
        protected onViewFilter(item: any): boolean;
    }
}
declare namespace CaseManagement.Administration {
    class UserRoleDialog extends Serenity.TemplatedDialog<UserRoleDialogOptions> {
        private permissions;
        constructor(opt: UserRoleDialogOptions);
        protected getDialogOptions(): JQueryUI.DialogOptions;
        protected getTemplate(): string;
    }
    interface UserRoleDialogOptions {
        userID: number;
        username: string;
    }
}
declare namespace CaseManagement.Administration {
    class PermissionCheckEditor extends Serenity.DataGrid<PermissionCheckItem, PermissionCheckEditorOptions> {
        protected getIdProperty(): string;
        private searchText;
        private byParentKey;
        private rolePermissions;
        constructor(container: JQuery, opt: PermissionCheckEditorOptions);
        private getItemGrantRevokeClass(item, grant);
        private getItemEffectiveClass(item);
        protected getColumns(): Slick.Column[];
        setItems(items: PermissionCheckItem[]): void;
        protected onViewSubmit(): boolean;
        protected onViewFilter(item: PermissionCheckItem): boolean;
        private matchContains(item);
        private getDescendants(item, excludeGroups);
        protected onClick(e: any, row: any, cell: any): void;
        private getParentKey(key);
        protected getButtons(): Serenity.ToolButton[];
        protected createToolbarExtensions(): void;
        private getSortedGroupAndPermissionKeys(titleByKey);
        get_value(): UserPermissionRow[];
        set_value(value: UserPermissionRow[]): void;
        get_rolePermissions(): string[];
        set_rolePermissions(value: string[]): void;
    }
    interface PermissionCheckEditorOptions {
        showRevoke?: boolean;
    }
    interface PermissionCheckItem {
        ParentKey?: string;
        Key?: string;
        Title?: string;
        IsGroup?: boolean;
        GrantRevoke?: boolean;
    }
}
declare namespace CaseManagement.Administration {
    class UserPermissionDialog extends Serenity.TemplatedDialog<UserPermissionDialogOptions> {
        private permissions;
        constructor(opt: UserPermissionDialogOptions);
        protected getDialogOptions(): JQueryUI.DialogOptions;
        protected getTemplate(): string;
    }
    interface UserPermissionDialogOptions {
        userID?: number;
        username?: string;
    }
}
declare namespace CaseManagement.Administration {
    class UserDialog extends Serenity.EntityDialog<UserRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getIsActiveProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: UserForm;
        constructor();
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
        protected afterLoadEntity(): void;
    }
}
declare namespace CaseManagement.Administration {
    class UserGrid extends Serenity.EntityGrid<UserRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof UserDialog;
        protected getIdProperty(): string;
        protected getIsActiveProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getDefaultSortBy(): string[];
    }
}
declare namespace CaseManagement.Authorization {
    let userDefinition: ScriptUserDefinition;
    function hasPermission(permissionKey: string): boolean;
}
declare namespace CaseManagement.Administration {
    class TranslationGrid extends Serenity.EntityGrid<TranslationItem, any> {
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        private hasChanges;
        private searchText;
        private sourceLanguage;
        private targetLanguage;
        private targetLanguageKey;
        constructor(container: JQuery);
        protected onClick(e: JQueryEventObject, row: number, cell: number): any;
        protected getColumns(): Slick.Column[];
        protected createToolbarExtensions(): void;
        protected saveChanges(language: string): RSVP.Promise<any>;
        protected onViewSubmit(): boolean;
        protected getButtons(): Serenity.ToolButton[];
        protected createQuickSearchInput(): void;
        protected onViewFilter(item: TranslationItem): boolean;
        protected usePager(): boolean;
    }
}
declare namespace CaseManagement.Administration {
    class RoleStepDialog extends Serenity.TemplatedDialog<RoleStepDialogOptions> {
        private steps;
        constructor(opt: RoleStepDialogOptions);
        protected getDialogOptions(): JQueryUI.DialogOptions;
        protected getTemplate(): string;
    }
    interface RoleStepDialogOptions {
        roleID?: number;
        title?: string;
    }
}
declare namespace CaseManagement.Administration {
    class StepCheckEditor extends Serenity.CheckTreeEditor<Serenity.CheckTreeItem<any>, any> {
        private searchText;
        constructor(div: JQuery);
        protected createToolbarExtensions(): void;
        protected getButtons(): any[];
        protected getTreeItems(): Serenity.CheckTreeItem<any>[];
        protected onViewFilter(item: any): boolean;
    }
}
declare namespace CaseManagement.Administration {
    class RolePermissionDialog extends Serenity.TemplatedDialog<RolePermissionDialogOptions> {
        private permissions;
        constructor(opt: RolePermissionDialogOptions);
        protected getDialogOptions(): JQueryUI.DialogOptions;
        protected getTemplate(): string;
    }
    interface RolePermissionDialogOptions {
        roleID?: number;
        title?: string;
    }
}
declare namespace CaseManagement.Administration {
    class RoleDialog extends Serenity.EntityDialog<RoleRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: RoleForm;
        protected getToolbarButtons(): Serenity.ToolButton[];
        protected updateInterface(): void;
    }
}
declare namespace CaseManagement.Administration {
    class RoleGrid extends Serenity.EntityGrid<RoleRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof RoleDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getDefaultSortBy(): string[];
    }
}
declare namespace CaseManagement.Administration {
    class NotificationGroupDialog extends Serenity.EntityDialog<NotificationGroupRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: NotificationGroupForm;
    }
}
declare namespace CaseManagement.Administration {
    class NotificationGroupGrid extends Serenity.EntityGrid<NotificationGroupRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof NotificationGroupDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Administration {
    class NotificationDialog extends Serenity.EntityDialog<NotificationRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: NotificationForm;
    }
}
declare namespace CaseManagement.Administration {
    class NotificationGrid extends Serenity.EntityGrid<NotificationRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof NotificationDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
declare namespace CaseManagement.Administration {
    class LogDialog extends Serenity.EntityDialog<LogRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: LogForm;
    }
}
declare namespace CaseManagement.Administration {
    class LogGrid extends Serenity.EntityGrid<LogRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof LogDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getButtons(): Serenity.ToolButton[];
    }
}
declare namespace CaseManagement.Administration {
    class LanguageDialog extends Serenity.EntityDialog<LanguageRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: LanguageForm;
    }
}
declare namespace CaseManagement.Administration {
    class LanguageGrid extends Serenity.EntityGrid<LanguageRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof LanguageDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
        protected getDefaultSortBy(): string[];
    }
}
declare namespace CaseManagement.Administration {
    class CalendarEventDialog extends Serenity.EntityDialog<CalendarEventRow, any> {
        protected getFormKey(): string;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getNameProperty(): string;
        protected getService(): string;
        protected form: CalendarEventForm;
    }
}
declare namespace CaseManagement.Administration {
    class CalendarEventGrid extends Serenity.EntityGrid<CalendarEventRow, any> {
        protected getColumnsKey(): string;
        protected getDialogType(): typeof CalendarEventDialog;
        protected getIdProperty(): string;
        protected getLocalTextPrefix(): string;
        protected getService(): string;
        constructor(container: JQuery);
    }
}
