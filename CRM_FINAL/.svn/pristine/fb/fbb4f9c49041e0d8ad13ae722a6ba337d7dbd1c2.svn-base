using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Data.Linq.Mapping;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using Folder;
using Enterprise;
using System.Transactions;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using CRM.Data.Schema;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace CRM.Data
{
    public static partial class DB
    {
        //public static UserInfo CurrentUser { get; set; }
        /***/


        public static bool IsInWebSiteMode
        {
            get;
            set;
        }

        public static UserInfo currentUser = new UserInfo();

        public static UserInfo CurrentUser
        {
            get
            {
                if (IsInWebSiteMode)
                {
                    if (HttpContext.Current.Session["CurrentUser"] == null)
                        HttpContext.Current.Session["CurrentUser"] = new UserInfo();
                    return HttpContext.Current.Session["CurrentUser"] as UserInfo;
                }
                else
                    return currentUser;
            }
            set
            {
                if (IsInWebSiteMode)
                    HttpContext.Current.Session["CurrentUser"] = value;
                else
                    currentUser = value;
            }
        }
        /***/

        public static string City { get; set; }

        public static string PersianCity
        {
            get;
            set;
        }

        /// <summary>
        /// .این متد با استفاده از نام انگلیسی یک شهر که در جدول تنظیمات ذخیره شده است، معادل فارسی نام آن شهر را برمیگرداند
        /// </summary>
        /// <param name="englishCityName">نام انگلیسی شهر</param>
        /// <returns></returns>Written by rad.
        public static string GetPerainCityNameByEnglishCityName(string englishCityName)
        {
            string persianCityName = string.Empty;
            if (!string.IsNullOrEmpty(englishCityName))
            {
                switch (englishCityName)
                {
                    case "kermanshah":
                        {
                            persianCityName = "کرمانشاه";
                            break;
                        }
                    case "tehran":
                        {
                            persianCityName = "سمنان";
                            break;
                        }
                }
            }
            return persianCityName;
        }

        public static MDF MDF { get; set; }

        public static string GetEnumDescription(int value, Type enumtype)
        {
            FieldInfo fi = enumtype.GetField((((CRM.Data.DB.TelephoneType[])(enumtype.GetEnumValues()))[value]).ToString());
            //((CRM.Data.DB.TelephoneType[])(enumtype.GetEnumValues()))[value];
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();

            //int value = 1; string description = Enumerations.GetEnumDescription((MyEnum)value); 

        }

        public static string GetEnumDescriptionByValue(Type enumType, int? value)
        {
            if (value != null)
            {
                FieldInfo fieldInfos = enumType.GetFields().Where(t => t.IsLiteral && Convert.ToInt32(t.GetRawConstantValue()) == value).SingleOrDefault();
                if (fieldInfos != null)
                    return (fieldInfos.GetCustomAttributes(typeof(DescriptionAttribute), false)[0] as DescriptionAttribute).Description;
                else
                    return "";

            }
            else
                return "";
        }

        public static DateTime GetServerDate()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExecuteQuery<DateTime>("SELECT GETDATE()").SingleOrDefault();
            }
        }

        public static DateTime GetServerCompleteFormatDate()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ufnGetServerCompleteDateTime().Value;
            }
        }

        public static string GetSettingByKey(string key)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var x = context.Settings.Where(t => t.Key.ToLower() == key.ToLower())
                            .SingleOrDefault()
                            .Value;
                return x;
            }
        }

        public static byte[] GetSettingContentByKey(string key)
        {
            using (MainDataContext context = new MainDataContext())
            {
                try
                {
                    return context.Settings.Where(current => current.Key.ToLower() == key.ToLower())
                                .SingleOrDefault()
                                .Content.ToArray();
                }
                catch (Exception)
                {
                    return (null);
                }
            }
        }

        public static void SetSettingContentByKey(string key, byte[] content)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var varItem = context.Settings
                    .First(current => current.Key.ToLower() == key.ToLower());

                varItem.Content = content;

                context.SubmitChanges();
            }
        }

        public static DateTime? ServerDate()
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.serverdate();
            }
        }

        public static void InsertFile(string name)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string path = "D:\\Folder\\Files\\" + name;
                string query = "Insert into DocumentsFile([name],[file_stream])   SELECT '" + name + "', *  FROM OPENROWSET(BULK  '" + path + "', SINGLE_BLOB) AS FileData";

                context.ExecuteQuery<object>(query);
            }
        }

        public static System.Data.DataSet ToDataSet<T>(this IList<T> list)
        {
            Type elementType = typeof(T);
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable t = new System.Data.DataTable();
            t.TableName = elementType.Name;
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                t.Columns.Add(propInfo.Name, ColType);
            }

            //go through each property on T and add each value to the table
            foreach (T item in list)
            {
                System.Data.DataRow row = t.NewRow();

                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }

                t.Rows.Add(row);
            }

            return ds;
        }

        //TODO:rad
        public static T GetDescriptiveLogEntity<T>(XElement description)
        {
            T result = default(T);
            if (description != null)
            {
                result = LogSchemaUtility.Deserialize<T>(description.ToString());
            }
            return result;
        }


        #region Select

        public static string GetAboneConnection(byte sourceType, long connectionID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string conNo = string.Empty;
                string centralcableno = string.Empty;
                centralcableno = context.Buchts.Where(b => b.ID == connectionID).Select(s => s.CablePair.CablePairNumber.ToString()).FirstOrDefault() + "-";
                Bucht bucht = context.Buchts.Where(b => b.ID == connectionID).Take(1).SingleOrDefault();
                if (sourceType == (byte)DB.SourceType.Post)
                {
                    if (bucht.ConnectionID != null)
                        conNo = context.PostContacts.Where(t => t.ID == bucht.ConnectionID).Select(t => t.Post.Cabinet.CabinetNumber.ToString() + "-"
                            + centralcableno
                            + t.Post.Number.ToString() + "-" + t.ConnectionNo.ToString()).SingleOrDefault().ToString();
                }
                else if (sourceType == (byte)DB.SourceType.PCM)
                {
                    conNo = context.Buchts.Where(t => t.ID == connectionID).Select(t => t.CabinetInput.Cabinet.CabinetNumber.ToString() + "-"
                        + centralcableno
                        + t.PostContact.Post.Number.ToString() + "-" + t.PostContact.ConnectionNo.ToString() + "," + t.PortNo.ToString()).SingleOrDefault().ToString();
                }

                return conNo;
            }

        }

        public static IEnumerable<StepStatusInfo> GetStepStatus(int requestTypeID, int step)
        {
            using (MainDataContext context = new MainDataContext())
            {

                var hierachy = context.RequestSteps
                                      .Join(context.Status, rs => rs.ID, ss => ss.RequestStepID, (rs, ss) => new { rquestStep = rs, stepStatus = ss })

                                      .Where(x => x.rquestStep.RequestTypeID == requestTypeID && x.rquestStep.ID == step)

                                      .Select(z => new StepStatusInfo
                                                {
                                                    reqStepID = z.rquestStep.ID,
                                                    StepStatusID = z.stepStatus.ID,
                                                    ParentStepStatusID = z.stepStatus.ParentID,
                                                    StatusResult = z.stepStatus.Title
                                                }
                                            )
                                      .ToList();
                return hierachy;

            }
        }

        public static void UpdateAll<T>(List<T> instance) where T : class
        {

            using (MainDataContext context = new MainDataContext())
            {
                Logger.WriteInfo(context.GetTable<T>().GetType().Name);
                context.GetTable(typeof(T)).AttachAll(instance);
                context.Refresh(RefreshMode.KeepCurrentValues, instance);
                context.SubmitChanges();
            }
        }

        public static SwitchCodeInfo GetSwitchCodeInfo(long telno)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telno)
                 .Select(t => new SwitchCodeInfo
                          {
                              TelephoneNo = t.TelephoneNo,
                              SwitchPreNo = t.SwitchPrecode.SwitchPreNo,
                              PreCodeType = t.SwitchPrecode.PreCodeType,
                              CommercialName = t.SwitchPrecode.Switch.SwitchType.CommercialName,
                              PortNo = t.SwitchPort.PortNo,
                              CenterName = t.Center.CenterName,
                              FeatureONU = t.SwitchPrecode.Switch.FeatureONU

                          }).SingleOrDefault();
                //return  context.Telephones.Join(context.Switches, t => t.CenterID, s => s.CenterID, (t, s) => new { tel = t, sw = s }).Where(x =>x.tel.TelephoneNo==telno && x.tel.TelephoneNo >= x.sw.StartNo && x.tel.TelephoneNo <= x.sw.EndNo)
                //.Select(x => new SwitchCodeInfo { SwitchPreNo = x.sw.SwitchPreNo, PreCodeType = x.sw.PreCodeType, PortNo = x.tel.SwitchPort.PortNo,TelephoneNo =telno}).SingleOrDefault();           
            }
        }
        public static SwitchCodeInfo GetSwitchCodeInfoWithUsingSwitchPort(long telno)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telno)
                 .Select(t => new SwitchCodeInfo { SwitchCode = t.SwitchPort.Switch.SwitchCode, PortNo = t.SwitchPort.PortNo, TelephoneNo = telno, FeatureONU = t.SwitchPort.Switch.FeatureONU }).SingleOrDefault();
            }
        }

        public static string GetDepositeNumber(int centerCode, string year)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.GetDepositeNumber(centerCode, year);
            }
        }

        public static List<T> SearchByPropertyName<T>(string propertyName, object value) where T : class
        {
            using (MainDataContext context = new MainDataContext())
            {
                BinaryExpression binaryExpression = null;
                IEnumerable<T> dataContainer = context.GetTable<T>();
                var parameter = Expression.Parameter(typeof(T), "dataContainer");
                var property = typeof(T).GetProperty(propertyName);
                var propertyType = property.PropertyType;
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                binaryExpression = GetEqualBinaryExpression(propertyAccess, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType), propertyType);
                Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
                Func<T, bool> compiled = predicate.Compile();
                return dataContainer.Where(compiled).ToList();
            }
        }

        public static Func<TSource, bool> ComparisonByByPropertyName<TSource>(string property, object value, int condition)
        {

            var type = typeof(TSource);
            var pe = Expression.Parameter(type, "p");
            var propertyReference = Expression.Property(pe, property);
            var constantReference = Expression.Constant(value);

            switch (condition)
            {
                case (int)DB.Condition.Equal:
                    return Expression.Lambda<Func<TSource, bool>>(Expression.Equal(propertyReference, constantReference), new[] { pe }).Compile();

                case (int)DB.Condition.GreaterThan:

                    return Expression.Lambda<Func<TSource, bool>>(Expression.GreaterThan(propertyReference, constantReference), new[] { pe }).Compile();

                case (int)DB.Condition.GreaterThanOrEqual:

                    return Expression.Lambda<Func<TSource, bool>>(Expression.GreaterThanOrEqual(propertyReference, constantReference), new[] { pe }).Compile();

                case (int)DB.Condition.LessThan:
                    return Expression.Lambda<Func<TSource, bool>>(Expression.LessThan(propertyReference, constantReference), new[] { pe }).Compile();

                case (int)DB.Condition.LessThanOrEqual:
                    return Expression.Lambda<Func<TSource, bool>>(Expression.LessThanOrEqual(propertyReference, constantReference), new[] { pe }).Compile();
            }

            return null;


        }

        public static T ChangeType<T>(object value)
        {

            Type conversionType = typeof(T);

            if (conversionType.IsGenericType &&

                conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {

                if (value == null) { return default(T); }

                conversionType = Nullable.GetUnderlyingType(conversionType); ;

            }

            return (T)Convert.ChangeType(value, conversionType);

        }

        //Milad doran
        //public static List<CheckableItem> GetConnectionColumnInfo(int mDFID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.VerticalMDFColumns.Where(t => t.MDFFrame.MDF.ID == mDFID)
        //                .OrderBy(t => t.VerticalCloumnNo)
        //                .Select(t => new CheckableItem
        //                {
        //                    ID = t.ID,
        //                    Name = t.VerticalCloumnNo.ToString() + " فریم: " + t.MDFFrame.FrameNo.ToString(),
        //                    IsChecked = false,
        //                }).ToList();
        //    }
        //}

        public static List<CheckableItem> GetConnectionColumnInfo(int mDFID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                var query = context.VerticalMDFColumns
                                   .Where(t => t.MDFFrame.MDF.ID == mDFID)
                                   .OrderBy(t => t.MDFFrame.FrameNo)
                                   .ThenBy(t => t.VerticalCloumnNo)
                                   .Select(t => new CheckableItem
                                                {
                                                    ID = t.ID,
                                                    Name = string.Format("فریم : {0} -- ردیف : {1}", t.MDFFrame.FrameNo.ToString(), t.VerticalCloumnNo.ToString()),
                                                    IsChecked = false,
                                                }
                                           )
                                   .AsQueryable();
                result = query.ToList();
                return result;
            }
        }

        public static List<CheckableItem> GetConnectionColumnInfoByMdfId(int mDFID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();
                var query = context.VerticalMDFColumns
                                   .Where(t => t.MDFFrame.MDF.ID == mDFID)
                                   .OrderBy(t => t.MDFFrame.FrameNo)
                                   .ThenBy(t => t.VerticalCloumnNo)
                                   .Select(t => new CheckableItem
                                                {
                                                    ID = t.ID,
                                                    Name = string.Format("فریم : {0} -- ردیف : {1}", t.MDFFrame.FrameNo.ToString(), t.VerticalCloumnNo.ToString()),
                                                    IsChecked = false,
                                                }
                                           )
                                   .AsQueryable();
                result = query.ToList();
                return result;
            }
        }

        public static ConnectionInfo GetConnectionInfoByBuchtID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == buchtID).AsEnumerable().Select(t => new ConnectionInfo
                {
                    CenterID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
                    MDFID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID,
                    MDF = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + DB.GetDescription(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description),
                    BuchtID = t.ID,
                    BuchtNo = t.BuchtNo,
                    BuchtStatus = t.Status,
                    VerticalColumnID = t.VerticalMDFRow.VerticalMDFColumn.ID,
                    VerticalColumnNo = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                    VerticalRowID = t.VerticalMDFRow.ID,
                    VerticalRowNo = t.VerticalMDFRow.VerticalRowNo,
                    BuchtTypeID = t.BuchtTypeID,
                }).SingleOrDefault();
            }
        }

        static BinaryExpression GetEqualBinaryExpression(MemberExpression propertyAccess, object columnValue, Type propertyType)
        {
            return Expression.Equal(propertyAccess, Expression.Constant(columnValue, propertyType));
            //GetLowerCasePropertyAccess(propertyAccess)
        }

        static MethodCallExpression GetLowerCasePropertyAccess(MemberExpression propertyAccess)
        {
            return Expression.Call(Expression.Call(propertyAccess, "ToString", new Type[0]), typeof(string).GetMethod("ToLower", new Type[0]));
        }

        public static Expression<Func<T, bool>> MakeFilter<T>(string propertyName, object value)
        {
            using (MainDataContext context = new MainDataContext())
            {
                PropertyInfo property = typeof(T).GetProperty(propertyName);
                ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
                MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, property);
                ConstantExpression constantValue = Expression.Constant(value);
                BinaryExpression equality = Expression.Equal(propertyAccess, constantValue);
                return Expression.Lambda<Func<T, bool>>(equality, parameter);
            }
        }

        public static IEnumerable<T> GetEntitiesbyID<T>(long Id) where T : class
        {

            using (MainDataContext context = new MainDataContext())
            {

                ITable tbl = context.GetTable(typeof(T));

                ParameterExpression pe = Expression.Parameter(typeof(T), "p"); // 1. create parameter P

                MetaDataMember id = context.Mapping.GetTable(typeof(T)).RowType.IdentityMembers[0];

                MemberExpression prop = Expression.Property(pe, id.Member.Name);  //2.Create property p.ID
                //MemberExpression prop = Expression.Property(pe, "ID");  //2.Create property p.ID

                /******************************************
                   var table = context.GetTable<T>();
                // get the metamodel mappings (database to domain objects)
                 MetaModel modelMapping = table.Context.Mapping;

                // get the data members for this type                    
                ReadOnlyCollection<MetaDataMember> dataMembers = modelMapping.GetMetaType(typeof(T)).DataMembers;

                // find the primary key and return its type
               Type Pktype= (dataMembers.Single<MetaDataMember>(m => m.IsPrimaryKey)).Type;
               string PkName = (dataMembers.Single<MetaDataMember>(m => m.IsPrimaryKey)).Name;  
                
                 ********************************************/


                ConstantExpression ce = Expression.Constant(Id); // 3. ==1

                BinaryExpression be = Expression.Equal(prop, ce); //4. p.ID==1
                //LambdaExpression keyselector = Expression.Lambda(be, pe); //5.Create lamba expression p =>(p.ID==1)

                return ((tbl) as IQueryable<T>).Where(Expression.Lambda<Func<T, bool>>(be, new ParameterExpression[] { pe }).Compile()).ToList();
                // return ((tbl) as IQueryable<T>).Where<T>(Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(pe, "ID"), Expression.Constant(Id)), pe)).ToList();


            }

        }

        public static T GetEntitybyID<T>(long Id) where T : class
        {

            using (MainDataContext context = new MainDataContext())
            {

                ITable tbl = context.GetTable(typeof(T));

                ParameterExpression pe = Expression.Parameter(typeof(T), "p"); // 1. create parameter P

                MetaDataMember id = context.Mapping.GetTable(typeof(T)).RowType.IdentityMembers[0];

                MemberExpression prop = Expression.Property(pe, id.Member.Name);  //2.Create property p.ID

                ConstantExpression ce = Expression.Constant(Id); // 3. ==1

                BinaryExpression be = Expression.Equal(prop, ce); //4. p.ID==1

                return ((tbl) as IQueryable<T>).Where(Expression.Lambda<Func<T, bool>>(be, new ParameterExpression[] { pe }).Compile()).SingleOrDefault();

            }

        }

        public static T GetEntitybyIntID<T>(long Id) where T : class
        {

            using (MainDataContext context = new MainDataContext())
            {

                ITable tbl = context.GetTable(typeof(T));

                ParameterExpression pe = Expression.Parameter(typeof(T), "p"); // 1. create parameter P

                MetaDataMember id = context.Mapping.GetTable(typeof(T)).RowType.IdentityMembers[0];

                MemberExpression prop = Expression.Property(pe, id.Member.Name);  //2.Create property p.ID

                ConstantExpression ce = Expression.Constant(Id); // 3. ==1

                BinaryExpression be = Expression.Equal(prop, ce); //4. p.ID==1

                return ((tbl) as IQueryable<T>).Where(Expression.Lambda<Func<T, bool>>(be, new ParameterExpression[] { pe }).Compile()).SingleOrDefault();

            }

        }

        public static IEnumerable<T> GetAllEntity<T>() where T : class
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.GetTable<T>().ToList<T>();
            }
        }

        public static TelephoneInfoForRequest GetTelephoneInfoForRequest(long? telephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones
                    .Where(t => t.TelephoneNo == telephoneNo)
                    .Select(t => new TelephoneInfoForRequest
                    {
                        TelephoneNo = t.TelephoneNo,
                        NationalCodeOrRecordNo = t.Customer.NationalCodeOrRecordNo,
                        CustomerName = t.Customer.FirstNameOrTitle + " " + t.Customer.LastName,
                        Center = t.Center.CenterName,
                        PostalCode = t.Address.PostalCode,
                        Address = t.Address.AddressContent,
                        CustomerTypeName = t.CustomerTypeID.HasValue ? t.CustomerType.Title : string.Empty,
                        CustomerGroupName = t.CustomerGroupID.HasValue ? t.CustomerGroup.Title : string.Empty,
                        CustomerTelephone = t.Customer.UrgentTelNo

                    }).SingleOrDefault();
            }
        }

        #endregion Select

        #region Delete Public Methods

        public static void Delete<T>(object primaryKey) where T : class
        {
            using (MainDataContext context = new MainDataContext())
            {
                Logger.WriteInfo(context.GetTable<T>().GetType().Name);

                BinaryExpression binaryExpression = null;
                IEnumerable<T> dataContainer = context.GetTable<T>();
                var parameter = Expression.Parameter(typeof(T), "dataContainer");
                MetaDataMember id = context.Mapping.GetTable(typeof(T)).RowType.IdentityMembers[0];
                var property = typeof(T).GetProperty(id.Member.Name);
                var propertyType = property.PropertyType;
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                binaryExpression = GetEqualBinaryExpression(propertyAccess, primaryKey, propertyType);
                Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
                Func<T, bool> compiled = predicate.Compile();
                dataContainer.Where(compiled).ToList();
                T entity = context.GetTable<T>().Single<T>(compiled);
                context.GetTable<T>().DeleteOnSubmit(entity);
                context.SubmitChanges();

                //ParameterExpression parameter = Expression.Parameter(typeof(T), "item");
                //T entity = context.GetTable<T>().Single<T>(Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(parameter, id.Member.Name), Expression.Constant(primaryKey, typeof(T).GetProperty(id.Member.Name).PropertyType))));
                //context.GetTable<T>().DeleteOnSubmit(entity);
                //context.SubmitChanges();
            }
        }

        public static void DeleteAll<T>(List<long> primaryKeys) where T : class
        {
            if (primaryKeys.Count == 0) return;

            using (MainDataContext context = new MainDataContext())
            {
                MetaTable table = context.Mapping.MappingSource.GetModel(typeof(T)).GetMetaType(typeof(T)).Table;

                MetaDataMember id = context.Mapping.GetTable(typeof(T)).RowType.IdentityMembers[0];
                string deleteCommand = string.Format("DELETE FROM {0} WHERE {1} IN ({2})", table.TableName, id.Name, string.Join(",", primaryKeys));
                int rowsAffected = context.ExecuteCommand(deleteCommand);
                context.SubmitChanges();
            }
        }

        #endregion

        #region Save Public Mehtod

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
            DateTime currentDateTime = DB.GetServerDate();
            int? customerTypeID = null;
            int? customerGroupID = null;
            string selectTable = string.Empty;
            using (MainDataContext context = new MainDataContext())
            {
                MetaDataMember primaryKey = context.Mapping.GetTable(instance.GetType()).RowType.IdentityMembers[0];
                MetaDataMember insertDateField = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "InsertDate").SingleOrDefault();
                MetaDataMember modifyDate = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "ModifyDate").SingleOrDefault();
                MetaDataMember creatorUesrID = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "CreatorUserID").SingleOrDefault();
                MetaDataMember modifyUserID = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "ModifyUserID").SingleOrDefault();

                // مقادیر جداول را قبل از تغییر لاگ می کند
                try
                {

                    if (context.Mapping.GetTable(instance.GetType()).TableName != "dbo.ActionLog")
                    {
                        object IdentityValue;
                        if (context.Mapping.GetTable(instance.GetType()).TableName == "dbo.[Setting]")
                        {
                            IdentityValue = context.Settings.Where(t => t.Key == primaryKey.MemberAccessor.GetBoxedValue(instance)).Select(t => t.Value).SingleOrDefault().ToString();
                        }
                        else
                        {
                            IdentityValue = context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == primaryKey.Name).SingleOrDefault().MemberAccessor.GetBoxedValue(instance).ToString();

                        }

                        if ((string)IdentityValue != "0")
                        {
                            selectTable = string.Format("select * from  {0}  where   {1}  =  {2}  FOR XML PATH('{0}') , TYPE", context.Mapping.GetTable(instance.GetType()).TableName, primaryKey.Name, IdentityValue.ToString());
                            context.ExecuteCommand(@"BEGIN TRY
                                                     IF OBJECT_ID('tempdb..#val') IS NOT NULL DROP TABLE #val
                                                     CREATE TABLE #temp ( c xml, )
                                                     INSERT #temp EXEC sp_executesql {5}
                                                     INSERT INTO [dbo].[Log] VALUES({0} , dbo.serverdate() ,{3} ,{1} ,{2} ,{4} , (select top 1 * from #temp))
                                                     END TRY BEGIN CATCH EXEC uspLogError;
                                                     END CATCH", context.Mapping.GetTable(instance.GetType()).TableName, primaryKey.Name, IdentityValue.ToString(), DB.currentUser.ID, string.Empty, selectTable);
                        }

                        // اطلاعات جدول تلفن را دریافت می شود تا بعد از تغییر بررسی شود کدام فیلد ها تغییر می کند( برای وب سرویس آبونمان)
                        if (context.Mapping.GetTable(instance.GetType()).TableName == "dbo.Telephone")
                        {


                            //oldTelephone = context.Telephones.Where(tel => tel.TelephoneNo == (long)primaryKey.MemberAccessor.GetBoxedValue(instance)).SingleOrDefault();
                            //oldTelephone.Detach();
                            customerTypeID = context.ExecuteQuery<int?>("select CustomerTypeID from dbo.Telephone where TelephoneNo = {0}", primaryKey.MemberAccessor.GetBoxedValue(instance)).Take(1).SingleOrDefault();
                            customerGroupID = context.ExecuteQuery<int?>("select CustomerGroupID from dbo.Telephone where TelephoneNo = {0}", primaryKey.MemberAccessor.GetBoxedValue(instance)).Take(1).SingleOrDefault();

                            // برای جدول تلفن در حال حاضر آبونمان نیاز به اطلاعات تغییر مشترک و نوع مشترک دارد
                            // SqlConnection sqlConnection1 = new SqlConnection(context.Connection.ConnectionString);
                            //SqlCommand cmd = new SqlCommand(string.Format("select * from {0} where [{1}] = {2}", context.Mapping.GetTable(instance.GetType()).TableName, primaryKey.Name, primaryKey.MemberAccessor.GetBoxedValue(instance)), sqlConnection1);
                            //SqlDataReader reader;
                            //if (sqlConnection1.State != ConnectionState.Open)
                            //    sqlConnection1.Open();
                            //reader = cmd.ExecuteReader();
                            //telephonedt.Clear();
                            //telephonedt.Load(reader);
                            //telephonedt.TableName = context.Mapping.GetTable(instance.GetType()).TableName;
                        }

                    }
                }
                catch
                {
                }
                //TODO:rad
                //در این قسمت کدی از گذشته به صورت کامنت شده وجود داشت که آن را برداشتم تاخواندن کد راحت تر شود
                //قسمت برداشته شده در فایل زیر در داکیومنت ها موجود میباشد
                //CommentedCodeInDBSaveMethod.txt
                // old log 

                object obj = instance;

                if (modifyDate != null)
                    modifyDate.MemberAccessor.SetBoxedValue(ref obj, currentDateTime);

                if (modifyUserID != null)
                    modifyUserID.MemberAccessor.SetBoxedValue(ref obj, DB.CurrentUser.ID);

                if (isNew || (primaryKey.MemberAccessor.GetBoxedValue(instance) == null || (IsValidType(primaryKey.MemberAccessor.GetBoxedValue(instance), typeof(Int64))) && Convert.ToInt64(primaryKey.MemberAccessor.GetBoxedValue(instance)) == 0))
                {
                    if (creatorUesrID != null)
                        creatorUesrID.MemberAccessor.SetBoxedValue(ref obj, DB.CurrentUser.ID);

                    if (insertDateField != null)
                        insertDateField.MemberAccessor.SetBoxedValue(ref obj, currentDateTime);

                    context.GetTable(instance.GetType()).InsertOnSubmit(instance);
                }
                else
                {
                    context.GetTable(instance.GetType()).Attach(instance);
                    context.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, instance);
                }
                context.SubmitChanges();

                // لاگ تغییرات علاوه بر جدول لاگ برای آبونمان در جدول لاگ درخواست ها نیز ذخیره میشود
                try
                {
                    if (context.Mapping.GetTable(instance.GetType()).TableName == "dbo.Customer")
                    {
                        //string var = context.ExecuteQuery<string>(selectTable).Take(1).SingleOrDefault();

                        //RequestLog requestLog = new RequestLog();
                        //requestLog.UserID = DB.CurrentUser.ID;
                        //requestLog.Date = currentDateTime;
                        //requestLog.RequestTypeID = (int)DB.RequestType.EditCustomer;
                        //requestLog.Description = XElement.Parse(var);
                        //context.RequestLogs.InsertOnSubmit(requestLog);
                        //context.SubmitChanges();
                        TransactionOptions transactionOptions = new TransactionOptions();
                        transactionOptions.IsolationLevel = System.Transactions.IsolationLevel.Serializable;
                        using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                        {
                            string var = context.ExecuteQuery<string>(selectTable).Take(1).SingleOrDefault();

                            RequestLog requestLog = new RequestLog();
                            requestLog.UserID = DB.CurrentUser.ID;
                            requestLog.Date = currentDateTime;
                            requestLog.RequestTypeID = (int)DB.RequestType.EditCustomer;
                            requestLog.Description = XElement.Parse(var);
                            context.RequestLogs.InsertOnSubmit(requestLog);
                            context.SubmitChanges();
                            scope.Complete();
                        }
                    }

                    if (context.Mapping.GetTable(instance.GetType()).TableName == "dbo.Address")
                    {
                        string var = context.ExecuteQuery<string>(selectTable).Take(1).SingleOrDefault();

                        RequestLog requestLog = new RequestLog();
                        requestLog.UserID = DB.CurrentUser.ID;
                        requestLog.Date = currentDateTime;
                        requestLog.RequestTypeID = (int)DB.RequestType.EditAddress;
                        requestLog.Description = XElement.Parse(var);
                        context.RequestLogs.InsertOnSubmit(requestLog);
                        context.SubmitChanges();
                    }

                    if (context.Mapping.GetTable(instance.GetType()).TableName == "dbo.Telephone")
                    {
                        Telephone newTelephone = (Telephone)instance;
                        if (newTelephone.CustomerTypeID != customerTypeID || newTelephone.CustomerGroupID != customerGroupID)
                        {
                            string var = context.ExecuteQuery<string>(selectTable).Take(1).SingleOrDefault();

                            RequestLog requestLog = new RequestLog();
                            requestLog.UserID = DB.CurrentUser.ID;
                            requestLog.Date = currentDateTime;
                            requestLog.RequestTypeID = (int)DB.RequestType.EditTelephone;
                            requestLog.TelephoneNo = newTelephone.TelephoneNo;
                            requestLog.Description = XElement.Parse(var);
                            context.RequestLogs.InsertOnSubmit(requestLog);
                            context.SubmitChanges();
                        }
                        //foreach(MetaDataMember t in context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "CustomerTypeID" || t.MappedName == "CustomerGroupID"))
                        // {
                        //     if ((int)oldTelephone.CustomerTypeID != (int)context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t2 => t2.MappedName == t.MappedName).SingleOrDefault().MemberAccessor.GetBoxedValue(instance))
                        //     {
                        //         string var = context.ExecuteQuery<string>(selectTable).Take(1).SingleOrDefault();

                        //         RequestLog requestLog = new RequestLog();
                        //         requestLog.Date = currentDateTime;
                        //         requestLog.RequestTypeID = (int)DB.RequestType.EditTelephone;
                        //         requestLog.TelephoneNo = (long)context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t2 => t2.MappedName == "TelephoneNo").SingleOrDefault().MemberAccessor.GetBoxedValue(instance);
                        //         requestLog.Description = XElement.Parse(var);
                        //         context.RequestLogs.InsertOnSubmit(requestLog);
                        //         context.SubmitChanges();
                        //         break;
                        //     }
                        // };
                    }

                }
                catch
                { }


                try
                {
                    Logger.WriteInfo(context.Mapping.GetTable(instance.GetType()).TableName + " ID = " + context.Mapping.GetTable(instance.GetType()).RowType.DataMembers.Where(t => t.MappedName == "ID").SingleOrDefault().MemberAccessor.GetBoxedValue(instance));
                }
                catch
                {
                }
            }
        }

        //TODO:rad 13950517 
        //متد زیر منحصراً برای ذخیره لاگ ویرایش مشخصات آدرس اضافه شد تا فیلد های مرکز و تلفن به این لاگ افزوده شود
        public static void SaveEditAddress(Address address, Telephone telephone)
        {
            //Must insert RequestLog for requestLog.RequestTypeID = (int)DB.RequestType.EditAddress;
            throw new NotImplementedException("در حال پیاده سازی");
        }

        #endregion

        public static object ToType<T>(this object obj) where T : class
        {
            //create instance of T type object:
            var tmp = Activator.CreateInstance(typeof(T));

            //loop through the properties of the object you want to covert:          
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                try
                {
                    //get the value of property and try 
                    //to assign it to the property of T type object:
                    tmp.GetType().GetProperty(pi.Name).SetValue(tmp,
                                              pi.GetValue(obj, null), null);
                }
                catch { }
            }

            //return the T type object:         
            return tmp;
        }

        public static object ToNonAnonymousList<T>(this List<T> list, Type t)
        {
            //define system Type representing List of objects of T type:
            Type genericType = typeof(List<>).MakeGenericType(t);

            //create an object instance of defined type:
            object l = Activator.CreateInstance(genericType);

            //get method Add from from the list:
            MethodInfo addMethod = l.GetType().GetMethod("Add");

            //loop through the calling list:
            foreach (T item in list)
            {
                //convert each object of the list into T object by calling extension ToType<T>()
                //Add this object to newly created list:
                //  addMethod.Invoke(l, new[] {item.ToType(t)});
            }
            //return List of T objects:
            return l;
        }

        public static void SaveAll<T>(List<T> instance) where T : class
        {
            //TODO : Add Update
            using (MainDataContext context = new MainDataContext())
            {
                Logger.WriteInfo(context.GetTable<T>().GetType().Name);
                context.GetTable(typeof(T)).InsertAllOnSubmit(instance);
                context.SubmitChanges();
            }
        }

        public static void InitializeUserInfo(string username)
        {
            Folder.User user = UserDB.GetUserByUserName(username);
            InitializeUserInfo(user);
        }

        public static void InitializeUserInfo(Folder.User folderUser)
        {
            CurrentUser = new UserInfo();

            using (MainDataContext context = new MainDataContext())
            {
                User user = context.Users.Where(t => t.UserName.ToLower().Trim() == folderUser.Username.ToLower().Trim() && t.IsDelete == false).SingleOrDefault();

                if (user != null)
                {
                    Role role = context.Roles.Where(t => t.ID == user.RoleID).SingleOrDefault();
                    CurrentUser.ID = user.ID;
                    CurrentUser.FullName = user.FirstName + " " + user.LastName;
                    CurrentUser.UserName = user.UserName;
                    CurrentUser.RoleID = user.RoleID;

                    CurrentUser.DataGridColumnConfig = DataGridColumnConfigDB.GetDataGridColumnConfig(user.ID);

                    if (user.Config != null)
                        CurrentUser.UserConfig = LogSchemaUtility.Deserialize<CRM.Data.Schema.UserConfig>(user.Config.ToString());

                    bool isSystem;

                    if (DB.IsInWebSiteMode)
                        isSystem = SecurityDB.GetUserRolesByFolderUser(folderUser).Where(t => t.RoleID.ToString() == Folder.Role.SystemRole).Any() || CurrentUser.UserName.ToLower() == "pendar";
                    else
                        isSystem = folderUser.UserRoles.ToList().Where(t => t.RoleID.ToString() == Folder.Role.SystemRole).Any() || CurrentUser.UserName.ToLower() == "pendar";

                    //bool isSystem = folderUser.UserRoles.ToList().Where(t => t.RoleID.ToString() == Folder.Role.SystemRole).Any() || CurrentUser.UserName.ToLower() == "pendar";

                    CurrentUser.CenterIDs = (isSystem) ? context.Centers.Select(t => t.ID).ToList() : user.UserCenters.Select(t => t.CenterID).ToList();

                    CurrentUser.RequestStepsIDs = (isSystem) ? context.RequestSteps.Select(t => t.ID).ToList() : role.RoleRequestSteps.Select(t => t.RequestStepID).ToList();
                    //CurrentUser.SubscriberTypeIDs = (isSystem) ? context.SubscriberTypes.Select(t => t.ID).ToList() : user.Role.RoleSubscriberTypes.Select(t => t.SubscriberTypeID).ToList();
                    CurrentUser.ResourceNames = (isSystem) ? context.Resources.Select(t => t.Name).ToList() : user.Role.RoleResources.Select(t => t.Resource.Name).ToList();

                    //CurrentUser.CallDetailTypeIDs = (isSystem) ? context.CallDetailTypes.Select(t => t.ID).ToList() : user.Role.RoleCallDetailTypes.Select(t => t.CallDetailTypeID).ToList();
                    CurrentUser.ReportTemplateIDs = (isSystem) ? context.ReportTemplates.Select(t => t.ID).ToList() : user.Role.RoleReportTemplates.Where(t => t.ReportTemplate.IsVisible.HasValue && t.ReportTemplate.IsVisible.Value).Select(t => t.ReportTemplateID).ToList();

                    //بازیابی تنظیمات مربوط به گزارش های گریدی
                    ReportSetting reportSetting = new ReportSetting();
                    if (System.IO.Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\WindowsSecurity"))
                    {
                        reportSetting = Helpers.GetReportDefaultSettings(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\WindowsSecurity");
                    }
                    else
                    {
                        reportSetting.SetMembersDefault();
                    }
                    CurrentUser.ReportSetting = reportSetting;

                    user.LastLoginDate = DB.GetServerDate();
                    user.Detach();
                    DB.Save(user, false);

                    Logger.WriteInfo("InitializingUserInfo ... UserName : {0} , UserFullName : {1}", user.UserName.ToString(), CurrentUser.FullName);
                }
                else
                {
                    Logger.WriteInfo("User is null");
                    throw new Exception("نام کاربری اشتباه است");
                }
            }
        }

        //milad doran
        //public static List<CheckableItem> GetConnectionRowInfo(int verticalMDFColumnID)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.VerticalMDFRows.Where(t => t.VerticalMDFColumn.ID == verticalMDFColumnID)
        //                .OrderBy(t => t.VerticalRowNo)
        //                .Select(t => new CheckableItem
        //                {
        //                    Name = t.VerticalRowNo.ToString(),
        //                    ID = t.ID,
        //                    IsChecked = false
        //                }).ToList();
        //    }
        //}

        //TODO:rad 13941215
        public static List<CheckableItem> GetConnectionRowInfo(int verticalMDFColumnID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.VerticalMDFRows
                                   .Where(t => t.VerticalMDFColumn.ID == verticalMDFColumnID)
                                   .OrderBy(t => t.VerticalRowNo)
                                   .Select(t => new CheckableItem
                                                {
                                                    Name = t.VerticalRowNo.ToString(),
                                                    ID = t.ID,
                                                    IsChecked = false
                                                }
                                           )
                                   .AsQueryable();

                result = query.ToList();
                return result;
            }
        }

        //TODO:rad 13941215
        public static List<CheckableItem> GetConnectionRowInfoByVerticalMdfColunmnId(int verticalMDFColumnID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.VerticalMDFRows
                                   .Where(t => t.VerticalMDFColumn.ID == verticalMDFColumnID)
                                   .OrderBy(t => t.VerticalRowNo)
                                   .Select(t => new CheckableItem
                                                {
                                                    Name = t.VerticalRowNo.ToString(),
                                                    ID = t.ID,
                                                    IsChecked = false
                                                }
                                           )
                                   .AsQueryable();

                result = query.ToList();
                return result;
            }
        }
        public static string GetDescription(string description)
        {
            if (description == string.Empty || description == null)
            {
                return string.Empty;
            }
            else
            {
                return " (" + description + ") ";
            }
        }

        public static List<CheckableItem> GetConnectionBuchtInfo(int verticalMDFRowID, bool connectionToCabinetInput = false, int? buchtType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                result = context.Buchts
                                .Where(t => t.MDFRowID == verticalMDFRowID && t.Status == (byte)DB.BuchtStatus.Free)
                                .Where(t => connectionToCabinetInput == false || t.CabinetInputID == null)
                                .Where(t => buchtType == null || t.BuchtTypeID == buchtType)
                                .OrderBy(t => t.BuchtNo)
                                .Select(t => new CheckableItem
                                            {
                                                Name = t.BuchtNo.ToString(),
                                                LongID = t.ID,
                                                IsChecked = false,
                                            }
                                        )
                                .ToList();
                return result;
            }
        }

        //TODO:rad 13941215
        public static List<CheckableItem> GetConnectionBuchtInfoByVerticalMdfRowId(int verticalMdfRowId, bool isConnectedToCabinetInput)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.Buchts
                                   .Where(t =>
                                                (t.MDFRowID == verticalMdfRowId && t.Status == (byte)DB.BuchtStatus.Free) &&
                                                (isConnectedToCabinetInput || t.CabinetInputID == null)
                                         )
                                   .OrderBy(t => t.BuchtNo)
                                   .Select(t => new CheckableItem
                                                {
                                                    Name = t.BuchtNo.ToString(),
                                                    LongID = t.ID,
                                                    IsChecked = false,
                                                }
                                          )
                                   .AsQueryable();

                result = query.ToList();
                return result;
            }
        }

        public static List<CheckableItem> GetConnectableConnectionBuchtInfo(int verticalMDFRowID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                   .Where(t => t.MDFRowID == verticalMDFRowID &&
                               (t.Status == (byte)DB.BuchtStatus.Free || t.Status == (byte)DB.BuchtStatus.ConnectedToPCM) &&
                               (t.BuchtTypeID != (int)DB.BuchtType.OutLine))
                   .OrderBy(t => t.BuchtNo)
                   .Select(t => new CheckableItem
                   {
                       Name = t.BuchtNo.ToString(),
                       LongID = t.ID,
                       IsChecked = false,
                   }).ToList();
            }
        }

        //milad doran
        //public static List<CheckableItem> GetBuchtsConnectedToCable(int verticalMDFRowID, int? buchtType = null)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Buchts
        //           .Where(t => t.MDFRowID == verticalMDFRowID && t.Status == (byte)DB.BuchtStatus.Free)
        //           .Where(t => buchtType == null || t.BuchtTypeID == buchtType)
        //           .OrderBy(t => t.BuchtNo)
        //           .Select(t => new CheckableItem
        //           {
        //               Name = t.BuchtNo.ToString(),
        //               LongID = t.ID,
        //               IsChecked = false,
        //           }).ToList();
        //    }
        //}

        //TODO:rad 13950115
        public static List<CheckableItem> GetBuchtsConnectedToCable(int verticalMDFRowID, int? buchtType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.Buchts
                                   .Where(t => t.MDFRowID == verticalMDFRowID && t.Status == (byte)DB.BuchtStatus.Free)
                                   .Where(t => buchtType == null || t.BuchtTypeID == buchtType)
                                   .OrderBy(t => t.BuchtNo)
                                   .Select(t => new CheckableItem
                                                {
                                                    Name = t.BuchtNo.ToString(),
                                                    LongID = t.ID,
                                                    IsChecked = false,
                                                }
                                           )
                                   .AsQueryable();

                result = query.ToList();

                return result;
            }
        }

        public static List<CheckableItem> GetBuchtsNotConnectedToCable(int verticalMDFRowID, int? buchtType = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                   .Where(t => t.MDFRowID == verticalMDFRowID)
                   .Where(t => buchtType == null || t.BuchtTypeID == buchtType)
                   .OrderBy(t => t.BuchtNo)
                   .Select(t => new CheckableItem
                   {
                       Name = t.BuchtNo.ToString(),
                       LongID = t.ID,
                       IsChecked = false,
                   }).ToList();
            }
        }

        //milad doran
        //public static List<CheckableItem> GetAllConnectionBuchtInfo(int verticalMDFRowID, bool connectionToCabinetInput = false)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        return context.Buchts
        //           .Where(t => t.MDFRowID == verticalMDFRowID)
        //           .Where(t => t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine)
        //           .Where(t => connectionToCabinetInput == false || t.CabinetInputID == null)
        //           .OrderBy(t => t.BuchtNo)
        //           .Select(t => new CheckableItem
        //           {
        //               Name = t.BuchtNo.ToString(),
        //               LongID = t.ID,
        //               IsChecked = false,
        //           }).ToList();
        //    }
        //}

        public static List<CheckableItem> GetAllConnectionBuchtInfo(int verticalMDFRowID, bool connectionToCabinetInput = false)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<CheckableItem> result = new List<CheckableItem>();

                var query = context.Buchts
                                   .Where(t => t.MDFRowID == verticalMDFRowID)
                                   .Where(t => t.BuchtTypeID != (int)DB.BuchtType.InLine && t.BuchtTypeID != (int)DB.BuchtType.OutLine)
                                   .Where(t => connectionToCabinetInput == false || t.CabinetInputID == null)
                                   .OrderBy(t => t.BuchtNo)
                                   .Select(t => new CheckableItem
                                                {
                                                    Name = t.BuchtNo.ToString(),
                                                    LongID = t.ID,
                                                    IsChecked = false,
                                                }
                                          )
                                   .AsQueryable();

                result = query.ToList();

                return result;
            }
        }

        public static List<CheckableItem> GetConnectionBuchtInfoByUsesType(int verticalMDFRowID, Byte requiredConnection)
        {
            if (requiredConnection == (byte)DB.RequiredConnection.UnConnectToCable)
            {
                using (MainDataContext context = new MainDataContext())
                {
                    return context.Buchts.Where(t => t.MDFRowID == verticalMDFRowID && t.CablePairID == null && t.PCMPortID == null)
                        .OrderBy(t => t.BuchtNo)
                         .Select(t => new CheckableItem
                         {
                             Name = t.BuchtNo.ToString(),
                             LongID = t.ID,
                             IsChecked = false,
                         }).ToList();
                }
            }
            if (requiredConnection == (byte)DB.RequiredConnection.AllExceptPCM)
            {
                using (MainDataContext context = new MainDataContext())
                {
                    return context.Buchts.Where(t => t.MDFRowID == verticalMDFRowID && t.PCMPortID == null)
                        .OrderBy(t => t.BuchtNo)
                         .Select(t => new CheckableItem
                         {
                             Name = t.BuchtNo.ToString(),
                             LongID = t.ID,
                             IsChecked = false,
                         })
                     .ToList();
                }
            }
            else
            {
                return null;
            }
        }

        public static List<ConnectionInfo> GetConnectionBuchtInfoForExchangeCentralMDF(int verticalMDFRowID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => (t.MDFRowID == verticalMDFRowID) && (t.Status == 0 || t.Status == (byte)DB.BuchtStatus.ExchangeCentralCableMDF))
                    .Select(t => new ConnectionInfo
                    {
                        BuchtNo = t.BuchtNo,
                        BuchtID = t.ID,
                    }).ToList();
            }
        }

        public static string GetColumnRowConnectionBuBuchtID(long? buchtID)
        {
            if (buchtID == null)
                return null;
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == buchtID).Select(t => t.BuchtNo.ToString() + " - " + t.VerticalMDFRow.VerticalRowNo.ToString() + " - " + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString()).SingleOrDefault().ToString();
            }
        }

        public static List<Bucht> GetSeveralBucht(long buchtID, int number)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID >= buchtID).Take(number).ToList();
            }
        }

        public static string GetConnectionByBuchtID(long? buchtID)
        {
            if (buchtID == null || buchtID == 0) return " ";
            using (MainDataContext context = new MainDataContext())
            {
                //milad doran
                //return context.Buchts.Where(t => t.ID == buchtID).AsEnumerable().Select(t => "ام دی اف:" + t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + DB.GetDescription(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description) +
                //                                                            "ردیف:" + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() +
                //                                                            "طبقه:" + t.VerticalMDFRow.VerticalRowNo.ToString() +
                //                                                            "اتصالی:" + t.BuchtNo.ToString()).SingleOrDefault().ToString();

                //TODO:rad 13941212
                string result = string.Empty;
                result = context.Buchts
                                .Where(t => t.ID == buchtID)
                                .AsEnumerable()
                                .Select(t =>
                                            "ام دی اف: " + t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + DB.GetDescription(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description) +
                                            "ردیف: " + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() +
                                            "طبقه: " + t.VerticalMDFRow.VerticalRowNo.ToString() +
                                            "اتصالی: " + t.BuchtNo.ToString()
                                        )
                                .SingleOrDefault()
                                .ToString();
                return result;
            }
        }

        public static ConnectionInfo GetBuchtInfoByID(long buchtID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts
                      .Where(t => t.ID == buchtID
                      ).AsEnumerable().Select(t => new ConnectionInfo
                                                   {
                                                       MDF = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + DB.GetDescription(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description),
                                                       MDFID = t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,

                                                       VerticalColumnNo = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                                       VerticalColumnID = t.VerticalMDFRow.VerticalMDFColumn.ID,

                                                       VerticalRowNo = t.VerticalMDFRow.VerticalRowNo,
                                                       VerticalRowID = t.VerticalMDFRow.ID,

                                                       BuchtNo = t.BuchtNo,
                                                       BuchtID = t.ID,
                                                       BuchtStatus = t.Status,
                                                   }).SingleOrDefault();
            }
        }

        public static AboneInfo GetAboneInfoByBuchtID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == buchtID).Select(t => new AboneInfo
                {
                    PostContactID = t.ConnectionID,
                    ConnectionNo = t.PostContact.ConnectionNo,
                    CabinetID = t.CabinetInput.Cabinet.ID,
                    CabinetNumber = t.CabinetInput.Cabinet.CabinetNumber,
                    CabinetInputID = t.CabinetInputID,
                    CabinetInputNumber = t.CabinetInput.InputNumber,
                    PostID = t.PostContact.Post.ID,
                    PostNumber = t.PostContact.Post.Number,

                }).SingleOrDefault();
            }
        }

        public static string GetConnectionWithPortByBuchtID(long? buchtID)
        {
            if (buchtID == null) return " ";
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == buchtID).AsEnumerable().Select(t => "ام دی اف:" + t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString()
                                                                                                          + DB.GetDescription(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description) +
                                                                                                "ردیف:" + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() +
                                                                                                "طبقه:" + t.VerticalMDFRow.VerticalRowNo.ToString() +
                                                                                                "اتصالی:" + t.BuchtNo.ToString()).SingleOrDefault().ToString();
            }
        }
        public static string GetBuchtInfoByBuchtID(long? buchtID)
        {
            if (buchtID == null) return string.Empty;
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == buchtID).Select(t => t.VerticalMDFRow.VerticalRowNo.ToString() + " - " + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() + " - " + t.BuchtNo.ToString()).SingleOrDefault().ToString();
            }
        }

        public static List<BuchtNoInfo> GetBuchtInfoByBuchtIDInSeperation(long? buchtID)
        {
            if (buchtID == null) return null;
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == buchtID).Select(t => new BuchtNoInfo
                {
                    Tabaghe = t.VerticalMDFRow.VerticalRowNo,
                    Radif = t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                    CabinetInputID = t.CabinetInputID
                }).ToList();


            }
        }

        public static int GetCabinetTypeByCabinetInputID(int cabdinetInputID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Cabinets.Where(c => c.CabinetInputs.Where(ci => ci.ID == cabdinetInputID).SingleOrDefault().CabinetID == c.ID).SingleOrDefault().CabinetTypeID;
            }
        }

        public static List<AssignmentInfo> GetAllInformation()
        {
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.PostContacts
                    .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })
                    .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts })
                    .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID))
                     .Select(t => new AssignmentInfo
                     {
                         BuchtType = t.Bucht.Buchts.BuchtTypeID,
                         BuchtStatus = t.Bucht.Buchts.Status,
                         MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " " + "شلف:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " " + "کارت:" + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " " + "پورت:" + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),
                         PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                         InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                         InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                         PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                         PostContactID = t.Bucht.Buchts.PostContact.ID,
                         PostContactStatus = t.Bucht.Buchts.PostContact.Status,
                         Connection = "ردیف:" + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t.Bucht.Buchts.BuchtNo,
                         BuchtID = t.Bucht.Buchts.ID,
                         PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                         SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                         SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                         SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                         TelePhoneNo = t.Telephone.TelephoneNo,
                         PostID = t.Bucht.Buchts.PostContact.Post.ID,
                         CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                         PostName = t.Bucht.Buchts.PostContact.Post.Number,
                         CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                         CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
                         CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                         TelephoneStatus = t.Telephone.Status,
                         PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                         MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                         IsADSL = t.Bucht.Buchts.ADSLStatus

                     }).OrderBy(t => t.PostContactID).ToList();
                return query;

            }
        }

        public static List<AssignmentInfo> GetAllInformationByPostIDAndWithOutpostContactType(int postID, byte postContactType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<AssignmentInfo> assignmentInfo = context.PostContacts

                                                                   .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
                                                                   .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })

                                                                   // join with the Otherbucht
                                                                   .GroupJoin(context.Buchts, fb => fb.Buchts.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new { PostContacts = fb.PostContacts, Buchts = fb.Buchts, OtherBucht = sb })
                                                                   .SelectMany(ob => ob.OtherBucht.DefaultIfEmpty(), (fb, ob) => new { PostContacts = fb.PostContacts, Buchts = fb.Buchts, OtherBucht = ob })

                                                                   // join with the telephone
                                                                   .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts })
                                                                   .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })

                                                                   // join with the Otherbucht telephone
                                                                   .GroupJoin(context.Telephones, b => b.Bucht.OtherBucht.SwitchPortID, t => t.SwitchPortID, (b, t) => new
                                                                   {
                                                                       Buchts = b.Bucht,
                                                                       OtherTelephones = t,
                                                                       PostContacts = b.PostContact,
                                                                       Telephone = b.Telephone
                                                                   })
                                                                   .SelectMany(x => x.OtherTelephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, OtherTelephone = t2, Telephone = t1.Telephone })

                                                                   .Where(t =>
                                                                               t.Bucht.Buchts.PostContact.Post.ID == postID &&
                                                                               t.Bucht.Buchts.PostContact.ConnectionType != postContactType &&
                                                                               t.Bucht.Buchts.PostContact.Status != (byte)DB.PostContactStatus.Deleted &&
                                                                               t.Bucht.Buchts.PostContact.Post.IsDelete == false
                                                                          )
                                                                    .OrderBy(t => t.Bucht.Buchts.PostContact.ConnectionNo)
                                                                    .ThenBy(t => t.Bucht.Buchts.PCMPort.PortNumber)

                                                                    .Select(t => new AssignmentInfo
                                                                                {
                                                                                    BuchtType = t.Bucht.Buchts.BuchtTypeID,
                                                                                    BuchtTypeName = t.Bucht.Buchts.BuchtType.BuchtTypeName,
                                                                                    BuchtStatus = t.Bucht.Buchts.Status,

                                                                                    MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " " +
                                                                                           "شلف:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " " +
                                                                                           "کارت:" + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " " +
                                                                                           "پورت:" + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),

                                                                                    PCMStatus = t.Bucht.Buchts.PCMPort.PCM.Status,
                                                                                    PCMPortStatus = t.Bucht.Buchts.PCMPort.Status,
                                                                                    PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                                                                                    InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                                                                                    InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                                                                                    PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                                                                                    PostContactID = t.Bucht.Buchts.PostContact.ID,
                                                                                    PostContactStatus = t.Bucht.Buchts.PostContact.Status,
                                                                                    PostContactStatusName = t.Bucht.Buchts.PostContact.PostContactStatus.Name,
                                                                                    Connection = "ردیف:" + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " +
                                                                                                 "طبقه:" + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " " +
                                                                                                 "اتصالی:" + t.Bucht.Buchts.BuchtNo,

                                                                                    BuchtID = t.Bucht.Buchts.ID,
                                                                                    PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                                                                                    SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                                                                                    SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                                                                                    SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                                                                                    TelePhoneNo = t.Telephone != null ? t.Telephone.TelephoneNo : t.OtherTelephone.TelephoneNo,
                                                                                    PostID = t.Bucht.Buchts.PostContact.Post.ID,
                                                                                    CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                                                                                    PostName = t.Bucht.Buchts.PostContact.Post.Number,
                                                                                    CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                                                                                    CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,

                                                                                    CustomerName = t.Telephone != null ?
                                                                                                   (t.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.Telephone.Customer.LastName ?? string.Empty) :
                                                                                                   (t.OtherTelephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.OtherTelephone.Customer.LastName ?? string.Empty),

                                                                                    TelephoneStatus = t.Telephone.Status,
                                                                                    PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                                                                                    MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                                                                                    OtherBuchtID = t.Bucht.OtherBucht.ID,
                                                                                    OtherBuchtTypeName = t.Bucht.OtherBucht.BuchtType.BuchtTypeName,
                                                                                    RequestID = context.ViewReservBuchts
                                                                                                       .Where(t3 => t3.PostContactID == t.Bucht.Buchts.PostContact.ID)
                                                                                                       .Take(1)
                                                                                                       .Select(t3 => t3.RequestID)
                                                                                                       .SingleOrDefault(),
                                                                                    IsADSL = (t.Telephone != null ? context.ADSLPAPPorts.Any(t2 => t2.TelephoneNo == t.Telephone.TelephoneNo) : false)
                                                                                }
                                                                    );
                //*************************************************************************************************************************************************************************************************************************************
                return assignmentInfo.ToList();
            }
        }

        public static List<AssignmentInfo> GetAllInformationInGroupPostByPostIDAndWithOutpostContactType(int postID, byte postContactType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<int> groupPost = context.Posts.Where(t => t.PostGroupID == context.Posts.Where(t2 => t2.ID == postID).Select(t2 => t2.PostGroupID).SingleOrDefault() && t.ID != postID).Select(t => t.ID).ToList();
                groupPost.Add(postID);

                return context.PostContacts
                    .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })

                    .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts })
                    .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })
                     .Where(t => groupPost.Contains(t.Bucht.Buchts.PostContact.Post.ID) &&
                                 t.Bucht.Buchts.PostContact.ConnectionType != postContactType &&
                                 t.Bucht.Buchts.PostContact.Status != (byte)DB.PostContactStatus.Deleted &&
                                 t.Bucht.Buchts.PostContact.Post.IsDelete == false)
                    .OrderBy(t => t.Bucht.Buchts.PostContact.Post.Number)
                     .Select(t => new AssignmentInfo
                     {
                         BuchtType = t.Bucht.Buchts.BuchtTypeID,
                         BuchtTypeName = t.Bucht.Buchts.BuchtType.BuchtTypeName,
                         BuchtStatus = t.Bucht.Buchts.Status,
                         MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " " + "شلف:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " " + "کارت:" + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " " + "پورت:" + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),
                         PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                         InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                         InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                         PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                         PostContactID = t.Bucht.Buchts.PostContact.ID,
                         PostContactStatus = t.Bucht.Buchts.PostContact.Status,
                         PostContactStatusName = t.Bucht.Buchts.PostContact.PostContactStatus.Name,
                         Connection = "ردیف:" + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t.Bucht.Buchts.BuchtNo,
                         BuchtID = t.Bucht.Buchts.ID,
                         PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                         SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                         SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                         SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                         TelePhoneNo = t.Telephone.TelephoneNo,
                         PostID = t.Bucht.Buchts.PostContact.Post.ID,
                         CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                         PostName = t.Bucht.Buchts.PostContact.Post.Number,
                         CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                         CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
                         CustomerName = (t.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.Telephone.Customer.LastName ?? string.Empty),
                         TelephoneStatus = t.Telephone.Status,
                         PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                         MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,


                     }).ToList();

            }
        }


        //public static List<AssignmentInfo> GetAllInformationBucht(
        //       List<int> cites,
        //       List<int> center,
        //       List<int> MDFs,
        //       int column,
        //       int row,
        //       List<int> cabinets,
        //       int cabinetInput,
        //       List<int> posts,
        //       int postContact,
        //       string customerName,
        //       long fromTelephone
        //       , long toTelephone
        //       , int startRowIndex
        //       , int pageSize
        //       , int buchtNo
        //       , List<int> buchtTypes
        //       , List<int> otherBuchtTypes
        //       , List<int> telephoneStatus
        //       , List<int> preCodeType
        //       , List<int> usageType
        //       , bool? IsADSL
        //       , List<int> AorBType
        //       , List<int> switchPreCode
        //       , bool? inRequest
        //       , bool? onlyHeadNumber
        //       )
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {

        //        context.CommandTimeout = 0;
        //        IQueryable<AssignmentInfo> query = context.Buchts

        //            //join with telephon
        //            .GroupJoin(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Bucht = b, Telephone = t })
        //            .SelectMany(t1 => t1.Telephone.DefaultIfEmpty(), (bt, t1) => new { bucht = bt.Bucht, BuchtTelephone = t1 })

        //            // join with PostContect
        //            .GroupJoin(context.PostContacts, b1 => b1.bucht.ConnectionID, pc => pc.ID, (bu, pc) => new { bucht = bu, PostContect = pc })
        //            .SelectMany(t2 => t2.PostContect.DefaultIfEmpty(), (BuchtPostcontect, t2) => new { BuchtPostContect = BuchtPostcontect.bucht, PostContect = t2 })

        //            .GroupJoin(context.CabinetInputs, b2 => b2.BuchtPostContect.bucht.CabinetInputID, ci => ci.ID, (BuchtPostContect, ci) => new { BuchtPostContect = BuchtPostContect, CabinetInput = ci })
        //            .SelectMany(t3 => t3.CabinetInput.DefaultIfEmpty(), (BuchtPostContect, t3) => new { BuchtPostContect = BuchtPostContect.BuchtPostContect, CabinetInput = t3 })

        //            // join with the Otherbucht
        //            .GroupJoin(context.Buchts, fb => fb.BuchtPostContect.BuchtPostContect.bucht.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = sb })
        //            .SelectMany(ob => ob.OtherBucht.DefaultIfEmpty(), (fb, ob) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = ob })

        //            // join with the Otherbucht telephone
        //            .GroupJoin(context.Telephones, b => b.OtherBucht.SwitchPortID, t => t.SwitchPortID, (b, t) => new { BuchtPostContect = b.BuchtPostContect, CabinetInput = b.CabinetInput, OtherBucht = b.OtherBucht, OtherTelephones = t, Telephone = b.BuchtPostContect.BuchtPostContect.BuchtTelephone })
        //            .SelectMany(x => x.OtherTelephones.DefaultIfEmpty(), (t1, t2) => new { BuchtPostContect = t1.BuchtPostContect, CabinetInput = t1.CabinetInput, OtherBucht = t1.OtherBucht, OtherTelephones = t2, Telephone = t1.BuchtPostContect.BuchtPostContect.BuchtTelephone })

        //            //join with ADSLPAPPort
        //            .GroupJoin(context.ADSLPAPPorts, b3 => b3.Telephone.TelephoneNo, p => p.TelephoneNo, (b3, p) => new { Bucht1 = b3, ADSLPAPPort = p })
        //            .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (bp, p1) => new { bucht1 = bp.Bucht1, BuchtADSLPAPPort = p1 })

        //            .Where(t => (cites.Count == 0 || cites.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID))
        //            && (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID) : center.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID))
        //            && (fromTelephone == -1 || (toTelephone == -1 ? t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo == fromTelephone : t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo >= fromTelephone))
        //            && (toTelephone == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo <= toTelephone)
        //            && (switchPreCode.Count == 0 || switchPreCode.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Telephone1.SwitchPrecode.ID))
        //            && (MDFs.Count == 0 || MDFs.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID))
        //            && (buchtTypes.Count == 0 || buchtTypes.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID))
        //            && (telephoneStatus.Count == 0 || telephoneStatus.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Status))
        //            && (preCodeType.Count == 0 || preCodeType.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.SwitchPrecode.PreCodeType))
        //            && (usageType.Count == 0 || usageType.Contains((int)t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.UsageType))
        //            && (column == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo == column)
        //            && (row == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalRowNo == row)
        //            && (buchtNo == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo == buchtNo)
        //            && (cabinets.Count == 0 || cabinets.Contains(t.bucht1.CabinetInput.Cabinet.ID))
        //            && (cabinetInput == -1 || t.bucht1.CabinetInput.InputNumber == cabinetInput)
        //            && (AorBType.Count == 0 || AorBType.Contains(t.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.ID))
        //            && (otherBuchtTypes.Count == 0 || otherBuchtTypes.Contains(t.bucht1.OtherBucht.BuchtTypeID))
        //            && (posts.Count == 0 || posts.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PostContact.Post.ID))
        //            && (t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PostContact.Post.IsDelete == false)
        //            && (postContact == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PostContact.ConnectionNo == postContact)
        //            && (string.IsNullOrEmpty(customerName) || customerName.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Customer.FirstNameOrTitle + " " + t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Customer.LastName))
        //            && (IsADSL == null || (IsADSL == true ? t.BuchtADSLPAPPort != null : t.BuchtADSLPAPPort == null))
        //            && (inRequest == null || (inRequest == true ? context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo) : !context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo)))
        //            && (onlyHeadNumber == null || (onlyHeadNumber == true ? (t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.InLine ? t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber == 1 : t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.OutLine) : (t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.InLine ? t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber != 1 : t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.OutLine)))
        //              )
        //            .Select(t => new AssignmentInfo
        //            {
        //                BuchtID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.ID,
        //                MDFID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
        //                PCMBuchtTypeID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID,
        //                MUID = "رک:" + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : " + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " + "کارت : " + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.Card.ToString() + " ،  " + "پورت : " + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber.ToString(),
        //                Connection = "ردیف:" + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه : " + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo,
        //                VerticalColumnNo = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
        //                VerticalRowNo = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalRowNo,
        //                BuchtNo = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo,
        //                ADSLVerticalColumnNo = t.BuchtADSLPAPPort.RowNo,
        //                ADSLVerticalRowNo = t.BuchtADSLPAPPort.ColumnNo,
        //                ADSLBuchtNo = t.BuchtADSLPAPPort.BuchtNo,
        //                AorBType = t.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.Name,
        //                BuchtTypeID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID,
        //                //CustomerName = t.BuchtPostContect.BuchtPostContect.BuchtTelephone.Customer.FirstNameOrTitle + " " + t.BuchtPostContect.BuchtPostContect.BuchtTelephone.Customer.LastName,
        //                CustomerName = t.bucht1.Telephone != null ? (t.bucht1.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.bucht1.Telephone.Customer.LastName ?? string.Empty) : (t.bucht1.OtherTelephones.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.bucht1.OtherTelephones.Customer.LastName ?? string.Empty),
        //                TelePhoneNo = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone != null ? t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo : t.bucht1.OtherTelephones.TelephoneNo,
        //                TelephoneStatus = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Status,
        //                CabinetID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.CabinetInput.Cabinet.ID,
        //                InputNumber = t.bucht1.CabinetInput.InputNumber,
        //                PostID = t.bucht1.BuchtPostContect.PostContect.PostID,
        //                PostContact = t.bucht1.BuchtPostContect.PostContect.ConnectionNo,
        //                PostContactStatus = t.bucht1.BuchtPostContect.PostContect.Status,
        //                BuchtTypeName = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtType.BuchtTypeName,
        //                MDFName = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + "(" + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description.ToString() + ")",
        //                CabinetName = t.bucht1.CabinetInput.Cabinet.CabinetNumber,
        //                PostContactStatusName = t.bucht1.BuchtPostContect.PostContect.PostContactStatus.Name,
        //                PostName = t.bucht1.BuchtPostContect.PostContect.Post.Number,
        //                OtherBucht = "ردیف:" + t.bucht1.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه : " + t.bucht1.OtherBucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.bucht1.OtherBucht.BuchtNo,
        //                OtherBuchtTypeName = t.bucht1.OtherBucht.BuchtType.BuchtTypeName,
        //                BuchtStatus = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.Status,
        //                BuchtStatusName = DB.GetEnumDescriptionByValue(typeof(DB.BuchtStatus), t.bucht1.BuchtPostContect.BuchtPostContect.bucht.Status),
        //                CenterID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
        //                Address = t.bucht1.Telephone.Address.AddressContent,
        //                PostallCode = t.bucht1.Telephone.Address.PostalCode,
        //                PAPName = t.BuchtADSLPAPPort.PAPInfo.Title,
        //                ADSLBucht = "ردیف : " + t.BuchtADSLPAPPort.RowNo + " ، طبقه : " + t.BuchtADSLPAPPort.ColumnNo + " ، اتصالی : " + t.BuchtADSLPAPPort.BuchtNo,
        //                RequestID = context.ViewReservBuchts.Where(t3 => t3.BuchtID == t.bucht1.BuchtPostContect.BuchtPostContect.bucht.ID).Take(1).SingleOrDefault().RequestID,
        //                //t.bucht1.BuchtPostContect.BuchtPostContect.bucht.InvestigatePossibilities.Where(t2 => t2.Request.EndDate == null && t2.Request.IsCancelation == false).OrderByDescending(t2 => t2.Request.InsertDate).Take(1).Select(t2 => t2.Request.ID).SingleOrDefault(),
        //                CauseOfCut = t.bucht1.Telephone.CauseOfCut.Name,

        //            }).OrderBy(t => t.VerticalColumnNo)
        //              .ThenBy(t => t.VerticalRowNo)
        //              .ThenBy(t => t.BuchtNo)
        //              .Skip(startRowIndex).Take(pageSize);



        //        return query.ToList();

        //    }
        //}


        public static List<AssignmentInfo> GetAllInformationBucht(
          List<int> cites,
          List<int> center,
          List<int> MDFs,
          int column,
          int row,
          List<int> cabinets,
          int cabinetInput,
          List<int> posts,
          int postContact,
          string customerName,
          long fromTelephone
          , long toTelephone
          , int startRowIndex
          , int pageSize
          , int buchtNo
          , List<int> buchtTypes
          , List<int> otherBuchtTypes
          , List<int> telephoneStatus
          , List<int> preCodeType
          , List<int> usageType
          , bool? IsADSL
          , List<int> AorBType
          , List<int> switchPreCode
          , bool? inRequest
          , bool? onlyHeadNumber
          , out int totalRecords
          , DateTime? installationDateFromDate
          , DateTime? installationDateToDate
          , DateTime? cutDate
          , List<int> causeOfCut
          , List<int> telephoneClass
          , List<int> buchtStatus
          , List<int> PCMType
          , bool isPrint
          )
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<AssignmentInfo> result = new List<AssignmentInfo>();
                context.CommandTimeout = 0;

                IQueryable<AssignmentInfo> query = context.Buchts

                    //join with telephon
                    .GroupJoin(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Bucht = b, Telephone = t })
                    .SelectMany(bt => bt.Telephone.DefaultIfEmpty(), (bt, t) => new { bucht = bt.Bucht, Telephone = t })

                    // join with PostContect
                    .GroupJoin(context.PostContacts, b1 => b1.bucht.ConnectionID, pc => pc.ID, (bu, pc) => new { bucht = bu, PostContect = pc })
                    .SelectMany(t2 => t2.PostContect.DefaultIfEmpty(), (BuchtPostcontect, t2) => new { PostContect = t2, BuchtPostContect = BuchtPostcontect.bucht })

                    .GroupJoin(context.CabinetInputs, b2 => b2.BuchtPostContect.bucht.CabinetInputID, ci => ci.ID, (BuchtPostContect, ci) => new { BuchtPostContect = BuchtPostContect, CabinetInput = ci })
                    .SelectMany(t3 => t3.CabinetInput.DefaultIfEmpty(), (BuchtPostContect, t3) => new { BuchtPostContect = BuchtPostContect.BuchtPostContect, CabinetInput = t3 })

                    // join with the Otherbucht
                    .GroupJoin(context.Buchts, fb => fb.BuchtPostContect.BuchtPostContect.bucht.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = sb })
                    .SelectMany(ob => ob.OtherBucht.DefaultIfEmpty(), (fb, ob) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = ob })

                    //join with ADSLPAPPort
                    .GroupJoin(context.ADSLPAPPorts, b3 => b3.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo, p => p.TelephoneNo, (b3, p) => new { Bucht1 = b3, ADSLPAPPort = p })
                    .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (bp, p1) => new { bucht1 = bp.Bucht1, BuchtADSLPAPPort = p1 })

                    //join with ReserveView
                    .GroupJoin(context.ViewReservBuchts, a => a.bucht1.BuchtPostContect.BuchtPostContect.bucht.ID, rv => rv.BuchtID, (a, rv) => new { Bucht1 = a, ViewReservBucht = rv })
                    .SelectMany(a => a.ViewReservBucht.DefaultIfEmpty(), (a, rv) => new { bucht1 = a.Bucht1, ViewReservBucht = rv })

                      .Where(t =>
                       (cites.Count == 0 || cites.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID))
                    && (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID) : center.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID))
                    && (fromTelephone == -1 || (toTelephone == -1 ? t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo == fromTelephone : t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo >= fromTelephone))
                    && (toTelephone == -1 || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo <= toTelephone)
                    && (switchPreCode.Count == 0 || switchPreCode.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Telephone1.SwitchPrecode.ID))
                    && (MDFs.Count == 0 || MDFs.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID))
                    && (buchtTypes.Count == 0 || buchtTypes.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID))
                    && (telephoneStatus.Count == 0 || telephoneStatus.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Status))
                    && (causeOfCut.Count == 0 || (causeOfCut.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CauseOfCut.ID) && center.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CenterID)))
                    && (PCMType.Count == 0 || (PCMType.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMTypeID) && center.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.PCMRock.CenterID)))
                    && (telephoneClass.Count == 0 || (telephoneClass.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.ClassTelephone) && center.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CenterID)))
                    && (preCodeType.Count == 0 || preCodeType.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.SwitchPrecode.PreCodeType))
                    && (usageType.Count == 0 || usageType.Contains((int)t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.UsageType))
                    && (column == -1 || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo == column)
                    && (row == -1 || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalRowNo == row)
                    && (buchtNo == -1 || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo == buchtNo)
                    && (cabinets.Count == 0 || cabinets.Contains(t.bucht1.bucht1.CabinetInput.Cabinet.ID))
                    && (cabinetInput == -1 || t.bucht1.bucht1.CabinetInput.InputNumber == cabinetInput)
                    && (AorBType.Count == 0 || AorBType.Contains(t.bucht1.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.ID))
                    && (otherBuchtTypes.Count == 0 || otherBuchtTypes.Contains(t.bucht1.bucht1.OtherBucht.BuchtTypeID))
                    && (posts.Count == 0 || (posts.Contains(t.bucht1.bucht1.BuchtPostContect.PostContect.PostID) && t.bucht1.bucht1.BuchtPostContect.PostContect.Post.IsDelete == false))
                    && (postContact == -1 || t.bucht1.bucht1.BuchtPostContect.PostContect.ConnectionNo == postContact)
                    && (string.IsNullOrEmpty(customerName) || customerName.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.FirstNameOrTitle + " " + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.LastName))
                    && (IsADSL == null || (IsADSL == true ? t.bucht1.BuchtADSLPAPPort != null : t.bucht1.BuchtADSLPAPPort == null))
                    && (inRequest == null || (inRequest == true ? context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo) : !context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo)))
                    && (onlyHeadNumber == null || (onlyHeadNumber == true ? (t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.InLine ? t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber == 1 : t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.OutLine) : (t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.InLine ? t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber != 1 : t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.OutLine)))
                    && (!installationDateFromDate.HasValue || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.InstallationDate >= installationDateFromDate)
                    && (!installationDateToDate.HasValue || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.InstallationDate <= installationDateToDate)
                    && (!cutDate.HasValue || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CutDate == cutDate)
                    && (buchtStatus.Count == 0 || buchtStatus.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Status))
                       )
                    .OrderBy(t => t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo)
                    .ThenBy(t => t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalRowNo)
                    .ThenBy(t => t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo)
                    .Select(t => new AssignmentInfo
                    {
                        BuchtID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.ID,
                        MDFID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                        PCMBuchtTypeID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID,
                        MUID = "رک:" + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : " + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " + "کارت : " + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.Card.ToString() + " ،  " + "پورت : " + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber.ToString(),
                        Rock = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.PCMRock.Number,
                        Shelf = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.Number,
                        Card = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.Card,
                        Connection = "ردیف:" + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه : " + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo,
                        VerticalColumnNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                        VerticalRowNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalRowNo,
                        BuchtNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo,
                        ADSLVerticalColumnNo = t.bucht1.BuchtADSLPAPPort.RowNo,
                        ADSLVerticalRowNo = t.bucht1.BuchtADSLPAPPort.ColumnNo,
                        ADSLBuchtNo = t.bucht1.BuchtADSLPAPPort.BuchtNo,

                        BuchtTypeID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID,
                        CustomerName = string.Format("{0} {1}", t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.FirstNameOrTitle, t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.LastName),
                        TelePhoneNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo,
                        TelephoneStatus = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Status,
                        InstallationDate = context.mi2sh(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.InstallationDate, true),
                        CabinetID = t.bucht1.bucht1.CabinetInput.Cabinet.ID,
                        CabinetName = t.bucht1.bucht1.CabinetInput.Cabinet.CabinetNumber,
                        InputNumber = t.bucht1.bucht1.CabinetInput.InputNumber,

                        PostID = t.bucht1.bucht1.BuchtPostContect.PostContect.PostID,
                        PostName = t.bucht1.bucht1.BuchtPostContect.PostContect.Post.Number,
                        PostContact = t.bucht1.bucht1.BuchtPostContect.PostContect.ConnectionNo,
                        PostContactStatus = t.bucht1.bucht1.BuchtPostContect.PostContect.Status,
                        PostContactStatusName = t.bucht1.bucht1.BuchtPostContect.PostContect.PostContactStatus.Name,
                        AorBType = t.bucht1.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.Name,
                        BuchtTypeName = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtType.BuchtTypeName,
                        MDFName = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + "(" + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description.ToString() + ")",

                        OtherBucht = "ردیف:" + t.bucht1.bucht1.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه : " + t.bucht1.bucht1.OtherBucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.bucht1.bucht1.OtherBucht.BuchtNo,

                        //PCMCabinetInputBucht = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.InLine ? new BuchtDetails { ColumnNo = t.bucht1.bucht1.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo, RowNo = t.bucht1.bucht1.OtherBucht.VerticalMDFRow.VerticalRowNo, BuchtNo = t.bucht1.bucht1.OtherBucht.BuchtNo } : context.Buchts
                        //                                .Where(bu => bu.CabinetInputID == t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.CabinetInputID && bu.BuchtTypeID == (int)DB.BuchtType.CustomerSide)
                        //                                .Select(bu => new BuchtDetails { ColumnNo = bu.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo, RowNo = bu.VerticalMDFRow.VerticalRowNo, BuchtNo = bu.BuchtNo}).SingleOrDefault(),

                        OtherVerticalColumnNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.InLine ? t.bucht1.bucht1.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo : context.Buchts
                                                        .Where(bu => bu.CabinetInputID == t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.CabinetInputID && bu.BuchtTypeID == (int)DB.BuchtType.CustomerSide)
                                                        .Select(bu => bu.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo)
                                                        .SingleOrDefault(),
                        OtherVerticalRowNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.InLine ? t.bucht1.bucht1.OtherBucht.VerticalMDFRow.VerticalRowNo : context.Buchts
                                                     .Where(bu2 => bu2.CabinetInputID == t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.CabinetInputID && bu2.BuchtTypeID == (int)DB.BuchtType.CustomerSide)
                                                     .Select(bu2 => bu2.VerticalMDFRow.VerticalRowNo)
                                                     .SingleOrDefault(),
                        OtherBuchtNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.InLine ? t.bucht1.bucht1.OtherBucht.BuchtNo : context.Buchts
                                                       .Where(bu3 => bu3.CabinetInputID == t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.CabinetInputID && bu3.BuchtTypeID == (int)DB.BuchtType.CustomerSide)
                                                       .Select(bu3 => bu3.BuchtNo)
                                                       .SingleOrDefault(),
                        OtherBuchtTypeName = t.bucht1.bucht1.OtherBucht.BuchtType.BuchtTypeName,
                        BuchtStatus = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Status,
                        BuchtStatusName = DB.GetEnumDescriptionByValue(typeof(DB.BuchtStatus), t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Status),
                        CenterID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
                        Address = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address.AddressContent,
                        PostallCode = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address.PostalCode,
                        PAPName = t.bucht1.BuchtADSLPAPPort.PAPInfo.Title,
                        ADSLBucht = "ردیف : " + t.bucht1.BuchtADSLPAPPort.RowNo + " ، طبقه : " + t.bucht1.BuchtADSLPAPPort.ColumnNo + " ، اتصالی : " + t.bucht1.BuchtADSLPAPPort.BuchtNo,
                        CauseOfCut = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CauseOfCut.Name,
                        CauseOfCutID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CauseOfCut.ID,
                        CutDate = context.mi2sh(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CutDate, true),
                        ClassTelephone = DB.GetEnumDescriptionByValue(typeof(DB.ClassTelephone), t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.ClassTelephone),
                        RequestID = t.ViewReservBucht.RequestID,
                    });


                if (isPrint)
                {


                    result = query.ToList();
                    totalRecords = result.Count();
                }
                else
                {

                    totalRecords = query.Count();
                    result = query.Skip(startRowIndex).Take(pageSize).ToList();

                }
                return result;

            }
        }

        public static List<AssignmentInfo> GetTotalInformationBucht(
     List<int> cites,
     List<int> center,
     List<int> MDFs,
     int column,
     int row,
     List<int> cabinets,
     int cabinetInput,
     List<int> posts,
     int postContact,
     string customerName,
     long fromTelephone
     , long toTelephone
     , int startRowIndex
     , int pageSize
     , int buchtNo
     , List<int> buchtTypes
     , List<int> otherBuchtTypes
     , List<int> telephoneStatus
     , List<int> preCodeType
     , List<int> usageType
     , bool? IsADSL
     , List<int> AorBType
     , List<int> switchPreCode
     , bool? inRequest
     , bool? onlyHeadNumber
     , out int totalRecords
     , DateTime? installationDateFromDate
     , DateTime? installationDateToDate
     , DateTime? cutDate
     , List<int> causeOfCut
     , List<int> telephoneClass
     , List<int> buchtStatus
     , List<int> PCMType
     , bool isPrint
     )
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<AssignmentInfo> result = new List<AssignmentInfo>();
                context.CommandTimeout = 0;

                IQueryable<AssignmentInfo> query = context.Buchts

                    //join with telephon
                    .GroupJoin(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Bucht = b, Telephone = t })
                    .SelectMany(bt => bt.Telephone.DefaultIfEmpty(), (bt, t) => new { bucht = bt.Bucht, Telephone = t })

                    // join with PostContect
                    .GroupJoin(context.PostContacts, b1 => b1.bucht.ConnectionID, pc => pc.ID, (bu, pc) => new { bucht = bu, PostContect = pc })
                    .SelectMany(t2 => t2.PostContect.DefaultIfEmpty(), (BuchtPostcontect, t2) => new { PostContect = t2, BuchtPostContect = BuchtPostcontect.bucht })

                    .GroupJoin(context.CabinetInputs, b2 => b2.BuchtPostContect.bucht.CabinetInputID, ci => ci.ID, (BuchtPostContect, ci) => new { BuchtPostContect = BuchtPostContect, CabinetInput = ci })
                    .SelectMany(t3 => t3.CabinetInput.DefaultIfEmpty(), (BuchtPostContect, t3) => new { BuchtPostContect = BuchtPostContect.BuchtPostContect, CabinetInput = t3 })

                    // join with the Otherbucht
                    .GroupJoin(context.Buchts, fb => fb.BuchtPostContect.BuchtPostContect.bucht.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = sb })
                    .SelectMany(ob => ob.OtherBucht.DefaultIfEmpty(), (fb, ob) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = ob })

                    //join with ADSLPAPPort
                    .GroupJoin(context.ADSLPAPPorts, b3 => b3.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo, p => p.TelephoneNo, (b3, p) => new { Bucht1 = b3, ADSLPAPPort = p })
                    .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (bp, p1) => new { bucht1 = bp.Bucht1, BuchtADSLPAPPort = p1 })

                    //join with ReserveView
                    .GroupJoin(context.ViewReservBuchts, a => a.bucht1.BuchtPostContect.BuchtPostContect.bucht.ID, rv => rv.BuchtID, (a, rv) => new { Bucht1 = a, ViewReservBucht = rv })
                    .SelectMany(a => a.ViewReservBucht.DefaultIfEmpty(), (a, rv) => new { bucht1 = a.Bucht1, ViewReservBucht = rv })

                      .Where(t =>
                                (cites.Count == 0 || cites.Contains((int)t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.CityID))
                                &&
                                (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains((int)t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.CenterID) : center.Contains((int)t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.CenterID))
                                &&
                                (fromTelephone == -1 || (toTelephone == -1 ? t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.TelephoneNo == fromTelephone : t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.TelephoneNo >= fromTelephone))
                                &&
                                (toTelephone == -1 || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.TelephoneNo <= toTelephone)
                                &&
                                (switchPreCode.Count == 0 || switchPreCode.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Telephone1.SwitchPrecode.ID))
                                &&
                                (MDFs.Count == 0 || MDFs.Contains((int)t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.MDFID))
                                &&
                                (buchtTypes.Count == 0 || buchtTypes.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID))
                                &&
                                (telephoneStatus.Count == 0 || (telephoneStatus.Contains(-1) ? (telephoneStatus.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Status) || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Status == (int)DB.TelephoneStatus.Connecting || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Status == (int)DB.TelephoneStatus.Cut) : telephoneStatus.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Status)))
                                &&
                                (causeOfCut.Count == 0 || (causeOfCut.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CauseOfCut.ID) && center.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CenterID)))
                                &&
                                (PCMType.Count == 0 || (PCMType.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMTypeID) && center.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.PCMRock.CenterID)))
                                &&
                                (telephoneClass.Count == 0 || (telephoneClass.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.ClassTelephone) && center.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CenterID)))
                                &&
                                (preCodeType.Count == 0 || preCodeType.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.SwitchPrecode.PreCodeType))
                                &&
                                (usageType.Count == 0 || usageType.Contains((int)t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.UsageType))
                                &&
                                (column == -1 || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.ColumnNo == column)
                                &&
                                (row == -1 || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.RowNo == row)
                                &&
                                (buchtNo == -1 || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo == buchtNo)
                                &&
                                (cabinets.Count == 0 || cabinets.Contains(t.bucht1.bucht1.CabinetInput.Cabinet.ID))
                                &&
                                (cabinetInput == -1 || t.bucht1.bucht1.CabinetInput.InputNumber == cabinetInput)
                                &&
                                (AorBType.Count == 0 || AorBType.Contains(t.bucht1.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.ID))
                                &&
                                (otherBuchtTypes.Count == 0 || otherBuchtTypes.Contains(t.bucht1.bucht1.OtherBucht.BuchtTypeID))
                                &&
                                (posts.Count == 0 || (posts.Contains(t.bucht1.bucht1.BuchtPostContect.PostContect.PostID) && t.bucht1.bucht1.BuchtPostContect.PostContect.Post.IsDelete == false))
                                &&
                                (postContact == -1 || t.bucht1.bucht1.BuchtPostContect.PostContect.ConnectionNo == postContact)
                                &&
                                (string.IsNullOrEmpty(customerName) || customerName.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.FirstNameOrTitle + " " + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.LastName))
                                &&
                                (IsADSL == null || (IsADSL == true ? t.bucht1.BuchtADSLPAPPort != null : t.bucht1.BuchtADSLPAPPort == null))
                                &&
                                (inRequest == null || (inRequest == true ? context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.TelephoneNo) : !context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.TelephoneNo)))
                                &&
                                (onlyHeadNumber == null || (onlyHeadNumber == true ? (t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.InLine ? t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber == 1 : t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.OutLine) : (t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.InLine ? t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber != 1 : t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.OutLine)))
                                &&
                                (!installationDateFromDate.HasValue || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.InstallationDate >= installationDateFromDate)
                                &&
                                (!installationDateToDate.HasValue || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.InstallationDate <= installationDateToDate)
                                &&
                                (!cutDate.HasValue || t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CutDate == cutDate)
                                &&
                                (buchtStatus.Count == 0 || buchtStatus.Contains(t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Status))
                       )
                    .OrderBy(t => t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.ColumnNo)
                    .ThenBy(t => t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.RowNo)
                    .ThenBy(t => t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo)
                    .Select(t => new AssignmentInfo
                    {
                        BuchtID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.ID,
                        MDFID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.MDFID,
                        PCMBuchtTypeID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID,
                        MUID = "رک:" + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : " + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " + "کارت : " + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.Card.ToString() + " ،  " + "پورت : " + t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber.ToString(),
                        Rock = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.PCMRock.Number,
                        Shelf = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.Number,
                        Card = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.Card,
                        VerticalColumnNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.ColumnNo,
                        VerticalRowNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.RowNo,
                        BuchtNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo,
                        ADSLVerticalColumnNo = t.bucht1.BuchtADSLPAPPort.RowNo,
                        ADSLVerticalRowNo = t.bucht1.BuchtADSLPAPPort.ColumnNo,
                        ADSLBuchtNo = t.bucht1.BuchtADSLPAPPort.BuchtNo,
                        BuchtTypeID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID,
                        CustomerName = string.Format("{0} {1}", t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.FirstNameOrTitle, t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.LastName),
                        TelePhoneNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo,
                        CustomerTypeTitle = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CustomerType.Title,
                        CityName = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.City.Name,
                        CenterName = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Center1.CenterName,
                        CustomerGroupTitle = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CustomerGroup.Title,
                        TelephoneStatus = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Status,
                        InstallationDate = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.InstallationDate.ToPersian(Date.DateStringType.Short),
                        CabinetID = t.bucht1.bucht1.CabinetInput.Cabinet.ID,
                        CabinetName = t.bucht1.bucht1.CabinetInput.Cabinet.CabinetNumber,
                        InputNumber = t.bucht1.bucht1.CabinetInput.InputNumber,
                        PostID = t.bucht1.bucht1.BuchtPostContect.PostContect.PostID,
                        PostName = t.bucht1.bucht1.BuchtPostContect.PostContect.Post.Number,
                        PostContact = t.bucht1.bucht1.BuchtPostContect.PostContect.ConnectionNo,
                        PostContactStatus = t.bucht1.bucht1.BuchtPostContect.PostContect.Status,
                        PostContactStatusName = t.bucht1.bucht1.BuchtPostContect.PostContect.PostContactStatus.Name,
                        AorBType = t.bucht1.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.Name,
                        BuchtTypeName = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtType.BuchtTypeName,
                        MDFName = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.MDF,
                        OtherVerticalColumnNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.CustomerSide ? t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Bucht1.ColumnNo : t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Bucht2.ColumnNo,
                        OtherVerticalRowNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.CustomerSide ? t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Bucht1.RowNo : t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Bucht2.RowNo,
                        OtherBuchtNo = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.CustomerSide ? t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Bucht1.BuchtNo : t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Bucht2.BuchtNo,
                        OtherBuchtTypeName = t.bucht1.bucht1.OtherBucht.BuchtType.BuchtTypeName,
                        BuchtStatus = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Status,
                        BuchtStatusName = DB.GetEnumDescriptionByValue(typeof(DB.BuchtStatus), t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.Status),
                        CenterID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.bucht.CenterID ?? default(int),
                        Address = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address.AddressContent,
                        PostallCode = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address.PostalCode,
                        PAPName = t.bucht1.BuchtADSLPAPPort.PAPInfo.Title,
                        ADSLBucht = "ردیف : " + t.bucht1.BuchtADSLPAPPort.RowNo + " ، طبقه : " + t.bucht1.BuchtADSLPAPPort.ColumnNo + " ، اتصالی : " + t.bucht1.BuchtADSLPAPPort.BuchtNo,
                        CauseOfCut = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CauseOfCut.Name,
                        CauseOfCutID = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CauseOfCut.ID,
                        CutDate = t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CutDate.ToPersian(Date.DateStringType.Short),
                        ClassTelephone = DB.GetEnumDescriptionByValue(typeof(DB.ClassTelephone), t.bucht1.bucht1.BuchtPostContect.BuchtPostContect.Telephone.ClassTelephone),
                        RequestID = t.ViewReservBucht.RequestID,
                    });


                if (isPrint)
                {
                    result = query.ToList();
                    totalRecords = result.Count();
                }
                else
                {
                    totalRecords = query.Count();
                    result = query.Skip(startRowIndex).Take(pageSize).ToList();
                }
                return result;
            }
        }

        public static DataTable GetDataTableAllInformationBucht(
     List<int> cites,
     List<int> center,
     List<int> MDFs,
     int column,
     int row,
     List<int> cabinets,
     int cabinetInput,
     List<int> posts,
     int postContact,
     string customerName,
     long fromTelephone
     , long toTelephone
     , int startRowIndex
     , int pageSize
     , int buchtNo
     , List<int> buchtTypes
     , List<int> otherBuchtTypes
     , List<int> telephoneStatus
     , List<int> preCodeType
     , List<int> usageType
     , bool? IsADSL
     , List<int> AorBType
     , List<int> switchPreCode
     , bool? inRequest
     , bool? onlyHeadNumber
     , out int totalRecords
     , DateTime? installationDateFromDate
     , DateTime? installationDateToDate
     , DateTime? cutDate
     , List<int> causeOfCut
     , List<int> telephoneClass
     , List<int> buchtStatus
     , List<int> PCMType
     , bool isPrint
     )
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<AssignmentInfo> result = new List<AssignmentInfo>();
                context.CommandTimeout = 0;
                IQueryable<AssignmentInfo> query = context.Buchts

                    //join with telephon
                    .GroupJoin(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Bucht = b, Telephone = t })
                    .SelectMany(t1 => t1.Telephone.DefaultIfEmpty(), (bt, t1) => new { bucht = bt.Bucht, BuchtTelephone = t1 })

                    // join with PostContect
                    .GroupJoin(context.PostContacts, b1 => b1.bucht.ConnectionID, pc => pc.ID, (bu, pc) => new { bucht = bu, PostContect = pc })
                    .SelectMany(t2 => t2.PostContect.DefaultIfEmpty(), (BuchtPostcontect, t2) => new { BuchtPostContect = BuchtPostcontect.bucht, PostContect = t2 })

                    .GroupJoin(context.CabinetInputs, b2 => b2.BuchtPostContect.bucht.CabinetInputID, ci => ci.ID, (BuchtPostContect, ci) => new { BuchtPostContect = BuchtPostContect, CabinetInput = ci })
                    .SelectMany(t3 => t3.CabinetInput.DefaultIfEmpty(), (BuchtPostContect, t3) => new { BuchtPostContect = BuchtPostContect.BuchtPostContect, CabinetInput = t3 })

                    // join with the Otherbucht
                    .GroupJoin(context.Buchts, fb => fb.BuchtPostContect.BuchtPostContect.bucht.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = sb })
                    .SelectMany(ob => ob.OtherBucht.DefaultIfEmpty(), (fb, ob) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = ob })

                    //join with ADSLPAPPort
                    .GroupJoin(context.ADSLPAPPorts, b3 => b3.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo, p => p.TelephoneNo, (b3, p) => new { Bucht1 = b3, ADSLPAPPort = p })
                    .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (bp, p1) => new { bucht1 = bp.Bucht1, BuchtADSLPAPPort = p1 })

                      .Where(t =>
                       (cites.Count == 0 || cites.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID))
                    && (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID) : center.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID))
                    && (fromTelephone == -1 || (toTelephone == -1 ? t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo == fromTelephone : t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo >= fromTelephone))
                    && (toTelephone == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo <= toTelephone)
                    && (switchPreCode.Count == 0 || switchPreCode.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Telephone1.SwitchPrecode.ID))
                    && (MDFs.Count == 0 || MDFs.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID))
                    && (buchtTypes.Count == 0 || buchtTypes.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID))
                    && (telephoneStatus.Count == 0 || telephoneStatus.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Status))
                    && (causeOfCut.Count == 0 || (causeOfCut.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.CauseOfCut.ID) && center.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.CenterID)))
                    && (PCMType.Count == 0 || (PCMType.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMTypeID) && center.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.PCMRock.CenterID)))
                    && (telephoneClass.Count == 0 || (telephoneClass.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.ClassTelephone) && center.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.CenterID)))
                    && (preCodeType.Count == 0 || preCodeType.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.SwitchPrecode.PreCodeType))
                    && (usageType.Count == 0 || usageType.Contains((int)t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.UsageType))
                    && (column == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo == column)
                    && (row == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalRowNo == row)
                    && (buchtNo == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo == buchtNo)
                    && (cabinets.Count == 0 || cabinets.Contains(t.bucht1.CabinetInput.Cabinet.ID))
                    && (cabinetInput == -1 || t.bucht1.CabinetInput.InputNumber == cabinetInput)
                    && (AorBType.Count == 0 || AorBType.Contains(t.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.ID))
                    && (otherBuchtTypes.Count == 0 || otherBuchtTypes.Contains(t.bucht1.OtherBucht.BuchtTypeID))
                    && (posts.Count == 0 || (posts.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PostContact.Post.ID) && t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PostContact.Post.IsDelete == false))
                    && (postContact == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PostContact.ConnectionNo == postContact)
                    && (string.IsNullOrEmpty(customerName) || customerName.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Customer.FirstNameOrTitle + " " + t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Customer.LastName))
                    && (IsADSL == null || (IsADSL == true ? t.BuchtADSLPAPPort != null : t.BuchtADSLPAPPort == null))
                    && (inRequest == null || (inRequest == true ? context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo) : !context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo)))
                    && (onlyHeadNumber == null || (onlyHeadNumber == true ? (t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.InLine ? t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber == 1 : t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.OutLine) : (t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.InLine ? t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber != 1 : t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.OutLine)))
                    && (!installationDateFromDate.HasValue || t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.InstallationDate >= installationDateFromDate)
                    && (!installationDateToDate.HasValue || t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.InstallationDate <= installationDateToDate)
                    && (!cutDate.HasValue || t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.CutDate == cutDate)
                    && (buchtStatus.Count == 0 || buchtStatus.Contains((int)t.bucht1.BuchtPostContect.BuchtPostContect.bucht.Status))
                       )
                    .Select(t => new AssignmentInfo
                    {
                        BuchtID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.ID,
                        MDFID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                        PCMBuchtTypeID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID,
                        MUID = "رک:" + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : " + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " + "کارت : " + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.Card.ToString() + " ،  " + "پورت : " + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber.ToString(),
                        Rock = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.PCMRock.Number,
                        Shelf = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.PCMShelf.Number,
                        Card = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PCM.Card,
                        Connection = "ردیف:" + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه : " + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo,
                        VerticalColumnNo = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                        VerticalRowNo = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalRowNo,
                        BuchtNo = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo,
                        ADSLVerticalColumnNo = t.BuchtADSLPAPPort.RowNo,
                        ADSLVerticalRowNo = t.BuchtADSLPAPPort.ColumnNo,
                        ADSLBuchtNo = t.BuchtADSLPAPPort.BuchtNo,
                        AorBType = t.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.Name,
                        BuchtTypeID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID,
                        CustomerName = string.Format("{1} {0}", t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Customer.FirstNameOrTitle, t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Customer.LastName),
                        TelePhoneNo = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo,
                        TelephoneStatus = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Status,
                        InstallationDate = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.InstallationDate.ToPersian(Date.DateStringType.Short),
                        CabinetID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.CabinetInput.Cabinet.ID,
                        InputNumber = t.bucht1.CabinetInput.InputNumber,
                        PostID = t.bucht1.BuchtPostContect.PostContect.PostID,
                        PostContact = t.bucht1.BuchtPostContect.PostContect.ConnectionNo,
                        PostContactStatus = t.bucht1.BuchtPostContect.PostContect.Status,
                        BuchtTypeName = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtType.BuchtTypeName,
                        MDFName = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString() + "(" + t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description.ToString() + ")",
                        CabinetName = t.bucht1.CabinetInput.Cabinet.CabinetNumber,
                        PostContactStatusName = t.bucht1.BuchtPostContect.PostContect.PostContactStatus.Name,
                        PostName = t.bucht1.BuchtPostContect.PostContect.Post.Number,
                        OtherBucht = "ردیف:" + t.bucht1.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه : " + t.bucht1.OtherBucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.bucht1.OtherBucht.BuchtNo,
                        OtherVerticalColumnNo = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.InLine ? t.bucht1.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo : context.Buchts
                                                        .Where(bu => bu.CabinetInputID == t.bucht1.BuchtPostContect.BuchtPostContect.bucht.CabinetInputID && bu.BuchtTypeID == (int)DB.BuchtType.CustomerSide)
                                                        .Select(bu => bu.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo)
                                                        .SingleOrDefault(),
                        OtherVerticalRowNo = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.InLine ? t.bucht1.OtherBucht.VerticalMDFRow.VerticalRowNo : context.Buchts
                                                     .Where(bu2 => bu2.CabinetInputID == t.bucht1.BuchtPostContect.BuchtPostContect.bucht.CabinetInputID && bu2.BuchtTypeID == (int)DB.BuchtType.CustomerSide)
                                                     .Select(bu2 => bu2.VerticalMDFRow.VerticalRowNo)
                                                     .SingleOrDefault(),
                        OtherBuchtNo = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.InLine ? t.bucht1.OtherBucht.BuchtNo : context.Buchts
                                                       .Where(bu3 => bu3.CabinetInputID == t.bucht1.BuchtPostContect.BuchtPostContect.bucht.CabinetInputID && bu3.BuchtTypeID == (int)DB.BuchtType.CustomerSide)
                                                       .Select(bu3 => bu3.BuchtNo)
                                                       .SingleOrDefault(),
                        OtherBuchtTypeName = t.bucht1.OtherBucht.BuchtType.BuchtTypeName,
                        BuchtStatus = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.Status,
                        BuchtStatusName = DB.GetEnumDescriptionByValue(typeof(DB.BuchtStatus), t.bucht1.BuchtPostContect.BuchtPostContect.bucht.Status),
                        CenterID = t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
                        Address = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Address.AddressContent,
                        PostallCode = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Address.PostalCode,
                        PAPName = t.BuchtADSLPAPPort.PAPInfo.Title,
                        ADSLBucht = "ردیف : " + t.BuchtADSLPAPPort.RowNo + " ، طبقه : " + t.BuchtADSLPAPPort.ColumnNo + " ، اتصالی : " + t.BuchtADSLPAPPort.BuchtNo,
                        CauseOfCut = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.CauseOfCut.Name,
                        CauseOfCutID = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.CauseOfCut.ID,
                        CutDate = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.CutDate.ToPersian(Date.DateStringType.Short),
                        ClassTelephone = DB.GetEnumDescriptionByValue(typeof(DB.ClassTelephone), t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.ClassTelephone),
                        RequestID = context.ViewReservBuchts.Where(t3 => t3.BuchtID == t.bucht1.BuchtPostContect.BuchtPostContect.bucht.ID).Take(1).Select(t3 => t3.RequestID).SingleOrDefault(),
                    }).OrderBy(t => t.VerticalColumnNo)
                            .ThenBy(t => t.VerticalRowNo)
                            .ThenBy(t => t.BuchtNo);



                totalRecords = query.Count();

                if (isPrint == false)
                {
                    query = query.Skip(startRowIndex).Take(pageSize);
                }

                DBHelper DBHelper = new DBHelper();
                DataTable dt = DBHelper.ExecuteDT((SqlCommand)context.GetCommand(query));
                return dt;
            }
        }


        //public static string GetADSLPAPPortInfo(long telephoneNo)
        //{
        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        ADSLPAPPort port = context.ADSLPAPPorts.Where(t => t.TelephoneNo == telephoneNo).SingleOrDefault();
        //        if (port != null)
        //            return port.RowNo + " - " + port.ColumnNo + " - " + port.BuchtNo;
        //        else
        //            return "";
        //    }
        //}

        public static int GetAllInformationBuchtCount(
               List<int> cites,
               List<int> center,
               List<int> MDFs,
               int column,
               int row,
               List<int> cabinets,
               int cabinetInput,
               List<int> posts,
               int postContact,
               string customerName,
               long fromTelephone
               , long toTelephone
               , int buchtNo
               , List<int> buchtTypes
               , List<int> otherBuchtTypes
               , List<int> telephoneStatus
               , List<int> preCodeType
               , List<int> usageType
               , bool? IsADSL
               , List<int> AorBType
               , List<int> switchPreCode
               , bool? inRequest
               , bool? onlyHeadNumber
               )
        {
            using (MainDataContext context = new MainDataContext())
            {

                context.CommandTimeout = 0;
                return context.Buchts

                    //join with telephon
                    .GroupJoin(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Bucht = b, Telephone = t })
                    .SelectMany(t1 => t1.Telephone.DefaultIfEmpty(), (bt, t1) => new { bucht = bt.Bucht, BuchtTelephone = t1 })

                    // join with PostContect
                    .GroupJoin(context.PostContacts, b1 => b1.bucht.ConnectionID, pc => pc.ID, (bu, pc) => new { bucht = bu, PostContect = pc })
                    .SelectMany(t2 => t2.PostContect.DefaultIfEmpty(), (BuchtPostcontect, t2) => new { BuchtPostContect = BuchtPostcontect.bucht, PostContect = t2 })

                    .GroupJoin(context.CabinetInputs, b2 => b2.BuchtPostContect.bucht.CabinetInputID, ci => ci.ID, (BuchtPostContect, ci) => new { BuchtPostContect = BuchtPostContect, CabinetInput = ci })
                    .SelectMany(t3 => t3.CabinetInput.DefaultIfEmpty(), (BuchtPostContect, t3) => new { BuchtPostContect = BuchtPostContect.BuchtPostContect, CabinetInput = t3 })

                    // join with the Otherbucht
                    .GroupJoin(context.Buchts, fb => fb.BuchtPostContect.BuchtPostContect.bucht.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = sb })
                    .SelectMany(ob => ob.OtherBucht.DefaultIfEmpty(), (fb, ob) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = ob })

                    // join with the Otherbucht telephone
                    .GroupJoin(context.Telephones, b => b.OtherBucht.SwitchPortID, t => t.SwitchPortID, (b, t) => new { BuchtPostContect = b.BuchtPostContect, CabinetInput = b.CabinetInput, OtherBucht = b.OtherBucht, OtherTelephones = t, Telephone = b.BuchtPostContect.BuchtPostContect.BuchtTelephone })
                    .SelectMany(x => x.OtherTelephones.DefaultIfEmpty(), (t1, t2) => new { BuchtPostContect = t1.BuchtPostContect, CabinetInput = t1.CabinetInput, OtherBucht = t1.OtherBucht, OtherTelephones = t2, Telephone = t1.BuchtPostContect.BuchtPostContect.BuchtTelephone })

                    //join with ADSLPAPPort
                    .GroupJoin(context.ADSLPAPPorts, b3 => b3.Telephone.TelephoneNo, p => p.TelephoneNo, (b3, p) => new { Bucht1 = b3, ADSLPAPPort = p })
                    .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (bp, p1) => new { bucht1 = bp.Bucht1, BuchtADSLPAPPort = p1 })

                    .Where(t => (cites.Count == 0 || cites.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Center.Region.CityID))
                    && (center.Count == 0 ? DB.CurrentUser.CenterIDs.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID) : center.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID))
                    && (fromTelephone == -1 || (toTelephone == -1 ? t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo == fromTelephone : t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo >= fromTelephone))
                    && (toTelephone == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo <= toTelephone)
                    && (switchPreCode.Count == 0 || switchPreCode.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Telephone1.SwitchPrecode.ID))
                    && (MDFs.Count == 0 || MDFs.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDFID))
                    && (buchtTypes.Count == 0 || buchtTypes.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID))
                    && (telephoneStatus.Count == 0 || telephoneStatus.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Status))
                    && (preCodeType.Count == 0 || preCodeType.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.SwitchPrecode.PreCodeType))
                    && (usageType.Count == 0 || usageType.Contains((int)t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.UsageType))
                    && (column == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo == column)
                    && (row == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.VerticalMDFRow.VerticalRowNo == row)
                    && (buchtNo == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtNo == buchtNo)
                    && (cabinets.Count == 0 || cabinets.Contains(t.bucht1.CabinetInput.Cabinet.ID))
                    && (cabinetInput == -1 || t.bucht1.CabinetInput.InputNumber == cabinetInput)
                    && (AorBType.Count == 0 || AorBType.Contains(t.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.ID))
                    && (otherBuchtTypes.Count == 0 || otherBuchtTypes.Contains(t.bucht1.OtherBucht.BuchtTypeID))
                    && (posts.Count == 0 || posts.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PostContact.Post.ID))
                    && (t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PostContact.Post.IsDelete == false)
                    && (postContact == -1 || t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PostContact.ConnectionNo == postContact)
                    && (string.IsNullOrEmpty(customerName) || customerName.Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Customer.FirstNameOrTitle + " " + t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.Customer.LastName))
                    && (IsADSL == null || (IsADSL == true ? t.BuchtADSLPAPPort != null : t.BuchtADSLPAPPort == null))
                    && (inRequest == null || (inRequest == true ? context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo) : !context.Requests.Where(t2 => t2.EndDate == null).Select(t2 => t2.TelephoneNo).Contains(t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTelephone.TelephoneNo)))
                    && (onlyHeadNumber == null || (onlyHeadNumber == true ? (t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.InLine ? t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber == 1 : t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.OutLine) : (t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID == (int)DB.BuchtType.InLine ? t.bucht1.BuchtPostContect.BuchtPostContect.bucht.PCMPort.PortNumber != 1 : t.bucht1.BuchtPostContect.BuchtPostContect.bucht.BuchtTypeID != (int)DB.BuchtType.OutLine)))
                         ).Count();
            }
        }


        public static CRM.Data.Status GetStatus(int requestType, int statusType)
        {
            return Data.StatusDB.GetStatusbyStatusType(requestType, statusType);
        }

        public static int GetADSLDischargeReasonIDByTitle(string ADSLDischargeReason)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLDischargeReasons.Where(t => t.Title.Equals(ADSLDischargeReason)).Select(t => t.ID).SingleOrDefault();
            }
        }

        public static string GetADSLDischargeReasonITitleById(long ADSLDischargeReasonId)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ADSLDischargeReasons.Where(t => t.ID.Equals(ADSLDischargeReasonId)).Select(t => t.Title).SingleOrDefault();
            }
        }

        public static long? GetTelephonNoByBuchtID(long? buchtID)
        {
            if (buchtID == null) return null;
            Bucht bucht = Data.BuchtDB.GetBuchtByID((long)buchtID);
            if (bucht == null || bucht.SwitchPortID == null) return null;
            return DB.SearchByPropertyName<Telephone>("SwitchPortID", bucht.SwitchPortID).SingleOrDefault().TelephoneNo;
        }

        public static Bucht GetBuchtIDByTelephonNo(long? TelephoneNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Join(context.Telephones, b => b.SwitchPortID, t => t.SwitchPortID, (b, t) => new { bucht = b, tel = t })
                                    .Where(x => x.tel.TelephoneNo == TelephoneNo).Select(t => t.bucht).SingleOrDefault();
            }
        }

        #region Generate RequestID

        public static long GetMaxRequestIDByDate(DateTime date)
        {
            using (MainDataContext context = new MainDataContext())
            {
                long requestId = 0;
                if (context.Requests.Where(t => t.InsertDate.Date == date.Date).ToList().Count != 0)
                {
                    requestId = context.Requests
                                       .Where(t => t.InsertDate.Date == date.Date)
                                       .Max(t => t.ID);
                }
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

        #endregion

        /// <summary>
        /// نوع رند تلفن را بررسی میکند
        /// </summary>
        /// <param name="telephoneNo"></param>
        /// <returns></returns>
        public static int? CheckTypeRoundTelephone(long telephoneNo)
        {
            string tel = telephoneNo.ToString();

            if (tel.Distinct().ToArray().Count() == 1)
            {
                //MMMMMMMM
                return (int)DB.RoundType.Diamond;
            }
            else if (tel.Substring(4).Distinct().ToArray().Count() == 1)
            {
                //XYZQMMMM
                return (int)DB.RoundType.Gold;
            }
            else if (tel.Substring(6).Distinct().ToArray().Count() == 1)
            {
                //XYZQNRMM
                return (int)DB.RoundType.Gold;
            }
            else if (tel.Distinct().ToArray().Count() == 2)
            {
                //XYXYXYXY
                // telephon must contain 2-type number
                return (int)DB.RoundType.Gold;
            }
            else if (tel.Distinct().ToArray().Count() == 3)
            {
                //XYZXYZXY
                // telephon must contain 3-type number
                return (int)DB.RoundType.Silver;
            }
            else if (tel.Substring(4).Distinct().ToArray().Count() == 2)
            {
                //XYZQMNMN
                return (int)DB.RoundType.Silver;
            }
            else if (tel.Substring(3).Distinct().ToArray().Count() == 1)
            {
                //XYZMMMMM
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (tel.Substring(4).Distinct().ToArray().Count() == 1 && tel.Substring(2, 2).Distinct().ToArray().Count() == 1)
            {
                //XYZZMMMM
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (
                     (string.Join("", tel.Substring(0, 2).Distinct()) == string.Join("", tel.Substring(4, 2).Distinct())) &&
                     (string.Join("", tel.Substring(2, 2).Distinct()) == string.Join("", tel.Substring(6, 2).Distinct()))
                    )
            {
                //XYMNXYMN
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (
                (string.Join("", tel.Substring(0, 2).Distinct()) == string.Join("", tel.Substring(4, 2).Distinct())) &&
                (tel.Substring(2, 2).Distinct().ToArray().Count() == 1) &&
                (tel.Substring(6, 2).Distinct().ToArray().Count() == 1)
                )
            {
                //XYMMXYNN
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (
                     (tel.Substring(0, 2).Distinct().ToArray().Count() == 1) &&
                     (tel.Substring(2, 2).Distinct().ToArray().Count() == 1) &&
                     (tel.Substring(4, 2).Distinct().ToArray().Count() == 1) &&
                     (tel.Substring(6, 2).Distinct().ToArray().Count() == 1)
                     )
            {
                //XYMMNNZZ
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (
                      (tel.Substring(4, 2).Distinct().ToArray().Count() == 1) &&
                      (tel.Substring(6, 2).Distinct().ToArray().Count() == 1)
                     )
            {
                //XYZQMMNN
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (
                     (string.Join("", tel.Substring(0, 2).Distinct()) == string.Join("", tel.Substring(2, 2).Distinct())) &&
                     (string.Join("", tel.Substring(4, 1).Distinct()) == string.Join("", tel.Substring(6, 1).Distinct())) &&
                     (string.Join("", tel.Substring(6, 1).Distinct()) == string.Join("", tel.Substring(7, 1).Distinct()))
                     )
            {
                //XYXYMNMN
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (
                    (string.Join("", tel.Substring(0, 2).Distinct()) != string.Join("", tel.Substring(2, 2).Distinct())) &&
                    (string.Join("", tel.Substring(4, 1).Distinct()) == string.Join("", tel.Substring(6, 1).Distinct())) &&
                    (string.Join("", tel.Substring(6, 1).Distinct()) == string.Join("", tel.Substring(7, 1).Distinct()))
                    )
            {
                //XYZQMNMN
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (
                     (string.Join("", tel.Substring(0, 3).Distinct()) == string.Join("", tel.Substring(3, 3).Distinct())) &&
                     (tel.Substring(6, 2).Distinct().Count() == 1)
                    )
            {
                //XYZXYZMM
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (
                     (tel.Substring(2, 5).Distinct().Count() == 1)
                    )
            {
                //XYMMMMMQ
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (
                  (tel.Substring(2, 2).Distinct().Count() == 1) &&
                  (string.Join("", tel.Substring(2, 2).Distinct()) == string.Join("", tel.Substring(5, 2).Distinct())) &&
                  (string.Join("", tel.Substring(4, 1).Distinct()) == string.Join("", tel.Substring(7, 1).Distinct()))
                 )
            {
                //XYMMQMMQ
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (
                     (tel.Substring(0, 2).Distinct().Count() == 1) &&
                     Regex.IsMatch(tel, @"^\d\d123456$")
                    )
            {
                //XX123456
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (Regex.IsMatch(tel, @"^\d\d123456$"))
            {
                //XY123456
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (
                    (tel.Substring(0, 2).Distinct().Count() == 1) &&
                    Regex.IsMatch(tel, @"^\d\d654321$")
                   )
            {
                //XX654321
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else if (Regex.IsMatch(tel, @"^\d\d654321$"))
            {
                //XY654321
                // استانی کرمانشاه
                return (int)DB.RoundType.Provincial;
            }
            else
            {
                return null;
            }
            //else if()
            //{

            //}

            //char[] tele = telephoneNo.ToString().ToCharArray();
            //char[] CharViewed = new char[8];
            //int NumberDifferentCharacters = 0;
            //int? roundType = null;
            //int j = 0;
            //for (int i = tele.Count() - 1; i >= 0; i--)
            //{
            //    if (!CharViewed.Contains(tele[i]))
            //    {
            //        NumberDifferentCharacters++;
            //        CharViewed[j] = tele[i];
            //        j++;
            //    }
            //    if (i == 0 && NumberDifferentCharacters == 1) roundType = (int)DB.RoundType.Diamond;
            //    else if (i == 4 && NumberDifferentCharacters == 1 && roundType != (int)DB.RoundType.Diamond) roundType = (int)DB.RoundType.Gold;
            //    else if (i == 2 && NumberDifferentCharacters == 1 && roundType != (int)DB.RoundType.Diamond) roundType = (int)DB.RoundType.Gold;
            //    else if (i == 0 && NumberDifferentCharacters == 2 && roundType != (int)DB.RoundType.Diamond) roundType = (int)DB.RoundType.Gold;
            //    else if (i == 0 && NumberDifferentCharacters == 3 && roundType != (int)DB.RoundType.Gold) roundType = (int)DB.RoundType.Silver;
            //    else if (i == 4 && NumberDifferentCharacters == 2 && roundType != (int)DB.RoundType.Gold) roundType = (int)DB.RoundType.Silver;
            //}

            //   return roundType;
        }

        public static bool ExistPortNo(SwitchPort switchPort)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.SwitchPorts.Any(t => t.PortNo == switchPort.PortNo);
            }
        }

        //public static int? GetSwitchPortIDTypeByTelephone(Telephone telephoneNo)
        //{
        //    return telephoneNo.SwitchPortID;
        //    //using (MainDataContext context = new MainDataContext())
        //    //{
        //    //    SwitchPort switchPort = context.SwitchPorts.Where(t => t.ID == telephoneNo.SwitchPortID).SingleOrDefault();
        //    //    if (switchPort.Type == true)
        //    //        return switchPort.ID;
        //    //    else
        //    //        return null;
        //    //}

        //}

        public static int GetCurrentStepByRequest(Request request)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Requests.Where(t => t.ID == request.ID).Select(t => t.Status.RequestStepID).SingleOrDefault();
            }
        }

        //milad doran
        //public static AssignmentInfo GetAllInformationByTelephoneNo(long telephoneNo)
        //{

        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        IQueryable<AssignmentInfo> query = context.PostContacts
        //            .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
        //            .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })

        //             // join with the Otherbucht
        //            .GroupJoin(context.Buchts, fb => fb.Buchts.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new { Bucht = fb.Buchts, PostContact = fb.PostContacts, OtherBucht = sb })
        //            .SelectMany(ob => ob.OtherBucht.DefaultIfEmpty(), (fb, ob) => new { Bucht = fb.Bucht, PostContact = fb.PostContact, OtherBucht = ob })

        //            // join with the Telephone
        //            .GroupJoin(context.Telephones, b => b.Bucht.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContact })
        //            .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })

        //               //join with ADSLPAPPort
        //            .GroupJoin(context.ADSLPAPPorts, b3 => b3.Telephone.TelephoneNo, p => p.TelephoneNo, (b3, p) => new { Bucht1 = b3, ADSLPAPPort = p })
        //            .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (bp, p1) => new { bucht1 = bp.Bucht1, BuchtADSLPAPPort = p1 })

        //            .Where(t3 => t3.bucht1.Telephone.TelephoneNo == telephoneNo &&
        //                         t3.bucht1.Bucht.Bucht.PostContact.ConnectionType != (byte)DB.PostContactStatus.Deleted &&
        //                         t3.bucht1.Bucht.Bucht.PostContact.Post.IsDelete == false)
        //             .Select(t => new AssignmentInfo
        //             {
        //                 BuchtType = t.bucht1.Bucht.Bucht.BuchtTypeID,
        //                 BuchtStatus = t.bucht1.Bucht.Bucht.Status,
        //                 MUID = "رک:" + t.bucht1.Bucht.Bucht.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : " + t.bucht1.Bucht.Bucht.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " + "کارت : " + t.bucht1.Bucht.Bucht.PCMPort.PCM.Card.ToString() + " ،  " + "پورت : " + t.bucht1.Bucht.Bucht.PCMPort.PortNumber.ToString(),
        //                 PCMPortIDInBuchtTable = t.bucht1.Bucht.Bucht.PCMPortID,
        //                 InputNumber = t.bucht1.Bucht.Bucht.CabinetInput.InputNumber,
        //                 InputNumberID = t.bucht1.Bucht.Bucht.CabinetInput.ID,
        //                 PostContact = t.bucht1.Bucht.Bucht.PostContact.ConnectionNo,
        //                 PostContactID = t.bucht1.Bucht.Bucht.PostContact.ID,
        //                 PostContactStatus = t.bucht1.Bucht.Bucht.PostContact.Status,
        //                 Connection = "ردیف : " + t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " ، " + "طبقه : " + t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.bucht1.Bucht.Bucht.BuchtNo,
        //                 OtherBucht = "ردیف:" + t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه : " + t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.bucht1.Bucht.OtherBucht.BuchtNo,
        //                 OtherBuchtTypeName = t.bucht1.Bucht.OtherBucht.BuchtType.BuchtTypeName,
        //                 VerticalColumnNo = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
        //                 VerticalRowNo = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalRowNo,

        //                 PCMCabinetInputColumnNo = t.bucht1.Bucht.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
        //                                           t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo :
        //                                           context.Buchts.Where(b =>
        //                                                                    b.CabinetInputID == t.bucht1.Bucht.Bucht.CabinetInputID && b.BuchtTypeID == (int)DB.BuchtType.CustomerSide
        //                                                               )
        //                                                         .Select(b => b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo)
        //                                                         .SingleOrDefault(),
        //                 PCMCabinetInputRowNo = t.bucht1.Bucht.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
        //                                        t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalRowNo :
        //                                        context.Buchts.Where(b =>
        //                                                                 b.CabinetInputID == t.bucht1.Bucht.Bucht.CabinetInputID && b.BuchtTypeID == (int)DB.BuchtType.CustomerSide
        //                                                            )
        //                                                      .Select(b => b.VerticalMDFRow.VerticalRowNo)
        //                                                      .SingleOrDefault(),
        //                 PCMCabinetInputBuchtNo = t.bucht1.Bucht.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
        //                                          t.bucht1.Bucht.OtherBucht.BuchtNo :
        //                                          context.Buchts.Where(b =>
        //                                                                   b.CabinetInputID == t.bucht1.Bucht.Bucht.CabinetInputID && b.BuchtTypeID == (int)DB.BuchtType.CustomerSide
        //                                                              )
        //                                                        .Select(b => b.BuchtNo)
        //                                                        .SingleOrDefault(),
        //                 BuchtNo = t.bucht1.Bucht.Bucht.BuchtNo,
        //                 BuchtID = t.bucht1.Bucht.Bucht.ID,
        //                 PortNo = t.bucht1.Bucht.Bucht.SwitchPort.PortNo,
        //                 SwitchPortID = t.bucht1.Bucht.Bucht.SwitchPort.ID,
        //                 SwitchCode = t.bucht1.Bucht.Bucht.SwitchPort.Switch.SwitchCode,
        //                 SwitchID = t.bucht1.Bucht.Bucht.SwitchPort.Switch.ID,
        //                 TelePhoneNo = t.bucht1.Telephone.TelephoneNo,
        //                 SwitchPreCodeID = t.bucht1.Telephone.SwitchPrecodeID,
        //                 ClassTelephone = t.bucht1.Telephone.ClassTelephone == 0 ? string.Empty : Helpers.GetEnumDescription((int)t.bucht1.Telephone.ClassTelephone, typeof(DB.ClassTelephone)),
        //                 PostID = t.bucht1.Bucht.Bucht.PostContact.Post.ID,
        //                 AorBTypeID = t.bucht1.Bucht.Bucht.PostContact.Post.AORBPostAndCabinet.ID,
        //                 AorBType = t.bucht1.Bucht.Bucht.PostContact.Post.AORBPostAndCabinet.Name,
        //                 CabinetID = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.ID,
        //                 BuchtTypeID = t.bucht1.Bucht.Bucht.BuchtTypeID,
        //                 CabinetInputID = t.bucht1.Bucht.Bucht.CabinetInputID,
        //                 CabinetUsageTypeID = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.CabinetUsageType,
        //                 CabinetSwitchID = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.SwitchID,
        //                 isOutBoundCabinet = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.IsOutBound,
        //                 CabinetName = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.CabinetNumber,
        //                 PostName = t.bucht1.Bucht.Bucht.PostContact.Post.Number,
        //                 isOutBoundPost = t.bucht1.Bucht.Bucht.PostContact.Post.IsOutBorder,
        //                 CenterName = t.bucht1.Telephone.Center.CenterName,
        //                 CenterID = t.bucht1.Telephone.Center.ID,
        //                 CityName = t.bucht1.Telephone.Center.Region.City.Name,
        //                 CabinetIDForSearch = t.bucht1.Bucht.Bucht.PostContact.Post.Cabinet.ID,
        //                 CustomerName = (t.bucht1.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.bucht1.Telephone.Customer.LastName ?? string.Empty),
        //                 Customer = t.bucht1.Telephone.Customer,
        //                 TelephoneStatus = t.bucht1.Telephone.Status,
        //                 CauseOfCut = t.bucht1.Telephone.CauseOfCut.Name,
        //                 PostContactType = t.bucht1.Bucht.Bucht.PostContact.ConnectionType,
        //                 MDFID = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
        //                 MDFName = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),
        //                 InstallAddress = t.bucht1.Telephone.Address,
        //                 CorrespondenceAddress = t.bucht1.Telephone.Address1,
        //                 Address = t.bucht1.Telephone.Address.AddressContent,
        //                 PostallCode = t.bucht1.Telephone.Address.PostalCode,
        //                 PAPName = t.BuchtADSLPAPPort.PAPInfo.Title,
        //                 ADSLBucht = "ردیف : " + t.BuchtADSLPAPPort.RowNo + " ، طبقه : " + t.BuchtADSLPAPPort.ColumnNo + " ، اتصالی : " + t.BuchtADSLPAPPort.BuchtNo,
        //                 ADSLVerticalRowNo = t.BuchtADSLPAPPort.ColumnNo,
        //                 ADSLVerticalColumnNo = t.BuchtADSLPAPPort.RowNo,
        //                 ADSLBuchtNo = t.BuchtADSLPAPPort.BuchtNo,
        //                 SpecialServices = context.TelephoneSpecialServiceTypes.Where(st => st.TelephoneNo == t.bucht1.Telephone.TelephoneNo).Any() ? string.Join(" , ", t.bucht1.Telephone.TelephoneSpecialServiceTypes.Select(st => st.SpecialServiceType.Title).ToArray()) : string.Empty,
        //                 RequestPaymentAmountSum = context.Requests
        //                                                  .Where(re =>
        //                                                              (re.TelephoneNo == t.bucht1.Telephone.TelephoneNo) &&
        //                                                              (re.RequestTypeID == (int)DB.RequestType.Dayri) &&
        //                                                              (re.EndDate.HasValue)
        //                                                         )
        //                                                  .OrderByDescending(re => re.EndDate.Value).Take(1)
        //                                                  .SingleOrDefault()
        //                                                  .RequestPayments
        //                                                  .Where(rp =>
        //                                                             (rp.IsPaid.HasValue && rp.IsPaid.Value) &&
        //                                                             (rp.IsKickedBack.HasValue && !rp.IsKickedBack.Value)
        //                                                         )
        //                                                  .Sum(rp => rp.AmountSum)
        //             });

        //        return query.OrderBy(t => t.PostContactID).SingleOrDefault();

        //    }
        //}

        //TODO:rad 13950324
        public static AssignmentInfo GetAllInformationByTelephoneNo(long telephoneNo)
        {

            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<AssignmentInfo> query = context.PostContacts

                                                          //join with Bucht
                                                          .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
                                                          .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })

                                                           //join with the Otherbucht
                                                          .GroupJoin(context.Buchts, fb => fb.Buchts.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new { Bucht = fb.Buchts, PostContact = fb.PostContacts, OtherBucht = sb })
                                                          .SelectMany(ob => ob.OtherBucht.DefaultIfEmpty(), (fb, ob) => new { Bucht = fb.Bucht, PostContact = fb.PostContact, OtherBucht = ob })

                                                          //join with the Telephone
                                                          .GroupJoin(context.Telephones, b => b.Bucht.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContact })
                                                          .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })

                                                           //join with ADSLPAPPort
                                                          .GroupJoin(context.ADSLPAPPorts, b3 => b3.Telephone.TelephoneNo, p => p.TelephoneNo, (b3, p) => new { Bucht1 = b3, ADSLPAPPort = p })
                                                          .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (bp, p1) => new { bucht1 = bp.Bucht1, BuchtADSLPAPPort = p1 })

                                                          .Where(t3 =>
                                                                       t3.bucht1.Telephone.TelephoneNo == telephoneNo &&
                                                                       t3.bucht1.Bucht.Bucht.PostContact.ConnectionType != (byte)DB.PostContactStatus.Deleted &&
                                                                       t3.bucht1.Bucht.Bucht.PostContact.Post.IsDelete == false
                                                                 )
                                                          .Select(t => new AssignmentInfo
                                                          {
                                                              BuchtType = t.bucht1.Bucht.Bucht.BuchtTypeID,
                                                              BuchtStatus = t.bucht1.Bucht.Bucht.Status,

                                                              MUID = "رک:" + t.bucht1.Bucht.Bucht.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " +
                                                                     "شلف : " + t.bucht1.Bucht.Bucht.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " +
                                                                     "کارت : " + t.bucht1.Bucht.Bucht.PCMPort.PCM.Card.ToString() + " ،  " +
                                                                     "پورت : " + t.bucht1.Bucht.Bucht.PCMPort.PortNumber.ToString(),

                                                              PCMPortIDInBuchtTable = t.bucht1.Bucht.Bucht.PCMPortID,
                                                              InputNumber = t.bucht1.Bucht.Bucht.CabinetInput.InputNumber,
                                                              InputNumberID = t.bucht1.Bucht.Bucht.CabinetInput.ID,
                                                              PostContact = t.bucht1.Bucht.Bucht.PostContact.ConnectionNo,
                                                              PostContactID = t.bucht1.Bucht.Bucht.PostContact.ID,
                                                              PostContactStatus = t.bucht1.Bucht.Bucht.PostContact.Status,

                                                              Connection = "ردیف : " + t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " ، " +
                                                                           "طبقه : " + t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalRowNo + " ،  " +
                                                                           "اتصالی : " + t.bucht1.Bucht.Bucht.BuchtNo,

                                                              OtherBucht = "ردیف:" + t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " +
                                                                           "طبقه : " + t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalRowNo + " ،  " +
                                                                           "اتصالی : " + t.bucht1.Bucht.OtherBucht.BuchtNo,

                                                              OtherBuchtTypeName = t.bucht1.Bucht.OtherBucht.BuchtType.BuchtTypeName,
                                                              VerticalColumnNo = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                                                              VerticalRowNo = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalRowNo,

                                                              PCMCabinetInputColumnNo = t.bucht1.Bucht.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
                                                                                        t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo :
                                                                                        context.Buchts.Where(b =>
                                                                                                                 b.CabinetInputID == t.bucht1.Bucht.Bucht.CabinetInputID &&
                                                                                                                 b.BuchtTypeID == (int)DB.BuchtType.CustomerSide
                                                                                                            )
                                                                                                      .Select(b => b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo)
                                                                                                      .SingleOrDefault(),

                                                              PCMCabinetInputRowNo = t.bucht1.Bucht.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
                                                                                     t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalRowNo :
                                                                                     context.Buchts.Where(b =>
                                                                                                              b.CabinetInputID == t.bucht1.Bucht.Bucht.CabinetInputID &&
                                                                                                              b.BuchtTypeID == (int)DB.BuchtType.CustomerSide
                                                                                                         )
                                                                                                   .Select(b => b.VerticalMDFRow.VerticalRowNo)
                                                                                                   .SingleOrDefault(),

                                                              PCMCabinetInputBuchtNo = t.bucht1.Bucht.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
                                                                                       t.bucht1.Bucht.OtherBucht.BuchtNo :
                                                                                       context.Buchts.Where(b =>
                                                                                                                b.CabinetInputID == t.bucht1.Bucht.Bucht.CabinetInputID &&
                                                                                                                b.BuchtTypeID == (int)DB.BuchtType.CustomerSide
                                                                                                           )
                                                                                                     .Select(b => b.BuchtNo)
                                                                                                     .SingleOrDefault(),

                                                              BuchtNo = t.bucht1.Bucht.Bucht.BuchtNo,
                                                              BuchtID = t.bucht1.Bucht.Bucht.ID,
                                                              PortNo = t.bucht1.Bucht.Bucht.SwitchPort.PortNo,
                                                              SwitchPortID = t.bucht1.Bucht.Bucht.SwitchPort.ID,
                                                              SwitchCode = t.bucht1.Bucht.Bucht.SwitchPort.Switch.SwitchCode,
                                                              SwitchID = t.bucht1.Bucht.Bucht.SwitchPort.Switch.ID,
                                                              TelePhoneNo = t.bucht1.Telephone.TelephoneNo,
                                                              SwitchPreCodeID = t.bucht1.Telephone.SwitchPrecodeID,

                                                              ClassTelephone = t.bucht1.Telephone.ClassTelephone == 0 ?
                                                                               string.Empty :
                                                                               Helpers.GetEnumDescription((int)t.bucht1.Telephone.ClassTelephone, typeof(DB.ClassTelephone)),

                                                              PostID = t.bucht1.Bucht.Bucht.PostContact.Post.ID,
                                                              AorBTypeID = t.bucht1.Bucht.Bucht.PostContact.Post.AORBPostAndCabinet.ID,
                                                              AorBType = t.bucht1.Bucht.Bucht.PostContact.Post.AORBPostAndCabinet.Name,
                                                              CabinetID = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.ID,
                                                              BuchtTypeID = t.bucht1.Bucht.Bucht.BuchtTypeID,
                                                              CabinetInputID = t.bucht1.Bucht.Bucht.CabinetInputID,
                                                              CabinetUsageTypeID = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.CabinetUsageType,
                                                              CabinetSwitchID = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.SwitchID,
                                                              isOutBoundCabinet = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.IsOutBound,
                                                              CabinetName = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.CabinetNumber,
                                                              PostName = t.bucht1.Bucht.Bucht.PostContact.Post.Number,
                                                              isOutBoundPost = t.bucht1.Bucht.Bucht.PostContact.Post.IsOutBorder,
                                                              CenterName = t.bucht1.Telephone.Center.CenterName,
                                                              CenterID = t.bucht1.Telephone.Center.ID,
                                                              CityName = t.bucht1.Telephone.Center.Region.City.Name,
                                                              CabinetIDForSearch = t.bucht1.Bucht.Bucht.PostContact.Post.Cabinet.ID,
                                                              CustomerName = (t.bucht1.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.bucht1.Telephone.Customer.LastName ?? string.Empty),
                                                              Customer = t.bucht1.Telephone.Customer,
                                                              TelephoneStatus = t.bucht1.Telephone.Status,
                                                              CauseOfCut = t.bucht1.Telephone.CauseOfCut.Name,
                                                              PostContactType = t.bucht1.Bucht.Bucht.PostContact.ConnectionType,
                                                              MDFID = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                                                              MDFName = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),
                                                              InstallAddress = t.bucht1.Telephone.Address,
                                                              CorrespondenceAddress = t.bucht1.Telephone.Address1,
                                                              Address = t.bucht1.Telephone.Address.AddressContent,
                                                              PostallCode = t.bucht1.Telephone.Address.PostalCode,
                                                              PAPName = t.BuchtADSLPAPPort.PAPInfo.Title,

                                                              ADSLBucht = "ردیف : " + t.BuchtADSLPAPPort.RowNo +
                                                                          " ، طبقه : " + t.BuchtADSLPAPPort.ColumnNo +
                                                                          " ، اتصالی : " + t.BuchtADSLPAPPort.BuchtNo,

                                                              ADSLVerticalRowNo = t.BuchtADSLPAPPort.ColumnNo,
                                                              ADSLVerticalColumnNo = t.BuchtADSLPAPPort.RowNo,
                                                              ADSLBuchtNo = t.BuchtADSLPAPPort.BuchtNo,

                                                              SpecialServices = context.TelephoneSpecialServiceTypes.Where(st => st.TelephoneNo == t.bucht1.Telephone.TelephoneNo).Any() ? //آیا این تلفن سرویس ویژه دارد یا خیر
                                                                                string.Join(" , ", t.bucht1.Telephone.TelephoneSpecialServiceTypes.Select(st => st.SpecialServiceType.Title).ToArray()) :
                                                                                string.Empty,

                                                              RequestPaymentAmountSum = context.Requests
                                                                                               .Where(re =>
                                                                                                           (re.TelephoneNo == t.bucht1.Telephone.TelephoneNo) &&
                                                                                                           (re.RequestTypeID == (int)DB.RequestType.Dayri) &&
                                                                                                           (re.EndDate.HasValue)
                                                                                                      )
                                                                                               .OrderByDescending(re => re.EndDate.Value).Take(1)
                                                                                               .SingleOrDefault()
                                                                                               .RequestPayments
                                                                                               .Where(rp =>
                                                                                                          (rp.IsPaid.HasValue && rp.IsPaid.Value) &&
                                                                                                          (rp.IsKickedBack.HasValue && !rp.IsKickedBack.Value)
                                                                                                      )
                                                                                               .Sum(rp => rp.AmountSum)
                                                          });

                return query.OrderBy(t => t.PostContactID).SingleOrDefault();

            }
        }

        public static List<AssignmentInfo> GetAllInformationByTelephoneNoToTelephone(long fromTelephoneNo, long toTelephoneNo)
        {

            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<AssignmentInfo> query = context.PostContacts
                    .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })

                    // join with the Otherbucht
                    .GroupJoin(context.Buchts, fb => fb.Buchts.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new { Bucht = fb.Buchts, PostContact = fb.PostContacts, OtherBucht = sb })
                    .SelectMany(ob => ob.OtherBucht.DefaultIfEmpty(), (fb, ob) => new { Bucht = fb.Bucht, PostContact = fb.PostContact, OtherBucht = ob })

                    // join with the Telephone
                    .GroupJoin(context.Telephones, b => b.Bucht.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContact })
                    .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })

                    //join with ADSLPAPPort
                    .GroupJoin(context.ADSLPAPPorts, b3 => b3.Telephone.TelephoneNo, p => p.TelephoneNo, (b3, p) => new { Bucht1 = b3, ADSLPAPPort = p })
                    .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (bp, p1) => new { bucht1 = bp.Bucht1, BuchtADSLPAPPort = p1 })

                    .Where(t3 => t3.bucht1.Telephone.TelephoneNo >= fromTelephoneNo &&
                                 t3.bucht1.Telephone.TelephoneNo <= toTelephoneNo &&
                                 t3.bucht1.Bucht.Bucht.PostContact.ConnectionType != (byte)DB.PostContactStatus.Deleted &&
                                 t3.bucht1.Bucht.Bucht.PostContact.Post.IsDelete == false)
                     .Select(t => new AssignmentInfo
                     {
                         BuchtType = t.bucht1.Bucht.Bucht.BuchtTypeID,
                         BuchtStatus = t.bucht1.Bucht.Bucht.Status,
                         MUID = "رک:" + t.bucht1.Bucht.Bucht.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : " + t.bucht1.Bucht.Bucht.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " + "کارت : " + t.bucht1.Bucht.Bucht.PCMPort.PCM.Card.ToString() + " ،  " + "پورت : " + t.bucht1.Bucht.Bucht.PCMPort.PortNumber.ToString(),
                         PCMPortIDInBuchtTable = t.bucht1.Bucht.Bucht.PCMPortID,
                         InputNumber = t.bucht1.Bucht.Bucht.CabinetInput.InputNumber,
                         InputNumberID = t.bucht1.Bucht.Bucht.CabinetInput.ID,
                         PostContact = t.bucht1.Bucht.Bucht.PostContact.ConnectionNo,
                         PostContactID = t.bucht1.Bucht.Bucht.PostContact.ID,
                         PostContactStatus = t.bucht1.Bucht.Bucht.PostContact.Status,
                         Connection = "ردیف : " + t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " ، " + "طبقه : " + t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.bucht1.Bucht.Bucht.BuchtNo,
                         OtherBucht = "ردیف:" + t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه : " + t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.bucht1.Bucht.OtherBucht.BuchtNo,
                         OtherBuchtTypeName = t.bucht1.Bucht.OtherBucht.BuchtType.BuchtTypeName,
                         VerticalColumnNo = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                         VerticalRowNo = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalRowNo,

                         PCMCabinetInputColumnNo = t.bucht1.Bucht.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
                                                   t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo :
                                                   context.Buchts.Where(b =>
                                                                            b.CabinetInputID == t.bucht1.Bucht.Bucht.CabinetInputID && b.BuchtTypeID == (int)DB.BuchtType.CustomerSide
                                                                       )
                                                                 .Select(b => b.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo)
                                                                 .SingleOrDefault(),
                         PCMCabinetInputRowNo = t.bucht1.Bucht.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
                                                t.bucht1.Bucht.OtherBucht.VerticalMDFRow.VerticalRowNo :
                                                context.Buchts.Where(b =>
                                                                         b.CabinetInputID == t.bucht1.Bucht.Bucht.CabinetInputID && b.BuchtTypeID == (int)DB.BuchtType.CustomerSide
                                                                    )
                                                              .Select(b => b.VerticalMDFRow.VerticalRowNo)
                                                              .SingleOrDefault(),
                         PCMCabinetInputBuchtNo = t.bucht1.Bucht.Bucht.BuchtTypeID != (int)DB.BuchtType.InLine ?
                                                  t.bucht1.Bucht.OtherBucht.BuchtNo :
                                                  context.Buchts.Where(b =>
                                                                           b.CabinetInputID == t.bucht1.Bucht.Bucht.CabinetInputID && b.BuchtTypeID == (int)DB.BuchtType.CustomerSide
                                                                      )
                                                                .Select(b => b.BuchtNo)
                                                                .SingleOrDefault(),
                         BuchtNo = t.bucht1.Bucht.Bucht.BuchtNo,
                         BuchtID = t.bucht1.Bucht.Bucht.ID,
                         PortNo = t.bucht1.Bucht.Bucht.SwitchPort.PortNo,
                         SwitchPortID = t.bucht1.Bucht.Bucht.SwitchPort.ID,
                         SwitchCode = t.bucht1.Bucht.Bucht.SwitchPort.Switch.SwitchCode,
                         SwitchID = t.bucht1.Bucht.Bucht.SwitchPort.Switch.ID,
                         TelePhoneNo = t.bucht1.Telephone.TelephoneNo,
                         SwitchPreCodeID = t.bucht1.Telephone.SwitchPrecodeID,
                         ClassTelephone = t.bucht1.Telephone.ClassTelephone == 0 ? string.Empty : Helpers.GetEnumDescription((int)t.bucht1.Telephone.ClassTelephone, typeof(DB.ClassTelephone)),
                         PostID = t.bucht1.Bucht.Bucht.PostContact.Post.ID,
                         AorBTypeID = t.bucht1.Bucht.Bucht.PostContact.Post.AORBPostAndCabinet.ID,
                         AorBType = t.bucht1.Bucht.Bucht.PostContact.Post.AORBPostAndCabinet.Name,
                         CabinetID = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.ID,
                         BuchtTypeID = t.bucht1.Bucht.Bucht.BuchtTypeID,
                         CabinetInputID = t.bucht1.Bucht.Bucht.CabinetInputID,
                         CabinetUsageTypeID = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.CabinetUsageType,
                         CabinetSwitchID = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.SwitchID,
                         isOutBoundCabinet = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.IsOutBound,
                         CabinetName = t.bucht1.Bucht.Bucht.CabinetInput.Cabinet.CabinetNumber,
                         PostName = t.bucht1.Bucht.Bucht.PostContact.Post.Number,
                         isOutBoundPost = t.bucht1.Bucht.Bucht.PostContact.Post.IsOutBorder,
                         CenterName = t.bucht1.Telephone.Center.CenterName,
                         CenterID = t.bucht1.Telephone.Center.ID,
                         CityName = t.bucht1.Telephone.Center.Region.City.Name,
                         CabinetIDForSearch = t.bucht1.Bucht.Bucht.PostContact.Post.Cabinet.ID,
                         CustomerName = (t.bucht1.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.bucht1.Telephone.Customer.LastName ?? string.Empty),
                         Customer = t.bucht1.Telephone.Customer,
                         TelephoneStatus = t.bucht1.Telephone.Status,
                         CauseOfCut = t.bucht1.Telephone.CauseOfCut.Name,
                         PostContactType = t.bucht1.Bucht.Bucht.PostContact.ConnectionType,
                         MDFID = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                         MDFName = t.bucht1.Bucht.Bucht.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),
                         InstallAddress = t.bucht1.Telephone.Address,
                         CorrespondenceAddress = t.bucht1.Telephone.Address1,
                         Address = t.bucht1.Telephone.Address.AddressContent,
                         PostallCode = t.bucht1.Telephone.Address.PostalCode,
                         PAPName = t.BuchtADSLPAPPort.PAPInfo.Title,
                         ADSLBucht = "ردیف : " + t.BuchtADSLPAPPort.RowNo + " ، طبقه : " + t.BuchtADSLPAPPort.ColumnNo + " ، اتصالی : " + t.BuchtADSLPAPPort.BuchtNo,
                         ADSLVerticalRowNo = t.BuchtADSLPAPPort.RowNo,
                         ADSLVerticalColumnNo = t.BuchtADSLPAPPort.ColumnNo,
                         ADSLBuchtNo = t.BuchtADSLPAPPort.BuchtNo,
                         SpecialServices = context.TelephoneSpecialServiceTypes.Where(st => st.TelephoneNo == t.bucht1.Telephone.TelephoneNo).Any() ? string.Join(" , ", t.bucht1.Telephone.TelephoneSpecialServiceTypes.Select(st => st.SpecialServiceType.Title).ToArray()) : string.Empty,
                         RequestPaymentAmountSum = context.Requests
                                                          .Where(re =>
                                                                      (re.TelephoneNo == t.bucht1.Telephone.TelephoneNo) &&
                                                                      (re.RequestTypeID == (int)DB.RequestType.Dayri) &&
                                                                      (re.EndDate.HasValue)
                                                                 )
                                                          .OrderByDescending(re => re.EndDate.Value).Take(1)
                                                          .SingleOrDefault()
                                                          .RequestPayments
                                                          .Where(rp =>
                                                                     (rp.IsPaid.HasValue && rp.IsPaid.Value) &&
                                                                     (rp.IsKickedBack.HasValue && !rp.IsKickedBack.Value)
                                                                 )
                                                          .Sum(rp => rp.AmountSum)
                     });

                return query.OrderBy(t => t.TelePhoneNo).ToList();

            }
        }

        //milad doran
        //public static AssignmentInfo GetGeneralInformationByTelephoneNo(long telephoneNo)
        //{

        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        IQueryable<AssignmentInfo> query = context.Buchts
        //            // join with PostContect
        //         .GroupJoin(context.PostContacts, b1 => b1.ConnectionID, pc => pc.ID, (bu, pc) => new { bucht = bu, PostContect = pc })
        //         .SelectMany(t2 => t2.PostContect.DefaultIfEmpty(), (BuchtPostcontect, t2) => new { PostContect = t2, BuchtPostContect = BuchtPostcontect.bucht })

        //         .GroupJoin(context.CabinetInputs, b2 => b2.BuchtPostContect.CabinetInputID, ci => ci.ID, (BuchtPostContect, ci) => new { BuchtPostContect = BuchtPostContect, CabinetInput = ci })
        //         .SelectMany(t3 => t3.CabinetInput.DefaultIfEmpty(), (BuchtPostContect, t3) => new { BuchtPostContect = BuchtPostContect.BuchtPostContect, CabinetInput = t3 })

        //         // join with the Otherbucht
        //         .GroupJoin(context.Buchts, fb => fb.BuchtPostContect.BuchtPostContect.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = sb })
        //         .SelectMany(ob => ob.OtherBucht.DefaultIfEmpty(), (fb, ob) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = ob })

        //         //join with ADSLPAPPort
        //         .GroupJoin(context.ADSLPAPPorts, b3 => b3.BuchtPostContect.BuchtPostContect.TelephoneNo, p => p.TelephoneNo, (b3, p) => new { Bucht1 = b3, ADSLPAPPort = p })
        //         .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (bp, p1) => new { bucht1 = bp.Bucht1, BuchtADSLPAPPort = p1 })


        //         .Where(t3 => (t3.bucht1.BuchtPostContect.BuchtPostContect.TelephoneNo == telephoneNo) &&
        //                      (t3.bucht1.BuchtPostContect.PostContect == null || t3.bucht1.BuchtPostContect.PostContect.ConnectionType != (byte)DB.PostContactStatus.Deleted) &&
        //                      (t3.bucht1.BuchtPostContect.PostContect == null || t3.bucht1.BuchtPostContect.PostContect.Post.IsDelete == false)
        //               )
        //          .OrderBy(t => t.bucht1.BuchtPostContect.BuchtPostContect.CabinetInput.InputNumber)
        //          .Select(t => new AssignmentInfo
        //          {
        //              BuchtType = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTypeID,
        //              BuchtStatus = t.bucht1.BuchtPostContect.BuchtPostContect.Status,
        //              MUID = "رک:" + t.bucht1.BuchtPostContect.BuchtPostContect.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : " + t.bucht1.BuchtPostContect.BuchtPostContect.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " + "کارت : " + t.bucht1.BuchtPostContect.BuchtPostContect.PCMPort.PCM.Card.ToString() + " ،  " + "پورت : " + t.bucht1.BuchtPostContect.BuchtPostContect.PCMPort.PortNumber.ToString(),
        //              PCMPortIDInBuchtTable = t.bucht1.BuchtPostContect.BuchtPostContect.PCMPortID,
        //              InputNumber = t.bucht1.BuchtPostContect.BuchtPostContect.CabinetInput.InputNumber,
        //              InputNumberID = t.bucht1.BuchtPostContect.BuchtPostContect.CabinetInput.ID,
        //              PostContact = t.bucht1.BuchtPostContect.BuchtPostContect.PostContact.ConnectionNo,
        //              PostContactID = t.bucht1.BuchtPostContect.BuchtPostContect.PostContact.ID,
        //              PostContactStatus = t.bucht1.BuchtPostContect.BuchtPostContect.PostContact.Status,
        //              Connection = "ردیف : " + t.bucht1.BuchtPostContect.BuchtPostContect.ColumnNo + " ، " + "طبقه : " + t.bucht1.BuchtPostContect.BuchtPostContect.RowNo + " ،  " + "اتصالی : " + t.bucht1.BuchtPostContect.BuchtPostContect.BuchtNo,
        //              OtherBucht = "ردیف:" + t.bucht1.OtherBucht.ColumnNo + " " + "طبقه : " + t.bucht1.OtherBucht.RowNo + " ،  " + "اتصالی : " + t.bucht1.OtherBucht.BuchtNo,
        //              OtherVerticalColumnNo = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTypeID == (int)DB.BuchtType.CustomerSide ? t.bucht1.BuchtPostContect.BuchtPostContect.Bucht1.ColumnNo : t.bucht1.BuchtPostContect.BuchtPostContect.Bucht2.ColumnNo,
        //              OtherVerticalRowNo = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTypeID == (int)DB.BuchtType.CustomerSide ? t.bucht1.BuchtPostContect.BuchtPostContect.Bucht1.RowNo : t.bucht1.BuchtPostContect.BuchtPostContect.Bucht2.RowNo,
        //              OtherBuchtNo = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTypeID == (int)DB.BuchtType.CustomerSide ? t.bucht1.BuchtPostContect.BuchtPostContect.Bucht1.BuchtNo : t.bucht1.BuchtPostContect.BuchtPostContect.Bucht2.BuchtNo,
        //              OtherBuchtTypeName = t.bucht1.OtherBucht.BuchtType.BuchtTypeName,
        //              BuchtID = t.bucht1.BuchtPostContect.BuchtPostContect.ID,
        //              PortNo = t.bucht1.BuchtPostContect.BuchtPostContect.SwitchPort.PortNo,
        //              SwitchPortID = t.bucht1.BuchtPostContect.BuchtPostContect.SwitchPort.ID,
        //              SwitchCode = t.bucht1.BuchtPostContect.BuchtPostContect.SwitchPort.Switch.SwitchCode,
        //              SwitchID = t.bucht1.BuchtPostContect.BuchtPostContect.SwitchPort.Switch.ID,
        //              TelePhoneNo = t.bucht1.BuchtPostContect.BuchtPostContect.TelephoneNo,
        //              ClassTelephone = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.ClassTelephone == 0 ? string.Empty : Helpers.GetEnumDescription((int)t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.ClassTelephone, typeof(DB.ClassTelephone)),
        //              PostID = t.bucht1.BuchtPostContect.PostContect.Post.ID,
        //              AorBTypeID = t.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.ID,
        //              AorBType = t.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.Name,
        //              CabinetID = t.bucht1.CabinetInput.Cabinet.ID,
        //              BuchtTypeID = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTypeID,
        //              CabinetInputID = t.bucht1.CabinetInput.ID,
        //              CabinetUsageTypeID = t.bucht1.CabinetInput.Cabinet.CabinetUsageType,
        //              CabinetSwitchID = t.bucht1.CabinetInput.Cabinet.SwitchID,
        //              isOutBoundCabinet = t.bucht1.CabinetInput.Cabinet.IsOutBound,
        //              CabinetName = t.bucht1.CabinetInput.Cabinet.CabinetNumber,
        //              PostName = t.bucht1.BuchtPostContect.PostContect.Post.Number,
        //              isOutBoundPost = t.bucht1.BuchtPostContect.PostContect.Post.IsOutBorder,
        //              CenterName = t.bucht1.BuchtPostContect.BuchtPostContect.Center,
        //              CenterID = (int)t.bucht1.BuchtPostContect.BuchtPostContect.CenterID,
        //              CityName = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Center.Region.City.Name,
        //              CabinetIDForSearch = t.bucht1.BuchtPostContect.PostContect.Post.Cabinet.ID,
        //              CustomerName = (t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " + (t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.LastName ?? string.Empty),
        //              Customer = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer,
        //              TelephoneStatus = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Status,
        //              CauseOfCut = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CauseOfCut.Name,
        //              PostContactType = t.bucht1.BuchtPostContect.PostContect.ConnectionType,
        //              MDFID = t.bucht1.BuchtPostContect.BuchtPostContect.MDFID,
        //              MDFName = t.bucht1.BuchtPostContect.BuchtPostContect.MDF,
        //              InstallAddress = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address,
        //              CorrespondenceAddress = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address1,
        //              Address = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address.AddressContent,
        //              PostallCode = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address.PostalCode,
        //              PAPName = t.BuchtADSLPAPPort.PAPInfo.Title,
        //              ADSLBucht = "ردیف : " + t.BuchtADSLPAPPort.RowNo + " ، طبقه : " + t.BuchtADSLPAPPort.ColumnNo + " ، اتصالی : " + t.BuchtADSLPAPPort.BuchtNo,
        //              SpecialServices = context.TelephoneSpecialServiceTypes.Where(st => st.TelephoneNo == t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo).Any() ? string.Join(" , ", t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneSpecialServiceTypes.Select(st => st.SpecialServiceType.Title).ToArray()) : string.Empty,
        //              //RequestPaymentAmountSum = context.Requests
        //              //                                 .Where(re =>
        //              //                                             (re.TelephoneNo == t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo) &&
        //              //                                             (re.RequestTypeID == (int)DB.RequestType.Dayri) &&
        //              //                                             (re.EndDate.HasValue)
        //              //                                        )
        //              //                                 .OrderByDescending(re => re.EndDate.Value).Take(1)
        //              //                                 .SingleOrDefault()
        //              //                                 .RequestPayments
        //              //                                 .Where(rp =>
        //              //                                            (rp.IsPaid.HasValue && rp.IsPaid.Value) &&
        //              //                                            (rp.IsKickedBack.HasValue && !rp.IsKickedBack.Value)
        //              //                                        )
        //              //                                 .Sum(rp => rp.AmountSum)
        //          });

        //        if (query.Any(t => t.BuchtTypeID == (int)DB.BuchtType.CustomerSide))
        //        {
        //            return query.Where(t => t.BuchtTypeID == (int)DB.BuchtType.CustomerSide).Take(1).SingleOrDefault();
        //        }
        //        else
        //        {
        //            return query.Take(1).SingleOrDefault();
        //        }

        //TODO:rad 13950324
        public static AssignmentInfo GetGeneralInformationByTelephoneNo(long telephoneNo)
        {

            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<AssignmentInfo> query = context.Buchts

                                                           //join with PostContect
                                                          .GroupJoin(context.PostContacts, b1 => b1.ConnectionID, pc => pc.ID, (bu, pc) => new { bucht = bu, PostContect = pc })
                                                          .SelectMany(t2 => t2.PostContect.DefaultIfEmpty(), (BuchtPostcontect, t2) => new { PostContect = t2, BuchtPostContect = BuchtPostcontect.bucht })

                                                            //join with CabinetInput
                                                          .GroupJoin(context.CabinetInputs, b2 => b2.BuchtPostContect.CabinetInputID, ci => ci.ID, (BuchtPostContect, ci) => new { BuchtPostContect = BuchtPostContect, CabinetInput = ci })
                                                          .SelectMany(t3 => t3.CabinetInput.DefaultIfEmpty(), (BuchtPostContect, t3) => new { BuchtPostContect = BuchtPostContect.BuchtPostContect, CabinetInput = t3 })

                                                          //join with the Otherbucht
                                                          .GroupJoin(context.Buchts, fb => fb.BuchtPostContect.BuchtPostContect.BuchtIDConnectedOtherBucht, sb => sb.ID, (fb, sb) => new
                                                                                                                                                                                    {
                                                                                                                                                                                        BuchtPostContect = fb.BuchtPostContect,
                                                                                                                                                                                        CabinetInput = fb.CabinetInput,
                                                                                                                                                                                        OtherBucht = sb
                                                                                                                                                                                    }
                                                                     )
                                                          .SelectMany(ob => ob.OtherBucht.DefaultIfEmpty(), (fb, ob) => new { BuchtPostContect = fb.BuchtPostContect, CabinetInput = fb.CabinetInput, OtherBucht = ob })

                                                          //join with ADSLPAPPort
                                                          .GroupJoin(context.ADSLPAPPorts, b3 => b3.BuchtPostContect.BuchtPostContect.TelephoneNo, p => p.TelephoneNo, (b3, p) => new { Bucht1 = b3, ADSLPAPPort = p })
                                                          .SelectMany(p1 => p1.ADSLPAPPort.DefaultIfEmpty(), (bp, p1) => new { bucht1 = bp.Bucht1, BuchtADSLPAPPort = p1 })

                                                          .Where(t3 =>
                                                                       (t3.bucht1.BuchtPostContect.BuchtPostContect.TelephoneNo == telephoneNo) &&
                                                                       (t3.bucht1.BuchtPostContect.PostContect == null || t3.bucht1.BuchtPostContect.PostContect.ConnectionType != (byte)DB.PostContactStatus.Deleted) &&
                                                                       (t3.bucht1.BuchtPostContect.PostContect == null || t3.bucht1.BuchtPostContect.PostContect.Post.IsDelete == false)
                                                                )

                                                          .OrderBy(t => t.bucht1.BuchtPostContect.BuchtPostContect.CabinetInput.InputNumber)
                                                          .Select(t => new AssignmentInfo
                                                          {
                                                              BuchtType = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTypeID,
                                                              BuchtStatus = t.bucht1.BuchtPostContect.BuchtPostContect.Status,

                                                              MUID = "رک : " + t.bucht1.BuchtPostContect.BuchtPostContect.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " +
                                                                     "شلف : " + t.bucht1.BuchtPostContect.BuchtPostContect.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " +
                                                                     "کارت : " + t.bucht1.BuchtPostContect.BuchtPostContect.PCMPort.PCM.Card.ToString() + " ،  " +
                                                                     "پورت : " + t.bucht1.BuchtPostContect.BuchtPostContect.PCMPort.PortNumber.ToString(),

                                                              PCMPortIDInBuchtTable = t.bucht1.BuchtPostContect.BuchtPostContect.PCMPortID,
                                                              InputNumber = t.bucht1.BuchtPostContect.BuchtPostContect.CabinetInput.InputNumber,
                                                              InputNumberID = t.bucht1.BuchtPostContect.BuchtPostContect.CabinetInput.ID,
                                                              PostContact = t.bucht1.BuchtPostContect.BuchtPostContect.PostContact.ConnectionNo,
                                                              PostContactID = t.bucht1.BuchtPostContect.BuchtPostContect.PostContact.ID,
                                                              PostContactStatus = t.bucht1.BuchtPostContect.BuchtPostContect.PostContact.Status,

                                                              Connection = "ردیف : " + t.bucht1.BuchtPostContect.BuchtPostContect.ColumnNo + " ، " +
                                                                           "طبقه : " + t.bucht1.BuchtPostContect.BuchtPostContect.RowNo + " ،  " +
                                                                           "اتصالی : " + t.bucht1.BuchtPostContect.BuchtPostContect.BuchtNo,

                                                              OtherBucht = "ردیف : " + t.bucht1.OtherBucht.ColumnNo + " " +
                                                                           "طبقه : " + t.bucht1.OtherBucht.RowNo + " ،  " +
                                                                           "اتصالی : " + t.bucht1.OtherBucht.BuchtNo,

                                                              OtherVerticalColumnNo = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTypeID == (int)DB.BuchtType.CustomerSide ?
                                                                                      t.bucht1.BuchtPostContect.BuchtPostContect.Bucht1.ColumnNo :
                                                                                      t.bucht1.BuchtPostContect.BuchtPostContect.Bucht2.ColumnNo,

                                                              OtherVerticalRowNo = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTypeID == (int)DB.BuchtType.CustomerSide ?
                                                                                   t.bucht1.BuchtPostContect.BuchtPostContect.Bucht1.RowNo :
                                                                                   t.bucht1.BuchtPostContect.BuchtPostContect.Bucht2.RowNo,

                                                              OtherBuchtNo = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTypeID == (int)DB.BuchtType.CustomerSide ?
                                                                             t.bucht1.BuchtPostContect.BuchtPostContect.Bucht1.BuchtNo :
                                                                             t.bucht1.BuchtPostContect.BuchtPostContect.Bucht2.BuchtNo,

                                                              OtherBuchtTypeName = t.bucht1.OtherBucht.BuchtType.BuchtTypeName,
                                                              BuchtID = t.bucht1.BuchtPostContect.BuchtPostContect.ID,
                                                              PortNo = t.bucht1.BuchtPostContect.BuchtPostContect.SwitchPort.PortNo,
                                                              SwitchPortID = t.bucht1.BuchtPostContect.BuchtPostContect.SwitchPort.ID,
                                                              SwitchCode = t.bucht1.BuchtPostContect.BuchtPostContect.SwitchPort.Switch.SwitchCode,
                                                              SwitchID = t.bucht1.BuchtPostContect.BuchtPostContect.SwitchPort.Switch.ID,
                                                              TelePhoneNo = t.bucht1.BuchtPostContect.BuchtPostContect.TelephoneNo,

                                                              ClassTelephone = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.ClassTelephone == 0 ?
                                                                               string.Empty :
                                                                               Helpers.GetEnumDescription((int)t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.ClassTelephone, typeof(DB.ClassTelephone)),

                                                              PostID = t.bucht1.BuchtPostContect.PostContect.Post.ID,
                                                              AorBTypeID = t.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.ID,
                                                              AorBType = t.bucht1.BuchtPostContect.PostContect.Post.AORBPostAndCabinet.Name,
                                                              CabinetID = t.bucht1.CabinetInput.Cabinet.ID,
                                                              BuchtTypeID = t.bucht1.BuchtPostContect.BuchtPostContect.BuchtTypeID,
                                                              CabinetInputID = t.bucht1.CabinetInput.ID,
                                                              CabinetUsageTypeID = t.bucht1.CabinetInput.Cabinet.CabinetUsageType,
                                                              CabinetSwitchID = t.bucht1.CabinetInput.Cabinet.SwitchID,
                                                              isOutBoundCabinet = t.bucht1.CabinetInput.Cabinet.IsOutBound,
                                                              CabinetName = t.bucht1.CabinetInput.Cabinet.CabinetNumber,
                                                              PostName = t.bucht1.BuchtPostContect.PostContect.Post.Number,
                                                              isOutBoundPost = t.bucht1.BuchtPostContect.PostContect.Post.IsOutBorder,
                                                              CenterName = t.bucht1.BuchtPostContect.BuchtPostContect.Center,
                                                              CenterID = (int)t.bucht1.BuchtPostContect.BuchtPostContect.CenterID,
                                                              CityName = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Center.Region.City.Name,
                                                              CabinetIDForSearch = t.bucht1.BuchtPostContect.PostContect.Post.Cabinet.ID,

                                                              CustomerName = (t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.FirstNameOrTitle ?? string.Empty) + " " +
                                                                             (t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer.LastName ?? string.Empty),

                                                              Customer = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Customer,
                                                              TelephoneStatus = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Status,
                                                              CauseOfCut = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.CauseOfCut.Name,
                                                              PostContactType = t.bucht1.BuchtPostContect.PostContect.ConnectionType,
                                                              MDFID = t.bucht1.BuchtPostContect.BuchtPostContect.MDFID,
                                                              MDFName = t.bucht1.BuchtPostContect.BuchtPostContect.MDF,
                                                              InstallAddress = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address,
                                                              CorrespondenceAddress = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address1,
                                                              Address = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address.AddressContent,
                                                              PostallCode = t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.Address.PostalCode,
                                                              PAPName = t.BuchtADSLPAPPort.PAPInfo.Title,

                                                              ADSLBucht = "ردیف : " + t.BuchtADSLPAPPort.RowNo +
                                                                          " ، طبقه : " + t.BuchtADSLPAPPort.ColumnNo +
                                                                          " ، اتصالی : " + t.BuchtADSLPAPPort.BuchtNo,

                                                              SpecialServices = context.TelephoneSpecialServiceTypes.Where(st => st.TelephoneNo == t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo).Any() ? //آیا این تلفن سرویس ویژه دارد
                                                                                string.Join(" , ", t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneSpecialServiceTypes.Select(st => st.SpecialServiceType.Title).ToArray()) :
                                                                                string.Empty,

                                                              //RequestPaymentAmountSum = context.Requests
                                                              //                                 .Where(re =>
                                                              //                                             (re.TelephoneNo == t.bucht1.BuchtPostContect.BuchtPostContect.Telephone.TelephoneNo) &&
                                                              //                                             (re.RequestTypeID == (int)DB.RequestType.Dayri) &&
                                                              //                                             (re.EndDate.HasValue)
                                                              //                                        )
                                                              //                                 .OrderByDescending(re => re.EndDate.Value).Take(1)
                                                              //                                 .SingleOrDefault()
                                                              //                                 .RequestPayments
                                                              //                                 .Where(rp =>
                                                              //                                            (rp.IsPaid.HasValue && rp.IsPaid.Value) &&
                                                              //                                            (rp.IsKickedBack.HasValue && !rp.IsKickedBack.Value)
                                                              //                                        )
                                                              //                                 .Sum(rp => rp.AmountSum)
                                                          });

                if (query.Any(t => t.BuchtTypeID == (int)DB.BuchtType.CustomerSide))
                {
                    return query.Where(t => t.BuchtTypeID == (int)DB.BuchtType.CustomerSide).Take(1).SingleOrDefault();
                }
                else
                {
                    return query.Take(1).SingleOrDefault();
                }
            }
        }

        //    }
        //}

        //milad doran
        //public static AssignmentInfo GetAllInformationByBuchtID(long buchtID)
        //{

        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        IQueryable<AssignmentInfo> query = context.PostContacts
        //           .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
        //           .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })
        //           .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts })
        //           .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })
        //           .Where(t3 => t3.Bucht.Buchts.ID == buchtID && t3.Bucht.Buchts.PostContact.Post.IsDelete == false)
        //            .Select(t => new AssignmentInfo
        //            {
        //                BuchtType = t.Bucht.Buchts.BuchtTypeID,
        //                BuchtStatus = t.Bucht.Buchts.Status,
        //                MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : " + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " + "کارت : " + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " ،  " + "پورت : " + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),
        //                PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
        //                InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
        //                CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
        //                PostName = t.Bucht.Buchts.PostContact.Post.Number,
        //                InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
        //                PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
        //                PostContactID = t.Bucht.Buchts.PostContact.ID,
        //                PostContactStatus = t.Bucht.Buchts.PostContact.Status,
        //                Connection = "ردیف : " + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " ، " + "طبقه : " + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.Bucht.Buchts.BuchtNo,
        //                BuchtID = t.Bucht.Buchts.ID,
        //                PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
        //                SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
        //                SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
        //                SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
        //                TelePhoneNo = t.Telephone.TelephoneNo,
        //                PostID = t.Bucht.Buchts.PostContact.Post.ID,
        //                CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
        //                CabinetUsageTypeID = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetUsageType,
        //                CabinetSwitchID = t.Bucht.Buchts.CabinetInput.Cabinet.SwitchID,
        //                //CabinetSwitchID = t.Bucht.Buchts.CabinetInput.Cabinet.SwitchID,
        //                CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
        //                CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
        //                Customer = t.Telephone.Customer,
        //                TelephoneStatus = t.Telephone.Status,
        //                PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
        //                MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
        //                MDFName = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),

        //                CenterID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
        //            });
        //        return query.OrderBy(t => t.PostContactID).SingleOrDefault();

        //    }
        //}

        //TODO:rad 13950301
        public static AssignmentInfo GetAllInformationByBuchtID(long buchtID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                AssignmentInfo result;
                IQueryable<AssignmentInfo> query = context.PostContacts
                                                          .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p }) //join with Bucht
                                                          .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })

                                                          .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts }) //join with Telephone
                                                          .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })

                                                          .Where(t3 =>
                                                                        t3.Bucht.Buchts.ID == buchtID &&
                                                                        t3.Bucht.Buchts.PostContact.Post.IsDelete == false
                                                                )
                                                           .Select(t => new AssignmentInfo
                                                                        {
                                                                            BuchtType = t.Bucht.Buchts.BuchtTypeID,
                                                                            BuchtStatus = t.Bucht.Buchts.Status,

                                                                            MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " +
                                                                                   "شلف : " + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " +
                                                                                   "کارت : " + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " ،  " +
                                                                                   "پورت : " + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),

                                                                            PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                                                                            InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                                                                            CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                                                                            PostName = t.Bucht.Buchts.PostContact.Post.Number,
                                                                            InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                                                                            PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                                                                            PostContactID = t.Bucht.Buchts.PostContact.ID,
                                                                            PostContactStatus = t.Bucht.Buchts.PostContact.Status,

                                                                            Connection = "ردیف : " + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " ، " +
                                                                                         "طبقه : " + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " ،  " +
                                                                                         "اتصالی : " + t.Bucht.Buchts.BuchtNo,

                                                                            BuchtID = t.Bucht.Buchts.ID,
                                                                            PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                                                                            SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                                                                            SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                                                                            SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                                                                            TelePhoneNo = t.Telephone.TelephoneNo,
                                                                            PostID = t.Bucht.Buchts.PostContact.Post.ID,
                                                                            CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                                                                            CabinetUsageTypeID = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetUsageType,
                                                                            CabinetSwitchID = t.Bucht.Buchts.CabinetInput.Cabinet.SwitchID,
                                                                            //CabinetSwitchID = t.Bucht.Buchts.CabinetInput.Cabinet.SwitchID,
                                                                            CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
                                                                            CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                                                                            Customer = t.Telephone.Customer,
                                                                            TelephoneStatus = t.Telephone.Status,
                                                                            PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                                                                            MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                                                                            MDFName = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),
                                                                            CenterID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
                                                                        }
                                                                  );
                result = query.OrderBy(t => t.PostContactID)
                              .SingleOrDefault();

                return result;
            }
        }

        public static AssignmentInfo GetTechInfoByBuchtID(long buchtID)
        {

            using (MainDataContext context = new MainDataContext())
            {
                IQueryable<AssignmentInfo> query = context.Buchts
                   .GroupJoin(context.PostContacts, b => b.ConnectionID, p => p.ID, (b, p) => new { Bucht = b, PostContact = p })
                   .SelectMany(t2 => t2.PostContact.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t2, Buchts = t1.Bucht })
                   .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts })
                   .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })
                   .Where(t3 => (t3.Bucht.Buchts.ID == buchtID) && (!t3.Bucht.Buchts.ConnectionID.HasValue || t3.Bucht.PostContacts.Post.IsDelete == false))

                    .Select(t => new AssignmentInfo
                    {
                        BuchtType = t.Bucht.Buchts.BuchtTypeID,
                        BuchtStatus = t.Bucht.Buchts.Status,
                        MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " ،  " + "شلف : " + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " ،  " + "کارت : " + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " ،  " + "پورت : " + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),
                        PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                        InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                        CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                        PostName = t.Bucht.Buchts.PostContact.Post.Number,
                        InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                        PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                        PostContactID = t.Bucht.Buchts.PostContact.ID,
                        PostContactStatus = t.Bucht.Buchts.PostContact.Status,
                        Connection = "ردیف : " + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " ، " + "طبقه : " + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " ،  " + "اتصالی : " + t.Bucht.Buchts.BuchtNo,
                        BuchtID = t.Bucht.Buchts.ID,
                        PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                        SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                        SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                        SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                        TelePhoneNo = t.Telephone.TelephoneNo,
                        PostID = t.Bucht.Buchts.PostContact.Post.ID,
                        CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                        CabinetUsageTypeID = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetUsageType,
                        CabinetSwitchID = t.Bucht.Buchts.CabinetInput.Cabinet.SwitchID,
                        //CabinetSwitchID = t.Bucht.Buchts.CabinetInput.Cabinet.SwitchID,
                        CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
                        CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                        Customer = t.Telephone.Customer,
                        TelephoneStatus = t.Telephone.Status,
                        PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                        MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                        MDFName = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Number.ToString(),

                        CenterID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID,
                    });
                return query.OrderBy(t => t.PostContactID).SingleOrDefault();

            }
        }

        public static List<AssignmentInfo> GetAllInformationPostByPostID(int postID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.PostContacts
                    .Where(j => j.PostID == postID && j.Post.IsDelete == false)
                    .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })
                    .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts })
                    .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })
                    // .Where(t => DB.CurrentUser.CenterIDs.Contains(t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID))
                   .OrderBy(t => t.Bucht.Buchts.PostContact.ConnectionNo)
                     .Select(t => new AssignmentInfo
                     {
                         BuchtType = t.Bucht.Buchts.BuchtTypeID,
                         BuchtStatus = t.Bucht.Buchts.Status,
                         MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " " + "شلف:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " " + "کارت:" + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " " + "پورت:" + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),
                         PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                         InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                         InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                         PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                         PostContactID = t.Bucht.Buchts.PostContact.ID,
                         PostContactStatus = t.Bucht.Buchts.PostContact.Status,
                         Connection = "ردیف:" + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t.Bucht.Buchts.BuchtNo,
                         BuchtID = t.Bucht.Buchts.ID,
                         PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                         SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                         SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                         SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                         TelePhoneNo = t.Telephone.TelephoneNo,
                         PostID = t.Bucht.Buchts.PostContact.Post.ID,
                         CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                         PostName = t.Bucht.Buchts.PostContact.Post.Number,
                         CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                         CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
                         CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                         TelephoneStatus = t.Telephone.Status,
                         PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                         MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,

                     }).OrderBy(t => t.PostContactID).ToList();

                return query;
            }
        }

        public static AssignmentInfo GetAllInformationPostContactID(long postContactID, byte BuchtType = (byte)DB.BuchtType.CustomerSide)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.PostContacts
                    .Where(j => j.ID == postContactID && j.Post.IsDelete == false)
                    .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })
                    .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts })
                    .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })
                    .Where(t => t.Bucht.Buchts.BuchtTypeID == BuchtType)
                     .Select(t => new AssignmentInfo
                     {
                         BuchtType = t.Bucht.Buchts.BuchtTypeID,
                         BuchtStatus = t.Bucht.Buchts.Status,
                         MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " " + "شلف:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " " + "کارت:" + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " " + "پورت:" + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),
                         PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                         InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                         InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                         PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                         PostContactID = t.Bucht.Buchts.PostContact.ID,
                         PostContactStatus = t.Bucht.Buchts.PostContact.Status,
                         Connection = "ردیف:" + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t.Bucht.Buchts.BuchtNo,
                         BuchtID = t.Bucht.Buchts.ID,
                         PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                         SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                         SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                         SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                         PostID = t.Bucht.Buchts.PostContact.Post.ID,
                         CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                         PostName = t.Bucht.Buchts.PostContact.Post.Number,
                         CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                         CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
                         CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                         Customer = t.Telephone.Customer,
                         TelePhoneNo = t.Telephone.TelephoneNo,
                         TelephoneStatus = t.Telephone.Status,
                         InstallAddress = t.Telephone.Address,
                         CorrespondenceAddress = t.Telephone.Address1,
                         PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                         MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,

                     });

                return query.SingleOrDefault();
            }
        }


        public static AssignmentInfo GetAllInformationWithoutPCMByPostContactID(long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.PostContacts
                    .Where(j => j.ID == postContactID && j.Post.IsDelete == false)
                    .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })
                    .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts })
                    .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })
                    .Where(t => !(t.Bucht.Buchts.BuchtTypeID == (int)DB.BuchtType.InLine || t.Bucht.Buchts.BuchtTypeID == (int)DB.BuchtType.OutLine))
                     .Select(t => new AssignmentInfo
                     {
                         BuchtType = t.Bucht.Buchts.BuchtTypeID,
                         BuchtStatus = t.Bucht.Buchts.Status,
                         MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " " + "شلف:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " " + "کارت:" + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " " + "پورت:" + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),
                         PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                         InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                         InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                         PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                         PostContactID = t.Bucht.Buchts.PostContact.ID,
                         PostContactStatus = t.Bucht.Buchts.PostContact.Status,
                         Connection = "ردیف:" + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t.Bucht.Buchts.BuchtNo,
                         BuchtID = t.Bucht.Buchts.ID,
                         PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                         SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                         SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                         SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                         PostID = t.Bucht.Buchts.PostContact.Post.ID,
                         CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                         PostName = t.Bucht.Buchts.PostContact.Post.Number,
                         CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                         CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
                         CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                         Customer = t.Telephone.Customer,
                         TelePhoneNo = t.Telephone.TelephoneNo,
                         TelephoneStatus = t.Telephone.Status,
                         InstallAddress = t.Telephone.Address,
                         CorrespondenceAddress = t.Telephone.Address1,
                         PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                         MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,

                     });

                return query.SingleOrDefault();
            }
        }

        public static List<AssignmentInfo> GetAllInformationPostContactIDs(List<long> postContactIDs)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.PostContacts
                    .Where(j => postContactIDs.Contains(j.ID) && j.Post.IsDelete == false)
                    .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })
                    .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts })
                    .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })
                     .Select(t => new AssignmentInfo
                     {
                         BuchtType = t.Bucht.Buchts.BuchtTypeID,
                         BuchtStatus = t.Bucht.Buchts.Status,
                         MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " " + "شلف:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " " + "کارت:" + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " " + "پورت:" + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),
                         PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                         InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                         InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                         PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                         PostContactID = t.Bucht.Buchts.PostContact.ID,
                         PostContactStatus = t.Bucht.Buchts.PostContact.Status,
                         Connection = "ردیف:" + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t.Bucht.Buchts.BuchtNo,
                         BuchtID = t.Bucht.Buchts.ID,
                         PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                         SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                         SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                         SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                         PostID = t.Bucht.Buchts.PostContact.Post.ID,
                         CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                         PostName = t.Bucht.Buchts.PostContact.Post.Number,
                         CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                         CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
                         CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                         Customer = t.Telephone.Customer,
                         TelePhoneNo = t.Telephone.TelephoneNo,
                         TelephoneStatus = t.Telephone.Status,
                         InstallAddress = t.Telephone.Address,
                         CorrespondenceAddress = t.Telephone.Address1,
                         PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                         MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,

                     });

                return query.ToList();
            }
        }

        public static List<AssignmentInfo> GetAllInformationByTelephoneNos(List<long> TelephoneNos)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.PostContacts
                    .Where(j => j.Post.IsDelete == false)
                    .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })
                    .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts })
                    .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })
                    .Where(t => TelephoneNos.Contains(t.Telephone.TelephoneNo))
                     .Select(t => new AssignmentInfo
                     {
                         BuchtType = t.Bucht.Buchts.BuchtTypeID,
                         BuchtStatus = t.Bucht.Buchts.Status,
                         MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " " + "شلف:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " " + "کارت:" + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " " + "پورت:" + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),
                         PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                         InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                         InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                         PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                         PostContactID = t.Bucht.Buchts.PostContact.ID,
                         PostContactStatus = t.Bucht.Buchts.PostContact.Status,
                         Connection = "ردیف:" + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t.Bucht.Buchts.BuchtNo,
                         BuchtID = t.Bucht.Buchts.ID,
                         PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                         SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                         SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                         SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                         PostID = t.Bucht.Buchts.PostContact.Post.ID,
                         CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                         PostName = t.Bucht.Buchts.PostContact.Post.Number,
                         CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                         CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
                         CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                         Customer = t.Telephone.Customer,
                         TelePhoneNo = t.Telephone.TelephoneNo,
                         TelephoneStatus = t.Telephone.Status,
                         InstallAddress = t.Telephone.Address,
                         CorrespondenceAddress = t.Telephone.Address1,
                         PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                         MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                     });

                return query.ToList();
            }
        }
        public static AssignmentInfo GetAllInformationPostContactIDWithOutBuchtType(long postContactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.PostContacts
                                   .Where(po =>
                                                po.ID == postContactID
                                                &&
                                                po.Post.IsDelete == false
                                         )

                                   .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p }) // left join with Bucht
                                   .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })

                                   .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts }) //left join with Telephone
                                   .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })

                                   .Select(t => new AssignmentInfo
                                            {
                                                BuchtType = t.Bucht.Buchts.BuchtTypeID,
                                                BuchtStatus = t.Bucht.Buchts.Status,

                                                MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() +
                                                       " " +
                                                       "شلف:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() +
                                                       " " +
                                                       "کارت:" + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() +
                                                       " " +
                                                       "پورت:" + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),

                                                PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                                                InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                                                InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                                                PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                                                PostContactID = t.Bucht.Buchts.PostContact.ID,
                                                PostContactStatus = t.Bucht.Buchts.PostContact.Status,

                                                Connection = "ردیف:" + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo +
                                                             " " +
                                                             "طبقه:" + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo +
                                                             " " +
                                                             "اتصالی:" + t.Bucht.Buchts.BuchtNo,

                                                BuchtID = t.Bucht.Buchts.ID,
                                                PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                                                SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                                                SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                                                SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                                                PostID = t.Bucht.Buchts.PostContact.Post.ID,
                                                CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                                                PostName = t.Bucht.Buchts.PostContact.Post.Number,
                                                CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                                                CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
                                                CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                                                Customer = t.Telephone.Customer,
                                                TelePhoneNo = t.Telephone.TelephoneNo,
                                                TelephoneStatus = t.Telephone.Status,
                                                PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                                                MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                                            }
                                         );

                return query.SingleOrDefault();
            }
        }
        public static string GetCustomerID(int centerID, int CustomerTypeID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                string customreID = string.Empty;
                long maxSequences = context.GetMaxSequencesNumberOfCustomerID() ?? 0;
                //var city = DB.GetSettingByKey(DB.GetEnumItemDescription(typeof(DB.SettingKeys), (int)DB.SettingKeys.City)).ToLower();
                //if (city == "tehran" || city == "kermanshah")
                //{
                CRM.Data.CustomerType customerType = CustomerTypeDB.GetCustomerTypeByID(CustomerTypeID);
                if (customerType == null) return string.Empty;

                maxSequences++;
                string provinceCode = Data.ProvinceDB.GetProvinceByCenterID(centerID).Code;
                customreID = maxSequences.ToString("000000000") + provinceCode + customerType.Code.ToString("00");
                int checkDigit = DB.GetCheckDigit(customreID);
                customreID += checkDigit.ToString("0");
                //}
                //else if (city == "semnan")
                //{
                //    maxSequences++;
                //    customreID = maxSequences.ToString("000000000") + "000000";
                //}
                return customreID;
            }
        }

        public static string GetEnumItemDescription(Type enumType, int itemIndex)
        {
            List<Pair> list = new List<Pair>();

            System.Reflection.FieldInfo[] fieldInfos = enumType.GetFields().Where(t => t.IsLiteral).ToArray();

            foreach (System.Reflection.FieldInfo item in fieldInfos)
                list.Add(new Pair()
                {
                    Name = (item.GetCustomAttributes(typeof(DescriptionAttribute), false)[0] as DescriptionAttribute).Description,
                    Value = item.GetRawConstantValue()
                }
            );

            var x = list.Where(t => (int)t.Value == itemIndex)
                .Select(t => t.Name)
                .Cast<string>()
                .SingleOrDefault();
            return x;
        }

        public static int GetCheckDigit(string customreID)
        {
            char[] chars = customreID.ToCharArray();
            int sum = 0;

            for (int i = chars.Length - 1, mul = 2; i >= 0; i--, mul++)
            {
                if (mul > 7) mul = 2;
                if (chars[i] != '-')
                    sum += Convert.ToInt16(chars[i].ToString()) * mul;
            }

            int remained = sum % 11;

            if (remained <= 1) return 0;

            return (11 - remained);
        }

        internal static string GetConnectionWithPortByBuchtID(long BuchtID, int PortNo)
        {
            if (BuchtID == null) return " ";
            using (MainDataContext context = new MainDataContext())
            {
                return context.Buchts.Where(t => t.ID == BuchtID).AsEnumerable().Select(t => "پورت:" + PortNo.ToString() + DB.GetDescription(t.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.Description) +
                                                                            "ردیف:" + t.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo.ToString() +
                                                                            "طبقه:" + t.VerticalMDFRow.VerticalRowNo.ToString() +
                                                                            "اتصالی:" + t.BuchtNo.ToString()).SingleOrDefault().ToString();
            }
        }

        public static string MakeTheList(List<int> List)
        {
            string result = string.Empty;
            foreach (int item in List)
            {
                result += item + ",";
            }
            result = result.Substring(0, result.Length - 1);
            result = new StringBuilder("(").Append(result).Append(")").ToString();
            return result;
        }

        public static void SaveRequestLog(long _RequestID, DateTime serverDate)
        {
            InvestigatePossibility investigatePossibility = new InvestigatePossibility();
            CabinetInput cabinetInput = new CabinetInput();
            List<InvestigatePossibility> investigatePossibilitys = InvestigatePossibilityDB.GetInvestigatePossibilityByRequestID(_RequestID);
            if (investigatePossibilitys != null && investigatePossibilitys.Count != 0)
            {
                investigatePossibility = investigatePossibilitys.Take(1).SingleOrDefault();

                if (investigatePossibility != null && investigatePossibility.BuchtID != null)
                    cabinetInput = CabinetInputDB.GetCabinetInputByBuchtID((long)investigatePossibility.BuchtID);
            }

            Request _Request = Data.RequestDB.GetRequestByID(_RequestID);
            RequestLog requestLog = new RequestLog();
            requestLog.IsReject = false;
            Telephone telephone = Data.TelephoneDB.GetTelephoneByTelephoneNo(_Request.TelephoneNo ?? 0);

            switch (_Request.RequestTypeID)
            {
                case (int)DB.RequestType.Dayri:

                    InstallRequest installRequest1 = Data.InstallRequestDB.GetInstallRequestByRequestID(_Request.ID);
                    Customer customer = CustomerDB.GetCustomerByID((int)_Request.CustomerID);
                    AssignmentInfo assignmentInfo = DB.GetAllInformationByTelephoneNo(_Request.TelephoneNo ?? 0);

                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = customer.CustomerID;
                    requestLog.UserID = DB.currentUser.ID;

                    Data.Schema.Dayeri dayeri = new Data.Schema.Dayeri();
                    dayeri.TelephoneNo = _Request.TelephoneNo ?? 0;
                    dayeri.Bucht = DB.GetConnectionByBuchtID(investigatePossibility.BuchtID) ?? string.Empty;
                    if (assignmentInfo != null)
                    {
                        dayeri.Cabinet = assignmentInfo.CabinetName ?? 0;
                        dayeri.CabinetInput = assignmentInfo.InputNumber ?? 0;
                        dayeri.Post = assignmentInfo.PostName ?? 0;
                        dayeri.PostContact = assignmentInfo.PostContact ?? 0;
                    }


                    dayeri.NationalCodeOrRecordNo = customer.NationalCodeOrRecordNo ?? string.Empty;
                    dayeri.FirstNameOrTitle = customer.FirstNameOrTitle ?? string.Empty;
                    dayeri.LastName = customer.LastName ?? string.Empty;

                    // این ایف موقتی است تا زمان اصلاح چند درخواست دارای مشکل
                    if (telephone.InstallAddressID == null && assignmentInfo != null)
                    {

                        telephone.InstallAddressID = installRequest1.InstallAddressID;
                        telephone.CorrespondenceAddressID = installRequest1.CorrespondenceAddressID;

                        telephone.Detach();
                        DB.Save(telephone);

                    }

                    Address InstallAddress = Data.AddressDB.GetAddressByID(installRequest1.InstallAddressID ?? 0);
                    dayeri.InstallPostalCode = InstallAddress.PostalCode;
                    dayeri.InstallAddress = InstallAddress.AddressContent;

                    Address CorrespondenceAddress = Data.AddressDB.GetAddressByID(installRequest1.CorrespondenceAddressID ?? 0);
                    dayeri.CorrespondencePostalCode = CorrespondenceAddress.PostalCode;
                    dayeri.CorrespondenceAddress = CorrespondenceAddress.AddressContent;


                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.Dayeri>(dayeri, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog);

                    break;


                case (int)DB.RequestType.ChangeLocationCenterInside:
                case (int)DB.RequestType.ChangeLocationCenterToCenter:



                    ChangeLocation changeLocation = new ChangeLocation();
                    changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(_Request.ID);
                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = (long)changeLocation.OldTelephone;
                    requestLog.ToTelephoneNo = (long?)changeLocation.NewTelephone;


                    customer = Data.CustomerDB.GetCustomerByID((long)_Request.CustomerID);
                    requestLog.CustomerID = customer.CustomerID;
                    requestLog.UserID = DB.currentUser.ID;


                    CRM.Data.Schema.ChangeLocation changeLocationLog = new Data.Schema.ChangeLocation();

                    changeLocationLog.OldCabinetInputID = changeLocation.OldCabinetInputID ?? -1;
                    changeLocationLog.OldPostContactID = changeLocation.OldPostContactID ?? -1;

                    changeLocationLog.OldCustomerAddressID = changeLocation.OldInstallAddressID ?? -1;
                    changeLocationLog.NewCustomerAddressID = changeLocation.NewInstallAddressID ?? -1;
                    changeLocationLog.OldTelephone = changeLocation.OldTelephone ?? -1;
                    changeLocationLog.NewTelephone = changeLocation.NewTelephone ?? -1;
                    changeLocationLog.OldBucht = changeLocation.OldBuchtID ?? -1;
                    changeLocationLog.NewBucht = investigatePossibility.BuchtID ?? -1;
                    changeLocationLog.TargetCenter = changeLocation.TargetCenter ?? -1;
                    changeLocationLog.SourceCenter = changeLocation.SourceCenter ?? -1;
                    changeLocationLog.RequestType = _Request.RequestTypeID;
                    changeLocationLog.ChangeLocationTypeID = changeLocation.ChangeLocationTypeID ?? 0;
                    changeLocationLog.NewPostContactID = investigatePossibility.PostContactID ?? 0;
                    changeLocationLog.NewCabinetInputID = cabinetInput.ID;

                    CabinetInput oldCabinetInput = Data.CabinetInputDB.GetCabinetInputByBuchtID(changeLocation.OldBuchtID ?? 0);
                    //CabinetInput oldCabinetInput = Data.CabinetInputDB.GetCabinetInputByID(changeLocation.OldCabinetInputID ?? 0);
                    Cabinet oldcabinet = Data.CabinetDB.GetCabinetByID(oldCabinetInput.CabinetID);
                    PostContact oldPostContact = Data.PostContactDB.GetPostContactByID(changeLocation.OldPostContactID ?? 0);
                    Post oldPost = Data.PostDB.GetPostByID(oldPostContact == null ? 0 : oldPostContact.PostID);
                    Center oldCenter = Data.CenterDB.GetCenterById(changeLocation.SourceCenter ?? 0);

                    Cabinet newcabinet = Data.CabinetDB.GetCabinetByID(cabinetInput.CabinetID);
                    PostContact newPostContact = Data.PostContactDB.GetPostContactByID(investigatePossibility.PostContactID ?? 0);
                    Post newPost = Data.PostDB.GetPostByID(newPostContact.PostID);
                    Center newCenter = Data.CenterDB.GetCenterById(changeLocation.TargetCenter ?? 0);
                    Customer newCustomer = Data.CustomerDB.GetCustomerByID(changeLocation.NewCustomerID ?? 0);

                    if (newCustomer != null)
                    {
                        changeLocationLog.NewNationalCodeOrRecordNo = newCustomer.NationalCodeOrRecordNo;
                        changeLocationLog.NewFirstNameOrTitle = newCustomer.FirstNameOrTitle;
                        changeLocationLog.NewLastName = newCustomer.LastName;
                    }

                    if (customer != null)
                    {
                        changeLocationLog.OldNationalCodeOrRecordNo = customer.NationalCodeOrRecordNo;
                        changeLocationLog.OldFirstNameOrTitle = customer.FirstNameOrTitle;
                        changeLocationLog.OldLastName = customer.LastName;
                    }

                    changeLocationLog.OldCabinet = oldcabinet.CabinetNumber;
                    changeLocationLog.OldCabinetInput = oldCabinetInput.InputNumber;
                    changeLocationLog.OldPostContact = oldPostContact != null ? oldPostContact.ConnectionNo : 0;
                    changeLocationLog.OldPost = oldPost != null ? oldPost.Number : 0;
                    changeLocationLog.OldConnectionNo = Data.BuchtDB.GetConnectionByBuchtID(changeLocation.OldBuchtID ?? 0);

                    if (oldCenter != null)
                        changeLocationLog.SourceCenterName = oldCenter.CenterName;

                    changeLocationLog.NewCabinet = newcabinet.CabinetNumber;
                    changeLocationLog.NewCabinetInput = cabinetInput.InputNumber;
                    changeLocationLog.NewPostContact = newPostContact.ConnectionNo;
                    changeLocationLog.NewPost = newPost.Number;
                    changeLocationLog.NewConnectionNo = Data.BuchtDB.GetConnectionByBuchtID(investigatePossibility.BuchtID ?? 0);

                    if (newCenter != null)
                        changeLocationLog.TargetCenterName = newCenter.CenterName;

                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ChangeLocation>(changeLocationLog, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog);
                    if (changeLocation.NewTelephone != null && changeLocation.NewTelephone != 0)
                    {
                        CRM.Data.Interface.DischargeTelephones(new List<Telephone> { telephone });
                    }
                    break;
                case (int)DB.RequestType.Dischargin:

                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;



                    TakePossession takePossession = Data.TakePossessionDB.GetTakePossessionByID(_Request.ID);
                    CRM.Data.Schema.DischargeTelephone dischargeTelephone = new Data.Schema.DischargeTelephone();

                    dischargeTelephone.BuchtID = takePossession.BuchtID ?? -1;
                    dischargeTelephone.TelephoneNo = _Request.TelephoneNo ?? -1;
                    dischargeTelephone.DischargLetterNo = takePossession.DischargLeterNo;
                    dischargeTelephone.PortID = takePossession.SwitchPortID ?? -1;
                    dischargeTelephone.CustomerID = takePossession.CustomerID ?? -1;
                    dischargeTelephone.InstallAddressID = takePossession.InstallAddressID ?? -1;
                    dischargeTelephone.CorrespondenceAddressID = takePossession.CorrespondenceAddressID ?? -1;
                    dischargeTelephone.CabinetInputID = takePossession.CabinetInputID ?? 0;
                    dischargeTelephone.PostContactID = takePossession.PostContactID ?? 0;
                    dischargeTelephone.CenterID = _Request.CenterID;
                    dischargeTelephone.Bucht = Data.BuchtDB.GetConnectionByBuchtID(takePossession.BuchtID ?? -1);



                    customer = Data.CustomerDB.GetCustomerByID(takePossession.CustomerID ?? -1);
                    if (customer != null)
                    {
                        dischargeTelephone.NationalCodeOrRecordNo = customer.NationalCodeOrRecordNo;
                        dischargeTelephone.FirstNameOrTitle = customer.FirstNameOrTitle;
                        dischargeTelephone.LastName = customer.LastName;
                    }

                    InstallAddress = Data.AddressDB.GetAddressByID(takePossession.InstallAddressID ?? -1);
                    if (InstallAddress != null)
                    {
                        dischargeTelephone.InstallPostalCode = InstallAddress.PostalCode;
                        dischargeTelephone.InstallAddress = InstallAddress.AddressContent;
                    }

                    CorrespondenceAddress = Data.AddressDB.GetAddressByID(takePossession.CorrespondenceAddressID ?? -1);
                    if (CorrespondenceAddress != null)
                    {
                        dischargeTelephone.CorrespondencePostalCode = CorrespondenceAddress.PostalCode;
                        dischargeTelephone.CorrespondenceAddress = CorrespondenceAddress.AddressContent;
                    }

                    oldCabinetInput = Data.CabinetInputDB.GetCabinetInputByID(takePossession.CabinetInputID ?? 0);
                    if (oldCabinetInput != null)
                    {
                        oldcabinet = Data.CabinetDB.GetCabinetByID(oldCabinetInput.CabinetID);
                        dischargeTelephone.Cabinet = oldcabinet.CabinetNumber;
                        dischargeTelephone.CabinetInput = oldCabinetInput.InputNumber;
                    }

                    oldPostContact = Data.PostContactDB.GetPostContactByID(takePossession.PostContactID ?? 0);
                    if (oldCabinetInput != null)
                    {
                        oldPost = Data.PostDB.GetPostByID(oldPostContact.PostID);
                        dischargeTelephone.Post = oldPost.Number;
                        dischargeTelephone.PostContact = oldPostContact.ConnectionNo;

                    }





                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.DischargeTelephone>(dischargeTelephone, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog);

                    CRM.Data.Interface.DischargeTelephones(new List<Telephone> { telephone });

                    break;

                case (int)DB.RequestType.ChangeNo:

                    ChangeNo changeNo = Data.ChangeNoDB.GetChangeNoDBByID((long)_Request.ID);
                    requestLog = new RequestLog(); ;
                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = (long)changeNo.OldTelephoneNo;
                    requestLog.ToTelephoneNo = (long)changeNo.NewTelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;

                    Data.Schema.ChangeNo changeNoLog = new Data.Schema.ChangeNo();
                    changeNoLog.NewTelephoneNo = changeNo.NewTelephoneNo ?? 0;
                    changeNoLog.NewSwitchPort = changeNo.NewSwitchPortID ?? 0;
                    changeNoLog.OldTelephoneNo = changeNo.OldTelephoneNo;
                    changeNoLog.OldSwitchPort = changeNo.OldSwitchPortID ?? 0;
                    changeNoLog.OldCabinetInputID = changeNo.OldCabinetInputID ?? 0;
                    changeNoLog.OldPostContactID = changeNo.OldPostContactID ?? 0;

                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ChangeNo>(changeNoLog, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog);

                    CRM.Data.Interface.DischargeTelephones(new List<Telephone> { telephone });
                    break;


                case (int)DB.RequestType.Reinstall:


                    InstallRequest installRequest = Data.InstallRequestDB.GetInstallRequestByRequestID(_RequestID);
                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = (long)_Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;


                    Data.Schema.Reinstall reinstall = new Data.Schema.Reinstall();
                    reinstall.TelephoneNo = _Request.TelephoneNo ?? 0;
                    reinstall.CustomerAddressID = telephone.InstallAddressID ?? 0;
                    reinstall.CustomerID = telephone.CustomerID ?? 0;
                    reinstall.SwitchPort = telephone.SwitchPortID ?? 0;

                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.Reinstall>(reinstall, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog);
                    break;


                case (int)DB.RequestType.ChangeName:

                    ChangeName _ChangeName = Data.ChangeNameDB.GetChangeNameByID(_RequestID);

                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;

                    CRM.Data.Schema.ChangeName changeNameLog = new Data.Schema.ChangeName();
                    changeNameLog.OldCustomerID = _ChangeName.OldCustomerID;
                    changeNameLog.NewCustomerID = _ChangeName.NewCustomerID;
                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ChangeName>(changeNameLog, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                    break;
                case (int)DB.RequestType.CutAndEstablish:


                    CutAndEstablish cutAndEstablish = Data.CutAndEstablishDB.GetCutAndEstablishByRequestID(_RequestID);
                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;

                    CRM.Data.Schema.CutAndEstablish cutAndEstablishLog = new Data.Schema.CutAndEstablish();
                    cutAndEstablishLog.TelephoneNo = _Request.TelephoneNo ?? 0;
                    cutAndEstablishLog.Status = cutAndEstablish.Status ?? 0;
                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.CutAndEstablish>(cutAndEstablishLog, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                    break;
                case (int)DB.RequestType.Connect:

                    cutAndEstablish = Data.CutAndEstablishDB.GetCutAndEstablishByRequestID(_RequestID);
                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;

                    CRM.Data.Schema.Connect connect = new Data.Schema.Connect();
                    connect.TelephoneNo = _Request.TelephoneNo ?? 0;
                    connect.Status = cutAndEstablish.Status ?? 0;
                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.Connect>(connect, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                    break;
                case (int)DB.RequestType.TitleIn118:

                    TitleIn118 titleIn118 = Data.TitleIn118DB.GetTitleIn118ByRequestID(_RequestID);
                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;

                    CRM.Data.Schema.Title118 title118Log = new Data.Schema.Title118();
                    title118Log.Date = (DateTime)titleIn118.Date;
                    title118Log.LastNameAt118 = titleIn118.LastNameAt118;
                    title118Log.NameTitleAt118 = titleIn118.NameTitleAt118;
                    title118Log.Status = (byte)titleIn118.Status;
                    title118Log.TitleAt118 = titleIn118.TitleAt118;

                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.Title118>(title118Log, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                    break;
                case (int)DB.RequestType.RemoveTitleIn118:

                    titleIn118 = Data.TitleIn118DB.GetTitleIn118ByRequestID(_RequestID);
                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;

                    title118Log = new Data.Schema.Title118();
                    title118Log.Date = (DateTime)titleIn118.Date;
                    title118Log.LastNameAt118 = titleIn118.LastNameAt118;
                    title118Log.NameTitleAt118 = titleIn118.NameTitleAt118;
                    title118Log.Status = (byte)titleIn118.Status;
                    title118Log.TitleAt118 = titleIn118.TitleAt118;

                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.Title118>(title118Log, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                    break;
                case (int)DB.RequestType.ChangeTitleIn118:

                    titleIn118 = Data.TitleIn118DB.GetTitleIn118ByRequestID(_RequestID);
                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;

                    title118Log = new Data.Schema.Title118();
                    title118Log.Date = (DateTime)titleIn118.Date;
                    title118Log.LastNameAt118 = titleIn118.LastNameAt118;
                    title118Log.NameTitleAt118 = titleIn118.NameTitleAt118;
                    title118Log.Status = (byte)titleIn118.Status;
                    title118Log.TitleAt118 = titleIn118.TitleAt118;

                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.Title118>(title118Log, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                    break;
                case (int)DB.RequestType.SpecialService:
                    // log be saved in step salon for this request
                    break;
                case (int)DB.RequestType.OpenAndCloseZero:
                    {
                        requestLog = new RequestLog();
                        requestLog.RequestID = _Request.ID;
                        requestLog.RequestTypeID = _Request.RequestTypeID;
                        requestLog.TelephoneNo = _Request.TelephoneNo;
                        requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);

                        CRM.Data.ZeroStatus zeroStatus = ZeroStatusDB.GetZeroStatusByID(_Request.ID);
                        CRM.Data.Schema.OpenAndCloseZero OpenAndCloseZeroLog = new Data.Schema.OpenAndCloseZero();

                        OpenAndCloseZeroLog.ClassTelephone = (byte)telephone.ClassTelephone;
                        OpenAndCloseZeroLog.OldClassTelephone = zeroStatus.OldClassTelephone;
                        OpenAndCloseZeroLog.HasSecondZeroBlockCost = (zeroStatus.HasSecondZeroBlockCost.HasValue) ?
                                                                     (zeroStatus.HasSecondZeroBlockCost.Value) ? true : false
                                                                                                                      : false;
                        requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.OpenAndCloseZero>(OpenAndCloseZeroLog, true));

                        requestLog.Date = DB.GetServerDate();
                        requestLog.Detach();
                        DB.Save(requestLog, true);
                    }
                    break;
                case (int)DB.RequestType.RefundDeposit:

                    telephone.DischargeDate = serverDate;
                    telephone.Detach();
                    DB.Save(telephone);

                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = (int)_Request.RequestTypeID;
                    requestLog.IsReject = false;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);

                    takePossession = Data.TakePossessionDB.GetTakePossessionByID(_Request.ID);
                    dischargeTelephone = new Schema.DischargeTelephone();
                    dischargeTelephone.BuchtID = takePossession.BuchtID ?? -1;
                    dischargeTelephone.TelephoneNo = _Request.TelephoneNo ?? -1;
                    dischargeTelephone.DischargLetterNo = takePossession.DischargLeterNo;
                    dischargeTelephone.PortID = takePossession.SwitchPortID ?? -1;
                    dischargeTelephone.CustomerID = takePossession.CustomerID ?? -1;
                    dischargeTelephone.InstallAddressID = takePossession.InstallAddressID ?? -1;
                    dischargeTelephone.CorrespondenceAddressID = takePossession.CorrespondenceAddressID ?? -1;
                    dischargeTelephone.CabinetInputID = takePossession.CabinetInputID ?? 0;
                    dischargeTelephone.PostContactID = takePossession.PostContactID ?? 0;
                    dischargeTelephone.CenterID = _Request.CenterID;

                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.DischargeTelephone>(dischargeTelephone, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog);
                    break;
                case (int)DB.RequestType.SpecialWire:


                    SpecialWire specialWire = Data.SpecialWireDB.GetSpecialWireByRequestID(_RequestID);
                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;

                    CRM.Data.Schema.EstablishSpecialWire establishSpecialWire = new Data.Schema.EstablishSpecialWire();

                    establishSpecialWire.CenterID = _Request.CenterID;
                    establishSpecialWire.CenterName = Data.CenterDB.GetCenterNameByIDs(new List<int> { _Request.CenterID }).Take(1).SingleOrDefault();
                    establishSpecialWire.BuchtID = investigatePossibility.BuchtID ?? 0;
                    establishSpecialWire.BuchtNo = Data.BuchtDB.GetBuchtNoByBuchtID(investigatePossibility.BuchtID ?? 0).ToString();
                    establishSpecialWire.PostContactID = investigatePossibility.PostContactID ?? 0;
                    establishSpecialWire.CabinetInputID = cabinetInput.ID;
                    establishSpecialWire.OtherBuchtID = specialWire.OtherBuchtID ?? 0;
                    establishSpecialWire.InstallAddressID = specialWire.InstallAddressID ?? 0;

                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.EstablishSpecialWire>(establishSpecialWire, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                    break;

                case (int)DB.RequestType.VacateSpecialWire:


                    VacateSpecialWire vacateSpecialWire = Data.VacateSpecialWireDB.GetVacateSpecialWireByRequestID(_RequestID);
                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;

                    CRM.Data.Schema.VacateSpecialWire vacateSpecialWireSchema = new Data.Schema.VacateSpecialWire();

                    vacateSpecialWireSchema.CenterID = _Request.CenterID;
                    vacateSpecialWireSchema.CenterName = Data.CenterDB.GetCenterNameByIDs(new List<int> { _Request.CenterID }).Take(1).SingleOrDefault();
                    vacateSpecialWireSchema.BuchtID = vacateSpecialWire.BuchtID;
                    vacateSpecialWireSchema.BuchtNo = Data.BuchtDB.GetBuchtNoByBuchtID(vacateSpecialWire.BuchtID).ToString();
                    vacateSpecialWireSchema.PostContactID = vacateSpecialWire.PostContactID ?? 0;
                    vacateSpecialWireSchema.CabinetInputID = vacateSpecialWire.CabinetInputID ?? 0;
                    vacateSpecialWireSchema.OtherBuchtID = vacateSpecialWire.OtherBuchtID ?? -1;
                    vacateSpecialWireSchema.OldInstallAddressID = vacateSpecialWire.OldInstallAddressID ?? 0;

                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.VacateSpecialWire>(vacateSpecialWireSchema, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);

                    CRM.Data.Interface.DischargeTelephones(new List<Telephone> { telephone });
                    break;
                case (int)DB.RequestType.ChangeLocationSpecialWire:

                    ChangeLocationSpecialWire changeLocationSpecialWire = Data.ChangeLocationSpecialWireDB.GetChangeLocationWireByRequestID(_RequestID);
                    requestLog.RequestID = _Request.ID;
                    requestLog.RequestTypeID = _Request.RequestTypeID;
                    requestLog.TelephoneNo = _Request.TelephoneNo;
                    requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                    requestLog.UserID = DB.currentUser.ID;

                    CRM.Data.Schema.ChangeLocationSpecialWire changeLocationSpecialWireSchema = new Data.Schema.ChangeLocationSpecialWire();

                    changeLocationSpecialWireSchema.CenterID = _Request.CenterID;
                    changeLocationSpecialWireSchema.CenterName = Data.CenterDB.GetCenterNameByIDs(new List<int> { _Request.CenterID }).Take(1).SingleOrDefault();
                    changeLocationSpecialWireSchema.OldBuchtID = changeLocationSpecialWire.OldBuchtID;
                    changeLocationSpecialWireSchema.OldBuchtNo = Data.BuchtDB.GetBuchtNoByBuchtID(changeLocationSpecialWire.OldBuchtID).ToString();
                    changeLocationSpecialWireSchema.OldPostContactID = changeLocationSpecialWire.OldPostContactID ?? -1;
                    changeLocationSpecialWireSchema.OldCabinetInputID = changeLocationSpecialWire.OldCabinetInputID ?? -1;
                    changeLocationSpecialWireSchema.OtherBuchtID = changeLocationSpecialWire.OldOtherBuchtID ?? -1;
                    changeLocationSpecialWireSchema.OldInstallAddressID = changeLocationSpecialWire.OldInstallAddressID ?? -1;

                    changeLocationSpecialWireSchema.NewBuchtID = changeLocationSpecialWire.OldBuchtID;
                    changeLocationSpecialWireSchema.NewBuchtNo = Data.BuchtDB.GetBuchtNoByBuchtID(changeLocationSpecialWire.OldBuchtID).ToString();
                    changeLocationSpecialWireSchema.NewPostContactID = changeLocationSpecialWire.OldPostContactID ?? -1;
                    changeLocationSpecialWireSchema.NewCabinetInputID = changeLocationSpecialWire.OldCabinetInputID ?? -1;
                    changeLocationSpecialWireSchema.NewInstallAddressID = changeLocationSpecialWire.InstallAddressID ?? -1;


                    requestLog.Description = XElement.Parse(CRM.Data.LogSchemaUtility.Serialize<CRM.Data.Schema.ChangeLocationSpecialWire>(changeLocationSpecialWireSchema, true));

                    requestLog.Date = DB.GetServerDate();
                    requestLog.Detach();
                    DB.Save(requestLog, true);
                    break;
                case (int)DB.RequestType.E1:
                case (int)DB.RequestType.E1Link:
                case (int)DB.RequestType.SwapTelephone:
                    // log be saved in reqeust
                    break;
                case (int)DB.RequestType.SpaceandPower:
                    {
                        SpaceAndPower spaceAndPower = Data.SpaceAndPowerDB.GetSpaceAndPowerByRequestId(_Request.ID);
                        customer = CustomerDB.GetCustomerByID(spaceAndPower.SpaceAndPowerCustomerID);
                        requestLog = new RequestLog();
                        requestLog.RequestID = _Request.ID;
                        requestLog.RequestTypeID = _Request.RequestTypeID;
                        requestLog.TelephoneNo = null;
                        requestLog.ToTelephoneNo = null;
                        requestLog.CustomerID = DB.GetCustomerIDByCustomerTableID(_Request.CustomerID);
                        requestLog.UserID = DB.CurrentUser.ID;

                        Data.Schema.SpaceAndPower spaceAndPowerLog = new Schema.SpaceAndPower();
                        spaceAndPowerLog.Duration = spaceAndPower.Duration;
                        spaceAndPowerLog.EquipmentType = spaceAndPower.EquipmentType;
                        spaceAndPowerLog.EquipmentWeight = spaceAndPower.EquipmentWeight;
                        spaceAndPowerLog.HasFibre = spaceAndPower.HasFibre;
                        spaceAndPowerLog.CustomerName = string.Format("{0} {1}", customer.FirstNameOrTitle, customer.LastName);
                        List<Data.PowerType> powerTypes = PowerTypeDB.GetPowerTypesBySpaceAndPowerID(spaceAndPower.ID);
                        spaceAndPowerLog.PowerType = string.Join(" , ", powerTypes.Select(pt => pt.Title + pt.Rate.ToString()).ToList().ToArray());
                        spaceAndPowerLog.RigSpace = spaceAndPower.RigSpace;
                        spaceAndPowerLog.SpaceAndPowerCustomerID = spaceAndPower.SpaceAndPowerCustomerID;
                        spaceAndPowerLog.SpaceSize = spaceAndPower.SpaceSize;
                        spaceAndPowerLog.SpaceType = spaceAndPower.SpaceType;
                        spaceAndPowerLog.SpaceUsage = spaceAndPower.SpaceUsage;
                        requestLog.Description = XElement.Parse(Data.LogSchemaUtility.Serialize<CRM.Data.Schema.SpaceAndPower>(spaceAndPowerLog, true));

                        requestLog.Date = DB.GetServerDate();
                        requestLog.Detach();
                        DB.Save(requestLog);
                    }
                    break;

            }



        }

        public static string GetCustomerIDByCustomerTableID(long? ID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Customers.Where(t => t.ID == ID).SingleOrDefault().CustomerID;
            }
        }

        /// <summary>
        /// دریافت وضعیت قبلی تلفن
        /// اگر تلفن دارای لاگ نباشد آخرین وضعیت تلفن ازاد است در غیر انصورت 
        /// تلفن تخلیه بوده است
        /// </summary>
        /// <param name="telephoneNo"></param>
        /// <returns></returns>
        public static byte GetTelephoneLastStatus(long telephoneNo)
        {

            Telephone telephone = TelephoneDB.GetTelephoneByTelephoneNo(telephoneNo);
            if (telephone.InstallationDate.HasValue && telephone.DischargeDate.HasValue && telephone.InstallationDate.Value <= telephone.DischargeDate.Value)
            {
                return (byte)DB.TelephoneStatus.Discharge;
            }
            else
            {
                return (byte)DB.TelephoneStatus.Free;
            }
        }

        public static List<AssignmentInfo> GetAllInformationByBuchtIDs(List<long> buchtIDs, byte? BuchtStatus = null)
        {
            using (MainDataContext context = new MainDataContext())
            {
                var query = context.PostContacts
                    .GroupJoin(context.Buchts, p => p.ID, b => b.ConnectionID, (b, p) => new { Bucht = b.Buchts, PostContact = p })
                    .SelectMany(t2 => t2.Bucht.DefaultIfEmpty(), (t1, t2) => new { PostContacts = t1, Buchts = t2 })
                    .GroupJoin(context.Telephones, b => b.Buchts.SwitchPortID, t => t.SwitchPortID, (b, t) => new { Buchts = b, Telephones = t, PostContacts = b.PostContacts })
                    .SelectMany(x => x.Telephones.DefaultIfEmpty(), (t1, t2) => new { Bucht = t1.Buchts, PostContact = t1.PostContacts, Telephone = t2 })
                    .Where(t => DB.CurrentUser.CenterIDs.Contains(t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.CenterID))
                    .Where(t3 => buchtIDs.Contains(t3.Bucht.Buchts.ID))
                    .Where(t3 => BuchtStatus == null || t3.Bucht.Buchts.Status != BuchtStatus)
                     .Select(t => new AssignmentInfo
                     {
                         BuchtType = t.Bucht.Buchts.BuchtTypeID,
                         BuchtTypeString = DB.GetEnumDescriptionByValue(typeof(DB.BuchtType), t.Bucht.Buchts.BuchtTypeID),
                         BuchtStatus = t.Bucht.Buchts.Status,
                         MUID = "رک:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.PCMRock.Number.ToString() + " " + "شلف:" + t.Bucht.Buchts.PCMPort.PCM.PCMShelf.Number.ToString() + " " + "کارت:" + t.Bucht.Buchts.PCMPort.PCM.Card.ToString() + " " + "پورت:" + t.Bucht.Buchts.PCMPort.PortNumber.ToString(),
                         PCMPortIDInBuchtTable = t.Bucht.Buchts.PCMPortID,
                         InputNumber = t.Bucht.Buchts.CabinetInput.InputNumber,
                         InputNumberID = t.Bucht.Buchts.CabinetInput.ID,
                         PostContact = t.Bucht.Buchts.PostContact.ConnectionNo,
                         PostContactID = t.Bucht.Buchts.PostContact.ID,
                         PostContactStatus = t.Bucht.Buchts.PostContact.Status,
                         PostContactStatusString = DB.GetEnumDescriptionByValue(typeof(DB.PostContactStatus), t.Bucht.Buchts.PostContact.Status),
                         PostContactStatusName = t.Bucht.Buchts.PostContact.PostContactStatus.Name,
                         Connection = "ردیف:" + t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo + " " + "طبقه:" + t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo + " " + "اتصالی:" + t.Bucht.Buchts.BuchtNo,
                         BuchtID = t.Bucht.Buchts.ID,
                         PortNo = t.Bucht.Buchts.SwitchPort.PortNo,
                         SwitchPortID = t.Bucht.Buchts.SwitchPort.ID,
                         SwitchCode = t.Bucht.Buchts.SwitchPort.Switch.SwitchCode,
                         SwitchID = t.Bucht.Buchts.SwitchPort.Switch.ID,
                         TelePhoneNo = t.Telephone.TelephoneNo,
                         PostID = t.Bucht.Buchts.PostContact.Post.ID,
                         CabinetID = t.Bucht.Buchts.CabinetInput.Cabinet.ID,
                         PostName = t.Bucht.Buchts.PostContact.Post.Number,
                         CabinetName = t.Bucht.Buchts.CabinetInput.Cabinet.CabinetNumber,
                         CabinetIDForSearch = t.Bucht.Buchts.PostContact.Post.Cabinet.ID,
                         CustomerName = t.Telephone.Customer.FirstNameOrTitle + " " + t.Telephone.Customer.LastName,
                         TelephoneStatus = t.Telephone.Status,
                         PostContactType = t.Bucht.Buchts.PostContact.ConnectionType,
                         MDFID = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.MDFFrame.MDF.ID,
                         IsADSL = t.Bucht.Buchts.ADSLStatus,
                         Radif = t.Bucht.Buchts.VerticalMDFRow.VerticalMDFColumn.VerticalCloumnNo,
                         Tabaghe = t.Bucht.Buchts.VerticalMDFRow.VerticalRowNo,
                         CabinetInputID = t.Bucht.Buchts.CabinetInputID


                     }).OrderBy(t => t.PostContactID).ToList();
                return query;

            }
        }

        public static int GetDuration(DateTime FromDate, DateTime ToDate)
        {
            int _duration = (ToDate - FromDate).Days + 1;
            switch (_duration)
            {
                case 29:
                case 30:
                case 31:
                    return 30;
                    break;
                case 59:
                case 60:
                case 61:
                case 62:
                    return 60;
                    break;
            }
            return 0;
        }

        public static string GenerateBillID(long telephoneNo, int? centerID, byte subsidiaryCodeType)
        {
            string subsidiaryCode = CenterDB.GetSubsidiaryCode(telephoneNo, centerID, subsidiaryCodeType);
            string billID = (telephoneNo.ToString().Substring(3) + subsidiaryCode + "4").AddCheckDigit();

            return billID;
        }

        //public static string GenerateAllBillID(long telephoneNo, int? centerID, int subsidiaryCodeType, long amount, bool hasBillID)
        //{
        //    string subsidiaryCode = CenterDB.GetSubsidiaryCode(telephoneNo, centerID, (byte)subsidiaryCodeType);

        //    TelephoneCycleFiche cycle = TelephoneCycleFicheDB.GetTelephoneCycle(telephoneNo,(byte)subsidiaryCodeType);

        //    if (cycle == null)
        //    {
        //        cycle = new TelephoneCycleFiche();
        //        cycle.TelephoneNo = telephoneNo;
        //        cycle.YearCode = 1392;
        //        cycle.CycleCode = 10;
        //        cycle.SubsidiaryCodeType = (byte)subsidiaryCodeType;
        //    }
        //    else
        //        if (!hasBillID)
        //            cycle.CycleCode = cycle.CycleCode + 1;

        //    cycle.Detach();
        //    Save(cycle);

        //    using (MainDataContext context = new MainDataContext())
        //    {
        //        string result = context.GetBarcode(telephoneNo.ToString(), Convert.ToInt32(subsidiaryCode), amount, cycle.CycleCode.ToString());

        //        return result;
        //    }
        //}

        public static string GeneratePaymentID(long amount, long telephoneNo, string billID, byte subsidiaryCodeType, bool hasBillID)
        {
            TelephoneCycleFiche cycle = TelephoneCycleFicheDB.GetTelephoneCycle(telephoneNo, subsidiaryCodeType);

            if (cycle == null)
            {
                cycle = new TelephoneCycleFiche();
                cycle.TelephoneNo = telephoneNo;
                cycle.YearCode = 1392;
                cycle.CycleCode = 10;
                cycle.SubsidiaryCodeType = subsidiaryCodeType;
            }
            else
                if (!hasBillID)
                    cycle.CycleCode = cycle.CycleCode + 1;

            cycle.Detach();
            Save(cycle);

            string paymentID = (amount.ToString().Substring(0, amount.ToString().Length - 3) + cycle.YearCode.ToString().Substring(3) + cycle.CycleCode.ToString().AddZeroPrefix(2)).AddCheckDigit();
            paymentID += (billID + paymentID).GetCheckDigit();

            return paymentID;
        }

        public static string GenerateBarCode(string billID, string paymentID)
        {
            return billID.ToString().AddZeroPrefix(13) + paymentID.AddZeroPrefix(13);
        }

        public static object GetAboneInfoByPostContact(long postConatactID)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.PostContacts.Where(t => t.ID == postConatactID).Select(t => new AboneInfo
                {
                    PostContactID = t.ID,
                    ConnectionNo = t.ConnectionNo,
                    CabinetID = t.Post.Cabinet.ID,
                    CabinetNumber = t.Post.Cabinet.CabinetNumber,
                    PostID = t.Post.ID,
                    PostNumber = t.Post.Number,

                }).SingleOrDefault();
            }
        }

        public static bool IsFixRequest(int requestTypeID)
        {
            bool result = false;
            switch (requestTypeID)
            {
                case (int)DB.RequestType.ADSL:
                case (int)DB.RequestType.Wireless:
                case (int)DB.RequestType.ADSLChangeService:
                case (int)DB.RequestType.ADSLChangeIP:
                case (int)DB.RequestType.ADSLInstall:
                case (int)DB.RequestType.ADSLDischarge:
                case (int)DB.RequestType.ADSLChangePort:
                case (int)DB.RequestType.ADSLSellTraffic:
                case (int)DB.RequestType.ADSLChangePlace:
                case (int)DB.RequestType.ADSLSupport:
                case (int)DB.RequestType.ADSLCutTemporary:
                case (int)DB.RequestType.ADSLInstalPAPCompany:
                case (int)DB.RequestType.ADSLDischargePAPCompany:
                case (int)DB.RequestType.ADSLExchangePAPCompany:
                case (int)DB.RequestType.Failure117:
                case (int)DB.RequestType.WirelessSellTraffic:
                case (int)DB.RequestType.WirelessChangeService:
                    {
                        result = false;
                    }
                    break;
                default:
                    {
                        result = true;
                    }
                    break;

            }

            return result;
        }


        //چانچه کاربر قصد توسعه برای درخواست فضا و پاور و یا ایوان را داشته باشد
        //و در خواست اصلی دارای فایل هایی بوده باشد آنگاه برای درخواست جدید هم باید آن فایلها کپی شوند
        public static void CopyFiles(long requestID, List<Guid> filesID, DB.FileOfficeType fileOfficeType)
        {
            using (MainDataContext context = new MainDataContext())
            {
                DateTime nowServerDate = DB.GetServerDate();
                switch (fileOfficeType)
                {
                    case DB.FileOfficeType.PowerOfficeFile:
                        {
                            List<PowerOffice> files = new List<PowerOffice>();
                            foreach (Guid fileId in filesID)
                            {
                                PowerOffice powerOffice = new PowerOffice();
                                powerOffice.PowerFileID = fileId;
                                powerOffice.RequestID = requestID;
                                powerOffice.InsertDate = nowServerDate;
                                files.Add(powerOffice);
                            }
                            DB.SaveAll<PowerOffice>(files);
                        }
                        break;
                    case DB.FileOfficeType.CableOfficeFile:
                        {
                            List<CableDesignOffice> files = new List<CableDesignOffice>();
                            foreach (Guid fileId in filesID)
                            {
                                CableDesignOffice cableOffice = new CableDesignOffice();
                                cableOffice.CableDesignFileID = fileId;
                                cableOffice.RequestID = requestID;
                                cableOffice.InsertDate = nowServerDate;
                                files.Add(cableOffice);
                            }
                            DB.SaveAll<CableDesignOffice>(files);
                        }
                        break;
                    case DB.FileOfficeType.SwitchOfficeFile:
                        {
                            List<SwitchOffice> files = new List<SwitchOffice>();
                            foreach (Guid fileId in filesID)
                            {
                                SwitchOffice switchOffice = new SwitchOffice();
                                switchOffice.SwitchFileID = fileId;
                                switchOffice.RequestID = requestID;
                                switchOffice.InsertDate = nowServerDate;
                                files.Add(switchOffice);
                            }
                            DB.SaveAll<SwitchOffice>(files);
                        }
                        break;
                    case DB.FileOfficeType.TransferDepartmentFile:
                        {
                            List<TransferDepartmentOffice> files = new List<TransferDepartmentOffice>();
                            foreach (Guid fileId in filesID)
                            {
                                TransferDepartmentOffice transferDepartment = new TransferDepartmentOffice();
                                transferDepartment.TransferDepartmentFileID = fileId;
                                transferDepartment.RequestID = requestID;
                                transferDepartment.InsertDate = nowServerDate;
                                files.Add(transferDepartment);
                            }
                            DB.SaveAll<TransferDepartmentOffice>(files);
                        }
                        break;
                }
            }
        }

        public static void CopyTelecomminucationServicePayments(long requestID, List<TelecomminucationServicePayment> telecomminucationServicePayments)
        {
            using (MainDataContext context = new MainDataContext())
            {
                List<TelecomminucationServicePayment> telecomminucationServicePaymentsForNewRequest = new List<TelecomminucationServicePayment>();
                foreach (TelecomminucationServicePayment tsp in telecomminucationServicePayments)
                {
                    TelecomminucationServicePayment extendedRequestTelecomminucationServicePayment = new TelecomminucationServicePayment();
                    extendedRequestTelecomminucationServicePayment.AmountSum = tsp.AmountSum;
                    extendedRequestTelecomminucationServicePayment.Discount = tsp.Discount;
                    extendedRequestTelecomminucationServicePayment.NetAmount = tsp.NetAmount;
                    extendedRequestTelecomminucationServicePayment.NetAmountWithDiscount = tsp.NetAmountWithDiscount;
                    extendedRequestTelecomminucationServicePayment.Quantity = tsp.Quantity;
                    extendedRequestTelecomminucationServicePayment.TaxAndTollAmount = tsp.TaxAndTollAmount;
                    extendedRequestTelecomminucationServicePayment.TelecomminucationServiceID = tsp.TelecomminucationServiceID;
                    extendedRequestTelecomminucationServicePayment.RequestID = requestID;
                    telecomminucationServicePaymentsForNewRequest.Add(extendedRequestTelecomminucationServicePayment);
                }
                DB.SaveAll<TelecomminucationServicePayment>(telecomminucationServicePaymentsForNewRequest);
            }
        }

        public static long GetRequestAddress(long requestID, int requestTypeID)
        {
            long? addressID = -1;

            switch (requestTypeID)
            {
                case (int)DB.RequestType.Dayri:
                case (int)DB.RequestType.Reinstall:
                    InstallRequest _installRequest = Data.InstallRequestDB.GetInstallRequestByRequestID(requestID);
                    addressID = _installRequest.InstallAddressID;
                    break;

                case (int)DB.RequestType.ChangeLocationCenterToCenter:
                case (int)DB.RequestType.ChangeLocationCenterInside:
                    CRM.Data.ChangeLocation changeLocation = Data.ChangeLocationDB.GetChangeLocationByRequestID(requestID);
                    addressID = changeLocation.NewInstallAddressID;
                    break;

                case (int)DB.RequestType.E1:
                case (int)DB.RequestType.E1Link:
                    CRM.Data.E1 _e1 = Data.E1DB.GetE1ByRequestID(requestID);
                    addressID = _e1.InstallAddressID;
                    break;
                case (int)DB.RequestType.SpecialWire:
                case (int)DB.RequestType.SpecialWireOtherPoint:
                    CRM.Data.SpecialWire _specialWire = Data.SpecialWireDB.GetSpecialWireByRequestID(requestID);
                    if (_specialWire.SpecialWireType == (int)DB.SpecialWireType.Middle)
                        addressID = -1;
                    addressID = _specialWire.InstallAddressID;

                    break;
                case (int)DB.RequestType.ChangeLocationSpecialWire:
                    CRM.Data.ChangeLocationSpecialWire _changeLocationSpecialWire = Data.ChangeLocationSpecialWireDB.GetChangeLocationWireByRequestID(requestID);
                    if (_changeLocationSpecialWire.SpecialWireTypeID == (int)DB.SpecialWireType.Middle)
                        addressID = -1;
                    addressID = _changeLocationSpecialWire.InstallAddressID;

                    break;
            }
            return addressID ?? -1;
        }

        public static string ToStringSpecial(object value)
        {
            if (value != null)
            {
                if (value.ToString().ToLower() == "Null")
                    return "";
                else
                    return value.ToString();
            }
            else
                return string.Empty;
        }

        public static string GetCustomerIDByTelephoneNo(long telephonNo)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.Telephones.Where(t => t.TelephoneNo == telephonNo).Select(t => t.Customer.CustomerID).SingleOrDefault();
            }
        }


        public static bool HasRestrictionsTelphone(long telephoneNo, out string message, out bool inWaitingList)
        {
            bool hasRestriction = false;
            inWaitingList = false;
            message = string.Empty;
            AssignmentInfo assingmentInfo = DB.GetAllInformationByTelephoneNo(telephoneNo);
            if (assingmentInfo != null && !DB.CurrentUser.CenterIDs.Contains(assingmentInfo.CenterID))
            {
                hasRestriction = true;
                message = string.Format("دسترسی شما شامل مرکز {0} نمی باشد", assingmentInfo.CenterName);

            }
            // throw new Exception("تلفن متعلق به مرکز " + assingmentInfo.CenterName + "می باشد. دسترسی شما شامل این مرکز نمی باشد . ");

            // check for blacklist
            if (BlackListDB.ExistTelephoneNoInBlackList(telephoneNo))
            {
                hasRestriction = true;
                message += string.Format("تلفن در لیست سیاه قرار دارد");

            }



            // check to exist telephone on other request
            string requestName = Data.RequestDB.GetOpenRequestNameTelephone(new List<long> { telephoneNo }, out inWaitingList);
            if (!string.IsNullOrWhiteSpace(requestName))
            {
                hasRestriction = true;
                message += string.Format("این تلفن در روال {0} درحال پیگیری می باشد.", requestName);
                //   throw new Exception("این تلفن در روال " + requestName + " در حال پیگیری می باشد.");
            }



            // check to exist telephone on other request
            CRM.Data.WarningHistory warningHistory = Data.WarningHistoryDB.GetLastWarningHistoryByTelephon(telephoneNo);
            if (warningHistory != null && warningHistory.Type == (byte)DB.WarningHistory.arrest)
            {
                hasRestriction = true;
                message += string.Format("امکان ثبت درخواست برای این تلفن بعلت بازداشت نمی باشد");
                //throw new Exception("امکان ثبت درخواست برای این تلفن بعلت بازداشت نمی باشد");
            }

            if (TelephoneDB.CheckReserveTelephone(telephoneNo))
            {
                hasRestriction = true;
                message += string.Format("تلفن رزرو می باشد");

            }




            return hasRestriction;
        }



        public static DateTime? sh2mi(string sh)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.sh2mi(sh);
            }
        }



        public static int ExecuteCommand(string sql)
        {
            using (MainDataContext context = new MainDataContext())
            {
                return context.ExecuteCommand(sql);
            }
        }



        public static string GetConditionString(int condition, int activePostContactCount, string param)
        {
            string str = string.Empty;
            switch (condition)
            {
                case (int)DB.Condition.Equal:
                    str = string.Format("{0} == {1}", param, activePostContactCount);
                    break;

                case (int)DB.Condition.GreaterThan:
                    str = string.Format("{0} > {1}", param, activePostContactCount);
                    break;

                case (int)DB.Condition.GreaterThanOrEqual:
                    str = string.Format("{0} >= {1}", param, activePostContactCount);
                    break;
                case (int)DB.Condition.LessThan:
                    str = string.Format("{0} < {1}", param, activePostContactCount);
                    break;
                case (int)DB.Condition.LessThanOrEqual:
                    str = string.Format("{0} <= {1}", param, activePostContactCount);
                    break;
            }

            return str;
        }


        public static DataTable FillSenmanDataTable()
        {
            System.Data.DataTable telephoneInfo = new System.Data.DataTable();

            System.Data.DataColumn c1 = new System.Data.DataColumn("CENTERCODE");
            telephoneInfo.Columns.Add(c1);
            System.Data.DataColumn c2 = new System.Data.DataColumn("MELLICODE");
            telephoneInfo.Columns.Add(c2);
            System.Data.DataColumn c3 = new System.Data.DataColumn("FIRSTNAME");
            telephoneInfo.Columns.Add(c3);
            System.Data.DataColumn c4 = new System.Data.DataColumn("LASTNAME");
            telephoneInfo.Columns.Add(c4);
            System.Data.DataColumn c5 = new System.Data.DataColumn("FATHERNAME");
            telephoneInfo.Columns.Add(c5);
            System.Data.DataColumn c7 = new System.Data.DataColumn("SHENASNAME");
            telephoneInfo.Columns.Add(c7);
            System.Data.DataColumn c8 = new System.Data.DataColumn("MOBILE");
            telephoneInfo.Columns.Add(c8);
            System.Data.DataColumn c9 = new System.Data.DataColumn("EMAIL");
            telephoneInfo.Columns.Add(c9);
            System.Data.DataColumn c10 = new System.Data.DataColumn("ADDRESS");
            telephoneInfo.Columns.Add(c10);
            System.Data.DataColumn c11 = new System.Data.DataColumn("CODE_POSTI");
            telephoneInfo.Columns.Add(c11);
            System.Data.DataColumn c12 = new System.Data.DataColumn("PHONENO");
            telephoneInfo.Columns.Add(c12);

            System.Data.DataColumn c13 = new System.Data.DataColumn("CEN_NAME");
            telephoneInfo.Columns.Add(c13);





            telephoneInfo.Rows.Add(1, "963", "", "", "", "", "", "", "123", "12545", "2333388243", "عاملو");
            telephoneInfo.Rows.Add(1, "963", "", "", "", "", "", "", "123", "12545", "2333388242", "عاملو");
            return telephoneInfo;
        }

        public static void BulkInsertAll<T>(List<T> entities)
        {
            using (MainDataContext context = new MainDataContext())
            {
                using (var conn = new SqlConnection(context.Connection.ConnectionString))
                {
                    conn.Open();

                    Type t = typeof(T);

                    var tableAttribute = (TableAttribute)t.GetCustomAttributes(
                        typeof(TableAttribute), false).Single();
                    var bulkCopy = new SqlBulkCopy(conn)
                    {
                        DestinationTableName = tableAttribute.Name
                    };

                    var properties = t.GetProperties().Where(t2 => t2.PropertyType.Namespace == "System").ToArray();

                    var table = new DataTable();

                    foreach (var property in properties)
                    {
                        Type propertyType = property.PropertyType;
                        if (propertyType.IsGenericType &&
                            propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            propertyType = Nullable.GetUnderlyingType(propertyType);
                        }

                        table.Columns.Add(new DataColumn(property.Name, propertyType));
                        bulkCopy.ColumnMappings.Add(new SqlBulkCopyColumnMapping(property.Name, property.Name));
                    }

                    foreach (var entity in entities)
                    {
                        table.Rows.Add(
                            properties.Select(
                            property => property.GetValue(entity, null) ?? DBNull.Value
                            ).ToArray());
                    }

                    bulkCopy.WriteToServer(table);
                }
            }
        }

        //TODO:rad 13950206
        public static void SetConnectionString(string connectionString)
        {
            Properties.Settings.Default["CRMConnectionString"] = connectionString;
        }
    }

    public static class EnumTypeNameHelper
    {
        #region Constructor

        static EnumTypeNameHelper()
        {
            // Create enum-to-friendly name dictionary
            ConnectionTypeFriendlyNames = new Dictionary<DB.PostContactConnectionType, string>
                {
                              {DB.PostContactConnectionType.Noraml, "عادی"},
                              {DB.PostContactConnectionType.PCMNormal, "پی سی ام"},
                                     
                };

            // Create friendly name-to-enum dictionary
            ConnectionTypeEnumValues = ConnectionTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);



            // Create enum-to-friendly name dictionary
            RoundTypeFriendlyNames = new Dictionary<DB.RoundType, string>
                {
                              {DB.RoundType.Diamond, "الماس"},
                              {DB.RoundType.Gold, "طلا"},
                              {DB.RoundType.Silver, "نقره"},
                              {DB.RoundType.Express, "سفارشی"},
                              {DB.RoundType.Old, "رند قدیم"},                            
                };

            // Create friendly name-to-enum dictionary
            RoundTypeEnumValues = RoundTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);


            // Create enum-to-friendly name dictionary
            DocumentTypeFriendlyNames = new Dictionary<DB.DocumentType, string>
                {
                            {DB.DocumentType.Document, "مدرک"},
                            {DB.DocumentType.Permission, "مجوز"},
                             {DB.DocumentType.Contract, "قرارداد"},
                            
                };

            // Create friendly name-to-enum dictionary
            DocumentTypeEnumValues = DocumentTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);


            // Create enum-to-friendly name dictionary
            AddressTypeFriendlyNames = new Dictionary<DB.AddressType, string>
                {
                            {DB.AddressType.Contact, "مکاتبه"},
                            {DB.AddressType.Install, "نصب"},
                            
                };

            // Create friendly name-to-enum dictionary
            AddressTypeEnumValues = AddressTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);

            // Create enum-to-friendly name dictionary
            TelephoneTypeFriendlyNames = new Dictionary<DB.TelephoneType, string>
                {
                            {DB.TelephoneType.Temporary, "موقت"},
                };

            // Create friendly name-to-enum dictionary
            TelephoneTypeEnumValues = TelephoneTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);

            PersonTypeFriendlyNames = new Dictionary<DB.PersonType, string>
                {
                            {DB.PersonType.Person, "حقیقی"},
                            {DB.PersonType.Company, "حقوقی"},
                            {DB.PersonType.Both, "هردو"},
                             {DB.PersonType.Nothing, "نامشخص"},
                           
                };

            // Create friendly name-to-enum dictionary
            PersonTypeEnumValues = PersonTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);


            OrderTypeFriendlyNames = new Dictionary<DB.OrderType, string>
                {
                            {DB.OrderType.Normal, "عادی"},
                            {DB.OrderType.OutOfOrder, "خارج از نوبت"},                        
                           
                };

            // Create friendly name-to-enum dictionary
            OrderTypeEnumValues = OrderTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);


            Use3PercentTypeFriendlyNames = new Dictionary<DB.Use3PercentType, string>
                {
                            {DB.Use3PercentType.CabinetPost, "کافو-پست"},
                            
                };

            // Create friendly name-to-enum dictionary
            Use3PercentTypeEnumValues = Use3PercentTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);




        }

        #endregion

        #region Properties

        public static Dictionary<DB.PostContactConnectionType, String> ConnectionTypeFriendlyNames { get; set; }

        public static Dictionary<String, byte> ConnectionTypeEnumValues { get; set; }


        public static Dictionary<DB.RoundType, String> RoundTypeFriendlyNames { get; set; }

        public static Dictionary<String, byte> RoundTypeEnumValues { get; set; }


        public static Dictionary<DB.ChargingGroup, String> ChargingTypeFriendlyNames { get; set; }



        public static Dictionary<DB.OrderType, String> OrderTypeFriendlyNames { get; set; }

        public static Dictionary<String, byte> OrderTypeEnumValues { get; set; }


        public static Dictionary<DB.PersonType, String> PersonTypeFriendlyNames { get; set; }

        public static Dictionary<String, byte> PersonTypeEnumValues { get; set; }



        public static Dictionary<DB.TelephoneType, String> TelephoneTypeFriendlyNames { get; set; }

        public static Dictionary<String, byte> TelephoneTypeEnumValues { get; set; }


        public static Dictionary<DB.Use3PercentType, String> Use3PercentTypeFriendlyNames { get; set; }

        public static Dictionary<String, byte> Use3PercentTypeEnumValues { get; set; }


        public static Dictionary<DB.AddressType, String> AddressTypeFriendlyNames { get; set; }

        public static Dictionary<String, byte> AddressTypeEnumValues { get; set; }


        public static Dictionary<DB.DocumentType, String> DocumentTypeFriendlyNames { get; set; }

        public static Dictionary<String, byte> DocumentTypeEnumValues { get; set; }

        #endregion
    }

    public class HierarchyNode<T> where T : class
    {
        public T Entity { get; set; }
        public IEnumerable<HierarchyNode<T>> ChildNodes { get; set; }
        public int Depth { get; set; }
    }

    public static class LinqExtensionMethods
    {
        private static System.Collections.Generic.IEnumerable<HierarchyNode<TEntity>> CreateHierarchy<TEntity, TProperty>
          (IEnumerable<TEntity> allItems, TEntity parentItem,
          Func<TEntity, TProperty> idProperty, Func<TEntity, TProperty> parentIdProperty, int depth) where TEntity : class
        {
            IEnumerable<TEntity> childs;

            if (parentItem == null)
                childs = allItems.Where(i => parentIdProperty(i).Equals(default(TProperty)));
            else
                childs = allItems.Where(i => parentIdProperty(i).Equals(idProperty(parentItem)));

            if (childs.Count() > 0)
            {
                depth++;

                foreach (var item in childs)
                    yield return new HierarchyNode<TEntity>()
                    {
                        Entity = item,
                        ChildNodes = CreateHierarchy<TEntity, TProperty>
                            (allItems, item, idProperty, parentIdProperty, depth),
                        Depth = depth
                    };
            }
        }

        /// <summary>
        /// LINQ IEnumerable AsHierachy() extension method
        /// </summary>
        /// <typeparam name="TEntity">Entity class</typeparam>
        /// <typeparam name="TProperty">Property of entity class</typeparam>
        /// <param name="allItems">Flat collection of entities</param>
        /// <param name="idProperty">Reference to Id/Key of entity</param>
        /// <param name="parentIdProperty">Reference to parent Id/Key</param>
        /// <returns>Hierarchical structure of entities</returns>
        public static System.Collections.Generic.IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity, TProperty>
          (this IEnumerable<TEntity> allItems, Func<TEntity, TProperty> idProperty, Func<TEntity, TProperty> parentIdProperty)
          where TEntity : class
        {
            return CreateHierarchy(allItems, default(TEntity), idProperty, parentIdProperty, 0);
        }
    }

    //TODO:rad- تعیین یک زمان پیش فرض برای اجرای تمام کوئری ها
    public partial class MainDataContext : System.Data.Linq.DataContext
    {
        partial void OnCreated()
        {
            base.CommandTimeout = 900;
        }
    }



}

