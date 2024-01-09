using CRM.Application.Reports.Viewer;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report;
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
    /// Interaction logic for WarningHistoryReport.xaml
    /// </summary>
    public partial class WarningHistoryReport : Local.ReportBase
    {
        #region Constructor

        public WarningHistoryReport()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            WarningHistoryTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.WarningHistory));
        }
        
        public override void Search()
        {
            //Before rad
            //List<WarningHistoryReportInfo> result = LoadData();
            //StiReport stiReport = new StiReport();
            //stiReport.Dictionary.DataStore.Clear();
            //stiReport.Dictionary.Databases.Clear();
            //stiReport.Dictionary.RemoveUnusedData();
            //string title = string.Empty;
            //string path = ReportDB.GetReportPath(UserControlID);
            //stiReport.Load(path);
            //title = " اخطار ها و توقیف ";
            //stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            //
            //
            //
            //stiReport.Dictionary.Variables["Header"].Value = title;
            //stiReport.RegData("Result", "Result", result);

            //ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            //reportViewerForm.ShowDialog();

            //TODO:rad 
            try
            {
                //آیا یک مقدار عددی برای شماره تلفن تعیین شده است است یا خیر
                long? telephoneNo = -1;
                if (!string.IsNullOrEmpty(TelephonNoTextBox.Text))
                {
                    bool telephoneNoIsValid = Helper.CheckDigitDataEntry(TelephonNoTextBox, out telephoneNo);
                    if (!telephoneNoIsValid)
                    {
                        MessageBox.Show(".برای تعیین «شماره تلفن» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                        TelephonNoTextBox.Focus();
                        return;
                    }
                }

                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = ReportDB.GetWarningHistory(
                                                                CityComboBox.SelectedIDs,
                                                                CenterComboBox.SelectedIDs,
                                                                FromDatePicker.SelectedDate,
                                                                ToDatePicker.SelectedDate,
                                                                telephoneNo.Value,
                                                                WarningHistoryTypeComboBox.SelectedIDs
                                                              );

                if (primaryResult.Count() > 0)
                {
                    //StiVariables
                    StiVariable dateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
                    StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));
                    StiVariable cityVariable = new StiVariable("CityName", "CityName", DB.PersianCity);
                    StiVariable fromDateVariable = new StiVariable();
                    fromDateVariable.Name = "FromDate";
                    fromDateVariable.Category = "FromDate";
                    fromDateVariable.ValueObject = (FromDatePicker.SelectedDate.HasValue) ? FromDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short) : "-----";
                    StiVariable toDateVariable = new StiVariable();
                    toDateVariable.Name = "ToDate";
                    toDateVariable.Category = "ToDate";
                    toDateVariable.ValueObject = (ToDatePicker.SelectedDate.HasValue) ? ToDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short) : "-----";

                    //تنظیمات برای نمایش
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.WarningHistoryReport, true, dateVariable, timeVariable, cityVariable, fromDateVariable, toDateVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش امور  مشترکین - گزارش توقیف");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Before rad
        //private List<WarningHistoryReportInfo> LoadData()
        //{
        //    long telephone = -1;
        //    if (!long.TryParse(TelephonNoTextBox.Text, out telephone)) telephone = -1;
        //
        //
        //    List<WarningHistoryReportInfo> result =
        //    ReportDB.GetWarningHistory(
        //    CityComboBox.SelectedIDs,
        //    CenterComboBox.SelectedIDs,
        //    FromDate.SelectedDate,
        //    ToDate.SelectedDate,
        //    telephone,
        //    TypeComboBox.SelectedIDs
        //    );
        //
        //    result.ForEach(t =>
        //    {
        //        t.InsertDatePersian = (string.IsNullOrEmpty(t.InsertDate.ToString())) ? "" : Helper.GetPersianDate(t.InsertDate, Helper.DateStringType.Short);
        //        t.DatePersian = (string.IsNullOrEmpty(t.Date.ToString())) ? "" : Helper.GetPersianDate(t.Date, Helper.DateStringType.Short);
        //    }
        //    );
        //
        //    return result;
        //}

        #endregion

        #region EventHandlers
        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        #endregion

    }
}
