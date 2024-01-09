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
    /// Interaction logic for EmptyTelephoneNoLisReportUserControl.xaml
    /// </summary>
    public partial class EmptyTelephoneNoLisReportUserControl : Local.ReportBase
    {
         #region Properties
        bool? IsRoud = null;
        #endregion

        #region Constructor
        public EmptyTelephoneNoLisReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region Initializer

        private void Initialize()
        {
            TelephoneTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.TelephoneType2));
        }
        #endregion

        #region Methods

        public override void Search()
        {
           
            if (TelephoneTypeComboBox.SelectedIndex == 2)
                IsRoud = true;
            List<EmptyTelephoneNoInfo> result = LoadData();
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


            title = "لیست شماره تلفن های خالی";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<EmptyTelephoneNoInfo> LoadData()
        {
            long? fromTel = (string.IsNullOrEmpty(FromTelNo.Text) ? -1 : Convert.ToInt64(FromTelNo.Text));
            long? toTel = string.IsNullOrEmpty(ToTelNo.Text) ? -1 : Convert.ToInt64(ToTelNo.Text);

            List<EmptyTelephoneNoInfo> Result = ReportDB.GetEmptyTelephoneNoInfo(fromTel, toTel, 
                                                                             CityCenterComboBox.CityComboBox.SelectedIDs, 
                                                                           CityCenterComboBox.CenterCheckableComboBox.SelectedIDs,
                                                                           TelephoneTypeComboBox.SelectedIDs,
                                                                           IsRoud);

            return Result;

        }

        #endregion  Methods
    }
}
