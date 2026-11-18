using System;
using System.Collections.Generic;
using System.Text;

namespace SchwimmbadConsole
{

    namespace SchwimmbadConsole
    {
        public class Kurs
        {
            public int KursID { get; set; }
            public string Kursname { get; set; }
            public string Wochentag { get; set; }
            public string Beginn { get; set; }
            public string Ende { get; set; }
            public int MaxTeilnehmer { get; set; }

            public Kurs(int kursID, string kursname, string wochentag,
                        string beginn, string ende, int maxTeilnehmer)
            {
                KursID = kursID;
                Kursname = kursname;
                Wochentag = wochentag;
                Beginn = beginn;
                Ende = ende;
                MaxTeilnehmer = maxTeilnehmer;



            }









        }
    }

}
