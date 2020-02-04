CREATE PROCEDURE Cinema.[DeletePrenotazione]
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