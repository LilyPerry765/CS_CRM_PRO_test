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
    public partial class ADSLServiceSellerForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private byte _Mode;
        private static List<CheckableItem> _SellerIDs = null;

        #endregion

        #region Constructors

        public ADSLServiceSellerForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLServiceSellerForm(int id, byte mode)
            : this()
        {
            _ID = id;
            _Mode = mode;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _SellerIDs = null;
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            AccessListBox.ItemsSource = _SellerIDs = Data.ADSLSellerGroupDB.GetADSLSellerAgentCheckable();
        }

        private void LoadData()
        {
            try
            {
                switch (_Mode)
                {
                    case (byte)DB.ADSLServiceSellerMode.Service:

                        ADSLServiceSeller serviceSeller = new ADSLServiceSeller();
                        AccessListBox.ItemsSource = null;

                        List<ADSLServiceSeller> serviceSellerList = ADSLServiceSellerDB.GetADSLServiceSellerByServiceId(_ID);

                        foreach (ADSLServiceSeller currentAccess in serviceSellerList)
                            if (_SellerIDs.Count != 0)
                                if (_SellerIDs.Select(t => t.ID).Contains(currentAccess.SellerAgentID))
                                    _SellerIDs.Where(t => (int)t.ID == currentAccess.SellerAgentID).SingleOrDefault().IsChecked = true;

                        AccessListBox.ItemsSource = _SellerIDs;

                        this.DataContext = serviceSeller;
                        break;

                    case (byte)DB.ADSLServiceSellerMode.ServiceGroup:

                        ADSLServiceGroupSeller serviceGroupSeller = new ADSLServiceGroupSeller();
                        AccessListBox.ItemsSource = null;

                        List<ADSLServiceGroupSeller> serviceGroupSellerList = ADSLServiceSellerDB.GetADSLServiceGroupSellerByIServiceGroupId(_ID);

                        foreach (ADSLServiceGroupSeller currentAccess in serviceGroupSellerList)
                            if (_SellerIDs.Count != 0)
                                if (_SellerIDs.Select(t => t.ID).Contains(currentAccess.SellerAgentID))
                                    _SellerIDs.Where(t => (int)t.ID == currentAccess.SellerAgentID).SingleOrDefault().IsChecked = true;

                        AccessListBox.ItemsSource = _SellerIDs;

                        this.DataContext = serviceGroupSeller;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            ResizeWindow();
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
                switch (_Mode)
                {
                    case (byte)DB.ADSLServiceSellerMode.Service:

                        ADSLServiceSeller aDSLServiceSeller = this.DataContext as ADSLServiceSeller;
                        List<int> selectedSellerIDs1 = AccessListBox.Items.Cast<CheckableItem>().ToList().Where(t => t.IsChecked == true).Select(t => (int)t.ID).ToList();

                        using (MainDataContext context = new MainDataContext())
                        {
                            if (CityComboBox.SelectedValue != null)
                                context.ExecuteCommand("DELETE FROM ADSLServiceSeller WHERE ADSLServiceID = {0} and SellerAgentID IN (select ID from ADSLSellerAgent where CityID = {1})", _ID, (int)CityComboBox.SelectedValue);
                            else
                                context.ExecuteCommand("DELETE FROM ADSLServiceSeller WHERE ADSLServiceID = {0}", _ID);

                            List<ADSLServiceSeller> accessList = new List<ADSLServiceSeller>();
                            foreach (int sellerAgentID in selectedSellerIDs1)
                            {
                                accessList.Add(new ADSLServiceSeller
                                {
                                    ADSLServiceID = _ID,
                                    SellerAgentID = sellerAgentID
                                });
                            }
                            context.ADSLServiceSellers.InsertAllOnSubmit(accessList);
                            context.SubmitChanges();
                        }

                        break;

                    case (byte)DB.ADSLServiceSellerMode.ServiceGroup:

                        ADSLServiceGroupSeller aDSLServiceGroupSeller = this.DataContext as ADSLServiceGroupSeller;
                        List<int> selectedSellerIDs2 = AccessListBox.Items.Cast<CheckableItem>().ToList().Where(t => t.IsChecked == true).Select(t => (int)t.ID).ToList();

                        using (MainDataContext context = new MainDataContext())
                        {
                            if (CityComboBox.SelectedValue != null)
                            context.ExecuteCommand("DELETE FROM ADSLServiceGroupSeller WHERE ServiceGroupID = {0} and SellerAgentID IN (select ID from ADSLSellerAgent where CityID = {1})", _ID, (int)CityComboBox.SelectedValue);
                            else
                                context.ExecuteCommand("DELETE FROM ADSLServiceGroupSeller WHERE ServiceGroupID = {0}", _ID);
                            List<ADSLServiceGroupSeller> accessList = new List<ADSLServiceGroupSeller>();
                            foreach (int sellerAgentID in selectedSellerIDs2)
                            {
                                accessList.Add(new ADSLServiceGroupSeller
                                {
                                    ServiceGroupID = _ID,
                                    SellerAgentID = sellerAgentID
                                });
                            }
                            context.ADSLServiceGroupSellers.InsertAllOnSubmit(accessList);
                            context.SubmitChanges();

                            List<ADSLService> serviceofGroup = ADSLServiceDB.GetADSLServicebyGroupID(_ID);
                            foreach (ADSLService currentTariff in serviceofGroup)
                            {
                                if (CityComboBox.SelectedValue != null)
                                    context.ExecuteCommand("DELETE FROM ADSLServiceSeller WHERE ADSLServiceID = {0} and SellerAgentID IN (select ID from ADSLSellerAgent where CityID = {1})", _ID, (int)CityComboBox.SelectedValue);
                                else
                                    context.ExecuteCommand("DELETE FROM ADSLServiceSeller WHERE ADSLServiceID = {0}", _ID);

                                List<ADSLServiceSeller> serviceAccessList = new List<ADSLServiceSeller>();
                                foreach (int sellerAgentID in selectedSellerIDs2)
                                {
                                    serviceAccessList.Add(new ADSLServiceSeller
                                    {
                                        ADSLServiceID = currentTariff.ID,
                                        SellerAgentID = sellerAgentID
                                    });
                                }
                                context.ADSLServiceSellers.InsertAllOnSubmit(serviceAccessList);
                                context.SubmitChanges();
                            }
                        }

                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره مجوز نماینده فروش", ex);
            }
        }

        private void ListBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (e.KeyboardDevice.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                switch (e.Key)
                {
                    case Key.A:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = true;
                        listBox.Items.Refresh();
                        break;

                    case Key.N:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = false;
                        listBox.Items.Refresh();
                        break;

                    case Key.R:
                        foreach (CheckableItem item in listBox.ItemsSource as List<CheckableItem>)
                            item.IsChecked = !item.IsChecked;
                        listBox.Items.Refresh();
                        break;

                    default:
                        break;
                }
            }
        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectAllCheckBox.IsChecked = false;

            if (CityComboBox.SelectedValue != null)
            {
                AccessListBox.ItemsSource = _SellerIDs = Data.ADSLSellerGroupDB.GetADSLSellerAgentCheckablebyCityID((int)CityComboBox.SelectedValue);

                LoadData();
            }
        }

        private void SelectAllCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AccessListBox.ItemsSource = null;

            foreach (CheckableItem item in _SellerIDs)
                item.IsChecked = true;
            
            AccessListBox.ItemsSource = _SellerIDs;
        }

        private void SelectAllCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            AccessListBox.ItemsSource = null;

            foreach (CheckableItem item in _SellerIDs)
                item.IsChecked = false;

            AccessListBox.ItemsSource = _SellerIDs;
        }

        #endregion
    }
}
