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
    /// <summary>
    /// Interaction logic for InstallmentForm.xaml
    /// </summary>
    public partial class InstallmentForm : Local.PopupWindow 
    {
        private long _ID = 0;

        // Flag = 0 mode Edit , Flag = 1 mode insert
        private int _Flag = 0;
        public InstallmentForm()
        {
            InitializeComponent();
            Initialize();
        }
        public InstallmentForm(long id , int flag):this()
        {
            _ID = id;
            _Flag = flag;
            Initialize();
        }

        private void Initialize()
        {
               BaseCostComboBox.ItemsSource=Data.BaseCostDB.GetBaseCostCheckable();
               OtherCostComboBox.ItemsSource=Data.OtherCostDB.GetOtherCostCheckable();
               //FicheType.ItemsSource = Helper.GetEnumCheckable(typeof(DB.FicheType));
               FicheType.ItemsSource = Helper.GetEnumItem(typeof(DB.FicheType));
               BankBranchColumn.ItemsSource = Data.BankBranchDB.GetBankBranchCheckable();
        }
        private void LoadData()
        {
            Installment installment = new Installment();
            if (_Flag == 0)
            {
                SetValuesOFOtherCostComboBoxAndBaseCostComboBox();
                List<PaymentFiche> pamentFiche = new List<PaymentFiche>();
                if (_Flag == 1)
                {
                    SaveButton.Content = "ذخیره";
                }
                else
                {
                    try
                    {
                        installment = DB.GetEntitybyID<Installment>(_ID);
                        pamentFiche = Data.PaymentFicheDB.GetPamentFicheByID(installment.ID);
                    }
                    catch { }
                    SaveButton.Content = "بروز رسانی";
                }


                this.InstallmentGroupBox.DataContext = installment;
                this.PaymentFicheGrid.DataContext = pamentFiche;
            }
            else if (_Flag == 1)
            {
                //this.InstallmentGroupBox.DataContext = installment;
                PaymentFicheGroupBox.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (OtherCostComboBox.SelectedIndex == -1 && BaseCostComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("نوع هزینه را انتخاب کنید");
              
            }
            else
            {

                if (_Flag == 0)
                {
                    try
                    {
                        Installment installment = this.InstallmentGroupBox.DataContext as Installment;
                        installment.Detach();
                        installment.ID = _ID;
                        Save(installment);
                        ShowSuccessMessage("تقسیط ذخیره شد");
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("خطا در ذخیره تقسیط", ex);
                    }
                }
                else if (_Flag == 1)
                {
                    // TO DO : Due to Existence ID can not using Function Save()
                    Installment installment = new Installment();
                    installment.ID = _ID;
                    installment.InsertDate = DB.GetServerDate();
                    if (BaseCostButton.IsSelected == true)
                    {
                        installment.BaseCostID = (int)BaseCostComboBox.SelectedValue;
                        installment.OtherCostID = null;
                    }
                    else if (OtherCostButtom.IsSelected == true)
                    {
                        installment.OtherCostID = (int)OtherCostComboBox.SelectedValue;
                        installment.BaseCostID = null;
                    }
                    installment.StartDate = (DateTime)StartDate.SelectedDate;
                    installment.EndDate = (DateTime)EndDate.SelectedDate;
                    installment.PrePaymentAmount = Convert.ToInt32(PrePaymentAmountTextBox.Text);
                    installment.Amount = Convert.ToInt32(AmountTextBox.Text);
                    installment.InstallmentCount = Convert.ToInt32(InstallmentCountTextBox.Text);
                    installment.DayDuration = Convert.ToInt32(DayDurationTextBox.Text);

                    installment.Detach();
                    using (MainDataContext context = new MainDataContext())
                    {
                        context.Installments.InsertOnSubmit(installment);
                        context.SubmitChanges();
                    }

                    this.Close();
                }
               
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (PaymentFicheGrid.SelectedIndex < 0 || PaymentFicheGrid.SelectedItem.ToString() == "{NewItemPlaceholder}") return;
            try
            {
                MessageBoxResult result = MessageBox.Show("آیا از حذف مطمئن هستید؟", "تایید", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    PaymentFiche item = PaymentFicheGrid.SelectedItem as PaymentFiche;
                    DB.Delete<PaymentFiche>(item.ID);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در حذف فیش", ex);
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            if (PaymentFicheGrid.SelectedIndex >= 0)
            {
                PaymentFiche paymentFiche = PaymentFicheGrid.SelectedItem as PaymentFiche;
                if (paymentFiche == null) return;
                PaymentFicheForm window = new PaymentFicheForm(paymentFiche.ID , _ID);
                window.ShowDialog();
                if (window.DialogResult == true)
                    LoadData();

            }
        }

        private void BaseCostButton_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                OtherCostComboBox.SelectedIndex = -1;
                //SetValuesOFOtherCostComboBoxAndBaseCostComboBox();
            }
            catch { }
        }

        private void OtherCostButtom_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                BaseCostComboBox.SelectedIndex = -1;
                //SetValuesOFOtherCostComboBoxAndBaseCostComboBox();
            }
            catch { }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OtherCostForm window = new OtherCostForm();
            window.ShowDialog();
            OtherCostComboBox.ItemsSource = Data.OtherCostDB.GetOtherCostCheckable();
           

        }

        private void PaymentFicheGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

            // TO DO : بعلت تاریخ دا خل گرید اضافه انجام نمی شود 
            try
            {
                PaymentFiche item = e.Row.Item as PaymentFiche;
                item.InsertDate = DB.GetServerDate();
                item.Detach();
                Save(item);
                ShowSuccessMessage("قیش مورد نظر ذخیره شد");
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                ShowErrorMessage("خطا در ذخیره فیش", ex);
            }
        }
        /// <summary>
        /// Set defult values of OtherComboBox and BaseCostComboBOx
        /// </summary>
        private void SetValuesOFOtherCostComboBoxAndBaseCostComboBox()
        {
            Installment installmentByID = InstallmentDB.GetInstallmentByID(_ID);
            if (installmentByID.OtherCostID != null)
            {
                OtherCostButtom.IsSelected = true;
                OtherCostComboBox.SelectedValue = installmentByID.OtherCostID;
            }
            if (installmentByID.BaseCostID != null)
            {
                BaseCostButton.IsSelected = true;
                BaseCostComboBox.SelectedValue = installmentByID.BaseCostID;
            }
        }

    }
}
