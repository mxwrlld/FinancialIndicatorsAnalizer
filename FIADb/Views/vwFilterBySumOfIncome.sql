USE [FIA];
GO

IF OBJECT_ID ('[dbo].[vwFilterBySumOfIncome]', 'V') IS NOT NULL
DROP VIEW [dbo].[vwFilterBySumOfIncome];  
GO  
CREATE VIEW [dbo].[vwFilterBySumOfIncome]
	AS SELECT TOP (SELECT 100) PERCENT
      [Name]	
	  , SUM([Income]) AS SumOfIncome
	FROM [dbo].[vwRegistry]
	GROUP BY [Name]
	ORDER BY [SumOfIncome] DESC
	;
GO