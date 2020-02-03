CREATE PROCEDURE Cinema.[AddNewPrenotazione]
      @DataOra datetime,
      @Username_UtenteFree varchar(30),
      @Codice_Evento int,
      @CodicePrenotazione int output
      
AS
BEGIN
      SET NOCOUNT ON;
      INSERT INTO  Cinema.Prenotazione (DataOra, Username_UtenteFree, Codice_Evento)
      VALUES (@DataOra, @Username_UtenteFree, @Codice_Evento)
      SET @CodicePrenotazione=SCOPE_IDENTITY()
      RETURN  @CodicePrenotazione
END