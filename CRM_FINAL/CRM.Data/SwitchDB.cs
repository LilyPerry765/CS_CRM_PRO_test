using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class SwitchDB
    {

        public static List<Switch> SearchData
            (
                List<int> center,
                List<int> switchType,
                List<int> workUnitResponsible,
                string switchCode,
                int capacity,
                int operationalCapacity,
                int installCapacity,
                List<int> status
            )
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Switches
                    .Where(t => (center.Count == 0 || center.Contains(t.CenterID)) &&
                        (switchType.Count == 0 || switchType.Contains(t.SwitchTypeID)) &&
                        (workUnitResponsible.Count == 0 || workUnitResponsible.Contains(t.WorkUnitResponsible)) &&
                        (string.IsNullOrEmpty(switchCode) || t.SwitchCode == switchCode) &&
                        //(switchPreNo == -1 || t.SwitchPrecodes.Where(s => s.SwitchID == t.ID && s.SwitchPreNo == switchPreNo).SingleOrDefault().SwitchPreNo == switchPreNo) &&
                        //(preCodeType.Count == 0 || preCodeType.Contains(t.SwitchPrecodes.Where(s => s.SwitchID == t.ID && s.SwitchPreNo == switchPreNo).SingleOrDefault().PreCodeType)) &&
                        //(startNo==-1 || t.StartNo == startNo) &&
                        //(endNo==-1 || t.EndNo == endNo) &&
                        (capacity == -1 || t.Capacity == capacity) &&
                        (operationalCapacity == -1 || t.OperationalCapacity == operationalCapacity) &&
                        (installCapacity == -1 || t.InstallCapacity == installCapacity) &&
                        //(specialServiceCapacity == -1 || t.SpecialServiceCapacity == specialServiceCapacity) &&
                        //(deploymentType.Count == 0 || deploymentType.Contains(t.DeploymentType)) &&
                        //(dorshoalNumberType.Count == 0 || dorshoalNumberType.Contains((int)t.DorshoalNumberType)) &&
                        (status.Count == 0 || status.Contains(t.Status))

                    ).ToList();
            }
        }

        public static byte GetTypeOfSwitchTypeBySwitchID(int switchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                byte result = default(byte);
                result = context.SwitchTypes
                                .Where(t =>
                                           t.Switches.Where(s => s.ID == switchID).SingleOrDefault().SwitchTypeID == t.ID
                                      )
                                .SingleOrDefault()
                                .SwitchTypeValue;
                return result;
            }
        }

        public static List<CheckableItem> GetSwitchCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Switches.Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID)).Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchCode.ToString(), IsChecked = false }).ToList();
            }
        }

        public static List<CheckableItem> GetSwitchWithCenterNameCheckableByCentersID(List<int> centersID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                result = context.Switches
                                .Where(sw =>
                                            (DB.CurrentUser.CenterIDs.Contains(sw.CenterID)) &&
                                            (centersID.Contains(sw.CenterID))
                                      )
                                .Select(sw => new CheckableItem
                                              {
                                                  ID = sw.ID,
                                                  Name = sw.Center.Region.City.Name + " - " + sw.Center.CenterName + " - " + sw.SwitchCode,
                                                  IsChecked = false
                                              }
                                       )
                                .OrderBy(ci => ci.Name)
                                .ToList();
                return result;
            }
        }

        public static List<CheckableItem> GetSwitchCheckableByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Switches.Where(t => t.CenterID == centerID).Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchCode.ToString(), Description = t.SwitchType.CommercialName, IsChecked = false }).ToList();
            }
        }
        public static List<Switch> GetSwitchByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Switches.Where(t => t.CenterID == centerID).ToList();
            }
        }
        public static Switch GetSwitchByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Switches
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static TelInfo GetSwitchInfoByTelNo(long telNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telNo).Select(t => new TelInfo
                {

                    switchID = t.SwitchPrecode.SwitchID,
                    TelephoneNo = t.TelephoneNo,
                    portNo = t.SwitchPort.PortNo,
                    switchPortID = t.SwitchPort.ID,
                    portType = t.SwitchPort.Type,
                    switchPreNo = t.SwitchPrecode.SwitchPreNo,
                    SwitchPrecodeID = t.SwitchPrecode.ID,
                    switchPreCodeType = t.SwitchPrecode.PreCodeType,

                }).SingleOrDefault();
            }
        }


        public static bool ExistSwitchCode(int centerID, string switchCode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Switches.Any(t => t.CenterID == centerID && t.SwitchCode == switchCode);
            }
        }


        public static System.Collections.IEnumerable GetSwitchCheckableByInfo(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Switches.Where(t => t.CenterID == centerID).AsEnumerable().Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchCode.ToString() + GetFeatureONUInfo(t.FeatureONU), IsChecked = false }).ToList();
            }
        }

        private static string GetFeatureONUInfo(string featureONU)
        {
            if (featureONU == null)
                return
                    string.Empty;
            else
                return
                    " ( " + featureONU + " ) ";

        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Switches.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        public static SwitchType GetSwitchTypeBySwitchID(int switchID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchTypes.Where(t => t.Switches.Where(s => s.ID == switchID).Select(s => s.SwitchTypeID).SingleOrDefault() == t.ID).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetOpticalCabinetSwitchbyCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                var query = context.Switches
                                   .Where(t =>
                                             t.CenterID == centerID &&
                                             (t.SwitchType.SwitchTypeValue == (int)DB.SwitchTypeCode.ONUCopper || t.SwitchType.SwitchTypeValue == (int)DB.SwitchTypeCode.ONUVWire)
                                         )
                                   .OrderBy(t => Convert.ToInt32(t.SwitchCode))
                                   .Select(t => new CheckableItem
                                                     {
                                                         ID = t.ID,
                                                         Name = t.SwitchCode,
                                                         IsChecked = false
                                                     }
                                         )
                                   .AsQueryable();
                result = query.ToList();
                return result;
            }

        }

        public static Switch GetSwitchByTelephonNo(long telephonNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephonNo).SingleOrDefault().SwitchPrecode.Switch;
            }
        }

        public static List<CheckableItem> GetWLLCabinetSwitchbyCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Switches.Where(t => t.CenterID == centerID && t.SwitchType.SwitchTypeValue == (int)DB.SwitchTypeCode.WLL).Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchCode, IsChecked = false }).ToList();
            }
        }
    }

}



