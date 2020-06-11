using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rent_CLib
{
    /// <summary>
    /// This class is incharge of the actual phisical cars
    /// </summary>
    [DataContract]
    public class CarsForRent
    {
        private readonly Car_RentalsEntities4 rentalCardb = new Car_RentalsEntities4();
        private readonly Cars_for_Rent cars = new Cars_for_Rent();
        #region Properties
        [DataMember]
        public int CarUsage { get; set; }
        [DataMember]
        public bool IsUsable { get; set; }
        [DataMember]
        public bool IsAvalible { get; set; }
        [DataMember]
        public int LicenseNumber { get; set; }
       [DataMember]
        public int Lot { get; set; }
        [DataMember]
        public int CarType{ get; set; }
       [DataMember]
        public Car CarModel { get; set; }
      [DataMember]
        public byte[] Image { get; set; }
        string fileName;

        #endregion

        #region Methods
        /// <summary>
        /// Creates new car
        /// </summary>
        /// <param name="carUsage"></param>
        /// <param name="isUsable"></param>
        /// <param name="isAvalible"></param>
        /// <param name="license"></param>
        /// <param name="lotId"></param>
        /// <param name="carId"></param>
        /// <returns></returns>
        public bool CreateCarsForRent(int carUsage,
                                      bool isUsable,
                                      bool isAvalible,
                                      int license,
                                      int lotId,
                                      int carId)
        {
            bool isCreated = false;
            try
            {
                CarUsage += carUsage;
                cars.Distance_Usage = CarUsage;
                IsAvalible = isAvalible;
                cars.IsAvalible = IsAvalible;
                IsUsable = isUsable;
                cars.IsUsable = IsUsable;
                if (license >= 10000000 && license <= 99999999)
                {
                    LicenseNumber = license;
                    cars.License = LicenseNumber;
                }
                cars.Car_Type_Id=carId;
                CarType = carId;
                Lot = lotId;
                cars.Branch_Id = lotId;
                string dir ="C:\\Users\\Tzvi.Goldstein\\source\\repos\\Car Rental WebA\\CarRentApi\\App_Data";
                foreach (var file in Directory.GetFiles(dir))
                {
                    fileName = file;
                    continue;
                }
                Image = File.ReadAllBytes(fileName);
                cars.CarImage = Image;
                rentalCardb.Cars_for_Rents.Add(cars);
                rentalCardb.SaveChanges();
                isCreated = true;
                string[] files = Directory.GetFiles(dir);
                foreach (var file in Directory.GetFiles(dir))
                {
                    File.Delete(file);
                }
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
            }
            return isCreated;
        }
        /// <summary>
        /// Set the car to be avalible (when a rent is finished)
        /// </summary>
        /// <param name="license"></param>
        public void SetCarAvalibility(int license)
        {
            Cars_for_Rent for_Rent = rentalCardb.Cars_for_Rents.FirstOrDefault(avalibleCar => avalibleCar.License == license);
            if (for_Rent != null)
            {
                IsAvalible = !for_Rent.IsAvalible;
                for_Rent.IsAvalible = IsAvalible;
                rentalCardb.SaveChanges();
            }
        }
        /// <summary>
        /// Get a list of all cars from database
        /// </summary>
        /// <returns></returns>
        public List<CarsForRent> RetriveAllCarsInStock()
        {
            List<CarsForRent> listOfCars = new List<CarsForRent>();
            try
           {
                var allCarsInvantory = rentalCardb.Cars_for_Rents;
                Car car = new Car();
                foreach (var carFromDb in allCarsInvantory)
                {
                    listOfCars.Add(new CarsForRent {
                        IsAvalible = carFromDb.IsAvalible,
                        IsUsable = carFromDb.IsUsable,
                        Lot = carFromDb.Branch_Id,
                        CarType = carFromDb.Car_Type_Id,
                        LicenseNumber = carFromDb.License,
                        CarUsage=carFromDb.Distance_Usage,
                        Image=carFromDb.CarImage,
                        CarModel =car.GetCarDetails(carFromDb.Car_Type_Id)
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
        /// Gets car by id from database
        /// </summary>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public List<CarsForRent> GetCarsByCarType(int typeId)
        {
            List<CarsForRent> listOfCars = RetriveAllCarsInStock();
            List<CarsForRent> carsByCarTypes = new List<CarsForRent>();
            foreach (var car in listOfCars)
            {
                if (car.CarType==typeId)
                {
                    carsByCarTypes.Add(new CarsForRent
                    {
                        CarUsage = car.CarUsage,
                        IsUsable = car.IsUsable,
                        IsAvalible = car.IsAvalible,
                        LicenseNumber = car.LicenseNumber,
                        Lot = car.Lot,
                        CarType = car.CarType,
                        Image=car.Image
                    });
                }
            }
            if (carsByCarTypes == null)
            {
                return null;
            }
            else
            {
                return carsByCarTypes;
            }
        }

        /// <summary>
        /// Gets all cartype info that is associated with the car by license
        /// </summary>
        /// <param name="license"></param>
        /// <returns></returns>
        public CarsForRent GetCarInfo(int license)
        {
            Cars_for_Rent for_Rent = rentalCardb.Cars_for_Rents.FirstOrDefault(avalibleCar => avalibleCar.License == license);
            CarsForRent car = new CarsForRent();
            car.CarUsage = for_Rent.Distance_Usage;
            car.IsUsable = for_Rent.IsUsable;
            car.IsAvalible = for_Rent.IsAvalible;
            car.LicenseNumber = for_Rent.License;
            car.Lot = for_Rent.Branch_Id;
            car.CarType = for_Rent.Car_Type_Id;
            car.Image=for_Rent.CarImage;
            return car;
        }
        /// <summary>
        /// Deletes car from database by license
        /// </summary>
        /// <param name="license"></param>
        /// <returns></returns>
        public bool DeleteCar(int license)
        {
            bool isDeleted = false;
            try
            {
                Cars_for_Rent carDeleting = rentalCardb.Cars_for_Rents.FirstOrDefault(avalibleCar => avalibleCar.License == license);

                rentalCardb.Cars_for_Rents.Remove(carDeleting);
                rentalCardb.SaveChanges();
                isDeleted = true;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                isDeleted = false;
            }
            return isDeleted;
        }
       
        #endregion
    }
}
