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
    public partial class CableUsedChannelForm : Local.PopupWindow
    {
        private int _ID = 0;

        public CableUsedChannelForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CableUsedChannelForm(int id)
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
            CableUsedChannel item = new CableUsedChannel();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.CableUsedChannelDB.GetCableUsedChannelByID(_ID);

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
                CableUsedChannel item = this.DataContext as CableUsedChannel;
                if (item.IsReadOnly == false)
                {
                    item.Detach();
                    Save(item);
                    ShowSuccessMessage("کانال مورد استفاده کابل ذخیره شد");
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("امکان تغییر این نوع کانال مورد استفاده موجود نمی باشد");
                }
               
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره کانال مورد استفاده کابل", ex);
            }
        }
    }
}
