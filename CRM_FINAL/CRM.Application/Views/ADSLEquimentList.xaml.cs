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
using Enterprise;
using System.Threading;
using System.Collections.ObjectModel;

namespace CRM.Application.Views
{
    public partial class ADSLEquimentList : Local.TabWindow
    {
        #region Properties

        List<ADSLEquipmentInfo> aDSLEquipmentInfoList;

        #endregion

        #region Constructor

        public ADSLEquimentList()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            CenterColumn.ItemsSource = Data.CenterDB.GetCenterCheckable();
            //PAPInfoColumn.ItemsSource = Data.PAPInfoDB.GetPAPInfoCheckable();
            ADSLPortTypeColumn.ItemsSource = Data.ADSLPortTypeDB.GetADSLPortTypeCheckable();
            //ADSLAAATypeColumn.ItemsSource = Data.ADSLAAATypeDB.GetADSLAAATypeCheckable();
            CenterComboBox.ItemsSource = Data.CenterDB.GetCenterCheckable();
            PortTypeComboBox.ItemsSource = Data.ADSLPortTypeDB.GetADSLPortTypeCheckable();
            //AAATypeComboBox.ItemsSource = Data.ADSLAAATypeDB.GetADSLAAATypeCheckable();
            //LocationInstallCulomn.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLEquimentLocationInstall));
            ProductCulomn.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLEquimentProduct));
            TypeCulomn.ItemsSource = Helper.GetEnumItem(typeof(DB.ADSLEquimentType));
            EquipmentTypeCheckableComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLEquimentType));
            //LocationInstallCheckableComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLEquimentLocationInstall));
            ProductCheckableComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ADSLEquimentProduct));
        }

        public void LoadData()
        {
            Search(null, null);
        }

        #endregion

        #region Properties

        private void TabWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ResetSearchForm(object sender, RoutedEventArgs e)
        {
            CenterComboBox.Reset();
            PortTypeComboBox.Reset();
            //AAATypeComboBox.Reset();
            EquipmentTypeCheckableComboBox.Reset();
            //LocationInstallCheckableComboBox.Reset();
            ProductCheckableComboBox.Reset();
            //SiteTextBox.Text = string.Empty;
            //ShelfComboBox.Reset();
            EquipmentTextBox.Text = string.Empty;

            Search(null, null);
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            //aDSLEquipmentInfoList = Data.ADSLEquipmentDB.SearchADSLEquipment(CenterComboBox.SelectedIDs, ShelfComboBox.SelectedIDs, PortTypeComboBox.SelectedIDs, AAATypeComboBox.SelectedIDs, EquipmentTextBox.Text.Trim(), EquipmentTypeCheckableComboBox.SelectedIDs, ProductCheckableComboBox.SelectedIDs, LocationInstallCheckableComboBox.SelectedIDs, SiteTextBox.Text.Trim());
            aDSLEquipmentInfoList = Data.ADSLEquipmentDB.SearchADSLEquipment(CenterComboBox.SelectedIDs, PortTypeComboBox.SelectedIDs,  EquipmentTextBox.Text.Trim(), EquipmentTypeCheckableComboBox.SelectedIDs, ProductCheckableComboBox.SelectedIDs);
            ItemsDataGrid.ItemsSource = aDSLEquipmentInfoList;
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ADSLEquimentForm window = new ADSLEquimentForm();
            window.ShowDialog();

            if (window.DialogResult == true)
                Search(null, null);
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLEquipmentInfo item = ItemsDataGrid.SelectedItem as Data.ADSLEquipmentInfo;
                if (item == null) return;

                ADSLEquimentForm window = new ADSLEquimentForm(item.ID);
                window.ShowDialog();

                if (window.DialogResult == true)
                    Search(null, null);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItems.ToString() == "{NewItemPlaceholder}") return;
            {
                try
                {
                    MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        List<Bucht> BuchtList = new List<Bucht>();
                        List<ADSLPort> ADSLPortList = new List<ADSLPort>();
                        ADSLEquipmentInfo item = ItemsDataGrid.SelectedItem as Data.ADSLEquipmentInfo;

                        ADSLEquipment aDSLEquipmet = DB.SearchByPropertyName<ADSLEquipment>("ID", item.ID).SingleOrDefault();
                        ADSLPortList = DB.SearchByPropertyName<ADSLPort>("ADSLEquipmentID", aDSLEquipmet.ID).OrderBy(t => t.ID).ToList();
                     //   BuchtList = Data.BuchtDB.getBuchtByADSLPortID(ADSLPortList.Select(p => p.ID).ToList()).ToList();

                        using (TransactionScope ts = new TransactionScope())
                        {
                            if (BuchtList != null)
                            {
                                if (BuchtList.Any(t => t.Status != (byte)DB.BuchtStatus.ConnectedToSpliter)) { MessageBox.Show("تجهیزات شامل بوخت متصل میباشد"); return; }
                                if (ADSLPortList.Any(t => t.Status != (byte)DB.ADSLPortStatus.Free && t.Status != (byte)DB.ADSLPortStatus.Destruction)) { MessageBox.Show("تجهیزات شامل پورت متصل میباشد"); return; }
                                foreach (Bucht bucht in BuchtList)
                                {
                                  //  bucht.ADSLPortID = null;
                                    bucht.Status = (byte)DB.BuchtStatus.ADSLFree;
                                //    bucht.ADSLType = null;
                                    bucht.Detach();

                                }

                                DB.UpdateAll(BuchtList);


                                foreach (ADSLPort aDSLPort in ADSLPortList)
                                {
                                    DB.Delete<Data.ADSLPort>(aDSLPort.ID);
                                }
                                DB.Delete<Data.ADSLEquipment>(aDSLEquipmet.ID);
                            }
                            else
                            {
                                if (ADSLPortList.Any(t => t.Status != (byte)DB.ADSLPortStatus.Free || t.Status != (byte)DB.ADSLPortStatus.Destruction)) { MessageBox.Show("تجهیزات شامل پورت متصل میباشد"); return; }
                                foreach (ADSLPort aDSLPort in ADSLPortList)
                                {
                                    DB.Delete<Data.ADSLPort>(aDSLPort.ID);
                                }
                                DB.Delete<Data.ADSLEquipment>(aDSLEquipmet.ID);
                            }
                            ts.Complete();
                            aDSLEquipmentInfoList.Remove(item);
                        }
                    }

                    ShowSuccessMessage("حذف تجهیزات ADSL انجام شد");

                }
                catch (Exception ex)
                {
                    ShowErrorMessage("حذف ADSL انجام نشد", ex);
                }
            }
        }

        private void PortsItem_Click(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex >= 0)
            {
                ADSLEquipmentInfo item = ItemsDataGrid.SelectedItem as Data.ADSLEquipmentInfo;
                if (item == null) return;

                ADSLPortsList aDSLPortsList = new ADSLPortsList(item.ID);
                Folder.Console.Navigate(aDSLPortsList, "پورت ها ADSL");
            }
        }

        #endregion
    }
}