//////using System;
//////using System.Data;
//////using System.Configuration;
//////using System.Linq;
//////using System.Xml.Linq;
//////using System.Data.Linq.Mapping;
//////using System.Linq.Expressions;
//////using System.Collections;
//////using System.Collections.Generic;
//////using System.Data.Linq;
//////using System.Collections.ObjectModel;
//////using System.ComponentModel;
//////using System.Reflection;



//////namespace CRM.Data
//////{

//////    public static partial class DB
//////    {
//////        public static UserInfo CurrentUser { get; set; }

//////        public static string GetEnumDescription(int value, Type enumtype)
//////        {
//////            FieldInfo fi = enumtype.GetField((((CRM.Data.DB.TelephoneType[])(enumtype.GetEnumValues()))[value]).ToString());
//////            //((CRM.Data.DB.TelephoneType[])(enumtype.GetEnumValues()))[value];
//////            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
//////            if (attributes != null && attributes.Length > 0)
//////                return attributes[0].Description;
//////            else
//////                return value.ToString();

//////            //int value = 1; string description = Enumerations.GetEnumDescription((MyEnum)value); 

//////        }

//////        public static DateTime GetServerDate()
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return Convert.ToDateTime(context.GetDate());

//////            }
//////        }

//////        public static DateTime? ServerDate()
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.serverdate();
//////            }
//////        }



