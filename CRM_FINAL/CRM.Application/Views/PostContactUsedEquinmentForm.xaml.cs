using CRM.Data;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for PostContactUsedEquinmentForm.xaml
    /// </summary>
    public partial class PostContactUsedEquinmentForm : Local.PopupWindow
    {
        private CRM.Data.PostContact _postContact { get; set; }

        public PostContactUsedEquinmentForm()
        {
            InitializeComponent();
        }
        public PostContactUsedEquinmentForm(long postContactID ):this()
        {
            _postContact = Data.PostContactDB.GetPostContactByID(postContactID);
            Initialize();
        }

        private void Initialize()
        {
            FirstCableColorComboBox.ItemsSource = SecondCableColorComboBox.ItemsSource = Data.CableColorDB.GetCheckableItem();
            UsedProductsComboBox.ItemsSource = Data.UsedProductsDB.GetCheckableItem();
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            FirstCableColorComboBox.SelectedValue = _postContact.FirsetCableColorID;
            SecondCableColorComboBox.SelectedValue = _postContact.SecondCableColorID;


            LoadData();

        }

        private void LoadData()
        {
            UsedProductDataGrid.ItemsSource = Data.PostContactEquipmentDB.GetPostContactEquipmentByID(_postContact.ID);
        }
        private void UsedProductDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (UsedProductDataGrid.SelectedIndex < 0 || UsedProductDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;
            try
            {

                using(TransactionScope ts = new TransactionScope())
                {
                _postContact.FirsetCableColorID = (int)FirstCableColorComboBox.SelectedValue;
                _postContact.SecondCableColorID = (int)SecondCableColorComboBox.SelectedValue;
                _postContact.Detach();
                DB.Save(_postContact);

                PostContactEquipment postContactEquipment = UsedProductDataGrid.SelectedItem as PostContactEquipment;
                if (postContactEquipment != null)
                {
                    postContactEquipment.PostContactID = _postContact.ID;
                    postContactEquipment.InsertDate = DB.GetServerDate();
                    postContactEquipment.Detach();
                    DB.Save(postContactEquipment);
                }
                    ts.Complete();
                }
                
            }
            catch(Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات" , ex);
            }
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {

                _postContact.FirsetCableColorID = (int)FirstCableColorComboBox.SelectedValue;
                _postContact.SecondCableColorID = (int)SecondCableColorComboBox.SelectedValue;
                _postContact.Detach();
                DB.Save(_postContact);
                ShowSuccessMessage("ذخیره رنگ کابل انجام شد.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (UsedProductDataGrid.SelectedIndex < 0 || UsedProductDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;
            try
            {
                PostContactEquipment postContactEquipment = UsedProductDataGrid.SelectedItem as PostContactEquipment;
                DB.Delete<Data.PostContactEquipment>(postContactEquipment.ID);
                LoadData();
                ShowSuccessMessage("حذف انجام شد");
            }
            catch(Exception ex)
            {
                ShowErrorMessage("خطا در حذف" , ex);
            }
        }

    }
}
