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
using System.Data;

namespace CRMConvertApplication
{
    public partial class ElkaTest : Window
    {
        public ElkaTest()
        {
            InitializeComponent();

            TestElka();

            Application.Current.Shutdown();
        }

        public void TestElka()
        {
            try
            {
                List<ADSLIRAN> aaa = ADSLDB.GetADSLIranListKermanshah();
                int prioriyt = 0;
                string sTime = string.Empty;
                string eTime = string.Empty;
                string IDList = string.Empty;
                string iranCode = "";
                int ostanCode = 0;
                string pishShomare = "";

                string city = ADSLDB.GetSettingValueByKey("City");
                if (city == "Semnan")
                {
                    iranCode = "12345semnan";
                    ostanCode = 30;
                    pishShomare = "23";
                }
                if (city == "Kermanshah")
                {
                    iranCode = "12345kermanshah";
                    ostanCode = 33;
                    pishShomare = "83";
                }

                ElkaService.web_service_Transmission elka = new ElkaService.web_service_Transmission();
                //********
                int handshakeResult = elka.handshake(ref prioriyt, ref sTime, iranCode, ref eTime, ostanCode);

                if (handshakeResult == 0)
                {
                    ADSLIRANRequest iranRequest = ADSLDB.GetLastADSLIRANRequest();
                    if (iranRequest != null)
                    {
                        int difrenceMinute = Convert.ToInt32((DateTime.Now - iranRequest.LastSendDate).TotalMinutes);

                        if (difrenceMinute > prioriyt)
                        {
                            DataSet dataSetSync = new DataSet();

                            DataTable cityTable = new DataTable("City");
                            DataColumn codeCity = cityTable.Columns.Add("ci_code", typeof(Int32));
                            DataColumn nameCity = cityTable.Columns.Add("ci_name", typeof(string));
                            DataColumn provinceCity = cityTable.Columns.Add("os_code", typeof(Int32));
                            DataColumn pishCity = cityTable.Columns.Add("ci_pish_code", typeof(string));

                            List<City> cityList = ADSLDB.GetAllCity();
                            foreach (City currentCity in cityList)
                            {
                                cityTable.Rows.Add(currentCity.ID, currentCity.Name, ostanCode, pishShomare);
                            }

                            dataSetSync.Tables.Add(cityTable);

                            DataTable centertable = new DataTable("Center");
                            DataColumn codeCenter = centertable.Columns.Add("cen_code", typeof(Int32));
                            DataColumn nameCenter = centertable.Columns.Add("cen_name", typeof(string));
                            DataColumn provinceCenter = centertable.Columns.Add("ci_code", typeof(Int32));

                            List<Center> centerList = ADSLDB.GetAllCenter();
                            foreach (Center currentCenter in centerList)
                            {
                                centertable.Rows.Add(currentCenter.ID, currentCenter.CenterName, currentCenter.RegionID);
                            }

                            dataSetSync.Tables.Add(centertable);

                            DataTable companytable = new DataTable("Company");
                            DataColumn codeCompany = companytable.Columns.Add("company_id", typeof(Int32));
                            DataColumn nameCompnay = companytable.Columns.Add("name", typeof(string));
                            DataColumn kind = companytable.Columns.Add("kind", typeof(Int32));

                            if (city == "Semnan")
                            {
                                List<PAPInfo> papList = ADSLDB.GetAllPAP();
                                foreach (PAPInfo currentPAP in papList)
                                {
                                    companytable.Rows.Add(currentPAP.ID, currentPAP.Title, 0);
                                }
                                companytable.Rows.Add(100, "مخابرات", 1);
                            }
                            if (city == "Kermanshah")
                            {
                                List<PAPInfo> papList = ADSLDB.GetAllPAP().Where(t => t.ID != 1).ToList();
                                foreach (PAPInfo currentPAP in papList)
                                {
                                    companytable.Rows.Add(currentPAP.ID, currentPAP.Title, 0);
                                }
                                companytable.Rows.Add(1, "مخابرات", 1);
                            }

                            dataSetSync.Tables.Add(companytable);
                            //********
                            int syncResult = elka.Sync_Base_Data(dataSetSync, iranCode);

                            if (syncResult == 0)
                            {
                                DataSet dataSetTransfer = new DataSet();

                                DataTable transferTable = new DataTable("Transfer");

                                DataColumn C_P_ID = transferTable.Columns.Add("C_P_ID", typeof(Int64));
                                DataColumn Os_Code = transferTable.Columns.Add("Os_Code", typeof(Int32));
                                DataColumn Ci_Code = transferTable.Columns.Add("Ci_Code", typeof(Int32));
                                DataColumn Cen_Code = transferTable.Columns.Add("Cen_Code", typeof(Int32));
                                DataColumn Pap_Code = transferTable.Columns.Add("Pap_Code", typeof(Int32));
                                DataColumn Tel_Num = transferTable.Columns.Add("Tel_Num", typeof(Int64));
                                DataColumn S_Date = transferTable.Columns.Add("S_Date", typeof(string));
                                DataColumn S_Time = transferTable.Columns.Add("S_Time", typeof(string));
                                DataColumn E_Date = transferTable.Columns.Add("E_Date", typeof(string));
                                DataColumn E_Time = transferTable.Columns.Add("E_Time", typeof(string));
                                DataColumn Kind_Code = transferTable.Columns.Add("Kind_Code", typeof(Int32));
                                DataColumn Fi_Code = transferTable.Columns.Add("Fi_Code", typeof(Int64));
                                DataColumn Work_Status = transferTable.Columns.Add("Work_Status", typeof(Int32));
                                DataColumn Ci_Pish_Code = transferTable.Columns.Add("Ci_Pish_Code", typeof(Int32));
                                DataColumn Project_Id = transferTable.Columns.Add("Project_Id", typeof(Int32));
                                DataColumn Company_Base_Id = transferTable.Columns.Add("Company_Base_Id", typeof(Int32));
                                DataColumn Mdf_Comment = transferTable.Columns.Add("Mdf_Comment", typeof(string));
                                DataColumn Confirm_Date = transferTable.Columns.Add("Confirm_Date", typeof(string));
                                DataColumn Confirm_Time = transferTable.Columns.Add("Confirm_Time", typeof(string));

                                List<ADSLIRAN> adslList = null;
                                if (city == "Semnan")
                                    adslList = ADSLDB.GetADSLIranListSemnan();
                                if (city == "Kermanshah")
                                    adslList = ADSLDB.GetADSLIranListKermanshah();

                                if (adslList != null)
                                {
                                    foreach (ADSLIRAN item in adslList)
                                    {
                                        ADSLPAPRequest papDate = ADSLDB.GetADSLPAPRequestByID(Convert.ToInt64(item.C_P_ID.ToString().Substring(2)));
                                        if (papDate != null)
                                        {
                                            if (papDate.FinalDate != null)
                                            {
                                                item.E_Date = "1394/08/23";// papDate.FinalDate.Value.ToShortDateString();
                                                item.E_Time = papDate.FinalDate.Value.Hour + ":" + papDate.FinalDate.Value.Minute + ":00";
                                            }
                                        }
                                        else
                                        {
                                            if (city == "Semnan")
                                            {
                                                ADSLRequest adslDate = ADSLDB.GetADSLRequestByID(Convert.ToInt64(item.C_P_ID.ToString().Substring(2)));
                                                if (adslDate != null)
                                                {
                                                    if (adslDate.MDFDate != null)
                                                    {
                                                        item.E_Date = adslDate.MDFDate.Value.ToShortDateString();
                                                        item.E_Time = adslDate.MDFDate.Value.ToShortTimeString();
                                                    }
                                                }
                                            }
                                        }

                                        transferTable.Rows.Add(item.C_P_ID, item.Os_Code, item.Ci_Code,
                                                               item.Cen_Code, item.Pap_Code, item.Tel_Num,
                                                               item.S_Date, item.S_Time, item.E_Date,
                                                               item.E_Time, item.Kind_Code, item.Fi_Code,
                                                               item.Work_Status, item.Ci_Pish_Code, item.Project_Id,
                                                               item.Company_Base_Id, item.Mdf_Comment, item.Confirm_Date, item.Confirm_Time);
                                    }

                                    dataSetTransfer.Tables.Add(transferTable);
                                    //********
                                    int transferResult = elka.transfer_Data(dataSetTransfer, iranCode, ref IDList);

                                    if (transferResult == 0)
                                    {
                                        string[] idListString = IDList.Split(',');
                                        foreach (string id in idListString)
                                        {
                                            ADSLPAPRequest papRequest = ADSLDB.GetADSLPAPRequestByID(Convert.ToInt64(id.Substring(2)));

                                            if (papRequest != null)
                                            {
                                                if (papRequest.IsIranInsert == null)
                                                    papRequest.IsIranInsert = true;
                                                else
                                                    if (papRequest.IsIranInsert == true)
                                                        if (papRequest.IsIranEnd == null)
                                                            papRequest.IsIranEnd = true;

                                                papRequest.Detach();
                                                DB.Save(papRequest);
                                            }
                                            else
                                            {
                                                ADSLRequest adslRequest = ADSLDB.GetADSLRequestByID(Convert.ToInt64(id.Substring(2)));
                                                if (adslRequest != null)
                                                {
                                                    if (adslRequest.IsIranInsert == null)
                                                        adslRequest.IsIranInsert = true;
                                                    else
                                                        if (adslRequest.IsIranInsert == true)
                                                            if (adslRequest.IsIranEnd == null)
                                                                adslRequest.IsIranEnd = true;

                                                    adslRequest.Detach();
                                                    DB.Save(adslRequest);
                                                }
                                            }
                                        }

                                        ADSLIRANRequest newRequest = new ADSLIRANRequest();

                                        newRequest.LastSendDate = DateTime.Now;
                                        newRequest.Count = idListString.Count();

                                        newRequest.Detach();
                                        DB.Save(newRequest);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void SendKafuInfo(long requestID, int centerID, int userID, int cabinetID, int cabinetNo, List<long> TelephoneNos)
        {
            try
            {
                string sTime = string.Empty;
                string eTime = string.Empty;
                string IDList = string.Empty;
                string iranCode = "";
                int ostanCode = 0;
                string pishShomare = "";

                string city = ADSLDB.GetSettingValueByKey("City");
                if (city == "Semnan")
                {
                    iranCode = "12345semnan";
                    ostanCode = 30;
                    pishShomare = "23";
                }
                if (city == "Kermanshah")
                {
                    iranCode = "12345kermanshah";
                    ostanCode = 33;
                    pishShomare = "83";
                }

                ElkaService.web_service_Transmission elka = new ElkaService.web_service_Transmission();
                DataSet dataSetSync = new DataSet();

                DataTable cityTable = new DataTable("City");
                DataColumn codeCity = cityTable.Columns.Add("ci_code", typeof(Int32));
                DataColumn nameCity = cityTable.Columns.Add("ci_name", typeof(string));
                DataColumn provinceCity = cityTable.Columns.Add("os_code", typeof(Int32));
                DataColumn pishCity = cityTable.Columns.Add("ci_pish_code", typeof(string));

                List<City> cityList = ADSLDB.GetAllCity();
                foreach (City currentCity in cityList)
                {
                    cityTable.Rows.Add(currentCity.ID, currentCity.Name, ostanCode, pishShomare);
                }

                dataSetSync.Tables.Add(cityTable);

                DataTable centertable = new DataTable("Center");
                DataColumn codeCenter = centertable.Columns.Add("cen_code", typeof(Int32));
                DataColumn nameCenter = centertable.Columns.Add("cen_name", typeof(string));
                DataColumn provinceCenter = centertable.Columns.Add("ci_code", typeof(Int32));

                List<Center> centerList = ADSLDB.GetAllCenter();
                foreach (Center currentCenter in centerList)
                {
                    centertable.Rows.Add(currentCenter.ID, currentCenter.CenterName, currentCenter.RegionID);
                }

                dataSetSync.Tables.Add(centertable);

                DataTable companytable = new DataTable("Company");
                DataColumn codeCompany = companytable.Columns.Add("company_id", typeof(Int32));
                DataColumn nameCompnay = companytable.Columns.Add("name", typeof(string));
                DataColumn kind = companytable.Columns.Add("kind", typeof(Int32));

                if (city == "Semnan")
                {
                    List<PAPInfo> papList = ADSLDB.GetAllPAP();
                    foreach (PAPInfo currentPAP in papList)
                    {
                        companytable.Rows.Add(currentPAP.ID, currentPAP.Title, 0);
                    }
                    companytable.Rows.Add(100, "مخابرات", 1);
                }
                if (city == "Kermanshah")
                {
                    List<PAPInfo> papList = ADSLDB.GetAllPAP().Where(t => t.ID != 1).ToList();
                    foreach (PAPInfo currentPAP in papList)
                    {
                        companytable.Rows.Add(currentPAP.ID, currentPAP.Title, 0);
                    }
                    companytable.Rows.Add(1, "مخابرات", 1);
                }

                dataSetSync.Tables.Add(companytable);

                int syncResult = elka.Sync_Base_Data(dataSetSync, iranCode);

                if (syncResult == 0)
                {
                    DataSet kafuDataSetSync = new DataSet();

                    DataTable kafuBaseInfoTable = new DataTable("KAFU_SWAP_BASE_INFO");
                    DataColumn KS_ADSLCO_ID = kafuBaseInfoTable.Columns.Add("KS_ADSLCO_ID", typeof(Int32));
                    DataColumn CI_CODE = kafuBaseInfoTable.Columns.Add("CI_CODE", typeof(Int32));
                    DataColumn CEN_CODE = kafuBaseInfoTable.Columns.Add("CEN_CODE", typeof(Int32));
                    DataColumn USER_ID = kafuBaseInfoTable.Columns.Add("USER_ID", typeof(Int32));
                    DataColumn USER_NAME = kafuBaseInfoTable.Columns.Add("USER_NAME", typeof(string));
                    DataColumn INSERT_DATE = kafuBaseInfoTable.Columns.Add("INSERT_DATE", typeof(string));
                    DataColumn SWAP_LETTER = kafuBaseInfoTable.Columns.Add("SWAP_LETTER", typeof(string));
                    DataColumn KAFU_ID = kafuBaseInfoTable.Columns.Add("KAFU_ID", typeof(Int32));
                    DataColumn KAFU_NUM = kafuBaseInfoTable.Columns.Add("KAFU_NUM", typeof(string));

                    // مقادیر داخل دیتاتیبل اینجا باید افزوده شود
                    int requestID1 = Convert.ToInt32(requestID);
                    int cityID = CenterDB.GetCityIDByCenterID(centerID);
                    string fullName = ADSLDB.GetUserFullName(userID);
                    string reqID = requestID.ToString();
                    string insertDate = "13" + reqID.Substring(0, 2) + "/" + reqID.Substring(2, 2) + "/" + reqID.Substring(4, 2);

                    kafuBaseInfoTable.Rows.Add(requestID1, cityID, centerID, userID, fullName, insertDate, "", cabinetID, cabinetNo);
                    kafuDataSetSync.Tables.Add(kafuBaseInfoTable);

                    DataTable kafuBaseDetailTable = new DataTable("KAFU_SWAP_DETAILS");
                    DataColumn KS_ADSLCO_ID1 = kafuBaseDetailTable.Columns.Add("KS_ADSLCO_ID", typeof(Int32));
                    DataColumn COMPANY_ID = kafuBaseDetailTable.Columns.Add("COMPANY_ID", typeof(Int32));
                    DataColumn BEFORE_VALUE = kafuBaseDetailTable.Columns.Add("BEFORE_VALUE", typeof(Int32));

                    // مقادیر داخل دیتاتیبل اینجا باید افزوده شود

                    List<PAPInfo> papList1 = ADSLDB.GetAllPAP();
                    int count = 0;
                    foreach (PAPInfo item in papList1)
                    {
                        count = 0;
                        foreach (long telephoneNo in TelephoneNos)
                        {
                            if (ADSLDB.HasADSLPAPPortbyTelephoneNo(telephoneNo, item.ID))
                                count = count + 1;
                        }

                        kafuBaseDetailTable.Rows.Add(requestID1, item.ID, count);
                    }

                    kafuDataSetSync.Tables.Add(kafuBaseDetailTable);

                    elka.Sync_KafuSwap_Info(ostanCode, iranCode, kafuDataSetSync, ref IDList);

                    string[] idListString = IDList.Split(',');
                    ADSLIRANRequest newRequest = new ADSLIRANRequest();

                    newRequest.LastSendDate = DateTime.Now;
                    newRequest.Count = idListString.Count();

                    newRequest.Detach();
                    DB.Save(newRequest);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}

