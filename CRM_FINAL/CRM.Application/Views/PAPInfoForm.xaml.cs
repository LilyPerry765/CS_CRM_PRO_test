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

namespace CRM.Application.Views
{
    public partial class PAPInfoForm : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        public static PAPInfo[] relatedPAPlist { get; set; }
        public PAPInfo pap { get; set; }

        #endregion

        #region Constructors

        public PAPInfoForm()
        {
            InitializeComponent();
            Initialize();
            
            pap = new PAPInfo();           
        }

        public PAPInfoForm(int id)
            : this()
        {
            _ID = id;
        }

        #endregion

        #region Methods

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {
            //CityColumn.ItemsSource = CityDB.GetAvailableCityCheckable();
            OperatingStatusComboBox.ItemsSource = DB.GetAllEntity<Data.PAPInfoOperatingStatus>();
        }

        private void LoadData()
        {
            PAPInfo item = new PAPInfo();

            if (_ID == 0)
            {
                item.LoginStatus = true;
                item.OperatingStatusID = 1;
                //ItemsDataGrid.IsEnabled = false;

                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Data.PAPInfoDB.GetPAPInfoByID(_ID);
                //ItemsDataGrid.ItemsSource = PAPInfoLimitationDB.SearchPAPInfoLimitation(item.ID);

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }

        protected static ObservableCollection<PAPInfo> GetContractNo(string Pattern)
        {
            return new ObservableCollection<PAPInfo>(

                relatedPAPlist.Where((pap, match) => pap.ID.ToString().Contains(Pattern.ToLower()) ||
                                      pap.Title.ToString().Contains(Pattern.ToLower()))
                );
        }

        #endregion

        #region Event Handlers

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                PAPInfo item = this.DataContext as PAPInfo;

                item.Detach();
                Save(item);

                //ItemsDataGrid.IsEnabled = true;

                ShowSuccessMessage("شرکت PAP ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره شرکت PAP", ex);
            }
        }

        //private void ItemsDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        //{
        //    try
        //    {
        //        PAPInfoLimitation item = e.Row.Item as PAPInfoLimitation;
        //        item.PAPInfoID = _ID;

        //        item.Detach();
        //        DB.Save(item);

        //        LoadData();

        //        ShowSuccessMessage("ذخیره شد");
        //    }
        //    catch (Exception ex)
        //    {
        //        e.Cancel = true;
        //        ShowErrorMessage("خطا در ذخیره ", ex);
        //    }
        //}

        //private void DeleteItem(object sender, RoutedEventArgs e)
        //{
        //    if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

        //    try
        //    {
        //        MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

        //        if (result == MessageBoxResult.Yes)
        //        {
        //            PAPInfoLimitation item = ItemsDataGrid.SelectedItem as PAPInfoLimitation;

        //            DB.Delete<PAPInfoLimitation>(item.ID);
        //            ShowSuccessMessage("مورد نظر حذف شد");
        //            LoadData();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowErrorMessage("خطا در حذف", ex);
        //    }
        //}

        #endregion
    }
}
