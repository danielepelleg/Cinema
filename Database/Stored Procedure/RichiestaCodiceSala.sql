CREATE PROCEDURE Cinema.[RichiestaCodiceSala]
      @CodiceEvento int
AS
BEGIN
      SET NOCOUNT ON;
      SELECT E.Codice_Sala FROM Cinema.Evento AS E 
	  WHERE E.CodiceEvento = @CodiceEvento;
END