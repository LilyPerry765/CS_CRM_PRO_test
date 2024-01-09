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
    public partial class SpaceAndPowerCustomerForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;
        public SpaceAndPowerCustomer customer { get; set; }

        #endregion

        #region Constructors

        public SpaceAndPowerCustomerForm()
        {
            InitializeComponent();
            Initialize();

            customer = new SpaceAndPowerCustomer();           
        }

        public SpaceAndPowerCustomerForm(long id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {            
        }

        private void LoadData()
        {
            SpaceAndPowerCustomer item = new SpaceAndPowerCustomer();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = SpaceAndPowerCustomerDB.GetCustomerByID(_ID);

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }
        
        #endregion

        #region Event Handlers

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                SpaceAndPowerCustomer item = this.DataContext as SpaceAndPowerCustomer;

                item.Detach();
                Save(item);

                ShowSuccessMessage("متقاضی فضا و پاور ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره متقاضی فضا و پاور", ex);
            }
        }
        
        #endregion
    }
}
