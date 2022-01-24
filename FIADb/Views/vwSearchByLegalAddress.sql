USE [FIA];
GO

IF OBJECT_ID ('[dbo].[vwSearchByLegalAddress]', 'V') IS NOT NULL
DROP VIEW [dbo].[vwSearchByLegalAddress];  
GO  
CREATE VIEW [dbo].[vwSearchByLegalAddress]
	AS SELECT * FROM [dbo].[Enterprise] WHERE [LegalAddress] LIKE 'Кемеровская область%';
GO