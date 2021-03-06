/****** Object:  Database [Cinema]    Script Date: 05/02/2020 10:54:46 ******/
USE master
GO

/** Elimina il Databasw **/
IF EXISTS (
   SELECT name
   FROM sys.databases
   WHERE name = N'Cinema'
)
DROP DATABASE [Cinema]
GO

/** Creazione del Database **/
IF NOT EXISTS (
   SELECT name
   FROM sys.databases
   WHERE name = N'Cinema'
)
CREATE DATABASE [Cinema]
GO
USE [Cinema]
GO


/****** Object:  Schema [Cinema] ******/
/** Si utilizza uno schema per una migliore gestione del Db **/
DROP SCHEMA IF EXISTS [Cinema]
GO
CREATE SCHEMA [Cinema]
GO


/****** Object:  Table [Cinema].[Evento] ******/
CREATE TABLE [Cinema].[Evento](
	[CodiceEvento] [int] IDENTITY(1,1) NOT NULL,
	[DataOra] [datetime] NOT NULL,
	[Codice_Film] [int] NOT NULL,
	[Codice_Sala] [int] NOT NULL,
	[Username_Admin] [varchar](30) NOT NULL,
	[Prezzo] [decimal](5, 2) NOT NULL,
PRIMARY KEY CLUSTERED ([CodiceEvento] ASC)) ON [PRIMARY]
GO


/****** Object:  Table [Cinema].[Film] ******/
CREATE TABLE [Cinema].[Film](
	[CodiceFilm] [int] IDENTITY(1,1) NOT NULL,
	[Titolo] [varchar](50) NOT NULL,
	[Anno] [int] NOT NULL,
	[Regia] [varchar](30) NOT NULL,
	[Durata] [int] NOT NULL,
	[Data_Uscita] [date] NOT NULL,
	[Genere] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED ([CodiceFilm] ASC )) ON [PRIMARY]
GO


/****** Object:  View [dbo].[Show] ******/
/** Vista utilizzata per mostrare all'utente le proiezioni degli spettacoli presenti nel cinema **/
CREATE VIEW [dbo].[Show] AS 
SELECT E.CodiceEvento, E.DataOra, F.Titolo, E.Codice_Sala, E.Prezzo 
FROM Cinema.Evento AS E, Cinema.Film F 
WHERE E.Codice_Film = F.CodiceFilm;
GO


/****** Object:  Table [Cinema].[Admin] ******/
CREATE TABLE [Cinema].[Admin](
	[UsernameAdmin] [varchar](30) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[Nome] [varchar](20) NOT NULL,
	[Cognome] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED ([UsernameAdmin] ASC)) ON [PRIMARY]
GO


/****** Object:  Table [Cinema].[Posto] ******/
/** Tabella utilizzata per la creazione di un nuovo posto in ciascuna sala **/
CREATE TABLE [Cinema].[Posto](
	[NumeroPosto] [int] NOT NULL,
	[Codice_Sala] [int] NOT NULL,
PRIMARY KEY CLUSTERED ( [NumeroPosto] ASC, [Codice_Sala] ASC)) ON [PRIMARY]
GO


/****** Object:  Table [Cinema].[Prenotazione] ******/
/** Tabella utilizzata per salvare la prenotazione effettuata da ciascun utente.
	Assieme ad altre informazioni relative al film, all'evento e al posto 
	viene utilizzata per creare il biglietto **/
CREATE TABLE [Cinema].[Prenotazione](
	[CodicePrenotazione] [int] IDENTITY(1,1) NOT NULL,
	[DataOra] [datetime] NOT NULL,
	[Username_UtenteFree] [varchar](30) NOT NULL,
	[Codice_Evento] [int] NOT NULL,
	[Prezzo] [decimal](5, 2) NOT NULL,
PRIMARY KEY CLUSTERED ( [CodicePrenotazione] ASC )) ON [PRIMARY]
GO


/****** Object:  Table [Cinema].[Riserva] ******/
/** La tabella tiene traccia dei Posti Riservati (o comprati) di ciascun utente **/
CREATE TABLE [Cinema].[Riserva](
	[Numero_Posto] [int] NOT NULL,
	[Codice_Prenotazione] [int] NOT NULL,
PRIMARY KEY CLUSTERED  ( [Numero_Posto] ASC, [Codice_Prenotazione] ASC )) ON [PRIMARY]
GO


