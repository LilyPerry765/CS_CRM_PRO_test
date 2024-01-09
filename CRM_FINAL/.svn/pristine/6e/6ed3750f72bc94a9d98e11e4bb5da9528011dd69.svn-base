using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data.CustomerHost
{
    public class SecurityDB
    {
        public static string UserAuthentication(string username, string password)
        {
           try
           {

               using(MainDataContext context = new MainDataContext())
               {
                   string codedPass = Folder.Cryptography.Encrypt(password);
                   Customer customer = context.Customers.Where(t => t.UserName == username && t.Password == codedPass).SingleOrDefault();

                   if(customer == null ||  customer.ID == 0)
                   {
                       return string.Empty;
                   }
                   else
                   {
                       return customer.UserName;
                   }
               }

           }
           catch
           {
               return string.Empty;
           }
        }
    }
}
