using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.Linq;

namespace CRM.Data
{
    public static class ChangePreCodeDB
    {
        public static List<ChangePreCode> SearchChangePreCode(long oldPreCode, long newPreCode, long fromTelephonNo, long toTelephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ChangePreCodes
                    .Where(t =>
                            (oldPreCode == -1 || t.OldPreCodeID == oldPreCode) &&
                           (newPreCode == -1 || t.NewPreCode== newPreCode) &&
                            (fromTelephonNo == -1 || t.FromTelephonNo == fromTelephonNo) &&
                            (toTelephoneNo == -1 || t.ToTelephoneNo == toTelephoneNo)
                          )
                    .ToList();
            }
        }

        public static ChangePreCode GetChangePreCodeByID(long id)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ChangePreCodes
                    .Where(t => t.ID == id)
                    .SingleOrDefault();
            }
        }
        /// <summary>
        /// update telephoneNo with new telephone
        /// Linq can not update telephone since The TelephoneNo is Primery Key 
        /// That is why i using ExecuteCommand
        /// </summary>
        /// <param name="telephoneNo">OldTelephoneNo</param>
        /// <param name="newTelephoneNo">NewTelephoneNo</param>
        public static void UpdateTelephone(long telephoneNo, long newTelephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                context.ExecuteCommand("UPDATE Telephone SET TelephoneNo={1} WHERE TelephoneNo={0}", telephoneNo, newTelephoneNo);

            }
        }
    }
}