//////        #region Select

//////        public static string GetAboneConnection(byte sourceType,long connectionID)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                string conNo = string.Empty;
//////                string centralcableno = string.Empty;
//////                centralcableno = context.Buchts.Where(b=>b.ID==connectionID).Select(s => s.CablePair.CablePairNumber).Take(1).SingleOrDefault().ToString() + "-";
//////                Bucht bucht=context.Buchts.Where(b=>b.ID==connectionID).Take(1).SingleOrDefault();
//////                if (sourceType == (byte)DB.SourceType.Post)
//////                {
//////                    conNo = context.PostContacts.Where(t => t.ID == bucht.ConnectionID).Select(t => t.Post.Cabinet.CabinetNumber.ToString() + "-"
//////                        + centralcableno
//////                        + t.Post.Number.ToString() + "-" + t.ConnectionNo.ToString()).SingleOrDefault().ToString();
//////                }
//////                else if (sourceType == (byte)DB.SourceType.PCM)
//////                {
//////                     conNo = context.Buchts.Where(t=>t.ID==connectionID).Select(t=>t.CabinetInput.Cabinet.CabinetNumber.ToString() + "-"
//////                         + centralcableno
//////                         + t.PostContact.Post.Number.ToString() + "-" + t.PostContact.ConnectionNo.ToString() + "," + t.PCMChannelNo.ToString()).SingleOrDefault().ToString();                   
//////                }

