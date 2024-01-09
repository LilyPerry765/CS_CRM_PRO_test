using CRM.Application.Local;
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report;
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
    /// Interaction logic for TelecomminucationServicePaymentStatisticsReportUserControl.xaml
    /// </summary>
    public partial class TelecomminucationServicePaymentStatisticsReportUserControl : ReportBase
    {
        #region Constructor
        public TelecomminucationServicePaymentStatisticsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        public override void Search()
        {
            try
            {
                if (RequestTypeComboBox.SelectedValue != null)
                {
                    //TODO:rad
                    long requestId = (!string.IsNullOrEmpty(RequestIDNumericTextBox.Text.Trim())) ? Convert.ToInt64(RequestIDNumericTextBox.Text.Trim()) : -1;
                    int requestTypeId = Convert.ToInt32(RequestTypeComboBox.SelectedValue);

                    //دیتای مورد نیاز برای ایجاد گزارش
                    List<CustomerReportInfo> customersInfo = new List<CustomerReportInfo>();
                    List<TelecomminucationServicePaymentReportInfo> telecomminucationServicePaymentReportInfo = ReportDB.SearchTelecomminucationServicePaymentStatistics(NationalCodeOrRecordNoTextBox.Text.Trim(), requestId, requestTypeId, out customersInfo);

                    StiReport report = new StiReport();
                    string path = string.Empty;
                    switch (requestTypeId)
                    {
                        case (int)DB.RequestType.SpaceandPower:
                            {
                                path = ReportDB.GetReportPath((int)DB.UserControlNames.TelecomminucationServicePaymentStatisticsReport);
                                break;
                            }
                        case (int)DB.RequestType.E1:
                            {
                                path = ReportDB.GetReportPath((int)DB.UserControlNames.TelecomminucationServicePaymentStatisticsWithInstallAndTargetAddressForE1Report);
                                break;
                            }
                    }
                    report.Load(path);
                    report.RegData("Categories", "Categories", customersInfo);
                    report.RegData("Products", "Products", telecomminucationServicePaymentReportInfo);
                    report.Dictionary.Variables["ReportDate"].ValueObject = DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                    report.CacheAllData = true;
                    ReportViewerForm reportViewer = new ReportViewerForm(report);
                    reportViewer.ShowDialog();
                }
                else
                {
                    MessageBox.Show(".برای ایجاد گزارش تعیین نوع درخواست الزامی است", "توجّه", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش امور  مشترکین - گزارش آمار صورتحساب فروش کالا و خدمات");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Initialize()
        {
            RequestTypeComboBox.ItemsSource = RequestTypeDB.GetAllEntity();
        }

        #endregion
    }
}
