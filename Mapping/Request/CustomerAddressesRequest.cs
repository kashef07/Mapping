using System.ComponentModel.DataAnnotations.Schema;

namespace Mapping.Request
{
    public class CustomerAddressesRequest
    {
        public string? City { get; set; }
        public string? Country { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
    }
}
