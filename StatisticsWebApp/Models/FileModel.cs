namespace StatisticsWebApp.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public int LinesCount { get; set; }
        public DateTime UploadDateTime { get; set; }
    }
}
