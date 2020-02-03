CREATE PROCEDURE Cinema.[DeleteEvento]
      @CodiceEvento int
AS
BEGIN
	/*
	 * Delete the Event
	 */
    DELETE Cinema.[Evento]  
    WHERE CodiceEvento = @CodiceEvento;
	
	/*
	 * Save the last value of the Primary Key and reset
	 * the Primary Key counters to the last value known.
	 */
	declare @maxEvent int;  
	select @maxEvent = max(CodiceEvento) from Cinema.Evento;  
	dbcc checkident('Cinema.Evento', reseed, @maxEvent);

	/*
	 * Save the Primary Key of the Prenotation 
	 * of the event to delete
	 */
	declare @CodicePrenotazione int;
	select @CodicePrenotazione = CodicePrenotazione from Cinema.Prenotazione WHERE Codice_Evento = @CodiceEvento;  

	/*
	 * Delete the Prenotation linked to the Event
	 */
	DELETE Cinema.[Prenotazione]  
    WHERE Codice_Evento = @CodiceEvento;

	/*
	 * Save the last value of the Primary Key and reset
	 * the Primary Key counters to the last value known.
	 */
	declare @maxPrenotation int;  
	select @maxPrenotation = max(CodicePrenotazione) from Cinema.Prenotazione;  
	dbcc checkident('Cinema.Prenotazione', reseed, @maxPrenotation);

	/*
	 * Delete the Reservation linked to the Prenotation
	 */
	DELETE Cinema.[Riserva]
	WHERE Codice_Prenotazione = @CodicePrenotazione
END