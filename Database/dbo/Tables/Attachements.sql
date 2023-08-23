CREATE TABLE [dbo].[Attachement]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TicketId] INT NOT NULL, 
    [AttachementName] NVARCHAR(100) NOT NULL, 
    [Attachements] VARBINARY(MAX) NOT NULL,
    CONSTRAINT [FK_Attachement_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [Ticket]([Id])
)
