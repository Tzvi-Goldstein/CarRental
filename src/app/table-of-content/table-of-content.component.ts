import { Component, OnInit } from '@angular/core';
import { User } from '../Models/User';
import { UserService } from '../Services/User.service';
import { AuthenticationService } from '../Services/authentication.service';
import { first } from 'rxjs/operators';
import { Router } from '@angular/router';

@Component({
  selector: 'app-table-of-content',
  templateUrl: './table-of-content.component.html',
  styleUrls: ['./table-of-content.component.css']
})
export class TableOfContentComponent implements OnInit {
  currentUser: User;
   
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService
  ) { 
    // Gets current user from local storge to see what buttons in table of content he should see
    this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
   }

  ngOnInit() {
    console.log('User permisions: '+ this.currentUser.Permissions);
   
  }

}
