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

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ChangeNumberReportUserControl.xaml
    /// </summary>
    public partial class ChangeNumberReportUserControl : Local.ReportBase
    {
        #region Properties And Fields
        #endregion  Properties And Fields

        #region Constructor

        public ChangeNumberReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion Constructor

        #region Initializer

        private void Initialize()
        {
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            ChangeNumberReasonComboBox.ItemsSource = Data.CauseOfChangeNoDB.GetCauseOfChangeNoCheckableItem();
        }
        #endregion  Initializer

        #region Event Handlers



        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        #endregion  Event Handlers

        #region Methods

        public override void Search()
        {
            List<ChangeNumberInfo> result = LoadData();
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
            stiReport.Dictionary.Variables["TelephoneNO"].Value =PhoneNoTextBox.Text.Trim();
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

            //if (RegionIdComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["Region"].Value = RegionIdComboBox.Text;
            //}

            //if (CenterIdComboBox.Text != string.Empty)
            //{
            //    stiReport.Dictionary.Variables["CenterName"].Value = CenterIdComboBox.Text;
            //}

            title = "گزارش تغییر شماره تلفن ";
            stiReport.Dictionary.Variables["Header"].Value = title;
            stiReport.RegData("Result", "Result", result);
      
              
            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private List<ChangeNumberInfo> LoadData()
        {
            long telphoneNo = -1;
            if (PhoneNoTextBox.Text.Trim() != string.Empty)
                telphoneNo = Convert.ToInt32(PhoneNoTextBox.Text.Trim());

            List<ChangeNumberInfo> result = ReportDB.GetChangeNumberInfo( CityComboBox.SelectedIDs,
                                                                          CenterComboBox.SelectedIDs,
                                                                          FromDate.SelectedDate,
                                                                          ToDate.SelectedDate,
                                                                          telphoneNo,
                                                                          ChangeNumberReasonComboBox.SelectedIDs);
            List<CheckableItem> changenoReason = Data.CauseOfChangeNoDB.GetCauseOfChangeNoCheckableItem();

            foreach (ChangeNumberInfo changeNumberInfo in result)
            {
                changeNumberInfo.ChangeReason = changenoReason.Find(item => item.ID == int.Parse(changeNumberInfo.ChangeReason)).Name;
            }

            //if (PhoneNoTextBox.Text.Trim() != string.Empty)
            //{
            //    result = result.Where
            //    (t => (t.OldTelephoneNo == PhoneNoTextBox.Text.Trim() ||
            //           t.NewTelephoneNo ==PhoneNoTextBox.Text.Trim())).ToList();
            //}

            //if (NationalIdTextBox.Text.Trim() != string.Empty)
            //{
            //    result = result.Where
            //        (t => (t.CustomerNationalCode == NationalIdTextBox.Text.Trim() || t.CustomerNationalCode == NationalIdTextBox.Text.Trim())).ToList();
            //}

            return result;
        }
        #endregion  Methods

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }


        
    }
}
