CREATE TABLE [dbo].[Attachement]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TicketId] INT NOT NULL, 
    [AttachementName] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_Attachement_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [Ticket]([Id])
)
