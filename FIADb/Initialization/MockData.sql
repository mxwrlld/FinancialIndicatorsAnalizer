﻿USE [FIA];
GO

DELETE FROM [dbo].[FinancialResult];
DELETE FROM [dbo].[Enterprise];
GO

INSERT INTO [dbo].[Enterprise] ([TIN], [Name], [LegalAddress]) 
VALUES 
	('0100000000', 'ООО Сок', 'Калужская область, город Одинцово, бульвар Будапештсткая, 95'), 
	('0100000001', 'OOO Совсем Добрый', 'Тульская область, город Талдом, въезд Ладыгина, 59'), 
	('0100000002', 'ООО Безусловный', 'Курганская область, город Орехово-Зуево, спуск Ладыгина, 74'), 
	('0100000003', 'ОДО Ясеневый', 'Кемеровская область, город Чехов, пл. Домодедовская, 65'), 
	('0100000004', 'АО Альпийский округ', 'Архангельская область, город Щёлково, наб. Чехова, 41'); 
GO


INSERT INTO [dbo].[FinancialResult] 
	([Year], [Quarter] , [Income], [Consumption], [Enterprise]) 
VALUES 
	(2010, 1, 200, 200, '0100000000'),
	(2010, 2, 200, 150, '0100000000'),
	(2010, 3, 200, 100, '0100000000'),
	(2010, 4, 1000, 200, '0100000000'),
	(2014, 1, 200, 100, '0100000001'),
	(2014, 2, 200, 100, '0100000001'),
	(2014, 3, 200, 100, '0100000001'),
	(2014, 4, 200, 100, '0100000001'),
	(2017, 1, 100, 1000, '0100000002'),
	(2017, 2, 50, 1000, '0100000002'),
	(2017, 3, 100, 1000, '0100000002'),
	(2017, 4, 105, 1000, '0100000002'),
	(2018, 1, 10000, 1000, '0100000002'),
	(2018, 2, 20000, 10000, '0100000002'),
	(2018, 3, 30000, 20000, '0100000002'),
	(2018, 4, 40000, 30000, '0100000002'),
	(2021, 1, 10, 1000, '0100000003'),
	(2021, 2, 20, 1000, '0100000003'),
	(2021, 3, 0, 1000, '0100000003'),
	(2021, 4, 50, 10000000000, '0100000003'),
	(2021, 1, 0, 10000, '0100000004'),
	(2021, 2, 0, 10000, '0100000004'),
	(2021, 3, 0, 10000, '0100000004'),
	(2021, 4, 0, 10000, '0100000004');
GO