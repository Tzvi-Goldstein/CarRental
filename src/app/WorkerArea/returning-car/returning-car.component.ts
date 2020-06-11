import { CarType } from 'src/app/Models/CarType';
import { ModelsService } from './../../Services/models.service';
import { Rentals } from './../../Models/Rentals';
import { Cars } from 'src/app/Models/Cars';
import { RentalsService } from './../../Services/rentals.service';
import { ManagerService } from 'src/app/Services/manager.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-returning-car',
  templateUrl: './returning-car.component.html',
  styleUrls: ['./returning-car.component.css']
})
export class ReturningCarComponent implements OnInit {

  allCar: Cars[];
  allRentals: Rentals[];
  searchedCar: Cars;
  public rental = new Rentals();
  isDisplayed = true;
  isAvalibleDisplayed = true;
  @Input() public searchLicense: number;
  public carModel = new CarType();
      
  constructor(private carService: ManagerService, private rentalService: RentalsService,
              private modelService: ModelsService) {}

private printCarRentedByLicense(license: number) {

}

  ngOnInit() {
    // Getting all the car models from the database
    this.carService.getAllCars().subscribe(cars => {
      this.allCar = cars; console.log('Got cars');
    }, err => {
      console.log('Error: ' + err);
    });

    // Getting all rentals from database
    this.rentalService.getAllRentals().subscribe(rentals => {
      this.allRentals = rentals;
    }, err => {
      console.log('Error: ' + err);
    });
  }

// Searching car by license and checking if it is being rented out
  private findCarInSearch(searchLicense: number) {
    let isSuchCar = false;
    let isRentedOut = false;
    this.allCar.forEach(car => {
  if (car.LicenseNumber === searchLicense) {
    isSuchCar = true;
    this.allRentals.forEach(rental => {
      if (rental.CarRented === searchLicense) {
        isRentedOut = true;
        this.rental = rental;
        this.searchedCar = car;
        this.isDisplayed = false;
      }
    });
}} );
    if (isSuchCar === false){
  alert('Sorry there is no such license- try again!');
} else if (isRentedOut === false){
  alert('That car is not being rented out');
}
  }

  // Calculating final price after car being returned- including delay fee
  private calculateFinalPrice(){
    const now = new Date();
    console.log('today: ' + now);
    const rentalFinish = new Date(this.rental.RentalEnd);
    console.log('final rent: ' + rentalFinish);
    // Checking if the car was returned on time- the date of today is equal to expected date
    if (now === rentalFinish){
      this.rental.ActualReturn = rentalFinish;
    } else {
      const expectedCost = this.rental.RentCost;
      this.modelService.getModelById(this.searchedCar.CarType).subscribe(model => {
       this.carModel = model; console.log('Got model: ' + model.ModelId);

       let days = Math.round(Math.abs((+now - +rentalFinish) / (1000 * 60 * 60 * 24)));
       console.log('dat gap: ' + days);
       const paneltyCost = days * +this.carModel.DelayCostPerDay;
       console.log('delay cost: ' + this.carModel.DelayCostPerDay);
       console.log('panelty charge: ' + paneltyCost);
       this.rental.RentCost = expectedCost + paneltyCost;
       this.rental.ActualReturn = now;
    

      }, err => {
        console.log('Error: ' + err);
      } );
      
      // Updating rental table in database
      this.rentalService.editRental(this.rental).subscribe(rental => {
      alert('This final cost is: ' + this.rental.RentCost);
      this.isAvalibleDisplayed = false;
    }, err => {
      alert('Something went wrong!'); console.log('Error: ' + err);
    });
  }
  }
// Setting the returned car to avalible
  private setAvalible(){
    this.searchedCar.IsAvalible = true;
    this.carService.putCarInventory(this.searchedCar).subscribe(avalible=>{
      console.log('car is avalible: ' + avalible.LicenseNumber + 'avalible= ' + avalible.IsAvalible);
    }, err => {
      console.log('Error: ' + err);
    });
  }
}
