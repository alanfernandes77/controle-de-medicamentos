CREATE TABLE [dbo].[TBMedicamento] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [Nome]                 VARCHAR (200) NOT NULL,
    [Descricao]            VARCHAR (300) NOT NULL,
    [Lote]                 VARCHAR (20)  NOT NULL,
    [Validade]             DATETIME      NOT NULL,
    [QuantidadeDisponivel] INT           NOT NULL,
    [Fornecedor_Id]        INT           NOT NULL,
    CONSTRAINT [PK_TBMedicamento] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TBMedicamento_TBFornecedor] FOREIGN KEY ([Fornecedor_Id]) REFERENCES [dbo].[TBFornecedor] ([Id])
);

