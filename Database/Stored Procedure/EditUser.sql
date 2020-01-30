CREATE PROCEDURE Cinema.[EditUser]
		@OldUsername varchar(30),
		@NewUsername varchar(30),
		@NewPassword varchar(32),
		@NewNome varchar(20),
		@NewCognome varchar(20)
AS
BEGIN
    UPDATE Cinema.[UtenteFree] 
	SET UsernameUtenteFree = @NewUsername, Password = @NewPassword, Nome = @NewNome, Cognome = @NewCognome
    WHERE UsernameUtenteFree = @OldUsername
END