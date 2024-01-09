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
    /// Interaction logic for ChangeLocationMDFForm.xaml
    /// </summary>
    public partial class ChangeLocationMDFForm : Local.PopupWindow
    {
        private long _RequestID;
        ChangeLocation changeLocation;
        Bucht bucht = new Bucht();
        InvestigatePossibility _InvestigatePossibility { get; set; }
        Telephone oldTelephone = new Telephone();
        public ChangeLocationMDFForm()
        {
            InitializeComponent();
        }

        public ChangeLocationMDFForm(long requestID)
            : this()
        {
            this._RequestID = requestID;
        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID((long)_RequestID);
            _InvestigatePossibility = Data.InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_RequestID).Take(1).SingleOrDefault();

            if (_InvestigatePossibility.BuchtID != null)
                ReservBuchtTextBox.Text = DB.GetConnectionByBuchtID((long)_InvestigatePossibility.BuchtID);
            else
                ReservBuchtTextBox.Text = "بوخت رزرو یافت نشد";

            OldTelTextBox.Text = changeLocation.OldTelephone.ToString();
            NewTelTextBox.Text = changeLocation.NewTelephone.ToString();
            oldTelephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.OldTelephone);
            bucht = Data.BuchtDB.GetBuchtBySwitchPortID((int)oldTelephone.SwitchPortID);
            BuchtTextBox.Text = DB.GetConnectionByBuchtID(bucht.ID);
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    Bucht newBucht = Data.BuchtDB.GetBuchtByID((long)_InvestigatePossibility.BuchtID);
                    if (changeLocation.NewTelephone != null)
                    {
                        Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo((long)changeLocation.NewTelephone);
                        telephone.Status = (byte)DB.TelephoneStatus.Connecting;
                        telephone.InstallAddressID = changeLocation.NewInstallAddressID;
                        telephone.Detach();
                        DB.Save(telephone);
                    }

                    newBucht.Status = (byte)DB.BuchtStatus.Connection;
                    newBucht.SwitchPortID = bucht.SwitchPortID;
                    newBucht.Detach();
                    DB.Save(newBucht);

                    bucht.Status = (byte)DB.BuchtStatus.Free;
                    bucht.SwitchPortID = null;
                    bucht.Detach();
                    DB.Save(bucht);

                    if (changeLocation.NewTelephone != null)
                    {
                        oldTelephone.Status = (byte)DB.TelephoneStatus.Free;
                        oldTelephone.CustomerID = null;
                        oldTelephone.InstallAddressID = changeLocation.NewInstallAddressID;
                        oldTelephone.Detach();
                        DB.Save(oldTelephone);
                    }
                    else
                    {
                        oldTelephone.InstallAddressID = changeLocation.NewInstallAddressID;
                        oldTelephone.Detach();
                        DB.Save(oldTelephone);

                    }

                    ts.Complete();
                }

                ShowSuccessMessage("ذخیره انجام شد");
            }
            catch (Exception ex)
            {
                ShowErrorMessage("ذخیره انجام نشد", ex);
            }

        }
    }
}
