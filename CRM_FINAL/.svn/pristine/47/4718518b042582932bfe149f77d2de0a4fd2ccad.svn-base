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
    /// Interaction logic for CustomerTypeReportUserControl.xaml
    /// </summary>
    public partial class PersonTypeReportUserControl : Local.ReportBase
    {
        #region Properties
        #endregion

        #region Constructor
        public PersonTypeReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion

        #region Initializer

        private void Initialize()
        {
            PersonTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PersonType));
        }
        #endregion

        #region Methods

        public override void Search()
        {
            List<PersonTypeInfo> result = LoadData();
            string title = string.Empty;
            string path;

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();


            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["fromDate"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.Short).ToString();
            stiReport.Dictionary.Variables["toDate"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.Short).ToString();

            stiReport.Dictionary.Variables["Report_Time"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Time).ToString();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();
            

            title = "نوع شخصیت ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<PersonTypeInfo> LoadData()
        {

            long? fromTel = (string.IsNullOrEmpty(FromTelNo.Text) ? -1 : Convert.ToInt64(FromTelNo.Text));
            long? toTel = string.IsNullOrEmpty(ToTelNo.Text) ? -1 : Convert.ToInt64(ToTelNo.Text);

            List<PersonTypeInfo> Result = ReportDB.GetPersonTypeInfo(FromDate.SelectedDate, ToDate.SelectedDate, fromTel, toTel, PersonTypeComboBox.SelectedIDs, CityCenterComboBox.CityComboBox.SelectedIDs, CityCenterComboBox.CenterCheckableComboBox.SelectedIDs, NameTextBox.Text.Trim(), FamilyTextBox.Text.Trim(), NationalCodeTextBox.Text.Trim());

            List<EnumItem> possessionType = Helper.GetEnumItem(typeof(DB.PossessionType));
            Result.ToList().ForEach(t =>
                {
                    t.PersonType = (t.PersonType == "0" ? "حقیقی" : "حقوقی");
                    t.PosessionType = possessionType.Find(x => x.ID == int.Parse(t.PosessionType)).Name;
                });


            return Result;

        }

        #endregion  Methods
    }
}
