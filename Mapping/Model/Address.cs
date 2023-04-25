using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Mapping.Model
{
    public class Address
    {
        [ForeignKey("Employee")]
        public int AddressId { get; set; }
        public string AddressDetails { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        //Navigation Properties
        public virtual Employee Employee { get; set; }
    }
}
