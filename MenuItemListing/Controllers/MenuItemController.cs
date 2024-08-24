using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruYum.Domain.Entities;

namespace MenuItemListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {

        List<MenuItem> menuItems = new List<MenuItem>
        {
                new MenuItem
                {
                    Id = 1,
                    Name = "Pizza",
                    FreeDelivery = true,
                    Price = 9.99,
                    DateOfLaunch = new DateTime(2024, 8, 24),
                    Active = true
                },
                new MenuItem
                {
                    Id = 2,
                    Name = "Burger",
                    FreeDelivery = false,
                    Price = 5.49,
                    DateOfLaunch = new DateTime(2024, 8, 24),
                    Active = true
                },
                new MenuItem
                {
                    Id = 3,
                    Name = "Pasta",
                    FreeDelivery = true,
                    Price = 7.99,
                    DateOfLaunch = new DateTime(2024, 8, 24),
                    Active = false
                },
                new MenuItem
                {
                    Id = 4,
                    Name = "Biryani",
                    FreeDelivery = true,
                    Price = 10.99,
                    DateOfLaunch = new DateTime(2024, 8, 22),
                    Active = true
                }
        };

        //...api/menuitem
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GiveMeAllProductsMethodNamecanbeAnyting()
        {
            return Ok(menuItems);
        }

        //...api/menuitem/1
        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public IActionResult GetmenunameById(int id)
        {
            var item= menuItems.Find(menu=>menu.Id==id);
            if (item == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(item.Name);
            }
        }
    }
}
