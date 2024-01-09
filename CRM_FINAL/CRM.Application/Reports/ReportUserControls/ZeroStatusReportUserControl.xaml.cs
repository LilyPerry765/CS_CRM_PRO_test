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
using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Base;
using Stimulsoft.Report;
using Enterprise;
using Stimulsoft.Report.Dictionary;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ZeroStatusReportUserControl.xaml
    /// </summary>
    public partial class ZeroStatusReportUserControl : Local.ReportBase
    {
        #region Constructor
        public ZeroStatusReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region EventHandlers

        // private void RegionIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        // {
        //     CenterIdComboBox.ItemsSource = CenterDB.GetCenterByCityId((int)((sender as ComboBox).SelectedValue));
        // }
        //

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
                    MessageBox.Show(".برای تعیین مقدار این فیلتر فقط از اعداد استفاده نمائید", "توجّه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    source.Focus();
                }
                else
                {
                    //do nothing
                }
            }
        }

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Methods

        public override void Search()
        {
            //before rad
            //List<ZeroStatusInfo> result = LoadData();
            //string title = string.Empty;
            //string path;
            //
            //StiReport stiReport = new StiReport();
            //stiReport.Dictionary.DataStore.Clear();
            //stiReport.Dictionary.Databases.Clear();
            //stiReport.Dictionary.RemoveUnusedData();
            //
            //path = ReportDB.GetReportPath(UserControlID);
            //stiReport.Load(path);
            //stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            //stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();
            //stiReport.Dictionary.Variables["TelephoneNO"].Value = PhoneNoTextBox.Text.Trim();
            //stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            //
            //if (RegionIdComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["Region"].Value = RegionIdComboBox.Text;
            //}
            //
            //if (CenterIdComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["CenterName"].Value = CenterIdComboBox.Text;
            //}
            //
            //title = "گزارش انسداد صفر ";
            //stiReport.Dictionary.Variables["Header"].Value = title;
            //stiReport.RegData("Result", "Result", result);

            //ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            //reportViewerForm.ShowDialog();
            //TODO:rad edit.
            try
            {
                long requestNo = (!string.IsNullOrEmpty(RequestNoTextBox.Text.Trim()) ? Convert.ToInt64(RequestNoTextBox.Text) : -1);
                long telephoneNo = (!string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? Convert.ToInt64(TelephoneNoTextBox.Text.Trim()) : -1);
     
                //دیتای مورد نیاز برای ایجاد گزارش
                List<ZeroStatusInfo> primaryResult = ReportDB.GetZeroStatusInfo(
                                                                                CityComboBox.SelectedIDs,
                                                                                CenterComboBox.SelectedIDs,
                                                                                FromDatePicker.SelectedDate,
                                                                                ToDatePicker.SelectedDate,
                                                                                requestNo,
                                                                                BlockZeroStatusComboBox.SelectedIDs,
                                                                                ZeroStatusComboBox.SelectedIDs,
                                                                                telephoneNo,
                                                                                NationalCodeOrRecordNoTextBox.Text.Trim());

                if (primaryResult.Count() > 0)
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

                    //StiVariable centerVariable = new StiVariable();
                    //centerVariable.Name = "Center";
                    //centerVariable.Category = "Center";
                    //centerVariable.ValueObject = string.IsNullOrEmpty(RegionCenterUC.CenterComboBox.Text) ? "-----" : RegionCenterUC.CenterComboBox.Text;

                    StiVariable reportDateVariable = new StiVariable("ReportDate", "ReportDate", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short));
                    StiVariable timeVariable = new StiVariable("ReportTime", "ReportTime", Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time));
                    StiVariable cityVariable = new StiVariable("CityName", "CityName", DB.PersianCity);

                    //تنظیمات برای نمایش
                    CRM.Application.Local.ReportBase.SendToPrint(primaryResult, (int)DB.UserControlNames.ZeroStatus, true, fromDateVariable, toDateVariable, /*regionVariable, centerVariable,*/ reportDateVariable, timeVariable, cityVariable);
                }
                else
                {
                    MessageBox.Show(".رکوردی برای ایجاد گزارش یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                Logger.Write(ex, "خطا در لیست گزارشات - بخش امور مشترکین - گزارش انسداد صفر");
                MessageBox.Show("خطا در ایجاد گزارش", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Initialize()
        {
            BlockZeroStatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.BlockZeroStatus));
            ZeroStatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ZeroStatus));
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            //RegionIdComboBox.ItemsSource = RegionDB.GetRegions();
        }

        // private List<ZeroStatusInfo> LoadData()
        //  {
        // List<ZeroStatusInfo> result = ReportDB.GetZeroStatusInfo(FromDatePicker.SelectedDate,
        //                                                      ToDatePicker.SelectedDate,
        //                                                      string.IsNullOrEmpty(RequestNoTextBox.Text.Trim()) ? (long?)null : long.Parse(RequestNoTextBox.Text),
        //                                                      string.IsNullOrEmpty(RegionCenterUC.CenterComboBox.Text) ? (int?)null : int.Parse(RegionCenterUC.CenterComboBox.SelectedValue.ToString()),
        //                                                      BlockZeroStatusComboBox.SelectedIDs, ZeroStatusComboBox.SelectedIDs,
        //                                                      string.IsNullOrEmpty(TelephoneNoTextBox.Text.Trim()) ? (string)null : TelephoneNoTextBox.Text.Trim(),
        //                                                      string.IsNullOrEmpty(NationalCodeOrRecordNoTextBox.Text.Trim()) ? (string)null : NationalCodeOrRecordNoTextBox.Text.Trim());
        //List<EnumItem> BlockZeroStatus = Helper.GetEnumItem(typeof(DB.ClassTelephone));
        //List<EnumItem> ZeroStatus = Helper.GetEnumItem(typeof(DB.ZeroStatus));

        //foreach (ZeroStatusInfo ZSI in result)
        //{
        // ZSI.RequestDate = string.IsNullOrEmpty(ZSI.RequestDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(ZSI.RequestDate), Helper.DateStringType.Short);
        //SI.RequestLetterDate = string.IsNullOrEmpty(ZSI.RequestLetterDate) ? (string)null : Helper.GetPersianDate(DateTime.Parse(ZSI.RequestLetterDate), Helper.DateStringType.Short);
        //ZSI.ClassTelephoneName = BlockZeroStatus.Find(item => item.ID == ZSI.ClassTelephone).Name;
        //}





        //return result;
        //}

        #endregion

    }
}

