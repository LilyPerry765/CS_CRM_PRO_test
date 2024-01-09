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
    public partial class RegularExpressionsForm : Local.PopupWindow
    {
        private int _ID = 0;

        public RegularExpressionsForm()
        {
            InitializeComponent();
          
        }

        public RegularExpressionsForm(int id)
            : this()
        {
            _ID = id;
            Initialize();
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
            CRM.Data.RegularExpression item = new CRM.Data.RegularExpression();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.RegularExpressionsDB.GetRegularExpressionsByID(_ID);

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
                RegularExpression item = this.DataContext as RegularExpression;

                item.Detach();
                DB.Save(item);

                ShowSuccessMessage("عبارت منظم ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره عبارت منظم", ex);
            }
        }
    }
}
