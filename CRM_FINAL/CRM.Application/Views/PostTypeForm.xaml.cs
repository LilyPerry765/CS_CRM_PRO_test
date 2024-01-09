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
    /// Interaction logic for PostTypeForm.xaml
    /// </summary>
    public partial class PostTypeForm : Local.PopupWindow
    {
        private int _ID=0;

        public PostTypeForm()
        {
            InitializeComponent();
        }

        public PostTypeForm(int id):this()
        {
            
            _ID = id;
        }

        private void LoadData()
        {
            PostType postType = new PostType();
            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                postType = Data.PostTypeDB.GetPostTypeByID(_ID);
                SaveButton.Content = "بروز رسانی";

            }
            this.DataContext = postType;
            
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                PostType postType = this.DataContext as PostType;

                postType.Detach();
                Save(postType);

                ShowSuccessMessage("استان ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره استان", ex);
            }

        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
