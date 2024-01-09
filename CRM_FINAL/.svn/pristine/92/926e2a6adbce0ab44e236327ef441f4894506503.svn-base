using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CRM.Data
{
    public static partial class DB
    {
        //TODO:rad
        public enum ReportSortingType
        {
            [Description("تعداد مرکزی")]
            CabinetInputCount = 1,

            [Description("تعداد پست")]
            PostCount
        }

        //TODO:rad
        public enum RequestCompletionStatus
        {
            [Description("درخواست تمام شده")]
            Complete = 1,

            [Description("درخواست تمام نشده")]
            Incomplete,

            [Description("همه")]
            All
        }

        public enum RoundType : int
        {
            [Description("الماس")]
            Diamond = 0,

            [Description("طلا")]
            Gold = 1,

            [Description("نقره")]
            Silver = 2,

            [Description("سفارشی")]
            Express = 3,

            [Description("رند قدیم")]
            Old = 4,

            [Description("رند استانی")]
            Provincial = 5
        }

        public enum CutAndEstablishStatus : byte
        {
            [Description("قطع")]
            Cut = 1,

            [Description("وصل")]
            Establish = 2
        }

        public enum SettingKeys
        {
            [Description("City")]
            City,

            [Description("RequestValidTime")]
            RequestValidTime,

            [Description("ApplyCabinetShare")]
            ApplyCabinetShare,

            [Description("ADSLPAPRequestDebt")]
            ADSLPAPRequestDebt,
        }

        public enum DocumentType : int
        {
            [Description("مدرک")]
            Document = 1,

            [Description("مجوز")]
            Permission = 2,

            [Description("قرارداد")]
            Contract = 3,
        }

        public enum ReasonReinstall : byte
        {
            [Description("درخواست اپراتور")]
            Operator = 1,

            [Description("درخواست مشترک")]
            Customer = 2

        }
        public enum RequestStatusType : byte
        {
            [Description("در دست اقدام")]
            Pending = 0,

            [Description("انجام شد")]
            Completed = 1,

            [Description("بروز مشکل")]
            Problem = 2,

            [Description("تایید")]
            Confirm = 3,

            [Description("اعلام نظر")]
            Feedback = 4,

            [Description("مشاهده")]
            Observation = 5,

            [Description("اعمال تغییرات")]
            Changes = 6,

            [Description("سلب امتیاز")]
            DisclaimerRating = 9,

            [Description("مرحله آغازین")]
            Start = 10,

            [Description("مرحله نهایی")]
            End = 11,

            [Description("بازدید از محل")]
            VisitPlaces = 12,

            [Description("خارج از مرز")]
            OutBound = 13,

            [Description("ورود به لیست انتظار")]
            WaitingList = 15,

            [Description("تغییر مرکز")]
            ChangeCenter = 16,

            [Description("تغییر مکان خودی")]
            ChangeTheLocationItself = 17,

            [Description("دریافت مدارک و هزینه")]
            GetCosts = 18,

            [Description("بازگشتی")]
            Recursive = 19,

            [Description("بررسی کالا و خدمات مخابرات")]
            TelecomminucationServicePaymentChecking = 20
        }

        public enum RequestStatusTypeExchangePost : int
        {
            [Description("در دست اقدام")]
            Pending = 2,

            [Description("اصلاحیه")]
            Amendment = 52
        }

        public enum ADSLPaymentType : byte
        {
            [Description("رایگان")]
            Free = 0,

            [Description("پیش پرداخت (PrePaid)")]
            PrePaid = 1,

            [Description("پس پرداخت (PostPaid)")]
            PostPaid = 3,
        }

        public enum ADSLPaymentTypeWithoutFree : byte
        {

            [Description("پیش پرداخت (PrePaid)")]
            PrePaid = 1,

            [Description("پس پرداخت (PostPaid)")]
            PostPaid = 3,
        }

        public enum PersonType : int
        {
            [Description("حقیقی")]
            Person = 0,

            [Description("حقوقی")]
            Company = 1,

            [Description("هردو")]
            Both = 2,

            [Description("نامشخص")]
            Nothing = 3
        }

        public enum Gender : int
        {
            [Description("مرد")]
            Male = 0,

            [Description("زن")]
            Female = 1
        }
        public enum TelephoneType : int
        {
            [Description("موقت")]
            Temporary = 205
        }


        public enum PossessionType : int
        {
            [Description("دائمی")]
            Normal = 0,

            [Description("موقت")]
            PartTimeRented = 1,

            [Description("اعتباری")]
            FullTimeRented = 2
        }

        public enum OrderType : int
        {
            [Description("عادی")]
            Normal = 0,

            [Description("خارج از نوبت")]
            OutOfOrder = 1
        }

        public enum Use3PercentType : int
        {
            [Description("کافو پست")]
            CabinetPost = 0
        }

        public enum AddressType : int
        {
            [Description("مکاتبه")]
            Contact = 0,

            [Description("نصب")]
            Install = 1
        }

        public enum InvestigateType : int
        {
            [Description("امکانات فنی")]
            TechnicalFacilities = 0,

            [Description("سرویس ویژه")]
            SpecialService = 1
        }

        public enum ThreePercentQuotaType : int
        {
            [Description("پست و کافو")]
            PostKafu = 0,

            [Description("اونو")]
            ONU = 1
        }

        public enum DeploymentType : int
        {
            [Description("عادي در محل")]
            NormalInPlase = 0,

            [Description("دور شوال")]
            PBX = 1,

            [Description("ميهمان و ميزبان")]
            GuestAndHost = 2,

            [Description("متمركز كننده")]
            Concentrator = 3
        }

        public enum Status : int
        {
            //[Description("در حال نصب")]
            //Installation = 0,

            //[Description("معرفی شده جهت نصب")]
            //IntroducedForInstallation = 1,

            [Description("بدون اتصال به كابل")]
            NonConnection = 0,

            [Description("تخصيص پي سي ام")]
            AllocationPCM = 1,

            [Description("خراب موقت ( تعويض بوخت)")]
            TemporarilyBroken = 2,

            [Description("خراب دائم")]
            PermanentBroken = 3,

            [Description("آزاد")]
            Free = 4,

            [Description("رزرو قسمتي از امكانات")]
            Booking = 5,

            [Description("رزرو كامل")]
            FullBooking = 6,

            [Description("در حال سيم بندي")]
            Wiring = 7,

            [Description("تخصيص يافته")]
            Allocation = 8,

            [Description("در حال سيم بندي تخليه")]
            WiringDischarging = 9
        }

        public enum WorkUnitResponsible : byte
        {
            [Description("ام دی اف")]
            MDF = 0,

            [Description("سوئیچ")]
            Switch = 1
        }

        public enum SourceType : byte
        {
            //وع منبع  تامین امکانات : صفر کافو-پست، یک کابل ویژه ، دو کابل اختصاصی ، سه اونو
            [Description("کافو-پست")]
            Post = 0,

            [Description("کابل ویژه")]
            SpecialCables = 1,

            [Description("کابل اختصاصی")]
            DedicatedCable = 2,

            [Description("اونو")]
            ONU = 3,

            [Description("پی سی ام")]
            PCM = 5
        }

        public enum GroupType : int
        {
            //CV-3 پست معمولي     CH-2 سركابل    SH-1 سر كابل ويژه   RS-0 رزرو
            [Description("سرکابل(20زوجی)")]
            CableConnector20 = 1,

            [Description("سرکابل(30زوجی)")]
            CableConnector30 = 2,

            [Description("سرکابل(40زوجی)")]
            CableConnector40 = 3,

            [Description("سرکابل(50زوجی)")]
            CableConnector50 = 4,

            [Description("سرکابل(70زوجی)")]
            CableConnector70 = 5,

            [Description("سرکابل(100زوجی)")]
            CableConnector100 = 6,

            [Description("سرکابل ویژه(20زوجی)")]
            SpacialCableConnector20 = 7,

            [Description("سرکابل ویژه(30زوجی)")]
            SpacialCableConnector30 = 8,

            [Description("سرکابل ویژه(40زوجی)")]
            SpacialCableConnector40 = 9,

            [Description("سرکابل ویژه(50زوجی)")]
            SpacialCableConnector50 = 10,

            [Description("سرکابل ویژه(70زوجی)")]
            SpacialCableConnector70 = 11,

            [Description("سرکابل ویژه(100زوجی)")]
            SpacialCableConnector100 = 12,

            [Description("سرکابل ویژه 100 به بالا")]
            SpacialCableUpTo100 = 13,

            [Description("معمولی")]
            Normal = 14,
        }

        public enum CableUsedChannel : int
        {
            [Description("کافو نوری")]
            OpticalCabinet = 8,
        }

        public enum CableType : int
        {
            [Description("کافو نوری")]
            OpticalCabinet = 8,
        }

        public enum TrafficTypeCode : int
        {
            [Description("پرترافیک")]
            HighTraffic = 0,

            [Description("کم ترافیک")]
            LowTraffic = 1,

            [Description("عادی")]
            Normal = 2,

            [Description("نامحدود")]
            Unlimeted = 3
        }

        public enum CabinetUsageType : int
        {
            [Description("WLL")]
            WLL = 1,

            [Description("بدون کافو")]
            WithoutCabinet = 2,

            [Description("معمولی")]
            Normal = 3,

            [Description("کافو نوری")]
            OpticalCabinet = 4,

            [Description("کابل اختصاصی")]
            Cable = 5,
        }

        public enum SwitchTypeCode : int
        {
            [Description("سوئیچ ثابت")]
            FixedSwitch = 0,

            [Description("اونوی AB/Wire")]
            ONUABWire = 1,

            [Description("اونوی V5")]
            ONUVWire = 2,

            [Description("اونوی مسی")]
            ONUCopper = 3,

            [Description("WLL")]
            WLL = 4
        }

        public enum UsageType : int
        {
            [Description("نوع 1")]
            Type1 = 0,

            [Description("نوع 2")]
            Type2 = 1
        }

        public enum StatusType : int
        {
            [Description("بدون اتصال")]
            WithoutContact = 0,

            [Description("تخصيص پي سي ام")]
            AllocationPMC = 1,

            [Description("خراب موقت")]
            TemporarilyBroken = 2
        }

        public enum WorkUnit : byte
        {
            [Description("امور مشترکین")]
            Customer = 0,

            [Description("واگدازی خطوط")]
            Transferlines = 1,

            [Description("واگذاری مدارات")]
            TransferCircuits = 2
        }

        public enum IsActive : byte
        {
            [Description("فعال")]
            Active = 0,

            [Description("غیر فعال")]
            NonActive = 1
        }

        public enum ReservationStatus : int
        {
            [Description("ثبت در دفتر حق تقدم")]
            RegisterInPrecedincyBook = 1,

            [Description("رزرو موقت")]
            TemReserve = 2,

            [Description("ذخیره موقت کلیه تجهیزات")]
            temSaveEquipments = 3,

            [Description("ذخیره جهت جایگزینی")]
            SaveForSituation = 4,

            [Description("تخلیه شده")]
            Discharged = 5
        }

        public enum CabinetStatus : byte
        {
            [Description("دایر")]
            Install = 0,

            [Description("در وضعیت برگردان")]
            ExchangeCabinetInput = 1
        }

        public enum BuchtStatus : int
        {
            [Description("آزاد")]
            Free = 0,

            [Description("متصل")]
            Connection = 1,

            [Description("خراب")]
            Destroy = 2,

            [Description("رزرو")]
            Reserve = 3,

            [Description("در حال سيم بندي")]
            Wiring = 5,

            [Description("تخصیص یافته")]
            Allocated = 6,

            [Description("اتصال به پی سی ام")]
            ConnectedToPCM = 7, // status 1 in bucht_base Elka

            [Description("رزرو قسمتی از امکانات")]
            PossibilitiesReservation = 8,

            [Description("بدون اتصال دائم")]
            LackOfConnectedToPCM = 9,

            [Description("در حال برگردان مرکزی ام دی اف")]
            ExchangeCentralCableMDF = 11,

            [Description("تخصیص یافته به خروجی پی سی ام")]
            AllocatedToInlinePCM = 13,

            [Description("ای دی اس ال متصل")]
            ADSLConnection = 14,

            [Description("ای دی اس ال آزاد")]
            ADSLFree = 15,

            [Description("ای دی اس ال قطع موقت")]
            ADSLTemporaryDisconnected = 16,

            [Description("متصل به اسپیلتر")]
            ConnectedToSpliter = 17,

            [Description("برگردان مرکز به مرکز")]
            ExchangeCenterToCenter = 18,

            [Description("برگردان اتصالی پست")]
            ExchangePostContact = 21,

        }

        public enum ServiceType : int
        {
            [Description("اینترنت")]
            Internet = 0,

            [Description("اینترانت")]
            Intranet = 1,

            [Description("اینترانت PPPOE استاتیک")]
            StaticPPPOEIntranet = 2
        }
        public enum TypeOfConnectedBuchtADSL : int
        {
            [Description("منظم")]
            regular = 0,

            [Description("متناوب")]
            Intermittent = 1,

            [Description("نامنظم")]
            Irregular = 2
        }
        public enum MDFUses : int
        {
            [Description("معمولی")]
            Normal = 1,

            [Description("پی سی ام")]
            PCM = 2,

            [Description("ای دی اس ال")]
            ADSL = 3
        }

        public enum StatusSpecialService : int
        {
            [Description("درخواست ایجاد")]
            RequestInstal = 1,

            [Description("درخواست حذف")]
            RequestUnInstal = 2,

            [Description("ایجاد")]
            Instal = 3,

            [Description("حذف")]
            UnInstal = 4,

            [Description("آرشیو")]
            ArchiveInstal = 5,

            [Description("حذف از آرشیو")]
            ArchiveUnInstal = 6
        }

        public enum EntryReasonID : int
        {
            [Description("نبود امکانات آبونه کافی")]
            SubscribeLackSufficient = 0,

            [Description("نبود امکانات  مرکزی کافی")]
            CenterLackSufficient = 1
        }

        public enum StatusWatingList : int
        {
            [Description("فعال")]
            Active = 0,

            [Description("غیر فعال")]
            NonActive = 1
        }

        public enum Form : byte
        {
            [Description("درخواست")]
            Install = 1,

            [Description("امکان سنجی")]
            Investigation = 2,

            [Description("تعیین شماره")]
            ChooseNumber = 3,

            [Description("صدور فرم سیم بندی")]
            IssueWiring = 4,

            [Description("سیم بندی شبکه هوایی")]
            Wiring = 5,

            [Description("سیم بندی ام دی اف")]
            MDFWiring = 6,

            [Description("دایری")]
            Dayeri = 7,

            [Description("مکالمه سه نفری")]
            ThreeWay = 10,

            [Description("برگردان پست")]
            ExchangPost = 11,

            [Description("برگردان مرکزی ام دی ام")]
            ExchangeCentralCableMDF = 12,

            [Description("برگردان ورودی های جعبه توزیع")]
            ExchangeCenralCableCabinet = 13,

            [Description("سیم بان")]
            LinsemanForm = 14,

            [Description("واگذاری")]
            Assignment = 15,

            [Description("سالن(دایری)")]
            countor = 16,

            [Description("ام دی اف")]
            MDF = 17,

            [Description("امور مشترکین (استرداد ودیعه تلفن ثابت)")]
            ThingsOfCustomer = 18,

            [Description("تعیین پست اصلی")]
            DeterminedPostForm = 19,

            [Description("تغییر نام")]
            ChangeName = 20,

            [Description("استعلام")]
            Inquiry = 21,

            [Description("قطع و وصل")]
            CutAndEstablish = 22,

            [Description("تایید تغییر مکان مرکز به مرکز")]
            ConfirmChangeLocationCenterToCenter = 23,

            [Description("سرویس ویژه")]
            SpecialService = 24,

            [Description("واگذاری خطوط ADSL")]
            ADSLAssignmentLines = 25,

            [Description("MDF برای ADSL")]
            ADSLMDF = 26,

            [Description("OMC برای ADSL")]
            ADSLOMC = 27,

            [Description("نصب و راه اندازی ADSL")]
            ADSLSetup = 28,

            [Description("ADSL شرکت های PAP")]
            ADSLPAPCompany = 29,

            [Description("واگذاری خطوط")]
            AssingmentLines = 30,

            [Description("امور مشترکین")]
            TaskOfCustomer = 31,

            [Description("تغییر پیش شماره")]
            ChangePreCode = 32,

            [Description("فضا و پاور")]
            SpaceAndPower = 33,

            [Description("خرابی 117")]
            Failure117 = 34,

            [Description("فرم خرابی شبکه")]
            Failure117Network = 35,

            [Description("بازدید از محل")]
            VisitPlaces = 36,

            [Description("امور مشترکین دایری")]
            Subscribers = 37,

            [Description("اداره سوئیچ")]
            ChooseNumberE1 = 38,

            [Description("اداره پشتیبانی فنی")]
            TechnicalSupportDepartment = 39,

            [Description("پشتیبانی فنی")]
            TechnicalSupport = 40,

            [Description("سالن E1")]
            SwitchE1 = 41,

            [Description("ماکروویو")]
            E1MicrowavesForm = 42,

            [Description("تایید مشترک بدهکار")]
            CustomerToApproveDebtorForm = 43,

            [Description("پشتیبانی ADSL")]
            ADSLSupport = 44,

            [Description("درخواست برگردان مرکز به مرکز")]
            CenterToCenterRequestFormTranslation = 45,

            [Description("تعیین شماره برگردان مرکز به مرکز")]
            CenterToCenterTranslationChooseNumberForm = 46,

            [Description("سوئیچ برگردان مرکز به مرکز")]
            CenterToCenterTranslationSwitchForm = 47,

            [Description("ام دی اف برگردان مرکز به مرکز")]
            CenterToCenterTranslationMDFForm = 48,

            [Description("شبکه هوایی برگردان مرکز به مرکز")]
            CenterToCenterTranslatioNetworkForm = 49,

            [Description("ثبت درخواست برگردان پست")]
            TranslationPostForm = 50,

            [Description("شبکه هوایی برگردان پست")]
            TranslationPostNetworkForm = 51,

            [Description("واگذاری خطوط برگردان پست")]
            TranslationPostInvestigatePossibilityForm = 52,

            [Description("ام دی اف برگردان کافو")]
            TranslationCabinetMDFFrom = 53,

            [Description("شبکه هوایی برگردان کافو")]
            TranslationCabinetNetworkFrom = 54,

            [Description("واگذاری خطوط برگردان کافو")]
            TranslationCabinetInvestigateFrom = 55,

            [Description("ثبت درخواست برگردان مرکزی ام دی اف")]
            TranslationCentralCableMDFFrom = 56,

            [Description("ام دی اف برگردان مرکزی ام دی اف")]
            TranslationCentralCableMDFForMDFFrom = 57,

            [Description("شبکه هوایی برگردان مرکزی ام دی اف")]
            TranslationCentralCableMDFNetworkFrom = 58,

            [Description("واگذاری خطوط برگردان مرکزی ام دی اف")]
            TranslationCentralCableMDFInvestigateFrom = 59,

            [Description("برگردان مرکزی پست")]
            TranslationPostInputFom = 60,

            [Description("شبکه هوایی برگردان مرکزی پست")]
            TranslationPostInputNetworkForm = 61,

            [Description("ام دی اف برگردان مرکزی پست")]
            TranslationPostInputMDFForm = 62,

            [Description("واگذاری برگردان مرکزی پست")]
            TranslationPostInputInvestigateForm = 63,

            [Description("ثبت تعویض بوخت")]
            BuchtSwitchingForm = 64,

            [Description("ام دی اف تعویض بوخت")]
            BuchtSwitchingMDFForm = 65,

            [Description("شبکه هوایی تعویض بوخت")]
            BuchtSwitchingNetworkFrom = 66,

            [Description("درخواست برگردان نوری به معمولی")]
            TranslationOpticalToNormalForm = 67,

            [Description("تعیین شماره برگردان نوری به معمولی")]
            TranslationOpticalToNormalChooseNumberForm = 68,

            [Description("سوئیچ برگردان نوری به معمولی")]
            TranslationOpticalToNormalSwitchForm = 69,

            [Description("ام دی اف برگردان نوری به معمولی")]
            TranslationOpticalToNormalMDFForm = 70,

            [Description("شبکه هوایی برگردان نوری به معمولی")]
            TranslationOpticalToNormalNetworkForm = 71,

            [Description("بازپرداخت")]
            RequestRefundForm = 72,


            [Description("در خواست پی سی ام به معمولی")]
            TranslationPCMToNormalForm = 73,

            [Description("ام دی اف پی سی ام به معمولی")]
            TranslationPCMToNormalMDFForm = 74,

            [Description("شبکه هوایی پی سی ام به معمولی")]
            TranslationPCMToNormalNetworkForm = 75,

            [Description("ام دی اف تعویض تلفن")]
            SwapTelephoneMDFForm = 76,

            [Description("ثبت درخواست تعویض تلفن")]
            SwapTelephoneForm = 77,

            [Description("ثبت درخواست تعویض پی سی ام")]
            SwapPCMForm = 78,

            [Description("ام دی اف تعویض پی سی ام")]
            SwapPCMMDFForm = 79,

            [Description("شبکه هوایی تعویض پی سی ام")]
            SwapPCMNetworkForm = 80,


            [Description("سیم بندی ام دی اف گروهی")]
            WiringMDFGroupedForm = 81,

            [Description("سیم بندی شبکه گروهی")]
            WiringNetworkGroupedForm = 82,

            [Description("مدیر طراحی")]
            DesignDirectorForm = 83,

            [Description("اداره طراحی و انتقال")]
            TransferDepartmentForm = 84,

            [Description("اداره طراحی نیرو")]
            PowerDepartmentForm = 85,

            [Description("رئیس مرکز")]
            HeadCenterForm = 86,

            [Description("اداره نظارت")]
            MonitoringDepartmentForm = 87,

            [Description("اداره نصب")]
            InstallingDepartmentForm = 88,

            [Description("اداره ساختمان")]
            ConstructionOfficeForm = 89,

            [Description("معاونت شبکه")]
            NetworkAssistantForm = 90,

            [Description("عقد قرارداد")]
            AgreementContractForm = 91,

            [Description("سالن دستگاه")]
            DeviceHallForm = 92,

            [Description("اداره نظارت تجهیزات مخابراتی")]
            AdministrationOfTheTelecommunicationEquipmentForm = 93,

            [Description("صدور صورتحساب")]
            InvoiceIssuanceForm = 94,

            [Description("ثبت برگردان کارت پی سی ام")]
            ExchangePCMCardFrom = 95,

            [Description("ام دی اف برگردان کارت پی سی ام")]
            ExchangePCMCardMDFFrom = 96,

            [Description("تعیین شماره GSM")]
            ExchangeGSMChooseNumberFrom = 97,

            [Description("سالن سوئیچ GSM")]
            ExchangeGSMCounterForm = 98,

            [Description("ام دی اف GSM")]
            ExchangeGSMMDFForm = 99,

            [Description("شبکه هوایی GSM")]
            ExchangeGSMNetworkForm = 100,


            [Description("ثبت درخواست GSM")]
            ExchangeGSMForm = 101,

            [Description("اداره طراحی سوئیچ")]
            SwitchDesigningOfficeForm = 102
        }

        public enum Action : byte
        {
            [Description("تایید")]
            Confirm = 1,

            [Description("رد درخواست")]
            Reject = 2,

            [Description("حذف درخواست")]
            Delete = 3,

            [Description("ارجاع مشروط")]
            AutomaticForward = 4,

            [Description("لیست انتظار")]
            WaitingList = 5,

            [Description("ارجاع بازگشتی")]
            RecursionForward = 6,

            [Description("رد مشروط")]
            RejectConditional = 7
        }

        public enum NewAction : byte
        {
            [Description("ذخیره")]
            Save = 1,

            [Description("حذف")]
            Delete = 2,

            [Description("چاپ")]
            Print = 3,

            [Description("تایید")]
            Confirm = 4,

            [Description("رد")]
            Deny = 5,

            [Description("ذخیره و ارجاع")]
            Forward = 6,

            [Description("استرداد ودیعه")]
            Refund = 7,

            [Description("تایید نهایی")]
            ConfirmEnd = 8,

            [Description("ابطال")]
            Cancelation = 9,

            [Description("ذخیره در لیست انتظار")]
            SaveWaitingList = 10,

            [Description("خروج از لیست انتظار")]
            ExitWaitingList = 11,

            [Description("ذخیره در لیست سیاه")]
            SaveBlackList = 12,

            [Description("خروج از لیست سیاه")]
            ExitBlackList = 13,

            [Description("خروج")]
            Exit = 14,

            [Description("باز پرداخت")]
            KickedBack = 15

        }

        public enum ActionLog : byte
        {
            [Description("مشاهده")]
            View = 1,

            [Description("ذخیره")]
            Save = 2,

            [Description("حذف")]
            Delete = 3,

            [Description("تایید")]
            Confirm = 4,

            [Description("رد")]
            Reject = 5,

            [Description("ابطال")]
            Cancelation = 6,

            [Description("ذخیره در لیست انتظار")]
            SaveWaitingList = 7,

            [Description("خروج از لیست انتظار لیست انتظار")]
            ExitWaitingList = 8,

            [Description("چاپ")]
            Print = 9,

            [Description("ارجاع به مرحله بعد")]
            Forward = 10,

            [Description("ورود به سیستم")]
            Login = 11,

            [Description("خروج از سیستم")]
            Logout = 12,

            [Description("جمع آوری پی سی ام")]
            PCMDrop = 13,

            [Description("انتقال پی سی ام")]
            PCMTransfer = 14,

            [Description("ایجاد پی سی ام")]
            PCMCreate = 15,

            [Description("نصب پی سی ام")]
            PCMInstall = 16,

            [Description("خرابی اتصال پست")]
            PostContact = 17,

            [Description("خرابی زوج سیم")]
            CablePaired = 18,

            [Description("خالی شدن زوج کابل")]
            CablePairEmpty = 19,

            [Description("رزرو زوج کابل")]
            CablePairReserve = 20,

            [Description("خرابی زوج کابل")]
            CablePairFail = 21,

            [Description("پر شدن زوج کابل")]
            CablePairFill = 22,

            [Description("بازپرداخت")]
            KickedBack = 23,

            //milad doran
            //[Description("اصلاح پی سی ام")]
            //PCMEdit = 23,

            [Description("اصلاح مشخصات پی سی ام")]
            PCMEditInfo = 24,

            [Description("حذف پی سی ام")]
            PCMDelete = 25,

            [Description("استرداد ودیعه")]
            Refund = 26,

            [Description("اصلاح پی سی ام")]
            PCMEdit = 27,

            [Description("ویرایش نقش")]
            RoleEdit = 28

        }

        public enum WiringType : int
        {
            [Description("تخلیه")]
            Discharge = 0,

            [Description("دایری")]
            Open = 1,

            [Description("کابل برگردان")]
            CableBack = 2,

            [Description("تغییر مکان")]
            ChangeLocation = 3,

            [Description("تعویض شماره")]
            ChangeNo = 4,

            [Description("استرداد ودیعه تلفن ثابت")]
            RefundDeposit = 5,

            [Description("دایری مجدد")]
            Reinstall = 6,
            [Description("E1")]
            E1 = 7,
            [Description("E1(فیبر)")]
            E1Fiber = 8,
            [Description("سیم خصوصی")]
            PrivateWire = 9,

            [Description("تخلیه سیم خصوصی")]
            VacateSpecialWire = 10,

            [Description("تغییر مکان سیم خصوصی")]
            ChangeLocationSpecialWire = 11
        }

        public enum StatusIssueWiring : int
        {
            [Description("صادر شد")]
            Issued = 0,

            [Description("باطل شد")]
            Cancel = 1
        }

        public enum FicheType : int
        {
            [Description("قبض پيش پرداخت")]
            BillPayment = 0,

            [Description("قبوض اقساط پرداخت نشده")]
            InstallmentsOfUnpaidBills = 1
        }

        public enum BuchtType : int
        {
            [Description(" بوخت نوری")]
            OpticalBucht = 1,

            [Description("BTS")]
            BTS = 2,

            [Description("E1")]
            E1 = 3,

            [Description("ADSL")]
            ADSL = 4,

            [Description("dect")]
            dect = 5,

            [Description("کابل ارتباط")]
            CableConnect = 6,

            [Description("FX")]
            FX = 7,

            [Description("ورودی")]
            InLine = 8,

            [Description("خروجی")]
            OutLine = 9,

            [Description(" داخل مخابرات")]
            InsideContacts = 10,

            [Description("دیتا")]
            Data = 11,

            [Description("سيم خصوصي  ")]
            PrivateWire = 12,

            [Description("طرف مشترک")]
            CustomerSide = 13,

            [Description("کابل اختصاصی")]
            SpecificCable = 43,

            [Description("شبکه دولت ")]
            NetworkGovernment = 44,



        }

        public enum PostContactConnectionType : int
        {
            [Description("عادی")]
            Noraml = 3,

            [Description("ورودی پی سی ام")]
            PCMRemote = 4,

            [Description("خروجی پی سی ام")]
            PCMNormal = 5
        }

        public enum RequiredConnection
        {
            [Description("غیر متصل های به کابل")]
            UnConnectToCable = 1,

            [Description("همه بجز پی سی ام")]
            AllExceptPCM = 1,
        }

        public enum PostStatus : int
        {
            [Description("فعال")]
            Dayer = 1,

            [Description("غیر فعال")]
            Broken = 2,

            [Description("رزرو برای برگردان")]
            ReserveForExchange = 3

        }

        public enum AORBPostAndCabinet : int
        {
            [Description("A/B")]
            AORB = 1,

            [Description("A")]
            A = 2,

            [Description("B")]
            B = 3
        }

        public enum PostContactStatus : int
        {
            [Description("بدون اتصال")]
            NoCableConnection = 0,

            [Description("متصل")]

            CableConnection = 1,

            //[Description("پي سي ام")]

            //PCMAllocation = 2, // استفاده نشده

            //[Description("خراب موقت")]
            //TemporarilyBroken = 3, // در اینپورت استفاده نکردم

            [Description("خراب")]
            PermanentBroken = 4,

            [Description("آزاد")]
            Free = 5,

            //[Description("رزرو قسمتي از امكانات")]
            //ReserveSomeOfTheFacilities = 6, // در اینپورت استفاده نکردم

            [Description("رزرو")]
            FullBooking = 7, // در تغییر مکان استفاده شده

            //[Description("در حال سيم بندي")]
            //TheWiring = 8, // استفاده نشده

            //[Description("تخصيص يافته")]
            //Allocated = 9, // استفاده نشده

            //[Description("در حال برگردان مرکزی ام دی اف")]
            //ExchangeCentralCableMDF = 11, // معادل در حال تعویض بوخت الکا قرار گرفت

            //[Description("برگردان مرکز به مرکز")]
            //ExchangeCenterToCenter = 12, // معادل برگردان مرکز به مرکز الکا قرار گرفت

            //[Description("در حال برگردان پست")]
            //ExchangePost = 10, // در برگردان از این گزینه استفاده نکردم

            //[Description("رزرو برای برگردان پست")]
            //ReserveExchangePost = 13,

            [Description("حذف")]
            Deleted = 14
        }

        public enum MDFType : int
        {
            [Description("ثابت (V) یا AB/Wire")]
            Fix = 0,

            [Description("اونو( C)")]
            ONU = 1
        }

        public enum CableStatus : int
        {
            [Description("نصب")]
            CableConnection = 0,

            [Description("خراب")]
            Destruction = 1,

            [Description("برگردان")]
            Exchange = 2,
        }

        public enum CablePairStatus : byte
        {
            [Description("وصل به بوخت")]
            ConnectedToBucht = 1,

            [Description("آزاد")]
            Free = 2,

            [Description("خراب")]
            Destruction = 3,

            [Description("در حال برگردان مرکزی ام دی اف")]
            ExchangeCentralCableMDF = 4,

            [Description("در حال برگردان ورودی کافو")]
            ExchangeCenralCableCabinet = 5,

            [Description("برگردان مرکز به مرکز")]
            ExchangeCenterToCenter = 6
        }

        public enum StatusPCMDevice : byte
        {
            [Description("نصب")]
            Connected = 0,

            [Description("دایری")]
            Dayeri = 1,

            [Description("جمع آوری")]
            Collect = 2
        }

        public enum PCMStatus : byte
        {
            [Description("نصب")]
            Install = 1,

            [Description("دایر")]
            Connection = 2,

            [Description("خراب")]
            Destruction = 3,

            [Description("رزرو برگردان")]
            Reserve = 4,
            // برای زمانی که ChekablaItem همه را برگردان استفاده میشود
            [Description("لیست همه")]
            All = 255,
        }

        public enum PCMPortStatus : byte
        {

            [Description("خالی")]
            Empty = 1, // 1 elka

            [Description("پر")]
            Connection = 2, // 2 elka

            [Description("خراب")]
            Malfaction = 3, // 3 elka

            [Description("رزرو")]
            Reserve = 4, // 4 elka
        }
        public enum selectAll : int
        {
            [Description("کلی")]
            All = 255
        }
        public enum ChannelType : int
        {
            [Description("پی سی ام")]
            PCM = 5
        }

        public enum SwitchSpecialServicesStatus : byte
        {
            [Description("فعال")]
            Active = 0,

            [Description("غیر فعال")]
            InActive = 1,

            [Description("در حال جمع اوری")]
            Collecting = 2
        }

        public enum PreCodeType : byte
        {
            [Description("عادی")]
            Normall = 1,

            [Description("همگانی")]
            General = 2,

            [Description("موقت")]
            Temporary = 3
        }

        public enum SwitchPrecodeDeploymentType : byte
        {
            [Description("در محل")]
            Local = 0,

            [Description("مهمان")]
            Guest = 1,

            [Description("میزبان")]
            Host = 2,

            [Description("متمرکز کننده")]
            Concentrator = 3
        }

        public enum DorshoalNumberType : int
        {
            [Description("ورودی")]
            Input = 0,

            [Description("خروجی")]
            Output = 1,

            [Description("ورودی خروجی")]
            InputOutput = 2
        }

        public enum SwitchPreCodeStatus : byte
        {
            [Description("فعال")]
            Active = 0,

            [Description("غیر فعال")]
            Reserve = 1,

            [Description("در حال جمع اوری")]
            Collecting = 2,

            [Description("در حال تعویض پیش شماره")]
            changePreCode = 3
        }

        public enum TelephoneStatus : byte
        {
            [Description("آزاد")]
            Free = 0,

            [Description("رزرو")]
            Reserv = 1,

            [Description("دایری")]
            Connecting = 2,

            [Description("قطع")]
            Cut = 3,

            [Description("در حال تغییر مکان")]
            ChangingLocation = 4,

            [Description("تخلیه")]
            Discharge = 5,

            [Description("خراب")]
            Destruction = 6,

            [Description("جمع آوری منصوبات")]
            CollectingEquipment = 7
        }

        public enum InquiryStatus : byte
        {
            [Description("تایید")]
            Accept = 1,

            [Description("رد")]
            Deny = 2
        }

        public enum RequestStep : int
        {

            [Description("صدور فرم سیم بندی برگردان پست")]
            WiringOfExchangePost = 39,

            [Description("خاتمه برگردان پست")]
            EndOfExchangePost = 40,

            [Description("صدور فرم سیم بندی برگردان کابل مرکزی در ام دی ام")]
            WiringOfExchangeCenralCableMDF = 42,

            [Description("خاتمه برگردان کابل مرکزی در ام دی ام")]
            EndOfExchangeCenralCableMDF = 43,

            [Description("صدور فرم سیم بندی برگردان ورودی های جعبه توزیع")]
            WiringOfExchangeInputCabinet = 45,

            [Description("خاتمه برگردان ورودی های جعبه توزیع")]
            EndOfExchangeInputCabinet = 46,

            [Description("سیم بان")]
            linesmane = 48,

            [Description("واگذاری")]
            Assignment = 49,

            [Description("ثبت درخواست تغییر نام")]
            ChangeNameRequest = 54,

            [Description("استعلام")]
            Inquiry = 55,

            [Description("تکمیل درخواست تغییر نام")]
            EndofChangeNameRequest = 57,

            [Description("درخواست قطع و وصل")]
            CutAndEstablishRequest = 67,

            [Description("ثبت درخواست")]
            ADSL_Request = 95,

            [Description("امور مشترکین")]
            ADSL_TaskOfCustomer = 97,

            [Description("واگذاری خطوط")]
            ADSL_Assignment = 98,

            [Description("رییس مرکز")]
            ADSL_CenterManager = 99,

            [Description("MDF")]
            ADSL_MDF = 100,

            [Description("OMC")]
            ADSL_OMC = 101,

            [Description("واحد نصب و راه اندازی")]
            ADSL_Setup = 102,

            [Description("ناظر امور مشترکین")]
            ADSL_TaskOfCustomerManager = 103
        }

        public enum Equipment : int
        {
            [Description("استفاده از تجهیزات عمومی")]
            PublicEquipment = 0,

            [Description("استفاده از کابل اختصاصی")]
            DedicatedCable = 1
        }

        public enum Statuses
        {
            [Description("واگذاری به سیم بان")]
            AssingmentToLinesman = 117,

            [Description("ثبت درخواست سیم بان")]
            LinemansRegistrationRequest = 114,

            [Description("ام دی اف تغییر مکان")]
            ChangeLocationMDF = 121,

            [Description("تعیین شماره ندارد")]
            NoChooseNumber = 142,

            [Description("تعیین شماره دارد")]
            ChooseNumber = 144,

            [Description("کاربر واگذاری")]
            Assignment = 119,
            [Description("تعیین شماره به واگذاری")]
            ChooseNoToAssignment = 126,

            [Description("تایید تغییر مکان مرکز به مرکز")]
            ConfirmChangeLocationCenterToCenter = 157,

            [Description("دایری(سالن)")]
            salon = 120,


        }

        public enum RequestType : byte
        {
            [Description("دايری")]
            Dayri = 1,

            [Description("افزایش پهنای باند")]
            IncreaseBandwidth = 3,

            [Description("کاهش پهنای باند")]
            DecrementBandwidth = 4,

            [Description("سرویس های ویژه")]
            SpecialService = 7,

            [Description("تغییر مکان داخل مرکز")]
            ChangeLocationCenterInside = 25,

            [Description("تغییر مکان مرکز به مرکز")]
            ChangeLocationCenterToCenter = 63,

            [Description("تغییر نام")]
            ChangeName = 28,

            [Description("قطع")]
            CutAndEstablish = 30,

            [Description("تخلیه")]
            Dischargin = 32,

            [Description("انسداد صفر")]
            OpenAndCloseZero = 34,

            [Description("ADSL")]
            ADSL = 35,

            [Description("خرید شارژ مجدد ADSL")]
            ADSLChangeService = 38,

            [Description("قطع و وصل موقت ADSL")]
            ADSLCutTemporary = 39,

            [Description("تخلیه ADSL")]
            ADSLDischarge = 43,

            [Description("تعویض پورت ADSL")]
            ADSLChangePort = 45,

            [Description("PAP - دایری ADSL")]
            ADSLInstalPAPCompany = 46,

            [Description("تعویض شماره")]
            ChangeNo = 47,

            [Description("استرداد ودیعه تلفن ثابت")]
            RefundDeposit = 50,

            [Description("دایری مجدد")]
            Reinstall = 53,

            [Description("ثبت عنوان در 118 ")]
            TitleIn118 = 54,

            [Description("اصلاح آدرس")]
            ChangeAddress = 56,

            [Description("برگردان پست")]
            ExchangePost = 10,

            [Description("برگردان کافو")]
            ExchangeCabinetInput = 22,

            [Description("برگردان مرکزی ام دی اف")]
            ExchangeCenralCableMDF = 21,

            [Description("PAP - تخلیه ADSL")]
            ADSLDischargePAPCompany = 57,

            [Description("تعویض پیش شماره")]
            ChangePreCode = 59,

            [Description("فضا و پاور")]
            SpaceandPower = 60,

            [Description("خرابی 117")]
            Failure117 = 65,

            [Description("PAP - تعویض پورت")]
            ADSLExchangePAPCompany = 67,

            [Description("E1(سیم)")]
            E1 = 71,

            [Description("E1(فیبر)")]
            E1Fiber = 73,

            [Description("سیم خصوصی")]
            SpecialWire = 74,

            [Description("تغییر IP استاتیک")]
            ADSLChangeIP = 76,

            [Description("تخلیه سیم خصوصی")]
            VacateSpecialWire = 77,

            [Description("تغییر مکان سیم خصوصی")]
            ChangeLocationSpecialWire = 78,

            [Description("پشتیبانی ADSl")]
            ADSLSupport = 79,

            [Description("نصب ADSl")]
            ADSLInstall = 80,

            [Description("برگردان مرکز به مرکز")]
            CenterToCenterTranslation = 82,

            [Description("خرید ترافیک")]
            ADSLSellTraffic = 83,

            [Description("برگردان پست با تغییر مرکزی")]
            ExchangePostWithChangeCabintInput = 84,

            [Description("تعویض شماره  ADSL")]
            ADSLChangePlace = 85,

            [Description("برگردان مرکزی پست")]
            TranlationPostInput = 88,

            [Description("تعویض بوخت")]
            BuchtSwiching = 90,

            [Description("سیم خصوصی نقاط دیگر")]
            SpecialWireOtherPoint = 91,

            [Description("PBX")]
            PBX = 92,

            [Description("لینک E1")]
            E1Link = 93,

            [Description("تخلیه(سیم)")]
            VacateE1 = 94,

            [Description("تغییر مشخصات مالک ADSL")]
            ADSLChangeCustomerOwnerCharacteristics = 95,

            [Description("وصل")]
            Connect = 96,

            [Description("حذف عنوان در 118 ")]
            RemoveTitleIn118 = 97,

            [Description("تغییر عنوان در 118 ")]
            ChangeTitleIn118 = 98,

            [Description("برگردان کافو نوری به عادی")]
            TranslationOpticalCabinetToNormal = 99,

            [Description("راه اندازی ارتباط دیتا ")]
            DataInstaltion = 100,

            [Description("پی سی ام به معمولی ")]
            PCMToNormal = 101,

            [Description("اصلاح مشخصات")]
            ModifyProfile = 102,

            [Description("تعویض تلفن")]
            SwapTelephone = 103,

            [Description("تعویض پی سی ام")]
            SwapPCM = 104,

            [Description("ویرایش دایری تلفن")]
            EditTelephoneInstallation = 107,

            [Description("دایری پی سی ام")]
            PCMInstallation = 108,

            [Description("حذف پی سی ام")]
            DeletePCMInstallation = 109,


            [Description("Wireless")]
            Wireless = 111,

            [Description("ویرایش مشخصات مشترک")]
            EditCustomer = 112,

            [Description("ویرایش مشخصات آدرس")]
            EditAddress = 113,

            [Description("ویرایش مشخصات تلفن")]
            EditTelephone = 114,

            [Description("خرابی کارت PCM")]
            BrokenPCM = 115,

            [Description("خرید ترافیک")]
            WirelessSellTraffic = 116,

            [Description("شارژ مجدد")]
            WirelessChangeService = 117,

            [Description("تغییر وضعیت رند بودن یک تلفن")]
            ChangeTelephoneRound = 118,

            [Description("بازدید از محل بر اساس تلفن")]
            TelephoneVisitAddress = 119,

            [Description("برگردان GSM")]
            ExchangeGSM = 120,

            [Description("فضاپاور - بر مبنای مصوبه سازمان تنظیم")]
            StandardSpaceAndPower = 121
        }

        /// <summary>
        /// مقادیر مطابق با جدول مرحله درخواست در سرور گیلان میباشد
        /// </summary>
        public enum RequestStepSpaceAndPower : int
        {
            [Description("ثبت درخواست")]
            Start = 176,

            [Description("اداره طراحی انتقال")]
            Enteghal = 177,

            [Description("اداره ساختمان")]
            Sakhteman = 178,

            //[Description("کمیته فضا")]
            //Faza = 179,

            [Description("اداره طراحی نیرو")]
            Niroo = 181,

            //[Description("مدیر کل طرح مهندسی")]
            //ModireMohandesi = 182,

            //[Description("مدیر کل منطقه")]
            //ModireMantaghe = 183,

            [Description("عقد قرارداد")]
            Ghardad = 184,

            //[Description("اداره کل حراست")]
            //Herasat = 185,

            //[Description("رییس مرکز")]
            //Manager = 187,

            [Description("صدور صورت حساب")]
            SooratHesab = 188,

            [Description("حوزه مالی")]
            FinancialScope = 5381,

            [Description("مدیر طراحی")]
            DesignManager = 5382,

            [Description("اداره طراحی سوئیج")]
            SwitchDesigningOffice = 5383,

            [Description("مدیر طراحی - بررسی نهایی")]
            DesignManagerFinalCheck = 5384,

            [Description("معاونت شبکه")]
            NetworkAssistant = 5385,

            [Description("اداره نظارت تجهیزات مخابراتی")]
            AdministrationOfTheTelecommunicationEquipment = 5386,

            [Description("اداره طراحی شبکه و کابل")]
            CableAndNetworkDesignOffice = 5389,

            [Description("سالن دستگاه")]
            DeviceHall = 5390
        }

        public enum RequestStepFailure117 : byte
        {
            [Description("ام دی اف - بررسی درخواست")]
            MDFAnalysis = 212,

            [Description("شبکه هوایی")]
            Network = 213,

            [Description("کابل")]
            Cable = 214,

            [Description("سالن دستگاه")]
            Saloon = 217,

            [Description("ام دی اف - تایید درخواست")]
            MDFConfirm = 218,

            [Description("بایگانی")]
            Archived = 219,

            [Description("همه موارد")]
            All = 255
        }

        //public enum RequestStepADSL : byte
        //{
        //    [Description("ام دی اف - بررسی درخواست")]
        //    Registing = 95,

        //    [Description("شبکه هوایی")]
        //    Network = 213,

        //    [Description("کابل")]
        //    Cable = 214,

        //    [Description("سالن دستگاه")]
        //    Saloon = 217,

        //    [Description("ام دی اف - تایید درخواست")]
        //    MDFConfirm = 218,

        //    [Description("بایگانی")]
        //    Archived = 219,

        //    [Description("همه موارد")]
        //    All = 255
        //}

        public enum ChangeLocationStatus : byte
        {
            [Description("تایید")]
            Confirm = 1
        }

        public enum TakePossessionStatus : byte
        {
            [Description("تایید")]
            Confirm = 1
        }


        //public enum PCMPortType : byte
        //{
        //    [Description("پی سی ام ورودی")]
        //    InLine = 4,

        //    [Description("پی سی ام خروجی")]
        //    OutLine = 5
        //}

        public enum PCMType : byte
        {
            // from table PCMType
            [Description("1/4a")]
            a = 2,
            [Description("1/4b")]
            b = 3,
        }


        public enum ZeroStatus : byte
        {
            [Description("صفر اول")]
            FirstZero = 1,

            [Description("صفر دوم")]
            SecondZero = 2
        }

        public enum BlockZeroStatus : byte
        {
            [Description("مسدود")]
            Close = 1,

            [Description("باز")]
            Open = 2
        }

        public enum ADSLServiceType : byte
        {
            [Description("خرید سرویس")]
            Service = 1,

            [Description("خرید ترافیک")]
            Traffic = 2
        }

        public enum ADSLOwnerStatus : byte
        {
            [Description("مالک")]
            Owner = 1,

            [Description("نماینده")]
            Representative = 2,

            [Description("مستاجر")]
            Tenant = 3
        }

        public enum ADSLServiceCostPaymentType : byte
        {
            [Description("PrePaid")]
            PrePaid = 1,

            [Description("PostPaid")]
            PostPaid = 3
        }

        public enum ADSLCustomerPriority : byte
        {
            [Description("VIP")]
            VIP = 1,

            [Description("تجاری")]
            Commercial = 2,

            [Description("معمولی")]
            Normal = 3
        }

        public enum ADSLRegistrationProjectType : byte
        {
            [Description("عادی")]
            None = 0,

            [Description("شباب")]
            Shabab = 1,

            [Description("پروژه مدارس")]
            SchoolsInterner = 2
        }

        public enum ADSLPortStatus : byte
        {
            [Description("آزاد")]
            Free = 0,

            [Description("دایر")]
            Install = 1,

            [Description("خراب")]
            Destruction = 2,

            [Description("رزرو")]
            reserve = 3,

            [Description("مسدود")]
            Closed = 4
        }

        public enum ADSLChangePortStatus : byte
        {
            [Description("آزاد")]
            Free = 0,

            [Description("خراب")]
            Destruction = 2
        }

        public enum ADSLPortType : byte
        {
            [Description("ورودی")]
            Input = 1,

            [Description("خروجی")]
            OutPut = 2
        }

        public enum ADSLStatus : byte
        {
            [Description("در دست اقدام")]
            Pending = 1,

            [Description("وصل")]
            Connect = 2,

            [Description("قطع موقت")]
            Cut = 3,

            [Description("تخلیه")]
            Discharge = 4
        }

        public enum ADSLCutType : byte
        {
            [Description("شخصی")]
            SubscriberRequest = 1,

            [Description("اداری")]
            Administrative = 2,
        }

        public enum ADSLPAPRequestStatus : byte
        {
            [Description("در دست اقدام")]
            Pending = 1,

            [Description("انجام شد")]
            Completed = 2,

            [Description("رد درخواست")]
            Reject = 3
        }

        public enum ADSLPAPRejectCommnet : byte
        {
            [Description("شرکت PAP درخواست دهنده در سیستم موجود نمی باشد")]
            InvalidPAPCompany = 0,

            [Description("شرکت PAP درخواست دهنده غیر فعال می باشد")]
            InactivePAPCompany = 1,

            [Description("شماره تلفن ثبت شده در سیستم موجود نمی باشد")]
            InvalidTelephoneNo = 2,

            [Description("رانژه شرکت دیگری می باشد")]
            InstalByAnotherCompany = 3
        }

        public enum ADSLMDFCommnet : byte
        {
            [Description("رانژه شرکت دیگری می باشد")]
            InstalByAnotherCompany = 0,

            [Description("پورت خراب می باشد")]
            DamagedPort = 1,

            [Description("مشکلی وجود ندارد")]
            NoProblem = 2,

            [Description("PCM می باشد")]
            PCM = 3
        }

        public enum ADSLCustomerSatisfaction : byte
        {
            [Description("تایید")]
            Confirm = 0,

            [Description("عدم تایید")]
            Reject = 1
        }

        public enum SwitchPortStatus : byte
        {
            [Description("آزاد")]
            Free = 0,

            [Description("متصل")]
            Install = 1
        }

        public enum TypeOfPort
        {
            [Description("مسي / AB_WIRE")]
            Z = 1,

            [Description("V5.2")]
            V = 2,
        }

        public enum PortStatusOfFile
        {
            [Description("آزاد")]
            Free = 1,

            [Description("تخصیص یافته")]
            Allocated = 7,
        }
        //public enum ADSLRequest
        //{
        //    [Description("دایری ADSL")]
        //    ADSL = 35,

        //    [Description("تغییر نوع سرویس")]
        //    ADSLChangeServiceType = 37,

        //    [Description("تغییر تعرفه")]
        //    ADSLChangeTariff = 38,

        //    [Description("قطع موقت")]
        //    ADSLTemproryCut = 39,

        //    [Description("تخلیه")]
        //    ADSLDischarge = 43,

        //    [Description("تغییر پورت")]
        //    ADSLChangePort = 45,

        //    [Description("شرکت Pap")]
        //    ADSLPapInfo = 46

        //}

        public enum ADSLEquimentType
        {
            [Description("DSLAM")]
            DSLAM = 1,

            [Description("ONU")]
            ONU = 2,

            [Description("GPON")]
            GPON = 3,
        }

        public enum ADSLEquimentLocationInstall
        {
            [Description("Local")]
            Local = 1,

            [Description("Indoor")]
            Indoor = 2,

            [Description("Outdoor")]
            Outdoor = 3,
        }

        public enum ADSLEquimentProduct
        {
            [Description("Huawei")]
            Huawei = 1,

            [Description("ZTE")]
            ZTE = 2,

            [Description("FiberHome")]
            FiberHome = 3,
        }

        public enum ADSLPAPInstalTimeOut : byte
        {
            [Description("24 ساعت")]
            OneDay = 1,

            [Description("48 ساعت")]
            TwoDay = 2,

            [Description("72 ساعت")]
            ThreeDay = 3,

            [Description("نامحدود")]
            NoLimitation = 4
        }

        public enum ADSLPAPPortStatus : byte
        {
            [Description("آزاد")]
            Free = 0,

            [Description("دایری")]
            Instal = 1,

            [Description("تخلیه")]
            Discharge = 2,

            [Description("خراب")]
            Broken = 3,

            [Description("رزرو")]
            Reserve = 4
        }

        public enum ADSLIPStatus : byte
        {
            [Description("آزاد")]
            Free = 0,

            [Description("رزرو")]
            Reserve = 1,

            [Description("دایری")]
            Instal = 2,

            [Description("تخلیه")]
            Discharge = 3,

            [Description("خراب")]
            Broken = 4,

            [Description("منقضی")]
            Expired = 5
        }

        public enum DayeriStatus
        {
            [Description("دایری با تاخیر")]
            DayeriWithDelay = 1,

            [Description("دایری به موقع")]
            DayeriOnTime = 2
        }

        public enum Color
        {
            [Description("قرمز")]
            Red = 1,

            [Description("سبز")]
            Green = 2,

            [Description("مشکی")]
            Black = 3
        }

        public enum CustomerType
        {
            [Description("مشترک قبلی")]
            OldCustomer = 1,

            [Description("مشترک جاری")]
            NewCustomer = 2,
        }

        public enum UserControlNames
        {
            [Description("تجهیزات ADSL")]
            ADSLEquipment = 1,

            [Description("ADSL")]
            ADSL = 2,

            [Description("درخواست های انجام شده بر اساس ADSL")]
            ADSLRequest = 3,

            [Description("تغییر شماره ها")]
            ChangeNumber = 4,

            [Description("آمار قطع و وصل")]
            DisconnectAndConnectCount = 5,

            [Description(" دایری ")]
            ADSLDayeriRequest = 6,

            [Description("درخواست های ADSL شرکت های PAP")]
            PapADSLRequest = 7,

            [Description("تاخیر اداری ADSL")]
            ADSLOfficialDelay = 8,

            [Description("آماری ADSL")]
            ADSLStatistic = 9,

            [Description("تغییر نام")]
            ChangeName = 10,

            [Description("درخواست دایری")]
            DayeriRequest = 11,

            [Description("عملکرد درخواست های شرکت های PAP")]
            PapRequestOperation = 12,

            [Description("گزارش تجهیزات فنی دایری")]
            InvestigatePossibility = 13,

            [Description("درخواست دایری مجدد")]
            ReDayeriRequest = 14,

            [Description("گزارش فرمهای سیم بندی")]
            IssueWiring = 15,

            [Description(" انسداد صفر")]
            ZeroStatus = 16,

            [Description(" تغییرات عنوان")]
            ChangeTitleIn118 = 17,

            [Description("فضا و پاور")]
            SpaceAndPower = 18,

            [Description("سرویس ویژه")]
            SpecialService = 19,

            [Description("تغییر مکان")]
            ChangeLocation = 20,

            [Description("اطلاعات فنی کافوهای مرکز کلی")]
            TotalCenterCabinetInfo = 21,

            [Description("اطلاعات فنی کافوهای مرکز جزئی")]
            DetailsCenterCabinetInfo = 72,

            [Description("اطلاعات فنی پست کلی")]
            PostInfoTotal = 22,

            [Description("اطلاعات فنی پست جزیی")]
            PostInfoDetails = 74,

            [Description("اطلاعات فنی پست سرکابل")]
            PostInfoCable = 76,

            [Description("اطلاعات فنی مرکزی های کافو")]
            CabinetCentersInfo = 23,

            [Description("اتصالی های پست")]
            PostContacts = 24,

            [Description("آمار اتصالهای دارای PCM")]
            PCMContactsStatistic = 25,

            [Description("آمار پست های دارای PCM")]
            PCMContactsPostStatistic = 26,

            [Description("آمار کل PCM ها")]
            PCMsStatistic = 27,

            [Description("آمار کل تجهیزات PCM")]
            PCMStatisticEquipment = 28,

            [Description("لیست پی سی ام")]
            AllPCMs = 29,

            [Description("خرابی 117")]
            Failure117Network = 30,

            [Description("خرابی 117")]
            Failure117Requests = 31,

            [Description(" وضعیت و تاریخ خرابيهاي ارسال شده به شبکه و کابل")]
            Status_DateSendingFailure117Requests = 32,

            [Description(" خرابيهاي ارسال شده به شبکه و کابل")]
            SendingFailure117RequestsToNetworkCable = 33,

            [Description("خرابی کافو")]
            CabinetInputFailure = 34,

            [Description("نصب پی سی ام")]
            InstallPCM = 36,

            [Description("پی سی ام های خالی")]
            EmptyPCMs = 35,

            [Description("کلی خرابی کافو")]
            TotalCabinetInputFailure = 37,

            [Description("زمانبندی خرابی 117")]
            FailureTimeTable = 38,

            [Description("ظرفیت کافو")]
            CabinetCapacity = 39,

            [Description("آمار وضعیت پست")]
            TotalStatisticPCMPorts = 40,

            [Description(" کابل های مرکز کلی ")]
            CenterCablesTotal = 41,

            [Description("آمار ورودیهای کافو")]
            InputStatistic = 42,

            [Description("آمار بوخت های عمودی")]
            VerticalBuchtsStatistic = 43,

            [Description("آمار پورت های خراب")]
            FailurePortsStatistic = 44,

            [Description("خرابی پورت های اصلاح شده")]
            FailureCorrectPorts = 45,

            [Description("خرابی پورت های اصلاح نشده")]
            FailureNotCorrectPorts = 46,

            [Description("رانژه و تخلیه شرکت های Pap")]
            InstallAndDisChargePapCompany = 47,

            [Description("خرابی اتصالی های کلی")]
            failurePostContacts = 48,

            [Description("خرابی اتصالی اصلاح شده")]
            failureCorrectedPostContacts = 49,

            [Description("خرابی اتصالی اصلاح نشده")]
            failureNotCorrectedPostContacts = 50,

            [Description("گزارش کلي خرابي مرکزی ها ")]
            failureCabinetInputReport = 51,

            [Description("خرابی ورودی های اصلاح شده")]
            failureCorrectedCabinetInputs = 52,

            [Description("خرابی ورودی های اصلاح نشده")]
            failureNotCorrectedCabinetInputs = 53,

            [Description("اطلاعات فضا و پاور شرکت های Pap")]
            SpaceAndPowerPapCompany = 54,

            [Description("آمار زوج کابل ها")]
            CablePair = 55,

            [Description(" آمار بوخت هاي افقي")]
            HorizintalBuchtsStatistic = 56,

            [Description("  آمار تلفن هاي خالي")]
            EmptyTelephone = 57,

            [Description(" آمار کلي واگذاري خطوط")]
            ResignationLines = 58,

            [Description(" تلفنهایی که پی سی ام ندارند")]
            TelephoneWithOutPCM = 59,

            [Description(" پي سي ام هاي داخل پست")]
            PCMInPost = 60,

            [Description("پرونده های آزاد شده")]
            ReleaseDocuments = 61,


            [Description("فرم درخواست دایری")]
            InstallRequestForm = 62,

            [Description("عملکرد خرابی 117")]
            PerformanceFailure117 = 66,

            [Description("فرم درخواست دایری")]
            DayeriWiringNetwork = 67,

            [Description("تعویض بوخت")]
            ChangeBucht = 63,

            [Description("عملکرد شبکه هوایی")]
            PerformanceWiringNetwork = 68,

            [Description("صورت حساب امکانات")]
            EquipmentBilling = 69,

            [Description(" ريز صورتحساب امکانات واگذارشده به شرکت")]
            TinyBillingOptions = 70,

            [Description(" خرابيهاي ارسال شده به کابل")]
            SendingFailure117Cable = 71,

            [Description("کابل های مرکز جزیی")]
            CenterCablesDetails = 73,

            [Description("فرم انتقال حق المتياز تلفن / فيش ثابت")]
            ChangeNameDocument = 75,

            [Description("پست رزرو")]
            PostInfoReserve = 77,

            [Description("پست پر")]
            PostInfoFill = 78,

            [Description("فاکتور فروش")]
            SaleFactor = 79,

            [Description("کلی کافو و  پست")]
            CenterCabinet_Subset = 80,

            [Description("کافو های مرکز ترتیب اتصالی کافو")]
            CenterCabinet_CabinetSyndeticOrder = 81,

            [Description("آمار اتصالی کل")]
            PostContactTotal = 82,

            [Description("آمار اتصالی خراب")]
            PostContactFail = 83,

            [Description("آمار اتصالی پر")]
            PostContactFill = 84,

            [Description("آمار اتصالی خالی")]
            PostContactEmpty = 85,

            [Description("آمار اتصالی رزرو")]
            PostContactReserve = 86,

            [Description("آمار ورودی کل")]
            CabinetInputTotal = 88,

            [Description("آمار ورودی خراب")]
            CabinetInputFail = 89,

            [Description("آمار ورودی پر")]
            CabinetInputFill = 90,

            [Description("آمار ورودی خالی")]
            CabinetInputEmpty = 91,

            [Description("آمار ورودی رزرو")]
            CabinetInputReserve = 92,

            [Description("آمار بوخت های عمودی کل")]
            VerticalBuchtTotal = 93,

            [Description("آمار بوخت های عمودی خراب")]
            VerticalBuchtFail = 94,

            [Description("آمار بوخت های عمودی پر")]
            VerticalBuchtFill = 95,

            [Description("آمار بوخت های عمودی خالی")]
            VerticalBuchtEmpty = 96,

            [Description("آمار بوخت های عمودی رزرو")]
            VerticalBuchtReserve = 97,

            [Description("آمار زوج کابل مرکز کل")]
            CablePairTotal = 98,

            [Description("آمار زوج کابل مرکز خراب")]
            CablePairFail = 99,

            [Description("آمار زوج کابل مرکز پر")]
            CablePairFill = 100,

            [Description("آمار زوج کابل مرکز خالی")]
            CablePairEmpty = 101,

            [Description("آمار زوج کابل مرکز رزرو")]
            CablePairReserve = 102,

            [Description("گزارش پرداخت ها")]
            RequestPayment = 103,

            [Description("گزارش سرویس های ADSL")]
            ADSLServiceReport = 104,

            [Description("گزارش تاریخچه شماره تلفن های ADSL")]
            ADSLTelephoneNoHistoryReport = 105,

            [Description("گزارش عملکرد نمایندگان فروش ADSL")]
            ADSLSellerAgentReport = 106,

            [Description(" تاریخچه مشترکین ADSL")]
            ADSLHistoryReport = 107,

            [Description(" اطلاعات مشترکین ADSL")]
            ADSLCustomerInfoReport = 108,

            [Description(" تجهیزات شرکت های PAP")]
            ADSLPAPEquipmentInfo = 109,

            [Description("پرداخت های ADSL")]
            ADSLPaymentReport = 110,

            [Description("هزینه نصب و راه اندازی ADSL توسط کارشناس")]
            ADSLInstallmentByExpertReport = 111,

            [Description("تخلیه/دایری")]
            ADSLDayeriDischargeReport = 112,

            [Description("اطلاعات و آمار کلی مشترکین")]
            ADSLGeneralCustomerInforeport = 113,

            [Description("آمار فروش تعدادی ADSL")]
            ADSLNumberSaleReport = 114,

            [Description("گزارش فروش ADSL")]
            ADSLSaleReport = 115,

            [Description("پهنای باند ADSL")]
            ADSLBandwidthReport = 116,

            [Description("ليست مشترکين آماده به نصب و ارسال جهت راه اندازي و دايري")]
            ADSLReadyToInstallCustomersReport = 117,

            [Description("ليست مشترکيني که مودم تحويل گرفته اند")]
            ADSLCustomersDeliveredModemReport = 118,

            [Description("آمار فروش شهرستان مرکز به مرکز")]
            ADSLCityCenterSaleStatisticsReport = 119,

            [Description("آمار فروش شهرستان  فروشنده به فروشنده")]
            ADSLCitySellerSaleStatisticsReport = 120,

            [Description("گزارش فروش نمايندکان فروش")]
            ADSLSellerAgentSaleReport = 121,

            [Description("گزارش فروش مراکز ")]
            ADSLCenterSaleReport = 122,

            [Description("گزارش فروش کلي روزانه شهرستان ")]
            ADSLCitySaleReport = 123,

            [Description("درآمد روزانه آنلاين شهرستان ")]
            ADSLOnlineDailyCitySaleReport = 124,

            [Description("گزارش روزانه تخليه شهرستان ")]
            ADSLCityDailyDischargeReport = 125,

            [Description("گزارش کلي ADSL مخابرات مرکز به مرکز ")]
            ADSLCenterGeneralContactsReport = 126,

            [Description("نمودار مقايسه ماهانه-91  ")]
            ADSLMonthlyComparisonDiagram91 = 127,


            [Description("گزارش کلی ADSl مخابرات شهرستان به شهرستان")]
            ADSLCityContactsGeneralReport = 128,

            [Description("درخواست پرداخت اقساطي ADSL")]
            ADSLInstalmentRequestPaymentReport = 129,


            [Description("گزارش هفتگي ADSL مخابرات شهرستان به شهرستان")]
            ADSLCityWeeklyContactsReport = 130,


            [Description("نمودار مقايسه هفتگي مخابرات و شرکت هاي PAP")]
            ADSLWeeklyComparisionDiagramContactsPAP = 131,

            [Description(" گزارش ماهانه ADSL شهرستان به شهرستان")]
            ADSLCityMonthlyReport = 132,

            [Description("گزارش مودم های فروخته شده")]
            ADSLSoldModemReport = 133,

            [Description("درخواست های دایری شرکت های PAP")]
            PAPADSLDayeriRequest = 134,

            [Description("درخواست های تخلیه شرکت های PAP")]
            PAPADSLDischargeRequest = 135,

            [Description("درخواست های تعویض پورت شرکت های PAP")]
            PAPADSLChangePortRequest = 136,

            [Description("گواهی تغییر مکان داخل مرکز")]
            ChangeLocationCenterInsideCertificateReport = 137,

            [Description("گواهی تغییر مکان  مرکز به مرکز")]
            ChangeLocationCenterToCenterCertificateReport = 138,

            [Description("گواهی تغییر مکان و نام  مرکز به مرکز")]
            ChangeLocationAndNameCenterToCenterCertificateReport = 139,

            [Description("گواهی تغییر مکان و نام داخل مرکز")]
            ChangeLocationAndNameCenterInsideCertificateReport = 140,

            [Description("گزارش تعداد پورت های قابل واگذاری به تفکیک شهر و مرکز")]
            ADSLPortsCityCenterReport = 141,

            [Description("آمار فروش به تفکيک سرويس و کانال فروش در بازه زماني مورد نظر ")]
            ADSLSaleServiceAndADSLSellChanellReport = 142,

            [Description("درآمد حاصل از فروش به تفکيک  کانال فروش و بازه زماني ")]
            ADSLIncomeSellChanellAndTimeReport = 143,

            [Description("تعداد درخواست هاي تمديد  سرويس به تفکيک کانال فروش در بازه زماني مشخص  ")]
            ADSLChangeServiceReportUserControl = 144,

            [Description("تعداد و ميزان ترافيک اضافي فروخته شده به تفکيک سرويس ")]
            ADSLAdditionalServiceSaleReport = 145,

            [Description("گزارش سفارشات نصب شده به تفکيک استان و سرويس و کانال هاي فروش")]
            ADSLInstalledADSLReport = 146,

            [Description("پرفروش ترين سرويس در هر کانال فروش")]
            ADSLMostSoldServicesReport = 147,

            [Description("تعداد درخواست هاي تخليه ")]
            ADSLDischargeRequestsReport = 148,

            [Description(" تخليه ")]
            DischargeReport = 149,

            [Description(" گزارش 118 ")]
            Report118 = 150,

            [Description("تعویض شماره")]
            ChangeNoReport = 151,

            [Description("قطع و وصل")]
            CutAndEstablishReport = 152,

            [Description("اقساط")]
            InstalmentRequestPaymentReport = 153,

            [Description("گزارش کلي هزينه")]
            GenrealCostReport = 154,

            [Description("گزارش جزيي هزينه")]
            FineToFineCostReport = 155,

            [Description("دايري")]
            InstallRequestReport = 156,

            [Description("ثبت نام")]
            RegisterReport = 157,

            [Description("آمار سرويس ويژه")]
            SpecialServiceStatisticsReport = 158,

            [Description("انسداد")]
            BlockingReport = 159,

            [Description("لیست پورت های ADSL")]
            ADSLPortsReport = 160,

            [Description("گزارش ريز ثبت نام ADSLهرنماينده فروش  ")]
            ADSLSellerAgentADSLRequestSaleDetailesReport = 161,

            [Description("ليست ثبت نام هاي  اينترنتي ")]
            ADSLOnlineRegistrationReport = 162,

            [Description("آمار فروش روزانه شرکت مخابرات ")]
            ADSCityCenterDailyReport = 163,

            [Description("آمار فروش روزانه اینترانت شرکت مخابرات ")]
            ADSLIntranetDailySaleReport = 164,

            [Description("درآمد روزانه شهرستان ")]
            ADSLCityDailyIncomeReport = 165,

            [Description("خريد سرويس ")]
            ADSLServiceSaleReport = 166,

            [Description("خريد ترافیک ")]
            ADSLTrafficSaleReport = 167,

            [Description("تعداد دايري-تخليه-تمديدي در تاريخ انتخاب شده ")]
            ADSLNumberOfDayeriDischargeReshargeReport = 168,

            [Description("ميزان فروش سرويس نمايندگان فروش ")]
            ADSLSellerAgentUsersSaleAmountReport = 169,

            [Description("تعداد دايري-منقضي شده در هر مرکز")]
            ADSLNumberOfDayeriExpirationReport = 170,

            [Description("گزارش تاريخ انقضا و آخرين سرويس براي هر تلفن")]
            ADSLExpirationDateAndLastServiceTelephoneNoReport = 171,

            [Description("گزارش وضعيت پورت هاي هر مرکز")]
            ADSLCenterPortStatusReport = 172,

            [Description("گزارش ريز فروش نقدي نمايندگان فروش")]
            ADSLSellerAgentCashSaleReport = 173,

            [Description("گزارش ريز فروش نمايندگان فروش")]
            ADSLSellerAgentSaleDetailsReport = 174,

            [Description("خريد سرويس جهت ورود به سيستم مالي ADSL به تفکيک سرعت")]
            ADSLSeviceSaleBandwidthSeparationReport = 175,

            [Description("خريد ترافيک ADSL به تفکيک حجم جهت ورود به سيستم مالي")]
            ADSLTrafficSaleTrafficSeperationReport = 176,

            [Description("خريد سرويسADSL  به تفکيک مشتري و سرويس -جهت ورود به سيستم مالي")]
            ADSLServiceSaleCustomerAndServiceSperationReport = 177,

            [Description("خريد سرويس ADSL تجميعي به تفکيک مرکز-جهت ورود به سيستم ماي")]
            ADSLServiceAggragateSaleCenterSeperationReport = 178,

            [Description("خريد ترافيک ADSL به تفکيک مشتري")]
            ADSLTrafficSaleCustomerSeperationReport = 179,

            [Description("خريد حجم ADSL تجميعي به تفکيک مرکز-جهت ورود به سيستم مالي")]
            ADSLTrafficAggregateSaleCenterCostCodeSeperationReport = 180,

            [Description("خريد مودم تجميعي به تفکيک حجم-جهت ورود به سيستم مالي")]
            ADSLModemAggragateSaleReport = 181,

            [Description("اطلاعات ADSL در AAA")]
            ADSLInformationReport = 182,

            [Description("اقساطي ها")]
            ADSLInstalmetTabReport = 183,

            [Description("گزارش ريز فروش نمايندگان فروش-گروه بندي بر اساس شماره تلفن")]
            ADSLSellerAgentSaleDetailsGroupByTelephoneNoReport = 184,

            [Description("استرداد")]
            RefundDepositReport = 185,

            [Description("نوع شخصیت")]
            PersonTypeReport = 186,

            [Description("لیست شماره تلفن های خالی")]
            EmptyTelephoneNoLisReport = 187,

            [Description("تخلیه شبکه هوایی")]
            DischargeWiringNetworkReport = 188,

            [Description("گواهی تخلیه")]
            DischargeCertificateReport = 189,


            [Description("شبکه هوایی دایری مجدد")]
            ReInstallWiringReport = 190,

            [Description("چاپ گواهی دایری مجدد")]
            ReInstallPrintCertificationReport = 191,

            [Description("گواهی تعویض شماره")]
            ChangeNoCertificateReport = 192,

            [Description("شبکه هوایی تغییر مکان داخل مرکز")]
            ChangeLocationCenterInsideWiringReport = 193,

            [Description("اطلاعات ADSL")]
            ADSLInformationSystemReport = 194,

            [Description("ریز تخلیه ADSL")]
            ADSLDischargeDetailsReport = 195,

            [Description("شبکه هوایی تغییر مکان مرکز به مرکز")]
            ChangeLocationCenterToCenterWiringReport = 196,

            [Description("سیم بندی ام-دی-اف")]
            MDFWiringReport = 197,

            [Description("گواهی تغییر نام")]
            ChangeNameCertificateReport = 198,

            [Description("گواهی قطع")]
            CutCertificateReport = 199,

            [Description("گواهی وصل")]
            EstablishCertificateReport = 200,

            [Description("چاپ گواهی سرویس ویژه")]
            SpecialServicePrintCertification = 201,

            [Description("چاپ گواهی انسداد صفر")]
            ZeroStatusPrintCertificationReport = 202,

            [Description("گواهی اصلاح آدرس")]
            ChangeAddressCertificateReport = 203,

            [Description("چاپ گواهی PBX")]
            TelephonePBXPrintCertificationReport = 204,

            [Description("ام دی اف دایری-تخلیه-تغییر مکان مرکز به مرکز-دایری مجدد")]
            DayeriDischargeChangeLocationCenterTocenterReInstallMDFReport = 205,

            [Description("سیم بندی ام دی اف تغییر مکان داخل مرکز")]
            ChangeLocationcenterTocenterMDFWiringReport = 206,

            [Description("سیم بندی ام دی اف تعویض شماره")]
            ChangeNoMDFWiringReport = 207,

            [Description("سوابق تلفن")]
            TelephoneRequestLogReport = 208,

            [Description("اطلاعات مودم ADSL")]
            ADSLModemInformationReport = 209,

            [Description("پورسانت نماینده فروش")]
            ADSLSellerAgentComissionReport = 210,

            [Description("اطلاعات فردی مشترک")]
            CustomerPersonalInformationReport = 211,

            [Description("اعتبار کاربران نماینده فروش")]
            ADSLSellerAgentUsersCreditReport = 212,

            [Description("MDFدایریE1")]
            MDFE1DayeriReport = 213,

            [Description("برگردان پست-شبکه هوایی")]
            PostTranslationWiring = 214,

            [Description("اقساط بر اساس شماره تلفن ")]
            ADSLInstallmentRequestPaymentTelephoneNo = 215,

            [Description("برگردان پست ام دی اف ")]
            MDFTranslationPostInput = 216,

            [Description("برگردان کافو- ام دی اف")]
            MDFExchangeCabinuteInput = 217,

            [Description("برگردان کافو-شبکه هوایی")]
            NetWorkingExchangeCabinuteInput = 218,

            [Description("برگردان مرکزی ام دی اف-ام دی اف")]
            MDfCentralTranslationForMDFReport = 219,

            [Description("برگرذان مرکز به مرکز-سالن سوییچ دایری")]
            CenterToCenterTranslationSwitchDayeri = 220,

            [Description("برگردان مرکز به مرکز ام دی اف دایری")]
            CenterToCenterTranslationMDFDayeri = 221,

            [Description("برگردان مرکز به مرکز شبکه هوایی دایری")]
            CenterToCenterTranslationNetworkingDayeri = 222,

            [Description("برگردان مرکز به مرکز سالن سوییچ تخلیه")]
            CenterToCenterTranslationSwitchDischarge = 223,

            [Description("برگردان مرکز به مرکز ام دی اف تخلیه")]
            CenterToCenterTranslationMDFDischarge = 224,

            [Description("برگردان مرکز به مرکز شبکه هوایی تخلیه")]
            CenterToCenterTranslationNetworkingDischarge = 225,

            [Description("برگردان مرکز به مرکز امور مشترکین چاپ گواهی")]
            CenterToCenterTranslationPrintCertification = 226,

            [Description("تعویض بوخت ام دی اف")]
            BuchtSwitchingMDF = 227,

            [Description("تعویض بوخت شبکه کابل و هوایی")]
            BuchtSwitchingNewtworking = 228,

            [Description("برگردان پست")]
            TranslationPostReport = 229,

            [Description("برگردان مرکزی پست")]
            TranslationPostInputReport = 230,

            [Description("برگردان کافو")]
            ExchangeCabinetInputReport = 231,

            [Description("برگردان مرکزی ام دی اف")]
            ExchangeCentralCableMDFReport = 232,

            [Description("برگردان مرکز به مرکز")]
            CenterToCenterTranslationReport = 233,

            [Description("تعویض بوخت")]
            BuchtSwitchingReport = 234,

            [Description("اطلاعات درخواست خرابی 17")]
            Failure117RequestPrint = 235,

            [Description("تعیین شماره کافو نوری به معمولی")]
            TranslationOpticalToNormalChooseNumberReport = 236,

            [Description("اطلاعات درخواست شرکت PAP")]
            PAPRequestPrint = 237,

            [Description("تلفن های مشغول بکار")]
            WorkingTelephone = 238,

            [Description("اخطار ها و توقیف")]
            WarningHistoryReport = 239,

            [Description("تلفن های رند")]
            RoundTelephone = 240,

            [Description("کد ملی مشترک")]
            CustomerNationalCodeReport = 241,

            [Description("تلفن های مشغول بکار بر اساس تاریخ")]
            WorkingTelephoneBaseDate = 242,

            [Description("دفاتر خدماتی")]
            CustomerOffice = 243,

            [Description("اطلاعات درخواست تعویض بوخت شرکت PAP")]
            PAPRequestExchangePrint = 244,

            [Description(" شبکه هوایی کافو نوری به معمولی")]
            TranslationOpticalCabinetToNormallNetwrokWiringReport = 245,

            [Description(" شبکه پی سی ام به معمولی")]
            TranslationPCMToNormallNetworkReport = 246,

            [Description(" ام دی اف پی سی ام به معمولی")]
            TranslationPCMToNormallMDFReport = 247,

            [Description(" ام دی اف سیم خصوصی")]
            SpecialWirelMDFReport = 248,

            [Description("گزارش چاپ گواهی برای برگردان کافو نوری به کافو معمولی")]
            TranslationOpticalCabinetToNormalReport = 249,

            [Description("بازدید از محل")]
            VisitAddressReport = 250,

            [Description("چاپ مامور سیم خصوصی")]
            SpecialWiringWiringNetworkReport = 251,

            [Description("درخواست برگردان کافو نوری به کافو معمولی")]
            TranslationOpticalCabinetToNormalRequestReport = 252,

            [Description("چاپ گواهی سیم خصوصی")]
            SpecialWireCertificatePrintReport = 253,

            [Description("گزارش اطلاعات جامع")]
            GeneralInformationReport = 254,

            [Description("گزارش تعویض شماره")]
            SwapTelephoneReport = 255,

            [Description("گزارش تعویض پی سی ام")]
            SwapPCMReport = 256,

            [Description("گزارش ام دی اف تخلیه سیم خصوصی")]
            MDFVacateSpecialWireReport = 257,

            [Description("چاپ مامور شبکه هوایی تخلیه سیم خصوصی")]
            NetworkVacateSpecialWireReport = 258,

            [Description("آمار خرابی ماهیانه مشترکین")]
            Failure117TotalInfoUserControl = 259,

            [Description("گزارش اصلاح مشخصات")]
            ModifyProfileReport = 260,

            [Description("چاپ گواهی تخلیه سیم خصوصی")]
            VacateSpecialWireCertificate = 261,

            [Description("گزارش بازداشت و توقیف")]
            DetentionAndArrestReport = 262,

            [Description("چاپ گواهی تغییر مکان سیم خصوصی")]
            ChangeLocationSpecialWireCertificate = 263,

            [Description("چاپ مامور تغییر مکان سیم خصوصی")]
            ChangeLocationNetworkSpecialWire = 264,

            [Description("ام دی اف تغییر مکان سیم خصوصی")]
            ChangeLocationMDFSpecialWire = 265,

            [Description("خرابی مانده در شبکه خرابی 117")]
            Failure117RequestRemaindInNetwork = 266,

            [Description("گزارش تاریخچه خرابی ها")]
            MalfuctionHistoryReport = 267,

            [Description("گزارش برگردان شبکه هوایی ")]
            NetworkWireExchangeCentralPostReport = 268,

            [Description("گزارش درخواست های خارج از مرز")]
            OutOfBoundRequestReport = 269,

            [Description("گزارش مدیریتی وضعیت درخواستها")]
            RequestStateReport = 270,

            [Description("گزارش آمار تلفن های مشغول به کار بر اساس سوئیج")]
            WorkingTelephoneStatisticsBySwitchTypeReport = 271,

            [Description("آمار ماهیانه درخواست شرکت PAP")]
            PAPRequestMontlyReport = 272,

            [Description("چاپ مامور ای وان")]
            E1WiringNetworkReport = 273,

            [Description("چاپ مامور لینک ای وان")]
            E1LINKWiringNetworkReport = 274,

            [Description("لیست سیاه آدرس")]
            BlackListAddress = 275,

            [Description("لیست سیاه تلفن")]
            BlackListTelephone = 276,

            [Description("لیست سیاه مشترکین")]
            BlackListCustomer = 277,

            [Description("گزارش لیست کافو های پر شده")]
            FilledCabinetReport = 278,

            [Description("آمار کلی شرکت های PAP")]
            PAPTotalReport = 279,

            [Description("اطلاعات فنی تلفن های دایر")]
            PAPTechnicalReport = 280,

            [Description("گواهی استرداد ودیعه تلفن ثابت")]
            RefundDepositCertificateReport = 281,

            [Description("گزارش سیم بندی ام دی اف در تغییر مکان داخل مرکز")]
            ChangeLocationInsideCenterMDFWiringReport = 282,

            [Description("گزارش سیم بندی ام دی اف در دایری")]
            DayeriMDFWiringReport = 283,

            [Description("گزارش تایید تخلیه توسط ام دی اف در تخلیه")]
            DischargeConfirmByMDFReport = 284,

            [Description("گزارش سیم بندی ام دی اف در مبدا - تغییر مکان مرکز به مرکز")]
            ChangeLocationCenterToCenterMdfWriringOfSourceCenterReport = 285,

            [Description("گزارش سیم بندی ام دی اف در مقصد - تغییر مکان مرکز به مرکز")]
            ChangeLocationCenterToCenterMdfWriringOfTargetCenterReport = 286,

            [Description("گزارش اخطارها")]
            WarningReport = 287,

            [Description("بازدید از محل همراه با آدرس قدیم")]
            VisitAddressWithOldAddressReport = 288,

            [Description("گواهی صدور صورتحساب دوماهه فضا و پاور")]
            SpaceAndPowerInvoiceIssuanceCertificate = 289,

            [Description("گواهی E1")]
            E1Certificate = 290,

            [Description("گواهی صدور صورتحساب دوماهه E1")]
            E1InvoiceIssuanceCertificate = 291,

            [Description("صورتحساب فروش کالا و خدمات - فرم خام")]
            TelecomminucationServicePaymentReportRaw = 292,

            [Description("گزارش ام دی اف تعویض تلفن")]
            SwapTelephoneMDFWiringReport = 293,

            [Description("گزارش ام دی اف تعویض PCM")]
            SwapPCMMDFWiringReport = 294,

            [Description("گزارش ام دی اف برگردان کافو نوری به معمولی")]
            TranslationOpticalCabinetToNormallMDFWiringReport = 295,

            [Description("گزارش صورتحساب فروش کالا و خدمات")]
            TelecomminucationServicePaymentReport = 296,

            [Description("آمار صورتحساب فروش کالا و خدمات")]
            TelecomminucationServicePaymentStatisticsReport = 297,

            [Description("آمار خرابی 17")]
            Failure117TotalInfoSemnanUserControl = 298,

            [Description("آمار صورتحساب فروش کالا و خدمات برای ایوان - همراه با آدرس مبدا و مقصد")]
            TelecomminucationServicePaymentStatisticsWithInstallAndTargetAddressForE1Report = 299,

            [Description("گزارش قطع تلفن ها")]
            CuttedTelephonesInformationReport = 300,

            [Description("گزارش وصل تلفن ها")]
            EstablishedTelephoneInformation = 301,

            [Description("گزارش هزینه ها - به تفکیک شهر،مرکز،نوع هزینه")]
            GeneralRequestPaymentsDividedByCityCenterBaseCostReport = 302
        }

        public enum SpaceType
        {
            [Description("فضای ساختمان های مخابراتی")]
            ContactsBuilding = 1,

            [Description("عرصه بدون بنا")]
            FieldWithoutBuildibg = 2,

            [Description("فضای دکل")]
            RigSpace = 3,

            [Description("عدم نیاز به فضا")]
            NoNeedSpace = 4,

            [Description("BTS")]
            BTS = 5
        }

        public enum FileOfficeType
        {
            [Description("فایل اداره طراحی نیرو")]
            PowerOfficeFile,

            [Description("فایل اداره طراحی شبکه و کابل")]
            CableOfficeFile,

            [Description("فایل اداره انتقال")]
            TransferDepartmentFile,

            [Description("فایل اداره طراحی سوئیچ")]
            SwitchOfficeFile
        }

        public enum PowerType
        {
            [Description("AC با پشتوانه تک فاز")]
            ACWithOnePhase = 1,

            [Description("AC بدون پشتوانه تک فاز")]
            ACWithoutOnePhase = 2,

            [Description("DC")]
            DC = 3,

            [Description("BTS")]
            BTS = 4,

            [Description("عدم نیاز به پاور")]
            NoNeedPower = 5,

            [Description("AC با پشتوانه سه فاز")]
            ACWithThreePhase = 6,

            [Description("AC بدون پشتوانه سه فاز")]
            ACWithoutThreePhase = 7
        }

        public enum EquipmentType
        {
            [Description("---")]
            a = 1
        }

        //TODO:rad 13950523
        /// <summary>
        /// نوع تجهیزات پایه ای
        /// </summary>
        public enum BasicEquipmentType : int
        {
            [Description("نامشخص")]
            None = 0,

            [Description("پاور")]
            Power = 1,

            [Description("زوج مسی")]
            CopperPair = 2,

            [Description("آنتن")]
            Antenna = 3,

            [Description("آنتن یاگی")]
            YagiAntenna = 4,

            [Description("آنتن بشقابی")]
            Dish = 5,

            [Description("دکل")]
            Rig = 6,

            [Description("فضا")]
            Space = 7,

            [Description("فیبر")]
            Fibre = 8,

            [Description("نگهداری ماهانه")]
            MonthlyMaintenance = 9,

            [Description("کابل")]
            Cable = 10
        }

        public enum FeasibilityStatus
        {
            [Description("بلا مانع")]
            NoProblem = 0,

            [Description("شماره تلفن موجود نمی باشد")]
            PhoneNotExist = 1,

            [Description("شماره تلفن دارای ADSL می باشد")]
            HaveADSL = 2,

            [Description("شماره تلفن روی PCM می باشد")]
            OnPCM = 3,

            [Description("در پست شماره تلفن PCM وجود دارد")]
            PostPCM = 4,

            [Description("شماره تلفن قطع می باشد")]
            Disconnected = 5,

            [Description("شماره تلفن بدهی دارد")]
            BillingProblem = 6,

            [Description("کافو غیر مجاز")]
            CabintTechnicalProblem = 7,

            [Description("شماره تلفن غیر مجاز")]
            TelephoneTechnicalProblem = 7
        }

        public enum ChangeLocationCenterType
        {
            [Description("تغییر مکان داخل مرکز")]
            InSideCenter = 1,

            [Description("تغییر مکان مرکز به مرکز")]
            CenterToCenter = 2,

            [Description("تغییر مکان خودی")]
            itself = 3
        }

        public enum CabinetInputStatus
        {
            [Description("خراب")]
            Malfuction = 0,

            [Description("سالم")]
            healthy = 1,

            [Description("برگردان")]
            Exchange = 2,

            // stastus of eleka

            //[Description("خالی")]
            //healthy = 1,

            //[Description("پر")]
            //healthy = 1,

            //[Description("خراب")]
            //healthy = 1,

            //[Description("رزرو")]
            //healthy = 1,

            // [Description("در حال تعويض بوخت")]
            //healthy = 1,
            // [Description("پی سی ام داخل کافو")]
            //healthy = 1,
            // [Description("برگردان مرکز به مرکز")]
            //healthy = 1,
        }

        public enum CabinetInputDirection
        {

            [Description("پیاده رو")]
            pavement = 1,

            [Description("سواره رو")]
            Roadway = 2

        }

        public enum CabinetInputMalfuctionType
        {

            [Description("اتصال")]
            Connect = 1,
            [Description("خط پاره")]
            RupturedLine = 2,
            [Description("هم شنوایی")]
            HearingLoss = 3,

            [Description("دیگر")]
            Other = 4,

        }

        public enum PostContactMalfuctionType
        {

            [Description("اتصال به زمین")]
            Connect = 1,
            [Description("خط پاره")]
            RupturedLine = 2,
            [Description("هم شنوایی")]
            HearingLoss = 3,
            [Description("دیگر")]
            Other = 4,

        }

        public enum PCMCardMalfuctionType
        {

            [Description("اتصال به زمین")]
            Connect = 1,
            [Description("خط پاره")]
            RupturedLine = 2,
            [Description("هم شنوایی")]
            HearingLoss = 3,
            [Description("دیگر")]
            Other = 4,

        }

        public enum PCMMalfuctionType
        {

            [Description("شبکه")]
            NetWork = 1,
            [Description("طرف مرکز")]
            CenterSide = 2

        }

        public enum MalfuctionType
        {

            [Description("اتصالی پست")]
            PostConntact = 1,
            [Description("مرکزی")]
            CabinetInput = 2,
            [Description("پورت پی سی ام")]
            PCMPort = 3,
            [Description("پی سی ام")]
            PCM = 4,
        }

        public enum MalfuctionStatus
        {

            [Description("خراب")]
            Malfuction = 0,

            [Description("سالم")]
            healthy = 1,
        }


        public enum CablePhysicalType
        {
            [Description("ژله ای")]
            Jelly = 1,
            [Description("AirCore")]
            Ercore = 2


        }


        public enum TypeCablePairToBucht
        {
            [Description("Assign")]
            Assign = 1,
            [Description("Leave")]
            Leave = 2
        }

        public enum TypeCabinetInputToBucht
        {
            [Description("Assign")]
            Assign = 1,
            [Description("Leave")]
            Leave = 2,
            [Description("Delete")]
            Delete = 3
        }
        public enum ChangeNumberType
        {
            [Description("تغییر مکان به همراه تغییر شماره")]
            ChangeNumber = 1,

            [Description("تغییر مکان بدون تغییر شماره")]
            UnChangeNumber = 2
        }

        public enum BlackListType : byte
        {
            [Description("تلفن")]
            TelephoneNo = 1,

            [Description("مشترک")]
            Customer = 2,

            [Description("آدرس")]
            Address = 3
        }

        public enum Failure117LineStatus : byte
        {
            [Description("فیزیکی - MDF")]
            PhysicalMDF = 1,

            [Description("قانونی")]
            Legal = 2,

            [Description("نرم افزاری")]
            Operational = 3,

            [Description("فیزیکی - شبکه")]
            PhysicalNetwork = 4,
        }

        public enum PCMAorB : byte
        {
            [Description("A")]
            A = 1,

            [Description("B")]
            B = 2,
            [Description("عادی")]
            Normal = 3
        }

        public enum Failure117ActionStatus : byte
        {
            [Description("ارجاع به شبکه")]
            ReferenceNetwork = 1,

            //[Description("ارجاع به کابل")]
            //ReferenceCabel = 2,

            [Description("ارجاع به سالن دستگاه")]
            ReferenceSaloon = 3,

            [Description("رفع خرابی")]
            RemovalFailure = 4
        }

        public enum Failure117AvalibilityStatus
        {
            [Description("ام دی اف - بررسی درخواست")]
            MDFAnalysis = 1,

            [Description("شبکه هوایی")]
            Network = 2,

            [Description("کابل")]
            Cable = 3,

            [Description("سالن دستگاه")]
            Saloon = 4,

            [Description("ام دی اف - تایید درخواست")]
            MDFConfirm = 5,

            [Description("درخواست های تایید شده")]
            Archived = 6
        }

        public enum FailureResultAfterReturn
        {
            [Description("بررسی نشده")]
            NoResult = 0,

            [Description("سالم")]
            Healthy = 1,

            [Description("بدهی آبونمان")]
            Billing = 2,

            [Description("خراب")]
            Fail = 3
        }

        public enum FailureSpeed
        {
            [Description("1 ساعت")]
            Hour1 = 1,

            [Description("2 ساعت")]
            Hour2 = 2,

            [Description("3 ساعت")]
            Hour3 = 3,

            [Description("4 ساعت")]
            Hour4 = 4,

            [Description("5 ساعت")]
            Hour5 = 5,

            [Description("6 ساعت")]
            Hour6 = 6,

            [Description("12 ساعت")]
            Hour12 = 12,

            [Description("24 ساعت")]
            Hour24 = 24,

            [Description("36 ساعت")]
            Hour36 = 36,

            [Description("48 ساعت")]
            Hour48 = 48,

            [Description("72 ساعت")]
            Hour72 = 72,

            [Description("72 بیش از ساعت")]
            Hour73 = 73
        }
        public enum Failure117RequestStatus : byte
        {
            [Description("رفع شده")]
            Eliminate = 1,

            [Description("درحال بررسی")]
            InProcess = 2,
            // برای زمانی که ChekablaItem همه را برگردان استفاده میشود
            [Description("همه موارد")]
            All = 255,
        }
        public enum TimeTable : byte
        {
            [Description("روزانه")]
            Daily = 1,

            [Description("هفته جاری")]
            Weekly = 2,
            [Description("ماه جاری")]
            Mounthly = 3,
            [Description("سال جاری")]
            Yearly = 4,
            // برای زمانی که ChekablaItem همه را برگردان استفاده میشود
            [Description("همه موارد")]
            All = 255
        }

        public enum ADSLIPGroupType : byte
        {
            [Description("IP اینترنتی")]
            InternetIP = 1,

            [Description("IP اینترانتی")]
            IntranetIP = 2
        }
        public enum LogType
        {
            [Description("تخلیه تلفن قدیم ")]
            OldTelephoneDischarge = 1,
            [Description("انتقال ویژگی ها")]
            TransforFeature = 6
        }

        public enum ProposedFacilityType
        {
            [Description("بازدیدازمحل")]
            VisitPlaces = 1,

            [Description("کارکن درمحل")]
            NearestTelephone = 2,

            [Description("سابقه تلفن")]
            PassTelephone = 3,

            [Description("تلفن فعلی")]
            CurrentTelephone = 4,
        }

        public enum E1ChanalType
        {
            [Description("ورودی")]
            InComing = 1,

            [Description("خروجی")]
            OutComing = 2,

            [Description("ورودی خروجی")]
            Duplex = 3,
        }

        public enum E1Type
        {
            [Description("سیم")]
            Wire = 1,

            [Description("فیبر")]
            Fiber = 2,


            [Description("تخلیه سیم")]
            VacateWire = 3,
        }

        public enum ADSLServiceCenterMode
        {
            [Description("سرویس")]
            Service = 1,

            [Description("گروه سرویس")]
            ServiceGroup = 2
        }

        public enum ADSLServiceSellerMode
        {
            [Description("سرویس")]
            Service = 1,

            [Description("گروه سرویس")]
            ServiceGroup = 2
        }

        public enum ADSLSellChanell
        {
            [Description("حضوری")]
            Person = 1,

            [Description("تلفنی")]
            Telephonic = 2,

            [Description("اینترنتی")]
            Internet = 3,

            [Description("پیام کوتاه")]
            SMS = 4
        }

        public enum ADSLSellChanellLimited
        {
            [Description("حضوری")]
            Person = 1,

            [Description("اینترنتی")]
            Internet = 2,

            //[Description("غیر حضوری")]
            //NonAttendance = 3
        }

        public enum WaitingListReason
        {
            [Description("عدم وجود پورت خالی")]
            PortLess = 1
        }

        public enum Charts
        {
            [Description("نمودار درخواست های")]
            AllRequest = 1
        }

        public enum WatingListType
        {
            [Description("بررسی امکانات")]
            investigatePossibility = 1
        }

        public enum SMSServiceTitle
        {
            [Description("ثبت نام ADSL")]
            ADSLRegister = 1,

            [Description("شارژ مجدد")]
            ChangeService = 2,

            [Description("دایری ADSL")]
            ConnectADSL = 3,

            [Description("فروش ADSL")]
            SaleADSL = 4,

            [Description("ثبت نام Wireless")]
            WirelessRegister = 6,

            [Description("فروش Wireless")]
            WirelessSale = 7,

            [Description("دایری Wireless")]
            WirelessConnect = 8,

            [Description("خرید ترافیک")]
            SellTraffic = 9,
        }

        public enum ADSLIPType
        {
            [Description("تکی")]
            Single = 1,

            [Description("گروهی")]
            Group = 2
        }

        public enum ADSLGroupIPBlockCount
        {
            [Description("2 تایی")]
            Count2 = 2,

            [Description("4 تایی")]
            Count4 = 4,

            [Description("8 تایی")]
            Count8 = 8,

            [Description("16 تایی")]
            Count16 = 16,

            [Description("32 تایی")]
            Count32 = 32,

            [Description("64 تایی")]
            Count64 = 64,
        }

        public enum ADSLChangeServiceType
        {
            [Description("حضوری")]
            Presence = 1,

            [Description("اینترنتی")]
            Internet = 2
        }

        //TODO:rad 13950727
        /// <summary>
        /// روش پرداخت هزینه اتصال تلفن
        /// </summary>
        public enum MethodOfPaymentForTelephoneConnection
        {
            [Description("نقدی")]
            Cash,

            [Description("قسطی")]
            Installment,

            [Description("نامشخص")]
            Unknown,

            /// <summary>
            /// مقدار سایر به علت فکس کرمانشاه در تاریخ 13950819 به شماره نامه 820/56/45948/تعریف شد 
            /// چنانچه در هنگام دایری تلفن ثابت ، گروه تلفن آن موقت ، مخابرات ، همگانی تعیین شده باشد نباید هزینه پرداختی آن چک شود
            /// RequestForm.Forward()
            /// </summary>
            [Description("سایر")]
            Other

        }

        public enum PaymentType
        {
            [Description("بدون هزینه")]
            NoPayment = 0,

            [Description("نقدی")]
            Cash = 1,

            [Description("اولین قبض")]
            AbonmanBill = 2,

            [Description("اقساط")]
            Instalment = 3,

            [Description("دوره ای")]
            Period = 4,

            [Description("نقدی - غیراجباری")]
            NonForcedCash = 5
        }

        public enum PaymentWay
        {
            [Description("بانک")]
            Bank = 1,

            [Description("دستگاه Pos")]
            Pos = 2,

            [Description("اینترنتی")]
            Internet = 3,

            [Description("حضوری")]
            Person = 4,
        }

        public enum SpecialCostID
        {
            [Description("شناسه هزینه پیش پرداخت")]
            PrePaymentTypeCostID = -1,

            [Description("شناسه هزینه سرویس های ویژه")]
            SpecialServiceTypeCostID = -2,

            [Description("شناسه هزینه سیم خصوصی بین دو مرکز")]
            BetweenCenterSpecialWireCostID = -3,

            [Description("شناسه هزینه بستن صفر دوم")]
            BlockSecondZero = -4,

            [Description("شناسه هزینه باز کردن صفر دوم")]
            OpenSecondZero = -5,
        }

        public enum SubsidiaryCodeType
        {
            [Description("دایری تلفن ثابت")]
            Telephone = 1,

            [Description("خدمات تلفن ثابت")]
            Service = 2,

            [Description("ADSL")]
            ADSL = 3
        }

        public enum ClassTelephone : int
        {
            [Description("بدون محدودیت")]
            LimitLess = 1,

            [Description("صفر بین شهری")]
            FirstZeroBlock = 2,

            [Description("صفر بین الملل")]
            SecondZeroBlock = 3


        }

        public enum ShahkarAuthenticationStatus
        {
            [Description("احراز شده است")]
            IsAunthenticated = 1,

            [Description("احراز نشده است")]
            NotAunthenticated,

            [Description("درخواستی برای احراز نداشته است")]
            HasNoAunthenticationRequest,

            [Description("همه")]
            All
        }

        public enum ShahkarActionType : int
        {
            [Description("احراز هویت مشترک حقیقی ایرانی")]
            IranianPersonCustomerAuthentication = 1,

            [Description("دایری تلفن مشترک حقیقی ایرانی")]
            IranianPersonCustomerInstallTelephone
        }

        public enum CompanyType
        {
            [Description("سهامی عام")]
            PublicJointStock = 1,

            [Description("سهامی خاص")]
            PrivateJointStock = 2,

            [Description("با مسئولیت محدود")]
            LimitedResponsibility = 3,

            [Description("تضامنی")]
            Partnership = 4,

            [Description("مختلط غیر سهامی")]
            MixedNonStock = 5,

            [Description("مختلط سهامی")]
            MixedStock = 6,

            [Description("نسبی")]
            Relative = 7,

            [Description("تعاونی")]
            Cooperative = 8,

            [Description("دولتی")]
            Governmental = 9,

            [Description("وزارت خانه")]
            Ministry = 10,

            [Description("سفارت خانه")]
            Embassy = 11,

            [Description("مسجد")]
            Mosque = 12,

            [Description("مدرسه")]
            School = 13,

            [Description("NGO")]
            NGO = 14
        }

        public enum PaymentServiceType
        {
            [Description("خرید سرویس")]
            PurchaseService = 1,

            [Description("تمدید سرویس")]
            Charge = 2,

            [Description("خرید ترافیک")]
            Traffic = 3,

            [Description("خرید IP")]
            PurchaseIP = 4
        }

        public enum TelephoneUsageType
        {
            [Description("عادی")]
            Usuall = 0,

            [Description("E1")]
            E1 = 1,

            [Description("سیم خصوصی")]
            PrivateWire = 2,

            [Description("GSM")]
            GSM = 3,
        }

        public enum ADSLSaleWays
        {
            [Description("مخابرات")]
            Contacts = 1,

            [Description("دفتر خدماتی")]
            OfficeServices = 2,

            [Description("اینترنتی")]
            Internet = 3,

            [Description("تلفنی")]
            Telephone = 4
        }

        public enum SpecialConditions
        {
            [Description("")]
            NuLL = -1,

            [Description("برابری نوع بوخت(طرف مشترک)")]
            EqualityOfBuchtTypeCusromerSide = 1,

            [Description("برگشت داده شده از شبکه هوایی")]
            ReturnedFromWiring = 2,

            [Description("آزاد بودن تلفن قدیم")]
            BeFreeOldPhone = 3,

            [Description("تغییر مکان خودی")]
            ChangeLocationInsider = 4,

            [Description("بدهکار بودن مشترک")]
            IsDebt = 5,

            [Description("کافو نوری")]
            IsOpticalCabinet = 6,

            [Description("برابری نوع بوخت(نوری)")]
            EqualityOfBuchtTypeOptical = 7,

            [Description("عدم برابری نوع بوخت")]
            NotEqualityOfBuchtType = 8,

            [Description("عدم برابری نوع بوخت(نوری)")]
            NotEqualityOfBuchtTypeOptical = 9,

            [Description("نقطه میانی سیم خصوصی")]
            MiddlePointSpecialWire = 10,

            [Description("E1 فیبر")]
            IsOpticalE1 = 11,

            [Description("PTP")]
            IsE1PTP = 12,

            [Description("ISGSM")]
            IsGSM = 13,

        }
        public enum ADSLRequestStep
        {
            [Description("ثبت درخواست")]
            SubmitRequest = 95,

            [Description("فروش")]
            Sale = 97,

            [Description("واگذاری خطوط")]
            LineAssignment = 98,

            [Description("رییس مرکز")]
            CenterPresident = 99,

            [Description("MDF")]
            MDF = 100,

            [Description("اداره Data")]
            DataManagement = 101,

            [Description("پشتیبان خارجی")]
            ForeignSupport = 102,

            [Description("مرحله نهایی")]
            FinalStep = 103
        }

        public enum ADSLDischargeRequestStep
        {
            [Description("ثبت درخواست")]
            SubmitRequest = 114,

            [Description("MDF")]
            MDF = 115

        }

        public enum ADSLInstallRequest
        {
            [Description("فروش")]
            Sale = 298,

            [Description("پشتیبان خارجی")]
            ForeignSupport = 299

        }

        public enum ADSLChangePlaceRequestStep
        {
            [Description("ثبت درخواست")]
            SubmitRequest = 326,

            [Description("ام دی اف - تخلیه")]
            MDFDischarge = 327,

            [Description("ام دی اف - دایری")]
            MDFInstall = 328
        }

        public enum CauseOfCut : int
        {
            [Description("به درخواست مشترک")]
            CustomerRequest = 2,

            [Description("سیم کشی غیر مجاز")]
            WiringIllegal = 4,

            [Description("مزاحمت بار سوم")]
            PerturbedThird = 7,

        }

        public enum CauseOfChangeNo : int
        {
            //TODO:rad هر چیزی که در دیتابیس رید انلی میشود اینجا باید آورده شود
            [Description("به دلیل فنی بدون هزینه")]
            TechnicalWithoutCost = 6
        }

        public enum CauseOfTakePossession : int
        {
            [Description("جمع آوری منصوبات")]
            CollectingEquipment = 13
        }

        public enum PCMChangeStatus
        {
            [Description("جمع آوری پی سی ام")]
            PCMDrop = 13,

            [Description("انتقال پی سی ام")]
            PCMTransfer = 14,

            [Description("ایجاد پی سی ام")]
            PCMCreate = 15,

            [Description("نصب پی سی ام")]
            PCMInstall = 16,

            [Description("اصلاح پی سی ام")]
            PCMEdit = 23,

            [Description("اصلاح مشخصات پی سی ام")]
            PCMEditInfo = 24,

            [Description("حذف پی سی ام")]
            PCMDelete = 25,

        }

        public enum PersianMonth
        {
            [Description("فروردین")]
            Farvardin = 1,

            [Description("اردیبهشت")]
            Ordibehesht = 2,

            [Description("خرداد")]
            Khordad = 3,

            [Description("تیر")]
            Tir = 4,

            [Description("مرداد")]
            Mordad = 5,

            [Description("شهریور")]
            Shahrivar = 6,

            [Description("مهر")]
            Mehr = 7,

            [Description("آبان")]
            Aban = 8,

            [Description("آذر")]
            Azar = 9,

            [Description("دی")]
            dey = 10,

            [Description("بهمن")]
            Bahman = 11,

            [Description("اسفند")]
            Esfand = 12

        }

        public enum ADSLSellerGroupType
        {
            [Description("دفتر خدماتی")]
            OfficeService = 1,

            [Description("بازاریاب")]
            Marketer = 2
        }

        public enum ADSLChangeIPType
        {
            [Description("افزودن IP")]
            AddIP = 1,

            [Description("تعویض IP")]
            ChangeIP = 2,

            [Description("حذف IP")]
            DischargeIP = 3
        }

        public enum ADSLAAAction
        {
            [Description("تغییر رمز ورود")]
            ChangePassword = 1,

            [Description("حذف آدرس فیزیکی")]
            DeleteMACAddress = 2,

            [Description("تغییر آدرس فیزیکی")]
            ChangeMACAddress = 3,

            [Description("صفر کردن روزهای باقیمانده")]
            DeleteRemainDay = 4,

            [Description("Kill کردن کاربر")]
            KillUser = 5,

            [Description("Lock کردن کاربر")]
            LockUser = 6,

            [Description("UnLock کردن کاربر")]
            UnLockUser = 7,

            [Description("تغییر چندین اتصال")]
            ChangeMultiLogin = 8,

            [Description("تغییر رمز ورود پنل مشترکین")]
            ChangeWebPassword = 9
        }

        public enum DDFType : byte
        {
            [Description("پشتیبانی فنی")]
            TechnicalSupport = 1,

            [Description("سالن سوئیچ")]
            SalonSwitch = 2
        }

        public enum ADSLChangeServiceActionType
        {
            [Description("ارتقاء سرویس")]
            ChangeService = 1,

            [Description("تمدید سرویس")]
            ExtensionService = 2
        }

        public enum ADSLModemStatus : byte
        {
            [Description("رزرو")]
            Reserve = 0,

            [Description(" فروخته شده")]
            Sold = 1,

            [Description("فروخته نشده")]
            NotSold = 2
        }
        public enum CauseBuchtSwitching : int
        {
            [Description("خرابی بوخت")]
            BuchtBroking = 1,
            [Description("خرابی مرکزی")]
            CabinetInputBroking = 6,
            [Description("خرابی بوخت و مرکزی")]
            BuchtAndCabinetInputBroking = 7,

        }

        public enum E1NumberStatus : int
        {
            [Description("آزاد")]
            Free = 1,

            [Description("متصل")]
            Connection = 2,

            [Description("خراب")]
            Broke = 3,

            [Description("رزرو")]
            Reserve = 4,
        }

        public enum TelRoundSaleStatus : int
        {
            [Description("در مزایده")]
            InSale = 1,

            [Description("ثبت  قرارداد فروش")]
            regContract = 2,

        }

        public enum InstallRequestType : int
        {
            [Description("ثبت نام شده")]
            Registered = 1,

            [Description("دفتر حق تقدم")]
            Primary = 2,

            [Description("دایر شده")]
            Dayer = 3,

            [Description("در حال انجام")]
            Ongoing = 4
        }

        public enum IBSngStatus : byte
        {
            [Description("Null")]
            Null = 0,

            [Description("OK")]
            OK = 1,

            [Description("No")]
            No = 2
        }

        public enum TimeType : byte
        {
            [Description("ساعت")]
            Hour = 1,

            [Description("روز")]
            Day = 2,

            [Description("ماه")]
            Month = 3
        }

        public enum ADSLAgentLog : byte
        {
            [Description("شارژ مجدد اینترنتی")]
            SellServiceInternet = 1,

            [Description("خرید ترافیک اینترنتی")]
            SellTrafficInternet = 2,

            [Description("شارژ مجدد CRM")]
            SellServiceCRM = 3,

            [Description("خرید ترافیک CRM")]
            SellTrafficCRM = 4,

            [Description("تنظیم تاریخ های ADSL")]
            CheckADSLDate = 5,

            [Description("ارسال SMS 7 روزه")]
            SendMessage7Days = 6,

            [Description("ارسال SMS 1 روزه")]
            SendMessage1Days = 7
        }

        public enum ADSLSellerType : byte
        {
            [Description("دفتر نماینده فروش")]
            SellerAgent = 1,

            [Description("کاربر نماینده فروش")]
            SellerAgentUser = 2
        }

        public enum ListType : byte
        {
            [Description("لیست پورت های ام دی اف ADSL ")]
            ADSLMDFPortList = 1,
        }

        public enum ADSLSaleType : byte
        {
            [Description("سرویس")]
            ADSLService = 1,

            [Description("ترافیک")]
            ADSLTraffic = 2,

            [Description("IP")]
            ADSLIP = 3,
            [Description("مودم")]
            ADSLModem = 4,

            [Description("رانژه")]
            ADSLRanje = 5,

            [Description("راه اندازی")]
            ADSLInstallment = 6,

            [Description("تعویض شماره")]
            ADSLChangeNo = 7

        }

        public enum ADSLSaleTypeDetails : byte
        {
            [Description("سرویس")]
            ADSLService = 1,

            [Description("ترافیک")]
            ADSLTraffic = 2,

            [Description("تمدید سرویس")]
            ADSLExtentionService = 3,

            [Description("ارتقاء سرویس")]
            ADSLChangeService = 4,

            [Description("IP")]
            ADSLIP = 5,

            [Description("IPتمدید")]
            ADSLIPExtention = 6,

            [Description("مودم")]
            ADSLModem = 7,

            [Description("رانژه")]
            ADSLRanje = 8,

            [Description("راه اندازی")]
            ADSLInstallment = 9
        }

        public enum IsAccepted : int
        {
            [Description("وصول شده")]
            IsAccepted = 1,

            [Description("وصول نشده")]
            IsNotAccepted = 2

        }

        public enum TelephoneType2 : int
        {

            [Description("عادی")]
            Usual = 0,

            [Description("E1")]
            E1 = 1,

            [Description("سیم خصوصی")]
            PrivateWire = 2,

            [Description("رند")]
            IsRound = 3
        }

        public enum CutOrEstablishStatusForTelephone : int
        {
            [Description("وصل")]
            Connect = 1,

            [Description("قطع")]
            Disconnected = 2
        }

        public enum TranslationOpticalCabinetToNormalType : byte
        {
            [Description("کلی")]
            General = 1,

            [Description("جزئی")]
            Slight = 2,

            [Description("پست")]
            Post = 3
        }

        public enum ExchangeGSMType : byte
        {
            [Description("GSM به معمولی")]
            GSMToNormal = 1,

            [Description("معمولی به GSM")]
            NormalToGSM = 2,

        }

        public enum ExchangeCabinetInputType : byte
        {
            [Description("کلی")]
            General = 1,

            [Description("جزئی")]
            Slight = 2,

            [Description("پست")]
            Post = 3,

            [Description("مرکزی")]
            CabinetInput = 4,
        }

        public enum ServiceHostCity : byte
        {
            [Description("Local")]
            Local = 1,

            [Description("کرمانشاه")]
            Kermanshah = 2,

            [Description("سمنان")]
            Semnan = 3,
        }

        public enum WarningHistory : int
        {

            [Description("رفع محدودیت")]
            LimitLess = 1,

            [Description("اخطار سیم کشی اول")]
            FirstWarning = 2,

            [Description("اخطار سیم کشی دوم")]
            SecondWarning = 3,

            [Description("اخطار سیم کشی سوم")]
            ThirdWarning = 4,

            [Description("بازداشت")]
            arrest = 5,

            [Description("اخطار مزاحمت اول")]
            FirstTroubleWarning = 6,

            [Description("اخطار مزاحمت دوم")]
            SecondTroubleWarning = 7,

            [Description("اخطار مزاحمت سوم")]
            ThirdTroubleWarning = 8,

        }

        public enum TranslationPCMToNormalType : byte
        {


            [Description("پی سی ام به معمولی")]
            PCMToNormal = 1,

            [Description("معمولی به پی سی ام")]
            NormalToPCM = 2,

            [Description("پی سی ام به پی سی ام")]
            PCMToPCM = 3
        }


        public enum MapShapeType : byte
        {
            [Description("مرکز")]
            Center = 1,

            [Description("کافو")]
            Cabinet = 2,

            [Description("پست")]
            Post = 3
        }

        public enum ADSLPAPRequestType
        {
            [Description("دایری")]
            Install = 46,

            [Description("تخلیه")]
            Discharge = 57,

            [Description("تعویض پورت")]
            Exchange = 67,
        }


        public enum SpecialWireType
        {
            [Description("اصلی")]
            General = 1,

            [Description("میانی")]
            Middle = 2,
        }


        public enum FormType
        {
            [Description("معمولی")]
            Normal = 1,

            [Description("روالی")]
            Request = 2,
        }

        public enum FormItemType
        {
            [Description("متنی")]
            TextBox = 1,

            [Description("تاریخ")]
            DatePicker = 2,

            [Description("انتخابی")]
            CheckBox = 3,

            [Description("عددی")]
            Numberic = 4,
        }

        public enum Condition
        {
            [Description("برابر")]
            Equal = 1,

            [Description("بزرگتر")]
            GreaterThan = 2,

            [Description("بزرگتر مساوی")]
            GreaterThanOrEqual = 3,

            [Description("کوچکتر")]
            LessThan = 4,

            [Description("کوچکتر مساوی")]
            LessThanOrEqual = 5,
        }


        public enum WirelessType
        {
            [Description("PTP")]
            PTP = 1,

            [Description("MPTP")]
            MPTP = 2,

            [Description("WiFi")]
            WiFi = 3,
        }

        public enum ChooseNumberType
        {
            [Description("مرتب")]
            Sort = 1,

            [Description("مشابه")]
            Similar = 2,

        }

        public enum ChargingGroup
        {
            [Description("مسکونی")]
            Normal = 0,

            [Description("تجاری")]
            Business = 1,

            //[Description("رایگان")]
            //Free = 2,

            [Description("اداری")]
            Ministerial = 3,

            [Description("اداری-تجاری")]
            MinisterialAndBusiness = 4,

        }

        public enum PAPRequestType : byte
        {
            [Description("PAP - دایری ADSL")]
            ADSLInstalPAPCompany = 46,

            [Description("PAP - تخلیه ADSL")]
            ADSLDischargePAPCompany = 57,

            [Description("PAP - تعویض پورت")]
            ADSLExchangePAPCompany = 67,
        }


        public enum ServicCallParameter : byte
        {
            [Description("با تلفن")]
            ByTelephone = 1,

            [Description("با کد ملی")]
            ByNationalCode = 2,
        }

        public enum ExchangeOrderType : byte
        {
            [Description("ترتیبی")]
            Sequential = 1,

            [Description("نظیر به نظیر")]
            Peer = 2,
        }

        public enum CRMServiceTechTelephoneType : byte
        {
            [Description("معمولی")]
            Normal = 1,

            [Description("پی سی ام")]
            PCM = 2,

            [Description("GSM")]
            GSM = 3,

            [Description("کافو نوری")]
            OpticalCabinet = 4,

            [Description("WLL")]
            Wll = 5,
        }
    }
}
