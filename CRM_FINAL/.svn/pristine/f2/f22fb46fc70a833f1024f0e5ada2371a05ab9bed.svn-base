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
    /// Interaction logic for SaveFormForm.xaml
    /// </summary>
    public partial class SaveFormForm : Window
    {
       #region Properties And Fields
        public int? FormID
        {
            get;
            set;
        }


       static   readonly DependencyProperty DependencyReport = DependencyProperty.Register("CurrentReport", typeof(FormTemplate), typeof(SaveFormForm));

        FormTemplate CurrentForm
        {
            get
            {
                return (FormTemplate)GetValue(DependencyReport);

            }
            set
            {
                SetValue(DependencyReport, value);
            }

        }

        int RequestTypeID { get; set; }
        string FormTitle {get; set;}
        public static bool cancel=false;
     
        #endregion  Properties And Fields

        #region Constructor

        public SaveFormForm(Byte[] reportFile, int? formID,int requestTypeID,string formTitle)
        {
            InitializeComponent();
            cancel = false;
            if (formID == null || formID == -1)
            {
                CurrentForm = new FormTemplate();
            }
            else
            {
                CurrentForm = ReportDB.GetFormTemplateByID(formID ?? -1);
                this.FormID = formID;
            }

            if (CurrentForm != null)
            {
                CurrentForm.Template = reportFile;
            }
            RequestTypeID = requestTypeID;
            FormTitle = formTitle;
            Initialize();
        }


        #endregion Constructor

        #region Event Handlers
        public void Initialize()
        {
            requestTypeNameTextBox.Text = DB.GetEnumDescriptionByValue(typeof(DB.RequestType), RequestTypeID);
            FormTitleTextBox.Text = FormTitle;
        }

        private void SaveReport(object sender, RoutedEventArgs e)
        {
            CurrentForm.TimeStamp = (DB.GetServerDate()).Ticks.ToString("X");
            CurrentForm.RequestTypeID = RequestTypeID;
            CurrentForm.Title = FormTitle;
            CurrentForm.Detach();
            DB.Save(CurrentForm);
            this.FormID = CurrentForm.ID;
            this.DialogResult = true;
            this.Close();
            
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            cancel = true;
            this.Close();
        }

        #endregion Event Handlers

        #region Methods

        private void ZipReport()
        {
            byte[] data = new byte[CurrentForm.Template.Length + 10];
            data = CurrentForm.Template.ToArray();

            StreamReader rd = new StreamReader(new MemoryStream(data));
            string str = rd.ReadToEnd();
            str = str + (char)(26);

            MemoryStream ms = new MemoryStream();
            GZipStream gZip = new GZipStream(ms, CompressionMode.Compress, true);
            gZip.Write(ASCIIEncoding.UTF8.GetBytes(str), 0, (ASCIIEncoding.UTF8.GetBytes(str).Length));
            CurrentForm.Template = ms.ToArray();
        }
        #endregion Methods
    }
}
