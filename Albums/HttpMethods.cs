using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Albums
{
    public class HttpMethods
    {
        public static async Task GetAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.GetAsync("albums/3");

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            var jSonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jSonResponse);
        }

        public static async Task GetFromList(HttpClient httpClient)
        {
            var albums = await httpClient.GetFromJsonAsync<List<Albums>>(
               "albums?userId=1&title=\"\"");

            albums?.ForEach(Console.WriteLine);
        }

        public static async Task<string> PostAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                "albums", new Albums(UserId: 9, Id: 99, Title: "Nimadur"));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jsonPost = await response.Content.ReadAsStringAsync();
            return jsonPost;
        }

        public static async Task<string> PutAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PutAsJsonAsync(
                "albums/3", new Albums(Title: "Put O`zgartirildi"));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonPut = await response.Content.ReadAsStringAsync();
            return jSonPut;
        }

        public static async Task<string> PatchAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PatchAsJsonAsync(
                "albums/3", new Albums(Title: "Patch O`zgartirildi"));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonPatch = await response.Content.ReadAsStringAsync();
            return jSonPatch;
        }

        public static async Task<string> DeleteAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.DeleteAsync("albums/3");

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jSonDelete = await response.Content.ReadAsStringAsync();
            return jSonDelete;
        }
    }
}
