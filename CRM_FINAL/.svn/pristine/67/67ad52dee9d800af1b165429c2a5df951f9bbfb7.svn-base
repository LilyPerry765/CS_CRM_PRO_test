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
    public partial class OtherCostForm : Local.PopupWindow
    {
        private int _ID = 0;
        OtherCost otherCost { get; set; }
        public OtherCostForm()
        {
            InitializeComponent();
            Initialize();
        }

        public OtherCostForm(int id)
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
            WorkUnitComboBox.ItemsSource=Helper.GetEnumItem(typeof(DB.WorkUnit));
            IsActiveComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.IsActive));

            otherCost = new OtherCost();
            otherCost.InsertDate = DB.GetServerDate();
            this.DataContext = otherCost;

        }

        private void LoadData()
        {
            if (_ID == 0)
            {
                Initialize();
                SaveButton.Content = "ذخیره";
            }
            else
            {
              
                SaveButton.Content = "بروزرسانی";
                otherCost = Data.OtherCostDB.GetOtherCostByID(_ID);
            }
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                OtherCost item = this.DataContext as OtherCost;

                item.Detach();
                Save(item);

                ShowSuccessMessage("هزینه متفرقه ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage("خطا در ذخیره هزینه متفرقه", ex);
            }
        }
    }
}
