using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace MyApp.Namespace
{
    public class ReflectedModel : PageModel
    {
        public string Query { get; set; }
        public string PictureUrl { get; set; }

        public ReflectedModel()
        {
            Query = "";
        }

        public async Task OnGet()
        {
            Query = Request.Query["query"];

            if (!string.IsNullOrEmpty(Query))
            {
                await Download();
            }
        }

        public async Task Download()
        {
            var client = new HttpClient();

            HttpResponseMessage responseMessage = await client.GetAsync($"https://api.unsplash.com/photos/random?query={Query}&client_id=Yv_82U8Vbp-hcda8RjmjTjqyuxeFS6U4tLYaXpKNstI");
            string jsonResponse = await responseMessage.Content.ReadAsStringAsync();


            // Unsplash api result
            var jobj = JObject.Parse(jsonResponse);
            PictureUrl = jobj.SelectToken("$.urls.regular").Value<string>();
        }
    }
}
