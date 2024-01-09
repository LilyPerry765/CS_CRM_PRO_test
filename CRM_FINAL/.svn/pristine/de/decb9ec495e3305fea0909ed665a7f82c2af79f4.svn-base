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

namespace CRM.Application.Views
{
    public partial class RoundSaleInfoForm : Local.PopupWindow
    {
        #region Properties

        private long _ID = 0;
        private int CityID = 0;
        List<CheckableObject> roundTelephoneList = new List<CheckableObject>();
        List<TelRoundSale> TelRoundSaleList = new List<TelRoundSale>();
        List<TelRoundSale> oldTelRoundSaleList = new List<TelRoundSale>();

        #endregion

        #region Constructors

        public RoundSaleInfoForm()
        {
            InitializeComponent();
            Initialize();
        }

        public RoundSaleInfoForm(long id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            RoundTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.RoundType));
            RoundTypeColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.RoundType));
            StatusColumn.ItemsSource = Helper.GetEnumItem(typeof(DB.TelephoneStatus));
            CityComboBox.ItemsSource = Data.CityDB.GetAvailableCityCheckable();

        }

        private void LoadData()
        {
            RoundSaleInfo item = new RoundSaleInfo();
            item.EntryDate = item.StartDate = DB.GetServerDate();


            if (_ID == 0)
            {
                SaveButton.Content = "ذخیره";

            }
            else
            {
                CityComboBox.IsEnabled = false;
                CenterComboBox.IsEnabled = false;

                item = Data.RoundSaleInfoDB.GetRoundSaleInfoByID(_ID);
                roundTelephoneList  = Data.TelephoneDB.GetRoundTelephone((byte)item.RoundType, item.CenterID);
                oldTelRoundSaleList = Data.TelRoundSaleDB.GetTelRoundSaleByRoundSaleInfoID(_ID);
                roundTelephoneList.Where(t => oldTelRoundSaleList.Select(t2 => t2.TelephoneNo).Contains((t.Object as Telephone).TelephoneNo) && (t.Object as Telephone).InRoundSale == true).ToList().ForEach(t => t.IsChecked = true);
                SaveButton.Content = "بروزرسانی";


                CityID = Data.CityDB.GetCityByCenterID(item.CenterID).ID;

                if (CityID == 0)
                    CityComboBox.SelectedIndex = 0;

                else
                    CityComboBox.SelectedValue = CityID;
            }

            this.DataContext = item;
            RoundTelephonDataGrid.ItemsSource = roundTelephoneList;

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

            RoundSaleInfo item = this.DataContext as RoundSaleInfo;
            if (_ID == 0)
            {

                try
                {
                    if (item.BasePrice == null || item.BasePrice == 0)
                        throw new Exception("لطفا قیمت پایه مزایده را وارد کنید ");

                    item.Detach();
                    DB.Save(item);

                    List<CheckableObject> roundTelephoneList = RoundTelephonDataGrid.ItemsSource as System.Collections.Generic.List<CRM.Data.CheckableObject>;
                    List<TelRoundSale> TelRoundSaleList = new List<TelRoundSale>();
                    List<Telephone> telephons = new List<Telephone>();
                    roundTelephoneList.Where(t => t.IsChecked == true).ToList().ForEach(t =>
                    {

                        TelRoundSale telRoundSale = new TelRoundSale();

                        Telephone tel = t.Object as Telephone;
                        tel.InRoundSale = true;
                        tel.Detach();
                        telephons.Add(tel);

                        telRoundSale.ID = 0;
                        telRoundSale.TelephoneNo = tel.TelephoneNo;
                        telRoundSale.RoundSaleInfoID = item.ID;
                        telRoundSale.SaleStatus = (byte)DB.TelRoundSaleStatus.InSale;
                        telRoundSale.IsActive = true;
                        telRoundSale.Detach();
                        TelRoundSaleList.Add(telRoundSale);

                    });
                    DB.SaveAll(TelRoundSaleList);
                    DB.UpdateAll(telephons);

                    ShowSuccessMessage("مزایده تلفن رند ذخیره شد");
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage("خطا در ذخیره مزایده تلفن رند", ex);
                }
            }
            else
            {
                if (item.BasePrice == null || item.BasePrice == 0)
                    throw new Exception("لطفا قیمت پایه مزایده را وارد کنید ");
                item.Detach();
                DB.Save(item);

                List<CheckableObject> roundTelephoneList = RoundTelephonDataGrid.ItemsSource as System.Collections.Generic.List<CRM.Data.CheckableObject>;
        
                List<Telephone> telephons = new List<Telephone>();

                List<Telephone> InstallTelephon = roundTelephoneList.Where(t2 => t2.IsChecked == true).Select(t2 => (t2.Object as Telephone)).Where(t => !oldTelRoundSaleList.Where(t2 => t2.SaleStatus == (byte)DB.TelRoundSaleStatus.InSale).Select(t2 => t2.TelephoneNo).Contains(t.TelephoneNo)).ToList();
                List<TelRoundSale> UnistallTelephone = oldTelRoundSaleList.Where(t2 => t2.SaleStatus == (byte)DB.TelRoundSaleStatus.InSale).Where(t => !roundTelephoneList.Where(t2 => t2.IsChecked == true).Select(t2 => (t2.Object as Telephone).TelephoneNo).Contains(t.TelephoneNo)).ToList();

                roundTelephoneList.Where(t => UnistallTelephone.Select(t2 => t2.TelephoneNo).Contains((t.Object as Telephone).TelephoneNo)).ToList().ForEach
                    (t =>
                      {
                       Telephone tel = t.Object as Telephone;
                       tel.InRoundSale = false;
                       tel.Detach();
                       telephons.Add(tel);
                      });

                DB.UpdateAll(telephons);

                DB.DeleteAll<Data.TelRoundSale>(UnistallTelephone.Select(t=>t.ID).ToList());

                List<TelRoundSale> TelRoundSaleList = new List<TelRoundSale>();
                telephons.Clear();
                roundTelephoneList.Where(t => InstallTelephon.Select(t2 => t2.TelephoneNo).Contains((t.Object as Telephone).TelephoneNo)).ToList().ForEach
                      (t =>
                      {
                          TelRoundSale telRoundSale = new TelRoundSale();

                          Telephone tel = t.Object as Telephone;
                          tel.InRoundSale = true;
                          tel.Detach();
                          telephons.Add(tel);

                          telRoundSale.ID = 0;
                          telRoundSale.TelephoneNo = tel.TelephoneNo;
                          telRoundSale.RoundSaleInfoID = item.ID;
                          telRoundSale.SaleStatus = (byte)DB.TelRoundSaleStatus.InSale;
                          telRoundSale.IsActive = true;
                          telRoundSale.Detach();
                          TelRoundSaleList.Add(telRoundSale);
                      });
                DB.SaveAll(TelRoundSaleList);
                DB.UpdateAll(telephons);

                //List<long> DelTelRoundSaleList = new List<long>();
                //bool flag = false;
                //for (int i = 0; i < roundTelephoneList.Count; i++)
                //{
                //    flag = false;
                //    if (oldTelRoundSaleList.Count != 0)
                //    {
                //        foreach (TelRoundSale telRoundSale in oldTelRoundSaleList)
                //        {
                //            Telephone tel = roundTelephoneList[i].Object as Telephone;
                //            if (tel.TelephoneNo == telRoundSale.TelephoneNo && roundTelephoneList[i].IsChecked == false)
                //            {
                //                DelTelRoundSaleList.Add(telRoundSale.ID);
                //                break;
                //            }
                //            if (tel.TelephoneNo == telRoundSale.TelephoneNo && roundTelephoneList[i].IsChecked == true)
                //            {
                //                flag = false;
                //                break;
                //            }
                //            if (tel.TelephoneNo != telRoundSale.TelephoneNo && roundTelephoneList[i].IsChecked == true)
                //            {
                //                flag = true;
                //            }
                //        }
                //        if (flag == true)
                //        {
                //            TelRoundSale newTelRoundSale = new TelRoundSale();
                //            Telephone newTel = roundTelephoneList[i].Object as Telephone;
                //            newTelRoundSale.ID = 0;
                //            newTelRoundSale.TelephoneNo = newTel.TelephoneNo;
                //            newTelRoundSale.RoundSaleInfoID = item.ID;
                //            newTelRoundSale.SaleStatus = 0;
                //            newTelRoundSale.IsActive = true;
                //            newTelRoundSale.Detach();
                //            TelRoundSaleList.Add(newTelRoundSale);
                //        }
                //    }
                //    else
                //    {

                //            if (roundTelephoneList[i].IsChecked == true)
                //            {
                //                TelRoundSale telRoundSale = new TelRoundSale();
                //                Telephone tel = roundTelephoneList[i].Object as Telephone;
                //                telRoundSale.ID = 0;
                //                telRoundSale.TelephoneNo = tel.TelephoneNo;
                //                telRoundSale.RoundSaleInfoID = item.ID;
                //                telRoundSale.SaleStatus = 0;
                //                telRoundSale.IsActive = true;
                //                telRoundSale.Detach();
                //                TelRoundSaleList.Add(telRoundSale);
                //            }

                //    }
                //}
                //if (DelTelRoundSaleList.Count != 0)
                //    DB.DeleteAll<Data.TelRoundSale>(DelTelRoundSaleList);
                //if(TelRoundSaleList.Count != 0)
                //  DB.SaveAll(TelRoundSaleList);
                LoadData();
            }
        }

        private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        private void RoundTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (RoundTypeComboBox.SelectedValue != null && CenterComboBox.SelectedValue != null)
            {
                roundTelephoneList = Data.TelephoneDB.GetRoundTelephone((byte)RoundTypeComboBox.SelectedValue, (int)CenterComboBox.SelectedValue);
                foreach (TelRoundSale telRoundSale in oldTelRoundSaleList)
                {
                    for (int i = 0; i < roundTelephoneList.Count; i++)
                    {
                        Telephone tel = roundTelephoneList[i].Object as Telephone;
                        if (tel.TelephoneNo == telRoundSale.TelephoneNo)
                        {
                            roundTelephoneList[i].IsChecked = true;
                        }

                    }
                }
                RoundTelephonDataGrid.ItemsSource = roundTelephoneList;

            }
        }

        #endregion

        private void CityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CityID == 0)
            {
                City city = Data.CityDB.GetCityById((int)CityComboBox.SelectedValue);
                CenterComboBox.ItemsSource = Data.CenterDB.GetCenterByCityId(city.ID);
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

        public void ApplyFilters()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(RoundTelephonDataGrid.ItemsSource);
            if (view != null)
            {
                // view.Filter = new System.Predicate<Object>(o => ((YourClass)o).Property1.Contains(SearchText1));
                view.Filter = new Predicate<object>(PredicateFilters);
            }
        }

        private bool PredicateFilters(object obj)
        {
            CheckableObject checkableObject = obj as CheckableObject;
            //Return members whose Orders have not been filled 
            return ((checkableObject.Object as Telephone).TelephoneNo.ToString().Contains(FilterTelephonNoTextBox.Text.Trim()));
        }

        private void FilterTelephonNoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

    }
}
