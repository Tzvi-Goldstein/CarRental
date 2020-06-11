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
    public class CarTypeController : ApiController
    {
         private readonly Car carDetails = new Car();
        // GET: api/CarType
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            try
            {
                return carDetails.RetriveAllCars();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: api/CarType/5
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

        // POST: api/CarType
        [HttpPost]
        public IHttpActionResult Post([FromBody]Car car)
        {
            bool carCreated= carDetails.CreatNewCar(car.Manufacturer, car.Model, car.CostPerDay, car.DelayCostPerDay, car.YearManufactured, car.IsGear);

            if (carCreated)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
         }

        // DELETE: api/CarType/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            bool isDeleted;
            isDeleted = carDetails.DeleteCarType(id);
            if (isDeleted)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/CarType/5
        public IHttpActionResult Put(int id, [FromBody]Car editCar)
        {
            try
            {
                using (Car_RentalsEntities4 carDb = new Car_RentalsEntities4())
                {
                    var updatedCarModel = carDb.Car_Types.FirstOrDefault(c => c.Model_Id == id);
                    if (updatedCarModel == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        updatedCarModel.Cost_pre_Day = editCar.CostPerDay;
                        updatedCarModel.Delay_Cost_per_Day = editCar.DelayCostPerDay;
                        updatedCarModel.isGear = editCar.IsGear;
                        updatedCarModel.Company_Name = editCar.Manufacturer;
                        updatedCarModel.Model = editCar.Model;
                        updatedCarModel.Manufactured_Year = editCar.YearManufactured;
                        carDb.SaveChanges();
                        return Ok();
                    }                    
                }                       
                                        
            }                           
            catch (Exception msg) {
                Console.WriteLine(msg);
                return BadRequest();
            }
        }
    }

}
