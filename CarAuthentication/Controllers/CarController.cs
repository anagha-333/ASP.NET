using CarAuthentication.Commands;
using CarAuthentication.models;
using CarAuthentication.Queries;
using CarAuthentication.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CarAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public CarController( IMediator mediator)
        {
            
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetAsync()
        {
            var result = await _mediator.Send(new GetAllCarQueryRequest());
            return Ok(result.cars);
        }

        [HttpGet("{id:length(24)}", Name = "GetCar")]
        public async Task<ActionResult<Car>> Get(string id)
        {
            var result = await _mediator.Send(new GetCarByIdQueryRequest(id));
            if (result.Car == null)
                return NotFound();

            return Ok(result.Car);
        }

      

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

            if (!int.TryParse(carUpload.Year, out var year))
                return BadRequest("Invalid year format.");

            if (!int.TryParse(carUpload.Number, out var number))
                return BadRequest("Invalid number format.");

            var command = new CreateCarCommandRequest
            {
                Image = fileName,
                Brand = carUpload.Brand,
                Model = carUpload.Model,
                Year = year,
                Place = carUpload.Place,
                Number = number,
                Date = carUpload.Date
            };

            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetCar", new { id = result.Car.Id }, result.Car);
        }

        [Authorize]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateCar(int number, [FromBody] Car updatedCar)
        {
            var command = new UpdateCarCommandRequest
            {
                Number = number,
                UpdatedCar = updatedCar
            };

            var result = await _mediator.Send(command);

            if (!result.IsUpdated)
                return NotFound(new { message = result.Message });

            return Ok(result.Car);
        }

        [Authorize]
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _mediator.Send(new DeleteCarCommandRequest(id));
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Car not found" });
            }
        }
    }
}
