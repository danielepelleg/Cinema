CREATE PROCEDURE Cinema.[VisualizzaPostiSala]
      @CodiceEvento int
AS
BEGIN
      SET NOCOUNT ON;
      SELECT P.NumeroPosto, P.Codice_Sala FROM Cinema.Evento AS E JOIN Cinema.Sala AS S ON E.Codice_Sala = S.CodiceSala
	  JOIN Cinema.Posto AS P ON P.Codice_Sala = S.CodiceSala
	  WHERE E.CodiceEvento = @CodiceEvento;
END