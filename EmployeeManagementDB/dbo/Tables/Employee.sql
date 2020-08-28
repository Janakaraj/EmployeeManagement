CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(20) NOT NULL, 
    [Surname] NVARCHAR(20) NOT NULL, 
    [Address] NVARCHAR(100) NOT NULL, 
    [Qualification] NVARCHAR(50) NOT NULL, 
    [ContactNumber] BIGINT NOT NULL, 
    [Department] NVARCHAR(50) NOT NULL
)
