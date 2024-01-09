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
    /// Interaction logic for CutAndEstablishReportUserControl.xaml
    /// </summary>
    public partial class CutAndEstablishReportUserControl : Local.ReportBase
    {
       #region Constructor

        public CutAndEstablishReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            ActionTypeComboBox.ItemsSource = new List<CheckableItem> { new CheckableItem { ID = (int)DB.RequestType.CutAndEstablish, Name = "قطع" }, new CheckableItem { ID = (int)DB.RequestType.Connect, Name = "وصل" } };
            CutAndEstablishReasonComboBox.ItemsSource = CauseOfCutDB.GetCauseOfCutCheckableItem();
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            List<CutAndEstablishInfo> Result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            if (fromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(fromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (toDate.SelectedDate != null)
                stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(toDate.SelectedDate, Helper.DateStringType.Short).ToString();

            foreach (CutAndEstablishInfo info in Result)
            {
                info.EstablishDate = Date.GetPersianDate(info.EstablishDatedate, Date.DateStringType.Short);
                info.CutDate = Date.GetPersianDate(info.CutDatedate, Date.DateStringType.Short);
                info.CounterDate = Date.GetPersianDate(info.InsertDate, Date.DateStringType.Short);
            }


            title = "قطع و وصل";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<CutAndEstablishInfo> LoadData()
        {
            long fromTel=(!string.IsNullOrEmpty(fromTelNo.Text)) ? Convert.ToInt64(fromTelNo.Text):-1;
            long toTel=(!string.IsNullOrEmpty(toTelNo.Text)) ? Convert.ToInt64(toTelNo.Text) :-1;

            List<CutAndEstablishInfo> Result = ReportDB.GetCutAndEstablishInfo(fromDate.SelectedDate, toDate.SelectedDate,
                                                                 fromTel, toTel, CutAndEstablishReasonComboBox.SelectedIDs, ActionTypeComboBox.SelectedIDs , CenterComboBox.SelectedIDs);

            return Result;
        }

        #endregion Methods

        private void CityCheckableComboBox_LostFocus_1(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }
    }
}
 