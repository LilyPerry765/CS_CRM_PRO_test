﻿using Serenity.Navigation;

using Administration = CaseManagement.Administration.Pages;
using Case = CaseManagement.Case.Pages;
using WorkFlow = CaseManagement.WorkFlow.Pages;
using Report = CaseManagement.StimulReport.Pages;
using CasePage = CaseManagement.Common.Pages;


[assembly: NavigationLink(1000, "Dashboard", url: "~/", permission: CaseManagement.Administration.PermissionKeys.General, icon: "icon-speedometer")]
//[assembly: NavigationLink(1001, "داشبورد کاربر", typeof(CasePage.CommonPageController), action: "DashboardUser", icon: "icon-speedometer" /* permission: CaseManagement.Administration.PermissionKeys.User*/)]
[assembly: NavigationLink(1002, "وضعیت سرور", typeof(CasePage.CommonPageController), action: "ServerStatus", icon: "icon-speedometer")]
[assembly: NavigationLink(1003, "استان", typeof(Case.ProvinceController))]

[assembly: NavigationMenu(2000, "اطلاعات پایه")]
[assembly: NavigationLink(2001, "اطلاعات پایه/گروه فعالیت", typeof(Case.ActivityGroupController))]
[assembly: NavigationLink(2002, "اطلاعات پایه/تعریف فعالیت", typeof(Case.ActivityController))]
[assembly: NavigationLink(2011, "اطلاعات پایه/تعریف فعالیت", typeof(CaseManagement.Case.Pages.ActivityHelpController))]
[assembly: NavigationLink(2003, "اطلاعات پایه/تکرار دوره", typeof(Case.RepeatTermController))]
//[assembly: NavigationLink(2004, "اطلاعات پایه/اثر بر مشتری", typeof(Case.CustomerEffectController))]
//[assembly: NavigationLink(2005, "اطلاعات پایه/سطح ریسک", typeof(Case.RiskLevelController))]
[assembly: NavigationLink(2006, "اطلاعات پایه/جریان درآمدی", typeof(Case.IncomeFlowController))]
[assembly: NavigationLink(2007, "اطلاعات پایه/دوره", typeof(Case.CycleController))]

[assembly: NavigationMenu(3000, "اطلاعات فنی")]
[assembly: NavigationLink(3001, "اطلاعات فنی/سوییچ", typeof(CaseManagement.Case.Pages.SwitchController))]
[assembly: NavigationLink(3002, "اطلاعات فنی/سوییچ DSLAM", typeof(CaseManagement.Case.Pages.SwitchDslamController))]
[assembly: NavigationLink(3003, "اطلاعات فنی/سوییچ ترانزیت", typeof(CaseManagement.Case.Pages.SwitchTransitController))]

[assembly: NavigationMenu(5000, "مدیریت فعالیت ها")]
[assembly: NavigationLink(5001, "مدیریت فعالیت ها/در دست اقدام فنی", typeof(Case.ActivityRequestTechnicalController))]
[assembly: NavigationLink(5002, "مدیریت فعالیت ها/در دست اقدام مالی", typeof(CaseManagement.Case.Pages.ActivityRequestFinancialController))]
[assembly: NavigationLink(5003, "مدیریت فعالیت ها/فعالیت های تایید شده", typeof(CaseManagement.Case.Pages.ActivityRequestConfirmController))]
[assembly: NavigationLink(5004, "مدیریت فعالیت ها/ارجاع جهت اصلاح", typeof(CaseManagement.Case.Pages.ActivityRequestDenyController))]
[assembly: NavigationLink(5005, "مدیریت فعالیت ها/فعالیت های جاری استان", typeof(CaseManagement.Case.Pages.ActivityRequestPenddingController))]
[assembly: NavigationLink(5006, "مدیریت فعالیت ها/فعالیت های استان زیرگروه", typeof(CaseManagement.Case.Pages.ActivityRequestLeaderController))]
[assembly: NavigationLink(5007, "مدیریت فعالیت ها/فعالیت های تایید شده", typeof(CaseManagement.Case.Pages.ActivityRequestConfirmAdminController))]
[assembly: NavigationLink(5008, "مدیریت فعالیت ها/فعالیت های حذف شده", typeof(CaseManagement.Case.Pages.ActivityRequestDeleteController))]

[assembly: NavigationMenu(7000, "Administration", icon: "icon-screen-desktop")]
[assembly: NavigationLink(7100, "Administration/Languages", typeof(Administration.LanguageController), icon: "icon-bubbles")]
[assembly: NavigationLink(7200, "Administration/Translations", typeof(Administration.TranslationController), icon: "icon-speech")]
[assembly: NavigationLink(7300, "Administration/Roles", typeof(Administration.RoleController), icon: "icon-lock")]
[assembly: NavigationLink(7400, "Administration/User Management", typeof(Administration.UserController), icon: "icon-people")]
[assembly: NavigationLink(7500, "Administration/Log", typeof(CaseManagement.Administration.Pages.LogController))]

