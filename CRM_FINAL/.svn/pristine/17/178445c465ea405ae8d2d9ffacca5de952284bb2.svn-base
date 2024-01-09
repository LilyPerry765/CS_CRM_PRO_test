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
    public partial class SystemChangesForm : Local.PopupWindow
    {
        private int _ID = 0;

        public SystemChangesForm()
        {
            InitializeComponent();
            Initialize();
        }

        public SystemChangesForm(int id)
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

        }

        private void LoadData()
        {
            SystemChange item = new SystemChange();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.SystemChangesDB.GetSystemChangesByID(_ID);

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }



        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                SystemChange item = this.DataContext as SystemChange;

                item.Detach();
                DB.Save(item);

                ShowSuccessMessage("تغییرات ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره تغییرات", ex);
            }
        }
    }
}
