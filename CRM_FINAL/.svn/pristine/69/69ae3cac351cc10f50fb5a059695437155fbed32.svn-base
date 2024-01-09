using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Collections;

namespace CRM.Data
{
    public static class BankDB
    {
        public static Bank GetBankById(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Banks
                    .Where(t => t.ID == id)
                    .SingleOrDefault(); 
            }
        }

        public static List<Bank> SearchBanks(string bankName, bool? status)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Banks
                    .Where(t => (string.IsNullOrWhiteSpace(bankName) || t.BankName.Contains(bankName)) &&
                                (!status.HasValue || t.Status == status))
                    .OrderBy(t => t.BankName).ToList();
            }
        }

        public static List<CheckableItem> GetBanksCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Banks.Select(t => new CheckableItem
                {
                    ID = t.ID,
                    Name = t.BankName,
                    IsChecked = false
                }
                ).ToList();
            }
        }

        public static int GetBankIDbyCode(int code)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Banks.Where(t => t.BankCode == code).SingleOrDefault().ID;
            }
        }
    }
}
