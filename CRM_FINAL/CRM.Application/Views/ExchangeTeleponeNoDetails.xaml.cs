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
    /// Interaction logic for ExchangeSwitchDetails.xaml
    /// </summary>
    public partial class ExchangeSwitchDetails : Local.PopupWindow
    {
        ExchangeTelephoneNo exchangeTelephoneNo;
        private long _requestID;
        public ExchangeSwitchDetails()
        {
            InitializeComponent();
            Initialize();
        }
            public ExchangeSwitchDetails(long requestID):this()
        {
            _requestID = requestID;
            Initialize();
        }
        
        private void Initialize()
        {
            exchangeTelephoneNo = new ExchangeTelephoneNo();
            BeforSwitchComboBox.ItemsSource = Data.SwitchDB.GetSwitchCheckable();
            AfterSwitchComboBox.ItemsSource = Data.SwitchDB.GetSwitchCheckable();
        }

        private void BeforSwitchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BeforSwitchComboBox.SelectedValue != null)
             ToBeforTelNoComboBox.ItemsSource = FromBeforTelNoComboBox.ItemsSource = Data.TelephoneDB.GetTelephoneBySwitchID((int)BeforSwitchComboBox.SelectedValue);
        }

        private void AfterSwitchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AfterSwitchComboBox.SelectedValue != null)
              ToAfterTelNoComboBox.ItemsSource = FromAfterTelNoComboBox.ItemsSource = Data.TelephoneDB.GetTelephoneBySwitchID((int)AfterSwitchComboBox.SelectedValue);
        }

        private void SaveForm(object sender, RoutedEventArgs e)
        {
            if (Codes.Validation.WindowIsValid.IsValid(this) == false)
            {
                return;
            }
            try
            {
                exchangeTelephoneNo = this.DataContext as ExchangeTelephoneNo;
                exchangeTelephoneNo.RequestID = _requestID;

                Request request = Data.RequestDB.GetRequestByID(_requestID);
                List<Telephone> BeforTelephoneList = Data.TelephoneDB.GetTelephoneFromTelToTel((long)FromBeforTelNoComboBox.SelectedValue, (long)ToBeforTelNoComboBox.SelectedValue);
                List<Telephone> AfterTelephoneList = Data.TelephoneDB.GetTelephoneFromTelToTel((long)FromAfterTelNoComboBox.SelectedValue, (long)ToAfterTelNoComboBox.SelectedValue);
                exchangeTelephoneNo.FromSwitchPreCode = (int)BeforTelephoneList[0].SwitchPrecodeID;
                exchangeTelephoneNo.ToSwitchPreCode = (int)AfterTelephoneList[0].SwitchPrecodeID;
                using (TransactionScope ts = new TransactionScope())
                {
                    
                    IssueWiring issueWiring = new IssueWiring();
                    issueWiring.InsertDate = DB.GetServerDate();
                    issueWiring.RequestID = _requestID;
                    issueWiring.WiringNo = _requestID.ToString() + "-" + DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                    issueWiring.WiringTypeID = 0;
                    issueWiring.WiringIssueDate = DB.GetServerDate();
                    issueWiring.PrintCount = 0;
                    issueWiring.IsPrinted = false;
                    issueWiring.Status = 0;
                    issueWiring.Detach();
                    DB.Save(issueWiring);



                    List<Wiring> wiringList = new List<Wiring>();
                    for (int i = 0; i < BeforTelephoneList.Count; i++)
                    {
                        Wiring wiring = new Wiring();
                        //AfterTelephoneList[i].SwitchPrecodeID = BeforTelephoneList[i].SwitchPrecodeID;
                        //BeforTelephoneList[i].SwitchPrecodeID = null;

                        AfterTelephoneList[i].PreCodeType = BeforTelephoneList[i].PreCodeType;
                        BeforTelephoneList[i].PreCodeType = null;

                        throw new Exception("خطا در ثبت اطلاعات");

                        //AfterTelephoneList[i].SwitchPortID = BeforTelephoneList[i].SwitchPortID;
                        //BeforTelephoneList[i].SwitchPortID = DB.GetSwitchPortIDTypeByTelephone(BeforTelephoneList[i]);

                        //AfterTelephoneList[i].IsVIP = BeforTelephoneList[i].IsVIP;
                        //BeforTelephoneList[i].IsVIP = null;

                        //AfterTelephoneList[i].IsRound = BeforTelephoneList[i].IsRound;
                        //BeforTelephoneList[i].IsRound = null;

                        //AfterTelephoneList[i].CenterID = BeforTelephoneList[i].CenterID;

                        // To Do
                        // Transport specialService with change PhoneNumber
                        //  AfterTelephoneList[i].LastSpecialServiceStatus = BeforTelephoneList[i].LastSpecialServiceStatus;
                        AfterTelephoneList[i].Status = BeforTelephoneList[i].Status;

                        AfterTelephoneList[i].Detach();
                        BeforTelephoneList[i].Detach();

                        wiring.NewTelephoneNo = AfterTelephoneList[i].TelephoneNo;
                        wiring.OldTelephoneNo = BeforTelephoneList[i].TelephoneNo;
                        wiring.IssueWiringID = issueWiring.ID;
                        wiring.RequestID = _requestID;
                        wiring.Status = request.StatusID;
                        wiringList.Add(wiring);
                    }
                    foreach (Wiring item in wiringList)
                        item.Detach();
                    DB.UpdateAll(AfterTelephoneList);
                    DB.UpdateAll(BeforTelephoneList);
                    DB.SaveAll(wiringList);
                    DB.Save(exchangeTelephoneNo);
                    ts.Complete();
                }
                ShowSuccessMessage("برگردان انجام شد");
            }
            catch(Exception ex)
            {
                ShowErrorMessage("برگردان انجام نشد", ex);
            }


        }

        private void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            this.DataContext = exchangeTelephoneNo;
        }
       
    }
}
