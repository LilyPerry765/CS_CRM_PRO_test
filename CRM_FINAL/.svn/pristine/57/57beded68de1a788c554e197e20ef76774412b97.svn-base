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
    public partial class SwitchForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private int CityID = 0;

        #endregion

        #region Constructors

        public SwitchForm()
        {
            InitializeComponent();
            Initialize();
        }

        public SwitchForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            SwitchTypeComboBox.ItemsSource = Data.SwitchTypeDB.GetSwitchCheckable();
            WorkUnitResponsibleComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.WorkUnitResponsible));
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.Status));
            CenterColumn.ItemsSource = Data.CenterDB.GetCenters();
            PreCodeTypeColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.PreCodeType));
            //SwitchColumn.ItemsSource = Data.SwitchDB.GetSwitchCheckable();
            DeploymentTypeColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.SwitchPrecodeDeploymentType));
            DorshoalNumberTypeColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.DorshoalNumberType));
            StatusColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.SwitchPreCodeStatus));
        }

        private void LoadData()
        {
            Switch item = new Switch();
            List<SwitchPrecode> switchPreCodeList = new List<SwitchPrecode>();

            if (_ID == 0)
                Save.Content = "ذخیره";
            else
            {
                item = SwitchDB.GetSwitchByID(_ID);
                switchPreCodeList = SwitchPrecodeDB.GetSwitchPrecodeBySwitchIDWithRemotePreCode(_ID);
                CityID = Data.SwitchDB.GetCity(item.ID);

                Save.Content = "بروز رسانی";
            }

            this.DataContext = item;
            SwitchPreCodeDataGrid.DataContext = switchPreCodeList;

            if (CityID == 0)
                CityComboBox.SelectedIndex = 0;
            else
                CityComboBox.SelectedValue = CityID;
        }

        #endregion

        #region Event Handlers

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            try
            {
                Switch switchData = this.DataContext as Switch;
                if (Data.SwitchDB.ExistSwitchCode(switchData.CenterID, switchData.SwitchCode))
                {
                    Folder.MessageBox.ShowError("!در مرکز تعیین شده،این کد سوئیچ موجود است");
                    return;
                }

                switchData.Detach();
                Save(switchData);
                _ID = switchData.ID;
                LoadData();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره سوئیچ", ex);
            }
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            SwitchPrecodeForm window = new SwitchPrecodeForm(0, _ID);
            window.ShowDialog();

            if (window.DialogResult == true)
                LoadData();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (SwitchPreCodeDataGrid.SelectedIndex >= 0)
            {
                SwitchPrecode item = SwitchPreCodeDataGrid.SelectedItem as SwitchPrecode;

                if (item == null) return;

                SwitchPrecodeForm window = new SwitchPrecodeForm(item.ID, _ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    LoadData();
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (SwitchPreCodeDataGrid.SelectedIndex < 0 || SwitchPreCodeDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("با حذف پیش شماره همه شماره های آزاد حذف می شوند. آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.SwitchPrecode item = SwitchPreCodeDataGrid.SelectedItem as CRM.Data.SwitchPrecode;

                    List<Telephone> telephones = Data.TelephoneDB.GetTelephoneBySwitchPreCodeID(item.ID);
                    if (telephones.Any(t => t.Status != (byte)DB.TelephoneStatus.Free)) { MessageBox.Show("فقط تلفن های آزاد را می توانید حذف کنید!"); return; }

                    DB.Delete<Data.SwitchPrecode>(item.ID);
                    ShowSuccessMessage("پیش شماره سوئیچ مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف پیش شماره سوئیچ", ex);
            }
        }

        private void SwitchPreCodeDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                SwitchPrecode SwitchPrecode = e.Row.Item as SwitchPrecode;
                SwitchPrecode.SwitchID = _ID;
                SwitchPrecode.Detach();
                DB.Save(SwitchPrecode);
                ShowSuccessMessage("سوئیچ مورد ویرایش شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ویرایش سوئیچ", ex);
            }
        }

        private void SwitchTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SwitchTypeComboBox.SelectedValue != null)
            {
                byte? switchType = Data.SwitchTypeDB.GetSwitchTypeByID((int)SwitchTypeComboBox.SelectedValue).SwitchTypeValue;

                if (
                      switchType != null
                      &&
                      (switchType == (byte)Data.DB.SwitchTypeCode.ONUVWire || switchType == (byte)Data.DB.SwitchTypeCode.ONUCopper || switchType == (byte)Data.DB.SwitchTypeCode.ONUABWire)
                   )
                {
                    FeatureONUTextBox.IsReadOnly = false;
                }
                else
                {
                    FeatureONUTextBox.Text = null;
                    FeatureONUTextBox.IsReadOnly = true;
                }
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as Switch).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
            }
            else
            {
                if (CityComboBox.SelectedValue == null)
                {
                    City city = Data.CityDB.GetCityById(CityID);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
                else
                {
                    City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
            }
        }

        #endregion
    }
}
