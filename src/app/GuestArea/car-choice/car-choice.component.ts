import { CarType } from './../../Models/CarType';
import { LOCAL_STORAGE, StorageService } from 'ngx-webstorage-service';
import { Cars } from './../../Models/Cars';
import { LocalStorageService } from './../../Services/local-storage.service';
import { ModelsService } from './../../Services/models.service';
import { ManagerService } from './../../Services/manager.service';
import { Component, OnInit, Inject } from '@angular/core';


@Component({
  selector: 'app-car-choice',
  templateUrl: './car-choice.component.html',
  styleUrls: ['./car-choice.component.css']
})
export class CarChoiceComponent implements OnInit {
  public listOfCars: Cars[];
  searchText;
  public listOfCarTypes = new Array<CarType>();
  carsFromStorage: Cars[];
  licenseNumber: number;
  avalibleCars: Cars[];
 
  public car = new Cars();
  constructor(private carService: ManagerService, private carType: ModelsService,
              private localStorageService: LocalStorageService) { }

   // Erasing car from local storage           
  removeFromLocalStorage(car: Cars){
    this.localStorageService.removeUserFromStorage(car);
    location.reload();
  } 
  
  private filterAvalibleCar(){

  }

  ngOnInit() {
    // Getting all the cars that are avalible
    this.carService.getAllCars().subscribe((carResponse) =>{
      this.listOfCars = carResponse.filter(car=> car.IsAvalible === true && car.IsUsable === true);
    },err=>{
      console.log('Error: ' + err);
    } );
    this.carsFromStorage = this.localStorageService.getAllCarsFromStorage();
    this.carsFromStorage.forEach(a=>{
      console.log('carsFromStorage: ' + a.LicenseNumber);

    });
    }
}