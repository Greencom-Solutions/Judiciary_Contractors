namespace Latest_Staff_Portal.Models
{
    public class SignatureRequest
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Otp { get; set; }
        public string DocumentNo { get; set; }
        public string FileName { get; set; }
        public int TableId { get; set; }
    }
}