using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using RevvApplication.Models;
using RevvApplication.Service;

namespace RevvApplication.Controllers
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Car>> Create([FromBody] Car car)
        {
            car.Id = null;
            await _carService.CreateAsync(car);
            return CreatedAtRoute("GetCar", new { id = car.Id }, car);
        }

        [Authorize]
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] Car carIn)
        {
            var car = await _carService.GetAsync(id);
            if (car == null) return NotFound();

            carIn.Id = id;
            await _carService.UpdateAsync(id, carIn);
            return NoContent();
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
