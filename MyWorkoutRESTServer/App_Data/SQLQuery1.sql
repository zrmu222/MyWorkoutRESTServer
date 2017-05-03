CREATE TABLE [dbo].[Users] (
    [UserId]      INT           IDENTITY (1, 1) NOT NULL,
    [UserName]    NVARCHAR (50) NOT NULL,
    [FirstName]   NVARCHAR (50) NOT NULL,
    [LastName]    NVARCHAR (50) NOT NULL,
    [Password]    NVARCHAR (50) NOT NULL,
    [CurrentDay]  INT           NOT NULL,
    [CurrentWeek] INT           NOT NULL,
    [Role]        NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    UNIQUE NONCLUSTERED ([UserName] ASC)
);

CREATE TABLE [dbo].[Weeks] (
    [WeekId]          INT IDENTITY (1, 1) NOT NULL,
    [WeekNumber]      INT NOT NULL,
    [UserId]          INT NOT NULL,
    [WeekOrderNumber] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([WeekId] ASC)
);

CREATE TABLE [dbo].[Days] (
    [DayId]          INT IDENTITY (1, 1) NOT NULL,
    [DayNumber]      INT NOT NULL,
    [DayOrderNumber] INT NOT NULL,
    [UserId]         INT NOT NULL,
    [WeekId]         INT NOT NULL,
    PRIMARY KEY CLUSTERED ([DayId] ASC)
);

CREATE TABLE [dbo].[DayExerciseList] (
    [Id]     INT IDENTITY (1, 1) NOT NULL,
    [Number] INT NOT NULL,
    [DayId]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Exercises] (
    [ExerciseId]     INT           IDENTITY (1, 1) NOT NULL,
    [UserId]         INT           NOT NULL,
    [Name]           NVARCHAR (50) NOT NULL,
    [ExerciseNumber] INT           NOT NULL,
    [Weight]         DECIMAL (18)  NOT NULL,
    [Sets]           INT           NOT NULL,
    [Reps]           INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ExerciseId] ASC)
);