CREATE PROCEDURE Cinema.[DeleteEvento]
      @CodiceEvento int
AS
BEGIN
    DELETE Cinema.[Evento]  
    WHERE CodiceEvento = @CodiceEvento
	DBCC CHECKIDENT (Evento, RESEED, 0)
END