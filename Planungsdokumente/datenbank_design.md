# Datenbankdesign – Schwimmbad-Kursplanung

## 1. Ziel der Datenbank

Die Datenbank `SchwimmbadDB` dient zur Verwaltung von Schwimmkursen, Kursleitern und Teilnehmern.

Mit dieser Datenbank soll die Organisation eines Schwimmbads erleichtert werden.

Folgende Funktionen werden unterstützt:

- Schwimmkurse anlegen und verwalten.
- Kursleiter speichern und verwalten.
- Teilnehmer speichern und verwalten.
- Mehrere Kursleiter einem Kurs zuordnen.
- Teilnehmer für mehrere Kurse anmelden.
- Anmeldedaten speichern.
- Informationen aus mehreren Tabellen gemeinsam abrufen.

## 2. Übersicht der Tabellen

Die Datenbank besteht aus fünf Tabellen:

| Tabelle | Aufgabe |
	|---|---|
| Kurs | Speichert Informationen zu den angebotenen Schwimmkursen. |
| Kursleiter | Speichert die persönlichen Daten der Kursleiter. |
| Teilnehmer | Speichert die persönlichen Daten der Teilnehmer. |
| KursKursleiter | Verbindet Kurse mit ihren Kursleitern. |
| KursTeilnehmer | Verbindet Kurse mit ihren Teilnehmern und speichert das Anmeldedatum. |

## 3. Beschreibung der Tabellen und Spalten

### 3.1 Tabelle Kurs

Die Tabelle `Kurs` speichert alle Informationen über die angebotenen Schwimmkurse.

| Spalte | Datentyp | Beschreibung |
		|---|---|---|
| KursID | INT | Primärschlüssel, automatische ID |
| Kursname | NVARCHAR(100) | Name des Kurses |
| Beschreibung | NVARCHAR(500) | Beschreibung des Kurses |
| Startdatum | DATE | Beginn des Kurses |
| Enddatum | DATE | Ende des Kurses |
| Wochentag | NVARCHAR(20) | Wochentag des Kurses |
| Anfangszeit | TIME(0) | Anfangszeit des Kurses |
| Endzeit | TIME(0) | Endzeit des Kurses |
| MaximaleTeilnehmerzahl | INT | Maximale Anzahl der Teilnehmer |

**Primärschlüssel:** `KursID`

### 3.2 Tabelle Kursleiter

Die Tabelle `Kursleiter` speichert die persönlichen Informationen der Kursleiter.

| Spalte | Datentyp | Beschreibung |
		|---|---|---|
| KursleiterID | INT | Primärschlüssel, automatische ID |
| Vorname | NVARCHAR(50) | Vorname des Kursleiters |
| Nachname | NVARCHAR(50) | Nachname des Kursleiters |
| Telefon | NVARCHAR(30) | Telefonnummer |
| Email | NVARCHAR(100) | E-Mail-Adresse |

**Primärschlüssel:** `KursleiterID`

### 3.3 Tabelle Teilnehmer

Die Tabelle `Teilnehmer` speichert die persönlichen Informationen der Kursteilnehmer.

| Spalte | Datentyp | Beschreibung |
		|---|---|---|
| TeilnehmerID | INT | Primärschlüssel, automatische ID |
| Vorname | NVARCHAR(50) | Vorname des Teilnehmers |
| Nachname | NVARCHAR(50) | Nachname des Teilnehmers |
| Geburtsdatum | DATE | Geburtsdatum |
| Telefon | NVARCHAR(30) | Telefonnummer |
| Email | NVARCHAR(100) | E-Mail-Adresse |

**Primärschlüssel:** `TeilnehmerID`

### 3.4 Tabelle KursKursleiter

Die Tabelle `KursKursleiter` verbindet die Tabellen `Kurs` und `Kursleiter`.

Sie wird benötigt, weil ein Kurs mehrere Kursleiter haben kann und ein Kursleiter mehrere Kurse betreuen kann.

| Spalte | Datentyp | Beschreibung |
		|---|---|---|
| KursKursleiterID | INT | Primärschlüssel, automatische ID |
| KursID | INT | Fremdschlüssel auf die Tabelle Kurs |
| KursleiterID | INT | Fremdschlüssel auf die Tabelle Kursleiter |

