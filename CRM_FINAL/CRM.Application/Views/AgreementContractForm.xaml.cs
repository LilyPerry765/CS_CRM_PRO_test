using CRM.Application.Local;
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
    /// عقد قرارداد در فضا و پاور
    /// </summary>
    public partial class AgreementContractForm : RequestFormBase
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

        public AgreementContractForm()
        {
            InitializeComponent();
        }

        public AgreementContractForm(long requestID)
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
            StatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = this.currentStat;

            switch (_request.RequestTypeID)
            {
                case (byte)DB.RequestType.SpaceandPower:
                    {
                        //بازیابی رکورد فضا و پاور
                        _spaceAndPower = SpaceAndPowerDB.GetSpaceAndPowerByRequestId(_requestID);

                        //مقداردهی عملیات های مربوطه - با توجه به چرخه کاری
                        this.ActionIDs = new List<byte> { (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };

                        //پر کردن کنترل های مربوط به اطلاعات رکورد فضا و پاور
                        _v2SpaceAndPowerInfoSummary = new UserControls.V2SpaceAndPowerInfoSummary(_requestID);
                        _v2SpaceAndPowerInfoSummary.SpaceAndPowerInfoSummaryExpander.IsExpanded = true;
                        V2SpaceAndPowerInfoSummaryUC.Content = _v2SpaceAndPowerInfoSummary;
                        V2SpaceAndPowerInfoSummaryUC.DataContext = _v2SpaceAndPowerInfoSummary;

                        this.DataContext = _spaceAndPower;

                        //توضیحات عقد قرارداد
                        DescriptionTextBox.Text = _spaceAndPower.GhardadComment;

                        //تاریخ
                        AgreementContractDatePicker.SelectedDate = _spaceAndPower.GhardadDate;
                    }
                    break;
            }
        }

        public override bool Save()
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
                                _spaceAndPower.GhardadComment = DescriptionTextBox.Text;
                                _spaceAndPower.GhardadDate = (AgreementContractDatePicker.SelectedDate.HasValue) ? AgreementContractDatePicker.SelectedDate : DB.GetServerDate();
                                _spaceAndPower.Detach();
                                DB.Save(_spaceAndPower, false);
                            }
                            break;
                    }

                    _request.StatusID = (int)StatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request, false);

                    scope.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage(".ذخیره اطلاعات انجام شد");
                }
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
            try
            {
                this.Save();
                if (this.IsSaveSuccess)
                {
                    Status status = Data.StatusDB.GetStatueByStatusID(_request.StatusID);

                    if (
                        this._request.RequestTypeID == (int)DB.RequestType.SpaceandPower //چنانچه درخواست فضاپاور باشد
                        &&
                        _requestInfoSummary._TelecomminucationServicePaymentInfos.Count == 0 //هیچ کالا و خدماتی انتخاب نشده باشد
                        &&
                        status.StatusType == (byte)DB.RequestStatusType.TelecomminucationServicePaymentChecking //بررسی کالا و خدمات الزامی باشد
                        )
                    {
                        _requestInfoSummary.RequestTelecomminucationServiceTabItem.IsSelected = true;
                        throw new Exception("تعیین کالا وخدمات مخابرات الزامی است");
                    }

                    IsForwardSuccess = true;
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در ارجاع", ex);
                IsForwardSuccess = false;
            }
            return IsForwardSuccess;
        }

        public override bool Deny()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _request.StatusID = (int)StatusComboBox.SelectedValue;
                    _request.Detach();
                    DB.Save(_request, false);

                    switch (_request.RequestTypeID)
                    {
                        case (byte)DB.RequestType.SpaceandPower:
                            {

                                _spaceAndPower = this.DataContext as CRM.Data.SpaceAndPower;
                                _spaceAndPower.GhardadComment = string.Empty;
                                _spaceAndPower.GhardadDate = null;
                                _spaceAndPower.Detach();
                                DB.Save(_spaceAndPower, false);
                            }
                            break;
                    }

                    scope.Complete();
                    IsRejectSuccess = true;
                }
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("خطا در رد", ex);
            }
            return IsRejectSuccess;
        }

        #endregion
    }
}
