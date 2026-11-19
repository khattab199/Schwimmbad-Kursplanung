
using SchwimmbadConsole.SchwimmbadConsole;
using System;
using System.Collections.Generic;
using System.IO;

namespace SchwimmbadConsole
{
    public class KursDatenzugriff
    {
        private string dateiPfad =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "kurse.txt");

        public List<Kurs> KurseLaden()
        {
            List<Kurs> kurse = new List<Kurs>();

            if (!File.Exists(dateiPfad))
            {
                Console.WriteLine("Die Datei kurse.txt wurde nicht gefunden.");
                return kurse;
            }

            string[] zeilen = File.ReadAllLines(dateiPfad);

            foreach (string zeile in zeilen)
            {
                if (string.IsNullOrWhiteSpace(zeile))
                    continue;

                string[] daten = zeile.Split(';');

                if (daten.Length == 6 &&
                    int.TryParse(daten[0], out int kursID) &&
                    int.TryParse(daten[5], out int maxTeilnehmer))
                {
                    Kurs kurs = new Kurs(
                        kursID,
                        daten[1],
                        daten[2],
                        daten[3],
                        daten[4],
                        maxTeilnehmer);

                    kurse.Add(kurs);
                }
                else
                {
                    Console.WriteLine("Ungültiger Datensatz: " + zeile);
                }
            }

            return kurse;
        }

        public void KursSpeichern(Kurs kurs)
        {
            string zeile =
                kurs.KursID + ";" +
                kurs.Kursname + ";" +
                kurs.Wochentag + ";" +
                kurs.Beginn + ";" +
                kurs.Ende + ";" +
                kurs.MaxTeilnehmer;

            File.AppendAllText(
                dateiPfad,
                zeile + Environment.NewLine);
        }
    }
}
