using Enterprise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Windows;

namespace CRM.Application.Codes
{
    public class ServiceReference
    {
        internal static List<CRM.Data.BillingServiceReference.DebtInfo> GetDebtInfo(List<string> telephones)
        {
            List<CRM.Data.BillingServiceReference.DebtInfo> debtInfos = new List<CRM.Data.BillingServiceReference.DebtInfo>();
            try
            {
                debtInfos = Data.BillingServiceDB.GetDebtInfo(telephones);
            }
            catch (WebException ex)
            {
                // Folder.MessageBox.ShowInfo("خطا در برقراری ارتباط با آبونمان");
                MessageBox.Show("خطا در برقراری ارتباط با آبونمان");
                Logger.WriteError(ex.Message);
            }
            catch (Exception ex)
            {
                //Folder.MessageBox.ShowInfo("خطا در برقراری ارتباط با آبونمان");
                MessageBox.Show("خطا در برقراری ارتباط با آبونمان");
                Logger.WriteError(ex.Message);
            }


            return debtInfos;

        }
    }
}
