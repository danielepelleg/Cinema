/*
Script di distribuzione per Cinema

Questo codice è stato generato da uno strumento.
Le modifiche apportate al file possono causare un comportamento non corretto e verranno perse se
il codice viene rigenerato.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Cinema"
:setvar DefaultFilePrefix "Cinema"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\"

GO
:on error exit
GO
/*
Rileva la modalità SQLCMD e disabilita l'esecuzione dello script se la modalità SQLCMD non è supportata.
Per abilitare nuovamente lo script dopo aver abilitato la modalità SQLCMD, eseguire l'istruzione seguente:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Per la corretta esecuzione dello script è necessario abilitare la modalità SQLCMD.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Modifica di [Cinema].[AddNewRiserva]...';


GO
ALTER PROCEDURE Cinema.[AddNewRiserva]
      @codice_prenotazione int,
	  @numero_posto int
AS
BEGIN
      SET NOCOUNT ON;
     INSERT INTO  Cinema.Riserva(Numero_Posto, Codice_Prenotazione)
      VALUES (@codice_prenotazione, @numero_posto)
END
GO
PRINT N'Modifica di [Cinema].[DeleteEvento]...';


GO
ALTER PROCEDURE Cinema.[DeleteEvento]
      @CodiceEvento int
AS
BEGIN
    DELETE Cinema.[Evento]  
    WHERE CodiceEvento = @CodiceEvento
END
GO
PRINT N'Modifica di [Cinema].[DeleteFilm]...';


GO
ALTER PROCEDURE Cinema.[DeleteFilm]
      @CodiceFilm int
AS
BEGIN
    DELETE Cinema.[Film]  
    WHERE CodiceFilm = @CodiceFilm
END
GO
PRINT N'Modifica di [Cinema].[VisualizzaPostiRiservati]...';


GO
ALTER PROCEDURE Cinema.[VisualizzaPostiRiservati]
      @codice_evento int
AS
BEGIN
      SET NOCOUNT ON;
      SELECT R.Numero_Posto, R.Codice_Prenotazione FROM Cinema.Evento AS E JOIN Cinema.Prenotazione AS P ON E.CodiceEvento = P.Codice_Evento
	  JOIN Cinema.Riserva AS R ON R.Codice_Prenotazione = P.CodicePrenotazione
	  WHERE E.CodiceEvento = @codice_evento;
END
GO
PRINT N'Modifica di [Cinema].[VisualizzaPostiSala]...';


GO
ALTER PROCEDURE Cinema.[VisualizzaPostiSala]
      @codice_evento int
AS
BEGIN
      SET NOCOUNT ON;
      SELECT P.NumeroPosto, P.Codice_Sala FROM Cinema.Evento AS E JOIN Cinema.Sala AS S ON E.Codice_Sala = S.CodiceSala
	  JOIN Cinema.Posto AS P ON P.Codice_Sala = S.CodiceSala
	  WHERE E.CodiceEvento = @codice_evento;
END
GO
PRINT N'Modifica di [Cinema].[VisualizzaPrenotazione]...';


GO
ALTER PROCEDURE Cinema.[VisualizzaPrenotazione]
      @user varchar(30)
AS
BEGIN
      SET NOCOUNT ON;
  SELECT P.CodicePrenotazione,P.DataOra,P.Username_UtenteFree,
                            P.Codice_Evento,F.Titolo,E.DataOra,E.Codice_Sala,
                            E.Prezzo, R.Numero_Posto
                            FROM Cinema.Evento AS E JOIN Cinema.Prenotazione AS P ON E.CodiceEvento = P.Codice_Evento
                            JOIN Cinema.Film AS F ON E.Codice_Film = F.CodiceFilm 
							JOIN Cinema.Riserva AS R ON R.Codice_Prenotazione = P.CodicePrenotazione
                            WHERE P.Username_UtenteFree = @user;
END
GO
PRINT N'Aggiornamento completato.';


GO
