using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class E1DDFDB
    {
        public static int SearchE1WayCount(List<int> Centers, int DDFNumber)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1DDFs
                    .Where(t =>
                        (Centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : Centers.Contains(t.CenterID)) &&
                        (DDFNumber == -1 || t.Number == DDFNumber)
                          )
                    .Count();
            }
        }

        public static List<E1DDF> SearchE1Way(List<int> Centers, int DDFNumber, int startRowIndex, int pageSize)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1DDFs
                         .Where(t =>
                             (Centers.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.CenterID) : Centers.Contains(t.CenterID)) &&
                             (DDFNumber == -1 || t.Number == DDFNumber)
                               ).OrderBy(t => t.Number).Skip(startRowIndex).Take(pageSize).ToList();
            }
        }

        public static E1DDF GetE1DDFByID(int ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1DDFs.Where(t => t.ID == ID).SingleOrDefault();
            }
        }
        public static List<CheckableItem> GetDDFCheckableByCenterIDs(List<int> centerIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1DDFs
                    .Where(t => centerIDs.Contains(t.CenterID)).AsEnumerable()
                    .Select(t => new CheckableItem { ID = t.ID, Name = t.Number.ToString(), IsChecked = false , Description = GetDescription(t.DDFType)}).ToList();
            }
        }

        private static string GetDescription(byte DDFID)
        {
            Type enumType = typeof(DB.DDFType);

            System.Reflection.FieldInfo fieldInfos = enumType.GetFields().Where(t => t.IsLiteral && Convert.ToInt32(t.GetRawConstantValue()) == DDFID).SingleOrDefault();
            return (fieldInfos.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false)[0] as System.ComponentModel.DescriptionAttribute).Description;
        }

        public static List<CheckableItem> GetDDFCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.E1DDFs.Where(t => DB.CurrentUser.CenterIDs.Contains(t.CenterID)).Select(t => new CheckableItem { ID = t.ID, Name = t.Number.ToString(), IsChecked = false }).ToList();
            }
        }

        public static List<CheckableItem> GetDDFCheckableByTypeAndCenterID(byte DDFType , int centerID)
        {
            using(MainDataContext context = new MainDataContext())
            {
                return context.E1DDFs.Where(t => t.DDFType == DDFType && centerID == t.CenterID)
                        .Select(t => new CheckableItem
                        {
                            ID = t.ID,
                            Name = t.Number.ToString(),
                            IsChecked = false
                        }
                        ).ToList();
            }
        }
    }
}
