namespace StatisticsWebApp.Models
{
    public class Statistic
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Number { get; set; }
        public string Label { get; set; }
        public FileModel File { get; set; }
    }
}
