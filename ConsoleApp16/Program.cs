using System.Net.Http.Json;
using TruYum.Domain.Entities;
using static System.Net.WebRequestMethods;
namespace ConsoleApp16
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------Welcome to Tru Yum!------------------\n");

            // TASK 1  : listing menuItems by setting api call for menu

            // creating Httpclient instance for calling menu api
            HttpClient client = new HttpClient();

            // base uri of apis
            string menuApiBaseUrl = "https://localhost:44306";

            // adding baseAddress
            client.BaseAddress = new Uri($"{menuApiBaseUrl}"); 

            //adding default Request Haeder
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            try
            {
                // calling MenuApi and getting response
                var menuResponse = client.GetFromJsonAsync<List<MenuItem>>($"/api/MenuItem").Result;

                //displaying menu items
                Console.WriteLine("------------------------Menu------------------------\n");
                Console.WriteLine($"Id\tItem\t\t Price\t\tFree Delivery\n");
                foreach (var item in menuResponse)
                {
                    Console.WriteLine($"{item.Id}\t{item.Name}\t\t$ {item.Price}\t\t{(item.FreeDelivery ? "yes" : "no")}");
                }
                Console.WriteLine("\n-------------------Menu Ends------------------------\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //TASK 2: order the item and call orderApi

            while (true)
            {
                //Asking for menu choice and storing it 
                Console.Write("Enter the Menu Item Id to add to cart: ");
                int menuChoice = Convert.ToInt32(Console.ReadLine());

                // creating Httpclient instance for calling order api
                HttpClient client2 = new HttpClient();

                //base uri of order api
                string orderApiBaseUrl = "https://localhost:44334";

                // adding baseAddress
                client2.BaseAddress = new Uri($"{orderApiBaseUrl}");

                //adding default Request Haeder
                client2.DefaultRequestHeaders.Add("Accept", "application/json");

                try
                {
                    // creating orderData to pass (in our case it is just a parameter)
                    var orderData = menuChoice;

                    // calling OrderApi and getting response for cart
                    var orderResponse = client2.PostAsJsonAsync($"/api/Order/{menuChoice}", orderData).Result;

                    Console.WriteLine("-------------------------------------------------------------");
                    if (orderResponse.IsSuccessStatusCode)
                    {
                        //Read Response Body to get cart object
                        var cart = orderResponse.Content.ReadFromJsonAsync<Cart>().Result;

                        //display cart details and message
                        Console.WriteLine("Thank You for Ordering! Your order is added to the cart.");
                        Console.WriteLine("-------------------------------------------------------------");
                        Console.WriteLine("Cart Details:\n");
                        Console.WriteLine($"Id: {cart.Id}\nUser Id: {cart.UserId}\nItem Id: {cart.MenuItemId}\nItem: {cart.MenuItemName}\n");
                    }
                    else
                    {
                        // if status code not ok then Invalid choice
                        Console.WriteLine("Order unsuccessful!\nPlease Enter Valid MenuId\n");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
           
    }
}
