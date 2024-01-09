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
using Microsoft.Win32;
using System.ComponentModel;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for RequestRejectReasonForm.xaml
    /// </summary>
    public partial class RequestRejectReasonForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        public RequestRejectReasonForm()
        {
            InitializeComponent();
             Initialize();
        }

        public RequestRejectReasonForm (int id)
            : this()
        {
            _ID = id;
        }

       

        #region Methods

        private void Initialize()
        {
            RequestStepComboBox.ItemsSource=RequestStepDB.GetRequestStepCheckable();
        }
        
        private void LoadData()
        {
            RequestRejectReason requestRejectReason = new RequestRejectReason();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {

                requestRejectReason = Data.RequestRejectReasonDB.GetRequestRejectReasonByID(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = requestRejectReason;
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
                RequestRejectReason requestRejectReason = this.DataContext as RequestRejectReason;
                requestRejectReason.Detach();
                Save(requestRejectReason);

                ShowSuccessMessage("رکورد مورد نظر ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage("خطا در ذخیره شهر", ex);
            }
        }

        #endregion
        }
    }

