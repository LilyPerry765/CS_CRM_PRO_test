using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;


namespace CRM.Data
{
    public static class SwitchPortDB
    {
        public static List<SwitchPort> GetSwitchPorts()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPorts.ToList();
            }
        }

        public static List<Pair> GetSwitchPortsNameValue()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPorts
                        .Select(t => new Pair
                        {
                            Name = t.PortNo,
                            Value = t.ID
                        })
                        .ToList();
            }
        }

        public static List<SwitchPort> GetSwitchPortsByCenterID(int centerID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPorts.Where(p => p.Switch.CenterID == centerID).ToList();

            }
        }


        public static List<CheckableItem> GetSwitchPortCheckable(int? centerID = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPorts.Where(t => centerID == null ? DB.CurrentUser.CenterIDs.Contains(t.Switch.CenterID) : t.Switch.CenterID == centerID).Select(t => new CheckableItem { ID = t.ID, Name = t.PortNo, IsChecked = false }).ToList();
            }
        }

        public static List<SwitchPort> GetSwitchPortsBySwitch(Data.SwitchPrecode switchPrecode)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPorts.Where(p => p.SwitchID == switchPrecode.SwitchID && p.Status == (byte)DB.SwitchPortStatus.Free).ToList();
            }
        }

        public static List<SwitchPortInfo> SearchSwitchPort(List<int> status, List<int> switchList, bool? type, string portNo, string mDFHorizentalID, List<int> centerList, int startRowIndex, int pageSize, out int count)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<SwitchPortInfo> result = new List<SwitchPortInfo>();
                var query = context.SwitchPorts
                                   .GroupJoin(context.Telephones, sp => sp.ID, t => t.SwitchPortID, (sp, t) => new { SwitchPorts = sp, Telephones = t })
                                   .SelectMany(t => t.Telephones.DefaultIfEmpty(), (t1, t2) => new { SwitchPorts = t1.SwitchPorts, Telephones = t2 })
                                   .Where(t =>
                                               (status.Count == 0 || status.Contains(t.SwitchPorts.Status)) &&
                                               (switchList.Count == 0 || switchList.Contains(t.SwitchPorts.SwitchID)) &&
                                               (!type.HasValue || t.SwitchPorts.Type == type) &&
                                               (string.IsNullOrWhiteSpace(portNo) || t.SwitchPorts.PortNo.Contains(portNo)) &&
                                               (string.IsNullOrWhiteSpace(mDFHorizentalID) || t.SwitchPorts.MDFHorizentalID.Contains(mDFHorizentalID)) &&
                                               (centerList.Count == 0 || centerList.Contains(t.SwitchPorts.Switch.CenterID))
                                         )
                                   .AsQueryable();
                count = query.Count();
                result = query.Select(t => new SwitchPortInfo
                                           {

                                               ID = t.SwitchPorts.ID,
                                               MDFHorizentalID = t.SwitchPorts.MDFHorizentalID,
                                               PortNo = t.SwitchPorts.PortNo,
                                               Status = t.SwitchPorts.Status,
                                               SwitchID = t.SwitchPorts.SwitchID,
                                               SwitchCode = t.SwitchPorts.Switch.SwitchCode,
                                               Telephone = t.Telephones.TelephoneNo,
                                               Type = t.SwitchPorts.Type
                                           }
                                    )
                             .Skip(startRowIndex)
                             .Take(pageSize)
                             .ToList();
                return result;
            }
        }

        public static int SearchSwitchPortCount(List<int> status, List<int> switchList, bool? type, string portNo, string mDFHorizentalID, List<int> centerList)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPorts
                    .GroupJoin(context.Telephones, sp => sp.ID, t => t.SwitchPortID, (sp, t) => new { SwitchPorts = sp, Telephones = t })
                    .SelectMany(t => t.Telephones.DefaultIfEmpty(), (t1, t2) => new { SwitchPorts = t1.SwitchPorts, Telephones = t2 })
                    .Where(t =>
                                (status.Count == 0 || status.Contains(t.SwitchPorts.Status)) &&
                                (switchList.Count == 0 || switchList.Contains(t.SwitchPorts.SwitchID)) &&
                                (!type.HasValue || t.SwitchPorts.Type == type) &&
                                (string.IsNullOrWhiteSpace(portNo) || t.SwitchPorts.PortNo.Contains(portNo)) &&
                                (string.IsNullOrWhiteSpace(mDFHorizentalID) || t.SwitchPorts.MDFHorizentalID.Contains(mDFHorizentalID)) &&
                                (centerList.Count == 0 || centerList.Contains(t.SwitchPorts.Switch.CenterID))
                          )
                    .Select(t => new SwitchPortInfo
                    {

                        ID = t.SwitchPorts.ID,
                        MDFHorizentalID = t.SwitchPorts.MDFHorizentalID,
                        PortNo = t.SwitchPorts.PortNo,
                        Status = t.SwitchPorts.Status,
                        SwitchID = t.SwitchPorts.SwitchID,
                        SwitchCode = t.SwitchPorts.Switch.SwitchCode,
                        Telephone = t.Telephones.TelephoneNo,
                        Type = t.SwitchPorts.Type
                    }).Count();
            }
        }
        public static SwitchPort GetSwitchPortByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPorts
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<SwitchPort> getSwitchPortBySwitchPortIDs(List<int> SwitchPortIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPorts.Where(t => SwitchPortIDs.Contains(t.ID)).ToList();
            }
        }

        public static List<SwitchPort> GetSwitchPortsFreeOfBuchtBySwitch(SwitchPrecode switchPrecodeItem)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPorts.Where(p => p.SwitchID == switchPrecodeItem.SwitchID &&
                                                      p.Status == (byte)DB.SwitchPortStatus.Free &&
                                                      !context.Buchts.Where(t => t.SwitchPortID != null).Select(b => b.SwitchPortID).Contains(p.ID)).ToList();
            }
        }

        public static List<SwitchPort> GetSwitchPortsFreeOfOpticalBuchtBySwitch(SwitchPrecode switchPrecodeItem)
        {
            using (MainDataContext context = new MainDataContext())
            {

                return context.SwitchPorts.Where(p => p.SwitchID == switchPrecodeItem.SwitchID && p.Status == (byte)DB.SwitchPortStatus.Free).ToList();
                //IQueryable<BuchtType> buchtType = context.BuchtTypes.Where(t2 => t2.ID == (int)DB.BuchtType.OpticalBucht || t2.ParentID == (int)DB.BuchtType.OpticalBucht);
                //IQueryable<Bucht> buchts = context.Buchts.Where(t2 => t2.SwitchPortID != null);
                //IQueryable<SwitchPort> c= context.SwitchPorts
                //              .Where(p => p.SwitchID == switchPrecodeItem.SwitchID && p.Status == (byte)DB.SwitchPortStatus.Free)
                //              .GroupJoin(buchts, p => p.ID, b => b.SwitchPortID, (p, b) => new { SwitchPort = p, Bucht = b })
                //              .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { SwitchPort = t1.SwitchPort, Buchts = t2 })
                //              .Where(t =>  !buchtType.Select(t3 => t3.ID).Contains(t.Buchts.BuchtTypeID))
                //              .Select(t => t.SwitchPort);
                //return c.ToList();

                //return context.SwitchPorts.Where(p => p.SwitchID == switchPrecodeItem.SwitchID && p.Status == (byte)DB.SwitchPortStatus.Free &&
                //                                      !context.Buchts.Where(t => t.SwitchPortID != null && !buchtType.Select(t2=>t2.ID).Contains(t.BuchtTypeID))
                //                                                    .Select(b => b.SwitchPortID).Contains(p.ID)).ToList();
            }
        }

        internal static List<SwitchPort> GetSwitchPortByPortNo(List<string> portNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPorts.Where(t => portNo.Contains(t.PortNo)).ToList();

            }
        }
    }
}

//////using System;
//////using System.Collections.Generic;
//////using System.Linq;
//////using System.Text;
//////using System.Collections;
//////using System.Data.Linq;

//////namespace CRM.Data
//////{
//////    public static class SwitchPortDB
//////    {
//////        public static List<SwitchPort> GetSwitchPorts()
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.SwitchPorts.ToList();
//////            }
//////        }

//////        public static List<Pair> GetSwitchPortsNameValue()
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.SwitchPorts
//////                        .Select(t => new Pair
//////                        {
//////                            Name = t.PortNo,
//////                            Value = t.ID
//////                        })
//////                        .ToList();
//////            }
//////        }

//////        public static List<SwitchPort> GetSwitchPortsByCenterID(int centerID)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.SwitchPorts.Where(p => p.Switch.CenterID == centerID).ToList();

//////            }
//////        }

//////        public static List<SwitchPort> GetSwitchPortsBySwitch(Switch switchItem)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.SwitchPorts.Where(p => p.SwitchID == switchItem.ID && switchItem.PreCodeType == p.Switch.PreCodeType).ToList();

//////            }
//////        }
//////    }
//////}