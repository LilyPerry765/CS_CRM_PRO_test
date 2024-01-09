using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public static class FicheDB
    {

        public static List<Fiche> SearchFiche(string ficheName, List<int> centre, DateTime? saleStartDate, DateTime? saleEndDate, DateTime? transferStartDate, DateTime? transferEndDate, List<int> status)
        {
   
            using (MainDataContext context = new MainDataContext())
            {
                return context.Fiches
                    .Where(t =>
                            (string.IsNullOrWhiteSpace(ficheName) || t.FicheName.Contains(ficheName)) &&
                            (centre.Count == 0 || centre.Contains((int)t.CentreID)) &&
                            (!saleStartDate.HasValue || t.SaleStartDate == saleStartDate) &&
                            (!saleEndDate.HasValue || t.SaleEndDate == saleEndDate) &&
                            (!transferStartDate.HasValue || t.TransferStartDate == transferStartDate) &&
                            (!transferEndDate.HasValue || t.TransferEndDate == transferEndDate) &&
                            (status.Count == 0 || status.Contains((int)t.Status))
                          )
                    .ToList();
            }
        }

        public static Fiche GetFicheByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Fiches
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<CheckableItem> GetFicheCheckable()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Fiches
                    .Select(t => new CheckableItem
                    {
                        ID = t.ID,
                        Name = t.FicheName.ToString(),
                        IsChecked = false
                    }
                        )
                    .ToList();
            }
        }

        public static int GetCity(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Fiches.Where(t => t.ID == id).SingleOrDefault().Center.Region.CityID;
            }
        }

        #region Get Methods

        public static Fiche GetCurrentFiche(DateTime date)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Fiches.Where(t => date >= t.SaleStartDate && date <= t.SaleEndDate).SingleOrDefault();

            }
        }

        public static List<Fiche> GetAllFiches()
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Fiches.Select(t => new Fiche { ID = t.ID, FicheName = t.FicheName, TransferStartDate = t.TransferStartDate, TransferEndDate = t.TransferEndDate, SaleStartDate = t.SaleStartDate }).ToList();

            }
        }

        #endregion Get Methods
    }
}
