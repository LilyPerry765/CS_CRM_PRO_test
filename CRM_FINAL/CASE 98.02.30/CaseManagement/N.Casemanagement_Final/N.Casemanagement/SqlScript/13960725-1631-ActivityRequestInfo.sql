USE [CaseManagement]
GO

/****** Object:  View [dbo].[ActivityRequestInfo]    Script Date: 10/17/2017 4:31:30 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ActivityRequestInfo]
AS
SELECT        dbo.ActivityRequest.ID, dbo.ActivityRequest.ProvinceID, dbo.ActivityRequest.ActivityID, dbo.ActivityRequest.CycleID, dbo.ActivityRequest.IncomeFlowID, dbo.ActivityRequest.Count, 
                         dbo.ActivityRequest.CycleCost, dbo.ActivityRequest.Factor, dbo.ActivityRequest.DelayedCost, dbo.ActivityRequest.YearCost, dbo.ActivityRequest.AccessibleCost, dbo.ActivityRequest.InaccessibleCost, 
                         dbo.ActivityRequest.TotalLeakage, dbo.ActivityRequest.RecoverableLeakage, dbo.ActivityRequest.Recovered, dbo.ActivityRequest.DelayedCostHistory, dbo.ActivityRequest.YearCostHistory, 
                         dbo.ActivityRequest.AccessibleCostHistory, dbo.ActivityRequest.InaccessibleCostHistory, dbo.ActivityRequest.RejectCount, dbo.ActivityRequest.EventDescription, dbo.ActivityRequest.MainReason, 
                         dbo.Cycle.CycleName, dbo.IncomeFlow.Name, dbo.Province.Name AS Expr1, dbo.Activity.CodeName, dbo.ActivityRequest.DiscoverLeakDate
FROM            dbo.ActivityRequest INNER JOIN
                         dbo.IncomeFlow ON dbo.ActivityRequest.IncomeFlowID = dbo.IncomeFlow.ID INNER JOIN
                         dbo.Cycle ON dbo.ActivityRequest.CycleID = dbo.Cycle.ID INNER JOIN
                         dbo.Province ON dbo.ActivityRequest.ProvinceID = dbo.Province.ID INNER JOIN
                         dbo.Activity ON dbo.ActivityRequest.ActivityID = dbo.Activity.ID

GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[29] 2[24] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "ActivityRequest"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 272
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Activity"
            Begin Extent = 
               Top = 6
               Left = 310
               Bottom = 136
               Right = 488
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "IncomeFlow"
            Begin Extent = 
               Top = 6
               Left = 526
               Bottom = 136
               Right = 697
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Cycle"
            Begin Extent = 
               Top = 6
               Left = 735
               Bottom = 136
               Right = 906
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Province"
            Begin Extent = 
               Top = 6
               Left = 944
               Bottom = 136
               Right = 1114
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 6720
         Alias = 900
         Table = 3660
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         O' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActivityRequestInfo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'r = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActivityRequestInfo'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ActivityRequestInfo'
GO

