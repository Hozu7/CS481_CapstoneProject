using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EventConnect;

public class Ticket
    {
        public int id { get; set; } = -1;

        public string priority { get; set; } = "Low";

        [Required]
        public string role { get; set; }

        [StringLength(150, ErrorMessage = "Description too long (150 character limit).")]
        public string? description { get; set; }

        public string status { get; set; } = "Unresolved";
    }