**Primärschlüssel:** `KursKursleiterID`

**Fremdschlüssel:** `KursID`, `KursleiterID`

Die Kombination aus `KursID` und `KursleiterID` ist durch eine UNIQUE-Einschränkung eindeutig. Dadurch kann dieselbe Zuordnung nicht doppelt gespeichert werden.

### 3.5 Tabelle KursTeilnehmer

Die Tabelle `KursTeilnehmer` verbindet die Tabellen `Kurs` und `Teilnehmer`.

Sie speichert, welcher Teilnehmer an welchem Kurs teilnimmt und wann die Anmeldung erfolgt ist.

| Spalte | Datentyp | Beschreibung |
		|---|---|---|
| KursTeilnehmerID | INT | Primärschlüssel, automatische ID |
| KursID | INT | Fremdschlüssel auf die Tabelle Kurs |
| TeilnehmerID | INT | Fremdschlüssel auf die Tabelle Teilnehmer |
| Anmeldedatum | DATE | Datum der Anmeldung |

**Primärschlüssel:** `KursTeilnehmerID`

**Fremdschlüssel:** `KursID`, `TeilnehmerID`

Auch hier ist die Kombination aus `KursID` und `TeilnehmerID` durch eine UNIQUE-Einschränkung eindeutig. Ein Teilnehmer kann sich dadurch nicht doppelt für denselben Kurs anmelden.

## 4. Beziehungen zwischen den Tabellen

In der Datenbank gibt es zwei ursprüngliche n:m-Beziehungen und vier daraus entstehende 1:n-Beziehungen.

### 4.1 Beziehung zwischen Kurs und Kursleiter (n:m)

Die Beziehung zwischen `Kurs` und `Kursleiter` ist vom Typ n:m (Viele-zu-Viele).

**Begründung:**

- Ein Kurs kann von mehreren Kursleitern betreut werden.
- Ein Kursleiter kann mehrere Kurse betreuen.

Deshalb können mehrere Kurse mehreren Kursleitern zugeordnet werden.

Diese Beziehung wird durch die Verbindungstabelle `KursKursleiter` umgesetzt.

### 4.2 Beziehung zwischen Kurs und Teilnehmer (n:m)

Die Beziehung zwischen `Kurs` und `Teilnehmer` ist ebenfalls vom Typ n:m (Viele-zu-Viele).

**Begründung:**

- Ein Kurs kann von mehreren Teilnehmern besucht werden.
- Ein Teilnehmer kann an mehreren Kursen teilnehmen.

Deshalb können mehrere Teilnehmer mehreren Kursen zugeordnet werden.

Diese Beziehung wird durch die Verbindungstabelle `KursTeilnehmer` umgesetzt.

### 4.3 Beziehungen zwischen Haupttabellen und Verbindungstabellen (1:n)

**Kurs zu KursKursleiter (1:n):**

Ein Kurs kann mehrere Einträge in der Tabelle `KursKursleiter` haben. Jeder Eintrag gehört aber genau zu einem Kurs.

**Kursleiter zu KursKursleiter (1:n):**

Ein Kursleiter kann mehreren Einträgen in der Tabelle `KursKursleiter` zugeordnet sein. Jeder Eintrag verweist aber genau auf einen Kursleiter.

**Kurs zu KursTeilnehmer (1:n):**

Ein Kurs kann mehrere Einträge in der Tabelle `KursTeilnehmer` haben. Jeder Eintrag gehört aber genau zu einem Kurs.

**Teilnehmer zu KursTeilnehmer (1:n):**

Ein Teilnehmer kann mehrere Einträge in der Tabelle `KursTeilnehmer` haben. Jeder Eintrag verweist aber genau auf einen Teilnehmer.

### 4.4 Zusammenfassung der Kardinalitäten

| Beziehung | Typ | Begründung |
			|---|---|---|
