using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Revv.Cars.Domain
{
    public class CarUpload
    {
        public IFormFile ImageFile { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Place { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
    }



}
