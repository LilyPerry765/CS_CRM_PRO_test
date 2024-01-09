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
    /// Interaction logic for TranslationOpticalToNormalSwitchForm.xaml
    /// </summary>
    public partial class TranslationOpticalToNormalSwitchForm : Local.RequestFormBase
    {
        private long _requestID;
        public ObservableCollection<CheckableItem> _preCodes { get; set; }
        TranslationOpticalCabinetToNormal _translationOpticalCabinetToNormal { get; set; }
        Request _reqeust { get; set; }

        ObservableCollection<TranslationOpticalCabinetToNormalInfo> cabinetInputsList;
        public ObservableCollection<Telephone> _newTelephons { get; set; }

        List<Counter> counter;
        public TranslationOpticalToNormalSwitchForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit , (byte)DB.NewAction.Deny };
        }

        public TranslationOpticalToNormalSwitchForm(long requestID)
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
            _translationOpticalCabinetToNormal = Data.TranslationOpticalCabinetToNormalDB.GetTranslationOpticalCabinetToNormal(_requestID);
            if (_translationOpticalCabinetToNormal.SwitchAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _translationOpticalCabinetToNormal.SwitchAccomplishmentDate = dateTime.Date;
                _translationOpticalCabinetToNormal.SwitchAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }
            _reqeust = Data.RequestDB.GetRequestByID(_requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(_reqeust.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = _reqeust.StatusID;

            cabinetInputsList = new ObservableCollection<TranslationOpticalCabinetToNormalInfo>(Data.TranslationOpticalCabinetToNormalDB.GetEquivalentCabinetInputs(_translationOpticalCabinetToNormal).Where(t => t.OldTelephonNo != null).ToList());
            TelItemsDataGrid.ItemsSource = cabinetInputsList;

            AccomplishmentGroupBox.DataContext = _translationOpticalCabinetToNormal;
        }

        public override bool Save()
        {
            try
            {
                DateTime currentDate = DB.GetServerDate();
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    // get telephone list of DataGrid
                    List<TranslationOpticalCabinetToNormalInfo>  translationOpticalCabinetToNormalInfos = new List<TranslationOpticalCabinetToNormalInfo>(TelItemsDataGrid.ItemsSource as ObservableCollection<TranslationOpticalCabinetToNormalInfo>);

                    List<CRM.Data.TranslationOpticalCabinetToNormalConncetion> translationOpticalCabinetToNormalConncetion = Data.TranslationOpticalCabinetToNormalConncetionDB.GetTranslationOpticalCabinetToNormalConncetionByRequestID(_requestID);

                    // update Counter
                    translationOpticalCabinetToNormalConncetion.ForEach(item =>
                    {
                        if (translationOpticalCabinetToNormalInfos.Any(t => t.OldTelephonNo == item.FromTelephoneNo))
                        {
                            item.FromCounter = translationOpticalCabinetToNormalInfos.Where(t => t.OldTelephonNo == item.FromTelephoneNo).SingleOrDefault().OldCounter;
                            item.ToCounter = translationOpticalCabinetToNormalInfos.Where(t => t.OldTelephonNo == item.FromTelephoneNo).SingleOrDefault().NewCounter;

                            if (counter != null)
                            {
                                Counter oldConter = new Counter();
                                oldConter.ID = 0;
                                oldConter.TelephoneNo = (long)item.FromTelephoneNo;
                                oldConter.InsertDate = currentDate;
                                oldConter.CounterReadDate = currentDate;
                                oldConter.CounterNo = item.FromCounter.ToString();
                                counter.Add(oldConter);




                                Counter newConter = new Counter();
                                newConter.ID = 0;
                                newConter.TelephoneNo = (long)item.ToTelephoneNo;
                                newConter.InsertDate = currentDate;
                                newConter.CounterReadDate = currentDate;
                                newConter.CounterNo = item.ToCounter.ToString();
                                counter.Add(newConter);


                                item.FromCounterID = oldConter.ID;
                                item.ToCounterID = newConter.ID;

       
                            }


                        }
                        item.Detach();
                    });


                    translationOpticalCabinetToNormalConncetion.ToList().ForEach(t => t.Detach());
                    DB.UpdateAll(translationOpticalCabinetToNormalConncetion);

                    _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                    _reqeust.Detach();
                    DB.Save(_reqeust, false);

                    _translationOpticalCabinetToNormal = AccomplishmentGroupBox.DataContext as TranslationOpticalCabinetToNormal;
                    _translationOpticalCabinetToNormal.Detach();
                    DB.Save(_translationOpticalCabinetToNormal, false);

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

        public override bool Deny()
        {
            try
            {

                base.RequestID = _requestID;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                   List<CRM.Data.TranslationOpticalCabinetToNormalConncetion> translationOpticalCabinetToNormalConncetion = Data.TranslationOpticalCabinetToNormalConncetionDB.GetTranslationOpticalCabinetToNormalConncetionByRequestID(_requestID);

                    // update Counter
                    translationOpticalCabinetToNormalConncetion.ForEach(item =>
                    {
                        if (item.FromCounterID != null && item.FromCounterID != 0)
                        {

                            DB.Delete<Counter>(item.FromCounterID);

                        }


                        if (item.ToCounterID != null && item.ToCounterID != 0)
                        {

                            DB.Delete<Counter>(item.ToCounterID);

                        }

                        item.FromCounter = null;
                        item.ToCounter = null;

                        item.Detach();
                    });


                    translationOpticalCabinetToNormalConncetion.ToList().ForEach(t => t.Detach());
                    DB.UpdateAll(translationOpticalCabinetToNormalConncetion);

                    _reqeust.StatusID = (int)StatusComboBox.SelectedValue;
                    _reqeust.Detach();
                    DB.Save(_reqeust, false);

                    _translationOpticalCabinetToNormal = AccomplishmentGroupBox.DataContext as TranslationOpticalCabinetToNormal;
                    _translationOpticalCabinetToNormal.Detach();
                    DB.Save(_translationOpticalCabinetToNormal, false);

                    ts.Complete();
                    IsRejectSuccess = true;
                    ShowSuccessMessage("ذخیره انجام شد");
                }
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }

            return IsRejectSuccess;
        }

        public override bool Forward()
        {
    

            using (TransactionScope ts1 = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(5)))
            {
                try
                {

                    counter = new List<Counter>();
                    Save();
                    this.RequestID = _requestID;
                    if (IsSaveSuccess)
                    {
                        //if (counter.Any(t => string.IsNullOrEmpty(t.CounterNo)))
                        //    throw new Exception("شماره کنتور نمی تواند خالی باشد");
                        counter = counter.Where(t => string.IsNullOrEmpty(t.CounterNo)).ToList();
                        DB.SaveAll(counter);
                        IsForwardSuccess = true;
                    }
                    
                    ts1.Complete();
                }
                catch(Exception ex)
                {

                    ShowErrorMessage("خطا در ذخیره سازی" , ex);
                }
            }

            return IsForwardSuccess;

        }

        #region Filters
        private bool PredicateFilters(object obj)
        {
            TranslationOpticalCabinetToNormalInfo checkableObject = obj as TranslationOpticalCabinetToNormalInfo;
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

        private void wiringButtom_Click(object sender, RoutedEventArgs e)
        {
            Folder.MessageBox.ShowInfo("این گزارش دردست  تهیه می باشد");
        }
    }
}
