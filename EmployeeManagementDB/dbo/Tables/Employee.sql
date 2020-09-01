CREATE TABLE [dbo].[Employee] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (20)  NOT NULL,
    [Surname]       NVARCHAR (20)  NOT NULL,
    [Address]       NVARCHAR (100) NOT NULL,
    [Qualification] NVARCHAR (MAX) NOT NULL,
    [ContactNumber] BIGINT         NOT NULL,
    [DepartmentId]  INT            NOT NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([DepartmentId]) ON DELETE CASCADE
);