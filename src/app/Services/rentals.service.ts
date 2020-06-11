import { Rentals } from './../Models/Rentals';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RentalsService {

  constructor(private http: HttpClient) { }
  url = 'http://localhost:52907/api/Rentals';

  public getAllRentals() {
   return this.http.get<Rentals[]>(this.url);
  }

  public createNewRental(rental: Rentals) {
   return this.http.post<Rentals>(this.url, rental);
  }

  public editRental(rental: Rentals) {
   return this.http.put(this.url + '/' + rental.RentalId, rental);
  }

  public deleteRentals(rental: Rentals) {
    return this.http.delete(this.url + '/' + rental.RentalId);
  }
}
