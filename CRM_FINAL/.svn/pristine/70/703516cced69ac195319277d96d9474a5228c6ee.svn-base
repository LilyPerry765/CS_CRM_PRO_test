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
	public partial class TelephoneInfoForm : Local.PopupWindow
	{
		#region Properties

		private long _TelephoneNo = 0;
        private string city = string.Empty;

		#endregion

		#region Constructors

		public TelephoneInfoForm(long telephoneNo)
		{
			_TelephoneNo = telephoneNo;

			InitializeComponent();

			Initialize();
			LoadData();
		}

		#endregion

		#region Methods

		private void Initialize()
		{
            city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
        }

        private void LoadData()
        {
            if (city == "semnan")
            {
                Service1 service = new Service1();

                if (_TelephoneNo == 0)
                {
                    return;
                }

                System.Data.DataTable telephoneInfo = service.GetInformationForPhone("Admin", "alibaba123", _TelephoneNo.ToString());

                TelephoneNoTextBox.Text = telephoneInfo.Rows[0]["Phone"].ToString();
                CenterNameTextBox.Text = telephoneInfo.Rows[0]["CEN_NAME"].ToString();
                CustomerNameTextBox.Text = telephoneInfo.Rows[0]["FIRSTNAME"].ToString() + " " + telephoneInfo.Rows[0]["LASTNAME"].ToString();
                PostalCodeTextBox.Text = telephoneInfo.Rows[0]["CODE_POSTI"].ToString();
                AddressTextBox.Text = telephoneInfo.Rows[0]["ADDRESS"].ToString();
                CabinetNoTextBox.Text = telephoneInfo.Rows[0]["KAFU_NUM"].ToString();
                CabinetinputNoTextBox.Text = telephoneInfo.Rows[0]["KAFU_MARKAZI"].ToString();
                PostNoTextBox.Text = telephoneInfo.Rows[0]["POST_NUM"].ToString();
                PostEtesaliNoTextBox.Text = telephoneInfo.Rows[0]["POST_ETESALI"].ToString();
                RadifBuchtTextBox.Text = telephoneInfo.Rows[0]["RADIF"].ToString();
                TabagheBuchtTextBox.Text = telephoneInfo.Rows[0]["TABAGHE"].ToString();
                EtesaliBuchtTextBox.Text = telephoneInfo.Rows[0]["ETESALII"].ToString();
                if (Convert.ToInt16(telephoneInfo.Rows[0]["PCM_STATUS"]) == 1)
                {
                    PCMCheckBox.IsChecked = true;

                    System.Data.DataTable pCMInfo = service.GetPCMInformation("Admin", "alibaba123", _TelephoneNo.ToString());

                    PCMTechnicalInfo.Visibility = Visibility.Visible;
                    PortPCMTextBox.Text = pCMInfo.Rows[0]["PORT"].ToString();
                    ModelPCMTextBox.Text = pCMInfo.Rows[0]["PCM_MARK_NAME"].ToString();
                    TypePCMTextBox.Text = pCMInfo.Rows[0]["PCM_TYPE_NAME"].ToString();
                    RockPCMTextBox.Text = pCMInfo.Rows[0]["ROCK"].ToString();
                    ShelfPCMTextBox.Text = pCMInfo.Rows[0]["SHELF"].ToString();
                    CardPCMTextBox.Text = pCMInfo.Rows[0]["CARD"].ToString();
                    RadifInputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMI_RADIF"].ToString();
                    TabagheInputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMI_TABAGHE"].ToString();
                    EtesaliInputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMI_ETESALI"].ToString();
                    RadifOutputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMO_RADIF"].ToString();
                    TabagheOutputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMO_TABAGHE"].ToString();
                    EtesaliOutputBuchtTextBox.Text = pCMInfo.Rows[0]["PCMO_ETESALI"].ToString();
                }
                else
                {
                    PCMCheckBox.IsChecked = false;
                    PCMTechnicalInfo.Visibility = Visibility.Collapsed;
                }
            }

            if (city == "kermanshah")
            {
                if (_TelephoneNo == 0)
                {
                    return;
                }

                TelephoneSummenryInfo telephoneSummeryInfo = TelephoneDB.GetTelephoneSummneryInfoByTelephoneNo(_TelephoneNo);
                TechnicalInfoFailure117 technicalInfo = Failure117DB.GetCabinetInfobyTelephoneNo(_TelephoneNo);

                TelephoneNoTextBox.Text = _TelephoneNo.ToString();
                CenterNameTextBox.Text = telephoneSummeryInfo.Center;
                CustomerNameTextBox.Text = telephoneSummeryInfo.CustomerName;
                PostalCodeTextBox.Text = telephoneSummeryInfo.PostalCode;
                AddressTextBox.Text = telephoneSummeryInfo.Address;
                CabinetNoTextBox.Text = technicalInfo.CabinetNo;
                CabinetinputNoTextBox.Text = technicalInfo.CabinetInputNumber;
                PostNoTextBox.Text = technicalInfo.PostNo;
                PostEtesaliNoTextBox.Text = technicalInfo.ConnectionNo;
                RadifBuchtTextBox.Text = technicalInfo.RADIF;
                TabagheBuchtTextBox.Text = technicalInfo.TABAGHE;
                EtesaliBuchtTextBox.Text = technicalInfo.ETESALII;
                
                if (technicalInfo.IsPCM)
                {
                    PCMCheckBox.IsChecked = true;                    

                    PCMTechnicalInfo.Visibility = Visibility.Visible;
                    PortPCMTextBox.Text = technicalInfo.PCMPort;
                    ModelPCMTextBox.Text = technicalInfo.PCMModel;
                    TypePCMTextBox.Text = technicalInfo.PCMType;
                    RockPCMTextBox.Text = technicalInfo.PCMRock;
                    ShelfPCMTextBox.Text = technicalInfo.PCMShelf;
                    CardPCMTextBox.Text = technicalInfo.PCMCard;
                    RadifInputBuchtTextBox.Text = "";
                    TabagheInputBuchtTextBox.Text = "";
                    EtesaliInputBuchtTextBox.Text = "";
                    RadifOutputBuchtTextBox.Text = "";
                    TabagheOutputBuchtTextBox.Text = "";
                    EtesaliOutputBuchtTextBox.Text = "";
                }
                else
                {
                    PCMCheckBox.IsChecked = false;
                    PCMTechnicalInfo.Visibility = Visibility.Collapsed;
                }
 
            }
        }

		#endregion
	}
}
