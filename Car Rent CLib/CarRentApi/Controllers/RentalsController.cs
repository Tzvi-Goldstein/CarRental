using Car_Rent_CLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CarRentApi.Controllers
{
    [EnableCors(origins: "*", methods: "*", headers: "*")]
    public class RentalsController : ApiController
    {
        private readonly CarRentals car = new CarRentals();
        // GET: api/Rentals
        [HttpGet]
        public IEnumerable<CarRentals> GetAllRentals()
        {
            return car.RetriveAllRentals();
        }

        // GET: api/Rentals/5
        [HttpGet]
        public CarRentals GetOneRental(int id)
        {
            try
            {
               return car.GetRental(id);
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                throw;
            }
        }

        // POST: api/Rentals
        [HttpPost]
        public IHttpActionResult Post([FromBody]CarRentals rental)
        {
            bool rentalIsPosted = false;
            rentalIsPosted = car.newRent(rental.CarRented, rental.RentalStart, rental.RentalEnd, rental.User, rental.RentCost);
            if (rentalIsPosted)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Rentals/5
        public IHttpActionResult Put(int id, [FromBody]CarRentals rental)
        {
            try
            {
                using (Car_RentalsEntities4 carDb = new Car_RentalsEntities4())
                {
                    var updatedRental = carDb.Rentals.FirstOrDefault(r => r.Rental_Id == id);
                    if (updatedRental==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        updatedRental.Rental_Id = rental.RentalId;
                        updatedRental.RentCost = rental.RentCost;
                        updatedRental.Start_Rental_Date = rental.RentalStart;
                        updatedRental.User_Id = rental.User;
                        updatedRental.End_Rental_Date = rental.RentalEnd;
                        updatedRental.Car_Id = rental.CarRented;
                        updatedRental.Actual_Date_Returned = rental.ActualReturn;
                        carDb.SaveChanges();
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
        
        // DELETE: api/Rentals/5
        public IHttpActionResult Delete(int id)
        {
            bool isDeleted = car.DeleteRental(id);
            if (isDeleted)
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
