using CarAuthentication.models;
using CarAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Car>>> Get() =>
            await _carService.GetAsync();

        [HttpGet("{id:length(24)}", Name = "GetCar")]
        public async Task<ActionResult<Car>> Get(string id)
        {
            var car = await _carService.GetAsync(id);
            if (car == null) return NotFound();
            return car;
        }

        // ✅ FIXED: Ensure number is int in both route and method
        [Authorize]
        [HttpGet("bynumber/{number:int}")]
        public async Task<ActionResult<Car>> GetByNumber(int number)
        {
            var car = await _carService.GetByNumberAsync(number);
            if (car == null) return NotFound();
            return Ok(car);
        }

        /*[Authorize]
        [HttpPost]
        public async Task<ActionResult<Car>> Create([FromBody] Car car)
        {
            try
            {
                car.Id = null;
                var result = await _carService.CreateAsync(car);
                return CreatedAtRoute("GetCar", new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Create Car Error: " + ex.Message);
                return BadRequest(new { message = ex.Message });
            }
        }*/
        /* [HttpPost]
         [Authorize]
         [Consumes("multipart/form-data")]
         public async Task<IActionResult> Create([FromForm] CarUpload carUpload)
         {
             if (!ModelState.IsValid)
             {
                 var errors = ModelState
                     .Where(x => x.Value?.Errors.Count > 0)
                     .ToDictionary(
                         e => e.Key,
                         e => e.Value.Errors.Select(err => err.ErrorMessage).ToArray()
                     );

                 return BadRequest(new { message = "Validation failed", errors });
             }

             if (carUpload.ImageFile == null || carUpload.ImageFile.Length == 0)
                 return BadRequest("Image file is required.");


             var imageFolder = Path.Combine("wwwroot", "images");
             if (!Directory.Exists(imageFolder))
                 Directory.CreateDirectory(imageFolder);

             var fileName = Guid.NewGuid() + Path.GetExtension(carUpload.ImageFile.FileName);
             var filePath = Path.Combine(imageFolder, fileName);

             using (var stream = new FileStream(filePath, FileMode.Create))
             {
                 await carUpload.ImageFile.CopyToAsync(stream);
             }

             var car = new Car
             {
                 Id = null,

                 Image = fileName,
                 Brand = carUpload.Brand,
                 Model = carUpload.Model,
                 Year = carUpload.Year,
                 Place = carUpload.Place,
                 Number = carUpload.Number,
                 Date = carUpload.Date,
                 CreatedAt = DateTime.UtcNow,

                 UpdatedAt = DateTime.UtcNow,

             };

             var created = _carService.CreateAsync(car);
             return CreatedAtAction(nameof(Get), new { id = created.Id }, created);


         }*/
        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CarUpload carUpload)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(
                        e => e.Key,
                        e => e.Value.Errors.Select(err => err.ErrorMessage).ToArray()
                    );

                return BadRequest(new { message = "Validation failed", errors });
            }

            if (carUpload.ImageFile == null || carUpload.ImageFile.Length == 0)
                return BadRequest("Image file is required.");

            var imageFolder = Path.Combine("wwwroot", "images");
            if (!Directory.Exists(imageFolder))
                Directory.CreateDirectory(imageFolder);

            var fileName = Guid.NewGuid() + Path.GetExtension(carUpload.ImageFile.FileName);
            var filePath = Path.Combine(imageFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await carUpload.ImageFile.CopyToAsync(stream);
            }

            var car = new Car
            {
                Id = null,
                Image = fileName,
                Brand = carUpload.Brand,
                Model = carUpload.Model,
                Year = carUpload.Year,
                Place = carUpload.Place,
                Number = carUpload.Number,
                Date = carUpload.Date,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            // ✅ Await the async call
            var created = await _carService.CreateAsync(car);

            return CreatedAtRoute("GetCar", new { id = created.Id }, created);
        }



        [Authorize]
        [HttpPut("{number:int}")]
        public async Task<IActionResult> UpdateCar(int number, [FromBody] Car updatedCar)
        {
            var existingCar = await _carService.GetByNumberAsync(number);
            if (existingCar == null)
            {
                return NotFound(new { message = "Car not found" });
            }

            updatedCar.Id = existingCar.Id;
            updatedCar.UpdatedAt = DateTime.UtcNow;
            updatedCar.CreatedAt = existingCar.CreatedAt;
            updatedCar.Number = existingCar.Number;
            updatedCar.Image = existingCar.Image;

            await _carService.UpdateByNumberAsync(number, updatedCar);
            return Ok(updatedCar);
        }

        [Authorize]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var car = await _carService.GetAsync(id);
            if (car == null) return NotFound();

            await _carService.DeleteAsync(id);
            return NoContent();
        }
    }
}
