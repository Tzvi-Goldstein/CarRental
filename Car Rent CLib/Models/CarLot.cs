using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rent_CLib
{
    /// <summary>
    /// Class that deals with the car lots
    /// </summary>
    [DataContract]
  public class CarLot
    {
        #region Properties
        Car_RentalsEntities4 db = new Car_RentalsEntities4();
        Branch branch = new Branch();
        [DataMember]
        public String LotName { get; set; }
        [DataMember]
        public string LotAddress { get; set; }
        [DataMember]
        public double latitude { get; set; }
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public int ID { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Get all car lots from database
        /// </summary>
        /// <returns></returns>
        public List<CarLot> RetriveAllCarLots()
        {
            List<CarLot> listOfLots = new List<CarLot>();
            try
            {
                var allLots = db.Branchs;
                foreach (var lot in allLots)
                {
                    listOfLots.Add(new CarLot
                    {
                        LotName = lot.Branch_name,
                        LotAddress = lot.Address,
                        ID = lot.Branch_Id,
                        latitude = (double)(lot.Latitude),
                        Longitude = (double)(lot.Longatude)
                       });
                }
                return listOfLots;
            }

            catch (Exception msg)
            {
                Console.WriteLine(msg);
                return null;
            }

        }
    }
    #endregion
}
