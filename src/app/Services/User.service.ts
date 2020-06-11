import { User } from './../Models/User';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url = 'http://localhost:52907/api/WebSiteUser/';
  
  constructor(private http: HttpClient) { }

  public getAllUsers() {
    return this.http.get<User[]>(this.url);
  }

  public getUserById(id: number) {
    return this.http.get<User>(this.url + id);
  }

  public addNewUser(user: User) {
    console.log('id from service: ' + user.Tz);
    return this.http.post<any>(this.url, user);
  }

  public addImage(userData: FormData) {
    return this.http.post(this.url + 'PostFormData', userData);
  }

  public deleteUser(userId: number) {
    return this.http.delete(this.url + '/' + userId);
  }

  public updateUser(user: User) {
    return this.http.put<User>(this.url + '/' + user.Tz , user);
  }
}
