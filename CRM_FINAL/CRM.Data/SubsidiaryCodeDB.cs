using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class SubsidiaryCodeDB
    {
        public static List<SubsidiaryCode> SearchSubsidiaryCodes(string code, string companyName)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SubsidiaryCodes
                    .Where(t => (string.IsNullOrWhiteSpace(code) || t.Code.Contains(code)) &&
                                (string.IsNullOrWhiteSpace(companyName) || t.CompanyName.Contains(companyName)))
                    .OrderBy(t => t.Code)
                    .ToList();
            }
        }

        public static SubsidiaryCode GetSubsidiaryCodeByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SubsidiaryCodes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetSubsidiaryCodesTelephoneCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SubsidiaryCodes.Where(t=>t.Type==(byte)DB.SubsidiaryCodeType.Telephone)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CompanyName + "              " + t.Code,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static List<CheckableItem> GetSubsidiaryCodesServiceCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SubsidiaryCodes.Where(t => t.Type == (byte)DB.SubsidiaryCodeType.Service)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CompanyName + "              " + t.Code,
                        IsChecked = false
                    })
                    .ToList();
            }
        }

        public static List<CheckableItem> GetSubsidiaryCodesADSLCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SubsidiaryCodes.Where(t => t.Type == (byte)DB.SubsidiaryCodeType.ADSL)
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.CompanyName + "              " + t.Code,
                        IsChecked = false
                    })
                    .ToList();
            }
        }
    }
}
