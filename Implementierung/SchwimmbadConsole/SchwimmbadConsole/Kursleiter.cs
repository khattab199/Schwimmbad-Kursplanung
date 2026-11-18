
namespace SchwimmbadConsole
{
    public class Kursleiter
    {
        public int KursleiterID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }

        public Kursleiter(int kursleiterID, string vorname,
                          string nachname, string telefon, string email)
        {
            KursleiterID = kursleiterID;
            Vorname = vorname;
            Nachname = nachname;
            Telefon = telefon;
            Email = email;
        }
    }
}
