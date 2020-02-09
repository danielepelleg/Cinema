USE Cinema;


-- ADMIN
/** Username: Tom, Password: 1234 **/
INSERT INTO [Cinema].[Admin] ([UsernameAdmin], [Password], [Nome], [Cognome]) VALUES (N'Tom', N'81dc9bdb52d04dc20036dbd8313ed055', N'Tommaso', N'Belli')
/** Username: Cate, Password: occhidiGatto **/
INSERT INTO [Cinema].[Admin] ([UsernameAdmin], [Password], [Nome], [Cognome]) VALUES (N'Cate', N'9def0cbf4ed60bd2aa9286831887a978', N'Caterina', N'Poggi')
/** Username: Stefano, Password: impossible **/
INSERT INTO [Cinema].[Admin] ([UsernameAdmin], [Password], [Nome], [Cognome]) VALUES (N'Ste', N'79a2520f22b9e1526ff93176029603b8', N'Stefano', N'Marini')




-- USER
/** Username: Maty, Password: qwerty **/
INSERT INTO [Cinema].[UtenteFree] ([UsernameUtenteFree], [Password], [Nome], [Cognome]) VALUES (N'Maty', N'd8578edf8458ce06fbc5bb76a58c5ca4', N'Matilde', N'Rossi')
/** Username: Mike, Password: asdfg **/
INSERT INTO [Cinema].[UtenteFree] ([UsernameUtenteFree], [Password], [Nome], [Cognome]) VALUES (N'Mike', N'040b7cf4a55014e185813e0644502ea9', N'Michele', N'Tuni')
/** Username: Cri, Password: filmLover **/
INSERT INTO [Cinema].[UtenteFree] ([UsernameUtenteFree], [Password], [Nome], [Cognome]) VALUES (N'Cri', N'55f5e8ca2c45b795f592bc86ab2240bc', N'Christian', N'Gherri')
/** Username: Jack, Password: hardtofind **/
INSERT INTO [Cinema].[UtenteFree] ([UsernameUtenteFree], [Password], [Nome], [Cognome]) VALUES (N'Jack', N'aa7dcd799df5136c89931152a274c449', N'Giacomo', N'Neri')
/** Username: Lucy, Password: 13022020 **/
INSERT INTO [Cinema].[UtenteFree] ([UsernameUtenteFree], [Password], [Nome], [Cognome]) VALUES (N'Lucy', N'08c9dc814260fad03e5a723746019d8d', N'Lucia', N'Prati')



-- ABBONAMENTO
INSERT INTO [Cinema].[Abbonamento] ([Username]) VALUES (N'Maty')


-- FILM
SET IDENTITY_INSERT [Cinema].[Film] ON
INSERT INTO [Cinema].[Film] ([CodiceFilm], [Titolo], [Anno], [Regia], [Durata], [Data_Uscita], [Genere]) VALUES (1, N'1917', 2020, N'Sam Mendes', 110, N'23/01/2020', N'Dramma')
INSERT INTO [Cinema].[Film] ([CodiceFilm], [Titolo], [Anno], [Regia], [Durata], [Data_Uscita], [Genere]) VALUES (2, N'Dolittle', 2020, N'Stephen Ghagan', 106, N'30/01/2020', N'Avventura')
INSERT INTO [Cinema].[Film] ([CodiceFilm], [Titolo], [Anno], [Regia], [Durata], [Data_Uscita], [Genere]) VALUES (3, N'Il diritto di opporsi', 2020, N'Destin Daniel Cretton', 136, N'30/01/2020', N'Dramma')
INSERT INTO [Cinema].[Film] ([CodiceFilm], [Titolo], [Anno], [Regia], [Durata], [Data_Uscita], [Genere]) VALUES (4, N'Underwater', 2020, N'William Eubank', 95, N'30/01/2020', N'Thriller')
INSERT INTO [Cinema].[Film] ([CodiceFilm], [Titolo], [Anno], [Regia], [Durata], [Data_Uscita], [Genere]) VALUES (5, N'Jojo Rabbit', 2020, N'Taika Waititi', 160, N'07/11/2019', N'Commedia')
INSERT INTO [Cinema].[Film] ([CodiceFilm], [Titolo], [Anno], [Regia], [Durata], [Data_Uscita], [Genere]) VALUES (6, N'Judy', 2019, N'Rupert Goold', 122, N'06/02/2020', N'Biografico')
SET IDENTITY_INSERT [Cinema].[Film] OFF

-- SALA
SET IDENTITY_INSERT [Cinema].[Sala] ON
INSERT INTO [Cinema].[Sala] ([CodiceSala], [Capienza]) VALUES (1, 24)
INSERT INTO [Cinema].[Sala] ([CodiceSala], [Capienza]) VALUES (2, 16)
INSERT INTO [Cinema].[Sala] ([CodiceSala], [Capienza]) VALUES (3, 30)
SET IDENTITY_INSERT [Cinema].[Sala] OFF