/****** Object:  Table [Cinema].[Sala] ******/
/** Salva le sale presenti nel cinema, ciascuna con la sua capienza **/
CREATE TABLE [Cinema].[Sala](
	[CodiceSala] [int] IDENTITY(1,1) NOT NULL,
	[Capienza] [int] NULL,
PRIMARY KEY CLUSTERED  ( [CodiceSala] ASC )) ON [PRIMARY]
GO


/****** Object:  Table [Cinema].[UtenteFree] ******/
CREATE TABLE [Cinema].[UtenteFree](
	[UsernameUtenteFree] [varchar](30) NOT NULL,
	[Password] [varchar](32) NOT NULL,
	[Nome] [varchar](20) NOT NULL,
	[Cognome] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED ( [UsernameUtenteFree] ASC )) ON [PRIMARY]
GO

/****** Object:  Table [Cinema].[Abbonamento] ******/
CREATE TABLE [Cinema].[Abbonamento](
	[Username] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED ( [Username] ASC )) ON [PRIMARY]
GO


/****** Aggiunta delle chiavi primarie e delle Azioni da fare in caso di UPDATE o DELETE ******/
ALTER TABLE [Cinema].[Evento]  WITH CHECK ADD  CONSTRAINT [FK_Codice_Film_Evento] FOREIGN KEY([Codice_Film])
REFERENCES [Cinema].[Film] ([CodiceFilm])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Cinema].[Evento] CHECK CONSTRAINT [FK_Codice_Film_Evento]
GO
ALTER TABLE [Cinema].[Evento]  WITH CHECK ADD  CONSTRAINT [FK_Codice_Sala_Evento] FOREIGN KEY([Codice_Sala])
REFERENCES [Cinema].[Sala] ([CodiceSala])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Cinema].[Evento] CHECK CONSTRAINT [FK_Codice_Sala_Evento]
GO
ALTER TABLE [Cinema].[Evento]  WITH CHECK ADD  CONSTRAINT [FK_Username_Admin_Evento] FOREIGN KEY([Username_Admin])
REFERENCES [Cinema].[Admin] ([UsernameAdmin])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Cinema].[Evento] CHECK CONSTRAINT [FK_Username_Admin_Evento]
GO


ALTER TABLE [Cinema].[Posto]  WITH CHECK ADD  CONSTRAINT [FK_Codice_Sala_Posto] FOREIGN KEY([Codice_Sala])
REFERENCES [Cinema].[Sala] ([CodiceSala])
GO
ALTER TABLE [Cinema].[Posto] CHECK CONSTRAINT [FK_Codice_Sala_Posto]
GO


ALTER TABLE [Cinema].[Prenotazione]  WITH CHECK ADD  CONSTRAINT [FK_Utente_Free_Prenotazione] FOREIGN KEY([Username_UtenteFree])
REFERENCES [Cinema].[UtenteFree] ([UsernameUtenteFree])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Cinema].[Prenotazione] CHECK CONSTRAINT [FK_Utente_Free_Prenotazione]
GO
ALTER TABLE [Cinema].[Prenotazione]  WITH CHECK ADD  CONSTRAINT [FK_Codice_Evento] FOREIGN KEY([Codice_Evento])
REFERENCES [Cinema].[Evento] ([CodiceEvento])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Cinema].[Prenotazione] CHECK CONSTRAINT [FK_Codice_Evento]
GO


ALTER TABLE [Cinema].[Riserva]  WITH CHECK ADD  CONSTRAINT [FK_Codice_Prenotazione_Riserva] FOREIGN KEY([Codice_Prenotazione])
REFERENCES [Cinema].[Prenotazione] ([CodicePrenotazione])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Cinema].[Riserva] CHECK CONSTRAINT [FK_Codice_Prenotazione_Riserva]
GO

ALTER TABLE [Cinema].[Abbonamento]  WITH CHECK ADD  CONSTRAINT [FK_Utente_Abbonamento] FOREIGN KEY([Username])
REFERENCES [Cinema].[UtenteFree] ([UsernameUtenteFree])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [Cinema].[Abbonamento] CHECK CONSTRAINT [FK_Utente_Abbonamento]
GO

/****** Object:  StoredProcedure [Cinema].[AddAbbonamento] ******/
/** Stored Procedure utilizzate per l'inserimento di un Abbonamento **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Cinema].[AddAbbonamento]
		@Username varchar(30)
	  
AS
BEGIN
      SET NOCOUNT ON;
     INSERT INTO  Cinema.Abbonamento(Username)
      VALUES (@Username)
END
GO


/****** Object:  StoredProcedure [Cinema].[AddNewEvento] ******/
/** Stored Procedure utilizzate per l'inserimento di un nuovo Evento all'interno del Database **/
CREATE PROCEDURE [Cinema].[AddNewEvento]
      @DataOra datetime,
      @Codice_Film int,
	  @Codice_Sala int,
      @Username_Admin varchar(30),
	  @Prezzo decimal (5, 2),
	  @CodiceEvento int output
