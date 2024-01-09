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
using System.Windows.Forms;
using System.Data.SqlClient;
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class SwitchPrecodeForm : Local.PopupWindow
    {
        #region Properties and Fields

        private int _ID = 0;
        private int _SwitchID = 0;
        private int CityID = 0;
        private Switch switchItem;

        #endregion

        #region Constructors

        public SwitchPrecodeForm()
        {
            InitializeComponent();
            Initialize();
        }

        public SwitchPrecodeForm(int id, int switchID)
            : this()
        {
            _ID = id;
            _SwitchID = switchID;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            switchItem = new Switch();
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            PreCodeTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.PreCodeType));
            DeploymentTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.SwitchPrecodeDeploymentType));
            UsageTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.TelephoneUsageType));
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.SwitchPreCodeStatus));
        }

        private void LoadData()
        {
            SwitchPrecode item = new SwitchPrecode();
            switchItem = Data.SwitchDB.GetSwitchByID(_SwitchID);


            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
                CityID = Data.CityDB.GetCityByCenterID(switchItem.CenterID).ID;
                item.CenterID = switchItem.CenterID;
            }
            else
            {
                item = Data.SwitchPrecodeDB.GetSwitchPrecodeByID(_ID);
                CityID = Data.SwitchPrecodeDB.GetCity(item.ID);
                StartNo.IsEnabled = false;
                EndNo.IsEnabled = false;
                SaveButton.Content = "بروزرسانی";
                AutoPortCheckBox.IsEnabled = false;
            }

            this.DataContext = item;

            if (CityID == 0)
            {
                CityComboBox.SelectedIndex = 0;
            }
            else
            {
                CityComboBox.SelectedValue = CityID;
            }
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
                bool autoPort = (bool)AutoPortCheckBox.IsChecked;
                byte? usageType = (byte?)UsageTypeComboBox.SelectedValue;

                SwitchPrecode item = this.DataContext as SwitchPrecode;
                if (!Enum.IsDefined(typeof(DB.PreCodeType), item.PreCodeType))
                {
                    throw new Exception("لطفا نوع پیش شماره را انتخاب کنید");
                }
                item.Detach();

                //    if (Data.SwitchDB.GetTypeOfSwitchTypeBySwitchID(_SwitchID) != (byte)DB.SwitchTypeCode.FixedSwitch && autoPort) { System.Windows.Forms.MessageBox.Show("فقط برای سوئیچ های ثابت میتوان ایجاد خودکار پورت داشته باشید!"); return; }

                string TelephoneFile = SwitchPrecodeDB.Save(item, StartNo.Text.Trim(), EndNo.Text.Trim(), _SwitchID, autoPort, usageType);

                if (autoPort == false)
                {
                    if (MessageBoxResult.Yes == System.Windows.MessageBox.Show("پیش شماره ذخیره شد. فایل شماره ها ایجاد شود؟", "", MessageBoxButton.YesNo, MessageBoxImage.Question))
                    {
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        fbd.SelectedPath = Convert.ToString(Environment.SpecialFolder.MyDocuments);
                        if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fbd.SelectedPath.ToString() + "\\SWITCH_PORT_INFO.TXT"))
                            {
                                file.Write(TelephoneFile);
                            }
                        }
                    }
                }
                else
                {
                }

                this.DialogResult = true;
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Cannot insert duplicate key in object"))
                {
                    ShowErrorMessage("شماره تلفن وارد شده موجود می باشد", ex);
                }
                else
                {
                    ShowErrorMessage("خطا در ذخیره پیش شماره سوئیچ در پایگاه داده", ex);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پیش شماره سوئیچ", ex);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as SwitchPrecode).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
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

        private void CenterComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UsageTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //اگر فیلد "استفاده برای" با مقادیر سیم خصوصی و یا ایوان مقداردهی شده باشد سوئیچ پورت باید به صورت اتوماتیک مقداردهی شود
            if (
                UsageTypeComboBox.SelectedValue != null
                &&
                ((byte)UsageTypeComboBox.SelectedValue == (byte)DB.TelephoneUsageType.E1 || (byte)UsageTypeComboBox.SelectedValue == (byte)DB.TelephoneUsageType.PrivateWire)
               )
            {
                AutoPortCheckBox.IsChecked = true;
                AutoPortCheckBox.IsEnabled = false;
            }
            else
            {
                AutoPortCheckBox.IsChecked = false;
                AutoPortCheckBox.IsEnabled = true;
            }
        }
        #endregion



    }
}