//////using System;
//////using System.Collections.Generic;
//////using System.Linq;
//////using System.Text;

//////namespace CRM.Data
//////{
//////    public static class SwitchDB
//////    {

//////        public static List<Switch> SearchData
//////            (
//////            List<int> center,
//////            List<int> switchType,
//////            List<int> workUnitResponsible,
//////            int switchCode,
//////            int capacity,
//////            int operationalCapacity,
//////            int installCapacity,
//////            List<int> status
//////            )
//////        {

//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.Switches
//////                    .Where(t => (center.Count == 0 || center.Contains(t.CenterID)) &&
//////                        (switchType.Count == 0 || switchType.Contains(t.SwitchTypeID)) &&
//////                        (workUnitResponsible.Count == 0 || workUnitResponsible.Contains(t.WorkUnitResponsible)) &&
//////                        (switchCode == -1 || t.SwitchCode == switchCode) &&
//////                        (capacity == -1 || t.Capacity == capacity) &&
//////                        (operationalCapacity == -1 || t.OperationalCapacity == operationalCapacity) &&
//////                        (installCapacity == -1 || t.InstallCapacity == installCapacity) &&
//////                        (status.Count == 0 || status.Contains(t.Status))

//////                    ).ToList();
//////            }

//////        }

//////        public static Switch GetSwitchByID(int id)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.Switches
//////                    .Where(t => t.ID == id)
//////                    .SingleOrDefault();
//////            }
//////        }

//////        public static TelInfo GetSwitchInfoByTelNo(long telNo)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.Telephones.Where(t => t.TelephoneNo == telNo).Select(t => new TelInfo
//////                {
//////                    switchID = t.SwitchPort.Switch.ID,
//////                    TelephoneNo = t.TelephoneNo,
//////                    portNo = t.SwitchPort.PortNo,
//////                    portType = t.SwitchPort.Type
//////                }
//////                                                                                     ).SingleOrDefault();
//////            }
//////        }


//////        public static List<CheckableItem> GetSwitchCheckable()
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.Switches.Select(t => new CheckableItem { ID = t.ID, Name = t.SwitchCode.ToString() , IsChecked = false }).ToList();
//////            }
//////        }
//////    }
//////    public class TelInfo
//////    {
//////        public long TelephoneNo { get; set; }
//////        public long? switchPreNo { get; set; }
//////        public string portNo { get; set; }
//////        public byte? switchPreCodeType { get; set; }
//////        public int? switchID { get; set; }
//////        public bool? portType { get; set; }
//////        public int investigatePossibilityStatus { get; set; }
//////    }
//////}
