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
using CookComputing.XmlRpc;
using CRM.Data.Services;
using System.Data.OleDb;
using System.Data;

namespace CRM.Application.Views
{
    public partial class TestForm : Window
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            bool isConfirmed = false;
            bool isDischarge = false;
            bool isTechnicalFailed = false;
            int cabinetID = 0;

            string requestID = SaveFailure117Kermanshah(38356791, 1, null, out result, out isConfirmed, out isDischarge, out isTechnicalFailed, out cabinetID);


        }

        public string SaveFailure117Kermanshah(long telephoneNo, long callingNo, byte[] recordeSound, out bool result, out bool isConfirmed, out bool isDischarge, out bool isTechnicalFailed, out int cabinetID)
        {
            isTechnicalFailed = false;
            isDischarge = false;
            cabinetID = 0;

            try
            {
                telephoneNo = 8300000000 + telephoneNo;

                if (TelephoneDB.HasTelephoneNo(telephoneNo))
                {
                    Telephone telephone = TelephoneDB.GetTelephoneByTelePhoneNo(telephoneNo);

                    if (telephone != null)
                    {
                        if (telephone.Status == (byte)DB.TelephoneStatus.Discharge || telephone.Status == (byte)DB.TelephoneStatus.Cut)
                        {
                            isDischarge = true;
                            result = false;
                            isConfirmed = false;
                            return "";
                        }
                    }
                }

                Request request = new Request();
                Data.Failure117 failureRequest = new Data.Failure117();
                request = Failure117DB.GetFailureRequest(telephoneNo);
                TechnicalInfoFailure117 technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo(telephoneNo);

                if (request != null)
                {
                    if (request.EndDate != null && request.EndDate > DB.GetServerDate())
                    {
                        result = false;
                        isConfirmed = true;
                    }
                    else
                    {
                        if (request.EndDate == null || request.EndDate > DB.GetServerDate())
                        {
                            result = false;
                            isConfirmed = false;
                        }
                        else
                        {
                            request = new Request();

                            int centerID = 0;
                            if (TelephoneDB.HasTelephoneNo(telephoneNo))
                            {
                                centerID = CenterDB.GetCenterIDbyTelephoneNo(telephoneNo);

                                int cabinetNo = Failure117DB.GetCabinetNobyTelephoneNo(telephoneNo);
                                if (cabinetNo != 0)
                                {
                                    if (Failure117CabenitAccuracyDB.CheckCabinetAccuracy(cabinetNo, centerID))
                                    {
                                        isTechnicalFailed = true;
                                        isDischarge = false;
                                        result = false;
                                        isConfirmed = false;
                                        cabinetID = cabinetNo;
                                        return "";
                                    }
                                    else
                                    {
                                        int postNo = Failure117DB.GetPostNobyTelephoneNo(telephoneNo);
                                        if (postNo != 0)
                                        {
                                            if (Failure117CabenitAccuracyDB.CheckPostAccuracy(cabinetNo, postNo, centerID))
                                            {
                                                isTechnicalFailed = true;
                                                isDischarge = false;
                                                result = false;
                                                isConfirmed = false;
                                                return "";
                                            }
                                            else
                                            {
                                                if (Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(telephoneNo, centerID))
                                                {
                                                    isTechnicalFailed = true;
                                                    isDischarge = false;
                                                    result = false;
                                                    isConfirmed = false;
                                                    return "";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                                centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(telephoneNo);

                            request.IsViewed = false;
                            request.TelephoneNo = telephoneNo;
                            request.RequestTypeID = (byte)DB.RequestType.Failure117;
                            request.CenterID = centerID;
                            request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                            request.RequestDate = DB.GetServerDate();
                            request.InsertDate = DB.GetServerDate();
                            request.CreatorUserID = 6;
                            request.ModifyUserID = 6;
                            request.IsWaitingList = false;
                            request.IsCancelation = false;
                            request.IsVisible = true;
                            Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                            request.StatusID = status.ID;

                            if (technicalInfo != null)
                            {
                                failureRequest.CabinetNo = technicalInfo.CabinetNo;
                                failureRequest.CabinetMarkazi = technicalInfo.CabinetInputNumber;
                                failureRequest.PostNo = technicalInfo.PostNo;
                                failureRequest.PostEtesali = technicalInfo.ConnectionNo;
                            }
                            else
                            {
                                failureRequest.CabinetNo = "";
                                failureRequest.CabinetMarkazi = "";
                                failureRequest.PostNo = "";
                                failureRequest.PostEtesali = "";
                            }

                            failureRequest.CallingNo = callingNo;
                            failureRequest.RecordeSound = recordeSound;

                            RequestForFailure117.SaveFailureRequest(request, failureRequest, true);

                            result = true;
                            isConfirmed = false;
                        }
                    }
                }
                else
                {
                    request = new Request();

                    int centerID = 0;
                    if (TelephoneDB.HasTelephoneNo(telephoneNo))
                    {
                        centerID = CenterDB.GetCenterIDbyTelephoneNo(telephoneNo);

                        int cabinetNo = Failure117DB.GetCabinetNobyTelephoneNo(telephoneNo);
                        if (cabinetNo != 0)
                        {
                            if (Failure117CabenitAccuracyDB.CheckCabinetAccuracy(cabinetNo, centerID))
                            {
                                isTechnicalFailed = true;
                                isDischarge = false;
                                result = false;
                                isConfirmed = false;
                                cabinetID = cabinetNo;
                                return "";
                            }
                            else
                            {
                                int postNo = Failure117DB.GetPostNobyTelephoneNo(telephoneNo);
                                if (postNo != 0)
                                {
                                    if (Failure117CabenitAccuracyDB.CheckPostAccuracy(cabinetNo, postNo, centerID))
                                    {
                                        isTechnicalFailed = true;
                                        isDischarge = false;
                                        result = false;
                                        isConfirmed = false;
                                        return "";
                                    }
                                    else
                                    {
                                        if (Failure117CabenitAccuracyDB.CheckTelephoneAccuracy(telephoneNo, centerID))
                                        {
                                            isTechnicalFailed = true;
                                            isDischarge = false;
                                            result = false;
                                            isConfirmed = false;
                                            return "";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                        centerID = TelephoneDB.GetCenterIDbyTelephoneNoTemp(telephoneNo);

                    request.IsViewed = false;
                    request.TelephoneNo = telephoneNo;
                    request.RequestTypeID = (byte)DB.RequestType.Failure117;
                    request.CenterID = centerID;
                    request.RequestPaymentTypeID = (byte)DB.PaymentType.NoPayment;
                    request.RequestDate = DB.GetServerDate();
                    request.InsertDate = DB.GetServerDate();
                    request.CreatorUserID = 6;
                    request.ModifyUserID = 6;
                    request.IsWaitingList = false;
                    request.IsCancelation = false;
                    request.IsVisible = true;
                    Data.Status status = DB.GetStatus(request.RequestTypeID, (int)DB.RequestStatusType.Start);
                    request.StatusID = status.ID;

                    if (technicalInfo != null)
                    {
                        failureRequest.CabinetNo = technicalInfo.CabinetNo;
                        failureRequest.CabinetMarkazi = technicalInfo.CabinetInputNumber;
                        failureRequest.PostNo = technicalInfo.PostNo;
                        failureRequest.PostEtesali = technicalInfo.ConnectionNo;
                    }
                    else
                    {
                        failureRequest.CabinetNo = "";
                        failureRequest.CabinetMarkazi = "";
                        failureRequest.PostNo = "";
                        failureRequest.PostEtesali = "";
                    }

                    failureRequest.CallingNo = callingNo;
                    failureRequest.RecordeSound = recordeSound;

                    RequestForFailure117.SaveFailureRequest(request, failureRequest, true);

                    result = true;
                    isConfirmed = false;
                }

                return request.ID.ToString();

            }
            catch (Exception ex)
            {
                result = false;
                isConfirmed = false;
                return "";
            }
        }

        private void InstalmentButton_Click(object sender, RoutedEventArgs e)
        {

            //List<RequestPayment> payments = PaymentDB.GetRequestPaymentforInstalment();

            //Request request = new Request();
            //int serviceID = 0;
            //ADSLService service = null;

            //foreach (RequestPayment currentPayment in payments)
            //{
            //    request = RequestDB.GetRequestByID(currentPayment.RequestID);

            //    if (request.EndDate != null)
            //    {
            //        if (!PaymentDB.HasInstalment(currentPayment.ID))
            //        {
            //            if (request.RequestTypeID == 35)
            //                serviceID = PaymentDB.GetADSLServiceServiceID(currentPayment.RequestID, 35);

            //            if (request.RequestTypeID == 38)
            //                serviceID = PaymentDB.GetADSLServiceServiceID(currentPayment.RequestID, 38);

            //            service = PaymentDB.GetADSLServicebyID(serviceID);

            //            int _floorValue = 1000;
            //            List<InstallmentRequestPayment> installmentRequestPayments = new List<InstallmentRequestPayment>();
            //            int PaymentAmountEachPart = 0;
            //            string startDate = "1393/05/01";// DB.GetServerDate().ToPersian(Date.DateStringType.Short);
            //            string endateCount = Helper.AddMonthToPersianDate(startDate, (int)service.DurationID);
            //            string endDate = Helper.AddMonthToPersianDate(startDate, (int)service.DurationID);

            //            string startDateEachPart = startDate;

            //            PaymentAmountEachPart = (int)(service.PriceSum / (decimal)service.DurationID);

            //            for (int i = 1; i <= service.DurationID; i++)
            //            {
            //                InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
            //                int dateEachPart = 1;
            //                installmentRequestPayment.RequestPaymentID = currentPayment.ID;
            //                installmentRequestPayment.TelephoneNo = request.TelephoneNo;
            //                installmentRequestPayment.IsCheque = false;
            //                installmentRequestPayment.IsPaid = false;
            //                installmentRequestPayment.IsDeleted = false;

            //                installmentRequestPayment.StartDate = Date.PersianToGregorian(startDateEachPart).Value.Date;
            //                string endDateEachPart = Helper.GetLastDayOfMount(startDateEachPart);

            //                installmentRequestPayment.EndDate = Date.PersianToGregorian(endDateEachPart).Value.Date;

            //                if (service.DurationID == i)
            //                    installmentRequestPayment.Cost = (long)(service.PriceSum - (decimal)installmentRequestPayments.Sum(t => t.Cost));
            //                else
            //                    installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

            //                startDateEachPart = Helper.GetPersianDateAddDays(endDateEachPart, 1);

            //                installmentRequestPayments.Add(installmentRequestPayment);

            //                installmentRequestPayment.Detach();
            //                DB.Save(installmentRequestPayment, true);
            //            }
            //        }
            //    }

            //}
        }

        private void DownLoadButton_Click(object sender, RoutedEventArgs e)
        {
//            try
//            {

//                Microsoft.Win32.SaveFileDialog sa = new Microsoft.Win32.SaveFileDialog();
//                sa.Filter = "xls|Excel";
//                if (sa.ShowDialog() == true)
//                {
//                    string connectionString =
//                    string.Format(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=Excel 8.0;", sa.FileName);

//                    using (OleDbConnection Connection =
//                        new OleDbConnection(connectionString))
//                    {
//                        Connection.Open();

//                        using (OleDbCommand command =
//                        new OleDbCommand())
//                        {
//                            command.Connection = Connection;

//                            command.CommandText = @"CREATE TABLE 
//                                    [Modem](SerialNo text(200))";

//                            command.ExecuteNonQuery();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
                if (op.ShowDialog() != true)
                    return;

                string connectionString = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=Excel 8.0;", op.FileName);

                using (OleDbConnection Connection =
                    new OleDbConnection(connectionString))
                {
                    Connection.Open();

                    using (OleDbCommand command =
                    new OleDbCommand())
                    {
                        command.Connection = Connection;

                        DataTable dt = null;

                        using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                        {
                            command.CommandText = "SELECT * FROM [Modem$]";
                            dt = new DataTable();
                            adapter.SelectCommand = command;
                            adapter.Fill(dt);

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                try
                                {
                                    string serial = dt.Rows[i]["SerialNo"].ToString();

                                    ADSLModemProperty modem = new ADSLModemProperty();
                                    modem.SerialNo = serial;
                                    modem.CenterID = 1;
                                    modem.ADSLModemID = 9;
                                    modem.TelephoneNo = null;
                                    modem.MACAddress = null;
                                    modem.Status = (byte)DB.ADSLModemStatus.NotSold;

                                    modem.Detach();
                                    DB.Save(modem, true);
                                }
                                catch
                                {
                                    continue;
                                }
                            }

                            System.Windows.MessageBox.Show(string.Format("{0} Records Inserted Successfully", dt.Rows.Count));
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
