using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using CRM.Data;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.ComponentModel;

namespace CRM.ADSLPortal
{
    public class Helper
    {
        private static bool IsValidType(object o, Type t)
        {
            try
            {
                System.Convert.ChangeType(o, t);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void Save(object instance, bool isNew = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                MetaDataMember primaryKey = context.Mapping.GetTable(instance.GetType()).RowType.IdentityMembers[0];
                MetaDataMember insertDateField = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "InsertDate").SingleOrDefault();
                MetaDataMember modifyDate = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "ModifyDate").SingleOrDefault();

                object obj = instance;

                if (modifyDate != null)
                    modifyDate.MemberAccessor.SetBoxedValue(ref obj, DB.GetServerDate());


                if (isNew || (primaryKey.MemberAccessor.GetBoxedValue(instance) == null || (IsValidType(primaryKey.MemberAccessor.GetBoxedValue(instance), typeof(Int64))) && Convert.ToInt64(primaryKey.MemberAccessor.GetBoxedValue(instance)) == 0))
                {
                    if (insertDateField != null)
                        insertDateField.MemberAccessor.SetBoxedValue(ref obj, DB.GetServerDate());

                    context.GetTable(instance.GetType()).InsertOnSubmit(instance);
                }
                else
                {
                    context.GetTable(instance.GetType()).Attach(instance);
                    context.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, instance);
                }
                context.SubmitChanges();
            }
        }

        public static bool AuthenticateUser(string userName, string password, out bool loginResult, out bool isActiveUser)
        {
            bool result = true;
            loginResult = true;
            isActiveUser = true;

            using (MainDataContext context = new MainDataContext())
            {
                PAPInfoUser papUser = context.PAPInfoUsers.Where(t => t.User.UserName == userName && t.Password == password).SingleOrDefault();
                if (papUser != null)
                {
                    if (!papUser.IsEnable)
                    {
                        isActiveUser = false;
                        result = false;
                    }
                    else
                    {
                        if (papUser.PAPInfo.LoginStatus)
                            loginResult = true;
                        else
                        {
                            result = false;
                            loginResult = false;
                        }
                    }
                }
                else
                    result = false;
            }

            return result;
        }

        public static DateTime? PersianToGregorian(string persianDate)
        {
            if (string.IsNullOrEmpty(persianDate)) return null;

            try
            {
                PersianCalendar persianCalendar = new PersianCalendar();
                string[] tokens = persianDate.Split('/');

                if (tokens[0].Length == 2)
                    tokens[0] = "13" + tokens[0];
                int year = Int32.Parse(tokens[0]);
                int month = Int32.Parse(tokens[1]);
                int day = Int32.Parse(tokens[2]);

                if (day < 1 || day > 31 || month < 1 || month > 12 || year < 1300 || year > 1400) throw new ApplicationException("تاریخ نامعتبر است");

                return persianCalendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetEnumDescriptionByValue(Type enumType, int value)
        {
            FieldInfo fieldInfos = enumType.GetFields().Where(t => t.IsLiteral && Convert.ToInt32(t.GetRawConstantValue()) == value).SingleOrDefault();
            return (fieldInfos.GetCustomAttributes(typeof(DescriptionAttribute), false)[0] as DescriptionAttribute).Description;
        }

        public static long GetMaxRequestIDByDate(DateTime date)
        {
            using (MainDataContext context = new MainDataContext())
            {
                long requestId = 0;
                if (context.Requests.Where(t => t.InsertDate.Date == date.Date).ToList().Count != 0)
                    requestId = context.Requests
                            .Where(t => t.InsertDate.Date == date.Date).Max(t => t.ID);

                return requestId;
            }
        }

        public static long GenerateRequestID()
        {
            DateTime serverDate = DB.GetServerDate();

            long requestID = GetMaxRequestIDByDate(serverDate) + 1;

            if (requestID == 1)
                requestID = long.Parse(Date.GetPersianDate(serverDate, Date.DateStringType.NoSlash) + "000" + requestID.ToString());

            return requestID;
        }
    }
}