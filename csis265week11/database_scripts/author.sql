CREATE TABLE [dbo].[Author]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Email_Address] VARCHAR(100) NULL, 
    [Date_Created] DATETIME NOT NULL DEFAULT GetDate()
)
