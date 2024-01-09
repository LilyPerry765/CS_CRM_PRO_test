using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class ZeroStatusDB
    {
        public static ZeroStatus GetZeroStatusByID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ZeroStatus.Where(t => t.ID == requestID).SingleOrDefault();
            }

        }

        public static byte? GetZeroBlockByRequestID(Request request)
        {
            byte? zeroBlock = null;
            using (MainDataContext context = new MainDataContext())
            {
                if (request.RequestTypeID == (int)DB.RequestType.Dayri)
                {
                    zeroBlock = context.InstallRequests.Where(t => t.RequestID == request.ID).OrderByDescending(t => t.Installdate).FirstOrDefault().ClassTelephone;
                }
                else if (request.RequestTypeID == (int)DB.RequestType.OpenAndCloseZero)
                {
                    zeroBlock = context.ZeroStatus.SingleOrDefault(t => t.ID == request.ID).ClassTelephone;
                }

            }

            return zeroBlock;
        }

        public static bool HasAnySecondZeroBlock(long telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                bool result = false;
                result = context.Requests
                                .Where(re =>
                                            (re.TelephoneNo == telephoneNo) &&
                                            (re.RequestTypeID == (int)DB.RequestType.OpenAndCloseZero) &&
                                            (re.EndDate.HasValue)
                                      )
                                .Any(re => re.ZeroStatus.ClassTelephone == (byte)DB.ClassTelephone.SecondZeroBlock);
                return result;
            }
        }
    }
}
