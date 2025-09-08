namespace IdentityApp.Models
{
    public class DeactivatedProfile
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string Reason { get; set; }
        public DateTime DeactivatedAt { get; set; }
    }
}