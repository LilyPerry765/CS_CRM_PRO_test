using CRM.Application.Reports.Viewer;
using CRM.Data;
using Stimulsoft.Report;
using System;
using System.Collections;
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
    /// Interaction logic for TranslationOpticalToNormalChooseNumberForm.xaml
    /// </summary>
    public partial class ExchangeGSMChooseNumberFrom : Local.RequestFormBase
    {
        private long _requestID;
        public ObservableCollection<CheckableItem> _preCodes { get; set; }

        ExchangeGSM _exchangeGSM { get; set; }


        ObservableCollection<ExchangeGSMInfo> exchangeGSMInfos;
        ObservableCollection<ExchangeGSMInfo> oldExchangeGSMInfos;
        public ObservableCollection<Telephone> _newTelephons { get; set; }

      //  CRM.Application.UserControls.TranslationOpticalToNormalInfo _translationOpticalToNormalInfo { get; set; }


        Request _reqeust { get; set; }

        public ExchangeGSMChooseNumberFrom()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ChooseNumberTypeComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.ChooseNumberType));
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny, (byte)DB.NewAction.Print};
        }

        public ExchangeGSMChooseNumberFrom(long requestID)
            :this()
        {
            this._requestID = requestID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            //_translationOpticalToNormalInfo = new UserControls.TranslationOpticalToNormalInfo(_requestID);
            //ExchangeInfoUserControl.Content = _translationOpticalToNormalInfo;
            //ExchangeInfoUserControl.DataContext = _translationOpticalToNormalInfo;
            //ExchangeInfoUserControl.IsEnabled = false;

            _exchangeGSM = Data.ExchangeGSMDB.GetExchangeGSMByRequestID(_requestID);
            if (_exchangeGSM.ChoiceNumberAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _exchangeGSM.ChoiceNumberAccomplishmentDate = dateTime.Date;
                _exchangeGSM.ChoiceNumberAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }
            _reqeust = Data.RequestDB.GetRequestByID(_requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(_reqeust.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = _reqeust.StatusID;


            Cabinet cabinet = CabinetDB.GetCabinetByID(_exchangeGSM.OldCabinetID);

           // Cabinet NewCabinet = CabinetDB.GetCabinetByID(_exchangeGSM.NewCabinetID);

            //if (NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet || NewCabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL)
            //{
            //    _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_reqeust.CenterID, NewCabinet.SwitchID));
            //}
            //else
            //{
            //    _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_reqeust.CenterID, null));
            //}


            if (_exchangeGSM.Type == (int)DB.ExchangeGSMType.NormalToGSM)
            {
                _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetGSMSwitchPrecodeCheckableItemByCenterID(_reqeust.CenterID));
            }
            else if (_exchangeGSM.Type == (int)DB.ExchangeGSMType.GSMToNormal)
            {
                if (cabinet.CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet || cabinet.CabinetUsageType == (int)DB.CabinetUsageType.WLL)
                {
                    _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_reqeust.CenterID, cabinet.SwitchID));
                }
                else
                {
                    _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemByCenterID(_reqeust.CenterID, null));
                }
            }

            
            PreCodeComboBox.ItemsSource = new List<CheckableItem>(_preCodes);

            exchangeGSMInfos = new ObservableCollection<ExchangeGSMInfo>(Data.ExchangeGSMDB.GetExchangeGSMList(_exchangeGSM).Where(t => t.FromTelephonNo != null).OrderBy(t => t.FromTelephonNo).ToList());
            TelItemsDataGrid.DataContext = exchangeGSMInfos;
            AccomplishmentGroupBox.DataContext = _exchangeGSM;

            this.ResizeWindow();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (ChooseNumberTypeComboBox.SelectedValue == null)
            {
                Folder.MessageBox.ShowError("نوع تعیین شماره را انتخاب کنید");
            }
            else
            {
                if ((int)ChooseNumberTypeComboBox.SelectedValue == (int)DB.ChooseNumberType.Sort)
                {
                    List<int> preCodes = PreCodeComboBox.SelectedIDs;
                    List<Telephone> telephones = Data.SwitchPrecodeDB.GetTelephonesByPreCodes(preCodes, -1);
                    exchangeGSMInfos.ToList().ForEach(item =>
                    {
                        string stringItem = item.FromTelephonNo.ToString();
                        Telephone telephon = telephones.Take(1).SingleOrDefault();
                        if (telephon != null)
                        {
                            item.ToTelephonNo = telephon.TelephoneNo;
                            item.ToTelephoneStatus = telephon.Status;
                            item.ToSwitchPreCodeID = (int)telephon.SwitchPrecodeID;
                             item.ToSwitchPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == telephon.SwitchPrecodeID).SingleOrDefault().Name));
                            telephones.Remove(telephon);
                        }
                    });
                }
                else if ((int)ChooseNumberTypeComboBox.SelectedValue == (int)DB.ChooseNumberType.Similar)
                {
                    List<int> preCodes = PreCodeComboBox.SelectedIDs;
                    List<Telephone> telephones = Data.SwitchPrecodeDB.GetTelephonesByPreCodes(preCodes, -1);
                    exchangeGSMInfos.ToList().ForEach(t => t.ToTelephonNo = null);
                    // لیست تلفن های جدید را به دنبال تلفنی که 4 رقم آخر ان مشابه شماره قبلی باشد جستجو می کند
                    exchangeGSMInfos.ToList().ForEach(item =>
                    {
                        string stringItem = item.FromTelephonNo.ToString();
                        Telephone telephon = telephones.Find(t => t.TelephoneNo.ToString().Substring(t.TelephoneNo.ToString().Length - 4) == stringItem.Substring(stringItem.Length - 4));
                        // اگر تلفن یافت شود از لیست حذف می شود تا در جستجو بعدی استفاده نشود
                        if (telephon != null)
                        {
                            item.ToTelephonNo = telephon.TelephoneNo;
                            item.ToTelephoneStatus = telephon.Status;
                            item.ToSwitchPreCodeID = (int)telephon.SwitchPrecodeID;
                            item.ToSwitchPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == telephon.SwitchPrecodeID).SingleOrDefault().Name));
                            telephones.Remove(telephon);
                        }
                    });

                    // برای تلفن هایی که 4 رقم مشابهت در آنها یافت نشد 3 رقم مشابهت را جستجو می کند
                    exchangeGSMInfos.Where(t => t.FromTelephonNo == null).ToList().ForEach(item =>
                    {
                        string stringItem = item.FromTelephonNo.ToString();
                        Telephone telephon = telephones.Find(t => t.TelephoneNo.ToString().Substring(t.TelephoneNo.ToString().Length - 3) == stringItem.Substring(stringItem.Length - 3));
                        if (telephon != null)
                        {
                            item.ToTelephonNo = telephon.TelephoneNo;
                            item.ToTelephoneStatus = telephon.Status;
                            item.ToSwitchPreCodeID = (int)telephon.SwitchPrecodeID;
                            item.ToSwitchPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == telephon.SwitchPrecodeID).SingleOrDefault().Name));
                            telephones.Remove(telephon);
                        }

                    });

                    // برای تلفن هایی که 3 رقم مشابهت در آنها یافت نشد 2 رقم مشابهت را جستجو می کند
                    exchangeGSMInfos.Where(t => t.FromTelephonNo == null).ToList().ForEach(item =>
                    {
                        string stringItem = item.FromTelephonNo.ToString();

                        Telephone telephon = telephones.Find(t => t.TelephoneNo.ToString().Substring(t.TelephoneNo.ToString().Length - 2) == stringItem.Substring(stringItem.Length - 2));
                        if (telephon != null)
                        {
                            item.ToTelephonNo = telephon.TelephoneNo;
                            item.ToTelephoneStatus = telephon.Status;
                            item.ToSwitchPreCodeID = (int)telephon.SwitchPrecodeID;
                            item.ToSwitchPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == telephon.SwitchPrecodeID).SingleOrDefault().Name));
                            telephones.Remove(telephon);
                        }

                    });

                    // برای تلفن هایی که 2 رقم مشابهت در آنها یافت نشد 1 رقم مشابهت را جستجو می کند
                    exchangeGSMInfos.Where(t => t.FromTelephonNo == null).ToList().ForEach(item =>
                    {
                        string stringItem = item.FromTelephonNo.ToString();

                        Telephone telephon = telephones.Find(t => t.TelephoneNo.ToString().Substring(t.TelephoneNo.ToString().Length - 1) == stringItem.Substring(stringItem.Length - 1));
                        if (telephon != null)
                        {
                            item.ToTelephonNo = telephon.TelephoneNo;
                            item.ToTelephoneStatus = telephon.Status;
                            item.ToSwitchPreCodeID = (int)telephon.SwitchPrecodeID;
                            item.ToSwitchPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == telephon.SwitchPrecodeID).SingleOrDefault().Name));
                            telephones.Remove(telephon);
                        }

                    });

                    // شماره های باقی مانده به ترتیب اختصاص داده می شود
                    telephones = telephones.OrderBy(t => t.TelephoneNo).ToList();
                    exchangeGSMInfos.Where(t => t.FromTelephonNo == null).OrderBy(t => t.FromTelephonNo).ToList().ForEach(item =>
                    {
                        Telephone telephon = telephones.Take(1).SingleOrDefault();
                        if (telephon != null)
                        {
                            item.ToTelephonNo = telephon.TelephoneNo;
                            item.ToTelephoneStatus = telephon.Status;
                            item.ToSwitchPreCodeID = (int)telephon.SwitchPrecodeID;
                            item.ToSwitchPreCodeNumber = Convert.ToInt64((_preCodes.Where(t => t.ID == telephon.SwitchPrecodeID).SingleOrDefault().Name));
                            telephones.Remove(telephon);
                        }

                    });
                }
            }

        }

        private void NewPreCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewPreCodeComboBox != null && NewPreCodeComboBox.SelectedValue != null)
            {
                _newTelephons = new ObservableCollection<Telephone>(Data.SwitchPrecodeDB.GetTelephonesByPreCodes(new List<int> { (int)NewPreCodeComboBox.SelectedValue }, -1));
                if (NewPreCodeComboBox.SelectedItem != null)
                {
                    exchangeGSMInfos[TelItemsDataGrid.SelectedIndex].ToSwitchPreCodeNumber = Convert.ToInt64((NewPreCodeComboBox.SelectedItem as CheckableItem).Name);
                    exchangeGSMInfos[TelItemsDataGrid.SelectedIndex].ToTelephonNo = null;

                    //TranslationOpticalCabinetToNormalInfo translationOpticalCabinetToNormalInfo = TelItemsDataGrid.SelectedItem as TranslationOpticalCabinetToNormalInfo;
                    //if (translationOpticalCabinetToNormalInfo != null)
                    //{
                    //    translationOpticalCabinetToNormalInfo.NewPreCodeNumber = Convert.ToInt64((NewPreCodeComboBox.SelectedItem as CheckableItem).Name);
                    //    translationOpticalCabinetToNormalInfo.NewTelephonNo = null;
                    //}
                }
            }
        }


        private void NewTelephonComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewTelephonComboBox != null && NewTelephonComboBox.SelectedValue != null && TelItemsDataGrid.SelectedItem != null)
            {
                exchangeGSMInfos[TelItemsDataGrid.SelectedIndex].ToTelephonNo = (long)NewTelephonComboBox.SelectedValue;
                exchangeGSMInfos[TelItemsDataGrid.SelectedIndex].ToTelephoneStatus = TelephoneDB.GetTelephoneByTelephoneNo((long)NewTelephonComboBox.SelectedValue).Status;
                //TranslationOpticalCabinetToNormalInfo translationOpticalCabinetToNormalInfo = TelItemsDataGrid.SelectedItem as TranslationOpticalCabinetToNormalInfo;
                //if (translationOpticalCabinetToNormalInfo != null)
                //    translationOpticalCabinetToNormalInfo.NewTelephonNo = (long)NewTelephonComboBox.SelectedValue;
            }
        }

        public override bool Save()
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    string error = string.Empty;
                    List<CRM.Data.ExchangeGSMConnection> exchangeGSMConnection = Data.ExchangeGSMConnectionDB.GetExchangeGSMConnectionByRequestID(_requestID);

                    List<ExchangeGSMInfo> exchangeGSMInfo = new List<ExchangeGSMInfo>(TelItemsDataGrid.ItemsSource as ObservableCollection<ExchangeGSMInfo>);
                    if (exchangeGSMInfo.Where(t => t.ToTelephonNo != null).GroupBy(t => t.ToTelephonNo).Any(t => t.Count() > 1))
                        error += string.Format("شماره های {0} تکراری می باشند", exchangeGSMInfo.Where(t => t.ToTelephonNo != null).GroupBy(t => t.ToTelephonNo).Where(t => t.Count() > 1).Select(t => t.Key.Value).Aggregate((i, j) => i + ',' + j));


                    List<Telephone> Newtelephones = Data.TelephoneDB.GetTelephones(exchangeGSMInfo.Where(t => t.ToTelephonNo != null).Select(t => (long)t.ToTelephonNo).ToList());
                    if (Newtelephones.Any(t => !exchangeGSMConnection.Where(t2 => t2.ToTelephoneNo != null).Select(t2 => (long)t2.ToTelephoneNo).Contains(t.TelephoneNo) && t.Status == (int)DB.TelephoneStatus.Reserv))
                        error += string.Format("شماره های {0} رزرو می باشند", Newtelephones.Where(t => t.Status == (int)DB.TelephoneStatus.Reserv).Select(t => t.TelephoneNo.ToString()).Aggregate((i, j) => i + ',' + j));


                    if (string.IsNullOrEmpty(error))
                    {



                        List<Telephone> newtelephones = Data.TelephoneDB.GetTelephones(exchangeGSMConnection.Where(t => t.ToTelephoneNo != null).Select(t => (long)t.ToTelephoneNo).ToList());
                        newtelephones.ForEach(item => { item.Status = (byte)exchangeGSMConnection.Find(t2=>t2.ToTelephoneNo == item.TelephoneNo).ToTelephoneStatus; item.Detach(); });
                        DB.UpdateAll(newtelephones);

                        exchangeGSMInfo.Where(t => t.FromTelephonNo != null).ToList().ForEach
                            (item =>
                            {
                                exchangeGSMConnection.Where(t => t.FromTelephoneNo == (long)item.FromTelephonNo).SingleOrDefault().ToTelephoneNo = (long)item.ToTelephonNo;
                                exchangeGSMConnection.Where(t => t.FromTelephoneNo == (long)item.FromTelephonNo).SingleOrDefault().ToSwitchPreCodeID = (int)item.ToSwitchPreCodeID;
                                exchangeGSMConnection.Where(t => t.FromTelephoneNo == (long)item.FromTelephonNo).SingleOrDefault().ToTelephoneStatus = (byte)item.ToTelephoneStatus;
                               
                            }
                            );

                        Newtelephones.ForEach(item => { item.Status = (byte)DB.TelephoneStatus.Reserv; item.Detach(); });
                        DB.UpdateAll(Newtelephones);

                        exchangeGSMConnection.ToList().ForEach(t => t.Detach());
                        DB.UpdateAll(exchangeGSMConnection);


                        _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                        _reqeust.Detach();
                        DB.Save(_reqeust, false);

                        _exchangeGSM = AccomplishmentGroupBox.DataContext as ExchangeGSM;
                        _exchangeGSM.Detach();
                        DB.Save(_exchangeGSM, false);




                        ts.Complete();
                        IsSaveSuccess = true;
                        ShowSuccessMessage("ذخیره انجام شد");
                    }
                    else
                    {
                        IsSaveSuccess = false;
                        throw new Exception(error);
                    }
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
            using (TransactionScope ts1 = new TransactionScope(TransactionScopeOption.RequiresNew , TimeSpan.FromMinutes(5)))
            {
                try
                {
                    string error = string.Empty;
                    List<ExchangeGSMInfo> exchangeGSMInfo = new List<ExchangeGSMInfo>(TelItemsDataGrid.ItemsSource as ObservableCollection<ExchangeGSMInfo>);
                    if (exchangeGSMInfo.Any(t => t.FromTelephonNo == null))
                        error += string.Format(" لطفا شماره  " + exchangeGSMInfo.Where(t => t.FromTelephonNo == null).Select(t => t.FromTelephonNo.ToString()).Aggregate((i, j) => i + ',' + j) + "را تعیین کنید ");

                    if (string.IsNullOrEmpty(error))
                    {
                        Save();
                        this.RequestID = _requestID;
                        if (IsSaveSuccess)
                        {


                            //
                            IsForwardSuccess = true;
                        }
                        ts1.Complete();
                    }
                    else
                    {
                        //
                        IsForwardSuccess = false;
                        throw new Exception(error);
                    }
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("خطا در ذخیره اطلاعات" , ex);
                }


            }

            return IsForwardSuccess;

        }

        #region Filters
        private bool PredicateFilters(object obj)
        {
            TranslationOpticalCabinetToNormalInfo checkableObject = obj as TranslationOpticalCabinetToNormalInfo;
            //Return members whose Orders have not been filled 
            return checkableObject.OldTelephonNo.ToString().Contains(FilterTelephonNoTextBox.Text.Trim());
        }

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

       #endregion Filters


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

        private void TelItemsDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            if (TelItemsDataGrid.SelectedItem != null)
            {
                ExchangeGSMInfo exchangeGSMInfo = TelItemsDataGrid.SelectedItem as ExchangeGSMInfo;
                _newTelephons = new ObservableCollection<Telephone>(Data.SwitchPrecodeDB.GetTelephonesByPreCodes(new List<int> { (int)(exchangeGSMInfo.ToSwitchPreCodeID ?? 0) }, -1));
            }
        }

        public override bool Print()
        {
            try 
	{
        SendToPrint(exchangeGSMInfos);
                IsPrintSuccess = true;
	}
	catch (Exception)
	{
		
		IsPrintSuccess = false;
	}
            return IsPrintSuccess;
        }


        private void SendToPrint(IEnumerable result)
        {
            StiReport stiReport = new StiReport();
            stiReport.Dictionary.DataStore.Clear();
            stiReport.Dictionary.Databases.Clear();
            stiReport.Dictionary.RemoveUnusedData();

            string path = ReportDB.GetReportPath((int)DB.UserControlNames.TranslationOpticalToNormalChooseNumberReport);
            stiReport.Load(path);
            stiReport.Dictionary.Variables["city"].Value = Data.CityDB.GetCityByCenterID(_reqeust.CenterID).Name;
            stiReport.Dictionary.Variables["center"].Value = Data.CenterDB.GetCenterById(_reqeust.CenterID).CenterName;
            stiReport.Dictionary.Variables["date"].Value = Helper.GetPersianDate(DB.GetServerDate(), Helper.DateStringType.DateTime).ToString();

            stiReport.CacheAllData = true;
            stiReport.RegData("result", "result", result);

            ReportViewerForm reportViewerForm = new ReportViewerForm(stiReport);
            reportViewerForm.ShowDialog();
        }

        public override bool Deny()
        {
            try
            {

                List<ExchangeGSMConnection> exchangeGSMConnection = Data.ExchangeGSMConnectionDB.GetExchangeGSMConnectionByRequestID(_requestID);


                using (TransactionScope ts = new TransactionScope())
                {
                    base.RequestID = _requestID;

                    List<Telephone> tel = Data.TelephoneDB.GetTelephones(exchangeGSMConnection.Where(t => t.ToTelephoneNo != null).Select(t => (long)t.ToTelephoneNo).ToList());
                    tel.ForEach(item => { item.Status = (byte)exchangeGSMConnection.Find(t2 => t2.ToTelephoneNo == item.TelephoneNo).ToTelephoneStatus; item.Detach(); });
                    DB.UpdateAll(tel);

                    exchangeGSMConnection.ForEach(t => { t.ToTelephoneNo = null; t.Detach(); });
                    DB.UpdateAll(exchangeGSMConnection);

                    _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                    _reqeust.Detach();
                    DB.Save(_reqeust, false);

                    _exchangeGSM.ChoiceNumberAccomplishmentDate = null;
                    _exchangeGSM.ChoiceNumberAccomplishmentTime = null;
                    _exchangeGSM.Detach();
                    DB.Save(_exchangeGSM, false);


                    ts.Complete();
                }

                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطادر رد درخواست", ex);
            }

            base.Deny();

            return IsRejectSuccess;
         }

        private void RequestFormBase_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
                Save();
        }
        }
    }

