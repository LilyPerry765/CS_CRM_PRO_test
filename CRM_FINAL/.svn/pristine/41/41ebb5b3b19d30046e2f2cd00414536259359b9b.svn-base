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
using System.ComponentModel;
using CRM.Data;
using Stimulsoft.Report;
using Stimulsoft.Base;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for DisconnectAndConnectCountReportUserControl.xaml
    /// </summary>
    public partial class DisconnectAndConnectCountReportUserControl : Local.ReportBase
    {
        #region Properties And Fields
        #endregion Properties And Fields

        #region Constructor
        public DisconnectAndConnectCountReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }
        #endregion  Constructor

        #region Initializer

        private void Initialize()
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {

                CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
                CauseOfCutIdsComboBox.ItemsSource = Data.CauseOfCutDB.GetCauseOfCutCheckableItem();
                ActionTypeIdCombBox.ItemsSource = new List<CheckableItem> { new CheckableItem { ID = (int)DB.RequestType.CutAndEstablish, Name = "قطع" }, new CheckableItem { ID = (int)DB.RequestType.Connect, Name = "وصل" } };
                ActionTypeIdCombBox.SelectedValue = (int)DB.RequestType.CutAndEstablish;
            }
        }

        #endregion  Initializer

        #region Event Handlers

        private void ActionTypeIdCombBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Convert.ToInt32((sender as ComboBox).SelectedValue) == (int)DB.RequestType.CutAndEstablish)
            {
                CauseOfCutIdsComboBox.IsEnabled = true;
            }
            else if (Convert.ToInt32((sender as ComboBox).SelectedValue) == (int)DB.RequestType.Connect)
            {
                CauseOfCutIdsComboBox.IsEnabled = false;
            }
        }


        #endregion  Event Handlers

        #region Methods

        public override void Search()
        {

            try
            {
                string title = string.Empty;

                string path;
                List<CutAndEstablishInfo> result =

                    ReportDB.GetCutAndEstablishInfo(CityComboBox.SelectedIDs,
                                                    CenterComboBox.SelectedIDs,
                                                    FromDate.SelectedDate,
                                                    ToDate.SelectedDate,
                                                    string.IsNullOrEmpty(PhoneNoTextBox.Text.Trim()) ? (long?)null : long.Parse(PhoneNoTextBox.Text),
                                                    (int)ActionTypeIdCombBox.SelectedValue,
                                                    CauseOfCutIdsComboBox.SelectedIDs,
                                                    string.IsNullOrEmpty(NationalIdTextBox.Text.Trim()) ? (string)null : NationalIdTextBox.Text);


                StiReport stiReport = new StiReport();
                stiReport.Dictionary.DataStore.Clear();
                stiReport.Dictionary.Databases.Clear();
                stiReport.Dictionary.RemoveUnusedData();

                path = ReportDB.GetReportPath(UserControlID);
                stiReport.Load(path);
                stiReport.Dictionary.Variables["data_a"].Value = Helper.GetPersianDate(FromDate.SelectedDate, Helper.DateStringType.DateTime).ToString();
                stiReport.Dictionary.Variables["data_b"].Value = Helper.GetPersianDate(ToDate.SelectedDate, Helper.DateStringType.DateTime).ToString();
                stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();

                //if (CenterIdComboBox.SelectedIndex != -1)
                //{
                //    stiReport.Dictionary.Variables["city"].Value = CenterIdComboBox.Text;
                //    City city =  Data.CityDB.GetCityByCenterID(((CenterInfo)CenterIdComboBox.SelectedItem).ID);
                //    stiReport.Dictionary.Variables["ci_name"].Value = city.Name;
                //}

                int? _ActiveTypeId = string.IsNullOrEmpty(ActionTypeIdCombBox.Text) ? (int?)null : int.Parse(ActionTypeIdCombBox.SelectedValue.ToString());

                switch (_ActiveTypeId)
                {
                    case (int)DB.RequestType.CutAndEstablish:
                        {
                            title = "گزارش قطع";
                            break;
                        }
                    case (int)DB.RequestType.Connect:
                        {
                            title = "گزارش وصل";
                            break;
                        }
                    case null:
                        {
                            title = "گزارش قطع و وصل";
                            break;
                        }

                }

                stiReport.Dictionary.Variables["ci_report_header"].Value = title;
                stiReport.RegData("result", "result", result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
            catch (Exception)
            {
                // ShowErrorMessage( ex);
            }
        }

        #endregion  Methods

        private void CityComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckableItemByCityIds(CityComboBox.SelectedIDs);
        }

        private void CenterComboBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

    }
}
