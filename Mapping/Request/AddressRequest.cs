namespace Mapping.Request
{
    public class AddressRequest
    {
        public string AddressDetails { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
}
