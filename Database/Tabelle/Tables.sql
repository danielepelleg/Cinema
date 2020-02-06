CREATE TABLE [Cinema].[Admin] (
    [UsernameAdmin] VARCHAR (30) NOT NULL,
    [Password]      VARCHAR (32) NOT NULL,
    [Nome]          VARCHAR (20) NOT NULL,
    [Cognome]       VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([UsernameAdmin] ASC)
);

GO

CREATE TABLE [Cinema].[UtenteFree] (
    [UsernameUtenteFree] VARCHAR (30) NOT NULL,
    [Password]           VARCHAR (32) NOT NULL,
    [Nome]               VARCHAR (20) NOT NULL,
    [Cognome]            VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([UsernameUtenteFree] ASC)
);

GO

CREATE TABLE [Cinema].[Film] (
    [CodiceFilm]  INT          IDENTITY (1, 1) NOT NULL,
    [Titolo]      VARCHAR (50) NOT NULL,
    [Anno]        INT          NOT NULL,
    [Regia]       VARCHAR (30) NOT NULL,
    [Durata]      INT          NOT NULL,
    [Data_Uscita] DATE         NOT NULL,
    [Genere]      VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([CodiceFilm] ASC)
);

GO

CREATE TABLE [Cinema].[Sala] (
    [CodiceSala] INT IDENTITY (1, 1) NOT NULL,
    [Capienza]   INT NULL,
    PRIMARY KEY CLUSTERED ([CodiceSala] ASC)
);

GO

CREATE TABLE [Cinema].[Evento] (
    [CodiceEvento]   INT            IDENTITY (1, 1) NOT NULL,
    [DataOra]        DATETIME       NOT NULL,
    [Codice_Film]    INT            NOT NULL,
    [Codice_Sala]    INT            NOT NULL,
    [Username_Admin] VARCHAR (30)   NOT NULL,
    [Prezzo]         DECIMAL (3, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([CodiceEvento] ASC),
    CONSTRAINT [FK_Codice_Film_Evento] FOREIGN KEY ([Codice_Film]) REFERENCES [Cinema].[Film] ([CodiceFilm]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Codice_Sala_Evento] FOREIGN KEY ([Codice_Sala]) REFERENCES [Cinema].[Sala] ([CodiceSala]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Username_Admin_Evento] FOREIGN KEY ([Username_Admin]) REFERENCES [Cinema].[Admin] ([UsernameAdmin]) ON DELETE CASCADE ON UPDATE CASCADE
);

GO

CREATE TABLE [Cinema].[Prenotazione] (
    [CodicePrenotazione]  INT          IDENTITY (1, 1) NOT NULL,
    [DataOra]             DATETIME     NOT NULL,
    [Username_UtenteFree] VARCHAR (30) NOT NULL,
    [Codice_Evento]       INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([CodicePrenotazione] ASC),
    CONSTRAINT [FK_Utente_Free_Prenotazione] FOREIGN KEY ([Username_UtenteFree]) REFERENCES [Cinema].[UtenteFree] ([UsernameUtenteFree]) ON DELETE CASCADE ON UPDATE CASCADE
);

GO

CREATE TABLE [Cinema].[Posto] (
    [NumeroPosto] INT NOT NULL,
    [Codice_Sala] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([NumeroPosto] ASC, [Codice_Sala] ASC),
    CONSTRAINT [FK_Codice_Sala_Posto] FOREIGN KEY ([Codice_Sala]) REFERENCES [Cinema].[Sala] ([CodiceSala])
);

GO

CREATE TABLE [Cinema].[Riserva] (
    [Numero_Posto]        INT NOT NULL,
    [Codice_Prenotazione] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Numero_Posto] ASC, [Codice_Prenotazione] ASC),
    CONSTRAINT [FK_Codice_Prenotazione_Riserva] FOREIGN KEY ([Codice_Prenotazione]) REFERENCES [Cinema].[Prenotazione] ([CodicePrenotazione]) ON DELETE CASCADE ON UPDATE CASCADE
);

GO

