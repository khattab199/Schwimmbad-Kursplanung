
using System;
using System.Collections.Generic;
using System.IO;

namespace SchwimmbadConsole
{
    public class KursKursleiterDatenzugriff
    {
        private string dateiPfad =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "kurskursleiter.txt");

        public List<KursKursleiter> ZuordnungenLaden()
        {
            List<KursKursleiter> zuordnungen =
                new List<KursKursleiter>();

            if (!File.Exists(dateiPfad))
                return zuordnungen;

            string[] zeilen = File.ReadAllLines(dateiPfad);

            foreach (string zeile in zeilen)
            {
                if (string.IsNullOrWhiteSpace(zeile))
                    continue;

                string[] daten = zeile.Split(';');

                if (daten.Length == 2 &&
                    int.TryParse(daten[0], out int kursID) &&
                    int.TryParse(daten[1], out int kursleiterID))
                {
                    KursKursleiter zuordnung =
                        new KursKursleiter(kursID, kursleiterID);

                    zuordnungen.Add(zuordnung);
                }
                else
                {
                    Console.WriteLine(
                        "Ungültiger Zuordnungsdatensatz: " + zeile);
                }
            }

            return zuordnungen;
        }



        public void ZuordnungSpeichern(KursKursleiter zuordnung)
        {
            string zeile =
                zuordnung.KursID + ";" +
                zuordnung.KursleiterID;

            // Prüfen, ob die Datei bereits existiert
            if (File.Exists(dateiPfad))
            {
                string inhalt = File.ReadAllText(dateiPfad);

                // Fehlenden Zeilenumbruch ergänzen
                if (inhalt.Length > 0 &&
                    !inhalt.EndsWith(Environment.NewLine))
                {
                    File.AppendAllText(
                        dateiPfad, Environment.NewLine);
                }
            }

            // Neue Zuordnung speichern
            File.AppendAllText(
                dateiPfad,
                zeile + Environment.NewLine);
        }



        public void KursZuordnungenLoeschen(int kursID)
        {
            // Alle Zuordnungen laden
            List<KursKursleiter> zuordnungen = ZuordnungenLaden();

            // Zuordnungen des ausgewählten Kurses entfernen
            zuordnungen.RemoveAll(z => z.KursID == kursID);

            // Verbleibende Zuordnungen als Text speichern
            List<string> zeilen = new List<string>();

            foreach (KursKursleiter zuordnung in zuordnungen)
            {
                string zeile =
                    zuordnung.KursID + ";" +
                    zuordnung.KursleiterID;

                zeilen.Add(zeile);
            }

            File.WriteAllLines(dateiPfad, zeilen);
        }



        

        public void KursleiterZuordnungenLoeschen(int kursleiterID)
        {
            // Alle Zuordnungen laden
            List<KursKursleiter> zuordnungen = ZuordnungenLaden();

            // Alle Zuordnungen dieses Kursleiters entfernen
            zuordnungen.RemoveAll(
                z => z.KursleiterID == kursleiterID);

            // Verbleibende Zuordnungen als Textzeilen speichern
            List<string> zeilen = new List<string>();

            foreach (KursKursleiter zuordnung in zuordnungen)
            {
                string zeile =
                    zuordnung.KursID + ";" +
                    zuordnung.KursleiterID;

                zeilen.Add(zeile);
            }

            // Datei aktualisieren
            File.WriteAllLines(dateiPfad, zeilen);
        }



    }
}
