
using SchwimmbadConsole.SchwimmbadConsole;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchwimmbadConsole
{
    public class KursleiterVerwaltung
    {
        private KursleiterDatenzugriff datenZugriff;
        private List<Kursleiter> kursleiterListe;
        private KursDatenzugriff kursDatenzugriff;
        private KursKursleiterDatenzugriff zuordnungsDatenzugriff;

        public KursleiterVerwaltung()
        {
            datenZugriff = new KursleiterDatenzugriff();
            kursleiterListe = datenZugriff.KursleiterLaden();
            kursDatenzugriff = new KursDatenzugriff();
            zuordnungsDatenzugriff = new KursKursleiterDatenzugriff();
        }





        public void KursleiterZuordnen()
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("       KURSLEITER ZUORDNEN");
            Console.WriteLine("==================================");

            // Vorhandene Kurse laden
            List<Kurs> kurse = kursDatenzugriff.KurseLaden();

            // Vorhandene Kursleiter laden
            kursleiterListe = datenZugriff.KursleiterLaden();

            if (kurse.Count == 0 || kursleiterListe.Count == 0)
            {
                Console.WriteLine(
                    "Es müssen mindestens ein Kurs und ein Kursleiter vorhanden sein.");

                Console.ReadKey();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Vorhandene Schwimmkurse:");

            foreach (Kurs kurs in kurse)
            {
                Console.WriteLine(
                    kurs.KursID + " - " + kurs.Kursname);
            }

            Console.WriteLine();
            Console.Write("Bitte Kurs-ID eingeben: ");

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
                Console.WriteLine("Dieser Kurs existiert nicht!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Vorhandene Kursleiter:");

            foreach (Kursleiter kursleiter in kursleiterListe)
            {
                Console.WriteLine(
                    kursleiter.KursleiterID + " - " +
                    kursleiter.Vorname + " " +
                    kursleiter.Nachname);
            }

            Console.WriteLine();
            Console.Write("Bitte Kursleiter-ID eingeben: ");

            if (!int.TryParse(Console.ReadLine(), out int kursleiterID))
            {
                Console.WriteLine("Ungültige Kursleiter-ID!");
                Console.ReadKey();
                return;
            }

            bool kursleiterExistiert = false;

            foreach (Kursleiter kursleiter in kursleiterListe)
            {
                if (kursleiter.KursleiterID == kursleiterID)
                {
                    kursleiterExistiert = true;
                    break;
                }
            }

            if (!kursleiterExistiert)
            {
                Console.WriteLine("Dieser Kursleiter existiert nicht!");
                Console.ReadKey();
                return;
            }

            // Vorhandene Zuordnungen laden
            List<KursKursleiter> zuordnungen =
                zuordnungsDatenzugriff.ZuordnungenLaden();

            // Prüfen, ob die Zuordnung bereits existiert
            foreach (KursKursleiter zuordnung in zuordnungen)
            {
                if (zuordnung.KursID == kursID &&
                    zuordnung.KursleiterID == kursleiterID)
                {
                    Console.WriteLine(
                        "Dieser Kursleiter ist diesem Kurs bereits zugeordnet!");

                    Console.ReadKey();
                    return;
                }
            }

            // Neue Zuordnung speichern
            KursKursleiter neueZuordnung =
                new KursKursleiter(kursID, kursleiterID);

            zuordnungsDatenzugriff.ZuordnungSpeichern(neueZuordnung);

            Console.WriteLine();
            Console.WriteLine("Kursleiter wurde erfolgreich zugeordnet!");

            Console.ReadKey();
        }




        public void AlleZuordnungenAnzeigen()
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("       KURSLEITER-ZUORDNUNGEN");
            Console.WriteLine("======================================");

            // Kurse laden
            List<Kurs> kurse = kursDatenzugriff.KurseLaden();

            // Kursleiter laden
            kursleiterListe = datenZugriff.KursleiterLaden();

            // Zuordnungen laden
            List<KursKursleiter> zuordnungen =
                zuordnungsDatenzugriff.ZuordnungenLaden();

            if (zuordnungen.Count == 0)
            {
                Console.WriteLine("Es sind noch keine Zuordnungen vorhanden.");
            }
            else
            {
                foreach (Kurs kurs in kurse)
                {
                    Console.WriteLine();
                    Console.WriteLine("Kurs: " + kurs.Kursname);

                    bool kursHatKursleiter = false;

                    foreach (KursKursleiter zuordnung in zuordnungen)
                    {
                        if (zuordnung.KursID == kurs.KursID)
                        {
                            foreach (Kursleiter kursleiter in kursleiterListe)
                            {
                                if (kursleiter.KursleiterID ==
                                    zuordnung.KursleiterID)
                                {
                                    Console.WriteLine(
                                        "Kursleiter: " +
                                        kursleiter.Vorname + " " +
                                        kursleiter.Nachname);

                                    kursHatKursleiter = true;
                                }
                            }
                        }
                    }

                    if (!kursHatKursleiter)
                    {
                        Console.WriteLine("Noch kein Kursleiter zugeordnet.");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine("Drücke eine Taste zum Zurückkehren.");
            Console.ReadKey();
        }




        public void KursleiterAnlegen()
        {
            Console.Clear();

            Console.WriteLine("==================================");
            Console.WriteLine("        KURSLEITER ANLEGEN");
            Console.WriteLine("==================================");

            Console.Write("Vorname: ");
            string vorname = Console.ReadLine() ?? "";

            Console.Write("Nachname: ");
            string nachname = Console.ReadLine() ?? "";

            Console.Write("Telefon: ");
            string telefon = Console.ReadLine() ?? "";

            Console.Write("E-Mail: ");
            string email = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(vorname) ||
                string.IsNullOrWhiteSpace(nachname))
            {
                Console.WriteLine("Vorname und Nachname sind erforderlich!");
                Console.ReadKey();
                return;
            }

            int neueID = 1;

            if (kursleiterListe.Count > 0)
            {
                neueID = kursleiterListe.Max(k => k.KursleiterID) + 1;
            }

            Kursleiter neuerKursleiter = new Kursleiter(
                neueID, vorname, nachname, telefon, email);

            datenZugriff.KursleiterSpeichern(neuerKursleiter);
            kursleiterListe.Add(neuerKursleiter);

            Console.WriteLine();
            Console.WriteLine("Kursleiter wurde erfolgreich angelegt!");
            Console.WriteLine("Kursleiter-ID: " + neueID);

            Console.WriteLine();
            Console.WriteLine("Drücke eine Taste zum Zurückkehren.");
            Console.ReadKey();
        }

        public void AlleKursleiterAnzeigen()
        {
            Console.Clear();

            kursleiterListe = datenZugriff.KursleiterLaden();

            Console.WriteLine("==============================================");
            Console.WriteLine("             ALLE KURSLEITER");
            Console.WriteLine("==============================================");

            if (kursleiterListe.Count == 0)
            {
                Console.WriteLine("Es sind noch keine Kursleiter vorhanden.");
            }
            else
            {
                Console.WriteLine(
                    "{0,-5} {1,-18} {2,-18} {3,-18} {4,-25}",
                    "ID", "Vorname", "Nachname", "Telefon", "E-Mail");

                Console.WriteLine(new string('-', 90));

                foreach (Kursleiter kursleiter in kursleiterListe)
                {
                    Console.WriteLine(
                        "{0,-5} {1,-18} {2,-18} {3,-18} {4,-25}",
                        kursleiter.KursleiterID,
                        kursleiter.Vorname,
                        kursleiter.Nachname,
                        kursleiter.Telefon,
                        kursleiter.Email);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Drücke eine Taste zum Zurückkehren.");
            Console.ReadKey();
        }

        public void KursleiterMenue()
        {
            bool menueLaeuft = true;

            while (menueLaeuft)
            {
                Console.Clear();

                Console.WriteLine("==================================");
                Console.WriteLine("       KURSLEITERVERWALTUNG");
                Console.WriteLine("==================================");
                Console.WriteLine();
                Console.WriteLine("1. Kursleiter anlegen");
                Console.WriteLine("2. Alle Kursleiter anzeigen");
                Console.WriteLine("3. Kursleiter einem Kurs zuordnen");
                Console.WriteLine("4. Alle Kursleiter-Zuordnungen anzeigen");
                Console.WriteLine("0. Zurück zum Hauptmenü");
                Console.WriteLine();

                Console.Write("Bitte wählen Sie eine Option: ");

                string? auswahl = Console.ReadLine();

                switch (auswahl)
                {
                    case "1":
                        KursleiterAnlegen();
                        break;

                    case "2":
                        AlleKursleiterAnzeigen();
                        break;

                    case "3":
                        KursleiterZuordnen();
                        break;

                    case "4":
                        AlleZuordnungenAnzeigen();
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
