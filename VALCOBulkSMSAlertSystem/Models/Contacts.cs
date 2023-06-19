namespace VALCOBulkSMSAlertSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace VALCOBulkSMSAlertSystem.Models
    {
        public class Contacts
        {
            public int Id { get; set; }

            [Required]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.PhoneNumber)]
            public string Phone { get; set; }

        }
    }

}
