using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Enterprise;

namespace CRM.Application.Views
{
    /// <summary>
    /// Interaction logic for ChangePreCodeList.xaml
    /// </summary>
    public partial class ChangePreCodeForm : Local.RequestFormBase
    {
        #region Constructor and fields
        CRM.Application.UserControls.ExchangeRequestInfo _exchangeRequestInfo { get; set; }
        Request _request;
        private int _requestType;
        private long _requestID;
        Status _statusEnd;
        ChangePreCode _changePreCode;
        SwitchPrecode _pastSwitchPrecode;
        SwitchPrecode _pastNewswitchPrecode;
        SwitchPrecode switchPrecode;
        public ChangePreCodeForm()
        {
            InitializeComponent();
        }
        public ChangePreCodeForm(int requestType)
            : this()
        {
            this._requestType = requestType;
            Initialize();
        }
        public  ChangePreCodeForm(long requestID):this()
        {

            this._requestID = requestID;
            Initialize();

            //_statusEnd = DB.GetStatus((int)DB.RequestType.ChangePreCode, (int)DB.RequestStatusType.Changes);

            //if (_requestID == 0)
            //{
            //    this._requestType = ExchangeRequestInfoUserControl.RequestType = (int)DB.RequestType.ChangePreCode;
            //}
            //else
            //{
            //     this._requestType = ExchangeRequestInfoUserControl.RequestType = (int)DB.RequestType.ChangePreCode;
            //     ExchangeRequestInfoUserControl.ID = _requestID;
            //}
            //ExchangeRequestInfoUserControl.Load();
            
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            PreCodeTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.SwitchTypeCode));
            DeploymentTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.DeploymentType));
            DorshoalNumberTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.DorshoalNumberType));
            StatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.SwitchPreCodeStatus));

            NewPreCodeTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.SwitchTypeCode));
            NewDeploymentTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.DeploymentType));
            NewDorshoalNumberTypeColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.DorshoalNumberType));
            NewStatusColumn.ItemsSource = Helper.GetEnumCheckable(typeof(DB.SwitchPreCodeStatus));

            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Print, (byte)DB.NewAction.Exit };
        }

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
             _exchangeRequestInfo = new UserControls.ExchangeRequestInfo(_requestID);
             _exchangeRequestInfo.RequestType = this._requestType;
             _exchangeRequestInfo.DoCenterChange += ExchangeRequestInfoUserControl_DoCenterChange;
             ExchangeRequestInfoUserControl.Content = _exchangeRequestInfo;
             ExchangeRequestInfoUserControl.DataContext = _exchangeRequestInfo;


             StatusComboBox.ItemsSource = DB.GetStepStatus((int)DB.RequestType.ChangePreCode, this.currentStep);
             StatusComboBox.SelectedValue = this.currentStat;

             if (_requestID == 0)
             {
                 _changePreCode = new ChangePreCode();
                 AccomplishmentGroupBox.Visibility = Visibility.Collapsed;
             }
             else
             {

                 _request = Data.RequestDB.GetRequestByID(_requestID);
                 _changePreCode = Data.ChangePreCodeDB.GetChangePreCodeByID((long)_requestID);

                 _pastSwitchPrecode = Data.SwitchPrecodeDB.GetSwitchPrecodeByID(_changePreCode.OldPreCodeID);
                 this.DataContext = _changePreCode;
                 SwitchComboBox.SelectedValue = _changePreCode.OldSwitchID;
                 SwitchComboBox_SelectionChanged(null, null);
                 SwitchPreCodeComboBox.SelectedValue = _changePreCode.OldPreCodeID;
                 SwitchPreCodeComboBox_SelectionChanged(null, null);
                 NewSwitchComboBox.SelectedValue = _changePreCode.NewSwhitchID;
                 NewSwitchComboBox_SelectionChanged(null, null);

                 if (Data.StatusDB.IsFinalStep(this.currentStat))
                 {
                     AccomplishmentDateLabel.Visibility = Visibility.Visible;
                     AccomplishmentDate.Visibility = Visibility.Visible;

                     AccomplishmentTimeLabel.Visibility = Visibility.Visible;
                     AccomplishmentTime.Visibility = Visibility.Visible;

                     if (_changePreCode.AccomplishmentTime == null)
                     {
                         DateTime currentDateTime = DB.GetServerDate();
                         _changePreCode.AccomplishmentTime = currentDateTime.ToShortTimeString();
                         _changePreCode.AccomplishmentDate = currentDateTime;
                     }

                 }


                 Status Status = Data.StatusDB.GetStatueByStatusID(_request.StatusID);
                 if (Status.StatusType == (byte)DB.RequestStatusType.Start)
                 {
                     StatusComboBox.Visibility = Visibility.Collapsed;
                     wiringButtom.Visibility = Visibility.Collapsed;
                     StatusComboBoxLabel.Visibility = Visibility.Collapsed;
                 }
             }

              this.DataContext = _changePreCode;

            //if (_changePreCode != null)
            //{
            //    _pastSwitchPrecode = Data.SwitchPrecodeDB.GetSwitchPrecodeByID(_changePreCode.OldPreCodeID);
            //     this.DataContext = _changePreCode;
            //     SwitchComboBox.SelectedValue = _changePreCode.OldSwitchID;
            //     SwitchComboBox_SelectionChanged(null, null);
            //     SwitchPreCodeComboBox.SelectedValue = _changePreCode.OldPreCodeID;
            //     SwitchPreCodeComboBox_SelectionChanged(null, null);
            //     NewSwitchComboBox.SelectedValue = _changePreCode.NewSwhitchID;
            //     NewSwitchComboBox_SelectionChanged(null, null);
            //}
            //else
            //{
            //    _changePreCode = new ChangePreCode();
            //    this.DataContext = _changePreCode;
            //}

              //if (_statusEnd.ID != _request.StatusID)
              //{
              //    ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Forward };
              //}
              //else
              //{
              //    ActionIDs = new List<byte> { (byte)DB.NewAction.Forward };
              //}
        }

        private void ExchangeRequestInfoUserControl_DoCenterChange(int centerID)
        {
            SwitchComboBox.ItemsSource = Data.SwitchDB.GetSwitchCheckableByCenterID(centerID);
            NewSwitchComboBox.ItemsSource = Data.SwitchDB.GetSwitchCheckableByCenterID(centerID);
        }



        #endregion

        #region Action

        public override bool Forward()
        {

            if (_changePreCode.Completion != true)
            {
                Save();
            }
            else
            {
                IsForwardSuccess = true;
            }
            base.RequestID = _request.ID;
            if(IsSaveSuccess == true)
                IsForwardSuccess = true;

            return IsForwardSuccess;
        }


        public override bool Save()
        {

            try
            {

                using (TransactionScope ts = new  TransactionScope())
                {
                    ChangePreCode changePreCode = this.DataContext as ChangePreCode;
                    _request = _exchangeRequestInfo.Request;

                    Status Status = Data.StatusDB.GetStatueByStatusID(_request.StatusID);

                    if (_request == null) throw new Exception("در خواست یافت نشد");
                    changePreCode.InsertDate =  _request.InsertDate = DB.GetServerDate();

                    if (_request.ID == 0)
                    {
                        _request.ID = DB.GenerateRequestID();
                        _request.RequestPaymentTypeID = 0;
                        _request.IsViewed = false;
                        _request.InsertDate = DB.GetServerDate();
                        _request.StatusID = DB.GetStatus(_requestType, (int)DB.RequestStatusType.Start).ID; // Get Start Status;
                        _request.Detach();
                        DB.Save(_request, true);

                        changePreCode.ID = _request.ID;
                        changePreCode.FromTelephonNo = (long)switchPrecode.FromNumber;
                        changePreCode.ToTelephoneNo = (long)switchPrecode.ToNumber;
                        changePreCode.Detach();
                        DB.Save(changePreCode,true);
                    }
                    else
                    {
                        _request.Detach();
                        DB.Save(_request,false);

                        changePreCode.Detach();
                        DB.Save(changePreCode,false);
                    }
                    base.RequestID = _request.ID;

                    // if status is not final position information is temporarily save
                    if (_requestID == 0 || Status.StatusType != (byte)DB.RequestStatusType.Changes)
                    {
                        if (_pastSwitchPrecode != null)
                        {

                            _pastSwitchPrecode.Status = (int)DB.SwitchPreCodeStatus.Active;
                            _pastSwitchPrecode.Detach();
                            DB.Save(_pastSwitchPrecode);

                        }

                        SwitchPrecode switchPrecode = Data.SwitchPrecodeDB.GetSwitchPrecodeByID(changePreCode.OldPreCodeID);
                        if (switchPrecode.Status != (byte)DB.SwitchPreCodeStatus.Active) { MessageBox.Show("پیش شماره در وضعیت فعال نیست", "خطا", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
                        switchPrecode.Status = (int)DB.SwitchPreCodeStatus.changePreCode;
                        switchPrecode.Detach();
                        DB.Save(switchPrecode);

                    }
                    // if status in final position change are
                    else
                    {
                        List<Telephone> Telephones = Data.TelephoneDB.GetTelephoneBySwitchPreCodeID(_changePreCode.OldPreCodeID);
                        foreach (Telephone item in Telephones)
                        {

                            ChangePreCodeDB.UpdateTelephone(item.TelephoneNo, Convert.ToInt64(_changePreCode.NewPreCode.ToString() + item.TelephoneNo.ToString().Remove(0, 4)));
                        }
                        if (Telephones != null)
                        {
                            SwitchPrecode switchPrecode = Data.SwitchPrecodeDB.GetSwitchPrecodeByID((int)Telephones[0].SwitchPrecodeID);
                            switchPrecode.SwitchPreNo = _changePreCode.NewPreCode;
                            switchPrecode.Status = (byte)DB.SwitchPreCodeStatus.Active;
                            switchPrecode.FromNumber = Convert.ToInt64(_changePreCode.NewPreCode.ToString() + switchPrecode.FromNumber.ToString().Remove(0, 4));
                            switchPrecode.ToNumber = Convert.ToInt64(_changePreCode.NewPreCode.ToString() + switchPrecode.ToNumber.ToString().Remove(0, 4));
                            switchPrecode.Detach();
                            DB.Save(switchPrecode);
                        }
                        changePreCode.Completion = true;
                        changePreCode.Detach();
                        DB.Save(changePreCode, false);

                    }
                    ts.Complete();
                    IsSaveSuccess = true;
                }

            }
            catch(Exception ex)
            {
                ShowErrorMessage("خطا در ذخیره تغییر پیش شماره", ex);
                Logger.Write(ex);
            }

            return IsSaveSuccess;

        }
        #endregion Action

        #region Event

        private void SwitchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SwitchComboBox.SelectedValue != null)
            {
                Logger.WriteError("SwitchComboBoxID: {0}" , SwitchComboBox.SelectedValue.ToString());
                SwitchPreCodeComboBox.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeBySwitchID((int)SwitchComboBox.SelectedValue).Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchPreNo.ToString(), IsChecked = false }).ToList();
                
            }
        }

        private  void SwitchPreCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SwitchPreCodeComboBox.SelectedValue != null)
            {

                int switchPreCodeId = (int)SwitchPreCodeComboBox.SelectedValue;
                Logger.WriteError("SwitchPreCodeComboBox: {0}",switchPreCodeId.ToString());
              
                List<SwitchPrecode> SwitchPrecodes = new List<SwitchPrecode>();
                switchPrecode = Data.SwitchPrecodeDB.GetSwitchPrecodeByID(switchPreCodeId);
                SwitchPrecodes.Add(switchPrecode);
                SwitchPreCodeDataGrid.ItemsSource  = SwitchPrecodes;
             


            }
        }

        private  void NewSwitchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewSwitchComboBox.SelectedValue != null)
            {
                int NewSwitchID = (int)NewSwitchComboBox.SelectedValue;
                Logger.WriteError("NewSwitchComboBox: {0}", NewSwitchComboBox.SelectedValue.ToString());

                NewSwitchPreCodeDataGrid.ItemsSource = Data.SwitchPrecodeDB.GetSwitchPrecodeBySwitchID(NewSwitchID);

            }

        }

        #endregion

        private void wiringButtom_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
