﻿CREATE TABLE [dbo].[Genre]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Date_Created] DATETIME NOT NULL DEFAULT GetDate()
)
