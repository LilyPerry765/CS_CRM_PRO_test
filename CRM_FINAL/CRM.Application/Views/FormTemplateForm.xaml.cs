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
using Stimulsoft.Report;
using System.Windows.Media.Effects;
using CRM.Application.Reports.ReportUserControls;
using System.Reflection;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for FormTemplateForm.xaml
    /// </summary>
    public partial class FormTemplateForm : Local.PopupWindow
    {
         #region Properties

        private int _ID = 0;

        public enum FormState
        {
            Nothing,
            Insert,
            Update,
            UpdateInsert
        }

        public FormState CurrentFormState
        {
            get;
            set;
        }

        public int SelectedReportTemplateId
        {
            get;
            set;
        }

        public static string temp;
        public static int RequestTypeID;

        public  FormTemplate UpdateFormTemplate=new FormTemplate();
        
        #endregion

        #region Constructors

        public FormTemplateForm()
        {
            InitializeComponent();
            Initialize();
            StiOptions.Engine.GlobalEvents.SavingReportInDesigner -= new Stimulsoft.Report.Design.StiSavingObjectEventHandler(GlobalEvents_SavingFormInDesigner);
            StiOptions.Engine.GlobalEvents.SavingReportInDesigner += new Stimulsoft.Report.Design.StiSavingObjectEventHandler(GlobalEvents_SavingFormInDesigner);
            this.CurrentFormState = FormState.Nothing;
           
        }

        public FormTemplateForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            
        }

        private void LoadData()
        {
                RequestTypeCombBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.RequestType));



                if (_ID != 0)
                {
                    UpdateFormTemplate = DocumentTypeDB.GetFormTemplateByID(_ID);
                    RequestTypeCombBox.SelectedValue = UpdateFormTemplate.RequestTypeID;
                    FormTitleTextBox.Text = UpdateFormTemplate.Title;
                }
            FormTemplate documentType = new FormTemplate();

            if (_ID == 0)
            {
                SaveButton.Content = "ایجاد فرم";
                CurrentFormState = FormState.Nothing;
            }

            else
            {
                //documentType = Data.DocumentTypeDB.GetFormTemplateByID(_ID);
                SaveButton.Content = "بروز رسانی";
                //RequestTypeCombBox.SelectedValue = documentType.RequestTypeID;
                //FormTitleTextBox.Text = documentType.Title;
                CurrentFormState = FormState.Update;

            }
            
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void NewForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            if (CurrentFormState == FormState.Nothing)
                {
                    CRM.Application.Reports.Viewer.FormDesignerForm formDesignerForm = new CRM.Application.Reports.Viewer.FormDesignerForm(true, -1);
                    temp = FormTitleTextBox.Text;
                    RequestTypeID = (int)RequestTypeCombBox.SelectedValue;
                    CurrentFormState = FormState.Insert;
                    formDesignerForm.ShowDialog();
                    
                }
            else if (CurrentFormState == FormState.Update)
                {

                    CRM.Application.Reports.Viewer.FormDesignerForm formDesignerForm = new CRM.Application.Reports.Viewer.FormDesignerForm(false, UpdateFormTemplate.ID);
                    UpdateFormTemplate.Title = FormTitleTextBox.Text;
                    UpdateFormTemplate.RequestTypeID =(int) RequestTypeCombBox.SelectedValue;
                    CurrentFormState = FormState.Update;
                        temp = FormTitleTextBox.Text;
                        RequestTypeID = (int)RequestTypeCombBox.SelectedValue;
                        formDesignerForm.ShowDialog();
                   
                }
        }


        private void GlobalEvents_SavingFormInDesigner(object sender, Stimulsoft.Report.Design.StiSavingObjectEventArgs e)
        {
            e.Processed = true;
            Save(((Stimulsoft.Report.WpfDesign.StiWpfDesignerControl)sender).Report.SaveToByteArray());
            
        }

        private void Save(byte[] rep)
        {

            if (CurrentFormState == FormState.Update)
            {
                if (FormTitleTextBox.Text == temp && (int)RequestTypeCombBox.SelectedValue == RequestTypeID && CRM.Application.Reports.Viewer.SaveFormForm.cancel==false)
                {
                    UpdateFormTemplate = DocumentTypeDB.GetFormTemplateByID(_ID);
                    UpdateFormTemplate.Title = FormTitleTextBox.Text;
                    UpdateFormTemplate.RequestTypeID = (int)RequestTypeCombBox.SelectedValue;
                    CRM.Application.Reports.Viewer.SaveFormForm frm = new CRM.Application.Reports.Viewer.SaveFormForm(rep, UpdateFormTemplate.ID, UpdateFormTemplate.RequestTypeID, UpdateFormTemplate.Title);
                    frm.ShowDialog();
                }
            }

            if (CurrentFormState == FormState.UpdateInsert)
            {
                if (FormTitleTextBox.Text == temp && (int)RequestTypeCombBox.SelectedValue == RequestTypeID)
                {
                    UpdateFormTemplate = DocumentTypeDB.GetFormTemplateByID(_ID);
                    UpdateFormTemplate.Title = FormTitleTextBox.Text;
                    CRM.Application.Reports.Viewer.SaveFormForm frm = new CRM.Application.Reports.Viewer.SaveFormForm(rep, UpdateFormTemplate.ID, UpdateFormTemplate.RequestTypeID, UpdateFormTemplate.Title);
                    frm.ShowDialog();
                    frm.Close();
                    CurrentFormState = FormState.UpdateInsert;
                }

            }

            if (CurrentFormState == FormState.Insert)
            {
                if (FormTitleTextBox.Text == temp && (int)RequestTypeCombBox.SelectedValue == RequestTypeID)
                {
                    UpdateFormTemplate.Title = FormTitleTextBox.Text;
                    UpdateFormTemplate.RequestTypeID = (int)RequestTypeCombBox.SelectedValue;
                    CRM.Application.Reports.Viewer.SaveFormForm frm = new CRM.Application.Reports.Viewer.SaveFormForm(rep, -1,(int) UpdateFormTemplate.RequestTypeID, FormTitleTextBox.Text);
                    frm.ShowDialog();
                    _ID = (int)frm.FormID;
                    CurrentFormState = FormState.UpdateInsert;
                    frm.Close();
                }
            }

            
            }
       

        #endregion
    }
}
