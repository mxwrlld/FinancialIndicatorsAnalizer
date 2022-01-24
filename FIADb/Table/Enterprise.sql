
CREATE TABLE [dbo].[Enterprise]
(
    [TIN] NVARCHAR(10) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL,
    [LegalAddress] NVARCHAR(MAX) NULL, 

    CONSTRAINT check_tin CHECK
		(TIN LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]'), 
    CONSTRAINT [PK_Enterprise] PRIMARY KEY ([TIN])
)