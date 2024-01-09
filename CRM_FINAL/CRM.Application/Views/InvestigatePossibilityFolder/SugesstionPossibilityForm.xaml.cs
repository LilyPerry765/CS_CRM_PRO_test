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
    public partial class SugesstionPossibilityForm : Local.PopupWindow
    {
        private long _SugesstionPossibilityID = 0;
        private long _InvestigatePossibilityID = 0;


        public SugesstionPossibilityForm()
        {
            InitializeComponent();
            Initialize();
        }

        public SugesstionPossibilityForm(long investigatePossibilityID, long sugesstionPossibilityID)
            : this()
        {
            _SugesstionPossibilityID = sugesstionPossibilityID;
            _InvestigatePossibilityID = investigatePossibilityID;
        }

       

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {
            SourceTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.SourceType));
            ConnectionTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PostContactConnectionType));

            VisitAddressComboBox.ItemsSource = Data.VisitAddressDB.GetVisitAddressCheckable();

        }

        private void LoadData()
        {
            SugesstionPossibility item = new SugesstionPossibility();

            if (_SugesstionPossibilityID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.SugesstionPossibilityDB.GetSugesstionPossibilityByID(_SugesstionPossibilityID);

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
                SugesstionPossibility item = this.DataContext as SugesstionPossibility;

                item.InsertDate = DB.GetServerDate();
                item.InvestigatePossibilityID = _InvestigatePossibilityID;
                item.Detach();
                DB.Save(item);

                ShowSuccessMessage("امکانات پیشنهادی ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                ShowErrorMessage("خطا در ذخیره امکانات پیشنهادی", ex);
            }
        }
    }
}
