using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseManagement.Common
{
    public class ProvinceReportInfo
    {
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public int? ActivityCount { get; set; }
        public int? TechnicalConfirmCount { get; set; }
        public int? FinancialConfirmCount { get; set; }
        public long? SumCycleCost { get; set; }
        public long? SumDelayedCost { get; set; }
        public long? SumTotalLeakage { get; set; }
        public long? SumRecoverableLeakage { get; set; }
        public long? SumRecovered { get; set; }
    }

    public class ActivityReportInfo
    {
        public int ActivityID { get; set; }
        public string ActivityCode { get; set; }
        public int? ActivityCount { get; set; }
        public long? SumCycleCost { get; set; }
        public long? SumDelayedCost { get; set; }
        public long? SumTotalLeakage { get; set; }
        public long? SumRecoverableLeakage { get; set; }
        public long? SumRecovered { get; set; }
    }

    public class IncomFlowReportInfo
    {
        public int IncomeFlowID { get; set; }
        public string IncomeFlowName { get; set; }
        public int? ActivityCount { get; set; }
        public long? SumCycleCost { get; set; }
        public long? SumDelayedCost { get; set; }
        public long? SumTotalLeakage { get; set; }
        public long? SumRecoverableLeakage { get; set; }
        public long? SumRecovered { get; set; }
    }
}