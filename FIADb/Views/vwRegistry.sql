USE [FIA];
GO

IF OBJECT_ID ('[dbo].[vwRegistry]', 'V') IS NOT NULL
DROP VIEW [dbo].[vwRegistry];  
GO  

CREATE VIEW [dbo].[vwRegistry]
	AS SELECT [TIN]
      ,[Name]
      ,[LegalAddress]
      ,[Year]
      ,[Quarter]
      ,[Income]
      ,[Consumption]
	FROM [dbo].[Enterprise] e
	JOIN [dbo].[FinancialResult] fr ON e.TIN = fr.Enterprise;
GO