//////                return conNo;
//////            }

//////        }

//////        public static IEnumerable<StepStatusInfo> GetStepStatus(int requestTypeID, int step)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {

//////                var hierachy = context.RequestSteps.Join(context.Status, rs => rs.ID, ss => ss.RequestStepID, (rs, ss) => new { rquestStep = rs, stepStatus = ss }).Where(x => x.rquestStep.RequestTypeID == requestTypeID && x.rquestStep.ID == step).ToList()
//////                               .Select(z => new StepStatusInfo { reqStepID = z.rquestStep.ID, StepStatusID = z.stepStatus.ID, ParentStepStatusID = z.stepStatus.ParentID,StatusResult=z.stepStatus.Title }).ToList();
//////                              // .AsHierarchy(e => e.reqStepID, e => e.StepStatusID).ToList();
//////                return hierachy;

//////            }
//////        }

//////        public static SwitchCodeInfo GetSwitchCodeInfo(long telno)
//////        {
//////            // Comment By Mild
//////            //using (MainDataContext context = new MainDataContext())
//////            //{
//////            //    return  context.Telephones.Join(context.Switches, t => t.CenterID, s => s.CenterID, (t, s) => new { tel = t, sw = s }).Where(x =>x.tel.TelephoneNo==telno && x.tel.TelephoneNo >= x.sw.StartNo && x.tel.TelephoneNo <= x.sw.EndNo)
//////            //        .Select(x => new SwitchCodeInfo { SwitchPreNo = x.sw.SwitchPreNo, PreCodeType = x.sw.PreCodeType, PortNo = x.tel.SwitchPort.PortNo,TelephoneNo =telno}).SingleOrDefault();


