USE [db809fe2633e1c45ffbdc3a5410180950e]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* Prova */

CREATE TABLE [dbo].[Prova] (
	[ProvaID] int IDENTITY(1,1) PRIMARY KEY,
	[CodigoProva] varchar(50) NULL,
	[ProfessorID] [int] NOT NULL,
	[Nome] varchar(50) NOT NULL,
	[QtdQuestoes] int NOT NULL,
	[DataCriada] varchar(50) NULL
) 
GO


/* Resposta */

CREATE TABLE [dbo].[Resposta] (
	[RespostaID] int IDENTITY(1,1) PRIMARY KEY,
	[ProvaID] int NOT NULL,
	[CodigoAluno] varchar(50) NOT NULL,
	[Questao] int NOT NULL,
	[Alternativa] varchar(50) NOT NULL,
	[isGabarito] tinyint NOT NULL
	
)
GO


/* Professor */

CREATE TABLE [dbo].[Professor] (
	[ProfessorID] int IDENTITY(1,1) PRIMARY KEY,
	[Nome] varchar(50) NOT NULL,
	[Email] varchar(50) NOT NULL,
	[Senha] varchar(50) NOT NULL
	
)
GO

ALTER TABLE [dbo].[Professor] ADD CONSTRAINT UQ_Email UNIQUE (Email)

/* Aluno*/

CREATE TABLE [dbo].[Aluno] (
	[AlunoID] int IDENTITY(1,1) PRIMARY KEY,
	[CodigoAluno] varchar(50) NOT NULL,
	[Nome] varchar(50) NOT NULL

)
GO

ALTER TABLE [dbo].[Aluno] ADD CONSTRAINT UQ_CodicoAluno UNIQUE (CodigoAluno)


/*
ALTER TABLE [dbo].[Prova]  WITH NOCHECK ADD  CONSTRAINT [FK_Prova_Professor] FOREIGN KEY([ProfessorID])
REFERENCES [dbo].[Professor] ([ProfessorID])
GO

ALTER TABLE [dbo].[Prova] CHECK CONSTRAINT [FK_Prova_Professor]
GO

/*--------------------------------------------------------------------------------------------------*/

ALTER TABLE [dbo].[Resposta]  WITH NOCHECK ADD  CONSTRAINT [FK_Resposta_Prova] FOREIGN KEY([ProvaID])
REFERENCES [dbo].[Prova] ([ProvaID])
GO

ALTER TABLE [dbo].[Resposta] CHECK CONSTRAINT [FK_Resposta_Prova]
GO

ALTER TABLE [dbo].[Resposta]  WITH NOCHECK ADD  CONSTRAINT [FK_Resposta_Aluno] FOREIGN KEY([CodigoAluno])
REFERENCES [dbo].[Aluno] ([CodigoAluno])
GO

ALTER TABLE [dbo].[Resposta] CHECK CONSTRAINT [FK_Resposta_Aluno]
GO

--------------------------------------------------------------------------------------------------*/
