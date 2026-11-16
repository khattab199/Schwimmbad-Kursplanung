
using SchwimmbadConsole.SchwimmbadConsole;
using System;
using System.Collections.Generic;

namespace SchwimmbadConsole
{
    public class KursVerwaltung
    {
        private KursDatenzugriff datenZugriff;

        public KursVerwaltung()
        {
            datenZugriff = new KursDatenzugriff();
        }


        public void Kursmenue()
        {
            List<Kurs> kurse = datenZugriff.KurseLaden();

            bool menueLaeuft = true;

            while (menueLaeuft)
            {
                Console.Clear();

                Console.WriteLine("==================================");
                Console.WriteLine("        KURSVERWALTUNG");
                Console.WriteLine("==================================");
                Console.WriteLine();
                Console.WriteLine("1. Alle Schwimmkurse anzeigen");
                Console.WriteLine("0. Zurück zum Hauptmenü");
                Console.WriteLine();
                Console.Write("Bitte wählen Sie eine Option: ");

                string? auswahl = Console.ReadLine();

                switch (auswahl)
                {
                    case "1":
                        AlleKurseAnzeigen(kurse);

                        Console.WriteLine();
                        Console.WriteLine("Drücke eine Taste zum Zurückkehren.");
                        Console.ReadKey();
                        break;

                    case "0":
                        menueLaeuft = false;
                        break;

                    default:
                        Console.WriteLine("Ungültige Auswahl!");
                        Console.ReadKey();
                        break;
                }
            }
        }


        public void AlleKurseAnzeigen(List<Kurs> kurse)
        {
            Console.Clear();

            Console.WriteLine("========== SCHWIMMKURSE ==========");

            if (kurse.Count == 0)
            {
                Console.WriteLine("Es sind keine Kurse vorhanden.");
                return;
            }

            Console.WriteLine(
                "{0,-5} {1,-22} {2,-12} {3,-8} {4,-8} {5,-12}",
                "ID", "Kursname", "Wochentag",
                "Beginn", "Ende", "Max. TN"
            );

            Console.WriteLine(new string('-', 75));

            foreach (Kurs kurs in kurse)
            {
                Console.WriteLine(
                    "{0,-5} {1,-22} {2,-12} {3,-8} {4,-8} {5,-12}",
                    kurs.KursID,
                    kurs.Kursname,
                    kurs.Wochentag,
                    kurs.Beginn,
                    kurs.Ende,
                    kurs.MaxTeilnehmer
                );
            }

            Console.WriteLine("==================================");
        }
    }
}
