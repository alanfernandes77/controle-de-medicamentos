CREATE TABLE [dbo].[TBFuncionario] (
    [Id]      INT           IDENTITY (1, 1) NOT NULL,
    [Nome]    VARCHAR (200) NOT NULL,
    [Usuario] VARCHAR (100) NOT NULL,
    [Senha]   VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_TBFuncionario] PRIMARY KEY CLUSTERED ([Id] ASC)
);

