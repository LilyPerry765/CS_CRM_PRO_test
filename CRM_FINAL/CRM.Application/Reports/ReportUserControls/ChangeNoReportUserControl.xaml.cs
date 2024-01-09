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
    /// Interaction logic for ChangeNoReportUserControl.xaml
    /// </summary>
    public partial class ChangeNoReportUserControl : Local.ReportBase
    {
        #region Constructor

        public ChangeNoReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            ChangeNoReasonComboBox.ItemsSource = CauseOfChangeNoDB.GetCauseOfChangeNoCheckableItem();
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            //before rad
            //List<ChangeNoInfo> Result = LoadData();
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
            //if (FromDatePicker.SelectedDate != null)
            //    stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDatePicker.SelectedDate, Helper.DateStringType.Short).ToString();
            //if (ToDatePicker.SelectedDate != null)
            //    stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDatePicker.SelectedDate, Helper.DateStringType.Short).ToString();
            //
            //foreach (ChangeNoInfo info in Result)
            //{
            //    info.EndDate = Date.GetPersianDate(info.InsertDatedate, Date.DateStringType.Short);
            //}
            //title = "تعویض شماره";
            //stiReport.Dictionary.Variables["Header"].Value = title;
            //stiReport.RegData("Result", "Result", Result);

            //ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            //reportViewerForm.ShowDialog();
            try
            {
                //TODO:rad edit
                long fromOldTel = (!string.IsNullOrEmpty(FromOldTelNo.Text.Trim())) ? Convert.ToInt64(FromOldTelNo.Text) : -1;
                long toOldTel = (!string.IsNullOrEmpty(ToOldTelNo.Text.Trim())) ? Convert.ToInt64(ToOldTelNo.Text) : -1;
                long fromNewTel = (!string.IsNullOrEmpty(FromNewTelNo.Text.Trim())) ? Convert.ToInt64(FromNewTelNo.Text) : -1;
                long toNewTel = (!string.IsNullOrEmpty(ToNewTelNo.Text.Trim())) ? Convert.ToInt64(ToNewTelNo.Text) : -1;

                //دیتای مورد نیاز برای ایجاد گزارش
                List<ChangeNoInfo> primaryResult = ReportDB.GetChangeNoInfo(
                                                                            FromDatePicker.SelectedDate,
                                                                            ToDatePicker.SelectedDate,
                                                                            fromOldTel,
                                                                            toOldTel,
                                                                            fromNewTel,
                                                                            toNewTel,
                                                                            ChangeNoReasonComboBox.SelectedIDs,
                                                                            CenterComboBox.SelectedIDs
                                                                           );

                if (primaryResult.Count() > 0)
                {
                    //StiVariables
                    StiVariable reportDateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
                    StiVariable reportTimeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));
                    StiVariable cityVariable = new StiVariable("CityName", "CityName", DB.PersianCity);
                    StiVariable fromDateVariable = new StiVariable();
                    fromDateVariable.Name = "FromDate";
                    fromDateVariable.Category = "FromDate";
                    fromDateVariable.ValueObject = (FromDatePicker.SelectedDate.HasValue) ? FromDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short) : "-----";
                    StiVariable toDateVariable = new StiVariable();
                    toDateVariable.Name = "ToDate";
                    toDateVariable.Category = "ToDate";
                    toDateVariable.ValueObject = (ToDatePicker.SelectedDate.HasValue) ? ToDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short) : "-----";

                    //تنظیمات برای نمایش گزارش
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.ChangeNoReport, true, reportDateVariable, reportTimeVariable, cityVariable, fromDateVariable, toDateVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش امور مشترکین - گزارش تعویض شماره");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private List<ChangeNoInfo> LoadData()
        //{
        //    long fromOldTel = (!string.IsNullOrEmpty(FromOldTelNo.Text)) ? Convert.ToInt64(FromOldTelNo.Text) : -1;
        //    long toOldTel = (!string.IsNullOrEmpty(ToOldTelNo.Text)) ? Convert.ToInt64(ToOldTelNo.Text) : -1;
        //    long fromNewTel = (!string.IsNullOrEmpty(FromNewTelNo.Text)) ? Convert.ToInt64(FromNewTelNo.Text) : -1;
        //    long toNewTel = (!string.IsNullOrEmpty(ToNewTelNo.Text)) ? Convert.ToInt64(ToNewTelNo.Text) : -1;
        //
        //    List<ChangeNoInfo> Result = ReportDB.GetChangeNoInfo(FromDatePicker.SelectedDate, ToDatePicker.SelectedDate, fromOldTel, toOldTel, fromNewTel, toNewTel, ChangeNoReasonComboBox.SelectedIDs, CenterComboBox.SelectedIDs);
        //
        //    return Result;
        //}

        #endregion Methods

        #region EventHandlers

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void FiltersTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //در اینجا چنانچه یک مقدار غیر عددی در فیلتر های عددی وارد شود به کاربر پیغام داده میشود
            TextBox source = e.Source as TextBox;
            if (source != null)
            {
                long? inputData = 0;
                bool inputDataIsValid = Helper.CheckDigitDataEntry(source, out inputData);
                if (!inputDataIsValid)
                {
                    MessageBox.Show(".برای تعیین مقدار این فیلتر فقط از اعداد استفاده نمائید", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    source.Focus();
                }
                else
                {
                    //do nothing
                }
            }
        }

        #endregion
    }
}
