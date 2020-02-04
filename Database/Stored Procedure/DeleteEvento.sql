CREATE PROCEDURE Cinema.[DeleteEvento]
      @CodiceEvento int
AS
BEGIN
    DELETE Cinema.[Evento]  
    WHERE CodiceEvento = @CodiceEvento
END