AS
BEGIN
      SET NOCOUNT ON;
      INSERT INTO  Cinema.Evento(DataOra, Codice_Film, Codice_Sala, Username_Admin, Prezzo)
      VALUES (@DataOra, @Codice_Film, @Codice_Sala, @Username_Admin, @Prezzo)
      SET @CodiceEvento=SCOPE_IDENTITY()
      RETURN  @CodiceEvento
END
GO

/****** Object:  StoredProcedure [Cinema].[AddNewFilm] ******/
/** Stored Procedure utilizzate per l'inserimento di un nuovo Film all'interno del Database **/
CREATE PROCEDURE [Cinema].[AddNewFilm]
      @Titolo varchar(50),
      @Anno int,
      @Regia varchar(30),
	  @Durata int,
      @Data_Uscita date,
      @Genere varchar(12),
	  @CodiceFilm int output
AS
BEGIN
      SET NOCOUNT ON;
      INSERT INTO  Cinema.Film (Titolo, Anno, Regia, Durata, Data_Uscita, Genere)
      VALUES (@Titolo, @Anno, @Regia, @Durata, @Data_Uscita, @Genere)
      SET @CodiceFilm=SCOPE_IDENTITY()
      RETURN  @CodiceFilm
END
GO


/****** Object:  StoredProcedure [Cinema].[AddNewPrenotazione] ******/
/** Stored Procedure utilizzate per l'inserimento di una nuova Prenotazione all'interno del Database **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Cinema].[AddNewPrenotazione]
      @DataOra datetime,
      @Username_UtenteFree varchar(30),
      @Codice_Evento int,
	  @Prezzo decimal (5,2),
      @CodicePrenotazione int output
      
AS
BEGIN
      SET NOCOUNT ON;
      INSERT INTO  Cinema.Prenotazione (DataOra, Username_UtenteFree, Codice_Evento, Prezzo)
      VALUES (@DataOra, @Username_UtenteFree, @Codice_Evento, @Prezzo)
      SET @CodicePrenotazione=SCOPE_IDENTITY()
      RETURN  @CodicePrenotazione
END
GO


/****** Object:  StoredProcedure [Cinema].[AddNewRiserva] ******/
/** Stored Procedure utilizzate per l'inserimento di un nuovo Posto Riservato all'interno del Database **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Cinema].[AddNewRiserva]
		@Numero_Posto int,
		@Codice_Prenotazione int
	  
AS
BEGIN
      SET NOCOUNT ON;
     INSERT INTO  Cinema.Riserva(Numero_Posto, Codice_Prenotazione)
      VALUES (@Numero_Posto, @Codice_Prenotazione)
END
GO


/****** Object:  StoredProcedure [Cinema].[DeleteEvento] ******/
/** Stored Procedure utilizzate per l'eliminazione di un Evento all'interno del Database **/
CREATE PROCEDURE [Cinema].[DeleteEvento]
      @CodiceEvento int
AS
BEGIN
    DELETE Cinema.[Evento]  
    WHERE CodiceEvento = @CodiceEvento
END
GO


/****** Object:  StoredProcedure [Cinema].[DeleteFilm] ******/
/** Stored Procedure utilizzate per l'eliminazione di un Film all'interno del Database **/
CREATE PROCEDURE [Cinema].[DeleteFilm]
      @CodiceFilm int
AS
BEGIN
    DELETE Cinema.[Film]  
    WHERE CodiceFilm = @CodiceFilm
END
GO


/****** Object:  StoredProcedure [Cinema].[DeletePrenotazione] ******/
/** Stored Procedure utilizzate per l'eliminazione di una Prenotazione all'interno del Database **/
CREATE PROCEDURE [Cinema].[DeletePrenotazione]
      @CodicePrenotazione int
AS
BEGIN
	/*
	 * Delete the Prenotation
	 */
    DELETE Cinema.[Prenotazione]
    WHERE CodicePrenotazione = @CodicePrenotazione

	/*
	 * Delete the Reservation
	 */
	DELETE Cinema.[Riserva]
    WHERE Codice_Prenotazione = @CodicePrenotazione
END
GO


/****** Object:  StoredProcedure [Cinema].[DeleteUser] ******/
/** Stored Procedure utilizzate per l'eliminazione di un Utente all'interno del Database **/
CREATE PROCEDURE [Cinema].[DeleteUser]
      @Username varchar(30)
