USE [FIA];
GO

IF OBJECT_ID ('[dbo].[vwSearchByTIN]', 'V') IS NOT NULL
DROP VIEW [dbo].[vwSearchByTIN];  
GO  
CREATE VIEW [dbo].[vwSearchByTIN]
	AS SELECT * FROM [dbo].[Enterprise] WHERE [TIN] LIKE '0100000001';
GO