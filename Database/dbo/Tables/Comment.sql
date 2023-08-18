CREATE TABLE [dbo].[Comment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Body] NVARCHAR(MAX) NOT NULL, 
    [UserId] INT NOT NULL, 
    [TicketId] INT NOT NULL, 
    [CreatedAt] INT NOT NULL, 
    CONSTRAINT [FK_Comment_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_Comment_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [Ticket]([Id])
)
