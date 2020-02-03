CREATE PROCEDURE Cinema.[AddNewFilm]
      @Titolo varchar(50),
      @Anno int,
      @Regia varchar(30),
	  @Durata int,
      @Data_Uscita date,
      @Genere varchar(12),
	  @CodiceFilm int output
AS
BEGIN
      SET NOCOUNT ON;
      INSERT INTO  Cinema.Film (Titolo, Anno, Regia, Durata, Data_Uscita, Genere)
      VALUES (@Titolo, @Anno, @Regia, @Durata, @Data_Uscita, @Genere)
      SET @CodiceFilm=SCOPE_IDENTITY()
      RETURN  @CodiceFilm
END