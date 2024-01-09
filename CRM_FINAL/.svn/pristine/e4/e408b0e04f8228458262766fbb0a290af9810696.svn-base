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
using System.Collections.ObjectModel;
using System.Transactions;
using System.Data;
using Enterprise;

namespace CRM.Application.Views
{
    public partial class CableForm : Local.PopupWindow
    {
        #region Properties

        public static List<ConnectionInfo> connectionColumnList { get; set; }
        public static List<ConnectionInfo> connectionRowList { get; set; }
        public static List<ConnectionInfo> connectionBuchtList { get; set; }
        public static List<ConnectionInfo> connectionList { get; set; }
        List<CheckableItem> CablePairs { get; set; }
        private List<CablePair> cablePairList = new List<CablePair>();
        BackgroundWorker _groupingCablePairBackgroundWorker;
        List<GroupingCablePair> _groupingCablePairs = new List<GroupingCablePair>();
        List<GroupingCablePair> _oldGroupingCablePairs;
        private long _ID = 0;
        private int CityID = 0;
        int oldToCablePairNumber = 0;
        public ObservableCollection<CheckableItem> _MDFLists { get; set; }
        public ObservableCollection<CheckableItem> _columns { get; set; }
        public ObservableCollection<CheckableItem> _rows { get; set; }
        public ObservableCollection<CheckableItem> _buchtNos { get; set; }

        #endregion

        #region Constructors

        public CableForm()
        {
            InitializeComponent();
            Initialize();
        }

        public CableForm(long id)
            : this()
        {
            _ID = id;
            Initialize();
        }

        #endregion

        #region Methods Initialize and  Load

