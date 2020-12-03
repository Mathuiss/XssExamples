using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using XssExamples.Models;

namespace XssExamples.Pages
{
    public class StoredModel : PageModel
    {
        private readonly string _dbPath;

        public List<PostModel> Posts { get; set; }

        public StoredModel(IWebHostEnvironment env)
        {
            _dbPath = Path.Combine(env.ContentRootPath, "ApplicationData", "db.json");

            using (var reader = new StreamReader(_dbPath))
            {
                Posts = JsonConvert.DeserializeObject<List<PostModel>>(reader.ReadToEnd());
            }
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            string formName = Request.Form["FormName"];
            string formContent = Request.Form["FormContent"];

            var post = new PostModel()
            {
                PostName = formName,
                PostContent = formContent
            };

            Posts.Add(post);

            using (var writer = new StreamWriter(_dbPath))
            {
                writer.Write(JsonConvert.SerializeObject(Posts, new JsonSerializerSettings() { Formatting = Formatting.Indented }));
            }
        }
    }
}
