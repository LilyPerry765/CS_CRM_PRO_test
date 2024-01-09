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
using System.Windows.Shapes;
using CRM.Data;
using System.IO;
using System.IO.Compression;

namespace CRM.Application.Reports.Viewer
{
    /// <summary>
    /// Interaction logic for SaveReportForm.xaml
    /// </summary>
    public partial class SaveReportForm : Window
    {
        #region Properties And Fields
        public int? ReportID
        {
            get;
            set;
        }


        static readonly DependencyProperty DependencyReport = DependencyProperty.Register("CurrentReport", typeof(ReportTemplate), typeof(SaveReportForm));

        ReportTemplate CurrentReport
        {
            get
            {
                return (ReportTemplate)GetValue(DependencyReport);

            }
            set
            {
                SetValue(DependencyReport, value);
            }

        }
     
        #endregion  Properties And Fields

        #region Constructor

        public SaveReportForm(Byte[] reportFile,int? reportID)
        {
            InitializeComponent();
            if (reportID == null || reportID == -1)
            {
                CurrentReport = new ReportTemplate();
            }
            else
            {
                CurrentReport = ReportDB.GetReportTemplateByID(reportID ?? -1);
                this.ReportID = reportID;
            }

            if (CurrentReport != null)
            {
                CurrentReport.Template = reportFile;
            }
            this.DataContext = this;
        }

        #endregion Constructor

        #region Event Handlers

        private void SaveReport(object sender, RoutedEventArgs e)
        {
            CurrentReport.TimeStamp = (DB.GetServerDate()).Ticks.ToString("X");
            CurrentReport.Detach();
            DB.Save(CurrentReport);
            this.ReportID = CurrentReport.ID;
            this.DialogResult = true;
            this.Close();
            
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion Event Handlers

        #region Methods

        private void ZipReport()
        {
            byte[] data = new byte[CurrentReport.Template.Length + 10];
            data = CurrentReport.Template.ToArray();

            StreamReader rd = new StreamReader(new MemoryStream(data));
            string str = rd.ReadToEnd();
            str = str + (char)(26);

            MemoryStream ms = new MemoryStream();
            GZipStream gZip = new GZipStream(ms, CompressionMode.Compress, true);
            gZip.Write(ASCIIEncoding.UTF8.GetBytes(str), 0, (ASCIIEncoding.UTF8.GetBytes(str).Length));
            CurrentReport.Template = ms.ToArray();
        }
        #endregion Methods
        
        
    }
}
