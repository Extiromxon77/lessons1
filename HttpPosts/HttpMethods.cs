
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HttpPost
{
    public class HttpMethods
    {
        public static async Task GetAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.GetAsync("posts/3");

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            var jSonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jSonResponse);
        }

        public static async Task GetFromList(HttpClient httpClient)
        {
            var posts = await httpClient.GetFromJsonAsync<List<Posts>>(
               "posts?userId=1&body=\"\"");

            posts?.ForEach(Console.WriteLine);
        }

        public static async Task<string> PostAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                "posts", new Posts(UserId: 9, Id: 99, Title: "Nimadur", Body: "dhhfhfg"));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jsonPost = await response.Content.ReadAsStringAsync();
            return jsonPost;
        }

        public static async Task<string> PutAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PutAsJsonAsync(
                "posts/4", new Posts(Title: "Put O`zgartirildi", Body: "dhhfhfg"));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonPut = await response.Content.ReadAsStringAsync();
            return jSonPut;
        }

        public static async Task<string> PatchAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PatchAsJsonAsync(
                "posts/2", new Posts(Title: "Patch O`zgartirildi", Body: "127"));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonPatch = await response.Content.ReadAsStringAsync();
            return jSonPatch;
        }

        public static async Task<string> DeleteAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.DeleteAsync("posts/3");

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonDelete = await response.Content.ReadAsStringAsync();
            return jSonDelete;
        }
    }
}
