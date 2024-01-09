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
using CRM.Data;
using Stimulsoft.Report;
using Stimulsoft.Base;
using System.Reflection;
using CRM.Application.Reports.Viewer;
using Enterprise;
using Stimulsoft.Report.Dictionary;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for SpecialServiceStatisticsReportUserControl.xaml
    /// </summary>
    public partial class SpecialServiceStatisticsReportUserControl : Local.ReportBase
    {
        #region Constructor

        public SpecialServiceStatisticsReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Consructor

        #region Methods

        //TODO:rad
        public override void Search()
        {
            //Before rad
            //List<SpecialServiceInfo> Result = LoadData();
            //string title = string.Empty;
            //string path;
            //StiReport stiReport = new StiReport();
            //stiReport.Dictionary.DataStore.Clear();
            //stiReport.Dictionary.Databases.Clear();
            //stiReport.Dictionary.RemoveUnusedData();
            //
            //path = ReportDB.GetReportPath(UserControlID);
            //stiReport.Load(path);
            //stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            //stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            //
            //title = "گزارش آمار سرویس های ویژه ";
            //stiReport.Dictionary.Variables["Header"].Value = title;
            //stiReport.RegData("Result", "Result", Result);
 
            //ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            //reportViewerForm.ShowDialog();
            try
            {
                int cityId = (CitiesComboBox.SelectedValue != null) ? Convert.ToInt32(CitiesComboBox.SelectedValue) : -1;
                int centerId = (CentersComboBox.SelectedValue != null) ? Convert.ToInt32(CentersComboBox.SelectedValue) : -1;
                long fromTelNo = (!string.IsNullOrEmpty(FromTelNoTextBox.Text.Trim()) ? Convert.ToInt64(FromTelNoTextBox.Text.Trim()) : -1);
                long toTelNo = (!string.IsNullOrEmpty(ToTelNoTextBox.Text.Trim()) ? Convert.ToInt64(ToTelNoTextBox.Text.Trim()) : -1);

                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = ReportDB.GetSpecialServiceStatisticsInfo(FromDate.SelectedDate,
                                                                             ToDate.SelectedDate,
                                                                             fromTelNo,
                                                                             toTelNo,
                                                                             SpecialserviceComboBox.SelectedIDs,
                                                                             StatusSpecialServiceComboBox.SelectedIDs,
                                                                             cityId,
                                                                             centerId);
                
                if (!primaryResult.IsNullOrEmpty())
                {
                    //StiVariables
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
                    StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));
                    StiVariable cityVariable = new StiVariable("CityName", "CityName", DB.PersianCity);

                    //تنظیمات برای نمایش گزارش
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.SpecialServiceStatisticsReport, true, dateVariable, timeVariable, cityVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش امور مشترکین - گزارش آمار سرویس ویژه");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Initialize()
        {
            SpecialserviceComboBox.ItemsSource = SpecialServiceTypeDB.GetSpecialServiceTypeCheckable();
            StatusSpecialServiceComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.StatusSpecialService));
            CitiesComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        //private List<SpecialServiceInfo> LoadData()
        //{
        //    long? fromTelNo = !string.IsNullOrEmpty(FromTelNoTextBox.Text) ? Convert.ToInt64(FromTelNoTextBox.Text) : -1;
        //    long? toTelNo = !string.IsNullOrEmpty(ToTelNoTextBox.Text) ? Convert.ToInt64(ToTelNoTextBox.Text) : -1;

        //    List<SpecialServiceInfo> result = ReportDB.GetSpecialServiceStatisticsInfo(FromDate.SelectedDate, ToDate.SelectedDate, fromTelNo, toTelNo,
        //                                                                              SpecialserviceComboBox.SelectedIDs, ActivationTypeComboBox.SelectedIDs);
        //    return result;
        //}

        #endregion Methods

        #region EventHandlers

        //TODO:rad دراینجا رورش بهتری برای چک کردن مقادیر فیلتری که باید عددی باشند پیاده سازی کردم
        private void FiltersTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;
            if (source != null)
            {
                //آیا یک مقدار عددی برای از شماره-تا شماره تعیین شده است یا خیر
                long? enteredTelNo = 0;
                bool enterdTelNoIsValid = Helper.CheckDigitDataEntry(source, out enteredTelNo);
                if (!enterdTelNoIsValid)
                {
                    MessageBox.Show(".برای تعیین مقادیر «ازشماره» و «تا شماره» فقط از اعداد استفاده نمائید", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    source.Focus();
                }
                else
                {
                    //do nothing
                }
            }
        }

        private void CitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CitiesComboBox.SelectedValue != null)
            {
                CentersComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(new List<int> { (int)CitiesComboBox.SelectedValue });
                CentersComboBox.SelectedIndex = 0;
                CentersComboBox.Items.Refresh();
            }
        }

        #endregion

        
    }
}
