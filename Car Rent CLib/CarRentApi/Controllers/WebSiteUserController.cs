using Car_Rent_CLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Car_Rental_WebA.Controllers
{
    [EnableCors(origins:"*",methods:"*",headers: "*")]
    public class WebSiteUserController : ApiController
    {
        private readonly SiteUser myUser = new SiteUser();
        
        // GET: api/WebSiteUser
        [HttpGet]
        public IEnumerable<SiteUser> Get()
        {
            return myUser.RetriveAllUsers();
        }

        [HttpGet]
        // GET: api/WebSiteUser/5
        public SiteUser GetSpcificUser(int id)
        {
            return myUser.GetUserInfo(id);
        }

        // POST: api/WebSiteUser
        [HttpPut]
        public IHttpActionResult PutPermissions(int userLevel, int userId)
        {
            bool success=false;
            if (myUser != null)
            {
               success= myUser.ChangeUserLevel(userLevel, userId);
            }
            if (success)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        // POST: api/WebSiteUser/5
        public IHttpActionResult Post([FromBody]SiteUser user)
        {
            bool IsSuccessfull;
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];

                    BinaryReader b = new BinaryReader(postedFile.InputStream);
                    user.Image = b.ReadBytes(file.Length); 
                }
            }
            IsSuccessfull = myUser.NewSiteUser(user.Tz, user.Fullname, user.UserName, user.Password, user.Birthdate, user.Gender, user.Email);

            if (IsSuccessfull)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE: api/WebSiteUser/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            bool IsSuccessfull = false;
           IsSuccessfull= myUser.DeleteUser(id);
            if (IsSuccessfull)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// We get the image from the client and put store it temporarily in a folder before posting it in the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/WebSiteUser/PostFormData")]
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            // Delete all files in direcotry
            string[] files = Directory.GetFiles(root);
            foreach (var file in files)
            {
                File.Delete(file);
            }
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);
                    Trace.WriteLine("Server file path: " + file.LocalFileName);
                }
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }


        // PUT: api/WebSiteUser/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]SiteUser user)
        {
            try
            {
                using (Car_RentalsEntities4 db = new Car_RentalsEntities4())
                {
                    var UpdatedUser = db.Users.FirstOrDefault(c => c.TZ == id);
                    if (UpdatedUser == null)
                    {
                        return NotFound();
                    }

                    else
                    {
                        UpdatedUser.User_Level= (int)user.Permissions;
                        UpdatedUser.User_Name= user.UserName;
                        UpdatedUser.Birth_Date= user.Birthdate;
                        UpdatedUser.Email = user.Email;
                        UpdatedUser.Gender = user.Gender;
                        UpdatedUser.Password = user.Password;
                        UpdatedUser.Full_name = user.Fullname;
                        UpdatedUser.UserImage= user.Image;
                        db.SaveChanges();
                        return Ok();
                    }
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                return BadRequest();
            }
        }
    }
}
