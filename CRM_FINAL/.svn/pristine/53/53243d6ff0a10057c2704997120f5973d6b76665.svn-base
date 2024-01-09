using CRM.Application.UserControls;
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
    /// Interaction logic for E1MicrowavesForm.xaml
    /// </summary>
    public partial class E1MicrowavesForm : Local.RequestFormBase
    {
        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }

        long _requestID = 0;

        Request _request { get; set; }
        CRM.Data.E1 _e1 { get; set; }


        public E1MicrowavesForm()
        {
            InitializeComponent();
        }

        public E1MicrowavesForm(long requstID)
        {
            InitializeComponent();
            base.RequestID = this._requestID = requstID;
            Initialize();
        }

        private void Initialize()
        {
            _request = Data.RequestDB.GetRequestByID(_requestID);

            _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
            _customerInfoSummary.IsExpandedProperty = true;
            _customerInfoSummary.Mode = true;
            CustomerInfoUC.Content = _customerInfoSummary;
            CustomerInfoUC.DataContext = _customerInfoSummary;

            _E1InfoSummary = new E1InfoSummary(_requestID , null);
            _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
            E1InfoSummaryUC.Content = _E1InfoSummary;
            E1InfoSummaryUC.DataContext = _E1InfoSummary;
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {

            _e1 = Data.E1DB.GetE1ByRequestID(_requestID);


            StatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = this.currentStat;




            if (_e1 != null)
            {
                this.DataContext = _e1;
            }
            else
            {
                Folder.MessageBox.ShowInfo("اطلاعات درخواست E1 یافت نشد.");
            }
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
                _e1 = this.DataContext as CRM.Data.E1;

                string pattern = "[0-9]*[-][0-9]*[-][0-9]*";

                if (!System.Text.RegularExpressions.Regex.IsMatch(_e1.DDFMacrowaveAdress, pattern))
                    throw new Exception("اتصالی DDF رابط سالن صحیح نمی باشد");

                if (!System.Text.RegularExpressions.Regex.IsMatch(_e1.TargetDDF, pattern))
                    throw new Exception("اتصالی DDF مقصد صحیح نمی باشد");


                using (TransactionScope ts = new TransactionScope())
                {
                    _request.StatusID = (int)StatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request);

                    _e1.Detach();
                    DB.Save(_e1, false);

                    ts.Complete();
                    IsSaveSuccess = true;
                }
                ShowSuccessMessage("دخیره اطلاعات انجام شد.");
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
            return IsSaveSuccess;
        }
        public override bool Forward()
        {
            Save();
            if (IsSaveSuccess == true)
                IsForwardSuccess = true;
            return IsForwardSuccess;
        }
    }
}
