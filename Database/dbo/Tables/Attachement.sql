CREATE TABLE [dbo].[Attachement]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TicketId] INT NOT NULL, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Link] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_Attachement_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [Ticket]([Id])
)
