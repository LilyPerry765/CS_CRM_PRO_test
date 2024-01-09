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
using System.ComponentModel;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class WaitingListReasonForm : Local.PopupWindow
    {
        private int _ID = 0;

        public WaitingListReasonForm()
        {
            InitializeComponent();
            Initialize();
        }

        public WaitingListReasonForm(int id)
            : this()
        {
            _ID = id;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {
            RequestTypeComboBox.ItemsSource = Data.RequestTypeDB.GetRequestTypeCheckable();

        }

        private void LoadData()
        {
            WaitingListReason item = new WaitingListReason();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.WaitingListReasonDB.GetWaitingListReasonByID(_ID);

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                WaitingListReason item = this.DataContext as WaitingListReason;

                item.Detach();
                DB.Save(item);

                ShowSuccessMessage("علت لیست انتظار ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره علت لیست انتظار", ex);
            }
        }
    }
}
