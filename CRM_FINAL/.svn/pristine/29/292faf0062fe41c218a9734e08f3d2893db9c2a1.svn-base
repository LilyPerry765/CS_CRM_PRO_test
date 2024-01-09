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
using System.Transactions;

namespace CRM.Application.Views
{
    public partial class SwitchPortToNumber : Local.PopupWindow
    {
        private int CityID = 0;
        public SwitchPortToNumber()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
          
            Status.ItemsSource = Helper.GetEnumItem(typeof(DB.SwitchPortStatus));
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                using(TransactionScope ts = new TransactionScope())
                {

                string prePortNo = PrePortNoTextBox.Text.Trim();
                int fromPortNumber = Convert.ToInt32(FromPostPortNoTextBox.Text);
                int toPortNumber = Convert.ToInt32(ToPostPortNoTextBox.Text);

                if (fromPortNumber <= toPortNumber)
                {
                    List<SwitchPort> switchPortList = new List<SwitchPort>();
                    for (int i = fromPortNumber; i <= toPortNumber; i++)
                    {
                        SwitchPort switchPort = new SwitchPort();
                        switchPort.PortNo = prePortNo + i.ToString();
                        switchPort.Status = (byte)Status.SelectedValue;
                        switchPort.SwitchID = (int)SwitchComboBox.SelectedValue;
                        switchPort.Type = TypeCheckBox.IsChecked;
                        switchPortList.Add(switchPort);
                    }

                    DB.SaveAll(switchPortList);
                    ts.Complete();
                    this.DialogResult = true;
                }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره پورت ، " + ex.Message, ex);
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                CenterComboBox.SelectedIndex = 0;
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
            if (CenterComboBox.SelectedValue != null)
            {
                SwitchComboBox.ItemsSource = Data.SwitchDB.GetSwitchCheckableByCenterID((int)CenterComboBox.SelectedValue);
            }

        }

        private void SwitchComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (SwitchComboBox.SelectedValue != null)
            {
                SwitchType switchTypeItem = Data.SwitchDB.GetSwitchTypeBySwitchID((int)SwitchComboBox.SelectedValue);
                if (switchTypeItem.SwitchTypeValue == (byte)DB.SwitchTypeCode.FixedSwitch)
                { 
                    TypeCheckBox.IsChecked = false;
                }
                else
                {
                    TypeCheckBox.IsChecked = true;
                }
                

            }
        }
    }
}
