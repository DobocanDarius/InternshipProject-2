CREATE TABLE [dbo].[History]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TicketId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [CreatedAt] DATETIME NOT NULL, 
    [Body] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_History_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_History_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [Ticket]([Id])

)
