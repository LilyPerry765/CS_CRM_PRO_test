using CRM.Application.Local;
using CRM.Data;
using Enterprise;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections;
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
    /// صدور صورت حساب در فضا و پاور
    /// </summary>
    public partial class InvoiceIssuanceForm : RequestFormBase
    {
        #region Properties And Fields

        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }

        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }

        private UserControls.V2SpaceAndPowerInfoSummary _v2SpaceAndPowerInfoSummary { get; set; }

        long _requestID = 0;
        CRM.Data.Request _request { get; set; }
        CRM.Data.SpaceAndPower _spaceAndPower { get; set; }

        #endregion

        #region Constructor
        public InvoiceIssuanceForm()
        {
            InitializeComponent();
        }

        public InvoiceIssuanceForm(long requestID)
            : this()
        {
            base.RequestID = this._requestID = requestID;
        }

        #endregion

        #region EventHandlers

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        #endregion

        #region Methods
        public void LoadData()
        {
            _request = RequestDB.GetRequestByID(this._requestID);
            _requestInfoSummary = new UserControls.RequestInfoSummary(_requestID);
            _requestInfoSummary.RequestInfoExpander.IsExpanded = true;
            RequestInfoSummaryUC.Content = _requestInfoSummary;
            RequestInfoSummaryUC.DataContext = _requestInfoSummary;

            _customerInfoSummary = new UserControls.CustomerInfoSummary(_request.CustomerID);
            _customerInfoSummary.IsExpandedProperty = true;
            _customerInfoSummary.Mode = true;
            CustomerInfoSummaryUC.Content = _customerInfoSummary;
            CustomerInfoSummaryUC.DataContext = _customerInfoSummary;

            CRM.Data.Status status = Data.StatusDB.GetStatueByStatusID(_request.StatusID);

            switch (_request.RequestTypeID)
            {
                case (byte)DB.RequestType.SpaceandPower:
                    {
                        //بازیابی رکورد فضا و پاور
                        _spaceAndPower = SpaceAndPowerDB.GetSpaceAndPowerByRequestId(_requestID);

                        //مقداردهی عملیات های مربوطه - با توجه به چرخه کاری
                        this.ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm, (byte)DB.NewAction.Exit };

                        //پر کردن کنترل های مربوط به اطلاعات رکورد فضا و پاور
                        _v2SpaceAndPowerInfoSummary = new UserControls.V2SpaceAndPowerInfoSummary(_requestID);
                        _v2SpaceAndPowerInfoSummary.SpaceAndPowerInfoSummaryExpander.IsExpanded = true;
                        V2SpaceAndPowerInfoSummaryUC.Content = _v2SpaceAndPowerInfoSummary;
                        V2SpaceAndPowerInfoSummaryUC.DataContext = _v2SpaceAndPowerInfoSummary;

                        this.DataContext = _spaceAndPower;

                        //توضیحات اداره نظارت تجهیزات مخابراتی
                        DescriptionTextBox.Text = _spaceAndPower.SooratHesabComment;

                        //تاریخ
                        InvoiceIssuanceDatePicker.SelectedDate = _spaceAndPower.SooratHesabDate;

                    }
                    break;
            }
        }

        public override bool Confirm()
        {
            if (!Codes.Validation.WindowIsValid.IsValid(this))
            {
                IsSaveSuccess = false;
                return IsSaveSuccess;
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    switch (_request.RequestTypeID)
                    {
                        case (byte)DB.RequestType.SpaceandPower:
                            {
                                _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                                _spaceAndPower.SooratHesabComment = DescriptionTextBox.Text;
                                _spaceAndPower.SooratHesabDate = (InvoiceIssuanceDatePicker.SelectedDate.HasValue) ? InvoiceIssuanceDatePicker.SelectedDate : DB.GetServerDate();
                                _spaceAndPower.Detach();
                                DB.Save(_spaceAndPower, false);
                            }
                            break;
                    }

                    scope.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage(".تایید انجام شد");
                }
            }
            catch (Exception ex)
            {
                IsSaveSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);
            }
            return IsSaveSuccess;
        }

        #endregion
    }
}
