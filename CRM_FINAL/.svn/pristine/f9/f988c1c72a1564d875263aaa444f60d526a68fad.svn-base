using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ModifyProfileReportUserControl.xaml
    /// </summary>
    public partial class ModifyProfileReportUserControl : ReportBase
    {
        #region Constructor

        public ModifyProfileReportUserControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        //TODO:rad
        public override void Search()
        {
            try
            {
                DateTime? fromDate = (FromDatePicker.SelectedDate.HasValue) ? FromDatePicker.SelectedDate : null;
                DateTime? toDate = (ToDatePicker.SelectedDate.HasValue) ? ToDatePicker.SelectedDate : null;

                var primaryResult = RequestLogDB.GetRequestLogReportList(DB.RequestType.ModifyProfile, fromDate, toDate, CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs);
                if (primaryResult.Count != 0)
                {
                    //لیست زیر به عنوان دیتا برای ایجاد گزارش ارسال خواهد شد
                    List<ModifyProfileRequestLog> modifyProfileRequestLogListForReport = new List<ModifyProfileRequestLog>();

                    //باید به ازای هر لاگ ثبت شده یک رکورد اصلاح مشخصات برای گزارش آمده کنیم
                    foreach (RequestLogReport requestLogReport in primaryResult)
                    {
                        ModifyProfileRequestLog modifyProfileRequestLogItem = new ModifyProfileRequestLog();

                        //مقدار دهی فیلدها
                        modifyProfileRequestLogItem.UserName = requestLogReport.UserName;
                        modifyProfileRequestLogItem.OldTelephoneNo = (requestLogReport.TelephoneNo.HasValue) ? requestLogReport.TelephoneNo.Value.ToString() : string.Empty;
                        modifyProfileRequestLogItem.NewTelephoneNo = (requestLogReport.ToTelephone.HasValue) ? requestLogReport.ToTelephone.Value.ToString() : string.Empty;

                        //حالا باید ریز اطلاعات مربوط به اصلاح مشخصات را بدست بیاوریم
                        if (requestLogReport.LogEntityDetails != null && requestLogReport.LogEntityDetails is CRM.Data.Schema.ModifyProfile)
                        {
                            CRM.Data.Schema.ModifyProfile modifyProfileDetails = requestLogReport.LogEntityDetails as CRM.Data.Schema.ModifyProfile;

                            modifyProfileRequestLogItem.Date = modifyProfileDetails.Date.ToPersian(Date.DateStringType.Short);

                            modifyProfileRequestLogItem.OldCabinet = modifyProfileDetails.OldCabinet.ToString();
                            modifyProfileRequestLogItem.OldCabinetInput = modifyProfileDetails.OldCabinetInput.ToString();
                            modifyProfileRequestLogItem.OldConnectionNo = modifyProfileDetails.OldConnectionNo;
                            modifyProfileRequestLogItem.OldCorrespondenceContactAddress = modifyProfileDetails.OldCorrespondenceContactAddress;
                            modifyProfileRequestLogItem.OldCorrespondencePostalCodeInstall = modifyProfileDetails.OldCorrespondencePostalCodeInstall;
                            modifyProfileRequestLogItem.OldCustomerName = modifyProfileDetails.OldCustomerName;
                            modifyProfileRequestLogItem.OldInstallContactAddress = modifyProfileDetails.OldInstallContactAddress;
                            modifyProfileRequestLogItem.OldInstallPostalCodeInstall = modifyProfileDetails.OldInstallPostalCodeInstall;
                            modifyProfileRequestLogItem.OldPost = modifyProfileDetails.OldPost.ToString();
                            modifyProfileRequestLogItem.OldPostContact = modifyProfileDetails.OldPostContact.ToString();

                            modifyProfileRequestLogItem.NewCabinet = modifyProfileDetails.NewCabinet.ToString();
                            modifyProfileRequestLogItem.NewCabinetInput = modifyProfileDetails.NewCabinetInput.ToString();
                            modifyProfileRequestLogItem.NewConnectionNo = modifyProfileDetails.NewConnectionNo;
                            modifyProfileRequestLogItem.NewCorrespondenceContactAddress = modifyProfileDetails.NewCorrespondenceContactAddress;
                            modifyProfileRequestLogItem.NewCorrespondencePostalCodeInstall = modifyProfileDetails.NewCorrespondencePostalCodeInstall;
                            modifyProfileRequestLogItem.NewCustomerName = modifyProfileDetails.NewCustomerName;
                            modifyProfileRequestLogItem.NewInstallContactAddress = modifyProfileDetails.NewInstallContactAddress;
                            modifyProfileRequestLogItem.NewInstallPostalCodeInstall = modifyProfileDetails.NewInstallPostalCodeInstall;
                            modifyProfileRequestLogItem.NewPost = modifyProfileDetails.NewPost.ToString();
                            modifyProfileRequestLogItem.NewPostContact = modifyProfileDetails.NewPostContact.ToString();

                            //Check for null and empty values.
                            modifyProfileRequestLogItem.CheckMembersValue();
                        }

                        modifyProfileRequestLogListForReport.Add(modifyProfileRequestLogItem);
                    }

                    //تنظیمات برای نمایش گزارش
                    StiVariable variable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                    ReportBase.SendToPrint(modifyProfileRequestLogListForReport, (int)DB.UserControlNames.ModifyProfileReport, variable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex, "خطا در لیست گزارش ها - گزارش اصلاح مشخصات");
            }
        }

        #endregion
    }
}