//////            //}
//////            throw new ArgumentException();
//////        }

//////        public static string GetDepositeNumber(int centerCode, string year)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.GetDepositeNumber(centerCode, year);
//////            }
//////        }

//////        public static List<T> SearchByPropertyName<T>(string propertyName, object value) where T : class
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                BinaryExpression binaryExpression = null;
//////                IEnumerable<T> dataContainer = context.GetTable<T>();
//////                var parameter = Expression.Parameter(typeof(T), "dataContainer");
//////                var property = typeof(T).GetProperty(propertyName);
//////                var propertyType = property.PropertyType;
//////                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
//////                 binaryExpression = GetEqualBinaryExpression(propertyAccess, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType), propertyType);   
//////                Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
//////                Func<T, bool> compiled = predicate.Compile();
//////                return dataContainer.Where(compiled).ToList();
//////            }

//////        }

//////        public static T ChangeType<T>(object value)
//////        {

//////            Type conversionType = typeof(T);

//////            if (conversionType.IsGenericType &&

//////                conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
//////            {

//////                if (value == null) { return default(T); }

//////                conversionType = Nullable.GetUnderlyingType(conversionType); ;

//////            }

//////            return (T)Convert.ChangeType(value, conversionType);

//////        }



//////        static BinaryExpression GetEqualBinaryExpression(MemberExpression propertyAccess, object columnValue, Type propertyType)
//////        {
//////            return Expression.Equal(propertyAccess, Expression.Constant(columnValue, propertyType));
//////            //GetLowerCasePropertyAccess(propertyAccess)
//////        }

