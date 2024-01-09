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
    public partial class WaitingListForm : Local.PopupWindow
    {
        private int _ID = 0;

        public WaitingListForm()
        {
            InitializeComponent();
            Initialize();
        }

        public WaitingListForm(int id)
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
            EntryReasonIDComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.EntryReasonID));
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.StatusWatingList));
       
        }

        private void LoadData()
        {
            WaitingList item = new WaitingList();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.WaitingListDB.GetWaitingListByID(_ID);

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
                WaitingList item = this.DataContext as WaitingList;

                item.Detach();
                Save(item);

                ShowSuccessMessage("لیست انتظار ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره لیست انتظار", ex);
            }
        }
    }
}
