using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rent_CLib
{
    /// <summary>
    /// This class in in charge of the cartypes or car models
    /// </summary>
    [DataContract]
  public class Car
    {
        Car_RentalsEntities4 carDb = new Car_RentalsEntities4();
        Car_Type car_Type = new Car_Type();
        #region Properties
        [DataMember]
        public string Manufacturer { get; set; }
        [DataMember]
        public string Model { get; set; }
        [DataMember]
        public int CostPerDay { get; set; }
        [DataMember]
        public int DelayCostPerDay { get; set; }
        [DataMember]
        public int YearManufactured { get; set; }
        [DataMember]
        public bool IsGear { get; set; }
        [DataMember]
        public int ModelId { get; set; }
        #endregion
        #region Methods        
        /// <summary>
        /// Creates new car type on database
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="model"></param>
        /// <param name="costPerDay"></param>
        /// <param name="delayPerDay"></param>
        /// <param name="yearManufacturer"></param>
        /// <param name="isGear"></param>
        /// <returns></returns>
        public bool CreatNewCar(string manufacturer, string model, int costPerDay, int delayPerDay, int yearManufacturer,bool isGear)
        {
            bool isCreated = false;
            try
            {

                Manufacturer = manufacturer;
                car_Type.Company_Name = Manufacturer;
                Model = model;
                car_Type.Model = Model;
                if (costPerDay > 0)
                {
                    CostPerDay = costPerDay;
                    car_Type.Cost_pre_Day = CostPerDay;
                }
                DelayCostPerDay = delayPerDay;
                car_Type.Delay_Cost_per_Day = DelayCostPerDay;
                if (yearManufacturer >= 1990 & yearManufacturer < DateTime.Now.Year)
                {
                    YearManufactured = yearManufacturer;
                    car_Type.Manufactured_Year = YearManufactured;
                }
                IsGear = isGear;
                car_Type.isGear = IsGear;
                carDb.Car_Types.Add(car_Type);
                carDb.SaveChanges();
                isCreated = true;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                isCreated = false;
            }
            return isCreated;
        }
        /// <summary>
        /// Gets all car type details by id
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public Car GetCarDetails(int carId)
        {
            Car spicificCar = new Car();
            Car_Type carType = carDb.Car_Types.FirstOrDefault(car => car.Model_Id == carId);
            spicificCar.CostPerDay = (int)carType.Cost_pre_Day;
            spicificCar.DelayCostPerDay = (int)carType.Delay_Cost_per_Day;
            spicificCar.IsGear = carType.isGear;
            spicificCar.Manufacturer = carType.Company_Name;
            spicificCar.Model = carType.Model;
            spicificCar.ModelId = carType.Model_Id;
            spicificCar.YearManufactured = carType.Manufactured_Year;
            return spicificCar;
        }

        /// <summary>
        /// Gets a list of all car models/ types
        /// </summary>
        /// <returns></returns>
        public List<Car> RetriveAllCars()
        {
            List<Car> listOfCars = new List<Car>();
            try
            {
                var allCars = carDb.Car_Types;
                foreach (var aCar in allCars)
                {
                    listOfCars.Add(new Car
                    {
                        Manufacturer = aCar.Company_Name,
                        Model = aCar.Model,
                        CostPerDay = (int)aCar.Cost_pre_Day,
                        DelayCostPerDay = (int)aCar.Delay_Cost_per_Day,
                        YearManufactured = aCar.Manufactured_Year,
                        IsGear = aCar.isGear,
                        ModelId = aCar.Model_Id
                    });
                }
                return listOfCars;
            }

            catch (Exception msg)
            {
                Console.WriteLine(msg);
                return null;
            }
        }

        /// <summary>
        /// Deleting car types but making sure to first erase all cars with this type
        /// </summary>
        /// <param name="modelId"></param>
        /// <returns></returns>
            public bool DeleteCarType(int modelId)
        {
            CarsForRent rents = new CarsForRent();
            List<CarsForRent> listOfCarsbyModel= new List<CarsForRent>();
            listOfCarsbyModel = rents.GetCarsByCarType(modelId);
            if (listOfCarsbyModel != null)
            {
                foreach (var deletingCar in listOfCarsbyModel)
                {
                    deletingCar.DeleteCar(deletingCar.LicenseNumber);
                }
            }
            bool isDeleted = false; 
            try
            {

                Car_Type carType = carDb.Car_Types.FirstOrDefault(spicifCar => spicifCar.Model_Id == modelId);

                carDb.Car_Types.Remove(carType);
                carDb.SaveChanges();
                isDeleted = true;
            }
            catch (Exception)
            {
                isDeleted = false;
                throw;
            }
            return isDeleted;
        }
        #endregion
    }
}
