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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Data;

namespace CRM.Application.UserControls
{
    public partial class Failure117 : UserControl
    {
        #region Properties

        private long _ReqID = 0;
        private long _CustomerID = 0;

        private CRM.Data.Failure117 _Failure117 { get; set; }
        private Request _Request { get; set; }
        private Customer _Customer { get; set; }
        private Telephone _Telephone { get; set; }
        public long TelephoneNo { get; set; }

        private byte _LineStatusTypeID = 0;
        private int _FailureStatusTypeID = 0;
        private Service1 service = new Service1();
        private System.Data.DataTable telephoneInfo;
        // private string city = string.Empty;
        private TechnicalInfoFailure117 technicalInfo;

        #endregion

        #region Costructors

        public Failure117()
        {
            ResourceDictionary popupResourceDictionary = new ResourceDictionary();
            popupResourceDictionary.Source = new Uri("pack://application:,,,/CRM.Application;component/Resources/Styles/PopupWindowStyles.xaml");
            base.Resources.MergedDictionaries.Add(popupResourceDictionary);

            InitializeComponent();
            Initialize();
        }

        public Failure117(long requestID,/* long customerID,*/ long telephoneNo)
            : this()
        {
            _ReqID = requestID;
            //_CustomerID = customerID;
            TelephoneNo = telephoneNo;

            LoadData(null, null);
        }

        #endregion

        #region Methods

