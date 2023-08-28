CREATE TABLE [dbo].[Ticket]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Body] NVARCHAR(MAX) NOT NULL,
    [Type] NVARCHAR(50) NOT NULL,
    [Priority] NVARCHAR(50) NOT NULL, 
    [Component] NVARCHAR(50) NOT NULL, 
    [ReporterId] INT NOT NULL, 
    [CreatedAt] DATETIME NOT NULL, 
    [UpdatedAt] DATETIME NULL, 
    [Attachements] VARBINARY(MAX) NULL, 
    [Status] INT NULL,
    CONSTRAINT [FK_Ticket_User] FOREIGN KEY ([ReporterId]) REFERENCES [User]([Id])
)
