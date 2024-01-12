using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Photos
{
    public class HttpMethods
    {
        public static async Task GetAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.GetAsync("photos/3");

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            var jSonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jSonResponse);
        }

        public static async Task GetFromList(HttpClient httpClient)
        {
            var photos = await httpClient.GetFromJsonAsync<List<Photos>>(
               "albums?userId=1&ThumbnailUrl:\"\"");

            photos?.ForEach(Console.WriteLine);
        }

        public static async Task<string> PostAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                "photos", new Photos(AlbumId: 9, Id: 99, Title: "Nimadur", Url: "@http\\ncjd", ThumbnailUrl:"Apifgdhbcbv"));

            response.EnsureSuccessStatusCode()
                .WriteRequestToConsole();

            string jsonPost = await response.Content.ReadAsStringAsync();
            return jsonPost;
        }

        public static async Task<string> PutAsJsonAsync(HttpClient httpClient)
        {
            using HttpResponseMessage response = await httpClient.PutAsJsonAsync(
                "photos/3", new Photos(Title: "Put O`zgartirildi"));

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
