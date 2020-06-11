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
using System.Web.UI.WebControls;

namespace CarRentApi.Controllers
{
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class CarInventoryController : ApiController
    {
        private readonly CarsForRent myCar = new CarsForRent();
        private readonly Car carDetails = new Car();
        // GET: api/CarInventory
        [HttpGet]
        public IEnumerable<CarsForRent> Get()
        {
            try
            {
                var carResult = myCar.RetriveAllCarsInStock();
                return carResult;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                throw;
            }

        }

        // GET: api/CarInventory/5
        [HttpGet]
        public CarsForRent Get(int id)
        {
            try
            {
                GetCarDetails(myCar.CarType);
                return myCar.GetCarInfo(id);
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                throw;
            }

        }

        // GET: api/CarInventory/5
        [HttpGet]
        public Car GetCarDetails(int id)
        {
            try
            {
                return carDetails.GetCarDetails(id);
            }
            catch (Exception msg)
            {

                Console.WriteLine(msg);
                throw;
            }
        }
        /// <summary>
        /// We get the image from the client and put store it temporarily in a folder before posting it in the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/CarInventory/PostFormData")]
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
        [HttpPost]
        public IHttpActionResult Post([FromBody]CarsForRent rentalCar)
        {
            bool managedToPost = false;

            managedToPost = myCar.CreateCarsForRent(rentalCar.CarUsage, rentalCar.IsUsable, rentalCar.IsAvalible, rentalCar.LicenseNumber, rentalCar.Lot, rentalCar.CarType);
            if (managedToPost)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/CarInventory/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]CarsForRent rental)
        {
            try
            {
                using (Car_RentalsEntities4 rentalCardb = new Car_RentalsEntities4())
                {
                    var UpdatedCar = rentalCardb.Cars_for_Rents.FirstOrDefault(c => c.License == id);
                    if (UpdatedCar == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        UpdatedCar.IsAvalible = rental.IsAvalible;
                        UpdatedCar.IsUsable = rental.IsUsable;
                        UpdatedCar.Branch_Id = rental.Lot;
                        UpdatedCar.Car_Type_Id = rental.CarType;
                        UpdatedCar.Distance_Usage = rental.CarUsage;
                        UpdatedCar.License = rental.LicenseNumber;
                        UpdatedCar.CarImage = rental.Image;
                        rentalCardb.SaveChanges();
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

        // DELETE: api/CarInventory/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            bool isSuccessfullyDeleted = false;
            isSuccessfullyDeleted = myCar.DeleteCar(id);
            if (isSuccessfullyDeleted)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