        private void Initialize()
        {
            if (DB.City == "semnan")
                LineStatusTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.Failure117LineStatus)).Where(t => t.ID != 4).ToList();
            if (DB.City == "kermanshah")
                LineStatusTypeComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.Failure117LineStatus));

            ActionStatusComboBox.ItemsSource = Helper.GetEnumItem(typeof(DB.Failure117ActionStatus));
            FailureStatusTypeComboBox.ItemsSource = Failure117DB.GetParentFailureStatus((byte)DB.Failure117AvalibilityStatus.MDFAnalysis);
            MDFPersonnelComboBox.ItemsSource = Data.MDFPersonnelDB.GetMDFPersonnelsCheckable();
        }

        private void DisableControls()
        {

        }

        #endregion

        #region Event Handlers

        private void LoadData(object sender, RoutedEventArgs e)
        {
            if (DB.City == "semnan")
            {
                if (_ReqID == 0)
                {
                    //_Telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo( TelephoneNo).SingleOrDefault();
                    telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", TelephoneNo.ToString());
                }
                else
                {
                    _Request = Data.RequestDB.GetRequestByID(_ReqID);
                    _Failure117 = Failure117DB.GetFailureRequestByID(_ReqID);

                    if (_Failure117.LineStatusID != null)
                    {
                        _LineStatusTypeID = Failure117DB.GetLineStatusByID((int)_Failure117.LineStatusID).Type;
                        LineStatusTypeComboBox.SelectedValue = _LineStatusTypeID;
                        LineStatusComboBox.SelectedValue = _Failure117.LineStatusID;
                    }

                    if (_Failure117.FailureStatusID != null)
                    {
                        _FailureStatusTypeID = Convert.ToInt32(Failure117DB.GetFailureStatusByID((int)_Failure117.FailureStatusID).ParentID);
                        FailureStatusTypeComboBox.SelectedValue = _FailureStatusTypeID;
                        FailureStatusComboBox.SelectedValue = _Failure117.FailureStatusID;
                    }

                    telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _Request.TelephoneNo.ToString());
                }

                this.DataContext = _Failure117;

                CabinetNoTextBox.Text = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                CabinetinputNoTextBox.Text = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                PostNoTextBox.Text = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                PostEtesaliNoTextBox.Text = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();
                if (Convert.ToInt16(telephoneInfo.Rows[0]["PCM_STATUS"]) == 1)
                {
                    System.Data.DataTable pCMInfo = service.GetPCMInformation("Admin", "alibaba123", TelephoneNo.ToString());

                    if (pCMInfo.Rows.Count != 0)
                    {
                        PCMTechnicalInfo.Visibility = Visibility.Visible;
                        PortPCMTextBox.Text = pCMInfo.Rows[0]["PORT"].ToString();
                        ModelPCMTextBox.Text = pCMInfo.Rows[0]["PCM_MARK_NAME"].ToString();
                        TypePCMTextBox.Text = pCMInfo.Rows[0]["PCM_TYPE_NAME"].ToString();
                        RockPCMTextBox.Text = pCMInfo.Rows[0]["ROCK"].ToString();
                        ShelfPCMTextBox.Text = pCMInfo.Rows[0]["SHELF"].ToString();
                        CardPCMTextBox.Text = pCMInfo.Rows[0]["CARD"].ToString();
                    }
                    else
                        PCMTechnicalInfo.Visibility = Visibility.Collapsed;
                }
                else
                    PCMTechnicalInfo.Visibility = Visibility.Collapsed;

                System.Data.DataTable aDSLInfo = service.Phone_CUSTOMER_BOOKHTINFO(TelephoneNo.ToString());
                if (aDSLInfo.Rows.Count != 0)
                {
                    System.Data.DataSet aDSLDataSet = new System.Data.DataSet();
                    aDSLDataSet.Tables.Add(aDSLInfo);
                    BuchtsDataGrid.DataContext = aDSLDataSet.Tables[0];
                }
                else
                    BuchtsGrid.Visibility = Visibility.Collapsed;

                List<FailureHistoryInfo> historyList = Failure117DB.SearchFailureHistory(0, TelephoneNo);

                if (historyList.Count != 0)
                {
                    ItemsDataGrid.ItemsSource = historyList;
                    HistoryMessageLable.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ItemsDataGrid.Visibility = Visibility.Collapsed;
                    HistoryMessageLable.Visibility = Visibility.Visible;
                }
            }

            if (DB.City == "kermanshah")
            {
                if (_ReqID == 0)
                {
                    technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo(TelephoneNo);
                }
                else
                {
                    _Request = RequestDB.GetRequestByID(_ReqID);
                    _Failure117 = Failure117DB.GetFailureRequestByID(_ReqID);

                    if (_Failure117.LineStatusID != null)
                    {
                        _LineStatusTypeID = Failure117DB.GetLineStatusByID((int)_Failure117.LineStatusID).Type;
                        LineStatusTypeComboBox.SelectedValue = _LineStatusTypeID;
                        LineStatusComboBox.SelectedValue = _Failure117.LineStatusID;
                    }

                    if (_Failure117.FailureStatusID != null)
                    {
                        _FailureStatusTypeID = Convert.ToInt32(Failure117DB.GetFailureStatusByID((int)_Failure117.FailureStatusID).ParentID);
                        FailureStatusTypeComboBox.SelectedValue = _FailureStatusTypeID;
                        FailureStatusComboBox.SelectedValue = _Failure117.FailureStatusID;
                    }

                    technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo((long)_Request.TelephoneNo);
                }

                this.DataContext = _Failure117;

                if (technicalInfo != null)
                {
                    List<TechnicalInfoFailure117> technicalInfoList = new List<TechnicalInfoFailure117>();

                    CabinetNoTextBox.Text = technicalInfo.CabinetNo;
                    CabinetinputNoTextBox.Text = technicalInfo.CabinetInputNumber;
                    PostNoTextBox.Text = technicalInfo.PostNo;
                    PostEtesaliNoTextBox.Text = technicalInfo.ConnectionNo;

                    if (technicalInfo.IsPCM)
                    {
                        PCMTechnicalInfo.Visibility = Visibility.Visible;

                        PortPCMTextBox.Text = technicalInfo.PCMPort;
                        ModelPCMTextBox.Text = technicalInfo.PCMModel;
                        TypePCMTextBox.Text = technicalInfo.PCMType;
                        RockPCMTextBox.Text = technicalInfo.PCMRock;
                        ShelfPCMTextBox.Text = technicalInfo.PCMShelf;
                        CardPCMTextBox.Text = technicalInfo.PCMCard;

                        TechnicalInfoFailure117 pCMInput = Failure117DB.GetPCMInputbyTelephoneNo(TelephoneNo);
                        pCMInput.BOOKHT_TYPE_NAME = "ورودی PCM";
                        technicalInfoList.Add(pCMInput);
                        BuchtsDataGrid.ItemsSource = technicalInfoList;
                    }
                    else
                        PCMTechnicalInfo.Visibility = Visibility.Collapsed;

                    if (technicalInfo.HasAnotherBucht == false)
                    {
                        technicalInfo.BOOKHT_TYPE_NAME = "بوخت اصلی";
                        technicalInfoList.Add(technicalInfo);
                    }
                    else
                    {
                        List<Bucht> anotheBuchtList = Failure117DB.GetAnotherBuchtInfobyTelephoneNo(TelephoneNo);
                        TechnicalInfoFailure117 technicalInfo1;
                        foreach (Bucht bucht in anotheBuchtList)
                        {
                            technicalInfo1 = new TechnicalInfoFailure117();
                            technicalInfo1.BOOKHT_TYPE_NAME = Helper.GetEnumDescriptionByValue(typeof(DB.BuchtType), bucht.BuchtTypeID);
                            technicalInfo1.RADIF = bucht.ColumnNo.ToString();
                            technicalInfo1.TABAGHE = bucht.RowNo.ToString();
                            technicalInfo1.ETESALII = bucht.BuchtNo.ToString();

                            technicalInfoList.Add(technicalInfo1);
                        }
                    }                    

                    BuchtsDataGrid.ItemsSource = technicalInfoList;

                    TechnicalInfoFailure117 aDSLInfo = Failure117DB.ADSLPAPbyTelephoneNo(TelephoneNo);

                    if (aDSLInfo != null)
                    {
                        technicalInfoList.Add(aDSLInfo);
                        BuchtsDataGrid.ItemsSource = technicalInfoList;
                    }
                }
                else
                    PCMTechnicalInfo.Visibility = Visibility.Collapsed;

                List<FailureHistoryInfo> historyList = Failure117DB.SearchFailureHistory(0, TelephoneNo);

                List<FailureHistoryInfo> historyList1 = new List<FailureHistoryInfo>();
                if (historyList != null && historyList.Count != 0)
                {
                    foreach (FailureHistoryInfo item in historyList)
                    {
                        FailureForm form = Failure117DB.GetFailureForm(item.ID);
                        if (form != null)
                            item.GetNetworkFormDate = Helper.GetPersianDate(form.GetNetworkFormDate, Helper.DateStringType.DateTime);

                        historyList1.Add(item);
                    }
                }

                GetNetworkFormDateColumn.Visibility = Visibility.Visible;

                if (historyList.Count != 0)
                {
                    ItemsDataGrid.ItemsSource = historyList1;
                    HistoryMessageLable.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ItemsDataGrid.Visibility = Visibility.Collapsed;
                    HistoryMessageLable.Visibility = Visibility.Visible;
                }
            }
        }

        private void LineStatusTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_LineStatusTypeID != 0)
            {
                if (LineStatusTypeComboBox.SelectedValue != null)
                    LineStatusComboBox.ItemsSource = DB.SearchByPropertyName<Failure117LineStatus>("Type", LineStatusTypeComboBox.SelectedValue).ToList();
                else
                    LineStatusComboBox.ItemsSource = DB.SearchByPropertyName<Failure117LineStatus>("Type", _LineStatusTypeID).ToList();
            }
            else
            {
                LineStatusComboBox.ItemsSource = DB.SearchByPropertyName<Failure117LineStatus>("Type", LineStatusTypeComboBox.SelectedValue).ToList();
            }
        }

        private void FailureStatusTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_FailureStatusTypeID != 0)
            {
                if (FailureStatusTypeComboBox.SelectedValue != null)
                    FailureStatusComboBox.ItemsSource = DB.SearchByPropertyName<Failure117FailureStatus>("ParentID", FailureStatusTypeComboBox.SelectedValue).ToList();
                else
                    FailureStatusComboBox.ItemsSource = DB.SearchByPropertyName<Failure117FailureStatus>("ParentID", _FailureStatusTypeID).ToList();
            }
            else
            {
                FailureStatusComboBox.ItemsSource = DB.SearchByPropertyName<Failure117FailureStatus>("ParentID", FailureStatusTypeComboBox.SelectedValue).ToList();
            }
        }

        private void LineStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LineStatusComboBox.SelectedValue != null)
            {
                if (Convert.ToInt32(LineStatusComboBox.SelectedValue) == DB.SearchByPropertyName<Failure117LineStatus>("Title", "همشنوایی").SingleOrDefault().ID)
                {
                    HearingTelephoneNoLabel.Visibility = Visibility.Visible;
                    HearingTelephoneNoTextBox.Visibility = Visibility.Visible;
                }
                else
                {
                    HearingTelephoneNoLabel.Visibility = Visibility.Collapsed;
                    HearingTelephoneNoTextBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ActionStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ActionStatusComboBox.SelectedValue != null)
            {
                if (Convert.ToInt32(ActionStatusComboBox.SelectedValue) == (int)DB.Failure117ActionStatus.RemovalFailure)
                {
                    ResultLabel.Visibility = Visibility.Visible;
                    ResultListBox.Visibility = Visibility.Visible;
                }
                else
                {
                    ResultLabel.Visibility = Visibility.Collapsed;
                    ResultListBox.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c))
                e.Handled = false;
            else
                e.Handled = true;

            base.OnPreviewTextInput(e);
        }

        #endregion
    }
}