        private void Initialize()
        {
            _groupingCablePairBackgroundWorker = new BackgroundWorker();
            _groupingCablePairBackgroundWorker.DoWork += new DoWorkEventHandler(_groupingCablePairBackgroundWorkerDoWork);
            _groupingCablePairBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_groupingCablePairBackgroundWorkerRunWorkerComleted);

            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();
            CableTypeComboBox.ItemsSource = Data.CableTypeDB.GetCableTypeCheckable();
            CableUsedChannelComboBox.ItemsSource = Data.CableUsedChannelDB.GetCableUsedChannelCheckable();
            StatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.CableStatus));
            PhysicalTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.CablePhysicalType));
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (!_groupingCablePairBackgroundWorker.IsBusy)
                _groupingCablePairBackgroundWorker.RunWorkerAsync();

            Cable item = new Cable();
            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";
                item.FromCablePairNumber = 1;
            }
            else
            {
                item = Data.CableDB.GetCableByID(_ID);
                oldToCablePairNumber = item.ToCablePairNumber;
                CenterComboBox.SelectedValue = item.CenterID;
                CenterComboBox_SelectionChanged_1(null, null);
                SaveButton.Content = "بروزرسانی";

                //FromCablePairLable.IsEnabled = false;
                //FromCablePairTextBox.IsEnabled = false;

                //ToCablePairLable.IsEnabled = false;
                //ToCablePairTextBox.IsEnabled = false;
                CityID = Data.CableDB.GetCity(item.ID);
            }

            this.DataContext = item;

            if (CityID == 0)
                CityComboBox.SelectedIndex = 0;
            else
                CityComboBox.SelectedValue = CityID;
        }

        #endregion

        #region Methods

        private void _groupingCablePairBackgroundWorkerRunWorkerComleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_groupingCablePairs.Count != 0)
            {
                CityComboBox.IsEnabled = false;
                CenterComboBox.IsEnabled = false;
            }
            else
            {
                CityComboBox.IsEnabled = true;
                CenterComboBox.IsEnabled = true;
            }
            ItemsDataGrid.ItemsSource = _groupingCablePairs;
        }

        private void _groupingCablePairBackgroundWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            //milad doran
            //Dispatcher.BeginInvoke((Action)delegate()
            //{
            //    _groupingCablePairs = Data.CablePairDB.GetGroupingCablePair(new List<int> { (int?)CityComboBox.SelectedValue ?? 0 }, new List<int> { (int?)CenterComboBox.SelectedValue ?? 0 }, new List<long> { _ID });
            //    _oldGroupingCablePairs = new List<GroupingCablePair>(_groupingCablePairs.Select(t => (GroupingCablePair)t.Clone()));

            //    CablePairs = Data.CablePairDB.GetCablePairCheckableByCableID(_ID);

            //    FromCablePairNumberComboBox.ItemsSource = CablePairs;
            //    ToCablePairNumberComboBox.ItemsSource = CablePairs;

            //});

            Dispatcher.BeginInvoke((Action)delegate()
            {
                //تعریف متغیرهای لازم برای متد زیر
                //Data.CablePairDB.GetGroupingCablePair
                List<int> cities = new List<int>();
                if (((int?)CityComboBox.SelectedValue) == null) //اگر شهری انتخاب نشد
                {
                    cities.Add(0);
                }
                else
                {
                    cities.Add((int)CityComboBox.SelectedValue);
                }

                List<int> centers = new List<int>();
                if ((int?)CenterComboBox.SelectedValue == null) //اگر مرکزی انتخاب نشد
                {
                    centers.Add(0);
                }
                else
                {
                    centers.Add((int)CenterComboBox.SelectedValue);
                }

                List<long> cables = new List<long>();
                cables.Add(this._ID);

                _groupingCablePairs = Data.CablePairDB.GetGroupingCablePair(cities, centers, cables);
                //*******************************************************************************************************************************

                _oldGroupingCablePairs = new List<GroupingCablePair>(_groupingCablePairs.Select(t => (GroupingCablePair)t.Clone()));

                CablePairs = Data.CablePairDB.GetCablePairCheckableByCableID(_ID);

                FromCablePairNumberComboBox.ItemsSource = CablePairs;
                ToCablePairNumberComboBox.ItemsSource = CablePairs;

            });
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }

            int fromCablePair;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, System.TimeSpan.FromMinutes(5)))
                {
                    Cable item = this.DataContext as Cable;
                    if (item.ToCablePairNumber < oldToCablePairNumber)
                    {
                        //following code has bad effect in performance
                        //throw new Exception("کاهش تعداد زوج ها  مقدور نمی باشد");

                        //correct code 
                        MessageBox.Show("کاهش تعداد زوج ها  مقدور نمی باشد", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                        ToCablePairTextBox.Focus();
                        return;
                    }

                    item.InsertDate = DB.GetServerDate();
                    item.Detach();
                    Save(item);
                    _ID = item.ID;

                    if (_ID == 0)
                        fromCablePair = 1;
                    else
                        fromCablePair = (int)++oldToCablePairNumber;

                    int toCablePair = -1;
                    if (!int.TryParse(ToCablePairTextBox.Text.Trim(), out toCablePair)) { toCablePair = -1; };

                    for (int i = fromCablePair; i <= toCablePair; i++)
                    {
                        CablePair cablePair = new CablePair();
                        cablePair.ID = 0;
                        cablePair.CableID = item.ID;
                        cablePair.CablePairNumber = i;
                        cablePair.InsertDate = DB.GetServerDate();
                        cablePair.Status = (byte)DB.CablePairStatus.Free;
                        cablePairList.Add(cablePair);
                    }
                    DB.SaveAll(cablePairList);
                    ts.Complete();
                    ShowSuccessMessage("کابل ذخیره شد");
                }

                LoadData();

            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                //if (ex.Message.Contains("Cannot insert duplicate key row in object"))
                //    ShowErrorMessage("مقادیر وارد شده در پایگاه داده وجود دارد", ex);
                string errorMessage = SqlExceptionHelper.ErrorMessage(ex);
                ShowErrorMessage(errorMessage, ex);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره کابل", ex);
            }
        }

        private void AssignBucht(object sender, RoutedEventArgs e)
        {
            if (_ID != 0)
            {
                AssignCablePairToBuchtForm window = new AssignCablePairToBuchtForm(_ID, (byte)DB.TypeCablePairToBucht.Assign);
                window.ShowDialog();
                if (!_groupingCablePairBackgroundWorker.IsBusy)
                    _groupingCablePairBackgroundWorker.RunWorkerAsync();
            }
        }

        private void ItemsDataGrid_RowEditEnding_1(object sender, DataGridRowEditEndingEventArgs e)
        {

            try
            {
                GroupingCablePair groupingCablePair = e.Row.Item as GroupingCablePair;
                // if the row in new saved. else first leaved the buchts then save new row

                if (groupingCablePair != null && groupingCablePair.ID == 0)
                {

                    long? FromCablePairID = CablePairs.Where(t => t.Name == groupingCablePair.FromCablePairNumber.ToString()).SingleOrDefault().LongID;
                    long? ToCablePairID = CablePairs.Where(t => t.Name == groupingCablePair.ToCablePairNumber.ToString()).SingleOrDefault().LongID;

                    long? FromBuchtID = _buchtNos.Where(t => t.Name == groupingCablePair.FromBuchtNo.ToString()).SingleOrDefault().LongID;
                    long? ToBuchtID = _buchtNos.Where(t => t.Name == groupingCablePair.ToBuchtNo.ToString()).SingleOrDefault().LongID;

                    if (CableDB.AssignCablePairToBuchtForm(FromCablePairID ?? 0, ToCablePairID ?? 0, FromBuchtID ?? 0, ToBuchtID ?? 0, (int?)RowComboBox.SelectedValue ?? 0))
                    {
                        if (!_groupingCablePairBackgroundWorker.IsBusy)
                            _groupingCablePairBackgroundWorker.RunWorkerAsync();
                        ShowSuccessMessage("ذخیره انجام شد");

                    }

                }
                else
                {


                    GroupingCablePair oldgroupingCablePairItem = _oldGroupingCablePairs.Where(t => t.ID == groupingCablePair.ID).SingleOrDefault();
                    long? oldFromCablePairID = CablePairs.Where(t => t.Name == oldgroupingCablePairItem.FromCablePairNumber.ToString()).SingleOrDefault().LongID;
                    long? oldToCablePairID = CablePairs.Where(t => t.Name == oldgroupingCablePairItem.ToCablePairNumber.ToString()).SingleOrDefault().LongID;

                    long? FromCablePairID = CablePairs.Where(t => t.Name == groupingCablePair.FromCablePairNumber.ToString()).SingleOrDefault().LongID;
                    long? ToCablePairID = CablePairs.Where(t => t.Name == groupingCablePair.ToCablePairNumber.ToString()).SingleOrDefault().LongID;

                    long? FromBuchtID = _buchtNos.Where(t => t.Name == groupingCablePair.FromBuchtNo.ToString()).SingleOrDefault().LongID;
                    long? ToBuchtID = _buchtNos.Where(t => t.Name == groupingCablePair.ToBuchtNo.ToString()).SingleOrDefault().LongID;
                    using (TransactionScope tsRoot = new TransactionScope(TransactionScopeOption.RequiresNew, System.TimeSpan.FromMinutes(2)))
                    {
                        if (CableDB.LeaveCablePairFromBuchtForm(oldFromCablePairID ?? 0, oldToCablePairID ?? 0, false))
                        {


                            if (CableDB.AssignCablePairToBuchtForm(FromCablePairID ?? 0, ToCablePairID ?? 0, FromBuchtID ?? 0, ToBuchtID ?? 0))
                            {
                                tsRoot.Complete();
                                ShowSuccessMessage("ذخیره انجام شد");
                                if (!_groupingCablePairBackgroundWorker.IsBusy)
                                    _groupingCablePairBackgroundWorker.RunWorkerAsync();
                            }

                        }
                        else
                        {
                            MessageBox.Show("خطا در بروز رسانی");
                        }

                    }


                    if (!_groupingCablePairBackgroundWorker.IsBusy)
                        _groupingCablePairBackgroundWorker.RunWorkerAsync();


                }


            }
            catch (Exception ex)
            {
                if (!_groupingCablePairBackgroundWorker.IsBusy)
                    _groupingCablePairBackgroundWorker.RunWorkerAsync();
                ShowErrorMessage("خطا در ذخیره", ex);
            }

        }

        public void LeaveFromBucht(object sender, RoutedEventArgs e)
        {
            try
            {
                GroupingCablePair groupingCablePair = ItemsDataGrid.SelectedItem as GroupingCablePair;
                if (groupingCablePair != null && groupingCablePair.ID != 0)
                {

                    long? FromCablePairID = CablePairs.Where(t => t.Name == groupingCablePair.FromCablePairNumber.ToString()).SingleOrDefault().LongID;
                    long? ToCablePairID = CablePairs.Where(t => t.Name == groupingCablePair.ToCablePairNumber.ToString()).SingleOrDefault().LongID;

                    List<CablePair> cablePairs = Data.CablePairDB.GetCablePairFromIDToToID(FromCablePairID ?? 0, ToCablePairID ?? 0);

                    if (cablePairs.Any(t => t.CabinetInputID != null)) { MessageBox.Show("امکان آزاد سازی زوج کابل های که به کافو متصل هستند نیست!"); return; }

                    if (CableDB.LeaveCablePairFromBuchtForm(FromCablePairID ?? 0, ToCablePairID ?? 0))
                    {
                        ShowSuccessMessage("آزاد سازی انجام شد");

                    }
                }
                else
                {
                    throw new Exception();
                }

                if (!_groupingCablePairBackgroundWorker.IsBusy)
                    _groupingCablePairBackgroundWorker.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                if (!_groupingCablePairBackgroundWorker.IsBusy)
                    _groupingCablePairBackgroundWorker.RunWorkerAsync();
                ShowErrorMessage("خطا در آزاد سازی", ex);
            }

        }

        private void ItemsDataGrid_BeginningEdit_1(object sender, DataGridBeginningEditEventArgs e)
        {
            var row = (e.Row.Item as GroupingCablePair);
            if (row != null)
            {

                _columns = new ObservableCollection<CheckableItem>(DB.GetConnectionColumnInfo(row.MDFID ?? 0));
                _rows = new ObservableCollection<CheckableItem>(DB.GetConnectionRowInfo(row.VerticalCloumnID ?? 0));
                _buchtNos = new ObservableCollection<CheckableItem>(DB.GetAllConnectionBuchtInfo(row.VerticalRowID ?? 0));

            }
        }

        private void ItemsDataGrid_LoadingRow_1(object sender, DataGridRowEventArgs e)
        {


        }

        #endregion

        #region SelectionChangedComboBox

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                (this.DataContext as Cable).CenterID = (CenterComboBox.Items[0] as CenterInfo).ID;
            }
            else
            {
                if (CityComboBox.SelectedValue == null)
                {
                    City city = Data.CityDB.GetCityById(CityID);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
                else
                {
                    City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                    CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
                }
            }
        }

        private void CenterComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (CenterComboBox.SelectedValue != null)
            {
                _MDFLists = new ObservableCollection<CheckableItem>(Data.MDFDB.GetMDFCheckableByCenterID((int)CenterComboBox.SelectedValue));
            }
        }

        private void MDFComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (MDFComboBox.SelectedItem != null)
            {
                if (ItemsDataGrid.SelectedItem != null)
                {
                    (ItemsDataGrid.SelectedItem as GroupingCablePair).MDF = (MDFComboBox.SelectedItem as CheckableItem).Name;

                }

                int mdfId = (MDFComboBox.SelectedValue != null) ? Convert.ToInt32(MDFComboBox.SelectedValue) : 0;
                _columns = new ObservableCollection<CheckableItem>(DB.GetConnectionColumnInfo(mdfId));

                //milad doran
                //_columns = new ObservableCollection<CheckableItem>(DB.GetConnectionColumnInfo((int)(MDFComboBox.SelectedValue ?? 0)));

            }
        }

        private void ColumnComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (ColumnComboBox.SelectedValue != null)
            {
                if (ItemsDataGrid.SelectedItem != null)
                {
                    (ItemsDataGrid.SelectedItem as GroupingCablePair).VerticalCloumnNo = _columns.Where(t => t.ID == (int)ColumnComboBox.SelectedValue).SingleOrDefault().Name;
                }

                int verticalMdfColumnId = Convert.ToInt32(ColumnComboBox.SelectedValue);
                _rows = new ObservableCollection<CheckableItem>(DB.GetConnectionRowInfo(verticalMdfColumnId));

                //milad doran
                //_rows = new ObservableCollection<CheckableItem>(DB.GetConnectionRowInfo((int)ColumnComboBox.SelectedValue));

            }
        }

        private void RowComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (RowComboBox.SelectedItem != null)
            {
                if (ItemsDataGrid.SelectedItem != null)
                {
                    (ItemsDataGrid.SelectedItem as GroupingCablePair).VerticalRowNo = (RowComboBox.SelectedItem as CheckableItem).Name;

                }

                int verticalMdfRowId = Convert.ToInt32(RowComboBox.SelectedValue);
                _buchtNos = new ObservableCollection<CheckableItem>(DB.GetAllConnectionBuchtInfo(verticalMdfRowId));

                //milad doran
                //_buchtNos = new ObservableCollection<CheckableItem>(DB.GetAllConnectionBuchtInfo((int)RowComboBox.SelectedValue));
            }
        }

        private void FromConnectionComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ToConnectionComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        #endregion SelectionChangedComboBox

        #region LoadComboBox
        ComboBox MDFComboBox = new ComboBox();
        private void MDFComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            MDFComboBox = sender as ComboBox;
        }
        ComboBox RowComboBox = new ComboBox();
        private void RowComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            RowComboBox = sender as ComboBox;
        }
        ComboBox ColumnComboBox = new ComboBox();
        private void ColumnComboBox_Loaded_1(object sender, RoutedEventArgs e)
        {
            ColumnComboBox = sender as ComboBox;
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
        #endregion

    }
}