//////        static MethodCallExpression GetLowerCasePropertyAccess(MemberExpression propertyAccess)
//////        {
//////            return Expression.Call(Expression.Call(propertyAccess, "ToString", new Type[0]), typeof(string).GetMethod("ToLower", new Type[0]));
//////        }

//////        public static Expression<Func<T, bool>> MakeFilter<T>(string propertyName, object value)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {

//////                PropertyInfo property = typeof(T).GetProperty(propertyName);
//////                ParameterExpression parameter = Expression.Parameter(typeof(T), "p");
//////                MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, property);
//////                ConstantExpression constantValue = Expression.Constant(value);
//////                BinaryExpression equality = Expression.Equal(propertyAccess, constantValue);
//////                return Expression.Lambda<Func<T, bool>>(equality, parameter);
//////            }
//////        }

//////        public static IEnumerable<T> GetEntitiesbyID<T>(long Id) where T : class
//////        {

//////            using (MainDataContext context = new MainDataContext())
//////            {

//////                ITable tbl = context.GetTable(typeof(T));

//////                ParameterExpression pe = Expression.Parameter(typeof(T), "p"); // 1. create parameter P

//////                MetaDataMember id = context.Mapping.GetTable(typeof(T)).RowType.IdentityMembers[0];

//////                MemberExpression prop = Expression.Property(pe, id.Member.Name);  //2.Create property p.ID
//////                //MemberExpression prop = Expression.Property(pe, "ID");  //2.Create property p.ID

