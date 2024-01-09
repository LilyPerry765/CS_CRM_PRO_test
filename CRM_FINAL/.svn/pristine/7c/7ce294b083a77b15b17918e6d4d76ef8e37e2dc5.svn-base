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
    public partial class RegionForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;

        #endregion

        #region Constructors

        public RegionForm()
        {
            InitializeComponent();
            Initialize();
        }

        public RegionForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetCitiesCheckable();
        }

        private void LoadData()
        {
            Region city = new Region();

            if (_ID == 0)
                SaveButton.Content = "ذخیره";
            else
            {
                city = Data.RegionDB.GetRegionById(_ID);
                SaveButton.Content = "بروز رسانی";
            }

            this.DataContext = city;
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                Region region = this.DataContext as Region;
                region.Detach();
                DB.Save(region);

                ShowSuccessMessage("منطقه ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره منطقه", ex);
            }
        }

        #endregion
    }
}
