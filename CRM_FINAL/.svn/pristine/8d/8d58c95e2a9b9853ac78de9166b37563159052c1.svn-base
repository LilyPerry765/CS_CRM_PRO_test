using CRM.Application.UserControls;
using CRM.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for E1ChooseNumber.xaml
    /// </summary>
    public partial class E1SwitchForm : Local.RequestFormBase
    {

        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }

        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }

        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }

        private UserControls.DDFUserControl _DDFUserControl { get; set; }

        private UserControls.DDFUserControl _DDFInterfaceUserControl { get; set; }

        long _requestID = 0;
        Request _request { get; set; }
        CRM.Data.E1 _e1 { get; set; }

        private long? _subID;
        E1Link _e1Link { get; set; }

        E1Number OldE1Number          { get; set; }
        E1Number OldE1InterfaceNumber { get; set; }



        public E1SwitchForm()
        {
            InitializeComponent();
            Initialize();
        }
        public E1SwitchForm(long requstID, long? subID)
            : this()
        {
            this._subID = subID;
            base.RequestID = this._requestID = requstID;

        }

        private void Initialize()
        {

            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        public void LoadData()
        {
            _request = Data.RequestDB.GetRequestByID(_requestID);
            _e1 = Data.E1DB.GetE1ByRequestID(_requestID);

            _requestInfoSummary = new RequestInfoSummary(_request.ID);
            RequestInfoUC.Content = _requestInfoSummary;
            RequestInfoUC.DataContext = _requestInfoSummary;


            _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
            _customerInfoSummary.IsExpandedProperty = true;
            _customerInfoSummary.Mode = true;
            CustomerInfoUC.Content = _customerInfoSummary;
            CustomerInfoUC.DataContext = _customerInfoSummary;

            _E1InfoSummary = new E1InfoSummary(_requestID , _subID);
            _E1InfoSummary.AcsessDDFTextBox.Visibility = Visibility.Visible;
            _E1InfoSummary.AcsessDDFLable.Visibility = Visibility.Visible;
            _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;

            E1InfoSummaryUC.Content = _E1InfoSummary;
            E1InfoSummaryUC.DataContext = _E1InfoSummary;

            if (_subID != null)
            {
                _e1Link = Data.E1LinkDB.GetE1LinkByID(_subID ?? 0);

                if (_e1Link.SwitchE1NumberID != null)
                {
                    _DDFUserControl = new DDFUserControl((int)_e1Link.SwitchE1NumberID);
                    _DDFUserControl.CenterID = _request.CenterID;
                    DDFGroupBox.DataContext = _DDFUserControl;
                    DDFGroupBox.Content = _DDFUserControl;
                }
                else
                {
                    _DDFUserControl = new DDFUserControl((byte)DB.DDFType.SalonSwitch);
                    _DDFUserControl.CenterID = _request.CenterID;
                    DDFGroupBox.DataContext = _DDFUserControl;
                    DDFGroupBox.Content = _DDFUserControl;
                }

                if (_e1Link.SwitchInterfaceE1NumberID != null)
                {
                    _DDFInterfaceUserControl = new DDFUserControl((int)_e1Link.SwitchInterfaceE1NumberID);
                    _DDFInterfaceUserControl.CenterID = _request.CenterID;
                    InterfaceDDFGroupBox.DataContext = _DDFInterfaceUserControl;
                    InterfaceDDFGroupBox.Content = _DDFInterfaceUserControl;
                }
                else
                {
                    _DDFInterfaceUserControl = new DDFUserControl((byte)DB.DDFType.SalonSwitch);
                    _DDFInterfaceUserControl.CenterID = _request.CenterID;
                    InterfaceDDFGroupBox.DataContext = _DDFInterfaceUserControl;
                    InterfaceDDFGroupBox.Content = _DDFInterfaceUserControl;
                }

                if (_e1Link.AcessE1NumberID != null)
                    OldE1Number = Data.E1NumberDB.GetE1NumberByID(_e1Link.SwitchE1NumberID ?? 0);

                if (_e1Link.SwitchInterfaceE1NumberID != null)
                    OldE1InterfaceNumber = Data.E1NumberDB.GetE1NumberByID(_e1Link.SwitchInterfaceE1NumberID ?? 0);

                DDFGroupBox.Visibility = Visibility.Visible;
                InterfaceDDFGroupBox.Visibility = Visibility.Visible;
                DetailGroupBox.Visibility = Visibility.Collapsed;

                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit};
            }
            else
            {

                StatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
                StatusComboBox.SelectedValue = this.currentStat;

                DDFGroupBox.Visibility = Visibility.Collapsed;
                InterfaceDDFGroupBox.Visibility = Visibility.Collapsed;
                DetailGroupBox.Visibility = Visibility.Visible;

            }



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
              
                using (TransactionScope ts = new TransactionScope())
                {
                    if (_subID != null)
                    {

                        if (OldE1Number != null)
                        {
                            OldE1Number.SwitchPortID = null;
                            OldE1Number.Status = (byte)DB.E1NumberStatus.Free;
                            OldE1Number.Detach();
                            DB.Save(OldE1Number);
                        }

                        if (OldE1InterfaceNumber != null)
                        {
                            OldE1InterfaceNumber.SwitchPortID = null;
                            OldE1InterfaceNumber.Status = (byte)DB.E1NumberStatus.Free;
                            OldE1InterfaceNumber.Detach();
                            DB.Save(OldE1InterfaceNumber);
                        }

                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_e1.TelephoneNo ?? 0);
                        telephone.Status = (byte)DB.TelephoneStatus.Connecting;
                        telephone.Detach();
                        DB.Save(telephone);

                        E1Number E1Number = null;
                        if (_DDFUserControl.E1Number != 0)
                        {

                            E1Number = Data.E1NumberDB.GetE1NumberByID(_DDFUserControl.E1Number);
                            E1Number.SwitchPortID = telephone.SwitchPortID;
                            E1Number.Status = (byte)DB.E1NumberStatus.Connection;
                            E1Number.Detach();
                            DB.Save(E1Number);
                            _e1Link.SwitchE1NumberID = E1Number.ID;

                        }
                           // throw new Exception("لطفا پورت دی دی اف سوئیچ را انتخاب کنید");
                        E1Number E1InterfaceNumber = null;
                        if (_DDFInterfaceUserControl.E1Number != 0)
                        {
                            E1InterfaceNumber = Data.E1NumberDB.GetE1NumberByID(_DDFInterfaceUserControl.E1Number);
                            E1InterfaceNumber.SwitchPortID = telephone.SwitchPortID;
                            E1InterfaceNumber.Status = (byte)DB.E1NumberStatus.Connection;
                            E1InterfaceNumber.Detach();
                            DB.Save(E1InterfaceNumber);
                            _e1Link.SwitchInterfaceE1NumberID = E1InterfaceNumber.ID;

                        }
                         //   throw new Exception("لطفا پورت دی دی اف رابط سوئیچ را انتخاب کنید");

                        if (_DDFUserControl.E1Number != 0 && _DDFInterfaceUserControl.E1Number != 0 && _DDFInterfaceUserControl.E1Number == _DDFUserControl.E1Number)
                            throw new Exception("پورت رایط و سوئیچ نمی توانند یکسان باشند");


                        E1Number E1AcessNumber = null;
                        if (_e1Link.AcessE1NumberID != null)
                        {
                            E1AcessNumber = Data.E1NumberDB.GetE1NumberByID((int)_e1Link.AcessE1NumberID);
                            E1AcessNumber.SwitchPortID = telephone.SwitchPortID;
                            E1AcessNumber.Status = (byte)DB.E1NumberStatus.Connection;
                            E1AcessNumber.Detach();
                            DB.Save(E1AcessNumber);
                        }





                        _e1Link.Detach();
                        DB.Save(_e1Link);





                    }
                    else
                    {
                        _request.StatusID = (int)StatusComboBox.SelectedValue;
                        _request.Detach();
                        DB.Save(_request);
                    }
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

        #region Action
        public override bool Forward()
        {
            Save();
            if (IsSaveSuccess == true)
                if (_request.RequestTypeID == (int)DB.RequestType.E1)
                {
                    IsForwardSuccess = true;
                    //if (Data.E1LinkDB.CheckALLSwitch(_request.ID))
                    //{
                    //    IsForwardSuccess = true;
                    //}
                    //else
                    //{
                    //    throw new Exception("برای همه درخواست ها اطلاعات سوئیچ ذخیره نشده است");
                    //}
                }
                else
                {
                    IsForwardSuccess = true;
                }
               
            return IsForwardSuccess;
        }
        public override bool Deny()
        {
            try
            {
                Reject();
                IsRejectSuccess = true;
            }
            catch (Exception ex)
            {
                IsRejectSuccess = false;
                ShowErrorMessage("خطا در رد اطلاعات", ex);
            }

            return IsRejectSuccess;
        }

        private void Reject()
        {
            using (TransactionScope subts = new TransactionScope(TransactionScopeOption.Required))
            {
                _request.StatusID = (int)StatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_e1.TelephoneNo ?? 0);
                if (telephone.Status == (byte)DB.TelephoneStatus.Connecting)
                {
                    telephone.Status = (byte)DB.TelephoneStatus.Reserv;
                    telephone.Detach();
                    DB.Save(telephone);


                    _e1 = this.DataContext as CRM.Data.E1;
                    _e1.DDFConnectingSwitch = string.Empty;
                    _e1.DDFConnectingInterface = string.Empty;
                    _e1.Detach();
                    DB.Save(_e1, false);
                }


                subts.Complete();
            }
        }
        #endregion Action

    }
}
