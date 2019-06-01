using System.ComponentModel.DataAnnotations;            //Bu yapı yerine Fluent kullanılabilir.

namespace Abc.Entities
{
    public class ShippingDetails
    {
        [Required]                                      //Alanın girilmesi gereklidir...
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]               //Email address formatında olmasını istemek.Password,DateTime,...
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Range(18,100)]                                 //Min ve Max değerler arasında alabilir...
        public int Age { get; set; }
    }
}