AS
BEGIN
    DELETE Cinema.[UtenteFree]  
    WHERE UsernameUtenteFree = @Username;
END
GO

/****** Object:  StoredProcedure [Cinema].[DeleteAbbonamento] ******/
/** Stored Procedure utilizzate per l'eliminazione di un utente abbonato dalla Tabella Abbonamento **/
CREATE PROCEDURE [Cinema].[DeleteAbbonamento]
      @Username varchar(30)
AS
BEGIN
    DELETE Cinema.[Abbonamento]  
    WHERE Username = @Username;
END
GO

/****** Object:  StoredProcedure [Cinema].[EditUser] ******/
/** Stored Procedure utilizzate per l'update di un Utente all'interno del Database **/
CREATE PROCEDURE [Cinema].[EditUser]
		@OldUsername varchar(30),
		@NewUsername varchar(30),
		@NewPassword varchar(32),
		@NewNome varchar(20),
		@NewCognome varchar(20)
AS
BEGIN
    UPDATE Cinema.[UtenteFree] 
	SET UsernameUtenteFree = @NewUsername, Password = @NewPassword, Nome = @NewNome, Cognome = @NewCognome
    WHERE UsernameUtenteFree = @OldUsername
END
GO


/****** Object:  StoredProcedure [Cinema].[RichiestaCodiceSala] ******/
/** Stored Procedure utilizzate per ottenere il Codice della Sala dove si svolge la proiezione di un Film **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Cinema].[RichiestaCodiceSala]
      @CodiceEvento int
AS
BEGIN
      SET NOCOUNT ON;
      SELECT E.Codice_Sala FROM Cinema.Evento AS E 
	  WHERE E.CodiceEvento = @CodiceEvento;
END
GO


/****** Object:  StoredProcedure [Cinema].[VisualizzaPostiRiservati] ******/
/** Stored Procedure utilizzate per ottenere i Posti Riservati della sala dove si svolge la proiezione di un Film **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Cinema].[VisualizzaPostiRiservati]
      @CodiceEvento int
AS
BEGIN
      SET NOCOUNT ON;
      SELECT R.Numero_Posto, R.Codice_Prenotazione FROM Cinema.Evento AS E JOIN Cinema.Prenotazione AS P ON E.CodiceEvento = P.Codice_Evento
	  JOIN Cinema.Riserva AS R ON R.Codice_Prenotazione = P.CodicePrenotazione
	  WHERE E.CodiceEvento = @CodiceEvento;
END
GO


/****** Object:  StoredProcedure [Cinema].[VisualizzaPostiSala]    Script Date: 05/02/2020 10:54:46 ******/
/** Stored Procedure utilizzate per ottenere i Posti della Sala dove si svolge la proiezione di un Film **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [Cinema].[VisualizzaPostiSala]
      @CodiceEvento int
AS
BEGIN
      SET NOCOUNT ON;
      SELECT P.NumeroPosto, P.Codice_Sala FROM Cinema.Evento AS E JOIN Cinema.Sala AS S ON E.Codice_Sala = S.CodiceSala
	  JOIN Cinema.Posto AS P ON P.Codice_Sala = S.CodiceSala
	  WHERE E.CodiceEvento = @CodiceEvento;
END
GO


/****** Object:  StoredProcedure [Cinema].[VisualizzaPrenotazione]    Script Date: 05/02/2020 10:54:46 ******/
/** Stored Procedure utilizzate per ottenere il biglietto di un utente. un biglietto è costituito da informazioni 
	riguardo la Prenotazione, il suo Posto Riservaro, il Film e l'Evento (lo spettacolo) che andrà a vedere **/
CREATE PROCEDURE [Cinema].[VisualizzaPrenotazione]
      @Username varchar(30)
AS
BEGIN
      SET NOCOUNT ON;
  SELECT P.CodicePrenotazione,P.DataOra,P.Username_UtenteFree,
                            P.Codice_Evento,F.Titolo,E.DataOra,E.Codice_Sala,
                            P.Prezzo, R.Numero_Posto
                            FROM Cinema.Evento AS E JOIN Cinema.Prenotazione AS P ON E.CodiceEvento = P.Codice_Evento
                            JOIN Cinema.Film AS F ON E.Codice_Film = F.CodiceFilm 
							JOIN Cinema.Riserva AS R ON R.Codice_Prenotazione = P.CodicePrenotazione
                            WHERE P.Username_UtenteFree = @Username;
END
GO