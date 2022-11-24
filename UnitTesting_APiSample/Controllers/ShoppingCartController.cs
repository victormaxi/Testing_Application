using Microsoft.AspNetCore.Mvc;
using UnitTesting_APiSample.Interface;
using UnitTesting_APiSample.Model;

namespace UnitTesting_APiSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {

        private readonly IShoppingCartService service;

        public ShoppingCartController(IShoppingCartService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var items = this.service.GetAllItems();
            return Ok(items);
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(Guid id)
        {
            var item = this.service.GetById(id);
            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        [Route("AddShoppingItem")]
        public IActionResult AddShoppingItem([FromBody] ShoppingItem value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = this.service.Add(value);
            return CreatedAtAction("Get", new {id = item.Id}, item);
        }

        [HttpDelete]
        [Route("removeshoppingitem/{id}")]
        public IActionResult RemoveShoppingItem(Guid id)
        {
            var existingItem = this.service.GetById(id);
            if (existingItem is null)
            {
                return NotFound();
            }
            this.service.Remove(id);
            return NoContent();
        }
    }
}
