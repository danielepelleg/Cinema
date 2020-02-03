CREATE PROCEDURE Cinema.[AddNewRiserva]
      @Codice_Prenotazione int,
	  @Numero_Posto int
AS
BEGIN
      SET NOCOUNT ON;
     INSERT INTO  Cinema.Riserva(Numero_Posto, Codice_Prenotazione)
      VALUES (@Numero_Posto, @Codice_Prenotazione)
END