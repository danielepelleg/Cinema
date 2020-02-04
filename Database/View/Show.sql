CREATE VIEW Show AS 
SELECT E.CodiceEvento, E.DataOra, F.Titolo, E.Codice_Sala, E.Prezzo 
FROM Cinema.Evento AS E, Cinema.Film F 
WHERE E.Codice_Film = F.CodiceFilm;