IF DB_ID('SchwimmbadDB') IS NULL
BEGIN
    CREATE DATABASE SchwimmbadDB;
END;
GO

USE SchwimmbadDB;
GO



DROP TABLE IF EXISTS KursKursleiter;
DROP TABLE IF EXISTS KursTeilnehmer;

DROP TABLE IF EXISTS Kurs;
DROP TABLE IF EXISTS Kursleiter;
DROP TABLE IF EXISTS Teilnehmer;
GO




-- Tabelle neu erstellen
CREATE TABLE Kurs
(
    KursID INT IDENTITY(1,1) PRIMARY KEY,
    Kursname NVARCHAR(100) NOT NULL,
    Beschreibung NVARCHAR(500),
    Startdatum DATE NOT NULL,
    Enddatum DATE NOT NULL,
    Wochentag NVARCHAR(20) NOT NULL,
    Anfangszeit TIME(0) NOT NULL,
    Endzeit TIME(0) NOT NULL,
    MaximaleTeilnehmerzahl INT NOT NULL
);
GO





-- Tabelle neu erstellen
CREATE TABLE Kursleiter
(
    KursleiterID INT IDENTITY(1,1) PRIMARY KEY,
    Vorname NVARCHAR(50) NOT NULL,
    Nachname NVARCHAR(50) NOT NULL,
    Telefon NVARCHAR(30),
    Email NVARCHAR(100)
);
GO






-- Tabelle neu erstellen
CREATE TABLE KursKursleiter
(
    KursKursleiterID INT IDENTITY(1,1) PRIMARY KEY,

    KursID INT NOT NULL,
    KursleiterID INT NOT NULL,

    FOREIGN KEY (KursID) REFERENCES Kurs(KursID),
    FOREIGN KEY (KursleiterID) REFERENCES Kursleiter(KursleiterID),

    UNIQUE (KursID, KursleiterID)
);
GO



-- Tabelle neu erstellen
CREATE TABLE Teilnehmer
(
    TeilnehmerID INT IDENTITY(1,1) PRIMARY KEY,
    Vorname NVARCHAR(50) NOT NULL,
    Nachname NVARCHAR(50) NOT NULL,
    Geburtsdatum DATE NOT NULL,
    Telefon NVARCHAR(30),
    Email NVARCHAR(100)
);
GO







-- Tabelle neu erstellen
CREATE TABLE KursTeilnehmer
(
    KursTeilnehmerID INT IDENTITY(1,1) PRIMARY KEY,

    KursID INT NOT NULL,
    TeilnehmerID INT NOT NULL,

    Anmeldedatum DATE NOT NULL,

    FOREIGN KEY (KursID) REFERENCES Kurs(KursID),
    FOREIGN KEY (TeilnehmerID) REFERENCES Teilnehmer(TeilnehmerID),

    UNIQUE (KursID, TeilnehmerID)
);
GO












INSERT INTO Kurs
(Kursname, Beschreibung, Startdatum, Enddatum,
 Wochentag, Anfangszeit, Endzeit, MaximaleTeilnehmerzahl)
VALUES
('Anfängerschwimmen', 'Schwimmkurs für Anfänger',
 '2027-01-04', '2027-03-29', 'Montag', '14:00', '15:00', 10),

('Kinderschwimmen', 'Schwimmkurs für Kinder',
 '2027-01-06', '2027-03-31', 'Mittwoch', '16:00', '17:00', 12),

('Erwachsenenschwimmen', 'Schwimmkurs für Erwachsene',
 '2027-01-08', '2027-04-02', 'Freitag', '18:00', '19:00', 15),

('Fortgeschrittenenschwimmen', 'Kurs für erfahrene Schwimmer',
 '2027-01-05', '2027-03-30', 'Dienstag', '15:00', '16:00', 10),

('Wassergewöhnung', 'Erster Kontakt mit dem Wasser',
 '2027-01-07', '2027-04-01', 'Donnerstag', '10:00', '11:00', 8);
GO


--select * From Kurs;



INSERT INTO Kursleiter (Vorname, Nachname, Telefon, Email)
VALUES
('Max', 'Müller', '017612345678', 'max.mueller@example.de'),
('Lisa', 'Weber', '017623456789', 'lisa.weber@example.de'),
('Thomas', 'Schmidt', '017634567890', 'thomas.schmidt@example.de'),
('Anna', 'Fischer', '017645678901', 'anna.fischer@example.de'),
('Peter', 'Hoffmann', '017656789012', 'peter.hoffmann@example.de');
GO


--select * From Kursleiter;




INSERT INTO Teilnehmer
(Vorname, Nachname, Geburtsdatum, Telefon, Email)
VALUES
('Lena', 'Schneider', '2015-03-12', '017611111111', 'lena.schneider@example.de'),
('Ben', 'Wagner', '2014-07-22', '017622222222', 'ben.wagner@example.de'),
('Mia', 'Becker', '2016-01-09', '017633333333', 'mia.becker@example.de'),
('Paul', 'Hoffmann', '2013-11-30', '017644444444', 'paul.hoffmann@example.de'),
('Emma', 'Schulz', '2015-06-18', '017655555555', 'emma.schulz@example.de');
GO



--select * From  Teilnehmer;



INSERT INTO KursKursleiter (KursID, KursleiterID)
VALUES
(1, 1),
(1, 2),
(2, 2),
(2, 3),
(3, 4);
GO


--select * From KursKursleiter;



INSERT INTO KursTeilnehmer
(KursID, TeilnehmerID, Anmeldedatum)
VALUES
(1, 1, '2026-11-10'),
(1, 2, '2026-11-11'),
(2, 3, '2026-11-12'),
(2, 4, '2026-11-13'),
(3, 5, '2026-11-14');
GO


--select * From KursTeilnehmer;




SELECT
    t.TeilnehmerID,
    t.Vorname + ' ' + t.Nachname AS Teilnehmer,
    k.Kursname,
    kl.Vorname + ' ' + kl.Nachname AS Kursleiter,
    kt.Anmeldedatum

FROM KursTeilnehmer AS kt

INNER JOIN Teilnehmer AS t
    ON kt.TeilnehmerID = t.TeilnehmerID

INNER JOIN Kurs AS k
    ON kt.KursID = k.KursID

INNER JOIN KursKursleiter AS kkl
    ON k.KursID = kkl.KursID

INNER JOIN Kursleiter AS kl
    ON kkl.KursleiterID = kl.KursleiterID

ORDER BY t.Nachname, k.Kursname;
GO




