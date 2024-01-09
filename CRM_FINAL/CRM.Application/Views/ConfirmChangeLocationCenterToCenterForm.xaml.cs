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
using System.Transactions;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ConfirmChangeLocationCenterToCenterForm.xaml
    /// </summary>
    public partial class ConfirmChangeLocationCenterToCenterForm : Local.RequestFormBase
    {

        #region Properties
        private long _RequestID;
        private Request request;
        private ChangeLocation changeLocation;
        #endregion

        #region Constructors
        public ConfirmChangeLocationCenterToCenterForm()
        {
            InitializeComponent();
        }
        public ConfirmChangeLocationCenterToCenterForm(long requestID)
            : this()
        {
            base.RequestID = this._RequestID = requestID;
            Initialize();
        }

        private void Initialize()
        {
            FromCenter.ItemsSource = Data.CenterDB.GetCenterCheckable();
            ToCenter.ItemsSource = Data.CenterDB.GetCenterCheckable();
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }
        #endregion

        #region Methods
        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            request = Data.RequestDB.GetRequestByID(_RequestID);
            changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID((long)_RequestID);

            OldTelTextBox.Text = changeLocation.OldTelephone.ToString();
            NewTelTextBox.Text = changeLocation.NewTelephone.ToString();

            FromCenter.SelectedValue =(int)changeLocation.SourceCenter;
            ToCenter.SelectedValue = (int)changeLocation.TargetCenter;
            
            CustomerInfoSummaryUserControl.IsExpandedProperty = true;
            CustomerInfoSummaryUserControl.Mode = true;
            CustomerInfoSummaryUserControl = new UserControls.CustomerInfoSummary(request.CustomerID);
        }
        #endregion

        #region actionMethod
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
                    changeLocation.ConfirmTheSourceCenter = true;
                    changeLocation.Detach();
                    DB.Save(changeLocation);
                    ts.Complete();
                }
                ShowSuccessMessage("تایید ذخیره شد");
                IsSaveSuccess = true;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("تایید ذخیره نشد", ex);
                IsSaveSuccess = false;

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

        #endregion 
    }
}