[assembly: NavigationLink(8000, "جریان کاری/مرحله", typeof(WorkFlow.WorkFlowStepController))]
[assembly: NavigationLink(8001, "جریان کاری/وضعیت", typeof(WorkFlow.WorkFlowStatusController))]
[assembly: NavigationLink(8002, "جریان کاری/جریان کاری", typeof(WorkFlow.WorkFlowRuleController))]

[assembly: NavigationMenu(9000, "گزارش")]
[assembly: NavigationLink(9001, "گزارش/گزارش برنامه استان ها", typeof(Case.ProvinceProgramController))]
[assembly: NavigationMenu(9100, "گزارش/گزارش های تجمیعی")]
[assembly: NavigationLink(9101, "گزارش/گزارش های تجمیعی/تجمیعی استان ها", typeof(Report.StimulReportPageController), action: "ProvinceReport")]
[assembly: NavigationLink(9102, "گزارش/گزارش های تجمیعی/تجمیعی فعالیت ها ", typeof(Report.StimulReportPageController), action: "ActivityReport")]
[assembly: NavigationLink(9103, "گزارش/گزارش های تجمیعی/تجمیعی جریان های درآمدی ", typeof(Report.StimulReportPageController), action: "IncomFlowReport")]
[assembly: NavigationLink(9006, "گزارش/گزارش فعالیت استان ها", typeof(Report.StimulReportPageController), action: "ProvinceActivityReport")]
[assembly: NavigationLink(9007, "گزارش/آمار  ", typeof(CaseManagement.StimulReport.Pages.ActivityRequestReportController))]
[assembly: NavigationLink(9008, " پیام کوتاه ", typeof(CaseManagement.Case.Pages.SMSLogController), icon: "icon-bell")]


[assembly: NavigationLink(9005, "گزارش/گزارش ریز فعالیت استان ها", typeof(CaseManagement.StimulReport.Pages.ActivityRequestDetailController))]
[assembly: NavigationLink(9006, "گزارش/نمودار میله ای استان ها", typeof(Report.StimulReportPageController), action: "ProvinceLineChart")]

[assembly: NavigationMenu(9200, "گزارش/ریزفعالیت کاربران")]

//[assembly: NavigationLink(9201, "گزارش/ریزفعالیت کاربران/فعالیت کاربران بر اساس زمان", typeof(Report.StimulReportPageController), action: "UserMonthActivityDetail")]
[assembly: NavigationLink(9202, "گزارش/ریزفعالیت کاربران/فعالیت کاربران بر اساس استان", typeof(Report.StimulReportPageController), action: "UserProvinceActivityDetail")]
[assembly: NavigationLink(9203, "گزارش/ریزفعالیت کاربران/فعالیت کاربران بر اساس سرگروه ها", typeof(Report.StimulReportPageController), action: "UserLeaderActivityDetail")]

//[assembly: NavigationMenu(9300, "گزارش/نمودار")]
//[assembly: NavigationLink(9301, "گزارش/نمودار/نمودار دایره ای", typeof(Report.ChartsController))]

//[assembly: NavigationLink(9006, "گزارش/گزارش تست", typeof(Report.ReportController))]

[assembly: NavigationLink(10000, " گزارش ساز", typeof(Report.StimulReportPageController), action: "Reporter")]

[assembly: NavigationMenu(10000, "تالار گفتگو")]
[assembly: NavigationLink(10001, "تالار گفتگو/تالار گفتگو", typeof(CasePage.CommonPageController), action: "NotificationUserGroupsList", icon: "icon-speech")]

[assembly: NavigationMenu(20000, "پیام ها")]
[assembly: NavigationLink(20001, "پیام ها/پیام جدید", typeof(CaseManagement.Messaging.Pages.NewMessageController))]
[assembly: NavigationLink(20002, "پیام ها/دریافت شده", typeof(CaseManagement.Messaging.Pages.InboxController))]
[assembly: NavigationLink(20003, "پیام ها/ارسال شده", typeof(CaseManagement.Messaging.Pages.SentController))]

[assembly: NavigationMenu(10000, "راهنما")]
[assembly: NavigationLink(10001, "راهنما/راهنما", typeof(CasePage.CommonPageController), action: "Document", icon: "icon-notebook")]

[assembly: NavigationLink(30001, "داشبورد مدیریتی 96", typeof(CasePage.CommonPageController), action: "Dashboard96", icon: "icon-speedometer")]
[assembly: NavigationLink(30002, "داشبورد 96", typeof(CasePage.CommonPageController), action: "DashboardUser96", icon: "icon-speedometer")]


