using CRM.Application.UserControls;
using CRM.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class CenterToCenterTranslationChooseNumberForm : Local.RequestFormBase
    {
        private long requestID = 0;
        private Request request { get; set; }
        CRM.Application.UserControls.CenterToCenterTranslationInfo _centerToCenterTranslationInfo;
        Data.CenterToCenterTranslation _centerToCenterTranslation { get; set; }
        ObservableCollection<CenterToCenterTranslationChooseNumberInfo> telphonNumbers;

        public ObservableCollection<CheckableItem> _preCodes { get; set; }
        public ObservableCollection<Telephone> _newTelephons { get; set; }

        public CenterToCenterTranslationChooseNumberForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit };
        }
        public CenterToCenterTranslationChooseNumberForm(long ID)
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


            _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_centerToCenterTranslation.TargetCenterID));
            PreCodeComboBox.ItemsSource = new List<CheckableItem>(_preCodes);

            telphonNumbers = new ObservableCollection<CenterToCenterTranslationChooseNumberInfo>(Data.CenterToCenterTranslationDB.GetTelephones(_centerToCenterTranslation));

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


            TelItemsDataGrid.DataContext = telphonNumbers;

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            List<int> preCodes = PreCodeComboBox.SelectedIDs;
            List<Telephone> telephones = Data.SwitchPrecodeDB.GetTelephonesByPreCodes(preCodes, -1);
            telphonNumbers.ToList().ForEach(t => t.NewTelephonNo = null);
            // لیست تلفن های جدید را به دنبال تلفنی که 4 رقم آخر ان مشابه شماره قبلی باشد جستجو می کند
            telphonNumbers.ToList().ForEach(item =>
            {
                string stringItem = item.TelephonNo.ToString();              
                Telephone telephon = telephones.Find(t => t.TelephoneNo.ToString().Substring(t.TelephoneNo.ToString().Length - 4) == stringItem.Substring(stringItem.Length - 4));
                // اگر تلفن یافت شود از لیست حذف می شود تا در جستجو بعدی استفاده نشود
                if (telephon != null)
                {
                    item.NewTelephonNo = telephon.TelephoneNo;
                    item.NewPreCodeID = (int)telephon.SwitchPrecodeID;
                    item.NewPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == telephon.SwitchPrecodeID).SingleOrDefault().Name));
                    telephones.Remove(telephon);
                }
            });

            // برای تلفن هایی که 4 رقم مشابهت در آنها یافت نشد 3 رقم مشابهت را جستجو می کند
            telphonNumbers.Where(t=>t.NewTelephonNo == null).ToList().ForEach(item =>
            {
                   string stringItem = item.TelephonNo.ToString();
                   Telephone telephon = telephones.Find(t => t.TelephoneNo.ToString().Substring(t.TelephoneNo.ToString().Length - 3) == stringItem.Substring(stringItem.Length - 3));
                   if (telephon != null)
                   {
                       item.NewTelephonNo = telephon.TelephoneNo;
                       item.NewPreCodeID = (int)telephon.SwitchPrecodeID;
                       item.NewPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == telephon.SwitchPrecodeID).SingleOrDefault().Name));
                       telephones.Remove(telephon);
                   }

            });

            // برای تلفن هایی که 3 رقم مشابهت در آنها یافت نشد 2 رقم مشابهت را جستجو می کند
            telphonNumbers.Where(t => t.NewTelephonNo == null).ToList().ForEach(item =>
            {
                string stringItem = item.TelephonNo.ToString();

                Telephone telephon = telephones.Find(t => t.TelephoneNo.ToString().Substring(t.TelephoneNo.ToString().Length - 2) == stringItem.Substring(stringItem.Length - 2));
                    if (telephon != null)
                    {
                        item.NewTelephonNo = telephon.TelephoneNo;
                        item.NewPreCodeID = (int)telephon.SwitchPrecodeID;
                        item.NewPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == telephon.SwitchPrecodeID).SingleOrDefault().Name));
                        telephones.Remove(telephon);
                    }
                
            });

            // برای تلفن هایی که 2 رقم مشابهت در آنها یافت نشد 1 رقم مشابهت را جستجو می کند
            telphonNumbers.Where(t => t.NewTelephonNo == null).ToList().ForEach(item =>
            {
                string stringItem = item.TelephonNo.ToString();

                Telephone telephon = telephones.Find(t => t.TelephoneNo.ToString().Substring(t.TelephoneNo.ToString().Length - 1) == stringItem.Substring(stringItem.Length - 1));
                if (telephon != null)
                {
                    item.NewTelephonNo = telephon.TelephoneNo;
                    item.NewPreCodeID = (int)telephon.SwitchPrecodeID;
                    item.NewPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == telephon.SwitchPrecodeID).SingleOrDefault().Name));
                    telephones.Remove(telephon);
                }

            });

            // شماره های باقی مانده به ترتیب اختصاص داده می شود
            telephones = telephones.OrderBy(t => t.TelephoneNo).ToList();
            telphonNumbers.Where(t => t.NewTelephonNo == null).OrderBy(t=>t.TelephonNo).ToList().ForEach(item =>
            {
                Telephone telephon = telephones.Take(1).SingleOrDefault();
                if (telephon != null)
                {
                    item.NewTelephonNo = telephon.TelephoneNo;
                    item.NewPreCodeID = (int)telephon.SwitchPrecodeID;
                    item.NewPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == telephon.SwitchPrecodeID).SingleOrDefault().Name));
                    telephones.Remove(telephon);
                }

            });


        }

        private void NewPreCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewPreCodeComboBox != null && NewPreCodeComboBox.SelectedValue != null)
            {
                _newTelephons = new ObservableCollection<Telephone>(Data.SwitchPrecodeDB.GetTelephonesByPreCodes(new List<int> { (int)NewPreCodeComboBox.SelectedValue }, telphonNumbers.Count()));
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
                if (CenterToCenterTranslationChooseNumberInfo != null)
                    CenterToCenterTranslationChooseNumberInfo.NewTelephonNo = (long)NewTelephonComboBox.SelectedValue;
            }
        }

        public override bool Save()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    List<CenterToCenterTranslationChooseNumberInfo> centerToCenterTranslationChooseNumberInfos = new List<CenterToCenterTranslationChooseNumberInfo>(TelItemsDataGrid.ItemsSource as ObservableCollection<CenterToCenterTranslationChooseNumberInfo>);
                    if (centerToCenterTranslationChooseNumberInfos.GroupBy(t => t.NewTelephonNo).Any(t => t.Count() > 1))
                        throw new Exception("در شماره های جدید شماره تکراری وجود دارد.");


                    DB.DeleteAll<CenterToCenterTranslationTelephone>(Data.CenterToCenterTranslationDB.GetCenterToCenterTranslationTelephon(requestID).Select(t => t.RequestID).ToList());

                    List<CRM.Data.CenterToCenterTranslationTelephone> centerToCenterTranslationTelephons = new List<CenterToCenterTranslationTelephone>();

                    centerToCenterTranslationChooseNumberInfos.ForEach
                        (item =>
                               {
                                   CenterToCenterTranslationTelephone centerToCenterTranslationTelephon = new CenterToCenterTranslationTelephone();
                                   centerToCenterTranslationTelephon.RequestID = requestID;
                                   centerToCenterTranslationTelephon.TelephoneNo = item.TelephonNo;
                                   centerToCenterTranslationTelephon.NewTelephoneNo = (long)item.NewTelephonNo;
                                   centerToCenterTranslationTelephon.NewSwitchPrecodeID = item.NewPreCodeID;
                                   centerToCenterTranslationTelephons.Add(centerToCenterTranslationTelephon);
                               }
                        );

                    DB.SaveAll(centerToCenterTranslationTelephons);


                    request.StatusID = (int)StatusComboBox.SelectedValue;
                    request.Detach();
                    DB.Save(request, false);

                    ts.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage("ذخیره انجام شد");
                }
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            return IsSaveSuccess;

        }

        public override bool Forward()
        {
            using (TransactionScope ts1 = new TransactionScope(TransactionScopeOption.RequiresNew))
            {
                List<CenterToCenterTranslationChooseNumberInfo> centerToCenterTranslationChooseNumberInfos = new List<CenterToCenterTranslationChooseNumberInfo>(TelItemsDataGrid.ItemsSource as ObservableCollection<CenterToCenterTranslationChooseNumberInfo>);
                if (centerToCenterTranslationChooseNumberInfos.Any(t => t.NewTelephonNo == null))
                    throw new Exception(" لطفا شماره جدید " + centerToCenterTranslationChooseNumberInfos.Where(t => t.NewTelephonNo == null) + "را تعیین کنید ");

                Save();
                this.RequestID = requestID;
                if (IsSaveSuccess)
                {
                    // the phone reserve
                    List<Telephone> telephones = Data.TelephoneDB.GetTelephones(centerToCenterTranslationChooseNumberInfos.Select(t => t.TelephonNo).ToList());
                    telephones.ForEach(item => { item.Status = (byte)DB.TelephoneStatus.Reserv; item.Detach(); });
                    DB.UpdateAll(telephones);

                    List<Telephone> Newtelephones = Data.TelephoneDB.GetTelephones(centerToCenterTranslationChooseNumberInfos.Select(t => (long)t.NewTelephonNo).ToList());
                    Newtelephones.ForEach(item => { item.Status = (byte)DB.TelephoneStatus.Reserv; item.Detach(); });
                    DB.UpdateAll(Newtelephones);

                    //


                    // change request center 
                    Status Status = Data.StatusDB.GetStatueByStatusID(request.StatusID);
                    if (request.CenterID == _centerToCenterTranslation.TargetCenterID && Status.StatusType == (byte)DB.RequestStatusType.ChangeCenter)
                    {
                        request.CenterID = _centerToCenterTranslation.SourceCenterID;
                        request.Detach();
                        DB.Save(request, false);
                    }
                    //

                    IsForwardSuccess = true;
                }
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
            if (TelItemsDataGrid.SelectedItem != null)
            {
                CenterToCenterTranslationChooseNumberInfo CenterToCenterTranslationChooseNumberInfo = TelItemsDataGrid.SelectedItem as CenterToCenterTranslationChooseNumberInfo;
                _newTelephons = new ObservableCollection<Telephone>(Data.SwitchPrecodeDB.GetTelephonesByPreCodes(new List<int> { CenterToCenterTranslationChooseNumberInfo.NewPreCodeID }, -1));
            }
        }
    }
}
