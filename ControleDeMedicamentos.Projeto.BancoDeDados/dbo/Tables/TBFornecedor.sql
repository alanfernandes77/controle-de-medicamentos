CREATE TABLE [dbo].[TBFornecedor] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Nome]     VARCHAR (200) NOT NULL,
    [Telefone] VARCHAR (20)  NOT NULL,
    [Email]    VARCHAR (200) NOT NULL,
    [Cidade]   VARCHAR (100) NOT NULL,
    [Uf]       VARCHAR (2)   NOT NULL,
    CONSTRAINT [PK_TBFornecedor] PRIMARY KEY CLUSTERED ([Id] ASC)
);

