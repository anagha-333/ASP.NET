using Microsoft.AspNetCore.Mvc;
using WebApi.model;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "iphone", Price = 199999 },
        new Product { Id = 2, Name = "samsung", Price = 15599.49m }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        return Ok(Products);
    }

    [HttpPost]
    public ActionResult<Product> AddProduct([FromBody] Product product)
    {
        product.Id = Products.Max(p => p.Id) + 1;
        Products.Add(product);
        return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
    }
}
