import { CarType } from 'src/app/Models/CarType';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ModelsService {

 public constructor(private carHttp: HttpClient) { }
  private carTypeURL = 'http://localhost:52907/api/CarType';

  public getAllCarTypes() {
    return this.carHttp.get<CarType[]>(this.carTypeURL);
  }

  public getModelById(modelId: number){
    return this.carHttp.get<CarType>(this.carTypeURL + '/' + modelId);
  }

  public addCarModel(model: CarType){
    return this.carHttp.post<CarType>(this.carTypeURL, model);
  }

  public deleteCarModel(modelId: number){
    return this.carHttp.delete(this.carTypeURL + '/' + modelId);
  }

  public putCarModel(carType: CarType){
    return this.carHttp.put<CarType>(this.carTypeURL + '/' + carType.ModelId, carType);
  }
}