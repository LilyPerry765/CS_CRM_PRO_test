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
using System.Windows.Shapes;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for SwapTelephoneReportUserControl.xaml
    /// </summary>
    public partial class SwapTelephoneReportUserControl : ReportBase
    {
        #region Constructor
        public SwapTelephoneReportUserControl()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        public override void Search()
        {
            //TODO:rad  
            try
            {
                DateTime? fromDate = (FromDatePicker.SelectedDate.HasValue) ? FromDatePicker.SelectedDate : null;
                DateTime? toDate = (ToDatePicker.SelectedDate.HasValue) ? ToDatePicker.SelectedDate : null;

                long? telephoneNo = null;
                long? toTelephoneNo = null;

                bool telephoneNoIsValid = Helper.CheckDigitDataEntry(TelephoneNoTextBox, out telephoneNo);
                bool toTelphoneNoIsValid = Helper.CheckDigitDataEntry(ToTelephoneNoTextBox, out toTelephoneNo);

                if (!toTelphoneNoIsValid || !telephoneNoIsValid)
                {
                    MessageBox.Show(".برای تعیین شماره تلفن فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var primaryResult = RequestLogDB.GetSwapTelephoneRequestLogReportList(telephoneNo, toTelephoneNo, fromDate, toDate, CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterComboBox.SelectedIDs);
                if (primaryResult.Count != 0)
                {
                    //لیست زیر به عنوان دیتا برای ایجاد گزارش ارسال خواهد شد
                    List<SwapTelephoneRequestLog> swapTelephoneRequestLogListForReport = new List<SwapTelephoneRequestLog>();

                    //به ازای هر لاگ ثبت شده باید یک رکورد لاگ تعویض شماره برای گزارش آماده کنیم
                    foreach (RequestLogReport requestLogReport in primaryResult)
                    {
                        SwapTelephoneRequestLog swapTelephoneRequestLogItem = new SwapTelephoneRequestLog();

                        //مقداردهی فیلدهای گزارش 
                        swapTelephoneRequestLogItem.UserName = requestLogReport.UserName;
                        swapTelephoneRequestLogItem.TelephoneNo = requestLogReport.TelephoneNo.HasValue ? requestLogReport.TelephoneNo.Value.ToString() : string.Empty;
                        swapTelephoneRequestLogItem.ToTelephoneNo = requestLogReport.ToTelephone.HasValue ? requestLogReport.ToTelephone.Value.ToString() : string.Empty;

                        //حالا باید ریز اطلاعات تعویش شماره را بدست بیاوریم
                        if (requestLogReport.LogEntityDetails != null && requestLogReport.LogEntityDetails is CRM.Data.Schema.SwapTelephone)
                        {
                            CRM.Data.Schema.SwapTelephone swapTelephoneDetail = requestLogReport.LogEntityDetails as CRM.Data.Schema.SwapTelephone;

                            swapTelephoneRequestLogItem.Date = swapTelephoneDetail.Date.ToPersian(Date.DateStringType.Short);
                            swapTelephoneRequestLogItem.FromBuchtID = swapTelephoneDetail.FromBuchtID.ToString();
                            swapTelephoneRequestLogItem.FromCabinet = swapTelephoneDetail.FromCabinet.ToString();
                            swapTelephoneRequestLogItem.FromCabinetID = swapTelephoneDetail.FromCabinetID.ToString();
                            swapTelephoneRequestLogItem.FromCabinetInput = swapTelephoneDetail.FromCabinetInput.ToString();
                            swapTelephoneRequestLogItem.FromCabinetInputID = swapTelephoneDetail.FromCabinetInputID.ToString();
                            swapTelephoneRequestLogItem.FromConnectionNo = swapTelephoneDetail.FromConnectionNo;
                            swapTelephoneRequestLogItem.FromCustomerID = swapTelephoneDetail.FromCustomerID.ToString();
                            swapTelephoneRequestLogItem.FromCustomerName = swapTelephoneDetail.FromCustomerName;
                            swapTelephoneRequestLogItem.FromPost = swapTelephoneDetail.FromPost.ToString();
                            swapTelephoneRequestLogItem.FromPostContact = swapTelephoneDetail.FromPostContact.ToString();
                            swapTelephoneRequestLogItem.FromPostContactID = swapTelephoneDetail.FromPostContactID.ToString();
                            swapTelephoneRequestLogItem.FromPostID = swapTelephoneDetail.FromPostID.ToString();

                            swapTelephoneRequestLogItem.ToBuchtID = swapTelephoneDetail.ToBuchtID.ToString();
                            swapTelephoneRequestLogItem.ToCabinet = swapTelephoneDetail.ToCabinet.ToString();
                            swapTelephoneRequestLogItem.ToCabinetID = swapTelephoneDetail.ToCabinetID.ToString();
                            swapTelephoneRequestLogItem.ToCabinetInput = swapTelephoneDetail.ToCabinetInput.ToString();
                            swapTelephoneRequestLogItem.ToCabinetInputID = swapTelephoneDetail.ToCabinetInputID.ToString();
                            swapTelephoneRequestLogItem.ToConnectionNo = swapTelephoneDetail.ToConnectionNo;
                            swapTelephoneRequestLogItem.ToCustomerID = swapTelephoneDetail.ToCustomerID.ToString();
                            swapTelephoneRequestLogItem.ToCustomerName = swapTelephoneDetail.ToCustomerName;
                            swapTelephoneRequestLogItem.ToPost = swapTelephoneDetail.ToPost.ToString();
                            swapTelephoneRequestLogItem.ToPostContact = swapTelephoneDetail.ToPostContact.ToString();
                            swapTelephoneRequestLogItem.ToPostContactID = swapTelephoneDetail.ToPostContactID.ToString();
                            swapTelephoneRequestLogItem.ToPostID = swapTelephoneDetail.ToPostID.ToString();
                        }
                        swapTelephoneRequestLogListForReport.Add(swapTelephoneRequestLogItem);
                    }

                    StiVariable variable = new StiVariable("ReportDate", "ReportDate", Folder.PersianDate.ToPersianDateString(DB.GetServerDate()));
                    ReportBase.SendToPrint(swapTelephoneRequestLogListForReport, (int)DB.UserControlNames.SwapTelephoneReport, variable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.Write(ex, "خطا در لیست گزارش ها - گزارش تعویض شماره");
            }
        }

        #endregion
    }
}
