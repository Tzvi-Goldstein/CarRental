using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rent_CLib
{/// <summary>
/// This class is incharge of the actual rentals
/// </summary>
    [DataContract]
    public class CarRentals
    {
        #region Properties
        [DataMember]
        public int RentalId { get; set; }
        [DataMember]
        public DateTime RentalStart { get; set; }
        [DataMember]
        public DateTime RentalEnd { get; set; }
        [DataMember]
        public int CarRented { get; set; }
        [DataMember]
        public int User { get; set; }
        [DataMember]
        public DateTime? ActualReturn { get; set; }
        [DataMember]
        public int RentCost { get; set; }
        #endregion
      
        Car_RentalsEntities4 rentalCardb = new Car_RentalsEntities4();
        Rental rental= new Rental();
        CarsForRent car = new CarsForRent();
        
        #region Methods
        /// <summary>
        /// Create new rental on database
        /// </summary>
        /// <param name="carLicense"></param>
        /// <param name="rentalStart"></param>
        /// <param name="rentalEnd"></param>
        /// <param name="userID"></param>
        /// <param name="rentalCost"></param>
        /// <returns></returns>
        public bool newRent(int carLicense,DateTime rentalStart, DateTime rentalEnd, int userID, int rentalCost)
        {
            bool rentCreated = false; try
            {
                RentalStart = rentalStart;
                rental.Start_Rental_Date = rentalStart;
                if (rentalEnd > rentalStart)
                {
                    RentalEnd = rentalEnd;
                    rental.End_Rental_Date = rentalEnd;
                }
                CarRented = carLicense;
                rental.Car_Id = carLicense;
                User = userID;
                rental.User_Id = userID;
                RentCost = rentalCost;
                rental.RentCost = rentalCost;
                car.SetCarAvalibility(CarRented);
                rentalCardb.Rentals.Add(rental);
                rentalCardb.SaveChanges();
                rentCreated = true;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                rentCreated = false;
                throw;
            }
            return rentCreated;
        }
         
        /// <summary>
        /// Get rental by car license
        /// </summary>
        /// <param name="license"></param>
        /// <returns></returns>
        public CarRentals GetRental(int license)
        {
            CarRentals carRentals = new CarRentals();
            Rental getRental = rentalCardb.Rentals.FirstOrDefault(carLicencse => carLicencse.Cars_for_Rent.License == license);
            carRentals.RentalId = getRental.Rental_Id;
            carRentals.RentalStart = getRental.Start_Rental_Date;
            carRentals.RentalEnd = getRental.End_Rental_Date;
            carRentals.RentCost = (int)getRental.RentCost;
            carRentals.User = getRental.User.TZ;
            carRentals.ActualReturn = getRental.Actual_Date_Returned;
            carRentals.CarRented= getRental.Cars_for_Rent.License;
            return carRentals;
        }
        /// <summary>
        /// Get all rentals that have to do with spicific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CarRentals> GetRentalByUser(int userId)
        {
            List<CarRentals> listOfRentals = RetriveAllRentals();
            List<CarRentals> rentalsWithSpicificUser = new List<CarRentals>();
            foreach (var rental in listOfRentals)
            {
                if (rental.User == userId)
                {
                    rentalsWithSpicificUser.Add(new CarRentals {

                        RentalId = rental.RentalId,
                        RentalStart = rental.RentalStart,
                        RentalEnd = rental.RentalEnd,
                        RentCost = (int)rental.RentCost,
                        User = rental.User,
                        ActualReturn = rental.ActualReturn,
                        CarRented = rental.CarRented
                    });
                }
            }
            if (rentalsWithSpicificUser == null)
            {
                return null;
            }
            else
            {
                return rentalsWithSpicificUser;
            }
        }
        /// <summary>
        /// Gets list of all rentals
        /// </summary>
        /// <returns></returns>
        public List<CarRentals> RetriveAllRentals()
        {
            List<CarRentals> listOfRentals = new List<CarRentals>();
            try
            {
                var rentalsFromDb = rentalCardb.Rentals;
                foreach (var rental in rentalsFromDb)
                {
                    listOfRentals.Add(new CarRentals
                    {
                        RentalId = rental.Rental_Id,
                        RentalStart = rental.Start_Rental_Date,
                        RentalEnd = rental.End_Rental_Date,
                        RentCost = (int)rental.RentCost,
                        User= rental.User.TZ,
                        ActualReturn = rental.Actual_Date_Returned,
                        CarRented= rental.Cars_for_Rent.License
                    });
                }
                return listOfRentals;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                return null;
            }
            
        }
        /// <summary>
        /// Deletes rental from database
        /// </summary>
        /// <param name="rentalId"></param>
        /// <returns></returns>
        public bool DeleteRental(int rentalId)
        {
            Rental rental = rentalCardb.Rentals.FirstOrDefault(rent => rent.Rental_Id == rentalId);
            bool isDeleted = false;
            try
            {
                rentalCardb.Rentals.Remove(rental);
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
