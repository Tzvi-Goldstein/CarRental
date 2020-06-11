import { Cars } from './../../Models/Cars';
import { CarType } from 'src/app/Models/CarType';
import { ModelsService } from './../../Services/models.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-car-model',
  templateUrl: './car-model.component.html',
  styleUrls: ['./car-model.component.css']
})
export class CarModelComponent implements OnInit {
  listOfCarTypes: any;
  submitText: string;
  submit = 'Submit';
  update = 'Update';
  newModelSectionDisplay = true;

  public carType = new CarType();
  constructor(private carService: ModelsService) { }

  ngOnInit() {
    // Getting all car models from server
    this.carService.getAllCarTypes().subscribe((modelResponse) => (this.listOfCarTypes = modelResponse));
    this.submitText = this.submit;
  }
 // Deleting spicific car model from database
  private deleteModel(model: any) {
    this.carService.deleteCarModel(model.ModelId).subscribe((deleted) => {
      this.listOfCarTypes = this.listOfCarTypes.filter(item =>
        item.ModelId !== model.ModelId);
      });
   }
// Creating new car model
   private newCarModel(model: any) {
    let isCreatable = false;
    for(let carModel in this.listOfCarTypes){
      
    // Checking if the car model already exists
      if (this.listOfCarTypes[carModel].ModelId === model.ModelId){
          isCreatable = false;
      }
      else{
        isCreatable = true;
      }
    }
    if (isCreatable === true){
      this.submitText = this.submit;
      return this.carService.addCarModel(this.carType).subscribe((model) => {
      alert('New car added to inventory: ' + model.Manufacturer);
    });
    }
    else{
      alert('That model already exists');
    }
  }
  // Displaying or hiding new/ update form
  private DisplayNewModelSection() {
    if(this.submitText === this.update) {
       this.submitText = this.submit;
       this.carType = new CarType();
    } else {
     this.newModelSectionDisplay = !this.newModelSectionDisplay;
    }
   }
   // Getting car model info to present on placeholder
   private catchEditModel(editModel: any) {
    console.log('Trying to catch car: ' + editModel.Manufacturer);
    this.carType.Manufacturer = editModel.Manufacturer;
    this.carType.Model = editModel.Model;
    this.carType.CostPerDay = editModel.CostPerDay;
    this.carType.DelayCostPerDay = editModel.DelayCostPerDay;
    this.carType.YearManufactured = editModel.YearManufactured;
    this.carType.IsGear = editModel.IsGear;
    this.carType.ModelId = editModel.ModelId;
    this.submitText = this.update;
    this.newModelSectionDisplay = false;
  }
  // Updating car model to database with put request
  private updateModel(updatedModel: any) {
    console.log('Editing fuction: ' + updatedModel.Manufacturer);
    this.carService.putCarModel(updatedModel).subscribe((updated) => {
      alert('Your car has been edited: ' + updatedModel.Manufacturer);
    },
    error => {
      console.log('Error: ' + error);
    }
    );
   }
   // Choosing put/ post functions based on writing the action button
   private submitAction(model: any) {
    if(this.submitText === this.submit) {
     this.newCarModel(model);
    } else {
      this.updateModel(model);
    }
  }
}
