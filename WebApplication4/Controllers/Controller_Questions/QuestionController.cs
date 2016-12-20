using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication4.Controllers.Controller_Questions
{
    public class QuestionController : ApiController
    {

        [HttpGet, Route(template: "getRasel/{a}")]
        public IHttpActionResult GetCustomerProductRating(int a)
        {
            try
            {
                
                return Ok(content: "rasel");
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

                JObject o1 = JObject.Parse(File.ReadAllText(@"D:\aaa\WebApplication4\WebApplication4\Models\datamanagementandanalysis-export.json"));

                // read JSON directly from a file
                //using (StreamReader file = File.OpenText(@"D:\aaa\WebApplication4\WebApplication4\Models\datamanagementandanalysis-export.json"))
                //using (JsonTextReader reader = new JsonTextReader(file))
                //{
                //    JObject o2 = (JObject)JToken.ReadFrom(reader);
                //}
                //hlw 

                JArray rssTitle = (JArray)o1["array"]["SubSubcategories"]; 
                JArray rssTitlee = (JArray)rssTitle;

                return Ok(content: rssTitlee.Skip(lastIndex).Take(nextIteams));
            }
            catch (Exception exception)
            {
                return InternalServerError(exception: exception);
            }
        }

    }
}
