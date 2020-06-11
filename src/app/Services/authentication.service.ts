import { UserService } from './User.service';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../Models/User';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  user: User;

  constructor(private userService: UserService) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();

   }

   public get currentUserValue(): User {
    return this.currentUserSubject.value;
}
// Loging in fuction adds the user to the local storage
login(username: string, password: string) {
    this.userService.getAllUsers().subscribe(users=> {
      users.forEach(user=> {
        if(user.UserName === username && user.Password === password){
          this.user = user;
          localStorage.setItem('currentUser', JSON.stringify(this.user));
          this.currentUserSubject.next(user);
        }
      });
    });
    return this.user;
  }

// remove user from local storage to log user out
logout() {
  localStorage.removeItem('currentUser');
  this.currentUserSubject.next(null);
      }
}


