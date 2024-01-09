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
using Stimulsoft.Report;
using Stimulsoft.Base;
using System.Reflection;
using CRM.Application.Reports.Viewer;
using CRM.Data;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PAPEquipmentReportUserControl.xaml
    /// </summary>
    public partial class PAPEquipmentReportUserControl : Local.ReportBase
    {
        public PAPEquipmentReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        #region Initializer

        private void Initialize()
        {
            PAPComboBox.ItemsSource = Data.PAPInfoDB.GetPAPInfoCheckable();
            CenterComboBox.ItemsSource = CenterDB.GetCenterCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPAPPortStatus));
        }

        #endregion Initializer

        #region Methods

        public override void Search()
        {
            if (PAPComboBox.SelectedIndex < 0)
                MessageBox.Show("لطفا نام شرکت را انتخاب کنید");
            else
            {
                List<PAPPortInfo> Result = LoadData();
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


                title = "تجهیزات شرکت های PAP";
                stiReport.Dictionary.Variables["Header"].Value = title;
                stiReport.RegData("Result", "Result", Result);

                ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
                reportViewerForm.ShowDialog();
            }
        }

        public List<PAPPortInfo> LoadData()
        {
            long TelNumber = !string.IsNullOrWhiteSpace(TelNoTextBox.Text) ? Convert.ToInt64(TelNoTextBox.Text) : -1;
            long portNumber = !string.IsNullOrWhiteSpace(PortNoTextBox.Text) ? Convert.ToInt64(PortNoTextBox.Text) : -1;
            List<PAPPortInfo> result = ReportDB.GetADSLPAPPortInfo
                                                    ((int)PAPComboBox.SelectedValue,
                                                     CenterComboBox.SelectedIDs,
                                                     StatusComboBox.SelectedIDs,
                                                     TelNumber,
                                                     portNumber);
            return result;
        }

        #endregion Methods
    }
}
