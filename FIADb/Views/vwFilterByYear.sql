USE [FIA];
GO

IF OBJECT_ID ('[dbo].[vwFilterByYear]', 'V') IS NOT NULL
DROP VIEW [dbo].[vwFilterByYear];  
GO  
CREATE VIEW [dbo].[vwFilterByYear]
	AS SELECT * FROM [dbo].[vwRegistry] WHERE [Year] = 2010;
GO