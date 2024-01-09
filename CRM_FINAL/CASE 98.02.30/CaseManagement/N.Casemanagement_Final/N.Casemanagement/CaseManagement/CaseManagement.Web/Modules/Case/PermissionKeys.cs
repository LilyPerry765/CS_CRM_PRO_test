
namespace CaseManagement.Case
{
    /// <summary>
    /// This class contains some permission key constants solely for
    /// easy access and intellisense purposes.
    /// 
    /// Please note that adding a permission here won't show it
    /// in user permissions dialog. In fact, Serenity doesn't
    /// care about this class at all.
    /// 
    /// To show a new permission in user/role permission dialog, just use
    /// its string key with ReadPermission / ModifyPermission / 
    /// DeletePermission / PageAuthorize / ServiceAuthorize etc. attributes 
    /// and Serenity will auto discover them at application start.
    /// 
    /// Permission tree hierarchy in dialog is determined by colons (:)
    /// in permission keys.
    /// </summary>
    public class PermissionKeys
    {
        public const string BasicInfo = "Case:BasicInfo";
        public const string Activity = "Case:Activity";
        public const string ActivityProvince = "Case:ActivityProvince";
        public const string RequestDeny = "Case:RequestDeny";
        public const string Report = "Case:Report";
        public const string Workflow = "Case:Workflow";
        public const string Manager = "Case:Manager";
        public const string Iran = "Case:Iran";
        public const string Ostan = "Case:Ostan";
        public const string JustRead = "Case:JustRead";
        public const string JustRead_Activity = "Case:JustRead|Case:Activity";
        public const string JustRead_WorkFlow = "Case:JustRead|Case:Workflow";
        public const string JustRead_BasicInfo = "Case:JustRead|Case:BasicInfo";
        public const string JustRead_Manager = "Case:JustRead|Case:Manager";
        public const string JustRead_ActivityProvince = "Case:JustRead|Case:ActivityProvince";
        
        

    }
}
