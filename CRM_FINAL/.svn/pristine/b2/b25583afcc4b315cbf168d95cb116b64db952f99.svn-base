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
using System.Collections;
using CRM.Application.Reports.Viewer;

namespace CRM.Application.Reports.ReportUserControls
{
    /// <summary>
    /// Interaction logic for PublicReportUserControl.xaml
    /// </summary>
    public partial class PublicReportUserControl : Local.ReportBase
    {
        public PublicReportUserControl()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            
        }

        public override void Search()
        {
            
            IEnumerable result = LoadData();
            string title = string.Empty;
            string path;
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            path = ReportDB.GetReportPath(UserControlID);
            stiReport.Load(path);
            
            stiReport.Dictionary.Variables["Report_Date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.Short).ToString();


            stiReport.RegData("Result", "Result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
        private IEnumerable LoadData()
        {
            IEnumerable result = null;
            //switch (UserControlID)
            //{
            //    //case (int)DB.UserControlNames.Status_DateSendingFailure117Requests:
            //    //    {
            //    //        result = ReportDB.GetSendingFailure117RequestsToNetworkCable();
            //    //        break;
            //    //    }

            //    //case (int)DB.UserControlNames.SendingFailure117RequestsToNetworkCable:
            //    //    {
            //    //        result = ReportDB.GetSendingFailure117RequestsToNetworkCable();
            //    //        break;
            //    //    }

            //    //case (int)DB.UserControlNames.CabinetInputFailure:
            //    //    {
            //    //        result = ReportDB.GetCabinetInputFailure();
            //    //        break;
            //    //    }

            //}
            return result;
        }
    }
}
