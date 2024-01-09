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
using System.ComponentModel;
using CRM.Data;
using System.Transactions;
using System.Collections.ObjectModel;
using System.Collections;
using CRM.Application.Codes;
using System.Data;

namespace CRM.Application.Views
{
    public partial class CabinetForm : Local.PopupWindow
    {
        #region Properties and Fields

        private int _ID = 0;
        private int CityID = 0;
        private int oldToCabinetInput = 0;
        private List<CabinetInput> _OldCabinetInputList = new List<CabinetInput>();
        private List<Bucht> OldbuchtList = new List<Bucht>();
        private List<Bucht> NewbuchtList = new List<Bucht>();
        List<DataGridSelectedIndex> dataGridSelectedIndexs = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _groupingColumn = new List<DataGridSelectedIndex>();
        List<DataGridSelectedIndex> _sumColumn = new List<DataGridSelectedIndex>();
        string _title = string.Empty;

        BackgroundWorker _gropingCabinetInputBackgroundWorker;

        List<GroupingCabinetInput> _groupingCabinetInputs = new List<GroupingCabinetInput>();
        // ObservableCollection<BuchtAndTelephonOfOpticalCabinet> _buchtAndTelephonOfOpticalCabinets = new ObservableCollection<BuchtAndTelephonOfOpticalCabinet>();
        // List<BuchtAndTelephonOfOpticalCabinet> _oldBuchtAndTelephonOfOpticalCabinets ;
        List<GroupingCabinetInput> _oldGroupingCabinetInputs;
        List<Post> PostList;

        public ObservableCollection<CheckableItem> _InputNumbers { get; set; }
        public ObservableCollection<CheckableItem> _CablePairNumbers { get; set; }
        public ObservableCollection<CheckableItem> _Cables { get; set; }


        public ObservableCollection<CheckableItem> _mdf { get; set; }
        public ObservableCollection<CheckableItem> _buchtType { get; set; }

        public ObservableCollection<CheckableItem> _Columns { get; set; }
        public ObservableCollection<CheckableItem> _fromRows { get; set; }
        public ObservableCollection<CheckableItem> _fromBuchts { get; set; }

        public ObservableCollection<CheckableItem> _toRows { get; set; }
        public ObservableCollection<CheckableItem> _toBuchts { get; set; }

        public ObservableCollection<CheckableItem> _switchs { get; set; }
        public ObservableCollection<CheckableItem> _preCodes { get; set; }
        public ObservableCollection<CheckableItem> _fromTelephone { get; set; }


        #endregion

        #region Constructor

        public CabinetForm()
        {
            InitializeComponent();
            //Initialize();
        }

        public CabinetForm(int id)
            : this()
        {
            _ID = id;
            Initialize();
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            _gropingCabinetInputBackgroundWorker = new BackgroundWorker();
            _gropingCabinetInputBackgroundWorker.DoWork += new DoWorkEventHandler(_gropingCabinetInputBackgroundWorkerDoWork);
            _gropingCabinetInputBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_gropingCabinetInputBackgroundWorkerRunWorkerCompleted);

            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            CabinetTypeComboBox.ItemsSource = Data.CabinetTypeDB.GetCabinetTypeCheckable();

            //LastPostComboBox.ItemsSource = Data.PostDB.GetPostCheckable();

