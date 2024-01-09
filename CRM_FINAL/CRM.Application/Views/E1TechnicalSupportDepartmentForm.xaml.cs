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
    /// Interaction logic for E1TechnicalSupportDepartmentForm.xaml
    /// </summary>
    public partial class E1TechnicalSupportDepartmentForm : Local.RequestFormBase
    {

        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }

        long _requestID = 0;
        Request _request { get; set; }
        CRM.Data.E1 _e1 { get; set; }

        private long? _subID;

        E1Link _e1Link { get; set; }

        public E1TechnicalSupportDepartmentForm()
        {
            InitializeComponent();
        }

        public E1TechnicalSupportDepartmentForm(long requstID, long? subID)
            : this()
        {
            _subID = subID;
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


            _requestInfoSummary = new RequestInfoSummary(_request.ID);
            _requestInfoSummary.RequestInfoExpander.IsExpanded = true;
            RequestInfoUC.Content = _requestInfoSummary;
            RequestInfoUC.DataContext = _requestInfoSummary;

            _E1InfoSummary = new E1InfoSummary(_requestID , _subID);
            _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
            E1InfoSummaryUC.Content = _E1InfoSummary;
            E1InfoSummaryUC.DataContext = _E1InfoSummary;

            ModemTypeComboBox.ItemsSource = Data.E1ModemDB.GetE1ModemCheckableItem();

            if (_subID != null)
                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };
            else
                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {

            _e1 = Data.E1DB.GetE1ByRequestID(_request.ID);
            if (_subID != null)
            {
                _e1Link = Data.E1LinkDB.GetE1LinkByID(_subID ?? 0);
                StatusComboBox.Visibility = Visibility.Collapsed;
                StatusLabel.Visibility = Visibility.Collapsed;
                this.DataContext = _e1Link;
            }
            else
            {
                StatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
                StatusComboBox.SelectedValue = this.currentStat;

                DescriptionLabel.Visibility = Visibility.Collapsed;
                DescriptionTextBox.Visibility = Visibility.Collapsed;

                ModemTypeLabel.Visibility = Visibility.Collapsed;
                ModemTypeComboBox.Visibility = Visibility.Collapsed;

                return;
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
                using (TransactionScope ts = new TransactionScope())
                {
                    if (_subID != null)
                    {
                        _e1Link = this.DataContext as CRM.Data.E1Link;
                        _e1Link.Detach();
                        DB.Save(_e1Link, false);

                        ts.Complete();
                        IsSaveSuccess = true;
                    }
                    else
                    {
                        _request.StatusID = (int)StatusComboBox.SelectedValue;
                        _request.Detach();
                        DB.Save(_request);

                        ts.Complete();
                        IsSaveSuccess = true;
                    }
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
            if (Data.E1LinkDB.CheckALLTechSupportDeparteman(_e1.RequestID))
            {
                Save();
                if (IsSaveSuccess == true)
                    IsForwardSuccess = true;
            }
            else
            {
                Folder.MessageBox.ShowError("همه لینک ها بررسی امکانات نشده اند");
                IsForwardSuccess = false;
            }

            return IsForwardSuccess;
        }

        public override bool Deny()
        {
            using (TransactionScope Subts = new TransactionScope(TransactionScopeOption.Required))
            {
                _request.StatusID = (int)StatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                _e1 = this.DataContext as CRM.Data.E1;
                _e1.ModemTypeID = null;
                _e1.Detach();
                DB.Save(_e1, false);

                Subts.Complete();
                IsRejectSuccess = true;
            }

            return IsRejectSuccess;
        }

    }
}
