using CRM.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for TranslationCabinetMDFFrom.xaml
    /// </summary>
    public partial class TranslationCentralCableMDFNetworkForm : Local.RequestFormBase
    {
        CRM.Application.UserControls.TranslationCentralCableMDFInfo _translationCentralCableMDFInfo;
        private Request request { get; set; }
        Data.ExchangeCentralCableMDF _exchangeCentralCableMDF { get; set; }
        private long _requestID = 0;



        public TranslationCentralCableMDFNetworkForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Save, (byte)DB.NewAction.ConfirmEnd, (byte)DB.NewAction.Exit };
        }
        public TranslationCentralCableMDFNetworkForm(long requestID)
            : this()
        {
            this._requestID = requestID;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            _translationCentralCableMDFInfo = new CRM.Application.UserControls.TranslationCentralCableMDFInfo(_requestID);
            TranslationInfo.DataContext = _translationCentralCableMDFInfo; ;
            TranslationInfo.Content = _translationCentralCableMDFInfo; ;

            _exchangeCentralCableMDF = Data.TranslationCentralCableMDFDB.GetTranslationVentralCableMDFByID(_requestID);
            request = Data.RequestDB.GetRequestByID(_requestID);

            StatusComboBox.ItemsSource = DB.GetStepStatus(request.RequestTypeID, this.currentStep);
            StatusComboBox.SelectedValue = request.StatusID;

            if (_exchangeCentralCableMDF.NetworkAccomplishmentDate == null)
            {
                DateTime dateTime = DB.GetServerDate();
                _exchangeCentralCableMDF.NetworkAccomplishmentDate = dateTime.Date;
                _exchangeCentralCableMDF.NetworkAccomplishmentTime = dateTime.ToString("hh:mm:ss");
            }

            AccomplishmentGroupBox.DataContext = _exchangeCentralCableMDF;
        }
        public override bool Save()
        {
            try
            {
                using (TransactionScope ts2 = new TransactionScope(TransactionScopeOption.Required))
                {
                    request.StatusID = (int)StatusComboBox.SelectedValue;
                    request.Detach();
                    DB.Save(request, false);


                    _exchangeCentralCableMDF.Detach();
                    DB.Save(_exchangeCentralCableMDF, false);

                    ts2.Complete();
                    IsSaveSuccess = true;
                    ShowSuccessMessage("ذخیره انجام شد");
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
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(5)))
                {

                    Save();
                    this.RequestID = _requestID;
                    if (IsSaveSuccess)
                    {
                        IsForwardSuccess = true;
                    }
                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                IsForwardSuccess = false;
                ShowErrorMessage("خطا در ذخیره اطلاعات", ex);

            }
            return IsForwardSuccess;

        }


        private void wiringButtom_Click(object sender, RoutedEventArgs e)
        {
            Folder.MessageBox.ShowInfo("فرم سیم بندی در دست تهیه می باشد.");
        }




    }
}


