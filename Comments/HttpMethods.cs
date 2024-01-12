using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Comments
{
    public class HttpMethods
    {
        public static async Task GetAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.GetAsync("comments/3");

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            var jSonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jSonResponse);
        }

        public static async Task GetFromList(HttpClient httpClient)
        {
            var posts = await httpClient.GetFromJsonAsync<List<Comments>>(
               "comments?PostId=1&body=\"\"");

            posts?.ForEach(Console.WriteLine);
        }

        public static async Task<string> PostAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                "comments", new Comments(PostId: 9, Id: 99, Name: "Nimadur", Email: "@email", Body: "dhhfhfg"));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jsonPost = await response.Content.ReadAsStringAsync();
            return jsonPost;
        }

        public static async Task<string> PutAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PutAsJsonAsync(
                "comments/3", new Comments(Name: "Put O`zgartirildi Samga", Body: "dhhfhfg"));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonPut = await response.Content.ReadAsStringAsync();
            return jSonPut;
        }

        public static async Task<string> PatchAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PatchAsJsonAsync(
                "comments/3", new Comments(Name: "Patch O`zgartirildi Tomga", Body: "127"));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonPatch = await response.Content.ReadAsStringAsync();
            return jSonPatch;
        }

        public static async Task<string> DeleteAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.DeleteAsync("comments/3");

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonDelete = await response.Content.ReadAsStringAsync();
            return jSonDelete;
        }
    }
}