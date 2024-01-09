using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CRM.Data
{
    public static class ElkaService
    {
        public static List<ElkaInfo> GetTranslationInfo(DateTime fromDate, DateTime toDate)
        {
            List<ElkaInfo> list = new List<ElkaInfo>();

            //////////خواندن اطلاعات برگردان های معمولی به نوری و پر کردن لیست
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.TranslationOpticalCabinetToNormals
                                   .Where(translationOpticalCabinetToNormal => (translationOpticalCabinetToNormal.CompletionDate.HasValue) && //حتماّ برگردان انجام شده باشد
                                                                               (fromDate <= translationOpticalCabinetToNormal.CompletionDate.Value) &&
                                                                               (toDate >= translationOpticalCabinetToNormal.CompletionDate.Value) &&
                                                                               (translationOpticalCabinetToNormal.OldCabinetUsageTypeID == (int)DB.CabinetUsageType.Normal) && //حتماّ کافو قدیم معمولی بوده باشد
                                                                               (translationOpticalCabinetToNormal.NewCabinetUsageTypeID == (int)DB.CabinetUsageType.OpticalCabinet)) //حتماّ کافوی جدید نوری بوده باشد                                                                              
                                   .Select(translationOpticalCabinetToNormal => new ElkaInfo
                                   {
                                       RequestID = translationOpticalCabinetToNormal.Request.ID,
                                       UserID = DB.CurrentUser.ID,
                                       CenterID = translationOpticalCabinetToNormal.Request.CenterID,
                                       CabinetID = translationOpticalCabinetToNormal.OldCabinetID,
                                       CabinetNo = translationOpticalCabinetToNormal.Cabinet.CabinetNumber,

                                       //کلیه تلفن های قدیم برگردان جاری برگردانده شود
                                       TelephoneNos = context.TranslationOpticalCabinetToNormalConncetions
                                                             .Where(toctnc => (toctnc.RequestID == translationOpticalCabinetToNormal.Request.ID) &&
                                                                              (toctnc.FromTelephoneNo.HasValue))
                                                             .Select(filtered => filtered.FromTelephoneNo.Value).ToList()
                                   }).AsQueryable();
                list = query.ToList();
            }
            return list;
        }

        public static void SendKafuInfo(long requestID, int centerID, int userID, int cabinetID, string cabinetNo, List<long> TelephoneNos)
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

                ElkaWebSevice.web_service_Transmission elka = new ElkaWebSevice.web_service_Transmission();
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
                    List<PAPInfo> papList = PAPInfoDB.GetAllPAP();
                    foreach (PAPInfo currentPAP in papList)
                    {
                        companytable.Rows.Add(currentPAP.ID, currentPAP.Title, 0);
                    }
                    companytable.Rows.Add(100, "مخابرات", 1);
                }
                if (city == "Kermanshah")
                {
                    List<PAPInfo> papList = PAPInfoDB.GetAllPAP().Where(t => t.ID != 1).ToList();
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
                    DataColumn KS_ADSLCO_ID = kafuBaseInfoTable.Columns.Add("KS_ADSLCO_ID", typeof(Int64));
                    DataColumn CI_CODE = kafuBaseInfoTable.Columns.Add("CI_CODE", typeof(Int32));
                    DataColumn CEN_CODE = kafuBaseInfoTable.Columns.Add("CEN_CODE", typeof(Int32));
                    DataColumn USER_ID = kafuBaseInfoTable.Columns.Add("USER_ID", typeof(Int32));
                    DataColumn USER_NAME = kafuBaseInfoTable.Columns.Add("USER_NAME", typeof(string));
                    DataColumn INSERT_DATE = kafuBaseInfoTable.Columns.Add("INSERT_DATE", typeof(string));
                    DataColumn SWAP_LETTER = kafuBaseInfoTable.Columns.Add("SWAP_LETTER", typeof(string));
                    DataColumn KAFU_ID = kafuBaseInfoTable.Columns.Add("KAFU_ID", typeof(Int32));
                    DataColumn KAFU_NUM = kafuBaseInfoTable.Columns.Add("KAFU_NUM", typeof(string));
                                        
                    int cityID = CenterDB.GetCityIDByCenterID(centerID);
                    string fullName = ADSLDB.GetUserFullName(userID);
                    string reqID = requestID.ToString();
                    string insertDate = "13" + reqID.Substring(0, 2) + "/" + reqID.Substring(2, 2) + "/" + reqID.Substring(4, 2);

                    kafuBaseInfoTable.Rows.Add(requestID, cityID, centerID, userID, fullName, insertDate, "123", cabinetID, cabinetNo);
                    kafuDataSetSync.Tables.Add(kafuBaseInfoTable);

                    DataTable kafuBaseDetailTable = new DataTable("KAFU_SWAP_DETAILS");
                    DataColumn KS_ADSLCO_ID1 = kafuBaseDetailTable.Columns.Add("KS_ADSLCO_ID", typeof(Int64));
                    DataColumn COMPANY_ID = kafuBaseDetailTable.Columns.Add("COMPANY_ID", typeof(Int32));
                    DataColumn BEFORE_VALUE = kafuBaseDetailTable.Columns.Add("BEFORE_VALUE", typeof(Int32));
                                       
                    List<PAPInfo> papList1 = PAPInfoDB.GetAllPAP();
                    int count = 0;
                    foreach (PAPInfo item in papList1)
                    {
                        count = 0;
                        foreach (long telephoneNo in TelephoneNos)
                        {
                            if (ADSLPAPPortDB.HasADSLPAPPortbyTelephoneNo(telephoneNo, item.ID))
                                count = count + 1;
                        }

                        int companyID= ADSLPAPPortDB.GetADSLIRANPAPIDKermanshah(item.ID);
                        kafuBaseDetailTable.Rows.Add(requestID, companyID, count);
                    }

                    kafuDataSetSync.Tables.Add(kafuBaseDetailTable);

                   int result= elka.Sync_KafuSwap_Info(ostanCode, iranCode, kafuDataSetSync, ref IDList);

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
