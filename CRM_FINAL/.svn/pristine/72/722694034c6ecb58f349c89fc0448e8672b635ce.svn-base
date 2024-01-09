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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CRM.Application.Views
{
    public partial class ADSLPortsList : Local.TabWindow
    {
        #region Properties

        private int _ADSLEquipmentID;
        private ObservableCollection<ADSLPortsInfo> _ADSLPortsInfoList;
        public BackgroundWorker Worker;
        private List<int> _aDSLEquipmentSelectedIDs;
        private string _portNo;
        private string _address;
        private List<int> _aDSLPortStatusSelectedIDs;

        #endregion

        #region Constractor

        public ADSLPortsList()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLPortsList(int aDSLEquipmentID)
            : this()
        {
            _ADSLEquipmentID = aDSLEquipmentID;            
        }

        #endregion

        #region Worker

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _ADSLPortsInfoList = Data.ADSLPortDB.GetPortsInfoByADSLEquipmentID(_aDSLEquipmentSelectedIDs, _portNo, TelephoneNoTextBox.Text.Trim(), _address, _aDSLPortStatusSelectedIDs);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ItemsDataGrid.ItemsSource = _ADSLPortsInfoList;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            // Define BackgroundWorker and the events
            Worker = new BackgroundWorker();
            Worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            
            _aDSLEquipmentSelectedIDs = new List<int>();
            _aDSLPortStatusSelectedIDs = new List<int>();
            ADSLPortStatus.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPortStatus));
            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLPortStatus));
            ADSLEquipmentComboBox.ItemsSource = Data.ADSLEquipmentDB.GetADSLEquipmentCheckable();
        }        

        #endregion

        #region Event Handlers

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (_ADSLEquipmentID != 0)
            {
                foreach (CheckableItem item in ADSLEquipmentComboBox.Items)
                {
                    if (item.ID == _ADSLEquipmentID)
                    {
                        ADSLEquipmentComboBox.SelectedIndex = ADSLEquipmentComboBox.Items.IndexOf(item);
                        item.IsChecked = true;
                        break;
                    }
                }
            }

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            _aDSLEquipmentSelectedIDs =ADSLEquipmentComboBox.SelectedIDs;
            _portNo = PortNoTextBox.Text;
            //_address = AddressTextBox.Text;
            _aDSLPortStatusSelectedIDs = ADSLPortStatus.SelectedIDs;
            ItemsDataGrid.ItemsSource = null;

            if (!Worker.IsBusy)
                Worker.RunWorkerAsync();           
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            ADSLEquipmentComboBox.Reset();
            PortNoTextBox.Text = string.Empty;
            //AddressTextBox.Text = string.Empty;
            ADSLPortStatus.Reset();
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedItem != null)
            {
                ADSLPortsInfo item = ItemsDataGrid.SelectedItem as ADSLPortsInfo;

                ADSLAssignmentBuchtToPort window = new ADSLAssignmentBuchtToPort(item.PortID);
                window.ShowDialog();
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ItemsDataGrid.SelectedItem != null)
                {
                    ADSLPortsInfo item = ItemsDataGrid.SelectedItem as ADSLPortsInfo;
                    if (item.StatusID == (byte)DB.ADSLPortStatus.Free)
                    {
                        using (TransactionScope ts = new TransactionScope())
                        {
                            Bucht buchtInput = Data.BuchtDB.GetBuchtByID((long)item.InputBucht);
                            Bucht buchtOutput = Data.BuchtDB.GetBuchtByID((long)item.OutBucht);
                            
                            if (buchtInput != null)
                            {
                                buchtInput.SwitchPortID = null;
                                buchtInput.BuchtIDConnectedOtherBucht = null;
                                buchtInput.ADSLStatus = false;
                                buchtInput.Status = (byte)DB.BuchtStatus.ADSLFree;
                               // buchtInput.ADSLPortID = null;
                                buchtInput.Detach();
                                DB.Save(buchtInput);
                            }

                            if (buchtOutput != null)
                            {
                                buchtOutput.SwitchPortID = null;
                                buchtOutput.BuchtIDConnectedOtherBucht = null;
                                buchtOutput.ADSLStatus = false;
                                buchtOutput.Status = (byte)DB.BuchtStatus.ADSLFree;
                              //  buchtOutput.ADSLPortID = null;
                                buchtOutput.Detach();
                                DB.Save(buchtOutput);
                            }

                            DB.Delete<ADSLPort>(item.PortID);
                            ts.Complete();
                            _ADSLPortsInfoList.Remove(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("فقط پورت های آزاد را میتوانید حذف کنید", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف", ex);
            }
        }

        #endregion
    }
}
