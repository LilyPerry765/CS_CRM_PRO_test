using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class ADSLModemDB
    {
        public static ADSLModem GetADSLModemById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModems
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }       

        public static List<ADSLModemInfo> SearchADSLModems(string title, string model, string price)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModems
                    .Where(t => (string.IsNullOrWhiteSpace(title) || t.Title.Contains(title)) &&
                                (string.IsNullOrWhiteSpace(model) || t.Model.Contains(model)) &&
                                (string.IsNullOrWhiteSpace(price) || t.Price.ToString().Contains(price)))
                    .OrderBy(t => t.Title).Select(t => new ADSLModemInfo
                    {
                        ID = t.ID,
                        Title = t.Title + " - " + t.Model,
                        Model = t.Model,
                        Price = t.Price.ToString(),
                        IsSalable = t.IsSalable,
                        Description = t.Description
                    }).ToList();
            }
        }

        public static List<CheckableItem> GetADSLModemsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModems.Select(t => new CheckableItem
                {
                    ID = t.ID,
                    Name = t.Title + " - " + t.Model,
                    IsChecked = false
                }
                ).ToList();
            }
        }

        public static List<ADSLModemInfo> GetSalableModemsTitle(bool wireless = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModems.Where(t => (t.IsSalable == true) && (!wireless || t.ForWireless == wireless))
                                   .Select(t => new ADSLModemInfo
                                   {
                                       ID=t.ID,
                                       Title = t.Title + " - " + t.Model
                                   }).ToList();
            }
        }

        public static int GetModemIDByModel(string Model)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModems.Where(t => string.IsNullOrEmpty(Model) || (t.Title + ":" + t.Model).Equals(Model))
                                   .Select(t => t.ID).SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetModemMOdelsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModems.Where(t => t.IsSalable == true)
                                   .Select(t => new CheckableItem
                                   {
                                       ID = t.ID,
                                       Name = t.Title + " - " + t.Model,
                                       IsChecked=false
                                   }).ToList();
            }
        }

        public static string GetSalableModemsTitleByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModems.Where(t => t.ID == id)
                                   .Select(t => new ADSLModemInfo
                                   {
                                       Title = t.Title + " - " + t.Model
                                   }).SingleOrDefault().Title;
            }
        }


        public static List<CheckableItem> GetAllModemMOdelsCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLModems
                                   .Select(t => new CheckableItem
                                   {
                                       ID = t.ID,
                                       Name = t.Title + " - " + t.Model,
                                       IsChecked = false
                                   }).ToList();
            }
        }
  
    }
}
