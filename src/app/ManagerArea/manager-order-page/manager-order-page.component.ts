import { UserService } from './../../Services/User.service';
import { ManagerService } from 'src/app/Services/manager.service';
import { Rentals } from './../../Models/Rentals';
import { CarType } from 'src/app/Models/CarType';
import { RentalsService } from './../../Services/rentals.service';
import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-manager-order-page',
  templateUrl: './manager-order-page.component.html',
  styleUrls: ['./manager-order-page.component.css']
})
export class ManagerOrderPageComponent implements OnInit {
listOfRentals: any;
submitText: string;
submit = 'Submit';
update = 'Update';
RentFormDisplay = true;
listOfCars: any;
listOfUsers: any;
public carRent = new Rentals();
  constructor(private orderService: RentalsService,private cars: ManagerService,private user: UserService, private datePipe: DatePipe) { }

  ngOnInit() {
    // Getting all rentals, cars and users from database
    this.orderService.getAllRentals().subscribe((rentalResponse) => (this.listOfRentals = rentalResponse));
    this.cars.getAllCars().subscribe((carResponse) => (this.listOfCars = carResponse));
    this.user.getAllUsers().subscribe((siteUser) => (this.listOfUsers = siteUser));
   // Settin action button to submit for creating new user- not updating existing user
    this.submitText = this.submit;
  }

  // Delete rental from database
  private deleteRental(rental: any) {
    this.orderService.deleteRentals(rental).subscribe((deleted) => {
      this.listOfRentals = this.listOfRentals.filter(item =>
        item.RentalID !== rental.RentalID);
      });
   }

   // Creating new rental on database
   private newRental() {
    this.submitText = this.submit;
    return this.orderService.createNewRental(this.carRent).subscribe((newRent) => {
      alert("New Rent Added: ");
    });
  }

  // Display/ hide create/ updat form
  private DisplayRentalFormFunction() {
    if(this.submitText === this.update) {
       this.submitText = this.submit;
       this.carRent = new Rentals();
    } else {
     this.RentFormDisplay = !this.RentFormDisplay;
    }
   }

   // Changing date to send to server - to format that the server car read
   private transformDateFormat(date: Date){
    return this.datePipe.transform(date, 'yyyy-MM-dd');
   }

   // Getting info of the rental to display on placeholder
  private catchEditRental(editRental: any) {
    console.log("Trying to catch rental: "+ editRental.RentalID);
    this.carRent.ActualReturn = editRental.ActualReturn;
    this.carRent.CarRented = editRental.CarRented;
    this.carRent.RentCost = editRental.RentCost;
    this.carRent.RentalEnd = editRental.RentalEnd;
    this.transformDateFormat(this.carRent.RentalEnd);
    this.carRent.RentalId = editRental.RentalID;
    this.carRent.RentalStart = editRental.RentalStart;
    this.transformDateFormat(this.carRent.RentalStart);
    this.carRent.User = editRental.User;
    this.submitText = this.update;
    this.RentFormDisplay = false;
  }
  
  // Updating rental in database with put function
  private updateRental(updatedRental: any) {
   console.log('Editing fuction: ' + updatedRental.LicenseNumber);
   this.orderService.editRental(updatedRental).subscribe((updated) => {
     alert('Your car has been edited: ' + updatedRental.RentalID);
   },
   error => {
     console.log('Error: ' + error);
   }
   );
  }

  // Call update/ create action based on writing on button
  private submitAction(update: any) {
    if(this.submitText === this.submit) {
     this.newRental();
    } else {
      this.updateRental(update);
    }
  }
}
