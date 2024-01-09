using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class TransferDepartmentDB
    {

        public static List<TransferDepartmentOffice> GetTransferDepartmentOfficeByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.TransferDepartmentOffices.Where(t => t.RequestID == requestID).ToList();
            }
        }
    }
}
