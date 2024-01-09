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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for CabinetTypeForm.xaml
    /// </summary>
    public partial class CabinetTypeForm : Local.PopupWindow 
    {
        private int _ID=0;

        public CabinetTypeForm()
        {
            InitializeComponent();
        }

        public CabinetTypeForm(int id):this()
        {
            _ID = id;
        }
        private void LoadData()
        {
            CabinetType cabinetType = new CabinetType();
            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
                
            }
            else
            {
                cabinetType = Data.CabinetTypeDB.GetCabinetTypeByID(_ID);
                SaveButton.Content = "بروز رسانی";
            }
            this.DataContext = cabinetType;
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                CabinetType cabinetType = this.DataContext as CabinetType;
                cabinetType.Detach();
                Save(cabinetType);
                ShowSuccessMessage("نوع کافو ذخیره شد");
            
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                else
                    ShowErrorMessage("خطا در ذخیره نوع کافو", ex);
            }
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
