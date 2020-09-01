CREATE TABLE [dbo].[depatments] (
    [DepartmentId]   INT           IDENTITY (1, 1) NOT NULL,
    [DepartmentName] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_depatments] PRIMARY KEY CLUSTERED ([DepartmentId] ASC)
);

