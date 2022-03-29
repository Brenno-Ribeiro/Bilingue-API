CREATE TABLE [Classrooms] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Number] VARCHAR(50) NOT NULL,
    [SchoolYear] INT NOT NULL,
    CONSTRAINT [PK_Classrooms] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Students] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] VARCHAR(30) NOT NULL,
    [Cpf] CHAR(11) NOT NULL,
    [Email] VARCHAR(50) NOT NULL,
    [Age] INT NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Registrations] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [StudentId] UNIQUEIDENTIFIER NOT NULL,
    [ClassroomId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Registrations] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ClassroomId] FOREIGN KEY ([ClassroomId]) REFERENCES [Classrooms] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Studenid] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE CASCADE
);
GO


CREATE INDEX [IX_Classrooms_Number] ON [Classrooms] ([Number]);
GO


CREATE INDEX [IX_Registrations_ClassroomId] ON [Registrations] ([ClassroomId]);
GO


CREATE INDEX [IX_Registrations_StudentId] ON [Registrations] ([StudentId]);
GO


CREATE INDEX [IX_Students_Cpf] ON [Students] ([Cpf]);
GO


