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
using System.Globalization;
using CookComputing.XmlRpc;
using CRM.Data.Services;

namespace CRMConvertApplication
{
    public partial class InstalForm : Window
    {
        public enum DateStringType { Short, Long, Compelete, TwoDigitsYear, DateTime, Year, Time }

        public InstalForm()
        {
            InitializeComponent();
        }

        public void GenerateInstalment()
        {
            long prePaymentAmount = 0;

            List<ADSL> aDSLList = ADSLDB.GetADSLwithInstalmentService();

            foreach (ADSL aDSL in aDSLList)
            {
                //ADSL aDSL = ADSLDB.GetADSLbyTelephoneNo(2324542515);
                if (aDSL.ExpDate != null)
                {
                    DateTime startcycle = Convert.ToDateTime("2013-11-22");

                    if ((Convert.ToInt32(((DateTime)aDSL.ExpDate - startcycle).TotalDays)) > 0)
                    {
                        InstallmentRequestPayment_Temp instalment_temp = InstalmentDB.GetInstalmentTempbyID(Convert.ToInt64(aDSL.TelephoneNo));

                        if (instalment_temp != null)
                        {
                            string realFirstTimeString = GetUserAuditChanges(aDSL.UserID.ToString(), "All", GetPersianDate(Convert.ToDateTime("2011-01-01"), DateStringType.Short), GetPersianDate(DateTime.Now, DateStringType.Short));

                            DateTime realFirstTime;
                            if (!string.IsNullOrWhiteSpace(realFirstTimeString))
                                realFirstTime = Convert.ToDateTime(realFirstTimeString);
                            else
                            {
                                if (aDSL.InstallDate != null)
                                    realFirstTime = (DateTime)aDSL.InstallDate;
                                else
                                    realFirstTime = (DateTime)aDSL.InsertDate;
                            }

                            if (((Convert.ToInt32((Convert.ToDateTime("2013-01-19") - realFirstTime).TotalDays)) >= -1) && ((Convert.ToInt32((realFirstTime - Convert.ToDateTime("2012-11-21")).TotalDays)) > 0))
                            {
                                instalment_temp.SUMPaid = Convert.ToInt64(instalment_temp._915) + Convert.ToInt64(instalment_temp._916) + Convert.ToInt64(instalment_temp._921) + Convert.ToInt64(instalment_temp._922) + Convert.ToInt64(instalment_temp._923) + Convert.ToInt64(instalment_temp._924);
                                prePaymentAmount = Convert.ToInt64(instalment_temp.SUMPaid);

                                GeneratePaidInstalment(instalment_temp, true, true, true, true, true, true);
                                SaveInstalments(aDSL, prePaymentAmount);
                            }
                            else
                            {
                                if (((Convert.ToInt32((Convert.ToDateTime("2013-03-20") - realFirstTime).TotalDays)) >= 0) && ((Convert.ToInt32((realFirstTime - Convert.ToDateTime("2013-01-20")).TotalDays)) > 0))
                                {
                                    instalment_temp.SUMPaid = Convert.ToInt64(instalment_temp._916) + Convert.ToInt64(instalment_temp._921) + Convert.ToInt64(instalment_temp._922) + Convert.ToInt64(instalment_temp._923) + Convert.ToInt64(instalment_temp._924);
                                    prePaymentAmount = Convert.ToInt64(instalment_temp.SUMPaid);

                                    GeneratePaidInstalment(instalment_temp, false, true, true, true, true, true);
                                    SaveInstalments(aDSL, prePaymentAmount);
                                }
                                else
                                {
                                    if (((Convert.ToInt32((Convert.ToDateTime("2013-05-21") - realFirstTime).TotalDays)) >= 0) && ((Convert.ToInt32((realFirstTime - Convert.ToDateTime("2013-03-21")).TotalDays)) > 0))
                                    {
                                        instalment_temp.SUMPaid = Convert.ToInt64(instalment_temp._921) + Convert.ToInt64(instalment_temp._922) + Convert.ToInt64(instalment_temp._923) + Convert.ToInt64(instalment_temp._924);
                                        prePaymentAmount = Convert.ToInt64(instalment_temp.SUMPaid);

                                        GeneratePaidInstalment(instalment_temp, false, false, true, true, true, true);
                                        SaveInstalments(aDSL, prePaymentAmount);
                                    }
                                    else
                                    {
                                        if (((Convert.ToInt32((Convert.ToDateTime("2013-07-22") - realFirstTime).TotalDays)) >= 0) && ((Convert.ToInt32((realFirstTime - Convert.ToDateTime("2013-05-22")).TotalDays)) > 0))
                                        {
                                            instalment_temp.SUMPaid = Convert.ToInt64(instalment_temp._922) + Convert.ToInt64(instalment_temp._923) + Convert.ToInt64(instalment_temp._924);
                                            prePaymentAmount = Convert.ToInt64(instalment_temp.SUMPaid);

                                            GeneratePaidInstalment(instalment_temp, false, false, false, true, true, true);
                                            SaveInstalments(aDSL, prePaymentAmount);
                                        }
                                        else
                                        {
                                            if (((Convert.ToInt32((Convert.ToDateTime("2013-09-22") - realFirstTime).TotalDays)) >= 0) && ((Convert.ToInt32((realFirstTime - Convert.ToDateTime("2013-07-23")).TotalDays)) > 0))
                                            {
                                                instalment_temp.SUMPaid = Convert.ToInt64(instalment_temp._923) + Convert.ToInt64(instalment_temp._924);
                                                prePaymentAmount = Convert.ToInt64(instalment_temp.SUMPaid);

                                                GeneratePaidInstalment(instalment_temp, false, false, false, false, true, true);
                                                SaveInstalments(aDSL, prePaymentAmount);
                                            }
                                            else
                                            {
                                                if (((Convert.ToInt32((Convert.ToDateTime("2013-11-21") - realFirstTime).TotalDays)) >= 0) && ((Convert.ToInt32((realFirstTime - Convert.ToDateTime("2013-09-23")).TotalDays)) > 0))
                                                {
                                                    instalment_temp.SUMPaid = Convert.ToInt64(instalment_temp._924);
                                                    prePaymentAmount = Convert.ToInt64(instalment_temp.SUMPaid);

                                                    GeneratePaidInstalment(instalment_temp, false, false, false, false, false, true);
                                                    SaveInstalments(aDSL, prePaymentAmount);
                                                }
                                                else
                                                    SaveInstalments(aDSL, 0);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            SaveInstalments(aDSL, 0);
                        }
                    }
                }
            }
        }

        private void SaveInstalments(ADSL aDSL, long prePaymentAmount)
        {
            int _floorValue = 1000;
            int PaymentAmountEachPart = 0;
            DateTime startcycle = Convert.ToDateTime("2013-11-22");

            if ((Convert.ToInt32((Convert.ToDateTime(aDSL.InstallDate) - startcycle).TotalDays)) > 0)
                startcycle = Convert.ToDateTime(aDSL.InstallDate);

            List<InstallmentRequestPayment> installmentRequestPayments = new List<InstallmentRequestPayment>();
            ADSLService service = ADSLServiceDB.GetADSLServicebyID((int)aDSL.TariffID);

            if (service != null)
            {
                int installmentCount = 0;

                installmentCount = ((Convert.ToInt32(((DateTime)aDSL.ExpDate - startcycle).TotalDays)) / 30) + 1;

                if (installmentCount > 12)
                    installmentCount = 12;

                //string endateCount = AddMonthToPersianDate(startDate, installmentCount);
                //string endDate = EndDatePicker.SelectedDate.ToPersian(Date.DateStringType.Short);
                //string endDate = AddMonthToPersianDate(startDate, installmentCount);

                decimal installmentPayment = (decimal)service.PriceSum - (decimal)prePaymentAmount;

                string startDateEachPart = GetPersianDate(startcycle, DateStringType.Short);

                PaymentAmountEachPart = (int)(installmentPayment / (decimal)installmentCount);

                //int count = installmentCount;

                for (int i = 1; i <= installmentCount; i++)
                {
                    InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
                    int dateEachPart = 1;
                    installmentRequestPayment.RequestPaymentID = null;
                    installmentRequestPayment.TelephoneNo = Convert.ToInt64(aDSL.TelephoneNo);
                    installmentRequestPayment.IsCheque = false;
                    installmentRequestPayment.IsPaid = false;

                    installmentRequestPayment.StartDate = PersianToGregorian(startDateEachPart).Value.Date;
                    string endDateEachPart = GetLastDayOfMount(startDateEachPart);

                    installmentRequestPayment.EndDate = PersianToGregorian(endDateEachPart).Value.Date;

                    if (installmentCount == i)
                        installmentRequestPayment.Cost = (long)(installmentPayment - (decimal)installmentRequestPayments.Sum(t => t.Cost));
                    else
                        installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

                    startDateEachPart = GetPersianDateAddDays(endDateEachPart, 1);

                    installmentRequestPayments.Add(installmentRequestPayment);
                }

                DB.SaveAll(installmentRequestPayments);
            }
        }

        public static DateTime? PersianToGregorian(string persianDate)
        {
            if (string.IsNullOrEmpty(persianDate)) return null;

            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                string[] tokens = persianDate.Split('/');

                if (tokens[0].Length == 2)
                    tokens[0] = "13" + tokens[0];
                int year = Int32.Parse(tokens[0]);
                int month = Int32.Parse(tokens[1]);
                int day = Int32.Parse(tokens[2]);
                return PersianToGregorian(day, month, year);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DateTime PersianToGregorian(int day, int month, int year)
        {
            if (day < 1 || day > 31 || month < 1 || month > 12 || year < 1300 || year > 1400) throw new ApplicationException("تاریخ نامعتبر است");

            PersianCalendar persianCalendar = new PersianCalendar();

            return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public static string GetPersianDateAddDays(string startDateEachPart, int addDay)
        {
            PersianCalendar persianCalendar = new PersianCalendar();
            return GetPersianDate(persianCalendar.AddDays(PersianToGregorian(startDateEachPart).Value.Date, addDay), DateStringType.Short);
        }

        public static string GetPersianDate(DateTime date, DateStringType type)
        {
            DateTime minDate = new DateTime(1000, 1, 1);
            DateTime maxDate = new DateTime(9999, 1, 1);

            if (date < minDate || date > maxDate) return string.Empty;

            PersianCalendar persianCalendar = new PersianCalendar();

            string result = string.Empty;

            DayOfWeek dayOfWeek = persianCalendar.GetDayOfWeek(date);
            int dayOfMonth = persianCalendar.GetDayOfMonth(date);
            int monthNumber = persianCalendar.GetMonth(date);
            int year = persianCalendar.GetYear(date);

            switch (type)
            {
                case DateStringType.Short:
                    result = string.Format("{0}/{1}/{2}", year.ToString(), monthNumber.ToString(), dayOfMonth.ToString());
                    break;

                //case DateStringType.Long:
                //    result = string.Format("{0} {1} {2}", dayOfMonth.ToString(), PersianMonths[monthNumber], year.ToString());
                //    break;

                //case DateStringType.Compelete:
                //    result = string.Format("{0}، {1} {2} {3}", PersianDayOfWeek(dayOfWeek), dayOfMonth.ToString(), PersianMonths[monthNumber], year.ToString());
                //    break;

                case DateStringType.TwoDigitsYear:
                    result = string.Format("{0}/{1}/{2}", (year - 1300).ToString(), monthNumber.ToString(), dayOfMonth.ToString());
                    break;

                case DateStringType.DateTime:
                    result = string.Format("{3}  {0}/{1}/{2}", (year - 1300).ToString(), monthNumber.ToString(), dayOfMonth.ToString(), GetTime(date));
                    break;

                case DateStringType.Year:
                    result = year.ToString();
                    break;

                case DateStringType.Time:
                    result = string.Format("{0}", GetTime(date));
                    break;
                default:
                    break;
            }
            return result;
        }

        public static string GetLastDayOfMount(string persianShortDate)
        {

            string[] tokens = persianShortDate.Split('/');
            if (tokens[0].Length == 2)
                tokens[0] = "13" + tokens[0];
            int year = Int32.Parse(tokens[0]);
            int month = Int32.Parse(tokens[1]);
            int day = Int32.Parse(tokens[2]);
            return string.Format("{0}/{1}/{2}", year.ToString(), month.ToString(), (month > 6 ? (month == 12 && (year % 4) != 3) ? 29 : 30 : 31));

        }

        public static string AddMonthToPersianDate(string persianShortDate, int monthsToAdd)
        {
            string[] tokens = persianShortDate.Split('/');
            if (tokens[0].Length == 2)
                tokens[0] = "13" + tokens[0];
            int year = Int32.Parse(tokens[0]);
            int month = Int32.Parse(tokens[1]);
            int day = Int32.Parse(tokens[2]);

            if ((month + monthsToAdd) > 12)
            {
                year += (month + monthsToAdd) / 12;
                month = (month + monthsToAdd) % 12;
            }
            else
            {
                month = (month + monthsToAdd);
            }

            return string.Format("{0}/{1}/{2}", year.ToString(), month.ToString(), day.ToString());
        }

        public static string GetTime(DateTime date)
        {
            return date.ToString("HH:mm:ss");
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            GenerateInstalment();
        }

        private void PaidButton_Click(object sender, RoutedEventArgs e)
        {
            //GetUserAuditChanges("4258", "All", GetPersianDate(Convert.ToDateTime("2011-01-01"), DateStringType.Short), GetPersianDate(DateTime.Now, DateStringType.Short));
        }

        private void GeneratePaidInstalment(InstallmentRequestPayment_Temp instalment_temp, bool is915, bool is916, bool is921, bool is922, bool is923, bool is924)
        {
            List<InstallmentRequestPayment> installmentRequestPayments = new List<InstallmentRequestPayment>();
            InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();

            if (instalment_temp._915 != 0 && is915 == true)
            {
                installmentRequestPayment = new InstallmentRequestPayment();
                installmentRequestPayment.RequestPaymentID = null;
                installmentRequestPayment.TelephoneNo = Convert.ToInt64(instalment_temp.Phone);
                installmentRequestPayment.IsCheque = false;
                installmentRequestPayment.IsPaid = true;

                installmentRequestPayment.StartDate = Convert.ToDateTime("2012-11-21");
                installmentRequestPayment.EndDate = Convert.ToDateTime("2013-01-19");

                installmentRequestPayment.Cost = Convert.ToInt64(instalment_temp._915);

                installmentRequestPayments.Add(installmentRequestPayment);
            }

            if (instalment_temp._916 != 0 && is916 == true)
            {
                installmentRequestPayment = new InstallmentRequestPayment();
                installmentRequestPayment.RequestPaymentID = null;
                installmentRequestPayment.TelephoneNo = Convert.ToInt64(instalment_temp.Phone);
                installmentRequestPayment.IsCheque = false;
                installmentRequestPayment.IsPaid = true;

                installmentRequestPayment.StartDate = Convert.ToDateTime("2013-01-20");
                installmentRequestPayment.EndDate = Convert.ToDateTime("2013-03-20");

                installmentRequestPayment.Cost = Convert.ToInt64(instalment_temp._916);

                installmentRequestPayments.Add(installmentRequestPayment);
            }

            if (instalment_temp._921 != 0 && is921 == true)
            {
                installmentRequestPayment = new InstallmentRequestPayment();
                installmentRequestPayment.RequestPaymentID = null;
                installmentRequestPayment.TelephoneNo = Convert.ToInt64(instalment_temp.Phone);
                installmentRequestPayment.IsCheque = false;
                installmentRequestPayment.IsPaid = true;

                installmentRequestPayment.StartDate = Convert.ToDateTime("2013-03-21");
                installmentRequestPayment.EndDate = Convert.ToDateTime("2013-05-21");

                installmentRequestPayment.Cost = Convert.ToInt64(instalment_temp._921);

                installmentRequestPayments.Add(installmentRequestPayment);
            }

            if (instalment_temp._922 != 0 && is922 == true)
            {
                installmentRequestPayment = new InstallmentRequestPayment();
                installmentRequestPayment.RequestPaymentID = null;
                installmentRequestPayment.TelephoneNo = Convert.ToInt64(instalment_temp.Phone);
                installmentRequestPayment.IsCheque = false;
                installmentRequestPayment.IsPaid = true;

                installmentRequestPayment.StartDate = Convert.ToDateTime("2013-05-22");
                installmentRequestPayment.EndDate = Convert.ToDateTime("2013-07-22");

                installmentRequestPayment.Cost = Convert.ToInt64(instalment_temp._922);

                installmentRequestPayments.Add(installmentRequestPayment);
            }

            if (instalment_temp._923 != 0 && is923 == true)
            {
                installmentRequestPayment = new InstallmentRequestPayment();
                installmentRequestPayment.RequestPaymentID = null;
                installmentRequestPayment.TelephoneNo = Convert.ToInt64(instalment_temp.Phone);
                installmentRequestPayment.IsCheque = false;
                installmentRequestPayment.IsPaid = true;

                installmentRequestPayment.StartDate = Convert.ToDateTime("2013-07-23");
                installmentRequestPayment.EndDate = Convert.ToDateTime("2013-09-22");

                installmentRequestPayment.Cost = Convert.ToInt64(instalment_temp._923);

                installmentRequestPayments.Add(installmentRequestPayment);
            }

            if (instalment_temp._924 != 0 && is924 == true)
            {
                installmentRequestPayment = new InstallmentRequestPayment();
                installmentRequestPayment.RequestPaymentID = null;
                installmentRequestPayment.TelephoneNo = Convert.ToInt64(instalment_temp.Phone);
                installmentRequestPayment.IsCheque = false;
                installmentRequestPayment.IsPaid = true;

                installmentRequestPayment.StartDate = Convert.ToDateTime("2013-09-23");
                installmentRequestPayment.EndDate = Convert.ToDateTime("2013-11-21");

                installmentRequestPayment.Cost = Convert.ToInt64(instalment_temp._924);

                installmentRequestPayments.Add(installmentRequestPayment);
            }

            DB.SaveAll(installmentRequestPayments);
        }

        private string GetUserAuditChanges(string IBS_User_ID, string PConnectionType, string startDate, string endDate)
        {
            try
            {
                IBSngService.Isend_recv ibsngService = (IBSngService.Isend_recv)XmlRpcProxyGen.Create(typeof(IBSngService.Isend_recv));

                XmlRpcStruct arguments = new XmlRpcStruct();
                XmlRpcStruct conds = new XmlRpcStruct();

                arguments.Add("auth_name", "pendar");
                arguments.Add("auth_pass", "Pendar#!$^");
                arguments.Add("auth_type", "ADMIN");

                conds.Add("change_time_from", startDate);
                conds.Add("change_time_from_unit", "jalali");
                conds.Add("change_time_to", endDate);
                conds.Add("change_time_to_unit", "jalali");

                conds.Add("user_ids", IBS_User_ID);
                arguments.Add("conds", conds);

                arguments.Add("from", 0);
                arguments.Add("to", 1000);
                arguments.Add("sort_by", "change_time");
                arguments.Add("desc", true);

                XmlRpcStruct result = ibsngService.GetUserAuditLogs(arguments);

                //double sum_deposit_change = 0, deposit_change = 0, count = 1, action, Last_deposit_change = 0;

                //ADSLAuditChangesInfo auditInfo = new ADSLAuditChangesInfo();
                //List<ADSLAuditChangesInfo> auditInfoList = new List<ADSLAuditChangesInfo>();

                object[] report = (object[])result["report"];
                string resuletDate;

                if (report.Count() != 0)
                {
                    List<string> groupTime = new List<string>();

                    foreach (XmlRpcStruct item in (XmlRpcStruct[])result["report"])
                    {
                        if (item["attr_name"].ToString().Contains("group"))
                        {
                            groupTime.Add(item["change_time_formatted"].ToString());
                        }
                    }

                    if (groupTime.Count != 0)
                        resuletDate = groupTime.Max();
                    else
                        resuletDate = "";
                }
                else
                    resuletDate = "";

                return resuletDate;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InstalmentButton_Click(object sender, RoutedEventArgs e)
        {         
            List<RequestPayment> payments = PaymentDB.GetRequestPaymentforInstalment();
            
            Request request = new Request();
            int serviceID = 0;
            ADSLService service = null;

            foreach (RequestPayment currentPayment in payments)
            {
                if (!PaymentDB.HasInstalment(currentPayment.ID))
                {
                    request = ADSLRequestDB.GetRequestbyID(currentPayment.RequestID);

                    if (request.RequestPaymentTypeID == 35)
                        serviceID = PaymentDB.GetADSLServiceServiceID(currentPayment.RequestID, 35);

                    if (request.RequestPaymentTypeID == 38)
                        serviceID = PaymentDB.GetADSLServiceServiceID(currentPayment.RequestID, 38);

                    service = PaymentDB.GetADSLServicebyID(serviceID);
                    
                    int _floorValue = 1000;
                    List<InstallmentRequestPayment> installmentRequestPayments = new List<InstallmentRequestPayment>();
                    int PaymentAmountEachPart = 0;
                    string startDate = "1393/05/01";// DB.GetServerDate().ToPersian(Date.DateStringType.Short);
                    string endateCount = Date.AddMonthToPersianDate(startDate, (int)service.DurationID);
                    string endDate = Date.AddMonthToPersianDate(startDate, (int)service.DurationID);

                    string startDateEachPart = startDate;

                    PaymentAmountEachPart = (int)(service.PriceSum / (decimal)service.DurationID);

                    for (int i = 1; i <= service.DurationID; i++)
                    {
                        InstallmentRequestPayment installmentRequestPayment = new InstallmentRequestPayment();
                        int dateEachPart = 1;
                        installmentRequestPayment.RequestPaymentID = currentPayment.ID;
                        installmentRequestPayment.TelephoneNo = request.TelephoneNo;
                        installmentRequestPayment.IsCheque = false;
                        installmentRequestPayment.IsPaid = false;
                        installmentRequestPayment.IsDeleted = false;

                        installmentRequestPayment.StartDate = Date.PersianToGregorian(startDateEachPart).Value.Date;
                        string endDateEachPart = Date.GetLastDayOfMount(startDateEachPart);

                        installmentRequestPayment.EndDate = Date.PersianToGregorian(endDateEachPart).Value.Date;
                        
                        if (service.DurationID == i)
                            installmentRequestPayment.Cost = (long)(service.PriceSum - (decimal)installmentRequestPayments.Sum(t => t.Cost));
                        else
                            installmentRequestPayment.Cost = _floorValue * ((PaymentAmountEachPart * dateEachPart) / _floorValue);

                        startDateEachPart = Date.GetPersianDateAddDays(endDateEachPart, 1);

                        installmentRequestPayments.Add(installmentRequestPayment);

                        installmentRequestPayment.Detach();
                        DB.Save(installmentRequestPayment, true);
                    }
                }
            }
        }
    }
}
