CREATE PROCEDURE Cinema.[DeletePrenotazione]
      @CodicePrenotazione int
AS
BEGIN
    DELETE Cinema.[Prenotazione]
    WHERE CodicePrenotazione = @CodicePrenotazione 
	DBCC CHECKIDENT ([Prenotazione], RESEED, 0)
	
	DELETE Cinema.[Riserva]
    WHERE Codice_Prenotazione = @CodicePrenotazione 
	DBCC CHECKIDENT ([Riserva], RESEED, 0)
END