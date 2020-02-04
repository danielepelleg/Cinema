CREATE PROCEDURE Cinema.[DeleteFilm]
      @CodiceFilm int
AS
BEGIN
    DELETE Cinema.[Film]  
    WHERE CodiceFilm = @CodiceFilm
END