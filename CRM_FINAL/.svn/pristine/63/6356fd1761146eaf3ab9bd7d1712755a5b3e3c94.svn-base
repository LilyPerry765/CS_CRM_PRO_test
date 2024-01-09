using CRM.Data;
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

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for CabinetInputDirectionForm.xaml
    /// </summary>
    public partial class CabinetInputDirectionForm : Local.PopupWindow
    {
        private int _cabinetID = 0;
        public CabinetInputDirectionForm()
        {
            InitializeComponent();
        }

        public CabinetInputDirectionForm(int cabinetID):this()
        {
            this._cabinetID = cabinetID;
            Initialize();
        }

        private void Initialize()
        {
            FromCabinetInput.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID(_cabinetID);
            ToCabinetInput.ItemsSource = Data.CabinetInputDB.GetCabinetInputByCabinetID(_cabinetID);
            DirectionComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.CabinetInputDirection));
        
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                if (FromCabinetInput.SelectedValue != null && ToCabinetInput.SelectedValue != null && DirectionComboBox.SelectedValue != null)
                {
                    List<CabinetInput> cabinetInputID = Data.CabinetInputDB.GetFromCabinetInputToCabinetID((long)FromCabinetInput.SelectedValue, (long)ToCabinetInput.SelectedValue);
                    cabinetInputID.ForEach((CabinetInput item) => { item.Direction = (byte)DirectionComboBox.SelectedValue; item.Detach(); });
                    DB.UpdateAll(cabinetInputID);
                }
                else
                {
                    MessageBox.Show("از ورودی و تا ورودی و سمت ورودی نمی تواند خالی باشد");

               }

                ShowSuccessMessage("ذخیره سمت مرکزی انجام شد");
            }
            catch(Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره سمت مرکزی",ex);
            }

        }
    }
}
