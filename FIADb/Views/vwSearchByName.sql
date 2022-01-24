USE [FIA];
GO

IF OBJECT_ID ('[dbo].[vwSearchByName]', 'V') IS NOT NULL
DROP VIEW [dbo].[vwSearchByName];  
GO  
CREATE VIEW [dbo].[vwSearchByName]
	AS SELECT * FROM [dbo].[Enterprise] WHERE [Name] LIKE 'ООО Безусловный';
GO