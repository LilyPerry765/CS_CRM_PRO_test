using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Website.Codes
{
    public class MenuClasses
    {
        #region 118
        public class RecordTitleIn118TelephoneNoInput
        {
            public RecordTitleIn118TelephoneNoInput()
            {
                //TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.TitleIn118, (byte)DB.TitleIn118RequestMode.Add);
                //window.ShowDialog();
            }
        }

        public class DeleteTitleIn118TelephoneNoInput
        {
            public DeleteTitleIn118TelephoneNoInput()
            {
                //TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.TitleIn118, (byte)DB.TitleIn118RequestMode.Delete);
                //window.ShowDialog();
            }
        }

        public class ChangeTitleIn118TelephoneNoInput
        {
            public ChangeTitleIn118TelephoneNoInput()
            {
                //TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.TitleIn118, (byte)DB.TitleIn118RequestMode.Update);
                //window.ShowDialog();
            }
        }
        #endregion 118

        #region ChangeAddress

        public class ChangeLocationInsideCenterTelephoneNoInput
        {
            public ChangeLocationInsideCenterTelephoneNoInput()
            {
                //TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ChangeLocationCenterInside);
                //window.ShowDialog();
            }
        }

        public class ChangeLocationCenterToCenterTelephoneNoInput
        {
            public ChangeLocationCenterToCenterTelephoneNoInput()
            {
                //TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ChangeLocationCenterToCenter);
                //window.ShowDialog();
            }
        }

        public class ChangeAddress
        {
            public ChangeAddress()
            {
            //    TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ChangeAddress);
            //    window.ShowDialog();
            }
        }
        #endregion ChangeAddress

        #region ADSL

        public class ADSLTelephoneNoInput
        {
            public ADSLTelephoneNoInput()
            {
                //TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSL);
                //window.ShowDialog();
            }
        }

        public class ADSLChangeServiceTelephoneNoInput
        {
            public ADSLChangeServiceTelephoneNoInput()
            {
                //TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLChangeService);
                //window.ShowDialog();
            }
        }

        public class ADSLChangeIPTelephoneNoInput
        {
            public ADSLChangeIPTelephoneNoInput()
            {
                //TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLChangeIP);
                //window.ShowDialog();
            }
        }

        public class ADSLTemporaryCutTelephoneNoInput
        {
            public ADSLTemporaryCutTelephoneNoInput()
            {
                //TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLCutTemporary);
                //window.ShowDialog();
            }
        }

        public class BuchtSwitchingADSLTelephoneNoInput
        {
            public BuchtSwitchingADSLTelephoneNoInput()
            {
                //TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ChangePortADSL);
                //window.ShowDialog();
            }
        }

        public class PAPInstalTelephoneNoInput
        {
            public PAPInstalTelephoneNoInput()
            {
                //TelephoneNoInputForm window = new TelephoneNoInputForm((byte)DB.RequestType.ADSLInstalPAPCompany);
                //window.ShowDialog();
            }
        }    
        #endregion ADSL

        #region E1

        public class E1
        {
            public E1()
            {
                //RequestForm window = new RequestForm((byte)DB.RequestType.E1, 0);
                //window.ShowDialog();
            }
        }
        public class E1Fiber
        {
            public E1Fiber()
            {
                //RequestForm window = new RequestForm((byte)DB.RequestType.E1Fiber, 0);
                //window.ShowDialog();
            }
        }

        public class PrivateWire
        {
            public PrivateWire()
            {
                //RequestForm window = new RequestForm((byte)DB.RequestType.SpecialWire, 0);
                //window.ShowDialog();
            }
        }

        #endregion E1

        #region SpaceandPower
        public class SpaceandPower
        {
            public SpaceandPower()
            {
                //RequestForm window = new RequestForm((byte)DB.RequestType.SpaceandPower, 0);
                //window.ShowDialog();
            }
        }
        #endregion SpaceandPower
    }
}