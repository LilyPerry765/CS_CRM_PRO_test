using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace CRM.Data
{
    public static class BillingServiceDB
    {
        public static List<BillingServiceReference.DebtInfo> GetDebtInfo(List<string> telephones)
        {
                        var city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
                        List<BillingServiceReference.DebtInfo> debtInfo = new List<BillingServiceReference.DebtInfo>();
                        switch (city)
                        {
                            case "semnan":
                                {
                                    BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                                    EndpointAddress endpointAddress = new EndpointAddress("http://10.10.110.239:83/Host/BillService.svc");

                                    using (CRM.Data.BillingServiceReference.BillServiceClient client = new CRM.Data.BillingServiceReference.BillServiceClient(basicHttpBinding, endpointAddress))
                                    using (var scope = new OperationContextScope(client.InnerChannel))
                                    {
                                        MessageHeader usernameMessageHeader = new MessageHeader<string>("pendar").GetUntypedHeader("UserName", "");
                                        MessageHeader passwordMessageHeader = new MessageHeader<string>("pajouh@srv").GetUntypedHeader("Password", "");

                                        OperationContext.Current.OutgoingMessageHeaders.Add(usernameMessageHeader);
                                        OperationContext.Current.OutgoingMessageHeaders.Add(passwordMessageHeader);

                                        debtInfo =   client.GetDebtInfo(telephones).ToList();
                                    }
                                }
                                break;
                        }

                        return debtInfo;
        }

        public static List<BillingServiceReference.DebtInfo> GetDebtInfoByMinDebtAmount(List<string> telephones, long minDebt)
        {
            var city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
            List<BillingServiceReference.DebtInfo> debtInfo = new List<BillingServiceReference.DebtInfo>();
            switch (city)
            {
                case "semnan":
                    {
                        BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
                        EndpointAddress endpointAddress = new EndpointAddress("http://10.10.110.239:83/Host/BillService.svc");

                        using (CRM.Data.BillingServiceReference.BillServiceClient client = new CRM.Data.BillingServiceReference.BillServiceClient(basicHttpBinding, endpointAddress))
                        using (var scope = new OperationContextScope(client.InnerChannel))
                        {
                            MessageHeader usernameMessageHeader = new MessageHeader<string>("pendar").GetUntypedHeader("UserName", "");
                            MessageHeader passwordMessageHeader = new MessageHeader<string>("pajouh@srv").GetUntypedHeader("Password", "");

                            OperationContext.Current.OutgoingMessageHeaders.Add(usernameMessageHeader);
                            OperationContext.Current.OutgoingMessageHeaders.Add(passwordMessageHeader);

                            debtInfo = client.GetDebtInfoByMinDebtAmount(telephones, minDebt).ToList();
                        }
                    }
                    break;
            }

            return debtInfo;
        }
    }
}
