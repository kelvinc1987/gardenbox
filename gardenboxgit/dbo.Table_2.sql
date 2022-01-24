CREATE TABLE [dbo].[user]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [user] VARCHAR(50) NULL, 
    [area] VARCHAR(50) NULL, 
    [whatiplanted] VARCHAR(50) NOT NULL 
)
