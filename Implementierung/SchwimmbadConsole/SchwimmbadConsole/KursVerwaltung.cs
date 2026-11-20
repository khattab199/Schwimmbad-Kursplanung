
using SchwimmbadConsole.SchwimmbadConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchwimmbadConsole
{
    public class KursVerwaltung
    {
        private KursDatenzugriff datenZugriff;

        private KursKursleiterDatenzugriff kursKursleiterDatenzugriff;
        private TeilnehmerDatenzugriff teilnehmerDatenzugriff;


        public KursVerwaltung()
        {
            datenZugriff = new KursDatenzugriff();

            kursKursleiterDatenzugriff = new KursKursleiterDatenzugriff();

            teilnehmerDatenzugriff = new TeilnehmerDatenzugriff();
        }




        public void KursAnlegen()
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("          KURS ANLEGEN");
            Console.WriteLine("==================================");

            Console.Write("Kursname: ");
            string kursname = Console.ReadLine() ?? "";

            Console.Write("Wochentag: ");
            string wochentag = Console.ReadLine() ?? "";

            Console.Write("Beginn (z.B. 14:00): ");
            string beginn = Console.ReadLine() ?? "";

            Console.Write("Ende (z.B. 15:00): ");
            string ende = Console.ReadLine() ?? "";

            Console.Write("Maximale Teilnehmerzahl: ");

            if (!int.TryParse(Console.ReadLine(), out int maxTeilnehmer) ||
                maxTeilnehmer <= 0)
            {
                Console.WriteLine("Ungültige Teilnehmerzahl!");
                Console.ReadKey();
                return;
            }

            if (string.IsNullOrWhiteSpace(kursname) ||
                string.IsNullOrWhiteSpace(wochentag) ||
                string.IsNullOrWhiteSpace(beginn) ||
                string.IsNullOrWhiteSpace(ende))
            {
                Console.WriteLine("Bitte alle Felder ausfüllen!");
                Console.ReadKey();
                return;
            }

            List<Kurs> kurse = datenZugriff.KurseLaden();

            int neueID = 1;

            if (kurse.Count > 0)
            {
                neueID = kurse.Max(k => k.KursID) + 1;
            }

            Kurs neuerKurs = new Kurs(
                neueID,
                kursname,
                wochentag,
                beginn,
                ende,
                maxTeilnehmer);

            datenZugriff.KursSpeichern(neuerKurs);

            Console.WriteLine();
            Console.WriteLine("Kurs wurde erfolgreich angelegt!");
            Console.WriteLine("Neue Kurs-ID: " + neueID);

            Console.WriteLine();
            Console.WriteLine("Drücke eine Taste zum Zurückkehren.");
            Console.ReadKey();
        }



        public void KursLoeschen()
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("        SCHWIMMKURS LÖSCHEN");
            Console.WriteLine("==================================");

            // Alle Kurse laden
            List<Kurs> kurse = datenZugriff.KurseLaden();

            // Prüfen, ob Kurse vorhanden sind
            if (kurse.Count == 0)
            {
                Console.WriteLine("Es sind keine Kurse vorhanden.");
                Console.ReadKey();
                return;
            }

            // Alle Kurse anzeigen
            AlleKurseAnzeigen(kurse);

            Console.WriteLine();
            Console.Write("Bitte geben Sie die Kurs-ID ein: ");

            if (!int.TryParse(Console.ReadLine(), out int kursID))
            {
                Console.WriteLine("Ungültige Kurs-ID!");
                Console.ReadKey();
                return;
            }

            // Prüfen, ob der Kurs existiert
            Kurs? kurs = kurse.FirstOrDefault(k => k.KursID == kursID);

            if (kurs == null)
            {
                Console.WriteLine("Dieser Kurs existiert nicht!");
                Console.ReadKey();
                return;
            }

            // Kurs anzeigen und Bestätigung verlangen
            Console.WriteLine();
            Console.WriteLine("Ausgewählter Kurs: " + kurs.Kursname);
            Console.Write("Möchten Sie diesen Kurs wirklich löschen? (j/n): ");

            string? bestaetigung = Console.ReadLine();

            if (bestaetigung?.ToLower() == "j")
            {
                // Kursleiter-Zuordnungen löschen
                kursKursleiterDatenzugriff.KursZuordnungenLoeschen(kursID);

                // Teilnehmer vom Kurs trennen
                teilnehmerDatenzugriff.KursZuordnungEntfernen(kursID);

                // Kurs löschen
                datenZugriff.KursLoeschen(kursID);

                Console.WriteLine("Kurs und zugehörige Zuordnungen wurden erfolgreich gelöscht!");
            }
            else
            {
                Console.WriteLine("Löschen wurde abgebrochen.");
            }

            Console.WriteLine();
            Console.WriteLine("Drücke eine Taste zum Zurückkehren.");
            Console.ReadKey();
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
                Console.WriteLine("2. Neuen Schwimmkurs anlegen");
                Console.WriteLine("3. Schwimmkurs löschen");
                Console.WriteLine("0. Zurück zum Hauptmenü"); 
                Console.WriteLine();
                Console.Write("Bitte wählen Sie eine Option: ");

                string? auswahl = Console.ReadLine();

                switch (auswahl)
                {
                    case "1":
                        kurse = datenZugriff.KurseLaden();

                        AlleKurseAnzeigen(kurse);

                        Console.WriteLine();
                        Console.WriteLine("Drücke eine Taste zum Zurückkehren.");
                        Console.ReadKey();
                        break;

                    case "2":
                        KursAnlegen();
                        break;

                    case "3":
                        KursLoeschen();
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
