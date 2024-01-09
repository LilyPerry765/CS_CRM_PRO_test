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
    public partial class OfficeEmployeeForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public OfficeEmployeeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public OfficeEmployeeForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            OfficeComboBox.ItemsSource = Data.OfficeDB.GetOfficesCheckable();
        }

        private void LoadData()
        {
            OfficeEmployee officeEmployee = new OfficeEmployee();


            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                officeEmployee = Data.OfficeDB.GetOfficeEmployeeByID(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = officeEmployee;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                OfficeEmployee officeEmployee = this.DataContext as OfficeEmployee;

                officeEmployee.Detach();
                Save(officeEmployee);

                ShowSuccessMessage("کارمند دفتر خدماتی ذخیره شد");
                
                this.DialogResult = true;
            }
            catch (Exception ex)
            {

                ShowErrorMessage("خطا در ذخیره کارمند دفتر خدماتی", ex);
            }
        }

        #endregion
    }
}
