using CRM.Application.Local;
using CRM.Data;
using CRM.Data.Schema;
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
    /// Interaction logic for SwapPCMReportUserControl.xaml
    /// </summary>
    public partial class SwapPCMReportUserControl : ReportBase
    {
        #region Constructor
        public SwapPCMReportUserControl()
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

                var primaryResult = RequestLogDB.GetRequestLogReportList(DB.RequestType.SwapPCM, fromDate, toDate, CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs);

                if (primaryResult.Count != 0)
                {
                    //لیست زیر به عنوان دیتا برای ایجاد گزارش ارسال خواهد شد
                    List<SwapPCMRequestLog> swapPcmRequestLogListForReport = new List<SwapPCMRequestLog>();

                    //به ازای هر لاگ ثبت شده باید یک رکورد تعویض پی سی ام برای گزارش آمده کنیم
                    foreach (RequestLogReport requestLogReport in primaryResult)
                    {
                        SwapPCMRequestLog swapPcmRequestLogItem = new SwapPCMRequestLog();

                        //مقداردهی فیلدهای گزارش
                        swapPcmRequestLogItem.UserName = requestLogReport.UserName;
                        swapPcmRequestLogItem.FromTelephoneNo = (requestLogReport.TelephoneNo.HasValue) ? requestLogReport.TelephoneNo.Value.ToString() : string.Empty;
                        swapPcmRequestLogItem.ToTelephoneNo = (requestLogReport.ToTelephone.HasValue) ? requestLogReport.ToTelephone.Value.ToString() : string.Empty;

                        //حالا باید ریز اطلاعات تعویض پی سی ام را بدست بیاوریم
                        if (requestLogReport.LogEntityDetails != null && requestLogReport.LogEntityDetails is CRM.Data.Schema.SwapPCM)
                        {
                            CRM.Data.Schema.SwapPCM swapPcmDetails = requestLogReport.LogEntityDetails as CRM.Data.Schema.SwapPCM;

                            swapPcmRequestLogItem.Date = swapPcmDetails.Date.ToPersian(Date.DateStringType.Short);
                            swapPcmRequestLogItem.FromBucht = swapPcmDetails.FromBucht;
                            swapPcmRequestLogItem.FromMUID = swapPcmDetails.FromMUID;
                            swapPcmRequestLogItem.FromPCMBucht = swapPcmDetails.FromPCMBucht;
                            swapPcmRequestLogItem.ToBucht = swapPcmDetails.ToBucht;
                        }
                        swapPcmRequestLogListForReport.Add(swapPcmRequestLogItem);
                    }

                    //تنظیمات برای نمایش
                    StiVariable variable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                    ReportBase.SendToPrint(swapPcmRequestLogListForReport, (int)DB.UserControlNames.SwapPCMReport, variable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex, "خطا در لیست گزارش ها - گزارش تعویض پی سی ام");
            }
        }

        #endregion
    }
}
