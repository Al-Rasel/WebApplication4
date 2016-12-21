using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace WebApplication4.Controllers.Controller_Questions
{
    public class ResetPsswordController : ApiController
    {


        [HttpPost, Route(template: "reset_password")]
        public IHttpActionResult signIn([FromBody]String Email)
        {

            string password = "";
            try
            {
                JArray arrayOfUSer = JArray.Parse(File.ReadAllText(@"F:\ApiProjects\WebApplication4\WebApplication4\Models\AuthData\UserAuthData.json"));
             
                string quarry = "[?(@.Email == " + "'" + Email + "'" + ")]";
                
                JToken singleUser = arrayOfUSer.SelectToken(quarry);
                if (singleUser != null)
                {
                    password = singleUser.Value<string>("Password");


             



                }
                else {
                    password = "This Email Is Not Reg. to Any Acconunt";
                }

              


                return Ok(content: password);
            }
            catch (Exception exception)
            {
                return InternalServerError(exception: exception);
            }
        }
    }
}
