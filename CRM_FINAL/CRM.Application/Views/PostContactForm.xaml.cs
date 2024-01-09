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
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class PostContactForm : Local.PopupWindow
    {
        private long _ID = 0;
        List<Bucht> buchtList = new List<Bucht>();
        public PostContactForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PostContactForm(long id)
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
            ConnectionTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.PostContactConnectionType));

            List<EnumItem> postContactStatus = Helper.GetEnumItem(typeof(DB.PostContactStatus));
            postContactStatus.RemoveAll(t => t.ID == (int)DB.PostContactStatus.Deleted || t.ID == (int)DB.PostContactStatus.NoCableConnection);
            StatusComboBox.ItemsSource = postContactStatus;           

        }

        private void LoadData()
        {
            PostContact item = new PostContact();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.PostContactDB.GetPostContactByID(_ID);
                PostComboBox.ItemsSource = Data.PostDB.GetPostCheckableByPostContact(_ID);
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
                PostContact item = this.DataContext as PostContact;
                using (TransactionScope ts = new TransactionScope())
                {
                    item.Detach();
                    Save(item);

                    ShowSuccessMessage("اتصال پست ذخیره شد");
                    this.DialogResult = true;
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اتصال پست", ex);
            }
        }

        private void CablePairNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

           
        }

     
    }
}
