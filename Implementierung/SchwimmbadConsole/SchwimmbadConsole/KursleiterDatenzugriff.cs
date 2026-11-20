
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.ConstrainedExecution;

namespace SchwimmbadConsole
{
    public class KursleiterDatenzugriff
  {
            //        Die einzelnen Bestandteile
            //1. AppDomain

            //Stellt einen Ausführungsbereich für unser Programm dar.

            //2. CurrentDomain

            //Bedeutet: der aktuelle Ausführungsbereich unseres Programms.

            //3. BaseDirectory

            //Gibt den Basisordner zurück, in dem unser Programm ausgeführt wird.
            //Beispiel aus unserem Projekt
            //Angenommen, unser Programm wird hier ausgeführt:
            //C:\SchwimmbadConsole\bin\Debug\net10.0\
        private string dateiPfad =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "kursleiter.txt");

        public List<Kursleiter> KursleiterLaden()
        {
            List<Kursleiter> kursleiterListe = new List<Kursleiter>();

            if (!File.Exists(dateiPfad))
                return kursleiterListe;

            string[] zeilen = File.ReadAllLines(dateiPfad);

            foreach (string zeile in zeilen)
            {
                if (string.IsNullOrWhiteSpace(zeile))
                    continue;

                string[] daten = zeile.Split(';');

                if (daten.Length == 5 &&
                    int.TryParse(daten[0], out int kursleiterID))
                {
                    Kursleiter kursleiter = new Kursleiter(
                        kursleiterID,
                        daten[1],
                        daten[2],
                        daten[3],
                        daten[4]);

                    kursleiterListe.Add(kursleiter);
                }
                else
                {
                    Console.WriteLine(
                        "Ungültiger Kursleiterdatensatz: " + zeile);
                }
            }

            return kursleiterListe;
        }




        public void KursleiterSpeichern(Kursleiter kursleiter)
        {
            string zeile =
                kursleiter.KursleiterID + ";" +
                kursleiter.Vorname + ";" +
                kursleiter.Nachname + ";" +
                kursleiter.Telefon + ";" +
                kursleiter.Email;

            // Speicherung im Ausführungsordner
            File.AppendAllText(
                dateiPfad, zeile + Environment.NewLine);

            // Pfad zum eigentlichen Projektordner
            //Das liefert den Ordner, in dem unser Programm ausgeführt wird.
            //C:\SchwimmbadConsole\bin\Debug\net10.0\
            //Jedes .. bedeutet: Gehe einen Ordner nach oben.
            //Path.GetFullPath(...):Diese Methode wandelt den zusammengesetzten Pfad in einen vollständigen Pfad um.
            //das alle methode liefert diese zeile
            //string projektDateiPfad =@"C:\Users\khale\OneDrive\Desktop\Schwimmbad-Kursplanung\Implementierung\SchwimmbadConsole\SchwimmbadConsole\kursleiter.txt";

            string projektDateiPfad = Path.GetFullPath(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "..", "..", "..", "kursleiter.txt"));

            // Speicherung im Projektordner
            File.AppendAllText(
                projektDateiPfad, zeile + Environment.NewLine);

            Console.WriteLine("Kursleiter wurde an beiden Speicherorten gespeichert!");
            Console.WriteLine("Projektdatei: " + projektDateiPfad);
        }


        public void KursleiterLoeschen(int kursleiterID)
        {
            // Alle Kursleiter laden
            List<Kursleiter> kursleiterListe = KursleiterLaden();

            // Kursleiter anhand der ID suchen
            Kursleiter? kursleiter = kursleiterListe.FirstOrDefault(k => k.KursleiterID == kursleiterID);

            // Prüfen, ob Kursleiter existiert
            if (kursleiter == null)
            {
                Console.WriteLine("Kursleiter wurde nicht gefunden!");
                return;
            }

            // Kursleiter aus der Liste entfernen
            kursleiterListe.Remove(kursleiter);

            // Verbleibende Kursleiter als Textzeilen speichern
            List<string> zeilen = new List<string>();

            foreach (Kursleiter vorhandenerKursleiter in kursleiterListe)
            {
                string zeile =
                    vorhandenerKursleiter.KursleiterID + ";" +
                    vorhandenerKursleiter.Vorname + ";" +
                    vorhandenerKursleiter.Nachname + ";" +
                    vorhandenerKursleiter.Telefon + ";" +
                    vorhandenerKursleiter.Email;

                zeilen.Add(zeile);
            }

            // Datei aktualisieren
            File.WriteAllLines(dateiPfad, zeilen);
        }


    }

}

