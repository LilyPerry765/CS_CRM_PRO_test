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
using CRM.Application.Reports.Viewer;
using Enterprise;
using Stimulsoft.Report.Dictionary;
using System.Collections;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for SpecialServiceReportUserControl.xaml
    /// </summary>
    public partial class SpecialServiceReportUserControl : Local.ReportBase
    {

        #region Constructor

        public SpecialServiceReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Event Handlers
        private void FiltersTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //در اینجا چنانچه یک مقدار غیر عددی در فیلترهای عددی وارد شود، به کاربر پیغام داده میشود
            //چنانچه یک کنترل تکست باکس در این فرم برای فیلتر غیر عددی در نظر گرفته شده باشد ، آنگاه تگ آن را به صورت زیر مقدار داده ایم
            //TextBox  Tag="StringInput"
            //بنابراین کاربر هر مقدار در آن میتواند وارد کند
            TextBox source = e.Source as TextBox;
            if (source != null && source.Tag == null)
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

        #endregion Event Handlers

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            ServiceStatusTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.StatusSpecialService));
            SpecialServiceTypeComboBox.ItemsSource = SpecialServiceTypeDB.GetSpecialServiceTypeCheckable();
        }

        public override void Search()
        {
            //before rad
            //List<SpecialServiceInfo> result = LoadData();
            //StiReport stiReport = new StiReport();
            //stiReport.Dictionary.DataStore.Clear();
            //stiReport.Dictionary.Databases.Clear();
            //stiReport.Dictionary.RemoveUnusedData();
            //string title = string.Empty;
            //string path = ReportDB.GetReportPath(UserControlID);
            //stiReport.Load(path);
            //title = "گزارش سرویس ویژه";
            //
            //stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            //stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            //
            //stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            //
            //if (regionCenterUserControl.RegionComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["Region"].Value = regionCenterUserControl.RegionComboBox.Text;
            //}
            //if (regionCenterUserControl.CenterComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["CenterName"].Value = regionCenterUserControl.CenterComboBox.Text;
            //}
            //
            //
            //stiReport.Dictionary.Variables["Header"].Value = title;
            //stiReport.RegData("result", "result", result);

            //ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            //reportViewerForm.ShowDialog();

            //TODO:rad edit
            //TODO:rad در اینجا باید روشی پیدا کنیم تا بتوانیم بر روی وضعیت و نوع سرویس نیز فیلتر را پیاده سازی کنیم چون در حال حاضر بدلیل نداشتن رابطه بین جدول مربوطه امکانش نیست
            try
            {
                long requestNo = (!string.IsNullOrEmpty(RequestNoTextBox.Text.Trim()) ? Convert.ToInt64(RequestNoTextBox.Text) : -1);
                long telephoneNo = (!string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? Convert.ToInt64(TelephoneNoTextBox.Text.Trim()) : -1);

                //دیتای مورد نیاز برای ایجاد گزارش
                var primaryResult = ReportDB.GetSpecialServiceInfo(   
                                                                    CityComboBox.SelectedIDs,
                                                                    CenterComboBox.SelectedIDs,
                                                                    FromDatePicker.SelectedDate,
                                                                    ToDatePicker.SelectedDate,
                                                                    requestNo,
                                                                    telephoneNo,
                                                                    SpecialServiceTypeComboBox.SelectedIDs,
                                                                    ServiceStatusTypeComboBox.SelectedIDs,
                                                                    NationalCodeOrRecordNoTextBox.Text.Trim()
                                                                   );

                if (!primaryResult.IsNullOrEmpty())
                {
                    //StiVariables
                    StiVariable fromDateVariable = new StiVariable();
                    fromDateVariable.Name = "FromDate";
                    fromDateVariable.Category = "FromDate";
                    fromDateVariable.ValueObject = (FromDatePicker.SelectedDate.HasValue) ? Helper.GetPersianDate(FromDatePicker.SelectedDate, Helper.DateStringType.Short) : "-----";

                    StiVariable toDateVariable = new StiVariable();
                    toDateVariable.Name = "ToDate";
                    toDateVariable.Category = "ToDate";
                    toDateVariable.ValueObject = (ToDatePicker.SelectedDate.HasValue) ? Helper.GetPersianDate(ToDatePicker.SelectedDate, Helper.DateStringType.Short) : "-----";

                    //StiVariable regionVariable = new StiVariable();
                    //regionVariable.Name = "Region";
                    //regionVariable.Category = "Region";
                    //regionVariable.ValueObject = string.IsNullOrEmpty(RegionCenterUC.RegionComboBox.Text) ? "-----" : RegionCenterUC.RegionComboBox.Text;

                    //StiVariable centerNameVariable = new StiVariable();
                    //centerNameVariable.Name = "CenterName";
                    //centerNameVariable.Category = "CenterName";
                    //centerNameVariable.ValueObject = string.IsNullOrEmpty(RegionCenterUC.CenterComboBox.Text) ? "-----" : RegionCenterUC.CenterComboBox.Text;

                    StiVariable reportDateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
                    StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));
                    StiVariable cityVariable = new StiVariable("CityName", "CityName", DB.PersianCity);

                    //تنظیمات برای نمایش
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.SpecialService, true, fromDateVariable, toDateVariable, reportDateVariable, timeVariable, /*centerNameVariable, regionVariable,*/ cityVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارش ها - بخش امور مشترکین - گزارش سرویس ویژه");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);

        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        //private IEnumerable LoadData()
        //{
        // var result =
        // ReportDB.GetSpecialServiceInfo(FromDate.SelectedDate,
        //                          ToDate.SelectedDate,
        //                          string.IsNullOrEmpty(txtRequestNo.Text.Trim()) ? (long?)null : long.Parse(txtRequestNo.Text),
        //                          string.IsNullOrEmpty(regionCenterUserControl.CenterComboBox.Text) ? (int?)null : int.Parse(regionCenterUserControl.CenterComboBox.SelectedValue.ToString()),
        //                          string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim(),
        //                          SpecialServiceTypeComboBox.SelectedIDs,
        //                          ServiceStatusTypeComboBox.SelectedIDs,
        //                         string.IsNullOrEmpty(CustomerNationalIDTextBox.Text.Trim()) ? (string)null : CustomerNationalIDTextBox.Text
        //                          );
        //
        //List<EnumItem> serviceStatusTypeList = Helper.GetEnumItem(typeof(DB.StatusSpecialService));
        //
        //foreach (SpecialServiceInfo I in result)
        //{
        //    I.RequestDate = Helper.GetPersianDate(DateTime.Parse(I.RequestDate), Helper.DateStringType.Short);
        //    I.RequestLetterDate = string.IsNullOrEmpty(I.RequestLetterDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(I.RequestLetterDate), Helper.DateStringType.Short);
        //    I.InstallDate = string.IsNullOrEmpty(I.InstallDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(I.InstallDate), Helper.DateStringType.Short);
        //    I.UnInstalDate = string.IsNullOrEmpty(I.UnInstalDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(I.UnInstalDate), Helper.DateStringType.Short);
        //}
        // return result;
        //  }

        #endregion Method

    }
}