CREATE TABLE [dbo].[Assignee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [TicketId] INT NOT NULL, 
    CONSTRAINT [FK_Assignee_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_Assignee_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [Ticket]([Id])
)
