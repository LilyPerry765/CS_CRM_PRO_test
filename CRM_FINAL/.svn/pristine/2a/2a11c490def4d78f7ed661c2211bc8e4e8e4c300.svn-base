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
using CRM.Data;

namespace CRM.Application.Views
{
    public partial class Failure117NetworkContractorFrom : Local.PopupWindow
    {
        #region Properties

        private int _ID = 0;
        private static List<CheckableItem> _CenterIDs;

        #endregion

        #region Constructors

        public Failure117NetworkContractorFrom()
        {
            InitializeComponent();
            Initialize();
        }

        public Failure117NetworkContractorFrom(int id)
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
            CenterListBox.ItemsSource = _CenterIDs = Data.CenterDB.GetCenterCheckable();
        }

        private void LoadData()
        {
            Failure117NetworkContractor item = new Failure117NetworkContractor();

            if (_ID == 0)
            {
                ItemsDataGrid.ItemsSource = new List<Failure117NetworkContractorOfficer>();

                SaveButton.Content = "ذخیره";
            }
            else
            {
                item = Failure117NetworkContractorDB.GetContractorById(_ID);
                ItemsDataGrid.ItemsSource = Failure117NetworkContractorDB.SearchContractorOfficer(item.ID);

                CenterListBox.ItemsSource = null;
                List<Failure117NetworkContractorCenter> contractorCenter = Failure117NetworkContractorDB.GetContractorCenterByContractorId(item.ID);
                foreach (Failure117NetworkContractorCenter center in contractorCenter)
                {
                    _CenterIDs.Where(t => (int)t.ID == center.CenterID).SingleOrDefault().IsChecked = true;
                }
                CenterListBox.ItemsSource = _CenterIDs;

                SaveButton.Content = "بروزرسانی";
            }

            this.DataContext = item;
        }

        #endregion

        #region Event Handlers

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                List<int> selectedCenterIds = CenterListBox.Items.Cast<CheckableItem>().ToList().Where(t => t.IsChecked == true).Select(t => (int)t.ID).ToList();

                Failure117NetworkContractor item = this.DataContext as Failure117NetworkContractor;

                if (string.IsNullOrEmpty(item.Title))
                    throw new Exception("لطفا نام شرکت پیمانکار را وارد نمایید");

                item.Detach();
                Save(item);

                using (MainDataContext context = new MainDataContext())
                {
                    context.ExecuteCommand("DELETE FROM Failure117NetworkContractorCenter WHERE ContractorID = {0}", item.ID);

                    List<Failure117NetworkContractorCenter> contractorCenter = new List<Failure117NetworkContractorCenter>();
                    foreach (int centerID in selectedCenterIds)
                    {
                        contractorCenter.Add(new Failure117NetworkContractorCenter
                        {
                            ContractorID = item.ID,
                            CenterID = centerID
                        });
                    }
                    context.Failure117NetworkContractorCenters.InsertAllOnSubmit(contractorCenter);

                    context.SubmitChanges();
                }

                List<Failure117NetworkContractorOfficer> officerList = new List<Failure117NetworkContractorOfficer>();
                foreach (Failure117NetworkContractorOfficer officer in ItemsDataGrid.ItemsSource)
                {
                    if (string.IsNullOrEmpty(officer.Name))
                        throw new Exception("لطفا نام مامور را وارد نمایید");

                    officer.ContractorID = item.ID;

                    officer.Detach();
                    DB.Save(officer);
                }

                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره شرکت پیمانکار شبکه هوایی", ex);
            }
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Failure117NetworkContractorOfficer item = ItemsDataGrid.SelectedItem as Failure117NetworkContractorOfficer;

                    DB.Delete<Failure117NetworkContractorOfficer>(item.ID);
                    ShowSuccessMessage("مامور مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف مامور", ex);
            }
        }

        #endregion
    }
}
