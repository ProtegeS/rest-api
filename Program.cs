using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;


public class Products
{
    public string name { get; set; }
    public int categoryId { get; set; }
}
public class Categories
{
    public int id { get; set; }
    public string name { get; set; }
}

public class Collection
{
    public List<Products> products{ get; set; }
    public List<Categories> categories { get; set; }
}

namespace test_task
{
  
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.Get();

          
        }

         
        private async Task Get()
        {
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\tq - quit");
            Console.WriteLine("\tw - to send a request");
            Console.Write("\nYour option? ");

            switch (Console.ReadLine())
            {
                case "q":
                    Environment.Exit(0);
                    break;
                case "w":
                    while (true)
                     {
                        try
                        {
                            var response = await client.GetStringAsync("https://tester.consimple.pro/");

                            Console.WriteLine("\nResponse:\n");

                            JObject json = JObject.Parse(response);

                            Console.WriteLine(json);

                            // convert the JSON string to a serious of objects (deserialize)
                            Collection collection = JsonConvert.DeserializeObject<Collection>(response);

                            Console.WriteLine("\n********** Product name **********");
                            foreach (var product in collection.products)
                            {
                                Console.WriteLine(product.name);
                            }

                            Console.WriteLine("\n********** Category name **********");
                            foreach (var category in collection.categories)
                            {
                                Console.WriteLine(category.name);
                            }
                         

                            Console.WriteLine("\nGet Again? [Y or N]");

                            string answer = Console.ReadLine().ToUpper();

                            Console.WriteLine();
                          
                            if (answer == "Y")
                            {
                                continue;
                            }
                            else if (answer == "N")
                            {
                                return;
                            }
                            else
                            {
                                return;
                            }
                          
                        }

                        catch (HttpRequestException ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                        }
                      
                    }
            }

        }
       
    }
   

}
