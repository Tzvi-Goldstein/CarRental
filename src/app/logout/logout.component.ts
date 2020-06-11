import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../Services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(private authenticationService: AuthenticationService, private router: Router) {}

  ngOnInit() {
  }

  // Erases the users info and permisions from local storage and redirects them to home page
  signOut(){
    this.authenticationService.logout();
    this.router.navigate(['/']);
  }

}
