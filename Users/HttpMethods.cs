using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Users
{
    public class HttpMethods
    {
        public static async Task GetAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.GetAsync("users/3");

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            var jSonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jSonResponse);
        }

        public static async Task GetFromList(HttpClient httpClient)
        {
            var users = await httpClient.GetFromJsonAsync<List<User>>(
               "users?Id=1&Lng=81.1496");

            users?.ForEach(Console.WriteLine);
        }

        public static async Task<string> PostAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                "users", new User(Id: 1, Name: "Leanne Graham", UserName: "Bret", Email: "Sincere@april.biz",
                    Address: new Address(Street: "Kulas Light", Suite: "Apt. 556", City: "Gwenborough", Zipcode: "92998-3874",
                        Geo: new Geo(Lat:  - 37.3159, Lng: 81.1496)
                        )
                    ));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jsonPost = await response.Content.ReadAsStringAsync();
            return jsonPost;
        }

        public static async Task<string> PutAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PutAsJsonAsync(
                "users/4", new User.User(Id: 1234 ));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonPut = await response.Content.ReadAsStringAsync();
            return jSonPut;
        }

        public static async Task<string> PatchAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PatchAsJsonAsync(
                "photos/3", new Photos(Title: "Patch O`zgartirildi"));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonPatch = await response.Content.ReadAsStringAsync();
            return jSonPatch;
        }

        public static async Task<string> DeleteAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.DeleteAsync("photos/3");

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonDelete = await response.Content.ReadAsStringAsync();
            return jSonDelete;
        }
    }
}
