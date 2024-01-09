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
    public partial class AssignmentLinesForm : Local.RequestFormBase
    {
        #region Properties

        private long _RequestID = 0;
        private Data.ChangeAddress ChangeAddress;
        private Data.Request _Request;
        private UserControls.ChangeAddressUserControl _ChangeAddress { get; set; }

        #endregion

        #region Constractor
        public AssignmentLinesForm()
        {
            InitializeComponent();
        }

        public AssignmentLinesForm(long requestID)
            : this()
        {
            _RequestID = requestID;
            Initialize();
        }

        #endregion

        #region Methode

        private void Initialize()
        {
            ActionIDs = new List<byte> { (byte)DB.NewAction.Confirm };
            base.RequestID = _RequestID;
        }       

        public override bool Confirm()
        {
            using (TransactionScope ts = new TransactionScope())
            {
                //ChangeAddress.ConfirmVaghozari = true;
                //ChangeAddress.DataChangeAddreess =DB.GetServerDate();
                //ChangeAddress.Detach();
                //DB.Save(ChangeAddress);

                //List<Address> adr = _ChangeAddress.adr;
                
                //Address addressInstall = adr.Where(t => t.AddressTypeID == (byte)DB.AddressType.Install).SingleOrDefault();
                //addressInstall.AddressContent = _ChangeAddress.NewInstallAddresstextBox.Text;
                //addressInstall.PostalCode = _ChangeAddress.NewPostalCodeInstalltextBox.Text;
                
                //Address addressContact = adr.Where(t => t.AddressTypeID == (byte)DB.AddressType.Contact).SingleOrDefault();
                //addressContact.AddressContent = _ChangeAddress.NewContactAddresstextBox.Text;
                //addressContact.PostalCode = _ChangeAddress.NewPostalCodeContacttextBox.Text;

                //addressInstall.Detach();
                //DB.Save(addressInstall);
                
                //addressContact.Detach();
                //DB.Save(addressContact);

                //ts.Complete();
            }

            IsConfirmSuccess = true;
            return base.Confirm();
        }
             
        #endregion

        #region Event Handlers

        private void RequestFormBase_Loaded(object sender, RoutedEventArgs e)
        {
            _Request = Data.RequestDB.GetRequestByID(_RequestID);
            _ChangeAddress = new UserControls.ChangeAddressUserControl(_RequestID, _Request.TelephoneNo);
            ChangeAddressLable.Content = _ChangeAddress;
            ChangeAddressLable.DataContext = _ChangeAddress;
        }

        #endregion
    }
}
