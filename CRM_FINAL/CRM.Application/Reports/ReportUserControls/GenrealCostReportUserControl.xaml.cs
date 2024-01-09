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
    /// Interaction logic for GenrealCostReportUserControl.xaml
    /// </summary>
    public partial class GenrealCostReportUserControl : Local.ReportBase
    {
        #region Properties

        //TODO:rad باید کلیه جاهای که فیلتر تکتست باکس چند تا دارند از طریق روش این فرم پیاده سازی شوند
        /// <summary>
        /// .اگر این ویژگی درست باشد به معنای آن است که کاربر مقادیر عددی فیلتر ها را به درستی وارد کرده است در غیر این صورت مقدار ان نادرست است
        /// </summary>
        private bool FiltersValueIsNumeric { get; set; }

        #endregion

        #region Constructor

        public GenrealCostReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CostTypeComboBox.ItemsSource = RequestPaymentDB.GetCostTypeCheckable();
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            this.FiltersValueIsNumeric = true;
        }

        public override void Search()
        {
            //before rad
            //List<GeneralRequestPaymentInfo> Result = LoadData();
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
            //if (FromDate.SelectedDate != null)
            //    stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            //if (ToDate.SelectedDate != null)
            //    stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            //
            //
            //title = "گزارش کلی هزینه";
            //stiReport.Dictionary.Variables["Header"].Value = title;
            //stiReport.RegData("Result", "Result", Result);

            //ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            //reportViewerForm.ShowDialog();
            //TODO:rad EDIT
            try
            {
                if (this.FiltersValueIsNumeric)//در صورتی بلاک زیر اجرا شود که در صورت مقدار دهی ، مقادیر از شماره ، تا شماره ، از مبلغ  و تا مبلغ عددی باشند
                {
                    long fromTelephoneNo = !string.IsNullOrEmpty(FromTelephoneNoTextBox.Text) ? Convert.ToInt64(FromTelephoneNoTextBox.Text) : -1;
                    long toTelephoneNo = !string.IsNullOrEmpty(ToTelephoneNoTextBox.Text) ? Convert.ToInt64(ToTelephoneNoTextBox.Text) : -1;
                    long fromCostAmount = !string.IsNullOrEmpty(FromCostAmountTextBox.Text) ? Convert.ToInt64(FromCostAmountTextBox.Text) : -1;
                    long toCostAmount = !string.IsNullOrEmpty(ToCostAmountTextBox.Text) ? Convert.ToInt64(ToCostAmountTextBox.Text) : -1;

                    //دیتای مورد نیاز برای ایجاد گزارش
                    var primaryResult = ReportDB.GetGeneralRequestPaymentInfo(
                                                                                CityComboBox.SelectedIDs,
                                                                                CenterComboBox.SelectedIDs,
                                                                                FromDatePicker.SelectedDate,
                                                                                ToDatePicker.SelectedDate,
                                                                                fromTelephoneNo,
                                                                                toTelephoneNo,
                                                                                fromCostAmount,
                                                                                toCostAmount,
                                                                                CostTypeComboBox.SelectedIDs
                                                                              );
                    if (primaryResult.Count() > 0)
                    {
                        //StiVariables
                        StiVariable reportDateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
                        StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));
                        StiVariable fromDateVariable = new StiVariable();
                        fromDateVariable.Name = "FromDate";
                        fromDateVariable.Category = "FromDate";
                        fromDateVariable.ValueObject = (FromDatePicker.SelectedDate.HasValue) ? FromDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short) : "-----";
                        StiVariable toDateVariable = new StiVariable();
                        toDateVariable.Name = "ToDate";
                        toDateVariable.Category = "ToDate";
                        toDateVariable.ValueObject = (ToDatePicker.SelectedDate.HasValue) ? ToDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short) : "-----";
                        StiVariable cityVariable = new StiVariable("CityName", "CityName", DB.PersianCity);

                        //تنظیمات برای نمایش
                        CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.GenrealCostReport, true, cityVariable, timeVariable, reportDateVariable, fromDateVariable, toDateVariable);
                    }
                    else
                    {
                        MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(".برای تعیین مقادیر «از شماره» ، «تا شماره» ، «از مبلغ» و «تا مبلغ» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش امور مشترکین - گزارش کلی هزینه ها");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private List<GeneralRequestPaymentInfo> LoadData()
        //{
        //    long fromTel = (!string.IsNullOrEmpty(FromTelNoTextBox.Text)) ? Convert.ToInt64(FromTelNoTextBox.Text) : -1;
        //    long toTel = (!string.IsNullOrEmpty(ToTelNoTextBox.Text)) ? Convert.ToInt64(ToTelNoTextBox.Text) : -1;
        //
        //    long FromCost = (!string.IsNullOrEmpty(FromCostTextBox.Text)) ? Convert.ToInt64(FromCostTextBox.Text) : -1;
        //    long ToCost = (!string.IsNullOrEmpty(ToCostTextBox.Text)) ? Convert.ToInt64(ToCostTextBox.Text) : -1;
        //
        //    List<GeneralRequestPaymentInfo> Result = ReportDB.GetGeneralRequestPaymentInfo(
        //                                                         CityComboBox.SelectedIDs,
        //                                                         CenterComboBox.SelectedIDs,
        //                                                         FromDate.SelectedDate, ToDate.SelectedDate,
        //                                                         fromTel, toTel, FromCost, ToCost,
        //                                                         CostTypeComboBox.SelectedIDs);
        //
        //    return Result;
        //
        //}
        #endregion

        #region EventHandlers

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void FiltersTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox source = e.Source as TextBox;
            if (source != null)
            {
                long? entryData = -1;
                bool entryDataIsValid = Helper.CheckDigitDataEntry(source, out entryData);
                if (!entryDataIsValid)
                {
                    MessageBox.Show(".برای تعیین مقادیر «از شماره» ، «تا شماره» ، «از مبلغ» و «تا مبلغ» فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    this.FiltersValueIsNumeric = false;
                    source.Focus();
                }
                else
                {
                    this.FiltersValueIsNumeric = true;
                }
            }
        }

        #endregion


    }
}
