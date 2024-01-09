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

namespace CRM.Application.Views
{
    public partial class Failure117FullView : Local.PopupWindow
    {
        #region Properties

        private long _RequestID = 0;

        #endregion

        #region Constuctors

        public Failure117FullView(long requestId)
        {
            InitializeComponent();

            _RequestID = requestId;

            LoadData();
        }

        #endregion

        #region Methods

        public void LoadData()
        {
            Request request = DB.SearchByPropertyName<Request>("ID", _RequestID).SingleOrDefault();
            Data.Failure117 failure117 = DB.SearchByPropertyName<Data.Failure117>("ID", _RequestID).SingleOrDefault();
            FailureFullViewInfo failure = new FailureFullViewInfo();
            failure = Data.Failure117DB.GetFailureRequestInfo(_RequestID);

            long? telephoneNo = DB.SearchByPropertyName<Request>("ID", _RequestID).SingleOrDefault().TelephoneNo;

            if (telephoneNo != null)
            {
                Service1 service = new Service1();
                System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", telephoneNo.ToString());

                failure.CustomerNationalCode = telephoneInfo.Rows[0]["MelliCode"].ToString();
                failure.CustomerName = telephoneInfo.Rows[0]["FirstName"].ToString() + " " + telephoneInfo.Rows[0]["Lastname"].ToString();
                failure.PostalCode = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                failure.Address = telephoneInfo.Rows[0]["ADDRESS"].ToString();

                failure.CabinetNo = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                failure.CabinetinputNo = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                failure.PostNo = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                failure.PostEtesaliNo = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();
                if (Convert.ToInt16(telephoneInfo.Rows[0]["PCM_STATUS"]) == 1)
                {
                    System.Data.DataTable pCMInfo = service.GetPCMInformation("Admin", "alibaba123", telephoneNo.ToString());

                    PCMTechnicalInfo.Visibility = Visibility.Visible;
                    failure.PortPCM = pCMInfo.Rows[0]["PORT"].ToString();
                    failure.ModelPCM = pCMInfo.Rows[0]["PCM_MARK_NAME"].ToString();
                    failure.TypePCM = pCMInfo.Rows[0]["PCM_TYPE_NAME"].ToString();
                    failure.RockPCM = pCMInfo.Rows[0]["ROCK"].ToString();
                    failure.ShelfPCM = pCMInfo.Rows[0]["SHELF"].ToString();
                    failure.CardPCM = pCMInfo.Rows[0]["CARD"].ToString();
                }
                else
                {
                    PCMTechnicalInfo.Visibility = Visibility.Collapsed;
                }

                System.Data.DataTable aDSLInfo = service.Phone_CUSTOMER_BOOKHTINFO(telephoneNo.ToString());
                if (aDSLInfo.Rows.Count != 0)
                {
                    System.Data.DataSet aDSLDataSet = new System.Data.DataSet();
                    aDSLDataSet.Tables.Add(aDSLInfo);
                    BuchtsDataGrid.DataContext = aDSLDataSet.Tables[0];
                }
                else
                    BuchtsGrid.Visibility = Visibility.Collapsed;
            }

            if (failure117 != null)
            {
                if (failure117.MDFDate != null && request.InsertDate != null)
                {
                    double compareResult = ((DateTime)failure117.MDFDate - (DateTime)request.InsertDate).TotalMinutes;
                    double hour = (compareResult < 60) ? 0 : Math.Round(compareResult / 60);
                    double min = Math.Round(compareResult % 60, 2);
                    failure.MDFAnalysisTime = string.Format("{0} : {1}", (min >= 10) ? min.ToString() : "0" + min.ToString(), (hour >= 10) ? hour.ToString() : "0" + hour.ToString());
                }

                if (failure117.NetworkDate != null && failure117.MDFDate != null)
                {
                    double compareResult1 = ((DateTime)failure117.NetworkDate - (DateTime)failure117.MDFDate).TotalMinutes;
                    double hour1 = (compareResult1 < 60) ? 0 : Math.Round(compareResult1 / 60);
                    double min1 = Math.Round(compareResult1 % 60, 2);
                    failure.NetworkTime = string.Format("{0} : {1}", (min1 >= 10) ? min1.ToString() : "0" + min1.ToString(), (hour1 >= 10) ? hour1.ToString() : "0" + hour1.ToString());
                }

                if (failure117.EndMDFDate != null && failure117.NetworkDate != null)
                {
                    double compareResult2 = ((DateTime)failure117.EndMDFDate - (DateTime)failure117.NetworkDate).TotalMinutes;
                    double hour2 = (compareResult2 < 60) ? 0 : Math.Round(compareResult2 / 60);
                    double min2 = Math.Round(compareResult2 % 60, 2);
                    failure.MDFConfirmTime = string.Format("{0} : {1}", (min2 >= 10) ? min2.ToString() : "0" + min2.ToString(), (hour2 >= 10) ? hour2.ToString() : "0" + hour2.ToString());
                }

                if (failure117.EndMDFDate != null && failure117.NetworkDate == null)
                    failure.MDFConfirmTime = failure.MDFAnalysisTime;

                if (failure117.EndMDFDate != null && failure117.SaloonDate != null)
                {
                    double compareResult2 = ((DateTime)failure117.EndMDFDate - (DateTime)failure117.SaloonDate).TotalMinutes;
                    double hour2 = (compareResult2 < 60) ? 0 : Math.Round(compareResult2 / 60);
                    double min2 = Math.Round(compareResult2 % 60, 2);
                    failure.MDFConfirmTime = string.Format("{0} : {1}", (min2 >= 10) ? min2.ToString() : "0" + min2.ToString(), (hour2 >= 10) ? hour2.ToString() : "0" + hour2.ToString());
                }
            }

            FailureFormInfo form = Failure117DB.GetFailureFormInfo(_RequestID);
            if (form != null)
            {
                failure.NetworkOfficer = form.NetworkOfficer;
                failure.CabelColor = form.CableColor1 + " - " + form.CableColor2;
                failure.CabelType = form.CableType;
                failure.GiveNetworkFormDate = Helper.GetPersianDate(form.GiveNetworkFormDate, Helper.DateStringType.DateTime);
                failure.GetNetworkFormDate = Helper.GetPersianDate(form.GetNetworkFormDate, Helper.DateStringType.DateTime);
                failure.SendToCabelDate = Helper.GetPersianDate(form.SendToCabelDate, Helper.DateStringType.DateTime);
                failure.CabelDate = Helper.GetPersianDate(form.CabelDate, Helper.DateStringType.DateTime);
            }

            RequestInfoGrid.DataContext = failure;



            ResizeWindow();

        }

        #endregion
    }
}
