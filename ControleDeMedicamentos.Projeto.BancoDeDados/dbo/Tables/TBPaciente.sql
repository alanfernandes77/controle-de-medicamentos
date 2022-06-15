CREATE TABLE [dbo].[TBPaciente] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Nome]      VARCHAR (100) NOT NULL,
    [CartaoSUS] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_TBPaciente] PRIMARY KEY CLUSTERED ([Id] ASC)
);

