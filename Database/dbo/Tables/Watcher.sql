﻿CREATE TABLE [dbo].[Watcher]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NULL, 
    [TicketId] INT NULL, 
    [IsDeleted] BIT NULL DEFAULT 0, 
    CONSTRAINT [FK_Watcher_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
    CONSTRAINT [FK_Watcher_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [Ticket]([Id])
)
