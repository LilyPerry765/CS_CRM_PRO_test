using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class PCMDeviceFormDB
    {

      
        public static List<ConnectionInfo> GetConnectionBuchtInfo(int verticalMDFRowID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.MDFRowID == verticalMDFRowID && t.Status==0)
                        .Select(t => new ConnectionInfo
                        {
                            BuchtNo = t.BuchtNo,
                            BuchtID = t.ID,

                        }).ToList();
            }
        }
        public static List<ConnectionInfo> GetConnectionRowInfo(int verticalMDFColumnID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFRows.Where(t => t.VerticalMDFColumn.ID == verticalMDFColumnID)
                        .Select(t => new ConnectionInfo
                        {
                            VerticalRowNo = t.VerticalRowNo,
                            VerticalRowID = t.ID,

                        }).ToList();
            }
        }



        public static List<ConnectionInfo> GetConnectionColumnInfo()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFColumns.Where(t => DB.CurrentUser.CenterIDs.Contains(t.MDFFrame.MDF.CenterID))
                        .Select(t => new ConnectionInfo
                        {
                            VerticalColumnNo = t.VerticalCloumnNo,
                            MFDAndVerticalColumnNo = t.VerticalCloumnNo.ToString() + "-" + t.MDFFrame.FrameNo.ToString() + "-" + t.MDFFrame.MDF.Number.ToString(),
                            VerticalColumnID = t.ID
                        }).ToList();
            }
        }
    }
}
