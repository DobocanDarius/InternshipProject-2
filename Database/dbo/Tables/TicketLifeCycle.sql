CREATE TABLE [dbo].[TicketLifeCycle]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[TicketId] Int, 
    [ToDo] BIT NOT NULL DEFAULT 1, 
    [Assigned] BIT NOT NULL DEFAULT 0, 
    [Solving] BIT NOT NULL DEFAULT 0, 
    [CodeReview] BIT NOT NULL DEFAULT 0, 
    [TestingDev] BIT NOT NULL DEFAULT 0, 
    [TestingUat] BIT NOT NULL DEFAULT 0, 
    [Closed] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_TicketLifeCycle_Ticket] FOREIGN KEY ([TicketId]) REFERENCES [Ticket]([Id]),
)
