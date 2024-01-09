using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Data;
using CRM.Application.Views.InvestigatePossibilityFolder;
using System.Diagnostics;

namespace CRM.Application.Views
{
    public class InstalTelephoneNoInput
    {
        public InstalTelephoneNoInput()
        {
            RequestForm window = new RequestForm((byte)DB.RequestType.Dayri, 0);
            window.ShowDialog();
        }
    }

    public class ChangeLocationInsideCenterTelephoneNoInput
    {
        public ChangeLocationInsideCenterTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ChangeLocationCenterInside);
            window.ShowDialog();
        }
    }

    public class ChangeLocationCenterToCenterTelephoneNoInput
    {
        public ChangeLocationCenterToCenterTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ChangeLocationCenterToCenter);
            window.ShowDialog();
        }
    }

    public class ChangeNameTelephoneNoInput
    {
        public ChangeNameTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ChangeName);
            window.ShowDialog();
        }
    }

    public class DischarginTelephoneNoInput
    {
        public DischarginTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.Dischargin);
            window.ShowDialog();
        }
    }

    public class CutTelephoneNoInput
    {
        public CutTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.CutAndEstablish);
            window.ShowDialog();
        }
    }

    public class EstablishTelephoneNoInput
    {
        public EstablishTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.Connect);
            window.ShowDialog();
        }
    }

    public class ConnectTelephoneNoInput
    {
        public ConnectTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.Connect);
            window.ShowDialog();
        }
    }

    public class SpecialServiceTelephoneNoInput
    {
        public SpecialServiceTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.SpecialService);
            window.ShowDialog();
        }
    }

    public class OpenAndCloseZeroTelephoneNoInput
    {
        public OpenAndCloseZeroTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.OpenAndCloseZero);
            window.ShowDialog();
        }
    }

    public class ADSLTelephoneNoInput
    {
        public ADSLTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSL);
            window.ShowDialog();
        }
    }

    public class PAPInstalTelephoneNoInput
    {
        public PAPInstalTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLInstalPAPCompany);
            window.ShowDialog();
        }
    }

    public class ADSLChangeServiceTelephoneNoInput
    {
        public ADSLChangeServiceTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLChangeService);
            window.ShowDialog();
        }
    }

    public class ADSLChangeIPTelephoneNoInput
    {
        public ADSLChangeIPTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLChangeIP);
            window.ShowDialog();
        }
    }

    public class ADSLInstallTelephoneNoInput
    {
        public ADSLInstallTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLInstall);
            window.ShowDialog();
        }
    }

    public class ADSLTemporaryCutTelephoneNoInput
    {
        public ADSLTemporaryCutTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLCutTemporary);
            window.ShowDialog();
        }
    }

    public class ADSLDischargeTelephoneNoInput
    {
        public ADSLDischargeTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLDischarge);
            window.ShowDialog();
        }
    }

    public class ADSLSellTrafficInfoPhoneNoInput
    {
        public ADSLSellTrafficInfoPhoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLSellTraffic);
            window.ShowDialog();
        }
    }

    public class ADSLChangePlaceInfoPhoneNoInput
    {
        public ADSLChangePlaceInfoPhoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLChangePlace);
            window.ShowDialog();
        }
    }

    public class ADSLChangePortTelephoneNoInput
    {
        public ADSLChangePortTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLChangePort);
            window.ShowDialog();
        }
    }
    public class ChangeNoNoInput
    {
        public ChangeNoNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ChangeNo);
            window.ShowDialog();
        }
    }

    public class RefundableDepositOfFixedPhone
    {
        public RefundableDepositOfFixedPhone()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.RefundDeposit);
            window.ShowDialog();
        }
    }

    public class Reinstall
    {
        public Reinstall()
        {
            RequestForm window = new RequestForm((byte)DB.RequestType.Reinstall, 0);
            window.ShowDialog();
        }
    }
    public class ChangeAddress
    {
        public ChangeAddress()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ChangeAddress);
            window.ShowDialog();
        }
    }
    public class RecordTitleIn118TelephoneNoInput
    {
        public RecordTitleIn118TelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.TitleIn118);
            window.ShowDialog();
        }
    }

    public class DeleteTitleIn118TelephoneNoInput
    {
        public DeleteTitleIn118TelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.RemoveTitleIn118);
            window.ShowDialog();
        }
    }

    public class ChangeTitleIn118TelephoneNoInput
    {
        public ChangeTitleIn118TelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ChangeTitleIn118);
            window.ShowDialog();
        }
    }

    public class SpaceandPower
    {
        public SpaceandPower()
        {
            RequestForm window = new RequestForm((byte)DB.RequestType.SpaceandPower, 0);
            window.ShowDialog();
        }
    }

    //TODO:rad 13950523
    public class NewSpaceAndPower
    {
        public NewSpaceAndPower()
        {
            RequestForm window = new RequestForm((byte)DB.RequestType.StandardSpaceAndPower, 0);
            window.ShowDialog();
        }
    }

    public class Failure117
    {
        public Failure117()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.Failure117);
            window.ShowDialog();
        }
    }

    public class E1
    {
        public E1()
        {
            RequestForm window = new RequestForm((byte)DB.RequestType.E1, 0);
            window.ShowDialog();
        }
    }
    public class E1Fiber
    {
        public E1Fiber()
        {
            RequestForm window = new RequestForm((byte)DB.RequestType.E1Fiber, 0);
            window.ShowDialog();
        }
    }
    public class PrivateWire
    {
        public PrivateWire()
        {
            RequestForm window = new RequestForm((byte)DB.RequestType.SpecialWire, 0);
            window.ShowDialog();
        }
    }
    public class UserConfig
    {
        public UserConfig()
        {
            UserConfigForm window = new UserConfigForm();
            window.ShowDialog();
        }
    }

    public class ApplicationConfig
    {
        public ApplicationConfig()
        {
            ApplicationConfigForm window = new ApplicationConfigForm();
            window.ShowDialog();
        }
    }

    public class VacateSpecialWire
    {
        public VacateSpecialWire()
        {
            RequestForm window = new RequestForm((byte)DB.RequestType.VacateSpecialWire, 0);
            window.ShowDialog();
        }
    }

    public class ChangeLocationSpecialWire
    {
        public ChangeLocationSpecialWire()
        {
            RequestForm window = new RequestForm((byte)DB.RequestType.ChangeLocationSpecialWire, 0);
            window.ShowDialog();


        }
    }
    public class TranslationPost
    {
        public TranslationPost()
        {
            TranslationPostForm window = new TranslationPostForm((byte)DB.RequestType.ExchangePost);
            window.ShowDialog();


        }
    }
    public class ExchangeCabinet
    {
        public ExchangeCabinet()
        {
            TranslationCabinetForm window = new TranslationCabinetForm((int)DB.RequestType.ExchangeCabinetInput);
            window.ShowDialog();
        }
    }
    public class ExchangeCentralCableMDFMenu
    {
        public ExchangeCentralCableMDFMenu()
        {
            TranslationCentralCableMDFForm window = new TranslationCentralCableMDFForm((int)DB.RequestType.ExchangeCenralCableMDF);
            window.ShowDialog();
        }
    }
    public class TranslationPostInputFomMenu
    {
        public TranslationPostInputFomMenu()
        {
            TranslationPostInputFom window = new TranslationPostInputFom((int)DB.RequestType.TranlationPostInput);
            window.ShowDialog();
        }
    }
    public class BuchtSwitchingMenu
    {
        public BuchtSwitchingMenu()
        {
            BuchtSwitchingForm window = new BuchtSwitchingForm((int)DB.RequestType.BuchtSwiching);
            window.ShowDialog();
        }
    }

    public class PBX
    {
        public PBX()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.PBX);
            window.ShowDialog();
        }
    }
    public class E1LinkMenu
    {
        public E1LinkMenu()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.E1Link);
            window.ShowDialog();
        }
    }
    public class E1VacateMenu
    {
        public E1VacateMenu()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.VacateE1);
            window.ShowDialog();
        }
    }


    public class ADSLChangeCustomerOwnerCharacteristicsTelephoneNoInput
    {
        public ADSLChangeCustomerOwnerCharacteristicsTelephoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLChangeCustomerOwnerCharacteristics);
            window.ShowDialog();
        }
    }

    public class Wireless
    {
        public Wireless()
        {
            RequestForm window = new RequestForm((byte)DB.RequestType.Wireless, 0);
            window.ShowDialog();
        }
    }

    public class WirelesschangeServiceInfoPhoneNoInput
    {
        public WirelesschangeServiceInfoPhoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.WirelessChangeService);
            window.ShowDialog();
        }
    }

    public class WirelessSellTrafficInfoPhoneNoInput
    {
        public WirelessSellTrafficInfoPhoneNoInput()
        {
            TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.WirelessSellTraffic);
            window.ShowDialog();
        }
    }

    public class Help
    {
        public Help()
        {
            if (DB.City.ToLower() == "kermanshah")
            {
               System.Diagnostics.Process.Start("http://192.168.240.104:88/");
            }
            else if (DB.City.ToLower() == "gilan")
            {
                System.Diagnostics.Process.Start("http://192.168.209.150:81/help_1.6/");
            }
            else if (DB.City.ToLower() == "kerman")
            {
                System.Diagnostics.Process.Start("http://172.17.17.40:81/");
            }
            else if (DB.City.ToLower() == "semnan")
            {
                //Milad Doran:System.Diagnostics.Process.Start("http://10.10.110.240:83/");
                //TODO:rad 13950322
                Process.Start("http://172.24.2.24:88");
            }
            else if (DB.City.ToLower() == "golestan")
            {
                System.Diagnostics.Process.Start("http://192.168.0.14:86/");
            }
        }
    }


}
