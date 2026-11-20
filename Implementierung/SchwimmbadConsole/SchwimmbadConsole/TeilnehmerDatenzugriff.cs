
using SchwimmbadConsole.SchwimmbadConsole;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SchwimmbadConsole
{
    public class TeilnehmerDatenzugriff
    {
        private string dateiPfad = "teilnehmer.txt";

        // Alle Teilnehmer aus der Textdatei laden
        public List<Teilnehmer> TeilnehmerLaden()
        {
            List<Teilnehmer> teilnehmerListe = new List<Teilnehmer>();

            if (!File.Exists(dateiPfad))
            {
                return teilnehmerListe;
            }

            string[] zeilen = File.ReadAllLines(dateiPfad);

            foreach (string zeile in zeilen)
            {
                if (string.IsNullOrWhiteSpace(zeile))
                {
                    continue;
                }

                string[] daten = zeile.Split(';');

                if (daten.Length == 7 &&
                    int.TryParse(daten[0], out int teilnehmerID) &&
                    int.TryParse(daten[6], out int kursID))
                {
                    Teilnehmer teilnehmer = new Teilnehmer(
                        teilnehmerID,
                        daten[1],
                        daten[2],
                        daten[3],
                        daten[4],
                        daten[5],
                        kursID
                    );

                    teilnehmerListe.Add(teilnehmer);
                }
                else
                {
                    Console.WriteLine("Ungültiger Teilnehmerdatensatz: " + zeile);
                }
            }

            return teilnehmerListe;
        }

        // Einen neuen Teilnehmer in der Textdatei speichern

        public void TeilnehmerSpeichern(Teilnehmer teilnehmer)
        {
            string zeile =
                teilnehmer.TeilnehmerID + ";" +
                teilnehmer.Vorname + ";" +
                teilnehmer.Nachname + ";" +
                teilnehmer.Geburtsdatum + ";" +
                teilnehmer.Telefon + ";" +
                teilnehmer.Email + ";" +
                teilnehmer.KursID;

            // Datei im Ausgabeverzeichnis
            File.AppendAllText(
                dateiPfad,
                zeile + Environment.NewLine
            );

            // Datei im Projektordner
            string projektDateiPfad =
                @"C:\Users\khale\OneDrive\Desktop\Schwimmbad-Kursplanung\Implementierung\SchwimmbadConsole\SchwimmbadConsole\teilnehmer.txt";

            File.AppendAllText(
                projektDateiPfad,
                zeile + Environment.NewLine
            );

            Console.WriteLine("Teilnehmer wurde in beiden Dateien gespeichert.");
        }



        public void KursZuordnungEntfernen(int kursID)
        {
            // Alle Teilnehmer laden
            List<Teilnehmer> teilnehmerListe = TeilnehmerLaden();

            // Teilnehmer durchlaufen
            foreach (Teilnehmer teilnehmer in teilnehmerListe)
            {
                if (teilnehmer.KursID == kursID)
                {
                    // Kurszuordnung entfernen
                    teilnehmer.KursID = 0;
                }
            }

            // Teilnehmer wieder als Textzeilen speichern
            List<string> zeilen = new List<string>();

            foreach (Teilnehmer teilnehmer in teilnehmerListe)
            {
                string zeile =
                    teilnehmer.TeilnehmerID + ";" +
                    teilnehmer.Vorname + ";" +
                    teilnehmer.Nachname + ";" +
                    teilnehmer.Geburtsdatum + ";" +
                    teilnehmer.Telefon + ";" +
                    teilnehmer.Email + ";" +
                    teilnehmer.KursID;

                zeilen.Add(zeile);
            }

            File.WriteAllLines(dateiPfad, zeilen);
        }



        public void TeilnehmerLoeschen(int teilnehmerID)
        {
            // Alle Teilnehmer laden
            List<Teilnehmer> teilnehmerListe = TeilnehmerLaden();

            // Teilnehmer anhand der ID suchen
            Teilnehmer? teilnehmer = teilnehmerListe.FirstOrDefault(t => t.TeilnehmerID == teilnehmerID);

            // Prüfen, ob Teilnehmer existiert
            if (teilnehmer == null)
            {
                Console.WriteLine("Teilnehmer wurde nicht gefunden!");
                return;
            }

            // Teilnehmer aus der Liste entfernen
            teilnehmerListe.Remove(teilnehmer);

            // Verbleibende Teilnehmer als Textzeilen speichern
            List<string> zeilen = new List<string>();

            foreach (Teilnehmer vorhandenerTeilnehmer in teilnehmerListe)
            {
                string zeile =
                    vorhandenerTeilnehmer.TeilnehmerID + ";" +
                    vorhandenerTeilnehmer.Vorname + ";" +
                    vorhandenerTeilnehmer.Nachname + ";" +
                    vorhandenerTeilnehmer.Geburtsdatum + ";" +
                    vorhandenerTeilnehmer.Telefon + ";" +
                    vorhandenerTeilnehmer.Email + ";" +
                    vorhandenerTeilnehmer.KursID;

                zeilen.Add(zeile);
            }

            // Datei aktualisieren
            File.WriteAllLines(dateiPfad, zeilen);
        }




    }
}
