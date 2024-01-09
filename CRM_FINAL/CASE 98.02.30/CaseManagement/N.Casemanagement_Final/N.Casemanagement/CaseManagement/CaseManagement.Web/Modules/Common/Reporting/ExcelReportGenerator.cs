using CaseManagement.Case.Entities;
using CaseManagement.StimulReport.Entities;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using Serenity.Data;
using Serenity.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Serenity.Reporting
{
    public static class ExcelReportGenerator
    {
        public static byte[] GeneratePackageBytes(List<ReportColumn> columns, IList rows,
            string sheetName = "Page1", string tableName = "Table1", TableStyles tableStyle = TableStyles.Medium2)
        {
            using (var package = GeneratePackage(columns, rows, sheetName, tableName, tableStyle))
                return package.GetAsByteArray();
        }

        public static ExcelPackage GeneratePackage(List<ReportColumn> columns, IList rows,
            string sheetName = "Page1", string tableName = "Table1", TableStyles tableStyle = TableStyles.Medium2)
        {
            /*  foreach(var item in columns)
              {
                  var a = item.DataType.Name;
              }*/

            List<Object> newList = new List<object>();
            Object object1 = new Object();


            foreach (var item in rows)
            {
                var a = item.GetType();
                 switch(a.Name)
                {
                    case "ActivityRequestTechnicalRow":
                        {
                            ActivityRequestTechnicalRow currentRow = item as CaseManagement.Case.Entities.ActivityRequestTechnicalRow;
                            //string date = currentRow.DiscoverLeakDate.ToString();  //CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short);

                            object1 = new
                            {
                                currentRow.Id,
                                currentRow.ActivityCode,
                                currentRow.ProvinceName,
                                currentRow.CycleName,
                                currentRow.IncomeFlowName,
                                currentRow.TotalLeakage,
                                DiscoverLeakDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.CreatedUserName,
                                currentRow.SendUserName,
                                SendDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.SendDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.StatusName
                            };

                            newList.Add(object1);
                            break;
                        }
                    case "ActivityRequestFinancialRow":
                        {
                            ActivityRequestFinancialRow currentRow = item as CaseManagement.Case.Entities.ActivityRequestFinancialRow;
                            //string date = currentRow.DiscoverLeakDate.ToString();  //CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short);

                            object1 = new
                            {
                                currentRow.Id,
                                currentRow.ActivityCode,
                                currentRow.ProvinceName,
                                currentRow.CycleName,
                                currentRow.IncomeFlowName,
                                currentRow.TotalLeakage,
                                DiscoverLeakDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.CreatedUserName,
                                currentRow.SendUserName,
                                SendDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.SendDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.StatusName
                            };

                            newList.Add(object1);
                            break;
                        }

                    case "ActivityRequestDenyRow":
                        {
                            ActivityRequestDenyRow  currentRow = item as CaseManagement.Case.Entities.ActivityRequestDenyRow;
                            //string date = currentRow.DiscoverLeakDate.ToString();  //CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short);

                            object1 = new
                            {
                                currentRow.Id,
                                currentRow.ActivityCode,
                                currentRow.ProvinceName,
                                currentRow.CycleName,
                                currentRow.IncomeFlowName,
                                currentRow.TotalLeakage,
                                DiscoverLeakDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.CreatedUserName,
                                currentRow.SendUserName,
                                SendDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.SendDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.StatusName
                            };

                            newList.Add(object1);
                            break;
                        }

                    case "ActivityRequestPenddingRow":
                        {
                            ActivityRequestPenddingRow currentRow = item as CaseManagement.Case.Entities.ActivityRequestPenddingRow;
                            //string date = currentRow.DiscoverLeakDate.ToString();  //CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short);

                            object1 = new
                            {
                                currentRow.Id,
                                currentRow.ActivityCode,
                                currentRow.ProvinceName,
                                currentRow.CycleName,
                                currentRow.IncomeFlowName,
                                currentRow.TotalLeakage,
                                DiscoverLeakDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.CreatedUserName,
                                currentRow.SendUserName,
                                SendDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.SendDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.StatusName
                            };

                            newList.Add(object1);
                            break;
                        }

                    case "ActivityRequestLeaderRow":
                        {
                            ActivityRequestLeaderRow currentRow = item as CaseManagement.Case.Entities.ActivityRequestLeaderRow;
                            //string date = currentRow.DiscoverLeakDate.ToString();  //CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short);

                            object1 = new
                            {
                                currentRow.Id,
                                currentRow.ActivityCode,
                                currentRow.ProvinceName,
                                currentRow.CycleName,
                                currentRow.IncomeFlowName,
                                currentRow.TotalLeakage,
                                DiscoverLeakDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short),
                                EndDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.EndDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.CreatedUserName,
                                currentRow.SendUserName,
                                SendDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.SendDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.StatusName
                            };

                            newList.Add(object1);
                            break;
                        }

                    case "ActivityRequestConfirmRow":
                        {
                            ActivityRequestConfirmRow currentRow = item as CaseManagement.Case.Entities.ActivityRequestConfirmRow;
                            //string date = currentRow.DiscoverLeakDate.ToString();  //CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short);

                            object1 = new
                            {
                                currentRow.Id,
                                currentRow.ActivityCode,
                                currentRow.ProvinceName,
                                currentRow.CycleName,
                                CreatedDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.CreatedDate, CaseManagement.Case.Helper.DateStringType.Short),
                                DiscoverLeakDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short),
                                EndDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.EndDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.CycleCost,
                                currentRow.DelayedCost,
                                currentRow.TotalLeakage,
                                currentRow.RecoverableLeakage ,
                                currentRow.Recovered
                            };

                            newList.Add(object1);
                            break;
                        }

                    case "ProvinceProgramRow":
                        {
                            ProvinceProgramRow currentRow = item as CaseManagement.Case.Entities.ProvinceProgramRow;
                            //string date = currentRow.DiscoverLeakDate.ToString();  //CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short);

                            object1 = new
                            {
                                currentRow.ProvinceName,
                                currentRow.Year,
                                currentRow.Program,
                                currentRow.TotalLeakage,
                                currentRow.RecoverableLeakage,
                                currentRow.Recovered,
                                currentRow.PercentTotalLeakage ,
                                currentRow.PercentRecoverableLeakage,
                                currentRow.PercentRecovered,
                                currentRow.PercentRecoveredonTotal,
                                currentRow.PercentTotal94to95,
                                currentRow.PercentRecovered94to95
                            };

                            newList.Add(object1);
                            break;
                        }

                    case "ActivityRequestReportRow":
                        {
                            CaseManagement.StimulReport.Entities.ActivityRequestReportRow currentRow = item as CaseManagement.StimulReport.Entities.ActivityRequestReportRow;
                            //string date = currentRow.DiscoverLeakDate.ToString();  //CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short);

                            object1 = new
                            {
                                currentRow.Id,
                                currentRow.ActivityCode,
                                currentRow.ProvinceName,
                                currentRow.IncomeFlowName,
                                CreatedDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.CreatedDate, CaseManagement.Case.Helper.DateStringType.Short),
                                DiscoverLeakDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short),
                                EndDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.EndDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.TotalLeakage,
                                currentRow.RecoverableLeakage,
                                currentRow.Recovered
                            };

                            newList.Add(object1);
                            break;
                        }

                    case "ActivityRequestDetailRow":
                        {
                            ActivityRequestDetailRow currentRow = item as ActivityRequestDetailRow;
                            //string date = currentRow.DiscoverLeakDate.ToString();  //CaseManagement.Case.Helper.GetPersianDate(currentRow.DiscoverLeakDate, CaseManagement.Case.Helper.DateStringType.Short);

                            object1 = new
                            {
                                currentRow.Id,
                                currentRow.ActivityCode,
                                currentRow.ProvinceName,
                                currentRow.CycleName,
                                CreatedDate = CaseManagement.Case.Helper.GetPersianDate(currentRow.CreatedDate, CaseManagement.Case.Helper.DateStringType.Short),
                                currentRow.CycleCost,
                                currentRow.DelayedCost,
                                currentRow.TotalLeakage,
                                currentRow.RecoverableLeakage,
                                currentRow.Recovered
                            };

                            newList.Add(object1);
                            break;
                        }
                      

                }
            }

            List<ReportColumn> ColumnList = new List<ReportColumn>();
            if (rows.Count != 0)
            {
                var TableName = rows[0].GetType().Name;

            //    if (TableName == "ActivityRequestTechnicalRow")
             //   {
               switch (TableName)
               {
                   case "ActivityRequestTechnicalRow":
                       {
                           ReportColumn ID = new ReportColumn();
                           ID.Title = "شناسه";
                           ID.Name = "Id";
                           ColumnList.Add(ID);
                           ReportColumn ActivityCode = new ReportColumn();
                           ActivityCode.Title = "کد فعالیت";
                           ActivityCode.Name = "ActivityCode";
                           ColumnList.Add(ActivityCode);
                           ReportColumn ProvinceName = new ReportColumn();
                           ProvinceName.Title = "استان";
                           ProvinceName.Name = "ProvinceName";
                           ColumnList.Add(ProvinceName);
                           ReportColumn CycleName = new ReportColumn();
                           CycleName.Title = "دوره";
                           CycleName.Name = "CycleName";
                           ColumnList.Add(CycleName);
                           ReportColumn IncomeFlowName = new ReportColumn();
                           IncomeFlowName.Title = "جریان درآمدی";
                           IncomeFlowName.Name = "IncomeFlowName";
                           ColumnList.Add(IncomeFlowName);
                           ReportColumn TotalLeakage = new ReportColumn();
                           TotalLeakage.Title = "نشتی شناسایی شده کل";
                           TotalLeakage.Name = "TotalLeakage";
                           ColumnList.Add(TotalLeakage);
                           ReportColumn DiscoverLeakDate = new ReportColumn();
                           DiscoverLeakDate.Title = "تاریخ شناسایی نشتی ";
                           DiscoverLeakDate.Name = "DiscoverLeakDate";
                           ColumnList.Add(DiscoverLeakDate);
                           ReportColumn CreatedUserName = new ReportColumn();
                           CreatedUserName.Title = "کاربر ایجادکننده ";
                           CreatedUserName.Name = "CreatedUserName";
                           ColumnList.Add(CreatedUserName);
                           ReportColumn SendUserName = new ReportColumn();
                           SendUserName.Title = "کاربر ارسال کننده";
                           SendUserName.Name = "SendUserName";
                           ColumnList.Add(SendUserName);
                           ReportColumn SendDate = new ReportColumn();
                           SendDate.Title = "تاریخ ارسال ";
                           SendDate.Name = "SendDate";
                           ColumnList.Add(SendDate);
                           ReportColumn StatusName = new ReportColumn();
                           StatusName.Title = "وضعیت ";
                           StatusName.Name = "StatusName";
                           ColumnList.Add(StatusName);
                           break;
                       }
                   case "ActivityRequestFinancialRow":
                       {
                           ReportColumn ID = new ReportColumn();
                           ID.Title = "شناسه";
                           ID.Name = "Id";
                           ColumnList.Add(ID);
                           ReportColumn ActivityCode = new ReportColumn();
                           ActivityCode.Title = "کد فعالیت";
                           ActivityCode.Name = "ActivityCode";
                           ColumnList.Add(ActivityCode);
                           ReportColumn ProvinceName = new ReportColumn();
                           ProvinceName.Title = "استان";
                           ProvinceName.Name = "ProvinceName";
                           ColumnList.Add(ProvinceName);
                           ReportColumn CycleName = new ReportColumn();
                           CycleName.Title = "دوره";
                           CycleName.Name = "CycleName";
                           ColumnList.Add(CycleName);
                           ReportColumn IncomeFlowName = new ReportColumn();
                           IncomeFlowName.Title = "جریان درآمدی";
                           IncomeFlowName.Name = "IncomeFlowName";
                           ColumnList.Add(IncomeFlowName);
                           ReportColumn TotalLeakage = new ReportColumn();
                           TotalLeakage.Title = "نشتی شناسایی شده کل";
                           TotalLeakage.Name = "TotalLeakage";
                           ColumnList.Add(TotalLeakage);
                           ReportColumn DiscoverLeakDate = new ReportColumn();
                           DiscoverLeakDate.Title = "تاریخ شناسایی نشتی ";
                           DiscoverLeakDate.Name = "DiscoverLeakDate";
                           ColumnList.Add(DiscoverLeakDate);
                           ReportColumn CreatedUserName = new ReportColumn();
                           CreatedUserName.Title = "کاربر ایجادکننده ";
                           CreatedUserName.Name = "CreatedUserName";
                           ColumnList.Add(CreatedUserName);
                           ReportColumn SendUserName = new ReportColumn();
                           SendUserName.Title = "کاربر ارسال کننده";
                           SendUserName.Name = "SendUserName";
                           ColumnList.Add(SendUserName);
                           ReportColumn SendDate = new ReportColumn();
                           SendDate.Title = "تاریخ ارسال ";
                           SendDate.Name = "SendDate";
                           ColumnList.Add(SendDate);
                           ReportColumn StatusName = new ReportColumn();
                           StatusName.Title = "وضعیت ";
                           StatusName.Name = "StatusName";
                           ColumnList.Add(StatusName);
                           break;
                       }

                   case "ActivityRequestDenyRow":
                       {
                           ReportColumn ID = new ReportColumn();
                           ID.Title = "شناسه";
                           ID.Name = "Id";
                           ColumnList.Add(ID);
                           ReportColumn ActivityCode = new ReportColumn();
                           ActivityCode.Title = "کد فعالیت";
                           ActivityCode.Name = "ActivityCode";
                           ColumnList.Add(ActivityCode);
                           ReportColumn ProvinceName = new ReportColumn();
                           ProvinceName.Title = "استان";
                           ProvinceName.Name = "ProvinceName";
                           ColumnList.Add(ProvinceName);
                           ReportColumn CycleName = new ReportColumn();
                           CycleName.Title = "دوره";
                           CycleName.Name = "CycleName";
                           ColumnList.Add(CycleName);
                           ReportColumn IncomeFlowName = new ReportColumn();
                           IncomeFlowName.Title = "جریان درآمدی";
                           IncomeFlowName.Name = "IncomeFlowName";
                           ColumnList.Add(IncomeFlowName);
                           ReportColumn TotalLeakage = new ReportColumn();
                           TotalLeakage.Title = "نشتی شناسایی شده کل";
                           TotalLeakage.Name = "TotalLeakage";
                           ColumnList.Add(TotalLeakage);
                           ReportColumn DiscoverLeakDate = new ReportColumn();
                           DiscoverLeakDate.Title = "تاریخ شناسایی نشتی ";
                           DiscoverLeakDate.Name = "DiscoverLeakDate";
                           ColumnList.Add(DiscoverLeakDate);
                           ReportColumn CreatedUserName = new ReportColumn();
                           CreatedUserName.Title = "کاربر ایجادکننده ";
                           CreatedUserName.Name = "CreatedUserName";
                           ColumnList.Add(CreatedUserName);
                           ReportColumn SendUserName = new ReportColumn();
                           SendUserName.Title = "کاربر ارسال کننده";
                           SendUserName.Name = "SendUserName";
                           ColumnList.Add(SendUserName);
                           ReportColumn SendDate = new ReportColumn();
                           SendDate.Title = "تاریخ ارسال ";
                           SendDate.Name = "SendDate";
                           ColumnList.Add(SendDate);
                           ReportColumn StatusName = new ReportColumn();
                           StatusName.Title = "وضعیت ";
                           StatusName.Name = "StatusName";
                           ColumnList.Add(StatusName);
                           break;
                       }

                   case "ActivityRequestPenddingRow":
                       {
                           ReportColumn ID = new ReportColumn();
                           ID.Title = "شناسه";
                           ID.Name = "Id";
                           ColumnList.Add(ID);
                           ReportColumn ActivityCode = new ReportColumn();
                           ActivityCode.Title = "کد فعالیت";
                           ActivityCode.Name = "ActivityCode";
                           ColumnList.Add(ActivityCode);
                           ReportColumn ProvinceName = new ReportColumn();
                           ProvinceName.Title = "استان";
                           ProvinceName.Name = "ProvinceName";
                           ColumnList.Add(ProvinceName);
                           ReportColumn CycleName = new ReportColumn();
                           CycleName.Title = "دوره";
                           CycleName.Name = "CycleName";
                           ColumnList.Add(CycleName);
                           ReportColumn IncomeFlowName = new ReportColumn();
                           IncomeFlowName.Title = "جریان درآمدی";
                           IncomeFlowName.Name = "IncomeFlowName";
                           ColumnList.Add(IncomeFlowName);
                           ReportColumn TotalLeakage = new ReportColumn();
                           TotalLeakage.Title = "نشتی شناسایی شده کل";
                           TotalLeakage.Name = "TotalLeakage";
                           ColumnList.Add(TotalLeakage);
                           ReportColumn DiscoverLeakDate = new ReportColumn();
                           DiscoverLeakDate.Title = "تاریخ شناسایی نشتی ";
                           DiscoverLeakDate.Name = "DiscoverLeakDate";
                           ColumnList.Add(DiscoverLeakDate);
                           ReportColumn CreatedUserName = new ReportColumn();
                           CreatedUserName.Title = "کاربر ایجادکننده ";
                           CreatedUserName.Name = "CreatedUserName";
                           ColumnList.Add(CreatedUserName);
                           ReportColumn SendUserName = new ReportColumn();
                           SendUserName.Title = "کاربر ارسال کننده";
                           SendUserName.Name = "SendUserName";
                           ColumnList.Add(SendUserName);
                           ReportColumn SendDate = new ReportColumn();
                           SendDate.Title = "تاریخ ارسال ";
                           SendDate.Name = "SendDate";
                           ColumnList.Add(SendDate);
                           ReportColumn StatusName = new ReportColumn();
                           StatusName.Title = "وضعیت ";
                           StatusName.Name = "StatusName";
                           ColumnList.Add(StatusName);
                           break;
                       }

                   case "ActivityRequestLeaderRow":
                       {
                           ReportColumn ID = new ReportColumn();
                           ID.Title = "شناسه";
                           ID.Name = "Id";
                           ColumnList.Add(ID);
                           ReportColumn ActivityCode = new ReportColumn();
                           ActivityCode.Title = "کد فعالیت";
                           ActivityCode.Name = "ActivityCode";
                           ColumnList.Add(ActivityCode);
                           ReportColumn ProvinceName = new ReportColumn();
                           ProvinceName.Title = "استان";
                           ProvinceName.Name = "ProvinceName";
                           ColumnList.Add(ProvinceName);
                           ReportColumn CycleName = new ReportColumn();
                           CycleName.Title = "دوره";
                           CycleName.Name = "CycleName";
                           ColumnList.Add(CycleName);
                           ReportColumn IncomeFlowName = new ReportColumn();
                           IncomeFlowName.Title = "جریان درآمدی";
                           IncomeFlowName.Name = "IncomeFlowName";
                           ColumnList.Add(IncomeFlowName);
                           ReportColumn TotalLeakage = new ReportColumn();
                           TotalLeakage.Title = "نشتی شناسایی شده کل";
                           TotalLeakage.Name = "TotalLeakage";
                           ColumnList.Add(TotalLeakage);
                           ReportColumn DiscoverLeakDate = new ReportColumn();
                           DiscoverLeakDate.Title = "تاریخ شناسایی نشتی ";
                           DiscoverLeakDate.Name = "DiscoverLeakDate";
                           ColumnList.Add(DiscoverLeakDate);
                           ReportColumn EndDate = new ReportColumn();
                           EndDate.Title = "تاریخ اتمام ";
                           EndDate.Name = "EndDate";
                           ColumnList.Add(EndDate);
                           ReportColumn CreatedUserName = new ReportColumn();
                           CreatedUserName.Title = "کاربر ایجادکننده ";
                           CreatedUserName.Name = "CreatedUserName";
                           ColumnList.Add(CreatedUserName);
                           ReportColumn SendUserName = new ReportColumn();
                           SendUserName.Title = "کاربر ارسال کننده";
                           SendUserName.Name = "SendUserName";
                           ColumnList.Add(SendUserName);
                           ReportColumn SendDate = new ReportColumn();
                           SendDate.Title = "تاریخ ارسال ";
                           SendDate.Name = "SendDate";
                           ColumnList.Add(SendDate);
                           ReportColumn StatusName = new ReportColumn();
                           StatusName.Title = "وضعیت ";
                           StatusName.Name = "StatusName";
                           ColumnList.Add(StatusName);
                           break;
                       }

                   case "ActivityRequestConfirmRow":
                       {
                           ReportColumn ID = new ReportColumn();
                           ID.Title = "شناسه";
                           ID.Name = "Id";
                           ColumnList.Add(ID);
                           ReportColumn ActivityCode = new ReportColumn();
                           ActivityCode.Title = "کد فعالیت";
                           ActivityCode.Name = "ActivityCode";
                           ColumnList.Add(ActivityCode);
                           ReportColumn ProvinceName = new ReportColumn();
                           ProvinceName.Title = "استان";
                           ProvinceName.Name = "ProvinceName";
                           ColumnList.Add(ProvinceName);
                           ReportColumn CycleName = new ReportColumn();
                           CycleName.Title = "دوره";
                           CycleName.Name = "CycleName";
                           ColumnList.Add(CycleName);
                           ReportColumn CreatedDate = new ReportColumn();
                           CreatedDate.Title = "تاریخ ایجاد";
                           CreatedDate.Name = "CreatedDate";
                           ColumnList.Add(CreatedDate);
                           ReportColumn DiscoverLeakDate = new ReportColumn();
                           DiscoverLeakDate.Title = "تاریخ شناسایی نشتی ";
                           DiscoverLeakDate.Name = "DiscoverLeakDate";
                           ColumnList.Add(DiscoverLeakDate);
                           ReportColumn EndDate = new ReportColumn();
                           EndDate.Title = "تاریخ اتمام ";
                           EndDate.Name = "EndDate";
                           ColumnList.Add(EndDate);
                           ReportColumn CycleCost = new ReportColumn();
                           CycleCost.Title = "مبلغ سیکل";
                           CycleCost.Name = "CycleCost";
                           ColumnList.Add(CycleCost);
                           ReportColumn DelayedCost = new ReportColumn();
                           DelayedCost.Title = "مبلغ معوق";
                           DelayedCost.Name = "DelayedCost";
                           ColumnList.Add(DelayedCost);
                           ReportColumn TotalLeakage = new ReportColumn();
                           TotalLeakage.Title = "نشتی شناسایی شده کل";
                           TotalLeakage.Name = "TotalLeakage";
                           ColumnList.Add(TotalLeakage);
                           ReportColumn RecoverableLeakage = new ReportColumn();
                           RecoverableLeakage.Title = "نشتی شناسایی شده قابل وصول";
                           RecoverableLeakage.Name = "RecoverableLeakage";
                           ColumnList.Add(RecoverableLeakage);
                           ReportColumn Recovered = new ReportColumn();
                           Recovered.Title = "مبلغ مصوب";
                           Recovered.Name = "Recovered";
                           ColumnList.Add(Recovered);

                           break;
                       }

                   case "ProvinceProgramRow":
                       {
                           ReportColumn ProvinceName = new ReportColumn();
                           ProvinceName.Title = "استان";
                           ProvinceName.Name = "ProvinceName";
                           ColumnList.Add(ProvinceName);
                           ReportColumn Year = new ReportColumn();
                           Year.Title = "سال";
                           Year.Name = "Year";
                           ColumnList.Add(Year);
                           ReportColumn TotalLeakage = new ReportColumn();
                           TotalLeakage.Title = "نشتی  کل";
                           TotalLeakage.Name = "TotalLeakage";
                           ColumnList.Add(TotalLeakage);
                           ReportColumn RecoverableLeakage = new ReportColumn();
                           RecoverableLeakage.Title = "نشتی قابل وصول";
                           RecoverableLeakage.Name = "RecoverableLeakage";
                           ColumnList.Add(RecoverableLeakage);
                           ReportColumn Recovered = new ReportColumn();
                           Recovered.Title = "مبلغ مصوب";
                           Recovered.Name = "Recovered";
                           ColumnList.Add(Recovered);
                           ReportColumn PercentTotalLeakage = new ReportColumn();
                           PercentTotalLeakage.Title = "درصد نشتی کل ";
                           PercentTotalLeakage.Name = "PercentTotalLeakage";
                           ColumnList.Add(PercentTotalLeakage);
                           ReportColumn PercentRecoverableLeakage = new ReportColumn();
                           PercentRecoverableLeakage.Title = "درصد نشتی قابل وصول ";
                           PercentRecoverableLeakage.Name = "PercentRecoverableLeakage";
                           ColumnList.Add(PercentRecoverableLeakage);
                           ReportColumn PercentRecovered = new ReportColumn();
                           PercentRecovered.Title = "درصد مصوب ";
                           PercentRecovered.Name = "PercentRecovered";
                           ColumnList.Add(PercentRecovered);
                           ReportColumn PercentRecoveredonTotal = new ReportColumn();
                           PercentRecoveredonTotal.Title = "درصد مصوب به نشتی کل";
                           PercentRecoveredonTotal.Name = "PercentRecoveredonTotal";
                           ColumnList.Add(PercentRecoveredonTotal);
                           ReportColumn PercentTotal94to95 = new ReportColumn();
                           PercentTotal94to95.Title = "درصد روند نشتی کل 94 به 95";
                           PercentTotal94to95.Name = "PercentTotal94to95";
                           ColumnList.Add(PercentTotal94to95);
                           ReportColumn PercentRecovered94to95 = new ReportColumn();
                           PercentRecovered94to95.Title = "درصد روند مصوب 94 به 95";
                           PercentRecovered94to95.Name = "PercentRecovered94to95";
                           ColumnList.Add(PercentRecovered94to95);

                           break;
                       }
                   case "ActivityRequestReportRow":
                       {
                           ReportColumn ID = new ReportColumn();
                           ID.Title = "شناسه";
                           ID.Name = "Id";
                           ColumnList.Add(ID);
                           ReportColumn ActivityCode = new ReportColumn();
                           ActivityCode.Title = "کد فعالیت";
                           ActivityCode.Name = "ActivityCode";
                           ColumnList.Add(ActivityCode);
                           ReportColumn ProvinceName = new ReportColumn();
                           ProvinceName.Title = "استان";
                           ProvinceName.Name = "ProvinceName";
                           ColumnList.Add(ProvinceName);
                           ReportColumn IncomeFlowName = new ReportColumn();
                           IncomeFlowName.Title = "جریان درآمدی";
                           IncomeFlowName.Name = "IncomeFlowName";
                           ColumnList.Add(IncomeFlowName);
                           ReportColumn CreatedDate = new ReportColumn();
                           CreatedDate.Title = "تاریخ ایجاد";
                           CreatedDate.Name = "CreatedDate";
                           ColumnList.Add(CreatedDate);
                           ReportColumn DiscoverLeakDate = new ReportColumn();
                           DiscoverLeakDate.Title = "تاریخ شناسایی نشتی ";
                           DiscoverLeakDate.Name = "DiscoverLeakDate";
                           ColumnList.Add(DiscoverLeakDate);
                           ReportColumn EndDate = new ReportColumn();
                           EndDate.Title = "تاریخ اتمام ";
                           EndDate.Name = "EndDate";
                           ColumnList.Add(EndDate);
                           ReportColumn TotalLeakage = new ReportColumn();
                           TotalLeakage.Title = "نشتی شناسایی شده کل";
                           TotalLeakage.Name = "TotalLeakage";
                           ColumnList.Add(TotalLeakage);
                           ReportColumn RecoverableLeakage = new ReportColumn();
                           RecoverableLeakage.Title = "نشتی شناسایی شده قابل وصول";
                           RecoverableLeakage.Name = "RecoverableLeakage";
                           ColumnList.Add(RecoverableLeakage);
                           ReportColumn Recovered = new ReportColumn();
                           Recovered.Title = "مبلغ مصوب";
                           Recovered.Name = "Recovered";
                           ColumnList.Add(Recovered);

                           break;
                       }
                   case "ActivityRequestDetailRow":
                       {
                           ReportColumn ID = new ReportColumn();
                           ID.Title = "شناسه";
                           ID.Name = "Id";
                           ColumnList.Add(ID);
                           ReportColumn ActivityCode = new ReportColumn();
                           ActivityCode.Title = "کد فعالیت";
                           ActivityCode.Name = "ActivityCode";
                           ColumnList.Add(ActivityCode);
                           ReportColumn ProvinceName = new ReportColumn();
                           ProvinceName.Title = "استان";
                           ProvinceName.Name = "ProvinceName";
                           ColumnList.Add(ProvinceName);
                           ReportColumn CycleName = new ReportColumn();
                           CycleName.Title = "دوره";
                           CycleName.Name = "CycleName";
                           ColumnList.Add(CycleName);
                           ReportColumn CreatedDate = new ReportColumn();
                           CreatedDate.Title = "تاریخ ایجاد";
                           CreatedDate.Name = "CreatedDate";
                           ColumnList.Add(CreatedDate);
                           ReportColumn CycleCost = new ReportColumn();
                           CycleCost.Title = "مبلغ سیکل";
                           CycleCost.Name = "CycleCost";
                           ColumnList.Add(CycleCost);
                           ReportColumn DelayedCost = new ReportColumn();
                           DelayedCost.Title = "مبلغ معوق";
                           DelayedCost.Name = "DelayedCost";
                           ColumnList.Add(DelayedCost);
                           ReportColumn TotalLeakage = new ReportColumn();
                           TotalLeakage.Title = "نشتی شناسایی شده کل";
                           TotalLeakage.Name = "TotalLeakage";
                           ColumnList.Add(TotalLeakage);
                           ReportColumn RecoverableLeakage = new ReportColumn();
                           RecoverableLeakage.Title = "نشتی شناسایی شده قابل وصول";
                           RecoverableLeakage.Name = "RecoverableLeakage";
                           ColumnList.Add(RecoverableLeakage);
                           ReportColumn Recovered = new ReportColumn();
                           Recovered.Title = "مبلغ مصوب";
                           Recovered.Name = "Recovered";
                           ColumnList.Add(Recovered);

                           break;
                       }

                }
            }
            var package = new ExcelPackage();
            var workbook = package.Workbook;
            var worksheet = package.Workbook.Worksheets.Add(sheetName);
            if (rows.Count != 0)
            {
                PopulateSheet(worksheet, ColumnList, newList, tableName, tableStyle);
            }
            else {
                PopulateSheet(worksheet, columns, newList, tableName, tableStyle); 
            }

            return package;
        }

        private static Type[] DateTimeTypes = new[]
        {
            typeof(DateTime),
            typeof(DateTime?),
            typeof(TimeSpan),
            typeof(TimeSpan?)
        };

        private static string FixFormatSpecifier(string format, Type dataType)
        {
            if (string.IsNullOrEmpty(format))
                return format;

            if (format.IndexOf('f') >= 0 &&
                Array.IndexOf(DateTimeTypes, dataType) >= 0)
                return format.Replace('f', '0');

            return format;
        }

        public static void PopulateSheet(ExcelWorksheet worksheet, List<ReportColumn> columns, IList rows,
            string tableName = "Table1", TableStyles tableStyle = TableStyles.Medium2)
        {
            if (columns == null)
                throw new ArgumentNullException("columns");

            if (rows == null)
                throw new ArgumentNullException("rows");

            Field[] fields = null;
            TypeAccessor accessor = null;

            var colCount = columns.Count;

            int endCol = colCount;
            int endRow = rows.Count + 1;

            var header = worksheet.Cells[1, 1, 1, columns.Count];
            header.LoadFromArrays(new List<object[]>
            {
                columns.ConvertAll(x => (x.Title ?? x.Name)).ToArray()
            });

            var dataList = new List<object[]>();
            foreach (var obj in rows)
            {
                var data = new object[colCount];
                var row = obj as Row;
                if (row != null)
                {
                    if (fields == null)
                    {
                        fields = new Field[colCount];
                        for (var i = 0; i < columns.Count; i++)
                        {
                            var n = columns[i].Name;
                            fields[i] = row.FindFieldByPropertyName(n) ?? row.FindField(n);
                        }
                    }
                }
                else if (obj != null)
                {
                    if (accessor == null)
                        accessor = TypeAccessor.Create(obj.GetType());
                }

                for (var c = 0; c < colCount; c++)
                {
                    if (row != null)
                    {
                        data[c] = fields[c].AsObject(row);
                    }
                    else if (obj != null)
                    {
                        data[c] = accessor[obj, columns[c].Name];
                    }
                }

                dataList.Add(data);
            }

            if (rows.Count > 0)
            {
                var dataRange = worksheet.Cells[2, 1, endRow, endCol];
                dataRange.LoadFromArrays(dataList);
            }

            var tableRange = worksheet.Cells[1, 1, endRow, endCol];
            var table = worksheet.Tables.Add(tableRange, tableName);
            table.TableStyle = tableStyle;

            for (var i = 1; i <= endCol; i++)
            {
                var column = columns[i - 1];
                if (!column.Format.IsEmptyOrNull())
                    worksheet.Column(i).Style.Numberformat.Format = FixFormatSpecifier(column.Format, column.DataType);
            }

            worksheet.Cells[1, 1, Math.Min(endRow, 250), endCol].AutoFitColumns(1, 100);

            for (var colNum = 1; colNum <= endCol; colNum++)
            {
                var col = columns[colNum - 1];
                var decorator = col.Decorator;
                if (decorator != null)
                {
                    for (var rowNum = 2; rowNum <= endRow; rowNum++)
                    {
                        var obj = rows[rowNum - 2];
                        var row = obj as Row;

                        decorator.Item = obj;
                        decorator.Name = col.Name;
                        decorator.Format = null;
                        decorator.Background = Color.Empty;
                        decorator.Foreground = Color.Empty;

                        object value;
                        if (row != null)
                        {
                            value = fields[colNum - 1].AsObject(row);
                        }
                        else if (obj != null)
                        {
                            value = accessor[obj, col.Name];
                        }
                        else
                            continue;

                        decorator.Value = value;
                        decorator.Decorate();

                        if (decorator.Background != Color.Empty ||
                            decorator.Foreground != Color.Empty ||
                            !Object.Equals(decorator.Value, value) ||
                            decorator.Format != null)
                        {
                            var cell = worksheet.Cells[rowNum, colNum];

                            if (decorator.Background != Color.Empty)
                            {
                                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                cell.Style.Fill.BackgroundColor.SetColor(decorator.Background);
                            }

                            if (decorator.Foreground != Color.Empty)
                                cell.Style.Font.Color.SetColor(decorator.Foreground);

                            if (decorator.Format != null)
                                cell.Style.Numberformat.Format = FixFormatSpecifier(decorator.Format, col.DataType);

                            if (!Object.Equals(decorator.Value, value))
                                cell.Value = decorator.Value;
                        }
                    }
                }
            }
        }


    }
}
