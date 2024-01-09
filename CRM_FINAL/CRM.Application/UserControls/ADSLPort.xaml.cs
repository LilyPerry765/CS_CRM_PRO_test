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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Data;

namespace CRM.Application.UserControls
{
    public partial class ADSLPort : UserControl
    {
        #region Properties

        private long RequestID = 0;
        private Data.ADSLRequest _ADSLRequest { get; set; }
        private Data.ADSLChangePort1 _ADSLChangePort { get; set; }
        private Data.ADSL _ADSL { get; set; }
        private Request _Request { get; set; }
        #endregion

        #region Constructors

        public ADSLPort(long requestID)
        {
            RequestID = requestID;
            _Request = Data.RequestDB.GetRequestByID(RequestID);
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {

            if (_Request.RequestTypeID == (int)DB.RequestType.ADSL)
            {
                _ADSLRequest = DB.SearchByPropertyName<ADSLRequest>("ID", RequestID).SingleOrDefault();

                ADSLEquipmentComboBox.ItemsSource = Data.ADSLEquipmentDB.GetAllADSLEquipment();

                Data.ADSLPort port;
                if (_ADSLRequest.ADSLPortID != null)
                {
                    port = Data.ADSLPortDB.GetADSlPortByID((long)_ADSLRequest.ADSLPortID);
                    ADSLEquipmentComboBox.SelectedValue = port.ADSLEquipmentID;
                }
                else
                    ADSLEquipmentComboBox.SelectedIndex = 0;
               
            }
            else if (_Request.RequestTypeID == (int)DB.RequestType.ADSLChangePort)
            {
                //_ADSLChangePort = DB.SearchByPropertyName<ADSLChangePort>("ID", RequestID).SingleOrDefault();
                //_ADSL = Data.ADSLDB.GetADSLByTelephoneNo(  _ADSLChangePort.TelephoneNo);
               

                //ADSLEquipmentComboBox.ItemsSource = Data.ADSLEquipmentDB.GetAllADSLEquipment();

                //Data.ADSLPort port;
                //if (_ADSL.ADSLPortID != null)
                //{
                //    port = Data.ADSLPortDB.GetADSlPortByID((long)_ADSL.ADSLPortID);
                //    ADSLEquipmentComboBox.SelectedValue = port.ADSLEquipmentID;
                //}
                //else
                //    ADSLEquipmentComboBox.SelectedIndex = 0;

            }
        }

        #endregion

        #region Event Handlers

        private void ADSLEquipmentComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (_Request.RequestTypeID == (int)DB.RequestType.ADSL)
            {
            if (ADSLEquipmentComboBox.SelectedItem != null)
            {
                ADSLEquipment equipment = ADSLEquipmentComboBox.SelectedItem as ADSLEquipment;
                if ((bool)AlBuchtCheckBox.IsChecked)
                    ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentID((int)equipment.ID);
                else
                    ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus(equipment.ID, (byte)DB.ADSLPortStatus.Free);

                StatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPortStatus));
            }
            else
            {
                if (_ADSLRequest.ADSLPortID != null)
                    ADSLEquipmentComboBox.SelectedValue = Data.ADSLPortDB.GetADSlPortByID((long)_ADSLRequest.ADSLPortID).ADSLEquipmentID;
                else
                    ADSLEquipmentComboBox.SelectedIndex = 0;
            }

            if (_ADSLRequest.ADSLPortID != null)
            {
                ADSLPortsInfo port = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSLRequest.ADSLPortID);
                PortInfo.DataContext = port;
            }
            else
            {
                ADSLPortsInfo port = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free).FirstOrDefault();
                PortInfo.DataContext = port;
            }
            }
            else if (_Request.RequestTypeID == (int)DB.RequestType.ADSLChangePort)
            {
                if (ADSLEquipmentComboBox.SelectedItem != null)
                {
                    ADSLEquipment equipment = ADSLEquipmentComboBox.SelectedItem as ADSLEquipment;
                    if ((bool)AlBuchtCheckBox.IsChecked)
                        ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentID((int)equipment.ID);
                    else
                        ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus(equipment.ID, (byte)DB.ADSLPortStatus.Free);

                    StatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPortStatus));
                }
                else
                {
                    if (_ADSL.ADSLPortID != null)
                        ADSLEquipmentComboBox.SelectedValue = Data.ADSLPortDB.GetADSlPortByID((long)_ADSL.ADSLPortID).ADSLEquipmentID;
                    else
                        ADSLEquipmentComboBox.SelectedIndex = 0;
                }

                if (_ADSL.ADSLPortID != null)
                {
                    ADSLPortsInfo port = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSL.ADSLPortID);
                    PortInfo.DataContext = port;
                }
                else
                {
                    ADSLPortsInfo port = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free).FirstOrDefault();
                    PortInfo.DataContext = port;
                }
            }
        }

        private void AlBuchtCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (ADSLEquipmentComboBox.SelectedValue != null)
            {
                ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentID((int)ADSLEquipmentComboBox.SelectedValue);

                ADSLPortsInfo port = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free).FirstOrDefault();
                PortInfo.DataContext = port;
            }
            else
                if (ADSLEquipmentComboBox.SelectedIndex == 0)
                {
                    ADSLEquipment Equipment = ADSLEquipmentComboBox.SelectedItem as ADSLEquipment;
                    ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentID(Equipment.ID);
                }
        }

        private void AlBuchtCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ADSLEquipmentComboBox.SelectedValue != null)
            {
                ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free);

                ADSLPortsInfo port = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus((int)ADSLEquipmentComboBox.SelectedValue, (byte)DB.ADSLPortStatus.Free).FirstOrDefault();
                PortInfo.DataContext = port;
            }
            else
                if (ADSLEquipmentComboBox.SelectedIndex == 0)
                {
                    ADSLEquipment Equipment = ADSLEquipmentComboBox.SelectedItem as ADSLEquipment;
                    ADSLPortDataGrid.ItemsSource = Data.ADSLPortDB.GetADSLPortsInfoByEquipmentIDandStatus(Equipment.ID, (byte)DB.ADSLPortStatus.Free);
                }
        }

        private void ADSLPortDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ADSLPortDataGrid.SelectedItem != null)
            {
                try
                {
                    ADSLPortsInfo port = ADSLPortDataGrid.SelectedItem as ADSLPortsInfo;

                    if (port.StatusID == (byte)DB.ADSLPortStatus.Free)
                        PortInfo.DataContext = port;
                    else
                        throw new Exception("لطفا یک پورت آزاد انتخاب نمایید !");

                }
                catch (Exception)
                {

                }

            }
        }

        private void SavedValueLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                if (_Request.RequestTypeID == (int)DB.RequestType.ADSL)
                {
                    if (_ADSLRequest.ADSLPortID != null)
                    {
                        ADSLPortsInfo port = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSLRequest.ADSLPortID);
                        PortInfo.DataContext = port;
                    }
                    else
                        throw new Exception("پورتی برای این درخواست ذخیره نشده است !");


                }
                else if (_Request.RequestTypeID == (int)DB.RequestType.ADSLChangePort)
                {

                    if (_ADSL.ADSLPortID != null)
                    {
                        ADSLPortsInfo port = Data.ADSLPortDB.GetADSlPortsInfoByID((long)_ADSL.ADSLPortID);
                        PortInfo.DataContext = port;
                    }
                    else
                        throw new Exception("پورتی برای این درخواست ذخیره نشده است !");
                }
            }
            catch (Exception)
            {
               
            }
        }

        #endregion
    }
}
