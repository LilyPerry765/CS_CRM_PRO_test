  --         [Description("WLL")]
  --          WLL = 1,

  --          [Description("بدون کافو")]
  --          WithoutCabinet = 2,

  --          [Description("معمولی")]
  --          Normal = 3,

  --          [Description("کافو نوری")]
  --          OpticalCabinet = 4,

  --          [Description("کابل")]
  --          Cable = 5,

update cabinet
set CabinetUsageType = 1
where CabinetUsageType = 21

update cabinet
set CabinetUsageType = 2
where CabinetUsageType = 22

update cabinet
set CabinetUsageType = 3
where CabinetUsageType = 1

update cabinet
set CabinetUsageType = 4
where CabinetUsageType = 2

update cabinet
set CabinetUsageType = 5
where CabinetUsageType = 41
