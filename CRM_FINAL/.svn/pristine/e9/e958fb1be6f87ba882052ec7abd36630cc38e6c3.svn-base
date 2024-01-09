using CRM.Application.Reports.Viewer;
using CRM.Application.UserControls;
using CRM.Data;
using Stimulsoft.Report;
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
    public partial class CenterToCenterTranslationMDFForm : Local.RequestFormBase
    {
        private long requestID = 0;
        private Request request { get; set; }
        CRM.Application.UserControls.CenterToCenterTranslationInfo _centerToCenterTranslationInfo;
        Data.CenterToCenterTranslation _centerToCenterTranslation { get; set; }
        ObservableCollection<CenterToCenterTranslationChooseNumberInfo> telphonNumbers;

        public ObservableCollection<CheckableItem> _preCodes { get; set; }
        public ObservableCollection<Telephone> _newTelephons { get; set; }



        List<CabinetInput> _newCabinetInputs { get; set; }
        List<CabinetInput> _oldCabinetInputs { get; set; }

        List<Bucht> _oldBuchtList { get; set; }
        List<ConnectionInfo> _newBuchtInfoList { get; set; }
        List<ConnectionInfo> _oldBuchtInfoList { get; set; }


        public CenterToCenterTranslationMDFForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
        }
        public CenterToCenterTranslationMDFForm(long ID)
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


            if (request.CenterID == _centerToCenterTranslation.TargetCenterID)
            {
                VerticalCloumnNoTextcolumn.Visibility = Visibility.Collapsed;
                VerticalRowNoTextcolumn.Visibility = Visibility.Collapsed;
                BuchtNoTextcolumn.Visibility = Visibility.Collapsed;
                TelephonNoTextColumn.Visibility = Visibility.Collapsed;
                DetailGroupBox.Header = "جزئیات دایری بوخت";
                PCMGroupBox.Header = "جزئیات دایری پی سی ام";
                if (Data.CenterToCenterTranslationDB.CheckExistCenterToCenterTranslationPCM(request.ID))
                {
                    PCMGroupBox.Visibility = Visibility.Visible;
                    OldPCMComboBoxColumn.Visibility = Visibility.Collapsed;
                    PCMDataGrid.ItemsSource = Data.CenterToCenterTranslationDB.GetCenterToCenterPCMs(request.ID);

                }
            }
            else if (request.CenterID == _centerToCenterTranslation.SourceCenterID)
            {
               NewCabinetInputNumberTextcolumn.Visibility = Visibility.Collapsed;
               NewVerticalCloumnNoTextcolumn.Visibility = Visibility.Collapsed;
               NewVerticalRowNoTextcolumn.Visibility = Visibility.Collapsed;
               NewBuchtNoTextcolumn.Visibility = Visibility.Collapsed;
               NewTelephonNoTextColumn.Visibility = Visibility.Collapsed;

               if (Data.CenterToCenterTranslationDB.CheckExistCenterToCenterTranslationPCM(request.ID))
               {
                   PCMGroupBox.Visibility = Visibility.Visible;
                   NewPCMTextColumn.Visibility = Visibility.Collapsed;
                   PCMDataGrid.ItemsSource = Data.CenterToCenterTranslationDB.GetCenterToCenterPCMs(request.ID);


               }
            }

            _oldCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_centerToCenterTranslation.FromOldCabinetInputID, _centerToCenterTranslation.ToOldCabinetInputID);
           // _oldBuchtList = Data.BuchtDB.GetBuchtByCabinetInputIDsOrderByInputNumber(_oldCabinetInputs.Select(t => t.ID).ToList());
            _oldBuchtInfoList = Data.BuchtDB.GetBuchtInfoByCabinetInputIDsOrderByInputNumber(_oldCabinetInputs.Select(t => t.ID).ToList());

            _newCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_centerToCenterTranslation.FromNewCabinetInputID, _centerToCenterTranslation.ToNewCabinetInputID);
            _newBuchtInfoList = Data.BuchtDB.GetBuchtInfoByCabinetInputIDsOrderByInputNumber(_newCabinetInputs.Select(t => t.ID).ToList());


            for (int i = 0; i < _oldCabinetInputs.Count(); i++)
            {

                if (telphonNumbers.Any(t => t.CabinetInputID == _oldCabinetInputs[i].ID))
                    {
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewCabinetInputID = _newCabinetInputs[i].ID;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewCabinetInputNumber = _newCabinetInputs[i].InputNumber;

                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewVerticalCloumnNo = _newBuchtInfoList[i].VerticalColumnNo;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewVerticalRowNo = _newBuchtInfoList[i].VerticalRowNo;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewBuchtNo = _newBuchtInfoList[i].BuchtNo;
                    }

            }

            TelItemsDataGrid.DataContext = telphonNumbers;
       
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
                // view.Filter = new System.Predicate<Object>(o => ((YourClass)o).Property1.Contains(SearchText1));
                view.Filter = new Predicate<object>(PredicateFilters);
            }
        }

        private bool PredicateFilters(object obj)
        {
           CenterToCenterTranslationChooseNumberInfo checkableObject = obj as CenterToCenterTranslationChooseNumberInfo;
            //Return members whose Orders have not been filled 
            return checkableObject.TelephonNo.ToString().Contains(FilterTelephonNoTextBox.Text.Trim());
        }

        private void TelItemsDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {

        }

        private void Print_MDF(object sender, RoutedEventArgs e)
        {
          
            ObservableCollection<CenterToCenterTranslationChooseNumberInfo> Result = new ObservableCollection<CenterToCenterTranslationChooseNumberInfo>();
           _centerToCenterTranslation = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslation(requestID);
            request = Data.RequestDB.GetRequestByID(requestID);
            Status StatusPlace = StatusDB.GetStatusByID(request.StatusID);
            RequestStep requestStep = RequestStepDB.GetRequestStepByID(StatusPlace.RequestStepID);

            if (requestStep.StepTitle.Contains("ام دی اف - دایری"))
            {
                telphonNumbers = new ObservableCollection<CenterToCenterTranslationChooseNumberInfo>(Data.CenterToCenterTranslationDB.GetTelephones(_centerToCenterTranslation));
                _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_centerToCenterTranslation.TargetCenterID));

                List<CenterToCenterTranslationTelephone> centerToCenterTranslationTelephones = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslationTelephon(requestID).ToList();
                centerToCenterTranslationTelephones.ForEach(item =>
                {
                    if (telphonNumbers.Any(t => t.TelephonNo == item.TelephoneNo))
                    {
                        telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewTelephonNo = item.NewTelephoneNo;
                        telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeID = item.NewSwitchPrecodeID;
                        telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().NewPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == item.NewSwitchPrecodeID).SingleOrDefault().Name));
                    }
                }
                                                               );


                _newCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_centerToCenterTranslation.FromNewCabinetInputID, _centerToCenterTranslation.ToNewCabinetInputID);
                _newBuchtInfoList = Data.BuchtDB.GetBuchtInfoByCabinetInputIDsOrderByInputNumber(_newCabinetInputs.Select(t => t.ID).ToList());


                for (int i = 0; i < _oldCabinetInputs.Count(); i++)
                {

                    if (telphonNumbers.Any(t => t.CabinetInputID == _oldCabinetInputs[i].ID))
                    {
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewCabinetInputID = _newCabinetInputs[i].ID;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewCabinetInputNumber = _newCabinetInputs[i].InputNumber;

                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewVerticalCloumnNo = _newBuchtInfoList[i].VerticalColumnNo;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewVerticalRowNo = _newBuchtInfoList[i].VerticalRowNo;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewBuchtNo = _newBuchtInfoList[i].BuchtNo;
                    }

                }

                Result = telphonNumbers;
                SendToPrint(Result);
            }

            if (requestStep.StepTitle.Contains("ام دی اف - تخلیه"))
            {
                telphonNumbers = new ObservableCollection<CenterToCenterTranslationChooseNumberInfo>(Data.CenterToCenterTranslationDB.GetTelephones(_centerToCenterTranslation));
                _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_centerToCenterTranslation.TargetCenterID));

                List<CenterToCenterTranslationTelephone> centerToCenterTranslationTelephones = Data.CenterToCenterTranslationDB.GetCenterToCenterTranslationTelephon(requestID).ToList();
                centerToCenterTranslationTelephones.ForEach(item =>
                {
                    if (telphonNumbers.Any(t => t.TelephonNo == item.TelephoneNo))
                    {
                        telphonNumbers.Where(t => t.TelephonNo == item.TelephoneNo).SingleOrDefault().TelephonNo = item.TelephoneNo;
                        
                    }
                }
                                                               );


                //_newCabinetInputs = Data.CabinetInputDB.GetCabinetInputFromIDToID(_centerToCenterTranslation.FromNewCabinetInputID, _centerToCenterTranslation.ToNewCabinetInputID);
                //_newBuchtInfoList = Data.BuchtDB.GetBuchtInfoByCabinetInputIDsOrderByInputNumber(_newCabinetInputs.Select(t => t.ID).ToList());


                for (int i = 0; i < _oldCabinetInputs.Count(); i++)
                {

                    if (telphonNumbers.Any(t => t.CabinetInputID == _oldCabinetInputs[i].ID))
                    {
                        //telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewCabinetInputID = _newCabinetInputs[i].ID;
                        //telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().NewCabinetInputNumber = _newCabinetInputs[i].InputNumber;

                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().VerticalCloumnNo =_oldBuchtInfoList[i].VerticalColumnNo;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().VerticalRowNo = _oldBuchtInfoList[i].VerticalRowNo;
                        telphonNumbers.Where(t => t.CabinetInputID == _oldCabinetInputs[i].ID).SingleOrDefault().BuchtNo = _oldBuchtInfoList[i].BuchtNo;
                    }

                }

                Result = telphonNumbers;
                SendToPrintDischarge(Result);
            }
        }

        private void SendToPrint(ObservableCollection<CenterToCenterTranslationChooseNumberInfo> Result)
        {

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.CenterToCenterTranslationMDFDayeri);
            stiReport.Load(path);
            stiReport.CacheAllData = true; stiReport.RegData("Result", "Result", Result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        private void SendToPrintDischarge(ObservableCollection<CenterToCenterTranslationChooseNumberInfo> Result)
        {

            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.CenterToCenterTranslationMDFDischarge);
            stiReport.Load(path);
            stiReport.CacheAllData = true; stiReport.RegData("Result", "Result", Result);

         

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }
    }
}

