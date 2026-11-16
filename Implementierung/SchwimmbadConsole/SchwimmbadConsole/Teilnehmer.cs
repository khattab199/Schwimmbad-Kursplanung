using System;
using System.Collections.Generic;
using System.Text;

namespace SchwimmbadConsole
{

    namespace SchwimmbadConsole
    {
        public class Teilnehmer
        {
            public int TeilnehmerID { get; set; }
            public string Vorname { get; set; }
            public string Nachname { get; set; }
            public string Geburtsdatum { get; set; }
            public string Telefon { get; set; }
            public string Email { get; set; }
            public int KursID { get; set; }

            public Teilnehmer(int teilnehmerID, string vorname,
                              string nachname, string geburtsdatum,
                              string telefon, string email, int kursID)
            {
                TeilnehmerID = teilnehmerID;
                Vorname = vorname;
                Nachname = nachname;
                Geburtsdatum = geburtsdatum;
                Telefon = telefon;
                Email = email;
                KursID = kursID;
            }










        }
    }

}
