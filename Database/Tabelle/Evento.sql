SET IDENTITY_INSERT [Cinema].[Evento] ON
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (1, N'2018-10-01 19:00:00', 7, 3, N'Tom', CAST(9.00 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (3, N'2019-10-05 10:30:00', 6, 2, N'Tom', CAST(7.50 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (4, N'2019-10-10 22:30:00', 7, 1, N'Tom', CAST(7.00 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (5, N'2019-10-12 00:00:00', 2, 1, N'Tom', CAST(8.00 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (6, N'2019-10-05 12:00:00', 6, 1, N'Tom', CAST(7.00 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (7, N'2019-10-02 16:00:00', 4, 2, N'Tom', CAST(7.00 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (8, N'2019-10-31 23:00:00', 7, 3, N'Tom', CAST(7.00 AS Decimal(3, 2)))
INSERT INTO [Cinema].[Evento] ([CodiceEvento], [DataOra], [Codice_Film], [Codice_Sala], [Username_Admin], [Prezzo]) VALUES (9, N'2019-10-20 00:00:00', 3, 1, N'Tom', CAST(7.32 AS Decimal(3, 2)))
SET IDENTITY_INSERT [Cinema].[Evento] OFF
