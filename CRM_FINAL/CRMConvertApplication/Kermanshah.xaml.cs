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
using CRMConvertApplication.CRMWebService;

namespace CRMConvertApplication
{
    /// <summary>
    /// Interaction logic for Kermanshah.xaml
    /// </summary>
    public partial class Kermanshah : Window
    {
        public Kermanshah()
        {
            InitializeComponent();
            Initilize();
        }

        private void Initilize()
        {
            try
            {
                CRMWebService.CRMWebService service = new CRMWebService.CRMWebService();

                //bool result = false;
                //bool isComfirmed = false;
                //bool isDescharge = false;
                //bool isTechnicalFailed = false;
                //bool isUnknown = false;

                //service.SaveFailure117Kermanshah(12345678, 111, null, out result, out isComfirmed, out isDescharge, out isTechnicalFailed, out isUnknown);

                DateTime fromDate = new DateTime(2015, 1, 21);
                DateTime toDate = new DateTime(2015, 2, 19);
                PAPBillingInfo[] list = new PAPBillingInfo[20];
                list = service.GetPAPInstallRequestCount(fromDate, toDate);

                ServiceReference1.CRMServiceClient service1 = new ServiceReference1.CRMServiceClient();

                bool error=false;
                string errorMessage="";
                CRM.Data.ServiceHost.ServiceHostCustomClass.CauseOfCutInfo[] aaa=  service1.CauseOfCut(out error, out errorMessage, "PendarPajouh", "Kermanshah@srv");

                ServiceReference1.ServiceHostCustomClassPAPBillingInfo[]  list1 = service1.GetPAPInstallRequestCount("", "", fromDate, toDate);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
