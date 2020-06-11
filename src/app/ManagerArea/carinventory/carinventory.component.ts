import { HttpClient } from '@angular/common/http';
import { ModelsService } from './../../Services/models.service';
import { Cars } from '../../Models/Cars';
import { ManagerService } from 'src/app/Services/manager.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';


@Component({
  selector: 'app-carinventory',
  templateUrl: './carinventory.component.html',
  styleUrls: ['./carinventory.component.css']
})

export class CarinventoryComponent implements OnInit {
 listOfCars: any;
 listOfCarLot: any;
 listOfCarTypes:any;
  newCarSectionDisplay = true;
  licensePlaceholder: any = 'Enter license Number';
  submitText: string;
  submit = "Submit";
  update = "Update";
  
  public car = new Cars();


  constructor(private manager: ManagerService, private carType: ModelsService, private http:HttpClient) { }

  
  ngOnInit() {
    // Getting all cars, car models, and car lots from database
    this.manager.getAllCars().subscribe((carResponse) => (this.listOfCars = carResponse));
    this.manager.getLots().subscribe((lotResponse) => (this.listOfCarLot = lotResponse));
    this.carType.getAllCarTypes().subscribe((modelResponse) => (this.listOfCarTypes = modelResponse));
    this.submitText = this.submit;
  }
  // Deleting a car from database
   private deleteCarFromInventory(car: any) {
    this.manager.deleteCarInventory(car.LicenseNumber).subscribe((deleted) => {
      this.listOfCars = this.listOfCars.filter(item =>
        item.LicenseNumber !== car.LicenseNumber);
      });
   }
   // Creating new car in database, first we send the image to the server
   // then we call the add car function- and in the server they add them both the the database
   private newCarToInventory(car: any) {
     this.submitText = this.submit;
     this.licensePlaceholder = "Lincense number";
     let isAddable = false;
     for (let myCar in this.listOfCars){
      // Checking that the car does not exist yet in database 
      if (this.listOfCars[myCar].LicenseNumber === car.LicenseNumber){
        isAddable = false;
       }
       else{
         isAddable = true;
       }
     }
     if (isAddable === true){
       console.log('image: ' + this.car.Image);
       // Sending image to server
      return this.manager.addImage(this.car.Image).subscribe((newImage) => {
        // Calling fuction to create new car
        this.addCarFunc();});
     }
     else{
       alert('That car already exists!');
     }
  }
  // Sending car info to create new car in database
  private addCarFunc(){
    this.manager.addCar(this.car).subscribe((newCar)=>{
      alert("New car added to inventory: " + newCar);
      location.reload();
    });
  }
  // Displaying/ hiding add/ update section
   private DisplayNewCarSection() {
     if(this.submitText === this.update) {
        this.submitText = this.submit;
        this.car = new Cars();
     } else {
      this.newCarSectionDisplay = !this.newCarSectionDisplay;
     }
    }

    // Getting info of car to display on placeholder
   private catchEditCar(editCar: any) {
     console.log("Trying to catch car: "+ editCar.LicenseNumber);
     this.car.CarType = editCar.CarType;
     this.car.CarUsage = editCar.CarUsage;
     this.car.IsAvalible = editCar.IsAvalible;
     this.car.IsUsable = editCar.IsUsable;
     this.licensePlaceholder = this.car.LicenseNumber;
     this.car.Lot = editCar.Lot;
     this.submitText = this.update;
     this.car.LicenseNumber = editCar.LicenseNumber;
     this.newCarSectionDisplay = false;
   }

   // Updating car in database with put function
   private updateCar(updatedCar: any) {
    console.log('Editing fuction: ' + updatedCar.LicenseNumber);
    this.manager.putCarInventory(updatedCar).subscribe((updated) => {
      alert('Your car has been edited: ' + updatedCar.LicenseNumber);
    },
    error => {
      console.log('Error: ' + error);
    }
    );
   }

   // Calling create/ update fuctions based on what is written on button
   private submitAction(car: any) {
     if(this.submitText === this.submit) {
      this.newCarToInventory(car);
     } else {
       this.updateCar(car);
     }
   }

   // Getting image from image component
   private addImageToUser(file: any) {
    this.car.Image = file;
    console.log('Lests see if we got the file' + file);
   }
}
