using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Collections;
namespace CRM.Data
{
    public static class BankBranchDB
    {
        public static BankBranch GetBankBranchByID(int id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BankBranches
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }

        public static List<BankBranch> SearchBankBranch(bool? status, List<int> bankID, string branchCode, string branchName, string accountNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BankBranches
                    .Where(t =>
                        ((!status.HasValue || t.Status == status) &&
                        (bankID.Count == 0 || bankID.Contains(t.BankID)) &&
                        (string.IsNullOrWhiteSpace(branchCode) || t.BranchCode.Contains(branchCode)) &&
                        (string.IsNullOrWhiteSpace(branchName) || t.BranchName.Contains(branchName)) &&
                        (string.IsNullOrWhiteSpace(accountNo) || t.AccountNo.Contains(accountNo)))).OrderBy(t => t.BranchName).ToList();
            }
        }

        public static List<CheckableItem> GetBankBranchCheckable()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.BankBranches.Select(t=> new CheckableItem{ID=t.ID , Name =" بانک " + t.Bank.BankName +" شعبه "+t.BranchName ,IsChecked=false}).ToList();
            }
        }
    }
}
