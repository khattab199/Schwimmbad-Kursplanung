
using System;

namespace SchwimmbadConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            KursVerwaltung kursVerwaltung = new KursVerwaltung();
            TeilnehmerVerwaltung teilnehmerVerwaltung = new TeilnehmerVerwaltung();
            KursleiterVerwaltung kursleiterVerwaltung = new KursleiterVerwaltung();

            bool programmLaeuft = true;

            while (programmLaeuft)
            {
                Console.Clear();

                Console.WriteLine("==================================");
                Console.WriteLine("     SCHWIMMBAD-KURSPLANUNG");
                Console.WriteLine("==================================");
                Console.WriteLine();
                Console.WriteLine("1. Kursverwaltung");
                Console.WriteLine("2. Teilnehmerverwaltung");
                Console.WriteLine("3. Kursleiterverwaltung");
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
                        teilnehmerVerwaltung.Teilnehmermenue();
                        break;

                    case "3":
                        kursleiterVerwaltung.KursleiterMenue();
                        break;

                    case "0":
                        //hier würde das programm beendet
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
