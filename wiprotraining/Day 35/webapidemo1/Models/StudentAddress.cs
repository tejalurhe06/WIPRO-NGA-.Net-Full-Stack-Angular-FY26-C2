namespace webapidemo1.Models
{
    public class StudentAddress
    {
        public int StudentAddressId { get; set; }

        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }

        public int? PinCode { get; set; }   // ✅ made nullable
        public string? City { get; set; }   // ✅ made nullable
        public string? State { get; set; }  // ✅ made nullable
        public string? Country { get; set; } // ✅ made nullable

        public virtual Student? Student { get; set; } // ✅ optional relationship
    }
}
