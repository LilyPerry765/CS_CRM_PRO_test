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
using CRM.Application.UserControls;
using CRM.Data;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for LinsemanForm.xaml
    /// </summary>
    public partial class LinsemanForm : Local.RequestFormBase
    {
        long _RequestID = 0;
        Request request = new Request();
        private UserControls.CustomerAddressUserControl _CustomerAddressUserControl;
        public LinsemanForm()
        {
            InitializeComponent();

        }
        public LinsemanForm(long requestID):this()
        {
            this.RequestID = _RequestID = requestID;
            
            Initialize();
            
        }

        private void Initialize()
        {
           

           request = Data.RequestDB.GetRequestByID( _RequestID);
           CabinetAndPostUserControl.CenterID = request.CenterID;
           CabinetColumn.ItemsSource = Data.CabinetDB.GetCabinetCheckable();
           PostColumn.ItemsSource = Data.PostDB.GetPostCheckable();
           _CustomerAddressUserControl = new CustomerAddressUserControl(_RequestID);
      
        }

        public void LoadData()
        {
           
            CabinetAndPostUserControl.LoadData();
            StatusComboBox.ItemsSource = DB.GetStepStatus((int)DB.RequestType.ChangeLocationCenterInside, this.currentStep);
            ItemsDataGrid.ItemsSource = Data.LinesmanDB.GetPostByChangeLocationByID(_RequestID);
            StatusComboBox.SelectedValue = request.StatusID;
            _CustomerAddressUserControl = new UserControls.CustomerAddressUserControl(_RequestID);
            this._CustomerAddressUserControl.DataContext = _CustomerAddressUserControl;
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit};
        }
        private void DeleteItem(object sender, RoutedEventArgs e)
        {

            if (ItemsDataGrid.SelectedIndex < 0 || ItemsDataGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;

            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    CRM.Data.AssignmentDB.NearestTelephonInfo item = ItemsDataGrid.SelectedItem as CRM.Data.AssignmentDB.NearestTelephonInfo;

                    DB.Delete<Data.Linesman>(item.LinemanID);
                    ShowSuccessMessage("پست مورد نظر حذف شد");
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف پست", ex);
            }
        }

        public override bool Forward()
        {
            Save();
            if (IsSaveSuccess == true)
                IsForwardSuccess = true;
            return IsForwardSuccess;
        }
        public override bool Save()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return false;
            }
            try
            {
                if (CabinetAndPostUserControl.CabinetComboBox.SelectedValue != null && CabinetAndPostUserControl.PostComboBox.SelectedValue != null)
                {
                    CRM.Data.Linesman linesman = new Data.Linesman();
                    linesman.ChangeLocationID = _RequestID;
                    linesman.CabinetID = (int)CabinetAndPostUserControl.CabinetComboBox.SelectedValue;
                    linesman.PostID = (int)CabinetAndPostUserControl.PostComboBox.SelectedValue;
                    linesman.Detach();
                    Save(linesman);
                    LoadData();

                }

                if (StatusComboBox.SelectedValue != null)
                {
                    request.StatusID = (int)StatusComboBox.SelectedValue;
                    request.Detach();
                    Save(request);
                }
                CabinetAndPostUserControl.CabinetComboBox.SelectedIndex = -1;
                CabinetAndPostUserControl.PostComboBox.SelectedIndex = -1;
                ShowSuccessMessage("ذخیره انجام شد");
                IsSaveSuccess = true;
            }
            catch(Exception ex)
            {
               
                ShowErrorMessage("ذخیره انجام نشد",ex);
            }
            return IsSaveSuccess;
        }


        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }


    }
}