//////                /******************************************
//////                   var table = context.GetTable<T>();
//////                // get the metamodel mappings (database to domain objects)
//////                 MetaModel modelMapping = table.Context.Mapping;

//////                // get the data members for this type                    
//////                ReadOnlyCollection<MetaDataMember> dataMembers = modelMapping.GetMetaType(typeof(T)).DataMembers;

//////                // find the primary key and return its type
//////               Type Pktype= (dataMembers.Single<MetaDataMember>(m => m.IsPrimaryKey)).Type;
//////               string PkName = (dataMembers.Single<MetaDataMember>(m => m.IsPrimaryKey)).Name;  

//////                 ********************************************/


//////                ConstantExpression ce = Expression.Constant(Id); // 3. ==1
//////                BinaryExpression be = Expression.Equal(prop, ce); //4. p.ID==1
//////                //LambdaExpression keyselector = Expression.Lambda(be, pe); //5.Create lamba expression p =>(p.ID==1)
//////                return ((tbl) as IQueryable<T>).Where(Expression.Lambda<Func<T, bool>>(be, new ParameterExpression[] { pe }).Compile()).ToList();
//////                // return ((tbl) as IQueryable<T>).Where<T>(Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(pe, "ID"), Expression.Constant(Id)), pe)).ToList();


//////            }

