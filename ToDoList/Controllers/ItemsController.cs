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
    public class ItemsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Items> Get()
        {
            //return new string[] { "value1", "value2" };
            //var result = new List<Items>();
            string filePath = @"C:\Users\thihaswe\source\repos\ToDoList\ToDoList\Data\Items.json"; //Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\")) + @"Data\Category.json";
            string itemJson = File.ReadAllText(filePath);
            var result = JsonConvert.DeserializeObject<List<Items>>(itemJson);
            return result;
        }

        // GET api/<controller>/5
        public string Get(string item)
        {
            return "value";
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