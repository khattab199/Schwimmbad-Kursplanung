
using System;

namespace SchwimmbadConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            KursVerwaltung kursVerwaltung = new KursVerwaltung();

            bool programmLaeuft = true;

            while (programmLaeuft)
            {
                Console.Clear();

                Console.WriteLine("==================================");
                Console.WriteLine("     SCHWIMMBAD-KURSPLANUNG");
                Console.WriteLine("==================================");
                Console.WriteLine();
                Console.WriteLine("1. Kursverwaltung");
                Console.WriteLine("2. Teilnehmer anmelden");
                Console.WriteLine("3. Teilnehmer anzeigen");
                Console.WriteLine("0. Programm beenden");
                Console.WriteLine();
                Console.Write("Bitte wählen Sie eine Option: ");

                string? auswahl = Console.ReadLine();

                switch (auswahl)
                {
                    case "1":
                        kursVerwaltung.Kursmenue();
                        break;

                    case "2":
                        Console.WriteLine("Teilnehmeranmeldung kommt als Nächstes.");
                   
                        break;

                    case "3":
                        Console.WriteLine("Teilnehmeranzeige kommt als Nächstes.");
                   
                        break;

                    case "0":
                        programmLaeuft = false;
                        Console.WriteLine("Programm wird beendet.");
                        break;

                    default:
                        Console.WriteLine("Ungültige Auswahl!");
                   
                        break;
                }
            }
        }
    }
}