//////        }

//////        public static T GetEntitybyID<T>(long Id) where T : class
//////        {

//////            using (MainDataContext context = new MainDataContext())
//////            {

//////                ITable tbl = context.GetTable(typeof(T));

//////                ParameterExpression pe = Expression.Parameter(typeof(T), "p"); // 1. create parameter P

//////                MetaDataMember id = context.Mapping.GetTable(typeof(T)).RowType.IdentityMembers[0];

//////                MemberExpression prop = Expression.Property(pe, id.Member.Name);  //2.Create property p.ID

//////                ConstantExpression ce = Expression.Constant(Id); // 3. ==1

//////                BinaryExpression be = Expression.Equal(prop, ce); //4. p.ID==1

//////                return ((tbl) as IQueryable<T>).Where(Expression.Lambda<Func<T, bool>>(be, new ParameterExpression[] { pe }).Compile()).SingleOrDefault();

//////            }

//////        }

//////        public static T GetEntitybyIntID<T>(long Id) where T : class
//////        {

//////            using (MainDataContext context = new MainDataContext())
//////            {

//////                ITable tbl = context.GetTable(typeof(T));

//////                ParameterExpression pe = Expression.Parameter(typeof(T), "p"); // 1. create parameter P

//////                MetaDataMember id = context.Mapping.GetTable(typeof(T)).RowType.IdentityMembers[0];

//////                MemberExpression prop = Expression.Property(pe, id.Member.Name);  //2.Create property p.ID

//////                ConstantExpression ce = Expression.Constant(Id); // 3. ==1

//////                BinaryExpression be = Expression.Equal(prop, ce); //4. p.ID==1

//////                return ((tbl) as IQueryable<T>).Where(Expression.Lambda<Func<T, bool>>(be, new ParameterExpression[] { pe }).Compile()).SingleOrDefault();

//////            }

//////        }

//////        public static IEnumerable<T> GetAllEntity<T>() where T : class
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                return context.GetTable<T>().ToList<T>();
//////            }
//////        }

//////        #endregion Select

//////        #region Delete Public Methods

//////        public static void Delete<T>(object primaryKey) where T : class
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {

//////                BinaryExpression binaryExpression = null;
//////                IEnumerable<T> dataContainer = context.GetTable<T>();
//////                var parameter = Expression.Parameter(typeof(T), "dataContainer");
//////                MetaDataMember id = context.Mapping.GetTable(typeof(T)).RowType.IdentityMembers[0];
//////                var property = typeof(T).GetProperty(id.Member.Name);
//////                var propertyType = property.PropertyType;
//////                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
//////                binaryExpression = GetEqualBinaryExpression(propertyAccess, primaryKey, propertyType);
//////                Expression<Func<T, bool>> predicate = Expression.Lambda<Func<T, bool>>(binaryExpression, parameter);
//////                Func<T, bool> compiled = predicate.Compile();
//////                dataContainer.Where(compiled).ToList();
//////                T entity = context.GetTable<T>().Single<T>(compiled);
//////                context.GetTable<T>().DeleteOnSubmit(entity);
//////                context.SubmitChanges();

//////                //ParameterExpression parameter = Expression.Parameter(typeof(T), "item");
//////                //T entity = context.GetTable<T>().Single<T>(Expression.Lambda<Func<T, bool>>(Expression.Equal(Expression.Property(parameter, id.Member.Name), Expression.Constant(primaryKey, typeof(T).GetProperty(id.Member.Name).PropertyType))));
//////                //context.GetTable<T>().DeleteOnSubmit(entity);
//////                //context.SubmitChanges();
//////            }
//////        }

//////        public static void DeleteAll<T>(List<int> primaryKeys) where T : class
//////        {
//////            if (primaryKeys.Count == 0) return;

//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                MetaTable table = context.Mapping.MappingSource.GetModel(typeof(T)).GetMetaType(typeof(T)).Table;

//////                MetaDataMember id = context.Mapping.GetTable(typeof(T)).RowType.IdentityMembers[0];

//////                context.ExecuteCommand(string.Format("Delete From {0} Where {1} in ({2})", table.TableName, id.Name, string.Join(",", primaryKeys)));

//////                context.SubmitChanges();
//////            }
//////        }

//////        #endregion

//////        #region Save Public Mehtod



//////        private static bool IsValidType(object o, Type t)
//////        {
//////            try
//////            {
//////                System.Convert.ChangeType(o, t);
//////                return true;
//////            }
//////            catch
//////            {
//////                return false;
//////            }
//////        }

//////        public static void Save(object instance)
//////        {
//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                //var entityType = instance.GetType();
//////                //var table = context.GetTable(entityType);
//////                //var metaType = context.Mapping.GetMetaType(entityType);
//////                //var member = metaType.DataMembers.Single(m => m.IsPrimaryKey).Member;      

//////                MetaDataMember primaryKey = context.Mapping.GetTable(instance.GetType()).RowType.IdentityMembers[0];

//////                if (primaryKey.MemberAccessor.GetBoxedValue(instance) == null
//////                                     || (IsValidType(primaryKey.MemberAccessor.GetBoxedValue(instance), typeof(Int64)))
//////                                    && Convert.ToInt64(primaryKey.MemberAccessor.GetBoxedValue(instance)) == 0)
//////                {
//////                    context.GetTable(instance.GetType()).InsertOnSubmit(instance);
//////                }
//////                else
//////                {
//////                    context.GetTable(instance.GetType()).Attach(instance);
//////                    context.Refresh(System.Data.Linq.RefreshMode.KeepCurrentValues, instance);
//////                }
//////                context.SubmitChanges();
//////            }
//////        }

//////        #endregion

//////        public static object ToType<T>(this object obj) where T : class
//////        {

//////            //create instance of T type object:
//////            var tmp = Activator.CreateInstance(typeof(T));

//////            //loop through the properties of the object you want to covert:          
//////            foreach (PropertyInfo pi in obj.GetType().GetProperties())
//////            {
//////                try
//////                {

//////                    //get the value of property and try 
//////                    //to assign it to the property of T type object:
//////                    tmp.GetType().GetProperty(pi.Name).SetValue(tmp,
//////                                              pi.GetValue(obj, null), null);
//////                }
//////                catch { }
//////            }

//////            //return the T type object:         
//////            return tmp;
//////        }
//////        public static object ToNonAnonymousList<T>(this List<T> list, Type t)
//////        {
//////            //define system Type representing List of objects of T type:
//////            Type genericType = typeof(List<>).MakeGenericType(t);

//////            //create an object instance of defined type:
//////            object l = Activator.CreateInstance(genericType);

//////            //get method Add from from the list:
//////            MethodInfo addMethod = l.GetType().GetMethod("Add");

//////            //loop through the calling list:
//////            foreach (T item in list)
//////            {
//////                //convert each object of the list into T object by calling extension ToType<T>()
//////                //Add this object to newly created list:
//////                //  addMethod.Invoke(l, new[] {item.ToType(t)});
//////            }
//////            //return List of T objects:
//////            return l;
//////        }

//////        public static void InitializeUserInfo(Folder.User user)
//////        {
//////            CurrentUser = new UserInfo();

//////            using (MainDataContext context = new MainDataContext())
//////            {
//////                List<Guid> centerRoles = user.AllRoles.Where(t => t.ParentID == Constants.FolderCenterID).Select(t => t.ID).ToList();
//////                CurrentUser.CenterIDs = context.Centers.Where(t => centerRoles.Contains(t.IDInFolder)).Select(t => t.ID).ToList();

//////                List<Guid> statusRoles = user.AllRoles.Where(t => t.ParentID == Constants.FolderStatusID).Select(t => t.ID).ToList();
//////                CurrentUser.RequestStepsIDs = context.RequestSteps.Where(t => statusRoles.Contains(t.FolderID.Value)).Select(t => t.ID).ToList();

//////                CurrentUser.UserID = user.ID;
//////                CurrentUser.UserName = user.Username;
//////                user.Fullname= string.Format("{0}/{1}", Folder.User.Current.Fullname, Folder.User.Current.RoleNames);
//////                CurrentUser.FullName = user.Fullname;
//////            }
//////        }



//////    }

//////    public static class EnumTypeNameHelper
//////    {
//////        #region Constructor

//////        static EnumTypeNameHelper()
//////        {
//////            // Create enum-to-friendly name dictionary
//////            ConnectionTypeFriendlyNames = new Dictionary<DB.ConnectionType, string>
//////                {
//////                              {DB.ConnectionType.Normal, "عادی"},
//////                              {DB.ConnectionType.PCM, "پی سی ام"},

//////                };

//////            // Create friendly name-to-enum dictionary
//////            ConnectionTypeEnumValues = ConnectionTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);



//////            // Create enum-to-friendly name dictionary
//////            RoundTypeFriendlyNames = new Dictionary<DB.RoundType, string>
//////                {
//////                              {DB.RoundType.Diamond, "الماس"},
//////                              {DB.RoundType.Gold, "طلا"},
//////                              {DB.RoundType.Silver, "نقره"},
//////                              {DB.RoundType.Express, "سفارشی"},
//////                              {DB.RoundType.Old, "رند قدیم"},                            
//////                };

//////            // Create friendly name-to-enum dictionary
//////            RoundTypeEnumValues = RoundTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);


//////            // Create enum-to-friendly name dictionary
//////            DocumentTypeFriendlyNames = new Dictionary<DB.DocumentType, string>
//////                {
//////                            {DB.DocumentType.Document, "مدرک"},
//////                            {DB.DocumentType.Permission, "مجوز"},
//////                             {DB.DocumentType.Contract, "قرارداد"},

//////                };

//////            // Create friendly name-to-enum dictionary
//////            DocumentTypeEnumValues = DocumentTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);


//////            // Create enum-to-friendly name dictionary
//////            AddressTypeFriendlyNames = new Dictionary<DB.AddressType, string>
//////                {
//////                            {DB.AddressType.Contact, "مکاتبه"},
//////                            {DB.AddressType.Install, "نصب"},

//////                };

//////            // Create friendly name-to-enum dictionary
//////            AddressTypeEnumValues = AddressTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);

//////            // Create enum-to-friendly name dictionary
//////            TelephoneTypeFriendlyNames = new Dictionary<DB.TelephoneType, string>
//////                {
//////                            {DB.TelephoneType.Normal, "عادی"},
//////                            {DB.TelephoneType.Public, "همگانی"},
//////                            {DB.TelephoneType.Temporary, "موقت"},
//////                };

//////            // Create friendly name-to-enum dictionary
//////            TelephoneTypeEnumValues = TelephoneTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);

//////            PersonTypeFriendlyNames = new Dictionary<DB.PersonType, string>
//////                {
//////                            {DB.PersonType.Person, "حقیقی"},
//////                            {DB.PersonType.Company, "حقوقی"},
//////                            {DB.PersonType.Both, "هردو"},
//////                             {DB.PersonType.Nothing, "نامشخص"},

//////                };

//////            // Create friendly name-to-enum dictionary
//////            PersonTypeEnumValues = PersonTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);

//////            ChargingTypeFriendlyNames = new Dictionary<DB.ChargingType, string>
//////                {
//////                            {DB.ChargingType.Normal, "عادی"},
//////                            {DB.ChargingType.Business, "تجاری"},
//////                            {DB.ChargingType.Free, "رایگان"},

//////                };

//////            // Create friendly name-to-enum dictionary

//////            OrderTypeFriendlyNames = new Dictionary<DB.OrderType, string>
//////                {
//////                            {DB.OrderType.Normal, "عادی"},
//////                            {DB.OrderType.OutOfOrder, "خارج از نوبت"},                        

//////                };

//////            // Create friendly name-to-enum dictionary
//////            OrderTypeEnumValues = OrderTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);

//////            PosessionTypeFriendlyNames = new Dictionary<DB.PosessionType, string>
//////                {
//////                            {DB.PosessionType.Normal, "تام"},
//////                            {DB.PosessionType.PartTimeRented, "استیجاری نیمه وقت"},                        
//////                            {DB.PosessionType.FullTimeRented, "استیجاری تمام وقت"},    
//////                };

//////            // Create friendly name-to-enum dictionary
//////            PosessionTypeEnumValues = PosessionTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);

//////            Use3PercentTypeFriendlyNames = new Dictionary<DB.Use3PercentType, string>
//////                {
//////                            {DB.Use3PercentType.CabinetPost, "کافو-پست"},

//////                };

//////            // Create friendly name-to-enum dictionary
//////            Use3PercentTypeEnumValues = Use3PercentTypeFriendlyNames.ToDictionary(x => x.Value, x => (byte)x.Key);




//////        }

//////        #endregion
//////        #region Properties

//////        public static Dictionary<DB.ConnectionType, String> ConnectionTypeFriendlyNames { get; set; }

//////        public static Dictionary<String, byte> ConnectionTypeEnumValues { get; set; }


//////        public static Dictionary<DB.RoundType, String> RoundTypeFriendlyNames { get; set; }

//////        public static Dictionary<String, byte> RoundTypeEnumValues { get; set; }


//////        public static Dictionary<DB.ChargingType, String> ChargingTypeFriendlyNames { get; set; }




//////        public static Dictionary<DB.OrderType, String> OrderTypeFriendlyNames { get; set; }

//////        public static Dictionary<String, byte> OrderTypeEnumValues { get; set; }


//////        public static Dictionary<DB.PersonType, String> PersonTypeFriendlyNames { get; set; }

//////        public static Dictionary<String, byte> PersonTypeEnumValues { get; set; }


//////        public static Dictionary<DB.PosessionType, String> PosessionTypeFriendlyNames { get; set; }




//////        public static Dictionary<DB.TelephoneType, String> TelephoneTypeFriendlyNames { get; set; }

//////        public static Dictionary<String, byte> TelephoneTypeEnumValues { get; set; }


//////        public static Dictionary<DB.Use3PercentType, String> Use3PercentTypeFriendlyNames { get; set; }

//////        public static Dictionary<String, byte> Use3PercentTypeEnumValues { get; set; }


//////        public static Dictionary<DB.AddressType, String> AddressTypeFriendlyNames { get; set; }

//////        public static Dictionary<String, byte> AddressTypeEnumValues { get; set; }


//////        public static Dictionary<DB.DocumentType, String> DocumentTypeFriendlyNames { get; set; }

//////        public static Dictionary<String, byte> DocumentTypeEnumValues { get; set; }



//////        #endregion
//////    }


//////    public class UserInfo
//////    {
//////        public Guid UserID { get; set; }
//////        public string FullName { get; set; }
//////        public string UserName { get; set; }
//////        public List<int> CenterIDs { get; set; }
//////        public List<int> RequestStepsIDs { get; set; }
//////        private static List<Guid> userCenterIDs = new List<Guid>();
//////        public static List<Guid> UserCenterIDs
//////        {
//////            get
//////            {
//////                return userCenterIDs;
//////            }
//////            set
//////            {
//////                userCenterIDs = value;
//////            }
//////        }

//////        public UserInfo()
//////        {
//////            CenterIDs = new List<int>();
//////            RequestStepsIDs = new List<int>();
//////        }
//////    }

//////    public class SwitchCodeInfo
//////    {
//////        public long SwitchPreNo { get; set; }
//////        public byte PreCodeType { get; set; }
//////        public string PortNo { get; set; }
//////        public long TelephoneNo { get; set; }
//////    }

//////    public class StepStatusInfo
//////    {
//////        public int StepStatusID { get; set; }
//////        public int? ParentStepStatusID { get; set; }
//////        public int reqStepID { get; set; }      
//////        public string StatusResult { get; set; }
//////    }



//////    //public static partial class LinqExtensions
//////    //{ 
//////    //    public class Node<T> 
//////    //    { 
//////    //        internal Node() 
//////    //        { 
//////    //        } 
//////    //        public int Level; 
//////    //        public Node<T> Parent; 
//////    //        public T Item;        
//////    //    } 
//////    //    public static IEnumerable<Node<T>> ByHierarchy<T>(this IEnumerable<T> source, Func<T, bool> startWith, Func<T, T, bool> connectBy) 
//////    //    { 
//////    //        return source.ByHierarchy<T>(startWith, connectBy, null); 
//////    //    } 
//////    //    private static IEnumerable<Node<T>> ByHierarchy<T>(this IEnumerable<T> source, Func<T, bool> startWith, Func<T, T, bool> connectBy, Node<T> parent) 
//////    //    { 
//////    //        int level = (parent == null ? 0 : parent.Level + 1);
//////    //        if (source == null)                
//////    //            throw new ArgumentNullException("source");
//////    //        if (startWith == null)               
//////    //            throw new ArgumentNullException("startWith"); 
//////    //        if (connectBy == null)               
//////    //            throw new ArgumentNullException("connectBy"); 
//////    //        foreach (T value in from item in source    where startWith(item) select item) 
//////    //        {
//////    //            Node<T> newNode = new Node<T> 
//////    //            { 
//////    //                Level = level, Parent = parent, Item = value 
//////    //            }; 

//////    //            yield return newNode; 

//////    //            foreach (Node<T> subNode in source.ByHierarchy<T>(possibleSub => connectBy(value, possibleSub), connectBy, newNode)) 
//////    //            { 
//////    //                yield return subNode; 
//////    //            } 
//////    //        }
//////    //    } 
//////    //}

//////    public class HierarchyNode<T> where T : class
//////    {
//////        public T Entity { get; set; }
//////        public IEnumerable<HierarchyNode<T>> ChildNodes { get; set; }
//////        public int Depth { get; set; }
//////    }



//////    public static class LinqExtensionMethods
//////    {
//////        private static System.Collections.Generic.IEnumerable<HierarchyNode<TEntity>> CreateHierarchy<TEntity, TProperty>
//////          (IEnumerable<TEntity> allItems, TEntity parentItem,
//////          Func<TEntity, TProperty> idProperty, Func<TEntity, TProperty> parentIdProperty, int depth) where TEntity : class
//////        {
//////            IEnumerable<TEntity> childs;

//////            if (parentItem == null)
//////                childs = allItems.Where(i => parentIdProperty(i).Equals(default(TProperty)));
//////            else
//////                childs = allItems.Where(i => parentIdProperty(i).Equals(idProperty(parentItem)));

//////            if (childs.Count() > 0)
//////            {
//////                depth++;

//////                foreach (var item in childs)
//////                    yield return new HierarchyNode<TEntity>()
//////                    {
//////                        Entity = item,
//////                        ChildNodes = CreateHierarchy<TEntity, TProperty>
//////                            (allItems, item, idProperty, parentIdProperty, depth),
//////                        Depth = depth
//////                    };
//////            }
//////        }

//////        /// <summary>
//////        /// LINQ IEnumerable AsHierachy() extension method
//////        /// </summary>
//////        /// <typeparam name="TEntity">Entity class</typeparam>
//////        /// <typeparam name="TProperty">Property of entity class</typeparam>
//////        /// <param name="allItems">Flat collection of entities</param>
//////        /// <param name="idProperty">Reference to Id/Key of entity</param>
//////        /// <param name="parentIdProperty">Reference to parent Id/Key</param>
//////        /// <returns>Hierarchical structure of entities</returns>
//////        public static System.Collections.Generic.IEnumerable<HierarchyNode<TEntity>> AsHierarchy<TEntity, TProperty>
//////          (this IEnumerable<TEntity> allItems, Func<TEntity, TProperty> idProperty, Func<TEntity, TProperty> parentIdProperty)
//////          where TEntity : class
//////        {
//////            return CreateHierarchy(allItems, default(TEntity), idProperty, parentIdProperty, 0);
//////        }
//////    }

//////}
