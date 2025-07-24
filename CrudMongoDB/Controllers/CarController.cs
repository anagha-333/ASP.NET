using CrudMongoDB.models;
using CrudMongoDB.service;
using Microsoft.AspNetCore.Mvc;


namespace CrudMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

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

        [HttpPost]
        public async Task<ActionResult<Car>> Create(Car car)
        {
            car.Id = null; // ✅ Ensure MongoDB generates a new unique _id
            await _carService.CreateAsync(car);
            return CreatedAtRoute("GetCar", new { id = car.Id }, car);
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Car carIn)
        {
            var car = await _carService.GetAsync(id);
            if (car == null) return NotFound();

            carIn.Id = id;
            await _carService.UpdateAsync(id, carIn);
            return NoContent();
        }

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
