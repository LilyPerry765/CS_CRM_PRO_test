using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class CabinetCentralCableInfoDB
    {
        public static List<ConnectionInfo> GetConnectionColumnInfo()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.VerticalMDFColumns
                        .Select(t => new ConnectionInfo
                        {
                            VerticalColumnNo = t.VerticalCloumnNo,
                            VerticalColumnID = t.ID
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
        public static List<ConnectionInfo> GetConnectionBuchtInfo(int verticalMDFRowID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.MDFRowID== verticalMDFRowID)
                        .Select(t => new ConnectionInfo
                        {
                            BuchtID = t.ID,
                            BuchtNo = t.BuchtNo,

                        }).ToList();
            }
        }

        public static List<ConnectionInfo> GetConnectionList(int MDFID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID == MDFID)
                    .Select(t => new ConnectionInfo
                    {
                       CenterID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
                       MDFID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                       BuchtID = t.ID,
                       BuchtNo = t.BuchtNo,
                       BuchtStatus = t.Status,
                       VerticalRowID =  t.VerticalMDFRow.ID,
                       VerticalRowNo = t.VerticalMDFRow.VerticalRowNo,
                       VerticalColumnID = t.VerticalMDFRow.VerticalMDFColumn.ID,
                       VerticalColumnNo = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                    })
                    .OrderBy(t => t.VerticalColumnNo)
                    .ThenBy(t => t.VerticalRowNo)
                    .ThenBy(t => t.BuchtNo)
                    .ToList();
            }
        }

        public static List<CabinetInput> GetListCabinetInputNumberOfNumberTo(int fromID, int toID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.CabinetInputs.Where(t => t.ID >= fromID && t.ID <= toID).ToList();
            }
        }

       
    }
    //(DB.CurrentUser.CenterIDs.Contains(t.CenterID)
   

}
