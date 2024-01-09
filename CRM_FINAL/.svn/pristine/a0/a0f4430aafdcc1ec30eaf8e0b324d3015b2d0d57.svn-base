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

namespace CRM.Application.Views
{
    public partial class DocumentBaseForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        
        #endregion

        #region Constructors

        public DocumentBaseForm()
        {
            InitializeComponent();          
            Initialize();
        }

        public DocumentBaseForm(int id)
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
            DocumentType documentType = new DocumentType();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                documentType = Data.DocumentTypeDB.GetDocumentTypeById(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = documentType;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                DocumentType documentType = this.DataContext as DocumentType;
                documentType.Detach();
                DB.Save(documentType);

                ShowSuccessMessage("مدرک ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                ShowErrorMessage("خطا در ذخیره مدرک", ex);
            }
        }

        #endregion
    }
}
