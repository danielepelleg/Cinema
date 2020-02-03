CREATE PROCEDURE Cinema.[AddNewEvento]
      @DataOra datetime,
      @Codice_Film int,
	  @Codice_Sala int,
      @Username_Admin varchar(30),
	  @Prezzo decimal,
	  @CodiceEvento int output
AS
BEGIN
      SET NOCOUNT ON;
      INSERT INTO  Cinema.Evento(DataOra, Codice_Film, Codice_Sala, Username_Admin, Prezzo)
      VALUES (@DataOra, @Codice_Film, @Codice_Sala, @Username_Admin, @Prezzo)
      SET @CodiceEvento=SCOPE_IDENTITY()
      RETURN  @CodiceEvento
END