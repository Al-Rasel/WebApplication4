using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication4.Models;

namespace WebApplication4.Controllers.Controller_Questions
{
    public class QuestionController : ApiController
    {

        [HttpPost, Route(template: "getProfile")]
        public IHttpActionResult GetCustomerProductRating([FromBody]UnitProfile singleProfile)
        {
            try
            {

                var filePath = @"F:\ApiProjects\WebApplication4\WebApplication4\Models\person.json";
                // Read existing json data
                var jsonData = System.IO.File.ReadAllText(filePath);
                // De-serialize to object or create new list
                var employeeList = JsonConvert.DeserializeObject<List<UnitProfile>>(jsonData)
                                      ?? new List<UnitProfile>();

                // Add any new employees
                employeeList.Add(singleProfile);
               
                // Update json data string
                jsonData = JsonConvert.SerializeObject(employeeList);
                System.IO.File.WriteAllText(filePath, jsonData);
                JArray o1 = JArray.Parse(File.ReadAllText(@"F:\ApiProjects\WebApplication4\WebApplication4\Models\person.json"));


                return Ok(content:o1);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception: exception);
            }
        }
        [HttpGet, Route(template: "getRasell/{lastIndex}/{nextIteams}")]
        public IHttpActionResult GetCustomerProductRatingSecond(int lastIndex, int nextIteams)
        {
            try
            {

                JObject o1 = JObject.Parse(File.ReadAllText(@"F:\ApiProjects\WebApplication4\WebApplication4\Models\datamanagementandanalysis-export.json"));

                // read JSON directly from a file
                //using (StreamReader file = File.OpenText(@"D:\aaa\WebApplication4\WebApplication4\Models\datamanagementandanalysis-export.json"))
                //using (JsonTextReader reader = new JsonTextReader(file))
                //{
                //    JObject o2 = (JObject)JToken.ReadFrom(reader);
                //}
                //hlw 

                JArray rssTitle = (JArray)o1["array"]["SubSubcategories"]; 
                JArray rssTitlee = rssTitle;

                return Ok(content: rssTitlee.Skip(lastIndex).Take(nextIteams));
            }
            catch (Exception exception)
            {
                return InternalServerError(exception: exception);
            }
        }

    }
}
