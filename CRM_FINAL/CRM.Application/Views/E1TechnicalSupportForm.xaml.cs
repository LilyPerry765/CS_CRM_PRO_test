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
    public partial class E1TechnicalSupportForm : Local.RequestFormBase
    {

        private UserControls.CustomerInfoSummary _customerInfoSummary { get; set; }
        private UserControls.RequestInfoSummary _requestInfoSummary { get; set; }
        private UserControls.E1InfoSummary _E1InfoSummary { get; set; }
        private UserControls.DDFUserControl _DDFUserControl { get; set; }
        private UserControls.MDFUserControl _MDFUserControl { get; set; }

        E1Number OldE1Number { get; set; }
        Bucht Oldbucht { get; set; }

        long _requestID = 0;
        Request _request { get; set; }
        CRM.Data.E1 _e1 { get; set; }

        private long? _subID;
        E1Link _e1Link { get; set; }

        public E1TechnicalSupportForm()
        {
            InitializeComponent();
            Initialize();
        }

        public E1TechnicalSupportForm(long requstID, long? subID)
            : this()
        {
            base.RequestID = this._requestID = requstID;
            this._subID = subID;

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

            switch (_request.RequestTypeID)
            {
                case (byte)DB.RequestType.E1:
                case (byte)DB.RequestType.E1Link:
                    {

                        _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
                        _customerInfoSummary.IsExpandedProperty = true;
                        _customerInfoSummary.Mode = true;
                        CustomerInfoUC.Content = _customerInfoSummary;
                        CustomerInfoUC.DataContext = _customerInfoSummary;


                        _requestInfoSummary = new RequestInfoSummary(_request.ID);
                        _requestInfoSummary.RequestInfoExpander.IsExpanded = true;
                        RequestInfoUC.Content = _requestInfoSummary;
                        RequestInfoUC.DataContext = _requestInfoSummary;

                        _E1InfoSummary = new E1InfoSummary(_requestID, _subID);
                        _E1InfoSummary.E1InfoSummaryExpander.IsExpanded = true;
                        E1InfoSummaryUC.Content = _E1InfoSummary;
                        E1InfoSummaryUC.DataContext = _E1InfoSummary;

                        _e1 = Data.E1DB.GetE1ByRequestID(_request.ID);
                        if (_subID != null)
                        {

                             ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };

                            _e1Link = Data.E1LinkDB.GetE1LinkByID(_subID ?? 0);

                            //StatusComboBox.Visibility = Visibility.Collapsed;
                            //StatusLabel.Visibility = Visibility.Collapsed;

                            if (_e1Link.AcessE1NumberID != null)
                            {
                                _DDFUserControl = new DDFUserControl((int)_e1Link.AcessE1NumberID);
                                _DDFUserControl.CenterID = _request.CenterID;
                                DDFGroupBox.DataContext = _DDFUserControl;
                                DDFGroupBox.Content = _DDFUserControl;
                            }
                            else
                            {
                                _DDFUserControl = new DDFUserControl((byte)DB.DDFType.TechnicalSupport);
                                _DDFUserControl.CenterID = _request.CenterID;
                                DDFGroupBox.DataContext = _DDFUserControl;
                                DDFGroupBox.Content = _DDFUserControl;
                            }

                            if (_e1Link.AcessBuchtID != null)
                            {
                                _MDFUserControl = new MDFUserControl((long)_e1Link.AcessBuchtID);
                                _MDFUserControl.CenterID = _request.CenterID;
                                MDFGroupBox.DataContext = _MDFUserControl;
                                MDFGroupBox.Content = _MDFUserControl;
                            }
                            else
                            {
                                _MDFUserControl = new MDFUserControl((int)DB.BuchtType.E1);
                                _MDFUserControl.CenterID = _request.CenterID;
                                MDFGroupBox.DataContext = _MDFUserControl;
                                MDFGroupBox.Content = _MDFUserControl;
                            }

                            if (_e1Link.AcessE1NumberID != null)
                                OldE1Number = Data.E1NumberDB.GetE1NumberByID(_e1Link.AcessE1NumberID ?? 0);

                            if (_e1Link.AcessBuchtID != null)
                                Oldbucht = Data.BuchtDB.GetBuchtByID(_e1Link.AcessBuchtID ?? 0);

                            StatusGroupBox.Visibility = Visibility.Collapsed;
                            this.DataContext = _e1Link;

                        }
                        else
                        {

                            DDFGroupBox.Visibility = Visibility.Collapsed;
                            MDFGroupBox.Visibility = Visibility.Collapsed;

                            StatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
                            StatusComboBox.SelectedValue = this.currentStat;

                            return;
                        }
                    }
                    break;
                case (byte)DB.RequestType.VacateE1:
                    {



                        _customerInfoSummary = new CustomerInfoSummary(_request.CustomerID);
                        _customerInfoSummary.IsExpandedProperty = true;
                        _customerInfoSummary.Mode = true;
                        CustomerInfoUC.Content = _customerInfoSummary;
                        CustomerInfoUC.DataContext = _customerInfoSummary;


                        _requestInfoSummary = new RequestInfoSummary(_request.ID);
                        _requestInfoSummary.RequestInfoExpander.IsExpanded = true;
                        RequestInfoUC.Content = _requestInfoSummary;
                        RequestInfoUC.DataContext = _requestInfoSummary;

                        E1InfoSummaryUC.Visibility = Visibility.Collapsed;


                        _e1 = Data.E1DB.GetE1ByRequestID(_request.ID);
                        if (_subID != null)
                        {
                                ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Exit };

                            _e1Link = Data.E1LinkDB.GetE1LinkByID(_subID ?? 0);

                            _DDFUserControl = new DDFUserControl((int)_e1Link.AcessE1NumberID);
                            _DDFUserControl.CenterID = _request.CenterID;
                            DDFGroupBox.DataContext = _DDFUserControl;
                            DDFGroupBox.Content = _DDFUserControl;
                            DDFGroupBox.IsEnabled = false;

                            _MDFUserControl = new MDFUserControl((long)_e1Link.AcessBuchtID);
                            _MDFUserControl.CenterID = _request.CenterID;
                            MDFGroupBox.DataContext = _MDFUserControl;
                            MDFGroupBox.Content = _MDFUserControl;
                            MDFGroupBox.IsEnabled = false;


                            StatusGroupBox.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            StatusComboBox.ItemsSource = DB.GetStepStatus(_request.RequestTypeID, this.currentStep);
                            StatusComboBox.SelectedValue = this.currentStat;

                            DDFGroupBox.Visibility = Visibility.Collapsed;
                            MDFGroupBox.Visibility = Visibility.Collapsed;

                            StatusGroupBox.Visibility = Visibility.Visible;
                        }

                    }
                    break;
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
                switch (_request.RequestTypeID)
                {
                    case (byte)DB.RequestType.E1:
                    case (byte)DB.RequestType.E1Link:
                        {
                            _e1 = this.DataContext as CRM.Data.E1;

                            using (TransactionScope ts = new TransactionScope())
                            {

                                if (_subID != null)
                                {

                                    if (_DDFUserControl.E1Number == 0 || _MDFUserControl.BuchtID == 0)
                                        throw new Exception("لطفا اطلاعات بوخت و دی دی اف را وارد کنید.");

                                    // Relase old data

                                    if (Oldbucht != null)
                                    {
                                        Oldbucht.E1NumberID = null;
                                        Oldbucht.Status = (byte)DB.BuchtStatus.Free;
                                        Oldbucht.Detach();
                                        DB.Save(Oldbucht);
                                    }

                                    if (OldE1Number != null)
                                    {
                                        OldE1Number.Status = (byte)DB.E1NumberStatus.Free;
                                        OldE1Number.Detach();
                                        DB.Save(OldE1Number);
                                    }
                                    //


                                    // save new data
                                    E1Number e1Number = null;
                                    if (_DDFUserControl.E1Number != 0)
                                    {
                                        e1Number = Data.E1NumberDB.GetE1NumberByID(_DDFUserControl.E1Number);
                                        e1Number.Status = (byte)DB.E1NumberStatus.Reserve;
                                        e1Number.Detach();
                                        DB.Save(e1Number);
                                        _e1Link.AcessE1NumberID = _DDFUserControl.E1Number;
                                    }

                                    if (_MDFUserControl.BuchtID != 0)
                                    {
                                        Bucht bucht = Data.BuchtDB.GetBuchtByID(_MDFUserControl.BuchtID);
                                        if (e1Number != null)
                                        {
                                            bucht.E1NumberID = e1Number.ID;
                                        }
                                        bucht.Status = (byte)DB.BuchtStatus.Reserve;
                                        bucht.Detach();
                                        DB.Save(bucht);
                                        _e1Link.AcessBuchtID = _MDFUserControl.BuchtID;
                                    }

                     
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
                        }
                        break;
                    case (byte)DB.RequestType.VacateE1:
                        {
                            if(_subID != null)
                            {
                                _e1Link.TechnicalSupportDate = DB.GetServerDate();
                                _e1Link.Detach();
                                DB.Save(_e1Link);

                            }
                            else
                            {
                                if (!Data.E1LinkDB.CheckALLTechnicalSupportDate(_request.ID))
                                {
                                    throw new Exception("لطفا اطلاعات همه لینک ها را ذخیره کنید");
                                }
                                else
                                {
                                    _request.StatusID = (int)StatusComboBox.SelectedValue;
                                    _request.Detach();
                                    DB.Save(_request);

                                    IsSaveSuccess = true;
                                }
                            }
                        }
                        break;
                }
                ShowSuccessMessage("ذخیره اطلاعات انجام شد.");
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
            //if (Data.E1LinkDB.CheckALLTechSupport(_e1.RequestID))
            //{
                Save();
                if (IsSaveSuccess == true)
                    IsForwardSuccess = true;
            //}
            //else
            //{
            //    Folder.MessageBox.ShowError("همه لینک ها بررسی امکانات نشده اند");
            //    IsForwardSuccess = false;
            //}

            //Save();
            //if (IsSaveSuccess == true)
            //    IsForwardSuccess = true;
            return IsForwardSuccess;
        }

        public override bool Deny()
        {
            using (TransactionScope Subts = new TransactionScope())
            {
                // _request.StatusID = (int)StatusComboBox.SelectedValue;
                _request.Detach();
                DB.Save(_request);

                _e1 = this.DataContext as CRM.Data.E1;
                _e1.DDFAcessAdress = string.Empty;
                _e1.MDFPortAddressOfE1 = string.Empty;
                _e1.Detach();
                DB.Save(_e1, false);

                Subts.Complete();
            }

            IsRejectSuccess = true;
            return IsRejectSuccess;
        }

    }
}
