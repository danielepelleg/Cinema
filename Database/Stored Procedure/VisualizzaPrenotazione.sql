CREATE PROCEDURE Cinema.[VisualizzaPrenotazione]
      @Username varchar(30)
AS
BEGIN
      SET NOCOUNT ON;
  SELECT P.CodicePrenotazione,P.DataOra,P.Username_UtenteFree,
                            P.Codice_Evento,F.Titolo,E.DataOra,E.Codice_Sala,
                            E.Prezzo, R.Numero_Posto
                            FROM Cinema.Evento AS E JOIN Cinema.Prenotazione AS P ON E.CodiceEvento = P.Codice_Evento
                            JOIN Cinema.Film AS F ON E.Codice_Film = F.CodiceFilm 
							JOIN Cinema.Riserva AS R ON R.Codice_Prenotazione = P.CodicePrenotazione
                            WHERE P.Username_UtenteFree = @Username;
END