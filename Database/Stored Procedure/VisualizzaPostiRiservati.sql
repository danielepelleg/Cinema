CREATE PROCEDURE Cinema.[VisualizzaPostiRiservati]
      @CodiceEvento int
AS
BEGIN
      SET NOCOUNT ON;
      SELECT R.Numero_Posto, R.Codice_Prenotazione FROM Cinema.Evento AS E JOIN Cinema.Prenotazione AS P ON E.CodiceEvento = P.Codice_Evento
	  JOIN Cinema.Riserva AS R ON R.Codice_Prenotazione = P.CodicePrenotazione
	  WHERE E.CodiceEvento = @CodiceEvento;
END