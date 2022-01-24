USE [FIA];
GO

IF OBJECT_ID ('[dbo].[vwFilterByQuarter]', 'V') IS NOT NULL
DROP VIEW [dbo].[vwFilterByQuarter];  
GO
CREATE VIEW [dbo].[vwFilterByQuarter]
	AS SELECT * FROM [dbo].[vwRegistry] WHERE [Quarter] = 1;
GO