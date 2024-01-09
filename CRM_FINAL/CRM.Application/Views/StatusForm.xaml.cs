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
    public partial class StatusForm : Local.PopupWindow
    {
        private int _ID = 0;

        public StatusForm()
        {
            InitializeComponent();
            Initialize();
        }

        public StatusForm(int id)
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
            RequestStepComboBox.ItemsSource = Data.RequestStepDB.GetRequestStepCheckable();
            RequestStatusTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.RequestStatusType));
        }

        private void LoadData()
        {
            Status item = new Status();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.StatusDB.GetStatusByID(_ID);

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
                Status item = this.DataContext as Status;
                using (MainDataContext context = new MainDataContext())
                {
                    item.InsertDate = DB.GetServerDate();
                }
                item.Detach();
                Save(item);

                ShowSuccessMessage("وضعیت ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره وضعیت", ex);
            }
        }
    }
}
