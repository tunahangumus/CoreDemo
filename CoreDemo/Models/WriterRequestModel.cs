namespace CoreDemo.Models
{
    public class WriterRequestModel
    {
        public string? WriterName { get; set; }
        public int WriterID { get; set; }
        public string? WriterMail { get; set; }
        public bool WriterStatus { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
