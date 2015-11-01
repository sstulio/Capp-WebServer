USE [db809fe2633e1c45ffbdc3a5410180950e]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* Prova

DROP TABLE [dbo].[Prova]
GO */

CREATE TABLE [dbo].[Prova] (
	[ProvaID] int IDENTITY(1,1) PRIMARY KEY,
	[CodigoProva] varchar(50) NOT NULL,
	[ProfessorID] [int] NOT NULL,
	[Nome] varchar(50) NOT NULL,
	[QtdQuestoes] [tinyint] NOT NULL,
	[DataCriada] [datetime] NOT NULL
) 
GO

ALTER TABLE [dbo].[Prova] ADD CONSTRAINT UQ_Codico UNIQUE (CodigoProva)


/* Resposta

DROP TABLE [dbo].[Resposta]
GO  */

CREATE TABLE [dbo].[Resposta] (
	[RespostaID] int IDENTITY(1,1) PRIMARY KEY,
	[CodigoProva] varchar(50) NOT NULL,
	[CodigoAluno] varchar(50) NOT NULL,
	[Questao] [tinyint] NOT NULL,
	[Alternativa] varchar(50) NOT NULL
	
)
GO


/* Professor 

DROP TABLE [dbo].[Professor]
GO*/

CREATE TABLE [dbo].[Professor] (
	[ProfessorID] int IDENTITY(1,1) PRIMARY KEY,
	[Nome] varchar(50) NOT NULL,
	[Email] varchar(50) NOT NULL,
	[Senha] varchar(50) NOT NULL
	
)
GO

ALTER TABLE [dbo].[Professor] ADD CONSTRAINT UQ_Email UNIQUE (Email)

/* Aluno

DROP TABLE [dbo].[Aluno]
GO */

CREATE TABLE [dbo].[Aluno] (
	[AlunoID] int IDENTITY(1,1) PRIMARY KEY,
	[CodigoAluno] varchar(50) NOT NULL,
	[Nome] varchar(50) NOT NULL

)
GO

ALTER TABLE [dbo].[Aluno] ADD CONSTRAINT UQ_CodicoAluno UNIQUE (CodigoAluno)

ALTER TABLE [dbo].[Prova]  WITH NOCHECK ADD  CONSTRAINT [FK_Prova_Professor] FOREIGN KEY([ProfessorID])
REFERENCES [dbo].[Professor] ([ProfessorID])
GO

ALTER TABLE [dbo].[Prova] CHECK CONSTRAINT [FK_Prova_Professor]
GO

/*--------------------------------------------------------------------------------------------------*/

ALTER TABLE [dbo].[Resposta]  WITH NOCHECK ADD  CONSTRAINT [FK_Resposta_Prova] FOREIGN KEY([CodigoProva])
REFERENCES [dbo].[Prova] ([CodigoProva])
GO

ALTER TABLE [dbo].[Resposta] CHECK CONSTRAINT [FK_Resposta_Prova]
GO

ALTER TABLE [dbo].[Resposta]  WITH NOCHECK ADD  CONSTRAINT [FK_Resposta_Aluno] FOREIGN KEY([CodigoAluno])
REFERENCES [dbo].[Aluno] ([CodigoAluno])
GO

ALTER TABLE [dbo].[Resposta] CHECK CONSTRAINT [FK_Resposta_Aluno]
GO

/*--------------------------------------------------------------------------------------------------*/
