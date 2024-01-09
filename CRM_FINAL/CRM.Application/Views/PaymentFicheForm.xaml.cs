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
    public partial class PaymentFicheForm : Local.PopupWindow
    {

        private long _ID = 0;
        private long _installmentID = 0;
        PaymentFiche paymentFiche { get; set; }
        public PaymentFicheForm()
        {
            InitializeComponent();
            Initialize();
        }

        public PaymentFicheForm(long id , long installmentID)
            : this()
        {
            
            _ID = id;
            _installmentID = installmentID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Initialize()
        {
    
            FicheTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.FicheType));
            BankBranchComboBox.ItemsSource = Data.BankBranchDB.GetBankBranchCheckable();
            paymentFiche = new PaymentFiche();
            paymentFiche.IssueDate = DB.GetServerDate();
           
        
        }

        private void LoadData()
        {
          
            if (_ID == 0)
            {
                Initialize();
                SaveButton.Content = "ذخیره";
                
            }
            else
            {
                SaveButton.Content = "بروزرسانی";
                paymentFiche = DB.GetEntitybyID<PaymentFiche>(_ID);
            }

            this.DataContext = paymentFiche;

        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            try
            {
                PaymentFiche item = this.DataContext as PaymentFiche;
                using(MainDataContext context = new MainDataContext())
                {
                    item.InsertDate =DB.GetServerDate();
                    item.InstallmentID = _installmentID;
                }
                item.Detach();
                Save(item);
                ShowSuccessMessage("فیش ذخیره شد");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره فیش", ex);
            }
        }
    }
}
