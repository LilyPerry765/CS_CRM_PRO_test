using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class SpecialWireDB
    {
        public static SpecialWire GetSpecialWireByRequestID(long requestID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialWires.Where(t => t.RequestID == requestID).SingleOrDefault();
            }
        }

        public static Center GetSourceCenterSpecialWireByTelephoneNo(long telephoneNo ,out Bucht sourceBucht )
        {
            using (MainDataContext context = new MainDataContext())
            {
             var bucht = new Bucht();
             var telephon =  context.Telephones.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();
             if (context.SpecialWireAddresses.Any(t2 => t2.Telephone == telephon))
             {
                  bucht = context.Buchts.Where(t => t.ID == context.SpecialWireAddresses.Where(t2 => t2.Telephone == telephon && t2.InstallAddressID == telephon.InstallAddressID).SingleOrDefault().BuchtID).SingleOrDefault();
             }
             else
             {
                 bucht = context.Buchts.Where(t => t.SwitchPortID == telephon.SwitchPortID).SingleOrDefault();
             }
             if (bucht != null)
             {
                 var center = context.Centers.Where(t => t.ID == bucht.CabinetInput.Cabinet.CenterID);
                 sourceBucht = bucht;
                 return center.SingleOrDefault();
             }
             else
             {
                 sourceBucht = null;
                 return null;
             }
             

            }
        }

        public static bool IsLastRequest(Request request)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<Request> tempRequests = new List<Request>();
                if (request.MainRequestID == null)
                {
                    tempRequests = context.Requests.Where(t => t.ID == request.ID || t.MainRequestID == request.ID)
                                                   .Where(t=>t.IsCancelation == false)
                                                   .ToList();
                }
                else
                {
                    Request reqeustTemp = context.Requests.Where(t => t.ID == request.MainRequestID).SingleOrDefault();
                    tempRequests = context.Requests.Where(t => t.ID == reqeustTemp.ID || t.MainRequestID == reqeustTemp.ID)
                                                   .Where(t => t.IsCancelation == false)
                                                   .ToList();
                }

                tempRequests = tempRequests.Where(t => t.ID != request.ID).ToList();

                var SpecialWire = context.SpecialWires.Where(t => tempRequests.Select(t2 => t2.ID).Contains(t.RequestID));

                return !SpecialWire.Any(t => t.SetupDate == null);
            }
        }

        public static void SpecialWireReturnOtherReqeust(long requestID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                context.Requests.Where(t => t.MainRequestID == requestID).ToList().ForEach(t => t.WaitForToBeCalculate = false);
                context.SubmitChanges();
            }
        }

        public static SpecialWire GetLastSpecialWireRequestByTelephone(long telephonNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SpecialWires.Where(r => r.Request.TelephoneNo == telephonNo).OrderByDescending(r => r.Request.EndDate).FirstOrDefault();
            }
        }
    }
}
