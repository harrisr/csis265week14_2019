CREATE TABLE [dbo].[Book]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL DEFAULT '', 
    [Description] VARCHAR(250) NULL, 
    [Genre_Id] INT NULL, 
    [Author_Id] INT NULL, 
    [Date_Created] DATETIME NOT NULL DEFAULT GetDate(), 
    CONSTRAINT [FK_Book_ToGenre] FOREIGN KEY ([Genre_Id]) REFERENCES [Genre]([Id]), 
    CONSTRAINT [FK_Book_ToAuthor] FOREIGN KEY ([Author_Id]) REFERENCES [Author]([Id])
)
