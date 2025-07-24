using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Revv.Cars.Domain;
using Revv.Cars.Shared.Commands;
using Revv.Cars.Shared.Queries;


namespace Revv.Cars.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Revv.Cars.Shared.Car>>> GetAsync()
        {
            var result = await _mediator.Send(new GetAllCarQueryRequest());
            return Ok(result.cars);
        }

        [HttpGet("{id:length(24)}", Name = "GetCar")]
        public async Task<ActionResult<Revv.Cars.Shared.Car>> Get(string id)
        {
            var result = await _mediator.Send(new GetCarByIdQueryRequest(id));
            if (result.Car == null)
                return NotFound();

            return Ok(result.Car);
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CarUpload carUpload)
        {
            try
            {
                if (carUpload == null)
                    return BadRequest("Form data is missing or malformed.");

                if (carUpload.ImageFile == null || carUpload.ImageFile.Length == 0)
                    return BadRequest("Image file is required.");

                var fileExtension = Path.GetExtension(carUpload.ImageFile.FileName);
                if (string.IsNullOrEmpty(fileExtension))
                    return BadRequest("Image file extension is missing.");

                var imageFolder = Path.Combine("wwwroot", "images");
                if (!Directory.Exists(imageFolder))
                    Directory.CreateDirectory(imageFolder);

                var fileName = Guid.NewGuid() + fileExtension;
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
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An internal error occurred.",
                    error = ex.Message,
                    stack = ex.StackTrace // optional: remove this in production
                });
            }
        }



        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> UpdateCar(int number, [FromBody] Revv.Cars.Shared.Car updatedCar)
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
