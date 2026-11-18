
namespace SchwimmbadConsole
{
    public class KursKursleiter
    {
        public int KursID { get; set; }
        public int KursleiterID { get; set; }

        public KursKursleiter(int kursID, int kursleiterID)
        {
            KursID = kursID;
            KursleiterID = kursleiterID;
        }
    }
}
