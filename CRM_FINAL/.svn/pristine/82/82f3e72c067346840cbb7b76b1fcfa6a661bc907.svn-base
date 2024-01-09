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
    public partial class E1LinkTypeForm : Local.PopupWindow
    {
        private int _ID = 0;

        public E1LinkTypeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public E1LinkTypeForm(int id)
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
            E1LinkType item = new E1LinkType();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.E1LinkTypeDB.GetE1LinkTypeByID(_ID);

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
                E1LinkType item = this.DataContext as E1LinkType;

                item.Detach();
                DB.Save(item);

                ShowSuccessMessage("نوع لینک ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره نوع لینک", ex);
            }
        }
    }
}
