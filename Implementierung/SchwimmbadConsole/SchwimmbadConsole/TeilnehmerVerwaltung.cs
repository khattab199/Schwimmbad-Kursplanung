
using SchwimmbadConsole.SchwimmbadConsole;
using System;
using System.Collections.Generic;

namespace SchwimmbadConsole
{
    public class TeilnehmerVerwaltung
    {
        private TeilnehmerDatenzugriff datenZugriff;
        private List<Teilnehmer> teilnehmerListe;

        private KursDatenzugriff kursDatenzugriff;
        private List<Kurs> kurse;


        public TeilnehmerVerwaltung()
        {
            datenZugriff = new TeilnehmerDatenzugriff();
            teilnehmerListe = datenZugriff.TeilnehmerLaden();

            kursDatenzugriff = new KursDatenzugriff();
            kurse = kursDatenzugriff.KurseLaden();
        }

        // Einen Teilnehmer anmelden
        public void TeilnehmerAnmelden()
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("        TEILNEHMER ANMELDEN");
            Console.WriteLine("==================================");

            Console.Write("Vorname: ");
            string vorname = Console.ReadLine() ?? "";

            Console.Write("Nachname: ");
            string nachname = Console.ReadLine() ?? "";

            Console.Write("Geburtsdatum (TT.MM.JJJJ): ");
            string geburtsdatum = Console.ReadLine() ?? "";

            Console.Write("Telefon: ");
            string telefon = Console.ReadLine() ?? "";

            Console.Write("E-Mail: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Kurs-ID: ");

            if (!int.TryParse(Console.ReadLine(), out int kursID))
            {
                Console.WriteLine("Ungültige Kurs-ID!");
                Console.ReadKey();
                return;
            }


            bool kursExistiert = false;

            foreach (Kurs kurs in kurse)
            {
                if (kurs.KursID == kursID)
                {
                    kursExistiert = true;
                    break;
                }
            }

            if (!kursExistiert)
            {
                Console.WriteLine("Dieser Schwimmkurs existiert nicht!");
                Console.ReadKey();
                return;
            }



            Kurs? ausgewaehlterKurs = null;

            foreach (Kurs kurs in kurse)
            {
                if (kurs.KursID == kursID)
                {
                    ausgewaehlterKurs = kurs;
                    break;
                }
            }

            int anzahlTeilnehmer = 0;

            foreach (Teilnehmer teilnehmer in teilnehmerListe)
            {
                if (teilnehmer.KursID == kursID)
                {
                    anzahlTeilnehmer++;
                }
            }

            if (anzahlTeilnehmer >= ausgewaehlterKurs.MaxTeilnehmer)
            {
                Console.WriteLine("Dieser Schwimmkurs ist bereits voll!");
                Console.ReadKey();
                return;
            }




            if (string.IsNullOrWhiteSpace(vorname) ||
                string.IsNullOrWhiteSpace(nachname))
            {
                Console.WriteLine("Vorname und Nachname sind erforderlich!");
                Console.ReadKey();
                return;
            }

            int neueID = 1;

            if (teilnehmerListe.Count > 0)
            {
                neueID = teilnehmerListe.Max(t => t.TeilnehmerID) + 1;
            }

            Teilnehmer neuerTeilnehmer = new Teilnehmer(
                neueID,
                vorname,
                nachname,
                geburtsdatum,
                telefon,
                email,
                kursID
            );

            datenZugriff.TeilnehmerSpeichern(neuerTeilnehmer);
            teilnehmerListe.Add(neuerTeilnehmer);

            Console.WriteLine();
            Console.WriteLine("Teilnehmer wurde erfolgreich angemeldet!");
            Console.WriteLine("Teilnehmer-ID: " + neueID);

            Console.WriteLine();
            Console.WriteLine("Drücke eine Taste zum Zurückkehren.");
            Console.ReadKey();

        }





        public void AlleTeilnehmerAnzeigen()
        {
            Console.Clear();

            teilnehmerListe = datenZugriff.TeilnehmerLaden();
            kurse = kursDatenzugriff.KurseLaden();

            Console.WriteLine("==============================================");
            Console.WriteLine("             ALLE TEILNEHMER");
            Console.WriteLine("==============================================");

            if (teilnehmerListe.Count == 0)
            {
                Console.WriteLine("Es sind noch keine Teilnehmer angemeldet.");
            }
            else
            {
                Console.WriteLine(
                    "{0,-5} {1,-12} {2,-15} {3,-13} {4,-16} {5,-25} {6,-8} {7,-22}",
                    "ID", "Vorname", "Nachname", "Geburtsdatum",
                    "Telefon", "E-Mail", "Kurs-ID", "Kursname");

                Console.WriteLine(new string('-', 125));

                foreach (Teilnehmer teilnehmer in teilnehmerListe)
                {
                    string kursname = "Unbekannt";

                    foreach (Kurs kurs in kurse)
                    {
                        if (kurs.KursID == teilnehmer.KursID)
                        {
                            kursname = kurs.Kursname;
                            break;
                        }
                    }

                    Console.WriteLine(
                        "{0,-5} {1,-12} {2,-15} {3,-13} {4,-16} {5,-25} {6,-8} {7,-22}",
                        teilnehmer.TeilnehmerID,
                        teilnehmer.Vorname,
                        teilnehmer.Nachname,
                        teilnehmer.Geburtsdatum,
                        teilnehmer.Telefon,
                        teilnehmer.Email,
                        teilnehmer.KursID,
                        kursname);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Drücke eine Taste zum Zurückkehren.");
            Console.ReadKey();
        }


        public void Teilnehmermenue()
        {
            bool menueLaeuft = true;

            while (menueLaeuft)
            {
                Console.Clear();

                Console.WriteLine("==================================");
                Console.WriteLine("      TEILNEHMERVERWALTUNG");
                Console.WriteLine("==================================");
                Console.WriteLine();
                Console.WriteLine("1. Teilnehmer anmelden");
                Console.WriteLine("2. Alle Teilnehmer anzeigen");
                Console.WriteLine("0. Zurück zum Hauptmenü");
                Console.WriteLine();
                Console.Write("Bitte wählen Sie eine Option: ");

                string? auswahl = Console.ReadLine();

                switch (auswahl)
                {
                    case "1":
                        TeilnehmerAnmelden();
                        break;

                    case "2":
                        AlleTeilnehmerAnzeigen();
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



    }
}