            StatusComboBox.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CabinetStatus));
            CabinetUsageType.ItemsSource = Helper.GetEnumCheckable(typeof(DB.CabinetUsageType));
            AorBComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.AORBPostAndCabinet));
        }

        private void LoadData()
        {
            Cabinet item = new Cabinet();

            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";

                if (CityID == 0)
                    CityComboBox.SelectedIndex = 0;

                else
                    CityComboBox.SelectedValue = CityID;
            }
            else
            {
                item = Data.CabinetDB.GetCabinetByID(_ID);
                CityID = Data.CabinetDB.GetCity(item.ID);



                oldToCabinetInput = (int)item.ToInputNo;

                if (CityID == 0)
                    CityComboBox.SelectedIndex = 0;

                else
                    CityComboBox.SelectedValue = CityID;

                CenterComboBox.SelectedValue = item.CenterID;

                //FromInputNo.IsEnabled = false;
                //ToInputNo.IsEnabled = false;

                SaveButton.Content = "بروزرسانی";

                #region postCount

                PostCountTextBox.Text = PostDB.GetDetailPostCountByCabinetID(_ID).ToString();
                #endregion postCount


                base.ResizeWindow();
            }

            this.DataContext = item;

            CabinetUsageType.SelectedValue = item.CabinetUsageType;
            CabinetUsageType_SelectionChanged(null, null);

            CenterComboBox_SelectionChanged_1(null, null);

            if (!_gropingCabinetInputBackgroundWorker.IsBusy)
                _gropingCabinetInputBackgroundWorker.RunWorkerAsync();


            //if ((this.DataContext as Cabinet).CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet)
            //{
            //    _buchtType = new ObservableCollection<CheckableItem>(Data.BuchtTypeDB.GetSubBuchtTypeCheckable((int)DB.BuchtType.OpticalBucht));
            //}
            //else
            //{
            //}

        }

        #endregion

        #region BackgroundWorker EventHandlers

        private void _gropingCabinetInputBackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((this.DataContext as Cabinet).CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet)
            {
                //if (_buchtAndTelephonOfOpticalCabinets.Count != 0)
                //{
                //    CityComboBox.IsEnabled = false;
                //    CenterComboBox.IsEnabled = false;
                //}
                //else
                //{
                //    CityComboBox.IsEnabled = true;
                //    CenterComboBox.IsEnabled = true;
                //}
                // ItemsAssignToTelephoneDataGrid.ItemsSource = _buchtAndTelephonOfOpticalCabinets;
            }
            //else
            //{
            if (_groupingCabinetInputs.Count != 0)
            {
                CityComboBox.IsEnabled = false;
                CenterComboBox.IsEnabled = false;
            }
            else
            {
                CityComboBox.IsEnabled = true;
                CenterComboBox.IsEnabled = true;
            }
            ItemsDataGrid.ItemsSource = _groupingCabinetInputs;
            //}

        }

        private void _gropingCabinetInputBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            //milad doran
            //Dispatcher.BeginInvoke((Action)delegate()
            //{
            //    _groupingCabinetInputs = Data.CabinetInputDB.GetGroupingCabinetInput(new List<int> { (int?)CityComboBox.SelectedValue ?? 0 }, new List<int> { (int?)CenterComboBox.SelectedValue ?? 0 }, new List<int> { _ID });
            //    _oldGroupingCabinetInputs = new List<GroupingCabinetInput>(_groupingCabinetInputs.Select(t => (GroupingCabinetInput)t.Clone()));

            //    //if ((this.DataContext as Cabinet).CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet)
            //    //{
            //    //    _buchtAndTelephonOfOpticalCabinets = new ObservableCollection<BuchtAndTelephonOfOpticalCabinet>(Data.CabinetDB.GetGroupsTelephonOfOpticalCabinet(_ID));
            //    //    //_buchtAndTelephonOfOpticalCabinets = new ObservableCollection<BuchtAndTelephonOfOpticalCabinet>();
            //    //    _oldBuchtAndTelephonOfOpticalCabinets = new List<BuchtAndTelephonOfOpticalCabinet>(_buchtAndTelephonOfOpticalCabinets.Select(t => (BuchtAndTelephonOfOpticalCabinet)t.Clone()));
            //    //}
            //    //else
            //    //{



            //    // }


            //});



            //_InputNumbers = new ObservableCollection<CheckableItem>(Data.CabinetInputDB.GetCabinetInputByCabinetID(_ID));

            //TODO:rad 13950220
            Dispatcher.BeginInvoke((Action)delegate()
            {
                //تعریف متغیرهای لازم برای متد زیر
                //Data.CabinetInputDB.GetGroupingCabinetInput
                List<int> cities = new List<int>();
                if ((int?)CityComboBox.SelectedValue == null)
                {
                    cities.Add(0);
                }
                else
                {
                    cities.Add((int)CityComboBox.SelectedValue);
                }

                List<int> centers = new List<int>();
                if ((int?)CenterComboBox.SelectedValue == null)
                {
                    centers.Add(0);
                }
                else
                {
                    centers.Add((int)CenterComboBox.SelectedValue);
                }

                List<int> cabinets = new List<int>();
                cabinets.Add(this._ID);

                _groupingCabinetInputs = Data.CabinetInputDB.GetGroupingCabinetInput(cities, centers, cabinets);
                //**************************************************************************************************************************************************

                _oldGroupingCabinetInputs = new List<GroupingCabinetInput>(_groupingCabinetInputs.Select(t => (GroupingCabinetInput)t.Clone()));

                //if ((this.DataContext as Cabinet).CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet)
                //{
                //    _buchtAndTelephonOfOpticalCabinets = new ObservableCollection<BuchtAndTelephonOfOpticalCabinet>(Data.CabinetDB.GetGroupsTelephonOfOpticalCabinet(_ID));
                //    //_buchtAndTelephonOfOpticalCabinets = new ObservableCollection<BuchtAndTelephonOfOpticalCabinet>();
                //    _oldBuchtAndTelephonOfOpticalCabinets = new List<BuchtAndTelephonOfOpticalCabinet>(_buchtAndTelephonOfOpticalCabinets.Select(t => (BuchtAndTelephonOfOpticalCabinet)t.Clone()));
                //}
                //else
                //{



                // }


            });

            _InputNumbers = new ObservableCollection<CheckableItem>(Data.CabinetInputDB.GetCabinetInputByCabinetID(_ID));
        }

        #endregion

        #region Event Handlers

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
                return;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, System.TimeSpan.FromMinutes(5)))
                {
                    Cabinet item = this.DataContext as Cabinet;

                    if (item.ToInputNo < oldToCabinetInput)
                    {
                        //following code has bad effect in performance
                        //throw new Exception("کاهش تعداد مرکزی ها  مقدور نمی باشد");

                        //correct code 
                        MessageBox.Show("کاهش تعداد مرکزی ها  مقدور نمی باشد", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        ToInputNo.Focus();
                        return;
                    }

                    item.FromInputNo = 1;
                    item.Detach();
                    if (item.ID == 0)
                    {
                        Save(item, true);
                    }
                    else
                    {
                        Save(item, false);
                    }


                    int fromInputNo;
                    if (_ID == 0)
                        fromInputNo = 1;
                    else
                        fromInputNo = (int)++oldToCabinetInput;

                    int toInputNo = Convert.ToInt32(ToInputNo.Text);

                    List<CabinetInput> cabinetInputList = new List<CabinetInput>();

                    for (int i = fromInputNo; i <= toInputNo; i++)
                    {
                        CabinetInput cabinetInput = new CabinetInput();
                        cabinetInput.CabinetID = item.ID;
                        cabinetInput.InputNumber = i;
                        cabinetInput.Status = (byte)DB.CabinetInputStatus.healthy;
                        cabinetInput.InsertDate = DB.GetServerDate();
                        cabinetInputList.Add(cabinetInput);
                    }

                    DB.SaveAll(cabinetInputList);

                    ts.Complete();
                    _ID = item.ID;
                    ShowSuccessMessage("ذخیره انجام شد");
                }
                LoadData();
            }
            catch (System.Data.SqlClient.SqlException se)
            {
                string errorMessage = SqlExceptionHelper.ErrorMessage(se);
                ShowErrorMessage(errorMessage, se);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره جعبه توزیع", ex);
            }
            //Milad doran
            //catch (Exception ex)
            //{
            //    if (ex.Message.Contains("Cannot insert duplicate key row in object"))
            //        ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
            //    else
            //        ShowErrorMessage("خطا در ذخیره جعبه توزیع", ex);

            //}
        }

        private void CenterComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //milad doran
            //if (CenterComboBox.SelectedValue != null)
            //{
            //    if (this.DataContext != null && (this.DataContext as Cabinet).CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet)
            //    {
            //        // _mdf = new ObservableCollection<CheckableItem>(Data.MDFDB.GetMDFCheckableByCenterID((int)CenterComboBox.SelectedValue));
            //        // _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetOpticalCabinetSwitchPrecodeCheckableItemByCenterID((int)CenterComboBox.SelectedValue));
            //        _Cables = new ObservableCollection<CheckableItem>(Data.CableDB.GetCableCheckableByCenterID(new List<int> { (int)CenterComboBox.SelectedValue }));
            //    }
            //    else
            //    {
            //        _Cables = new ObservableCollection<CheckableItem>(Data.CableDB.GetCableCheckableByCenterID(new List<int> { (int)CenterComboBox.SelectedValue }));
            //    }
            //}
            //***************************************************************************************************************************************************************************************
            if (CenterComboBox.SelectedValue != null)
            {
                List<int> centers = new List<int>();
                centers.Add((int)CenterComboBox.SelectedValue);

                if (this.DataContext != null && (this.DataContext as Cabinet).CabinetUsageType == (int)DB.CabinetUsageType.OpticalCabinet)
                {
                    _Cables = new ObservableCollection<CheckableItem>(Data.CableDB.GetCableCheckableByCenterID(centers));
                }
                else
                {
                    _Cables = new ObservableCollection<CheckableItem>(Data.CableDB.GetCableCheckableByCenterID(centers));
                }
            }
        }

        private void AssignToCableSaveButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                //   City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                if (this.DataContext != null)
                    (this.DataContext as Cabinet).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
            }
            else
            {
                if (CityComboBox.SelectedValue == null)
                {
                    //   City city = Data.CityDB.GetCityById(CityID);

                    City city = Data.CityDB.GetCityById(CityID);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
                else
                {
                    // City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);

                    City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
            }
        }

        private void CableNumberComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CableNumberComboBox.SelectedValue != null)
            {
                if (ItemsDataGrid.SelectedItem != null)
                {
                    (ItemsDataGrid.SelectedItem as GroupingCabinetInput).CableNumber = _Cables.Where(t => t.LongID == (long)CableNumberComboBox.SelectedValue).SingleOrDefault().Name;
                }

                _CablePairNumbers = new ObservableCollection<CheckableItem>(Data.CablePairDB.GetCablePairCheckableByCableID((long)CableNumberComboBox.SelectedValue));
            }
        }

        private void ItemsDataGrid_BeginningEdit_1(object sender, DataGridBeginningEditEventArgs e)
        {
            var row = (e.Row.Item as GroupingCabinetInput);
            if (row != null)
            {
                _CablePairNumbers = new ObservableCollection<CheckableItem>(Data.CablePairDB.GetCablePairCheckableByCableID(row.CableID));
            }
        }

        private void AssignToCable(object sender, RoutedEventArgs e)
        {
            if (_ID != 0)
            {
                AssignCabinetInputToBuchtForm window = new AssignCabinetInputToBuchtForm(_ID, (byte)DB.TypeCabinetInputToBucht.Assign);
                window.ShowDialog();
                if (!_gropingCabinetInputBackgroundWorker.IsBusy)
                    _gropingCabinetInputBackgroundWorker.RunWorkerAsync();
            }
        }

        private void LeaveFromBucht(object sender, RoutedEventArgs e)
        {

            try
            {
                GroupingCabinetInput groupingCabinetInput = ItemsDataGrid.SelectedItem as GroupingCabinetInput;
                if (groupingCabinetInput != null && groupingCabinetInput.ID != 0)
                {

                    long? FromCabinetInputID = _InputNumbers.Where(t => t.Name == groupingCabinetInput.FromInputNumber.ToString()).SingleOrDefault().LongID;
                    long? ToCabinetInputID = _InputNumbers.Where(t => t.Name == groupingCabinetInput.ToInputNumber.ToString()).SingleOrDefault().LongID;

                    if (CabinetDB.LeaveCabinetInputFromBucht(FromCabinetInputID ?? 0, ToCabinetInputID ?? 0))
                    {
                        ShowSuccessMessage("آزاد سازی انجام شد");
                    }
                }
                else
                {
                    throw new Exception();
                }

                if (!_gropingCabinetInputBackgroundWorker.IsBusy)
                    _gropingCabinetInputBackgroundWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                if (!_gropingCabinetInputBackgroundWorker.IsBusy)
                    _gropingCabinetInputBackgroundWorker.RunWorkerAsync();
                ShowErrorMessage("خطا در آزاد سازی", ex);

            }
        }

        private void ItemsDataGrid_RowEditEnding_1(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                // if the record is new, assingnment is done. else First, it is clear early assignment. then new assignment is performed
                GroupingCabinetInput groupingCabinetInput = ItemsDataGrid.SelectedItem as GroupingCabinetInput;
                if (groupingCabinetInput != null && groupingCabinetInput.ID == 0)
                {
                    long? fromCabinetInputID = _InputNumbers.Where(t => t.Name == groupingCabinetInput.FromInputNumber.ToString()).SingleOrDefault().LongID;
                    long? toCabinetInputID = _InputNumbers.Where(t => t.Name == groupingCabinetInput.ToInputNumber.ToString()).SingleOrDefault().LongID;

                    long? fromCablePairID = _CablePairNumbers.Where(t => t.Name == groupingCabinetInput.FromCablePairNumber.ToString()).SingleOrDefault().LongID;
                    long? toCablePairID = _CablePairNumbers.Where(t => t.Name == groupingCabinetInput.ToCablePairNumber.ToString()).SingleOrDefault().LongID;



                    if (CabinetDB.AssignCabinetInputToCable(fromCabinetInputID ?? 0, toCabinetInputID ?? 0, fromCablePairID ?? 0, toCablePairID ?? 0))
                    {
                        if (!_gropingCabinetInputBackgroundWorker.IsBusy)
                            _gropingCabinetInputBackgroundWorker.RunWorkerAsync();

                        ShowSuccessMessage("ذخیره انجام شد");
                    }
                }
                else
                {


                    GroupingCabinetInput oldGroupingCabinetInputItem = _oldGroupingCabinetInputs.Where(t => t.ID == groupingCabinetInput.ID).SingleOrDefault();

                    long? oldFromCabinetInputID = _InputNumbers.Where(t => t.Name == oldGroupingCabinetInputItem.FromInputNumber.ToString()).SingleOrDefault().LongID;
                    long? oldToCabinetInputID = _InputNumbers.Where(t => t.Name == oldGroupingCabinetInputItem.ToInputNumber.ToString()).SingleOrDefault().LongID;

                    long? fromCabinetInputID = _InputNumbers.Where(t => t.Name == groupingCabinetInput.FromInputNumber.ToString()).SingleOrDefault().LongID;
                    long? toCabinetInputID = _InputNumbers.Where(t => t.Name == groupingCabinetInput.ToInputNumber.ToString()).SingleOrDefault().LongID;

                    long? fromCablePairID = _CablePairNumbers.Where(t => t.Name == groupingCabinetInput.FromCablePairNumber.ToString()).SingleOrDefault().LongID;
                    long? toCablePairID = _CablePairNumbers.Where(t => t.Name == groupingCabinetInput.ToCablePairNumber.ToString()).SingleOrDefault().LongID;

                    using (TransactionScope tsRoot = new TransactionScope(TransactionScopeOption.RequiresNew, System.TimeSpan.FromMinutes(2)))
                    {
                        if (CabinetDB.LeaveCabinetInputFromBucht(oldFromCabinetInputID ?? 0, oldToCabinetInputID ?? 0))
                        {


                            if (CabinetDB.AssignCabinetInputToCable(fromCabinetInputID ?? 0, toCabinetInputID ?? 0, fromCablePairID ?? 0, toCablePairID ?? 0))
                            {
                                tsRoot.Complete();
                                ShowSuccessMessage("ذخیره انجام شد");

                            }

                        }
                        else
                        {
                            MessageBox.Show("خطا در بروز رسانی");
                        }

                    }
                    if (!_gropingCabinetInputBackgroundWorker.IsBusy)
                        _gropingCabinetInputBackgroundWorker.RunWorkerAsync();

                }


            }
            catch (Exception ex)
            {

                if (!_gropingCabinetInputBackgroundWorker.IsBusy)
                    _gropingCabinetInputBackgroundWorker.RunWorkerAsync();
                ShowErrorMessage("خطا در ذخیره سازی", ex);
            }
        }

        private void CabinetUsageType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CabinetUsageType.SelectedValue != null && (int)CabinetUsageType.SelectedValue == (int)DB.CabinetUsageType.OpticalCabinet)
            {
                OpticalCabinetSwitchComboBox.ItemsSource = Data.SwitchDB.GetOpticalCabinetSwitchbyCenterID((this.DataContext as Cabinet).CenterID);
                OpticalCabinetSwitchComboBoxLabel.Visibility = Visibility.Visible;
                OpticalCabinetSwitchComboBox.Visibility = Visibility.Visible;
            }
            else if (CabinetUsageType.SelectedValue != null && (int)CabinetUsageType.SelectedValue == (int)DB.CabinetUsageType.WLL)
            {
                OpticalCabinetSwitchComboBox.ItemsSource = Data.SwitchDB.GetWLLCabinetSwitchbyCenterID((this.DataContext as Cabinet).CenterID);
                OpticalCabinetSwitchComboBoxLabel.Visibility = Visibility.Visible;
                OpticalCabinetSwitchComboBox.Visibility = Visibility.Visible;
            }
            else
            {
                OpticalCabinetSwitchComboBoxLabel.Visibility = Visibility.Collapsed;
                OpticalCabinetSwitchComboBox.Visibility = Visibility.Collapsed;
            }
        }

        private void SwitchComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        #region LoadComboBox

        ComboBox CableNumberComboBox = new ComboBox();
        private void CableNumberComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            CableNumberComboBox = sender as ComboBox;
        }


        ComboBox MDFComboBox = new ComboBox();
        private void MDFComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            MDFComboBox = sender as ComboBox;
        }

        ComboBox FromColumnComboBox = new ComboBox();
        private void FromColumnComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            FromColumnComboBox = sender as ComboBox;
        }

        ComboBox ToColumnComboBox = new ComboBox();
        private void ToColumnComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            ToColumnComboBox = sender as ComboBox;
        }

        ComboBox FromRowComboBox = new ComboBox();
        private void FromRowComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            FromRowComboBox = sender as ComboBox;
        }

        ComboBox ToRowComboBox = new ComboBox();
        private void ToRowComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            ToRowComboBox = sender as ComboBox;
        }

        ComboBox FromConnectionComboBox = new ComboBox();
        private void FromConnectionComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            FromConnectionComboBox = sender as ComboBox;
        }

        ComboBox ToConnectionComboBox = new ComboBox();
        private void ToConnectionComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            ToConnectionComboBox = sender as ComboBox;
        }

        ComboBox SwitchComboBox = new ComboBox();
        private void SwitchComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            SwitchComboBox = sender as ComboBox;
        }

        ComboBox PreCodeComboBox = new ComboBox();
        private void PreCodeComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            PreCodeComboBox = sender as ComboBox;
        }

        #endregion

        #region Print

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.AddToSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void UnCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.Content != null)
            {
                DataGridColumn dataGridColumn = ItemsDataGrid.Columns.Single(c => c.Header == checkBox.Content);
                dataGridSelectedIndexs = Print.RemoveFromSelectedIndex(dataGridSelectedIndexs, dataGridColumn, checkBox.Content.ToString());
            }
        }

        private void SaveColumnItem_Click(object sender, RoutedEventArgs e)
        {
            CRM.Application.Codes.Print.SaveDataGridColumn(dataGridSelectedIndexs, this.GetType().Name, ItemsDataGrid.Name, ItemsDataGrid.Columns);
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;


            DataSet data = _groupingCabinetInputs.ToDataSet("Result", ItemsDataGrid);

            Print.DynamicPrintV2(data, _title, dataGridSelectedIndexs, _groupingColumn, _sumColumn);

            this.Cursor = Cursors.Arrow;
        }

        private void ReportSettingItem_Click(object sender, RoutedEventArgs e)
        {
            List<DataGridColumn> dataGridColumn = new List<DataGridColumn>(ItemsDataGrid.Columns);
            ReportSettingForm reportSettingForm = new ReportSettingForm(dataGridColumn);
            reportSettingForm._title = _title;
            reportSettingForm._checkedList.Clear();
            reportSettingForm._checkedList = _groupingColumn;
            reportSettingForm._sumCheckedList = _sumColumn;
            reportSettingForm.ShowDialog();
            _sumColumn = reportSettingForm._sumCheckedList;
            _groupingColumn = reportSettingForm._checkedList;
            _title = reportSettingForm._title;

        }

        #endregion

        #region AsignBuchtEvents
        //private void MDFComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (MDFComboBox.SelectedValue != null)
        //    {
        //        _Columns = new ObservableCollection<CheckableItem>(DB.GetConnectionColumnInfo((int)MDFComboBox.SelectedValue));
        //        if (ItemsAssignToBuchtDataGrid.SelectedItem != null)
        //        {
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).MDFNumber = (MDFComboBox.SelectedItem as CheckableItem).Name;
        //        }

        //    }
        //}
        //private void BuchtTypeComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (BuchtTypeComboBox.SelectedValue != null)
        //    {
        //        if (ItemsAssignToBuchtDataGrid.SelectedItem != null)
        //        {
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).BuchtTypeName = (BuchtTypeComboBox.SelectedItem as CheckableItem).Name;
        //        }
        //    }
        //}
        //ComboBox BuchtTypeComboBox = new ComboBox();
        //private void BuchtTypeComboBox_Loaded_1(object sender, RoutedEventArgs e)
        //{
        //    BuchtTypeComboBox = sender as ComboBox;
        //}
        //private void FromColumnComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (FromColumnComboBox.SelectedValue != null)
        //    {
        //        _fromRows = new ObservableCollection<CheckableItem>(DB.GetConnectionRowInfo((int)FromColumnComboBox.SelectedValue));
        //        if (ItemsAssignToBuchtDataGrid.SelectedItem != null)
        //        {
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromVerticalCloumnNo = (FromColumnComboBox.SelectedItem as CheckableItem).Name;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromVerticalRowID = 0;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromVerticalRowNo = String.Empty;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromBuchtID = 0;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromBuchtNo = string.Empty;
        //            //_buchtAndTelephonOfOpticalCabinets.Where(t => t.ID == (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ID).SingleOrDefault().FromVerticalRowID = 0;
        //            //_buchtAndTelephonOfOpticalCabinets.Where(t => t.ID == (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ID).SingleOrDefault().FromVerticalRowNo = String.Empty;
        //            //_buchtAndTelephonOfOpticalCabinets.Where(t => t.ID == (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ID).SingleOrDefault().FromBuchtID = 0;
        //            //_buchtAndTelephonOfOpticalCabinets.Where(t => t.ID == (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ID).SingleOrDefault().FromBuchtNo = string.Empty;

        //        }
        //    }
        //}
        //private void ToColumnComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (ToColumnComboBox.SelectedValue != null)
        //    {
        //        _toRows = new ObservableCollection<CheckableItem>(DB.GetConnectionRowInfo((int)ToColumnComboBox.SelectedValue));
        //        if (ItemsAssignToBuchtDataGrid.SelectedItem != null)
        //        {
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToVerticalCloumnNo = (ToColumnComboBox.SelectedItem as CheckableItem).Name;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToVerticalRowID = 0;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToVerticalRowNo = String.Empty;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToBuchtID = 0;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToBuchtNo = string.Empty;
        //        }
        //    }
        //}

        //private void FromRowComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (FromRowComboBox.SelectedValue != null)
        //    {
        //        _fromBuchts = new  ObservableCollection<CheckableItem>(DB.GetConnectionBuchtInfo((int)FromRowComboBox.SelectedValue,true , (int?)BuchtTypeComboBox.SelectedValue ?? 0));
        //        if (ItemsAssignToBuchtDataGrid.SelectedItem != null)
        //        {
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromVerticalRowNo = (FromRowComboBox.SelectedItem as CheckableItem).Name;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromBuchtID = 0;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromBuchtNo = string.Empty;
        //        }
        //    }
        //}

        //private void ToRowComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (ToRowComboBox.SelectedValue != null)
        //    {
        //        _toBuchts = new ObservableCollection<CheckableItem>(DB.GetConnectionBuchtInfo((int)ToRowComboBox.SelectedValue, true, (int?)BuchtTypeComboBox.SelectedValue ?? 0 ));
        //        if (ItemsAssignToBuchtDataGrid.SelectedItem != null)
        //        {
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToVerticalRowNo = (ToRowComboBox.SelectedItem as CheckableItem).Name;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToBuchtID = 0;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToBuchtNo = string.Empty;
        //        }
        //    }
        //}

        //private void FromConnectionComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (FromConnectionComboBox.SelectedValue != null)
        //    {
        //        if (ItemsAssignToBuchtDataGrid.SelectedItem != null)
        //        {
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromBuchtNo = (FromConnectionComboBox.SelectedItem as CheckableItem).Name;
        //        }
        //    }
        //}

        //private void ToConnectionComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (ToConnectionComboBox.SelectedValue != null)
        //    {
        //        if (ItemsAssignToBuchtDataGrid.SelectedItem != null)
        //        {
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToBuchtNo = (ToConnectionComboBox.SelectedItem as CheckableItem).Name;
        //        }
        //    }
        //}
        #region Switch && Telephone

        //private void SwitchComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if(SwitchComboBox.SelectedValue != null)
        //    {
        //        _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemBySwitchID((int)SwitchComboBox.SelectedValue));
        //        if (ItemsAssignToBuchtDataGrid.SelectedItem != null)
        //        {
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).SwitchCode = (SwitchComboBox.SelectedItem as CheckableItem).Name;

        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).SwitchPreNo = string.Empty;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).SwitchPrecodeID = 0;

        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromTelephone = 0;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToTelephone = 0;

        //        }
        //    }
        //}

        //private void PreCodeComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{

        //    if (PreCodeComboBox.SelectedValue != null)
        //    {
        //        _fromTelephone = new ObservableCollection<CheckableItem>(Data.TelephoneDB.GetCheckableItemFreeTelephoneBySwitchPreCodeID((int)PreCodeComboBox.SelectedValue));
        //        if (ItemsAssignToBuchtDataGrid.SelectedItem != null)
        //        {
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).SwitchPreNo = (PreCodeComboBox.SelectedItem as CheckableItem).Name;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromTelephone = 0;
        //            (ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToTelephone = 0;
        //        }
        //    }
        //}
        private void FromTelephoneComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
        private void FromTelephoneComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ToTelephoneComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void ToTelephoneComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion

        //private void ItemsAssignToBuchtDataGrid_RowEditEnding_1(object sender, DataGridRowEditEndingEventArgs e)

        //{
        //    // if row is new save it , else first delete The previous row then save new row 
        //    BuchtAndTelephonOfOpticalCabinet row = ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet;
        //    if(row.ID == 0)
        //    {

        //       if(BuchtAndTelephonOfOpticalCabinetSave(row))
        //        ShowSuccessMessage("انتساب انجام شد");

        //    }

        //    else if(row.ID != 0)
        //    {
        //        using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew))
        //        {
        //            if (BuchtAndTelephonOfOpticalCabinetDelete(_oldBuchtAndTelephonOfOpticalCabinets.Where(t => t.ID == row.ID).SingleOrDefault())
        //                && BuchtAndTelephonOfOpticalCabinetSave(row))
        //                ShowSuccessMessage("دخیره انجام شد");
        //            ts.Complete();



        //        }

        //    }
        //    if (!_gropingCabinetInputBackgroundWorker.IsBusy)
        //        _gropingCabinetInputBackgroundWorker.RunWorkerAsync();
        //}

        //private bool BuchtAndTelephonOfOpticalCabinetSave(BuchtAndTelephonOfOpticalCabinet row)
        //{
        //    try
        //    {
        //        using (TransactionScope tsSave = new TransactionScope(TransactionScopeOption.Required))
        //        {
        //            int maxCablePairNumber = 1;
        //            List<Bucht> buchts = Data.BuchtDB.GetBuchtFromIDToID(row.FromBuchtID, row.ToBuchtID, -1, true);
        //            List<Telephone> telephones = Data.TelephoneDB.GetTelephoneFromTelToTel(row.FromTelephone, row.ToTelephone);
        //            List<CheckableItem> cabinetInput = Data.CabinetInputDB.GetCabinetInputFreeByCabinetID(_ID);

        //            if (buchts.Count() != telephones.Count()) { MessageBox.Show("تعداد بوخت ها برابر تعداد تلفن ها نمیباشد"); return false; }
        //            if (telephones.Any(t => t.SwitchPortID == null)) { MessageBox.Show("در میان تلفن ها تلفن غییر متصل به پورت وجود دارد"); return false; }
        //            if (buchts.Count > cabinetInput.Count) { MessageBox.Show("ظریفیت آزاد کافو کمتر از تعداد بوخت های انتخاب شده میباشد."); return false; }

        //            List<int> SwitchPortIDs = telephones.Select(t => (int)t.SwitchPortID).ToList();
        //            List<SwitchPort> switchPorts = Data.SwitchPortDB.getSwitchPortBySwitchPortIDs(SwitchPortIDs);

        //            Cable virtualCable = Data.CableDB.GetVirtualCableByCabinetID(_ID);

        //            if (virtualCable == null)
        //            {
        //                virtualCable = new Cable();
        //                virtualCable.CabinetIDInVirtualCable = _ID;
        //                virtualCable.CenterID = (this.DataContext as Cabinet).CenterID;
        //                virtualCable.CableUsedChannelID = (int)DB.CableUsedChannel.OpticalCabinet;
        //                virtualCable.CableTypeID = (int)DB.CableType.OpticalCabinet;
        //                virtualCable.CableNumber = (this.DataContext as Cabinet).CabinetNumber;
        //                virtualCable.CableDiameter = 0;
        //                virtualCable.Detach();
        //                DB.Save(virtualCable, true);

        //            }
        //            else
        //            {
        //                maxCablePairNumber = Data.CablePairDB.GetMaxCablePairNumber(virtualCable.ID) + 1 ?? 1;
        //            }
        //            DateTime serverDate = DB.ServerDate() ?? Convert.ToDateTime(0);
        //            List<CablePair> cablePairs = new List<CablePair>();
        //            for (int i = 0; i < buchts.Count; i++)
        //            {
        //                CablePair cablePair = new CablePair();
        //                cablePair.CableID = virtualCable.ID;
        //                cablePair.CablePairNumber = maxCablePairNumber++;
        //                cablePair.CabinetInputID = cabinetInput[i].LongID;
        //                cablePair.Status = (byte)DB.CablePairStatus.ConnectedToBucht;
        //                cablePair.InsertDate = serverDate;
        //                cablePair.Detach();

        //                cablePairs.Add(cablePair);
        //            }
        //            DB.SaveAll(cablePairs);

        //            for (int i = 0; i < buchts.Count; i++)
        //            {
        //                buchts[i].CablePairID = cablePairs[i].ID;
        //                buchts[i].CabinetInputID = cablePairs[i].CabinetInputID;
        //                buchts[i].SwitchPortID = telephones[i].SwitchPortID;
        //                buchts[i].Detach();
        //            }

        //            DB.UpdateAll(buchts);

        //            switchPorts.ForEach((SwitchPort t) => {
        //                                                    t.Type = true;
        //                                                    t.Status = (byte)DB.SwitchPortStatus.Install;
        //                                                    t.Detach();
        //                                                  }
        //                                                  );

        //            DB.UpdateAll(switchPorts);

        //            tsSave.Complete();
        //      }

        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //private void ItemsAssignToBuchtDataGrid_BeginningEdit_1(object sender, DataGridBeginningEditEventArgs e)
        //{

        //    BuchtAndTelephonOfOpticalCabinet row = ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet;
        //    if (row.ID != 0)
        //    {
        //    _Columns = new ObservableCollection<CheckableItem>(DB.GetConnectionColumnInfo(row.MDFID));
        //    _fromRows = new ObservableCollection<CheckableItem>(DB.GetConnectionRowInfo(row.FromVerticalCloumnID));
        //    _fromBuchts = new ObservableCollection<CheckableItem>(DB.GetConnectionBuchtInfo(row.FromVerticalRowID, true, row.BuchtTypeNameID));

        //    _toRows = new ObservableCollection<CheckableItem>(DB.GetConnectionRowInfo(row.ToVerticalCloumnID));
        //    _toBuchts = new ObservableCollection<CheckableItem>(DB.GetConnectionBuchtInfo(row.ToVerticalRowID, true, row.BuchtTypeNameID));

        //    _preCodes = new ObservableCollection<CheckableItem>(Data.SwitchPrecodeDB.GetSwitchPrecodeCheckableItemBySwitchID(row.SwitchID));
        //    _fromTelephone = new ObservableCollection<CheckableItem>(Data.TelephoneDB.GetTelephoneBySwitchPreCodeID(row.SwitchPrecodeID).Select(t => new CheckableItem { LongID = t.TelephoneNo, Name = t.TelephoneNo.ToString(), IsChecked = false }));
        //    }


        //}

        //private void LeaveOpticalBucht(object sender, RoutedEventArgs e)
        //{
        //    BuchtAndTelephonOfOpticalCabinet row = ItemsAssignToBuchtDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet;

        //    if(BuchtAndTelephonOfOpticalCabinetDelete(row))
        //    ShowSuccessMessage("آزاد سازی انجام شد");

        //    if (!_gropingCabinetInputBackgroundWorker.IsBusy)
        //        _gropingCabinetInputBackgroundWorker.RunWorkerAsync();


        //}

        //private bool BuchtAndTelephonOfOpticalCabinetDelete(BuchtAndTelephonOfOpticalCabinet row)
        //{
        //    try
        //    {
        //        using (TransactionScope tsDelete = new TransactionScope(TransactionScopeOption.Required))
        //        {
        //            if (row.ID != 0)
        //            {
        //                List<Bucht> buchts = Data.BuchtDB.GetBuchtFromIDToID(row.FromBuchtID, row.ToBuchtID, -1, false);
        //                List<Telephone> telephones = Data.TelephoneDB.GetTelephoneFromTelToTel(row.FromTelephone, row.ToTelephone);
        //                //  List<CheckableItem> cabinetInput = Data.CabinetInputDB.GetCabinetInputFreeByCabinetID(_ID).OrderBy(t=>t.LongID).ToList();

        //                List<SwitchPort> switchPorts = Data.SwitchPortDB.getSwitchPortBySwitchPortIDs(telephones.Select(t => (int)t.SwitchPortID).ToList());

        //                Cable virtualCable = Data.CableDB.GetVirtualCableByCabinetID(_ID);

        //                List<CablePair> cablePairs = Data.CablePairDB.GetCablePairsIDs(buchts.Select(t => t.CablePairID ?? 0).ToList());


        //                switchPorts.ForEach((SwitchPort t) => { t.Status = (byte)DB.SwitchPortStatus.Free; t.Detach(); });
        //                buchts.ForEach((Bucht obj) => { obj.SwitchPortID = null; obj.CabinetInputID = null; obj.CablePairID = null; obj.Detach(); });




        //                DB.UpdateAll(switchPorts);
        //                DB.UpdateAll(buchts);

        //                DB.DeleteAll<CablePair>(cablePairs.Select(t => t.ID).ToList());


        //            }
        //            tsDelete.Complete();
        //        }
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        #endregion

        //private void ItemsAssignToTelephoneDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        //{


        //    try
        //    {
        //        BuchtAndTelephonOfOpticalCabinet row = ItemsAssignToTelephoneDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet;
        //        if (row.ID == 0)
        //        {
        //            List<Telephone> telephones = Data.TelephoneDB.GetTelephoneFromTelToTel(row.FromTelephone, row.ToTelephone);

        //            if (telephones.Any(t => t.Status != (int)DB.TelephoneStatus.Free )) { throw new Exception("همه تلفن ها باید آزاد باشند"); }

        //            telephones.ForEach(item => { item.CabinetID = (this.DataContext as Cabinet).ID; item.Detach(); });

        //            DB.UpdateAll(telephones);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        ShowErrorMessage("خطا در انجام عملیات", ex);
        //    }
        //}

        //private void LeaveOpticalBucht(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        BuchtAndTelephonOfOpticalCabinet row = ItemsAssignToTelephoneDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet;
        //        if (row.ID != 0)
        //        {
        //            List<Telephone> telephones = Data.TelephoneDB.GetTelephoneFromTelToTel(row.FromTelephone, row.ToTelephone);

        //            if (telephones.Any(t => t.Status != (int)DB.TelephoneStatus.Free)) { throw new Exception("همه تلفن ها باید آزاد باشند"); }

        //            telephones.ForEach(item => { item.CabinetID = null ; item.Detach(); });

        //            DB.UpdateAll(telephones);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowErrorMessage("خطا در انجام عملیات", ex);
        //    }
        //}

        //private void PreCodeComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        //{
        //    if (PreCodeComboBox.SelectedValue != null)
        //    {
        //        _fromTelephone = new ObservableCollection<CheckableItem>(Data.TelephoneDB.GetCheckableItemFreeTelephoneBySwitchPreCodeID((int)PreCodeComboBox.SelectedValue));
        //        if (ItemsAssignToTelephoneDataGrid.SelectedItem != null)
        //        {
        //            (ItemsAssignToTelephoneDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).SwitchPreNo = (PreCodeComboBox.SelectedItem as CheckableItem).Name;
        //            (ItemsAssignToTelephoneDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).FromTelephone = 0;
        //            (ItemsAssignToTelephoneDataGrid.SelectedItem as BuchtAndTelephonOfOpticalCabinet).ToTelephone = 0;
        //        }
        //    }
        //}

    }
}
