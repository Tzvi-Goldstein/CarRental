import { Cars } from './../Models/Cars';
import { Injectable, Inject } from '@angular/core';
import { NgModule } from '@angular/core';

@Injectable(
  {
  providedIn: 'root'
}
)
export class LocalStorageService {
  listOfCars: Cars[] = [];
  LOCAL_KEY = 'carsStored';
  constructor() {  }
// Adding a car that was looked at to the local storage
  public setCarToStorage(car: Cars): void {
    let alreadExists = false;
    if (localStorage.getItem(this.LOCAL_KEY) == null) {
      this.listOfCars.push(car);
    } else {
      this.listOfCars = JSON.parse(localStorage.getItem(this.LOCAL_KEY));
      this.listOfCars.forEach(inList => {
       // Checks that the car is not in the local storage yet and then adds it to the local storage
        if (inList.LicenseNumber === car.LicenseNumber){
          alreadExists = true;
        }
      });
      if (!alreadExists)
      {
        this.listOfCars.push(car);
      }
      }
    console.log('getCarStorage: ' + this.listOfCars);
    localStorage.setItem(this.LOCAL_KEY, JSON.stringify(this.listOfCars));
  }
// Getting all the cars from the local storage
  public getAllCarsFromStorage(): Cars[] {
    if (localStorage.getItem(this.LOCAL_KEY) == null) {
      this.listOfCars = [];
    } else {
      this.listOfCars = [];
      this.listOfCars = JSON.parse(localStorage.getItem(this.LOCAL_KEY));
    }
    return this.listOfCars;
  }
// Removing car from local storage
  public removeUserFromStorage(carToRemove: Cars) {
   const newList: Cars[] = [];
   this.listOfCars.forEach(car => {console.log('In removing' + car.LicenseNumber);
                                   if (car.LicenseNumber !== carToRemove.LicenseNumber) {
         newList.push(car);
         console.log('new list' + newList);
       }
     });
   this.listOfCars = [];
   this.listOfCars = newList;
   localStorage.clear();
   localStorage.setItem(this.LOCAL_KEY, JSON.stringify(newList));
     }
  }

