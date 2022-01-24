CREATE TABLE [dbo].[FinancialResult]
(
	[Year] INT NOT NULL, 
    [Quarter] INT NOT NULL, 
    [Income] DECIMAL NOT NULL, 
    [Consumption] DECIMAL NOT NULL,
    [Enterprise] NVARCHAR(10) NOT NULL, 

    CONSTRAINT [FK_FinancialResult_Enterprise] FOREIGN KEY ([Enterprise]) REFERENCES [Enterprise]([TIN]) 
)
