using CRM.Application.UserControls;
using CRM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for CenterToCenterTranslationChooseNumberInfoFrom.xaml
    /// </summary>
    public partial class CenterToCenterTranslationSwitchForm : Local.RequestFormBase
    {
        private long requestID = 0;
        private Request request { get; set; }
        CRM.Application.UserControls.CenterToCenterTranslationInfo _centerToCenterTranslationInfo;
        Data.CenterToCenterTranslation _centerToCenterTranslation { get; set; }
        ObservableCollection<CenterToCenterTranslationChooseNumberInfo> telphonNumbers;

        public ObservableCollection<CheckableItem> _preCodes { get; set; }
        public ObservableCollection<Telephone> _newTelephons { get; set; }

        public CenterToCenterTranslationSwitchForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
        }
        public CenterToCenterTranslationSwitchForm(long ID)
            : this()
        {
            this.requestID = ID;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

            _centerToCenterTranslationInfo = new CRM.Application.UserControls.CenterToCenterTranslationInfo(requestID);
            TranslationInfo.DataContext = _centerToCenterTranslationInfo;
            TranslationInfo.Content = _centerToCenterTranslationInfo;

            _centerToCenterTranslation = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslation(requestID);
            request = Data.RequestDB.GetRequestByID(requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = request.StatusID;
            

            telphonNumbers = new ObservableCollection<CenterToCenterTranslationChooseNumberInfo>( Data.CenterToCenterTranslationDB.GetTelephones(_centerToCenterTranslation));
            _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_centerToCenterTranslation.TargetCenterID));

            List<CenterToCenterTranslationTelephone> centerToCenterTranslationTelephones = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslationTelephon(requestID).ToList();
            centerToCenterTranslationTelephones.ForEach(item => 
                                                           {
                                                               if (telphonNumbers.Any(t => t.TelephonNo == item.TelephoneNo))
                                                               {
                                                                   telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewTelephonNo = item.NewTelephoneNo;
                                                                   telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeID  = item.NewSwitchPrecodeID;
                                                                   telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == item.NewSwitchPrecodeID).SingleOrDefault().Name));
                                                               }
                                                           }
                                                           );


            TelItemsDataGrid.DataContext = telphonNumbers;

            if (request.CenterID == _centerToCenterTranslation.TargetCenterID)
            {
              IsVIPCheckBoxColumn.Visibility = Visibility.Collapsed;
              IsRoundCheckBoxColumn.Visibility = Visibility.Collapsed;
              TelephonNoTextColumn.Visibility = Visibility.Collapsed;
              TelephoneDetailGroupBox.Header = "جزئیات دایری تلفن";
            }
            else if (request.CenterID == _centerToCenterTranslation.SourceCenterID)
            {
                NewPreCodeTemplateColumn.Visibility = Visibility.Collapsed;
                NewTelephonNoTemplateColumn.Visibility = Visibility.Collapsed;
            }
       
        }


        private void NewPreCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewPreCodeComboBox != null && NewPreCodeComboBox.SelectedValue != null)
            {
                _newTelephons = new ObservableCollection<Telephone>( Data.SwitchPrecodeDB.GetTelephonesByPreCodes(new List<int> { (int)NewPreCodeComboBox.SelectedValue }, telphonNumbers.Count()));
                if (NewPreCodeComboBox.SelectedItem != null)
                {
                    CenterToCenterTranslationChooseNumberInfo CenterToCenterTranslationChooseNumberInfo = TelItemsDataGrid.SelectedItem as CenterToCenterTranslationChooseNumberInfo;
                    if (CenterToCenterTranslationChooseNumberInfo != null)
                    {
                        CenterToCenterTranslationChooseNumberInfo.NewPreCodeNumber = Convert.ToInt64((NewPreCodeComboBox.SelectedItem as CheckableItem).Name);
                        CenterToCenterTranslationChooseNumberInfo.NewTelephonNo = null;
                           
                    }
                }
            }
        }


        private void NewTelephonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewTelephonComboBox != null && NewTelephonComboBox.SelectedValue != null && TelItemsDataGrid.SelectedItem != null)
            {
                CenterToCenterTranslationChooseNumberInfo CenterToCenterTranslationChooseNumberInfo = TelItemsDataGrid.SelectedItem as CenterToCenterTranslationChooseNumberInfo;
                if(CenterToCenterTranslationChooseNumberInfo != null)
                  CenterToCenterTranslationChooseNumberInfo.NewTelephonNo = (long)NewTelephonComboBox.SelectedValue;
            }
        }

        public override bool Save()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {


                    request.StatusID = (int)StatusComboBox.SelectedValue;
                    request.Detach();
                    DB.Save(request , false);

                    ts.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage("ذخیره انجام شد");
                }
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات" , ex);
            }

            return IsSaveSuccess;

        }

        public override bool Forward()
        {
            using (TransactionScope ts1 = new TransactionScope(TransactionScopeOption.RequiresNew))
            {

                Save();
                this.RequestID = requestID;
                if (IsSaveSuccess)
                    IsForwardSuccess = true;
                ts1.Complete();
            }

            return IsForwardSuccess;

        }
        #region Load Control
        ComboBox NewPreCodeComboBox;
        private void NewPreCodeComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            NewPreCodeComboBox = sender as ComboBox;
        }

        ComboBox NewTelephonComboBox;
        private void NewTelephonComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            NewTelephonComboBox = sender as ComboBox;
        }
        #endregion

        private void FilterTelephonNoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        public void ApplyFilters()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(TelItemsDataGrid.ItemsSource);
            if (view != null)
            {
                view.Filter = new Predicate<object>(PredicateFilters);
            }
        }

        private bool PredicateFilters(object obj)
        {
           CenterToCenterTranslationChooseNumberInfo checkableObject = obj as CenterToCenterTranslationChooseNumberInfo;
            return checkableObject.TelephonNo.ToString().Contains(FilterTelephonNoTextBox.Text.Trim());
        }

        private void TelItemsDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }
    }
}
