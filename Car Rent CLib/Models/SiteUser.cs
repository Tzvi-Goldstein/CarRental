using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
namespace Car_Rent_CLib
{
   public enum UserLevel
    {
        Manager=1,
        Employee=2,
        SystemUser=3,
        Visitor=4
    }
    /// <summary>
    /// This class is incharge of all server side things that have to do with users
    /// </summary>
    [DataContract]
    public class SiteUser
    {
        Car_RentalsEntities4 db = new Car_RentalsEntities4();
        User user = new User();
        #region properties
        [DataMember]
        public int Tz { get; set; }
        [DataMember]
        public string Fullname { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public DateTime ?Birthdate { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public byte[] Image{ get; set; }
         [DataMember]
        public UserLevel Permissions { get; set; }
        private string fileName;
        #endregion

        #region Methods
        // Creates new user on database
        public bool NewSiteUser(int tz, string fullName, string siteName,string password, DateTime? birthDay, string gender, string email)
        {
            bool success = false;
            try
            {

                if (tz >= 100000000 && tz <= 999999999)
                {
                    Tz= tz;
                    user.TZ = Tz;
                }
                Fullname = fullName;
                user.Full_name = Fullname;
                UserName = siteName;
                user.User_Name = UserName;
                Password = password;
                user.Password = Password;
                Birthdate = birthDay;
                user.Birth_Date = birthDay;
                if (gender.ToLower() == "male" || gender.ToLower() == "female")
                {
                    Gender = gender;
                    user.Gender = Gender;
                }
                if (email.Contains("@"))
                {
                    Email = email;
                    user.Email = Email;
                }
                Permissions = UserLevel.Visitor;
                user.User_Level = 4;
                // Sends image to temp local folder and then adds it to database and erases the image
                string dir = "~\\App_Data";
                foreach (var file in Directory.GetFiles(dir))
                {
                    fileName = file;
                    continue;
                }
                Image = File.ReadAllBytes(fileName);
                user.UserImage= Image;
                db.Users.Add(user);
                db.SaveChanges();
                success = true;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                success = false;
            }
            return success;
        }

        /// <summary>
        /// Change the user level - meant for Manager
        /// </summary>
        public bool ChangeUserLevel(int userLevel, int userID)
        {
            User user = db.Users.FirstOrDefault(t => t.TZ == userID);
            bool success = false;
            try
            {
                if (userLevel >= 1 && userLevel <= 4 && user != null)
                {
                    user.User_Level = userLevel;
                    db.SaveChanges();
                }
                success = true;
            }
            catch (Exception)
            {

                success=false;
            }
            return success;
        }

        /// <summary>
        /// Gets user from database by id
        /// </summary>
        /// <param name="tz"></param>
        /// <returns></returns>
        public SiteUser GetUserInfo(int tz)
        {
            try
            {
                SiteUser siteUser = new SiteUser();
                User user = db.Users.FirstOrDefault(t => t.TZ == tz);
                siteUser.Tz = user.TZ;
                siteUser.Fullname = user.Full_name;
                siteUser.UserName = user.User_Name;
                siteUser.Password = user.Password;
                siteUser.Birthdate = user.Birth_Date;
                siteUser.Gender = user.Gender;
                siteUser.Email = user.Email;
                siteUser.Permissions = (UserLevel)user.User_Level;
                siteUser.Image = user.UserImage;
                return siteUser;
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                throw;
            }
        }

        /// <summary>
        /// Gets all users from database and returns them in a list
        /// </summary>
        /// <returns></returns>
        public List<SiteUser> RetriveAllUsers()
        {
            List<SiteUser> listOfUsers = new List<SiteUser>();
            try
            {
                var allUsers = db.Users;
                foreach (var user in allUsers)
                {
                    listOfUsers.Add(new SiteUser
                    {
                        Tz= user.TZ,
                        Fullname = user.Full_name,
                        UserName = user.User_Name,
                        Password = user.Password,
                        Birthdate = user.Birth_Date,
                        Gender = user.Gender,
                        Email = user.Email,
                        Permissions = (UserLevel)user.User_Level,
                        Image = user.UserImage
                    });
                }
                return listOfUsers;
            }
                
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                return null;
            }
     
        }

        /// <summary>
        /// Deletes user by id (Tz)
        /// </summary>
        /// <param name="tz"></param>
        /// <returns></returns>
        public bool DeleteUser(int tz)
        {
            CarRentals rentals = new CarRentals();
            List<CarRentals> carRentalsList = new List<CarRentals>();
            carRentalsList = rentals.GetRentalByUser(tz);
            if (carRentalsList!=null)
            {
                foreach (var rental in carRentalsList)
                {
                    rentals.DeleteRental(rental.RentalId);
                }
            }
            bool isDeleted = false;
            User user = db.Users.FirstOrDefault(t => t.TZ == tz);
            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
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
