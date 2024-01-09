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
using System.Reflection;
using CRM.Application.Reports.Viewer;
using Stimulsoft.Report;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for BuchtSwitchingReportUserControl.xaml
    /// </summary>
    public partial class BuchtSwitchingReportUserControl : Local.ReportBase
    {
        #region Constructor
        public BuchtSwitchingReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region Initializer

        private void Initialize()
        {
            //CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            //CityComboBox.ItemsComboBox.DropDownClosed += new EventHandler(ItemsComboBox_DropDownClosed);
        }

        #endregion Initializer

        #region EventHAndler

        //void ItemsComboBox_DropDownClosed(object sender, EventArgs e)
        //{
        //    bool AllChecked = false;
        //    if (CityComboBox.SelectedIDs.Count > 0)
        //        AllChecked = true;

        //    CenterComboBox.ItemsSource = Data.CenterDB.GetCentersCheckable(AllChecked, CityComboBox.SelectedIDs);
            
        //}

        #endregion

        #region Methods

        public override void Search()
        {
            List<BuchtSwitchingReportInfo> Result = LoadData();

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

            if (FromDate.SelectedDate != null)
                stiReport.Dictionary.Variables["FromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            if (ToDate.SelectedDate != null)
                stiReport.Dictionary.Variables["ToDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();


            title = "تعویض بوخت ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", Result);


            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();

        }

        private List<BuchtSwitchingReportInfo> LoadData()
        {

            List<BuchtSwitchingReportInfo> Result = ReportDB.GetBuchtSwitchingReportInfo( FromDate.SelectedDate,ToDate.SelectedDate);

             return Result;
        }

        #endregion Methods
    }
}
