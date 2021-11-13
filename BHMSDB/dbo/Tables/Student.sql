CREATE TABLE [dbo].[Student]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [MatricNo] VARCHAR(20) NOT NULL, 
    [Firstname] NCHAR(10) NOT NULL, 
    [Lastname] NCHAR(10) NOT NULL, 
    [Phonenumber] INT NOT NULL, 
    [Course] VARCHAR(50) NOT NULL, 
    [EmailAddress] VARCHAR(50) NULL
)
