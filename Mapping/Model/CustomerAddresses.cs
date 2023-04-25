using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mapping.Model
{
    public class CustomerAddresses
    {
        [Key]
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
