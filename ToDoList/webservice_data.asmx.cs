using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using Newtonsoft.Json;
using ToDoList.Models;
using Newtonsoft.Json.Linq;
using System.Web.Script.Services;

namespace ToDoList
{
    /// <summary>
    /// Summary description for webservice_data
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [ScriptService]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class webservice_data : System.Web.Services.WebService
    {


        /// <summary>
        /// Data Manipulation for Category
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        [WebMethod]
        public List<String> GetCategoriesByName(string categoryName)
        {
            List<Category> categories = new List<Category>();
            List<string> names = new List<string>();
            string filePath = GetCategoryFile();
            string categoryJson = File.ReadAllText(filePath);
            categories = JsonConvert.DeserializeObject<List<Category>>(categoryJson).ToList()
                  .Where(x => x.Category_Name.Contains(categoryName.ToString())).ToList();
            foreach (var item in categories)
            {
                names.Add(item.Category_Name);
            }
            
            return names;
        }

        [WebMethod]
        public List<String> GetCategoryByName(string categoryName)
        {
            List<Category> categories = new List<Category>();
            List<string> names = new List<string>();
            string filePath = GetCategoryFile();
            string categoryJson = File.ReadAllText(filePath);
            categories = JsonConvert.DeserializeObject<List<Category>>(categoryJson).ToList()
                  .Where(x => x.Category_Name.Equals(categoryName.ToString())).ToList();
            foreach (var item in categories)
            {
                names.Add(item.Category_Name);
            }

            return names;
        }

        [WebMethod]
        public List<String> GetItemsByCategoryName(string categoryName)
        {
            List<Items> categories = new List<Items>();
            List<string> items = new List<string>();
            string filePath = GetCategoryFile();
            string categoryJson = File.ReadAllText(filePath);
            categories = JsonConvert.DeserializeObject<List<Items>>(categoryJson).ToList()
                  .Where(x => x.CategoryName.Contains(categoryName.ToString())).ToList();
            foreach (var item in categories)
            {
                items.Add(item.CategoryName);
                items.Add(item.ItemName);
            }

            return items;
        }

        [WebMethod]
        public List<String> GetCategories()
        {
            List<Category> categories = new List<Category>();
            List<string> names = new List<string>();
            string filePath = GetCategoryFile();
            string categoryJson = File.ReadAllText(filePath);
            categories = JsonConvert.DeserializeObject<List<Category>>(categoryJson).ToList();
            foreach (var item in categories)
            {
                names.Add(item.Category_Name);
            }
            return names;
        }

        [WebMethod]
        public List<Items> GetToDoItems()
        {
            List<Items> items = new List<Items>();
            string filePath = GetItemsFile();
            string itemsJson = File.ReadAllText(filePath);
            items = JsonConvert.DeserializeObject<List<Items>>(itemsJson).ToList();
            return items;
        }

        [WebMethod]
        public void SaveCategory(string categoryName)
        {
            var newCategoryName = "{ 'category_name': '" + categoryName + "'}";
            try
            {
                string jsonFile = GetCategoryFile();
                var json = File.ReadAllText(jsonFile);
                JArray jsonArray = JArray.Parse(json);
                bool dataExists = false;
                if (categoryName.Length > 0)
                {
                    foreach (var category in jsonArray.Where(obj => obj["category_name"].Value<string>() == categoryName))
                    {
                        dataExists = !string.IsNullOrEmpty(categoryName) ? true : false;
                    }
                }
                if (!dataExists) {
                    var newCategory = JObject.Parse(newCategoryName);
                    jsonArray.Add(newCategory);
                    string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonArray,
                                           Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFile, newJsonResult);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Add Error : " + ex.Message.ToString());
            }
        }

        [WebMethod]
        public void UpdateCategory(string categoryName, string categoryNewName)
        {
            string jsonFile = GetCategoryFile();
            string json = File.ReadAllText(jsonFile);
            try
            {
                JArray categoryArrary = JArray.Parse(json);
                if (categoryName.Length > 0)
                {
                    foreach (var category in categoryArrary.Where(obj => obj["category_name"].Value<string>() == categoryName))
                    {
                        category["category_name"] = !string.IsNullOrEmpty(categoryName) ? categoryNewName : categoryName;
                    }
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(categoryArrary, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFile, output);
                }
                else
                {
                    Console.Write("Invalid Category Name, Try Again!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Error : " + ex.Message.ToString());
            }
        }
        [WebMethod]
        public void DeleteCategory(string categoryName)
        {
            string jsonFile = GetCategoryFile();
            var json = File.ReadAllText(jsonFile);
            try
            {
                JArray jsonArray = JArray.Parse(json);
                if (categoryName.Length > 0)
                {
                    JObject categoryToDeleted = (JObject)jsonArray.FirstOrDefault(obj => obj["category_name"].Value<string>() == categoryName);
                    jsonArray.Remove(categoryToDeleted);
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonArray,
                                        Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFile, output);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Data Manipulation for Items
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public String SaveItems(string categoryName, string itemName)
        {
            String result = string.Empty;
            var newItem = "{ 'CategoryName': '" + categoryName + "', 'ItemName' : '" + itemName +"'}";
            try
            {
                string jsonFile = GetItemsFile();
                var json = File.ReadAllText(jsonFile);
                JArray jsonArray = JArray.Parse(json);
                bool dataExists = false;
                if (categoryName.Length > 0)
                {
                    foreach (var category in jsonArray.Where(obj => obj["ItemName"].Value<string>() == itemName))
                    {
                        dataExists = !string.IsNullOrEmpty(itemName) ? true : false;
                    }
                }
                if (!dataExists)
                {
                    var newCategory = JObject.Parse(newItem);
                    jsonArray.Add(newCategory);
                    string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonArray,
                                           Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFile, newJsonResult);
                    result = itemName + " has created successfully!";
                }
                else {
                    result = itemName + " already exists!";
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Add Error : " + ex.Message.ToString());
            }
            return result;
        }

        [WebMethod]
        public void UpdateItem(string categoryName, string itemName, string itemNewName)
        {
            string jsonFile = GetItemsFile();
            string json = File.ReadAllText(jsonFile);
            try
            {
                JArray itemArrary = JArray.Parse(json);
                if (itemName.Length > 0)
                {
                    foreach (var item in itemArrary.Where(obj => obj["ItemName"].Value<string>() == itemName))
                    {
                        item["ItemName"] = !string.IsNullOrEmpty(itemName) ? itemNewName : itemName;
                    }
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(itemArrary, Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFile, output);
                }
                else
                {
                    Console.Write("Invalid Category Name, Try Again!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Update Error : " + ex.Message.ToString());
            }
        }

        [WebMethod]
        public void DeleteItem(string itemName)
        {
            string jsonFile = GetItemsFile();
            var json = File.ReadAllText(jsonFile);
            try
            {
                JArray jsonArray = JArray.Parse(json);
                if (itemName.Length > 0)
                {
                    JObject itemToDeleted = (JObject)jsonArray.FirstOrDefault(obj => obj["ItemName"].Value<string>() == itemName);
                    jsonArray.Remove(itemToDeleted);
                    string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonArray,
                                        Newtonsoft.Json.Formatting.Indented);
                    File.WriteAllText(jsonFile, output);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //JSSON File Paths
        public String GetCategoryFile()
        {
            string fileName = "Category.json";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);
            return path;
        }

        public String GetItemsFile()
        {
            string fileName = "Items.json";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);
            return path;
        }
    }
}
