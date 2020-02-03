CREATE PROCEDURE Cinema.[DeleteUser]
      @Username varchar(30)
AS
BEGIN
    DELETE Cinema.[UtenteFree]  
    WHERE UsernameUtenteFree = @Username;
END