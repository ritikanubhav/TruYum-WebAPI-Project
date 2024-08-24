using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using Newtonsoft.Json; // Add Newtonsoft.Json for handling extra quotes
using TruYum.Domain.Entities; // Assuming Cart is defined here

namespace OrderItem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly HttpClient client;

        public OrderController(HttpClient httpClient)
        {
            client = httpClient;
            client.BaseAddress = new Uri("https://localhost:44306"); // Base address of the MenuListing API
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        [HttpPost("{menuId}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Cart), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(int menuId)
        {
            try
            {
                // Make a GET request to the external API to get the menu name
                var response = await client.GetStringAsync($"/api/MenuItem/{menuId}");

                // Deserialize the response into an object
                var menuName = JsonConvert.DeserializeObject<string>(response);

                if (string.IsNullOrEmpty(menuName))
                {
                    return BadRequest("Unable to retrieve menu name.");
                }

                // Create the cart object with the retrieved menu name
                var cart = new Cart
                {
                    Id = 1, 
                    MenuItemId = menuId,
                    UserId = 1, 
                    MenuItemName = menuName
                };

                // Return 201 Created response with the cart object
                return CreatedAtAction(nameof(Add), new { id = cart.Id }, cart);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }
    }
}
