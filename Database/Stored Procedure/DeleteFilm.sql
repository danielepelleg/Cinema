CREATE PROCEDURE Cinema.[DeleteFilm]
      @CodiceFilm int
AS
BEGIN
	/*
	 * Delete the Event
	 */
    DELETE Cinema.[Film]  
    WHERE CodiceFilm = @CodiceFilm;

	/*
	 * Save the last value of the Primary Key and reset
	 * the Primary Key counters to the last value known.
	 */
	declare @max int;  
	select @max = max(CodiceFilm) from Cinema.Film;  
	dbcc checkident('Cinema.Film', reseed, @max)
END