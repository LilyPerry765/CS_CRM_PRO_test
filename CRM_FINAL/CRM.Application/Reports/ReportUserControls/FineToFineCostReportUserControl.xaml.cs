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

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for FineToFineCostReportUserControl.xaml
    /// </summary>
    public partial class FineToFineCostReportUserControl : Local.ReportBase
    {
        #region Constructor

        public FineToFineCostReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region EventHandlers

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CostTypeComboBox.ItemsSource = RequestPaymentDB.GetCostTypeCheckable();
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
        }

        public override void Search()
        {
            List<FineToFineRequestPaymentInfo> result = this.LoadData();
            if (result == null || result.Count != 0)
            {
                string title = "گزارش جزیی هزینه";
                string path;
                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                path = ReportDB.GetReportPath(UserControlID);
                stiReport.Load(path);

                DateTime currentDate = DB.GetServerDate();
                stiReport.Dictionary.Variables["Report_Time"].Value = currentDate.ToString("HH:mm:ss tt");
                stiReport.Dictionary.Variables["Report_Date"].Value = currentDate.ToPersian(Date.DateStringType.Short);
                stiReport.Dictionary.Variables["Header"].Value = title;

                if (FromDatePicker.SelectedDate != null)
                {
                    stiReport.Dictionary.Variables["fromDate"].Value = FromDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short);
                }
                if (ToDatePicker.SelectedDate != null)
                {
                    stiReport.Dictionary.Variables["toDate"].Value = ToDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short);
                }

                foreach (FineToFineRequestPaymentInfo Info in result)
                {
                    if (Info.BaseCostTitle != null)
                    {
                        Info.CostType = Info.BaseCostTitle;
                    }
                    else if (Info.OtherCostTitle != null)
                    {
                        Info.CostType = Info.OtherCostTitle;
                    }
                }

                stiReport.RegData("Result", "Result", result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
            else
            {
                MessageBox.Show(".رکوردی یافت نشد", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private List<FineToFineRequestPaymentInfo> LoadData()
        {
            long fromTelephoneNo = (!string.IsNullOrEmpty(FromTelephnoeNoTextBox.Text)) ? Convert.ToInt64(FromTelephnoeNoTextBox.Text) : -1;
            long toTelephoneNo = (!string.IsNullOrEmpty(ToTelephoneNoTextBox.Text)) ? Convert.ToInt64(ToTelephoneNoTextBox.Text) : -1;

            long FromCost = (!string.IsNullOrEmpty(FromCostTextBox.Text)) ? Convert.ToInt64(FromCostTextBox.Text) : -1;
            long ToCost = (!string.IsNullOrEmpty(ToCostTextBox.Text)) ? Convert.ToInt64(ToCostTextBox.Text) : -1;

            List<FineToFineRequestPaymentInfo> result;
            try
            {
                result = ReportDB.GetFineToFineRequestPaymentInfo(
                                                                 CityComboBox.SelectedIDs,
                                                                 CenterComboBox.SelectedIDs,
                                                                 FromDatePicker.SelectedDate, ToDatePicker.SelectedDate,
                                                                 fromTelephoneNo, toTelephoneNo, FromCost, ToCost, CostTypeComboBox.SelectedIDs,
                                                                 IsPaidCheckBox.IsChecked
                                                                 );
            }
            catch (Exception ex)
            {
                result = null;
                MessageBox.Show(ex.Message);
            }
            return result;
        }

        //milad doran
        //public override void Search()
        //{
        //    List<FineToFineRequestPaymentInfo> Result = LoadData();
        //    string title = string.Empty;
        //    string path;
        //    StiReport stiReport = new StiReport();
        //    stiReport.Dictionary.DataStore.Clear();
        //    stiReport.Dictionary.Databases.Clear();
        //    stiReport.Dictionary.RemoveUnusedData();

        //    path = ReportDB.GetReportPath(UserControlID);
        //    stiReport.Load(path);
        //    stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
        //    stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

        //    if (FromDate.SelectedDate != null)
        //        stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
        //    if (ToDate.SelectedDate != null)
        //        stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

        //    foreach (FineToFineRequestPaymentInfo Info in Result)
        //    {
        //        if (Info.BaseCostTitle != null)
        //            Info.CostType = Info.BaseCostTitle;

        //        else if (Info.OtherCostTitle != null)
        //            Info.CostType = Info.OtherCostTitle;
        //    }

        //    title = "گزارش جزیی هزینه";
        //    stiReport.Dictionary.Variables["Header"].Value = title;
        //    stiReport.RegData("Result", "Result", Result);

        //    ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
        //    reportViewerForm.ShowDialog();
        //}

        //milad doran
        //private List<FineToFineRequestPaymentInfo> LoadData()
        //{
        //    long fromTel = (!string.IsNullOrEmpty(FromTelNoTextBox.Text)) ? Convert.ToInt64(FromTelNoTextBox.Text) : -1;
        //    long toTel = (!string.IsNullOrEmpty(ToTelNoTextBox.Text)) ? Convert.ToInt64(ToTelNoTextBox.Text) : -1;

        //    long FromCost = (!string.IsNullOrEmpty(FromCostTextBox.Text)) ? Convert.ToInt64(FromCostTextBox.Text) : -1;
        //    long ToCost = (!string.IsNullOrEmpty(ToCostTextBox.Text)) ? Convert.ToInt64(ToCostTextBox.Text) : -1;

        //    List<FineToFineRequestPaymentInfo> Result = ReportDB.GetFineToFineRequestPaymentInfo(
        //                                                         CityComboBox.SelectedIDs,
        //                                                         CenterComboBox.SelectedIDs,
        //                                                         FromDate.SelectedDate, ToDate.SelectedDate,
        //                                                         fromTel,toTel,FromCost,ToCost,CostTypeComboBox.SelectedIDs,
        //                                                         IsPaidCheckBox.IsChecked);

        //    return Result;
        //}

        #endregion Methods

    }
}
