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
	 * Save the last value of the Primary Key and reset
	 * the Primary Key counters to the last value known.
	 */
	declare @maxPrenotation int;  
	select @maxPrenotation = max(CodicePrenotazione) from Cinema.Prenotazione;  
	dbcc checkident('Cinema.Prenotazione', reseed, @maxPrenotation);

	/*
	 * Delete the Reservation
	 */
	DELETE Cinema.[Riserva]
    WHERE Codice_Prenotazione = @CodicePrenotazione
END