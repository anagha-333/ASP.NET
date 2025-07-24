using System.ComponentModel.DataAnnotations;

namespace CarAuthentication.models
{
    public class CarUpload
    {
        public IFormFile ImageFile { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;

        public string Year { get; set; } = default!;   // ✅ must be string
        public string Number { get; set; } = default!; // ✅ must be string

        public string Place { get; set; } = default!;
        public string Date { get; set; } = default!;
    }


}
