using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRMConvertApplication
{
    public static class ADSLServiceDB
    {
        public static ADSLService GetADSLServiecNamebyIBSngName(string iBSngName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                ADSLService service = new ADSLService();
                List<ADSLService> serviceList = context.ADSLServices.Where(t => t.IBSngGroupName == iBSngName).ToList();

                if (serviceList.Count == 1)
                    service = serviceList[0];
                else
                    foreach (ADSLService currentService in serviceList)
                    {
                        if (currentService.IsFree == false)
                            service = currentService;
                    }

                return service;
            }
        }

        public static ADSLService GetADSLServicebyID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLServices.Where(t => t.ID == id).SingleOrDefault();
            }
        }
    }
}
