using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoList.Models;
using System.IO;
using Newtonsoft.Json;

namespace ToDoList.Controllers
{
    public class CategoryController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Category> Get()
        {
            string fileName = "Category.json";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);
            string categoryJson = File.ReadAllText(filePath);
            var category= JsonConvert.DeserializeObject<List<Category>>(categoryJson);
            return category;
        }

        // GET api/<controller>/5
        public string Get(string categoryName)
        {
            string fileName = "Category.json";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);
            string categoryJson = File.ReadAllText(filePath);
            var category = JsonConvert.DeserializeObject<List<Category>>(categoryJson);
            var result = category.FirstOrDefault(x => x.Category_Name.Contains(categoryName.ToString()));
            string category_name = result.Category_Name;
            return category_name;
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}