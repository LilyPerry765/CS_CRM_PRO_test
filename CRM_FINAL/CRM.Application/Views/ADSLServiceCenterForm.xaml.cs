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
    public partial class ADSLServiceCenterForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private byte _Mode;
        private static List<CheckableItem> _CenterIDs = null;

        #endregion

        #region Constructors

        public ADSLServiceCenterForm()
        {
            InitializeComponent();
            Initialize();
        }

        public ADSLServiceCenterForm(int id, byte mode)
            : this()
        {
            _ID = id;
            _Mode = mode;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _CenterIDs = null;
            CityComboBox.ItemsSource = CityDB.GetAvailableCityCheckable();
            AccessListBox.ItemsSource = _CenterIDs = Data.CenterDB.GetCenterCheckable();
        }

        private void LoadData()
        {
            try
            {
                switch (_Mode)
                {
                    case (byte)DB.ADSLServiceCenterMode.Service:

                        ADSLServiceCenter serviceCenter = new ADSLServiceCenter();
                        AccessListBox.ItemsSource = null;

                        List<ADSLServiceCenter> serviceCenterList = ADSLServiceCenterDB.GetADSLServiceCenterByServiceId(_ID);

                        foreach (ADSLServiceCenter currentAccess in serviceCenterList)
                            if (_CenterIDs.Count != 0)
                                if (_CenterIDs.Select(t => t.ID).Contains(currentAccess.CenterID))
                                    _CenterIDs.Where(t => (int)t.ID == currentAccess.CenterID).SingleOrDefault().IsChecked = true;

                        AccessListBox.ItemsSource = _CenterIDs;

                        this.DataContext = serviceCenter;
                        break;

                    case (byte)DB.ADSLServiceCenterMode.ServiceGroup:

                        ADSLServiceGroupCenter serviceGroupCenter = new ADSLServiceGroupCenter();
                        AccessListBox.ItemsSource = null;

                        List<ADSLServiceGroupCenter> serviceGroupCenterList = ADSLServiceCenterDB.GetADSLServiceGroupCenterByIServiceGroupId(_ID);

                        foreach (ADSLServiceGroupCenter currentAccess in serviceGroupCenterList)
                            if (_CenterIDs.Count != 0)
                                if (_CenterIDs.Select(t => t.ID).Contains(currentAccess.CenterID))
                                    _CenterIDs.Where(t => (int)t.ID == currentAccess.CenterID).SingleOrDefault().IsChecked = true;

                        AccessListBox.ItemsSource = _CenterIDs;

                        this.DataContext = serviceGroupCenter;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message, ex);
            }

            //ResizeWindow();
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
                    case (byte)DB.ADSLServiceCenterMode.Service:

                        ADSLServiceCenter aDSLServiceCenter = this.DataContext as ADSLServiceCenter;
                        List<int> selectedCenterIDs1 = AccessListBox.Items.Cast<CheckableItem>().ToList().Where(t => t.IsChecked == true).Select(t => (int)t.ID).ToList();

                        using (MainDataContext context = new MainDataContext())
                        {
                            if (CityComboBox.SelectedValue != null)
                                context.ExecuteCommand("DELETE FROM ADSLServiceCenter WHERE ADSLServiceID = {0} and CenterID IN (select ID from Center where RegionID = {1})", _ID, (int)CityComboBox.SelectedValue);
                            else
                                context.ExecuteCommand("DELETE FROM ADSLServiceCenter WHERE ADSLServiceID = {0}", _ID);

                            List<ADSLServiceCenter> accessList = new List<ADSLServiceCenter>();
                            foreach (int centerID in selectedCenterIDs1)
                            {
                                accessList.Add(new ADSLServiceCenter
                                {
                                    ADSLServiceID = _ID,
                                    CenterID = centerID
                                });
                            }
                            context.ADSLServiceCenters.InsertAllOnSubmit(accessList);
                            context.SubmitChanges();
                        }

                        break;

                    case (byte)DB.ADSLServiceCenterMode.ServiceGroup:

                        ADSLServiceGroupCenter aDSLServiceGroupCenter = this.DataContext as ADSLServiceGroupCenter;
                        List<int> selectedCenterIDs2 = AccessListBox.Items.Cast<CheckableItem>().ToList().Where(t => t.IsChecked == true).Select(t => (int)t.ID).ToList();

                        using (MainDataContext context = new MainDataContext())
                        {
                            if (CityComboBox.SelectedValue != null)
                                context.ExecuteCommand("DELETE FROM ADSLServiceGroupCenter WHERE ServiceGroupID = {0} and CenterID IN (select ID from Center where RegionID = {1})", _ID, (int)CityComboBox.SelectedValue);
                            else
                                context.ExecuteCommand("DELETE FROM ADSLServiceGroupCenter WHERE ServiceGroupID = {0}", _ID);
                            

                            List<ADSLServiceGroupCenter> accessList = new List<ADSLServiceGroupCenter>();
                            foreach (int centerID in selectedCenterIDs2)
                            {
                                accessList.Add(new ADSLServiceGroupCenter
                                {
                                    ServiceGroupID = _ID,
                                    CenterID = centerID
                                });
                            }
                            context.ADSLServiceGroupCenters.InsertAllOnSubmit(accessList);
                            context.SubmitChanges();

                            List<ADSLService> serviceofGroup = ADSLServiceDB.GetADSLServicebyGroupID(_ID);
                            foreach (ADSLService currentTariff in serviceofGroup)
                            {
                                if (CityComboBox.SelectedValue != null)
                                    context.ExecuteCommand("DELETE FROM ADSLServiceCenter WHERE ADSLServiceID = {0} and CenterID IN (select ID from Center where RegionID = {1})", _ID, (int)CityComboBox.SelectedValue);
                                else
                                    context.ExecuteCommand("DELETE FROM ADSLServiceCenter WHERE ADSLServiceID = {0}", _ID);

                                List<ADSLServiceCenter> serviceAccessList = new List<ADSLServiceCenter>();
                                foreach (int centerID in selectedCenterIDs2)
                                {
                                    serviceAccessList.Add(new ADSLServiceCenter
                                    {
                                        ADSLServiceID = currentTariff.ID,
                                        CenterID = centerID
                                    });
                                }
                                context.ADSLServiceCenters.InsertAllOnSubmit(serviceAccessList);
                                context.SubmitChanges();
                            }
                        }

                        break;

                    default:
                        break;
                }




                this.DialogResult = true;
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
                AccessListBox.ItemsSource = _CenterIDs = Data.CenterDB.GetCenterCheckableByCityId((int)CityComboBox.SelectedValue);

                LoadData();
            }
        }

        private void SelectAllCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            AccessListBox.ItemsSource = null;

            foreach (CheckableItem item in _CenterIDs)
                item.IsChecked = true;

            AccessListBox.ItemsSource = _CenterIDs;
        }

        private void SelectAllCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            AccessListBox.ItemsSource = null;

            foreach (CheckableItem item in _CenterIDs)
                item.IsChecked = false;

            AccessListBox.ItemsSource = _CenterIDs;
        }

        #endregion
    }
}
