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
    public class LotController : ApiController
    {
        private readonly CarLot lot = new CarLot();
        // GET: api/Lot
        [HttpGet]
        public IEnumerable<CarLot> GetAllLots()
        {
            try
            {
                return lot.RetriveAllCarLots();
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                throw;
            }

        }
        // GET: api/Lot/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Lot
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Lot/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Lot/5
        public void Delete(int id)
        {
        }
    }
}
