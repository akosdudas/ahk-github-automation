namespace Ahk.Review.Ui.Models
{
    public class FinalStudentGrade
    {
        public FinalStudentGrade(string neptun, string repo, string prUrl, Dictionary<string, double> points)
        {
            Neptun = neptun;
            Repo = repo;
            PrUrl = prUrl;
            Points = points;
        }

        public string Neptun { get; }
        public string Repo { get; }
        public string PrUrl { get; }
        public Dictionary<string, double> Points { get; }
    }
}