-- POSTO
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (1, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (1, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (1, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (2, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (2, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (2, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (3, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (3, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (3, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (4, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (4, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (4, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (5, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (5, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (5, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (6, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (6, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (6, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (7, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (7, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (7, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (8, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (8, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (8, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (9, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (9, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (9, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (10, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (10, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (10, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (11, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (11, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (11, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (12, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (12, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (12, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (13, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (13, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (13, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (14, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (14, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (14, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (15, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (15, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (15, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (16, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (16, 2)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (16, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (17, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (17, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (18, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (18, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (19, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (19, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (20, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (20, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (21, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (21, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (22, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (22, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (23, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (23, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (24, 1)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (24, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (25, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (26, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (27, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (28, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (29, 3)
INSERT INTO [Cinema].[Posto] ([NumeroPosto], [Codice_Sala]) VALUES (30, 3)

-- EVENTO
SET IDENTITY_INSERT [Cinema].[Evento] ON
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (1, N'2020-17-02 16:20:00', 1, 3, N'Tom', CAST(8.50 AS Decimal(5, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (2, N'2020-17-02 16:30:00', 2, 1, N'Cate', CAST(8.50 AS Decimal(5, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (3, N'2020-17-02 17:40:00', 3, 2, N'Ste', CAST(8.50 AS Decimal(5, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (4, N'2020-21-02 18:30:00', 5, 2, N'Tom', CAST(5.00 AS Decimal(5, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (5, N'2020-19-02 19:10:00', 6, 1, N'Ste', CAST(8.50 AS Decimal(5, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (6, N'2020-18-02 22:20:00', 4, 2, N'Cate', CAST(7.50 AS Decimal(5, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (7, N'2020-22-02 20:30:00', 1, 3, N'Cate', CAST(9.50 AS Decimal(5, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (8, N'2019-22-02 20:50:00', 2, 1, N'Tom', CAST(9.50 AS Decimal(5, 2)))
SET IDENTITY_INSERT [Cinema].[Evento] OFF


-- PRENOTAZIONE
SET IDENTITY_INSERT [Cinema].[Prenotazione] ON
INSERT INTO [Cinema].[Prenotazione] ([CodicePrenotazione], [DataOra], [Username_UtenteFree], [Codice_Evento], [Prezzo]) VALUES (1, N'2020-02-05 16:17:57', N'Maty', 5, CAST(6.40 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Prenotazione] ([CodicePrenotazione], [DataOra], [Username_UtenteFree], [Codice_Evento], [Prezzo]) VALUES (2, N'2020-02-05 16:21:49', N'Mike', 1, CAST(8.50 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Prenotazione] ([CodicePrenotazione], [DataOra], [Username_UtenteFree], [Codice_Evento], [Prezzo]) VALUES (3, N'2020-02-05 16:22:13', N'Mike', 6, CAST(7.50 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Prenotazione] ([CodicePrenotazione], [DataOra], [Username_UtenteFree], [Codice_Evento], [Prezzo]) VALUES (4, N'2020-02-05 16:24:29', N'Cri', 3, CAST(8.50 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Prenotazione] ([CodicePrenotazione], [DataOra], [Username_UtenteFree], [Codice_Evento], [Prezzo]) VALUES (5, N'2020-02-05 16:27:26', N'Jack', 8, CAST(9.50 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Prenotazione] ([CodicePrenotazione], [DataOra], [Username_UtenteFree], [Codice_Evento], [Prezzo]) VALUES (6, N'2020-02-05 16:28:07', N'Maty', 2, CAST(6.40 AS Decimal(3, 2)))
SET IDENTITY_INSERT [Cinema].[Prenotazione] OFF

-- RISERVA (POSTI RISERVATI - COMPRATI)
INSERT INTO [Cinema].[Riserva] ([Numero_Posto], [Codice_Prenotazione]) VALUES (7, 4)
INSERT INTO [Cinema].[Riserva] ([Numero_Posto], [Codice_Prenotazione]) VALUES (9, 2)
INSERT INTO [Cinema].[Riserva] ([Numero_Posto], [Codice_Prenotazione]) VALUES (10, 6)
INSERT INTO [Cinema].[Riserva] ([Numero_Posto], [Codice_Prenotazione]) VALUES (11, 3)
INSERT INTO [Cinema].[Riserva] ([Numero_Posto], [Codice_Prenotazione]) VALUES (15, 1)
INSERT INTO [Cinema].[Riserva] ([Numero_Posto], [Codice_Prenotazione]) VALUES (21, 5)