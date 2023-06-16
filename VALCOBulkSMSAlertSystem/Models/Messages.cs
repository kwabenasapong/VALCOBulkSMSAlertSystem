namespace VALCOBulkSMSAlertSystem.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Sender { get; set; }
        public string? Recipient { get; set; }
        public string? Status { get; set; }
        public string? Date { get; set; } = System.DateTime.Now.ToString();

        //Foreign Key for the IdentityUser
        public string? AspnetUsers { get; set; }
    }
}
