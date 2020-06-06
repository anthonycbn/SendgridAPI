using System.ComponentModel.DataAnnotations;


namespace SendgridEmaiWebAPI.Model
{
    public class EmailModel
    {
        [Required]
        [EmailAddress]
        public string To { get; set; }
        [EmailAddress]
        public string Cc { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
