using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpPost
{
    public class Program
    {
        private static HttpClient sheredClined = new()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
        };

        static void Main(string[] args)
        {
            HttpMethods.GetAsync(sheredClined).Wait();
            string resultPost = HttpMethods.PostAsJsonAsync(sheredClined).Result;
            Console.WriteLine(resultPost);

            string resultPut = HttpMethods.PutAsJsonAsync(sheredClined).Result;
            Console.WriteLine(resultPut);

            string resultPatch = HttpMethods.PatchAsJsonAsync(sheredClined).Result;
            Console.WriteLine(resultPatch);

            string resultDelete = HttpMethods.DeleteAsync(sheredClined).Result;
            Console.WriteLine(resultDelete);

        }
    }
}
