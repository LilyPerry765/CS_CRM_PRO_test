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
    /// Interaction logic for ChanngeLocationCountorForm.xaml
    /// </summary>
    public partial class ChanngeLocationCountorForm : Local.RequestFormBase
    {

        #region fields && properties

        private long _RequestID=0;
        Counter countor;
        Request request = new Request();
        ChangeLocation changeLocation;

        #endregion

        #region constractor
        public ChanngeLocationCountorForm()
        {
            InitializeComponent();
        }

        public ChanngeLocationCountorForm(long requestID):this()
        {
           base.RequestID = this._RequestID = requestID;
           Initializ();
        }
        private void Initializ()
        {
            changeLocation = new ChangeLocation();
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.Print, (byte)DB.NewAction.Forward, (byte)DB.NewAction.Exit, (byte)DB.NewAction.Deny };
        }
        #endregion

        #region Method

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                CustomerAddressUserControl = new UserControls.CustomerAddressUserControl(_RequestID);
                
                if (_RequestID != 0)
                {



                    request = Data.RequestDB.GetRequestByID(_RequestID);

                    switch (request.RequestTypeID)
                    {
                        case (byte)DB.RequestType.ChangeLocationCenterInside:
                            ChangeLocationCenterInsideLoad();
                            break;
                        case (byte)DB.RequestType.ChangeLocationCenterToCenter:
                            ChangeLocationCenterToCenterLoad();
                            break;
                    }

              }
                   

            }
            catch (Exception ex)
            {
                ShowErrorMessage("خطا در بارگزاری اطلاعات",ex);
            }
            
        }

        private void ChangeLocationCenterToCenterLoad()
        {
               Counter counter = new Counter();

               changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID((long)_RequestID);


                if (changeLocation.SourceCenter != null && changeLocation.SourceCenter == request.CenterID)
                {
                    // اگر تغییر مکان مرکز به مرکز است ودر مبدا هستیم
                    // کنتور قدیم تخلیه میشود
                    counter = Data.CounterDB.GetCounterByTelephonNo( (long)changeLocation.OldTelephone);
                    NewCountor.CountorID = counter.ID;
                    NewCountorInfo.Header = "تلفن تخلیه";
                    NewCountor.NewTelephoneTextBox.IsReadOnly = true;
                    OldTeleInfo.Visibility = Visibility.Collapsed;



                }
                else if (changeLocation.SourceCenter != null && changeLocation.TargetCenter == request.CenterID)
                {
                    // اگر تغییر مکان مرکز به مرکز است و در مقصد هستیم
                    // کنتور جدید ثبت میشود
                    NewCountor.CountorID = 0;
                    if (changeLocation != null)
                    {
                        OldTel.Text = changeLocation.OldTelephone.ToString();
                        if (changeLocation.NewTelephone != null)
                            NewCountor.TelephoneNo = (long)changeLocation.NewTelephone;
                    }


                    if (changeLocation.NewCounterID != null)
                    {
                        NewCountor.CountorID = (long)changeLocation.NewCounterID;
                        NewCountor.NewTelephoneTextBox.IsReadOnly = true;

                    }


                }
            
        }

        private void ChangeLocationCenterInsideLoad()
        {
            // تغییر مکان داخل مرکز
            Counter counter = new Counter();
            changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID((long)_RequestID);
            if (changeLocation != null)
            {
                OldTel.Text = changeLocation.OldTelephone.ToString();
                if (changeLocation.NewTelephone != null)
                    NewCountor.TelephoneNo = (long)changeLocation.NewTelephone;
            }


            if (changeLocation.NewCounterID != null)
            {
                counter = Data.CounterDB.GetCounterByTelephonNo((long)changeLocation.NewTelephone);
                NewCountor.CountorID = counter.ID;
                NewCountor.NewTelephoneTextBox.IsReadOnly = true;
            }
        }

        #endregion

        #region action

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

                    this.countor = NewCountor.Counter;
                    switch (request.RequestTypeID)
                    {
                        case (int)DB.RequestType.ChangeLocationCenterInside:
                        case (int)DB.RequestType.ChangeLocationCenterToCenter:
                            ChangeLocationSave();
                            break;
                    }

                    ts.Complete();
                }

                ShowSuccessMessage("کنتور ذخیره شد");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("ذخیره کنتور انجام نشد", ex);
            }
           return IsSaveSuccess = true;
        }

        private void ChangeLocationSave()
        {

                if (changeLocation.SourceCenter != null && changeLocation.SourceCenter == request.CenterID)
                {
                    // اگر تغییر مکان مرکز به مرکز است ودر مبدا هستیم
                    this.countor.InsertDate = (DateTime)DB.GetServerDate();
                    this.countor.Detach();
                    DB.Save(this.countor);
                }

                else
                {

                    this.countor.InsertDate = (DateTime)DB.GetServerDate();
                    this.countor.Detach();
                    DB.Save(this.countor);
                    changeLocation.NewCounterID = countor.ID;
                    changeLocation.Detach();
                    DB.Save(changeLocation);
                }          
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
