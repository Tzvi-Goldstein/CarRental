import { CarType } from './../Models/CarType';
import { Cars } from '../Models/Cars';
import { observable, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {
 public constructor(private carHttp: HttpClient) { }

 private url = 'http://localhost:52907/api/CarInventory/';
 private lotUrl = 'http://localhost:52907/api/Lot';


  public getAllCars() {
    return this.carHttp.get<Cars[]>(this.url);
  }

  public getLots() {
    return this.carHttp.get(this.lotUrl);
 }

  public addImage(carData: FormData) {
    return this.carHttp.post(this.url + 'PostFormData', carData);
  }

  public addCar(car: Cars) {
    return this.carHttp.post(this.url, car);
  }

   public putCarInventory(car: Cars) {
    return this.carHttp.put<Cars>(this.url + '/' + car.LicenseNumber, car);
  }

  public deleteCarInventory(carId: number) {
    return this.carHttp.delete(this.url + '/' + carId);
  }

}