| Kurs – Kursleiter | n:m | Ein Kurs hat mehrere Kursleiter und ein Kursleiter betreut mehrere Kurse. |
| Kurs – Teilnehmer | n:m | Ein Kurs hat mehrere Teilnehmer und ein Teilnehmer besucht mehrere Kurse. |
| Kurs – KursKursleiter | 1:n | Ein Kurs kann mehrere Zuordnungseinträge besitzen. |
| Kursleiter – KursKursleiter | 1:n | Ein Kursleiter kann mehreren Zuordnungseinträgen zugeordnet sein. |
| Kurs – KursTeilnehmer | 1:n | Ein Kurs kann mehrere Anmeldungen besitzen. |
| Teilnehmer – KursTeilnehmer | 1:n | Ein Teilnehmer kann mehrere Anmeldungen besitzen. |

**Warum werden n:m-Beziehungen aufgelöst?**

In einem relationalen Datenbankmodell werden n:m-Beziehungen mithilfe von Verbindungstabellen umgesetzt. Dadurch entstehen jeweils zwei 1:n-Beziehungen.

Diese Umsetzung erleichtert die Verwaltung der Daten und verhindert unnötige Datenwiederholungen.

## 5. Primärschlüssel und Fremdschlüssel

Jede Tabelle besitzt einen Primärschlüssel (PRIMARY KEY). Dieser identifiziert jeden Datensatz eindeutig.

Die Verbindungstabellen besitzen zusätzlich Fremdschlüssel (FOREIGN KEY). Diese stellen sicher, dass nur vorhandene Kurse, Kursleiter und Teilnehmer miteinander verbunden werden können.

Die IDs werden mit `IDENTITY(1,1)` automatisch erzeugt.

Zusätzlich verhindern die UNIQUE-Einschränkungen doppelte Kursleiterzuordnungen und doppelte Anmeldungen desselben Teilnehmers für denselben Kurs.

## 6. Begründung der Datenbankstruktur

Die Datenbank wurde in fünf Tabellen aufgeteilt, damit Informationen nicht unnötig mehrfach gespeichert werden.

Die persönlichen Daten der Teilnehmer und Kursleiter werden jeweils nur in einer eigenen Tabelle gespeichert.

Die Verbindungstabellen ermöglichen die Verwaltung der n:m-Beziehungen zwischen Kursen, Kursleitern und Teilnehmern.

Das Datenbankdesign ist dadurch übersichtlich, nachvollziehbar und erweiterbar.

## 7. Testdaten und SQL-Abfragen

Jede der fünf Tabellen wurde mit mindestens fünf Beispieldatensätzen gefüllt.

Zur Überprüfung der Datenbank wurde eine SELECT-Abfrage mit mehreren INNER JOIN-Verknüpfungen erstellt.

Damit können die Namen der Teilnehmer, die zugehörigen Kurse, die Kursleiter und die Anmeldedaten gemeinsam angezeigt werden.

Die Datenbank wurde mit SQL Server Management Studio erstellt und erfolgreich getestet.

## 8. Verwendung von KI

Für die Dokumentation des Datenbankdesigns wurde ChatGPT als Unterstützung verwendet.

**Verwendeter Prompt:**

„Ich habe bereits ein eigenes Datenbankdesign für mein Schwimmbad-Kursplanung-Projekt erstellt. Die Datenbank besteht aus den Tabellen Kurs, Kursleiter, Teilnehmer, KursKursleiter und KursTeilnehmer.

Hilf mir dabei, mein bestehendes Datenbankdesign verständlich und strukturiert zu dokumentieren. Erkläre die Aufgaben der einzelnen Tabellen, die verwendeten Datentypen, Primär- und Fremdschlüssel sowie die Beziehungen zwischen den Tabellen (1:n und n:m). Begründe außerdem, warum die jeweiligen Beziehungen diesen Typ haben.

Wichtig: Erstelle kein neues Datenbankdesign und ändere meine Tabellenstruktur nicht. Unterstütze mich ausschließlich bei der schriftlichen Dokumentation und bei der verständlichen Erklärung meiner bereits erstellten Datenbank.“

**Verwendung des KI-Ergebnisses:**

ChatGPT wurde ausschließlich zur Unterstützung bei der schriftlichen Dokumentation und zur Erklärung der Datenbankstruktur verwendet. Die Datenbanktabellen, Beziehungen und SQL-Befehle wurden selbst erstellt und umgesetzt.

