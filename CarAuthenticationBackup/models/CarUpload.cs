using System.ComponentModel.DataAnnotations;

namespace CarAuthentication.models
{
    public class CarUpload
    {
        [Required]
        public IFormFile? ImageFile { get; set; }

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string? Place { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public string? Date { get; set; }

       
    }


}
