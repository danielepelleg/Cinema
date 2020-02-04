CREATE PROCEDURE Cinema.[AddNewRiserva]
		@Numero_Posto int,
		@Codice_Prenotazione int
	  
AS
BEGIN
      SET NOCOUNT ON;
     INSERT INTO  Cinema.Riserva(Numero_Posto, Codice_Prenotazione)
      VALUES (@Numero_Posto, @Codice_Prenotazione)
END