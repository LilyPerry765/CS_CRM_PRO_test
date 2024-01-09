using Serenity.ComponentModel;
using System.ComponentModel;

namespace CaseManagement.Case
{
    [EnumKey("Case.RequestAction")]
    public enum RequestAction
    {        
        [Description("ذخیره موقت")]
        Save = 1,
        [Description("تایید و ارسال")]
        Forward = 2,
        [Description("بازگشت")]
        Deny = 3,
        [Description("حذف")]
        Delete= 4,
       // [Description("تایید گزارش اولیه فنی")]
       // ConfirmTechnical = 4,
       // [Description("تایید مالی")]
       // ConfirmFinancial = 5
    }

    [EnumKey("Case.ConfirmType")]
    public enum ConfirmType
    {
        [Description("گزارش اولیه فنی")]
        Technical = 1,
        [Description("تایید مالی")]
        Financial = 2
    }
}