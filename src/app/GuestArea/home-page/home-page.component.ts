import { Component, OnInit, NgModule } from '@angular/core';
import { isGeneratedFile } from '@angular/compiler/src/aot/util';
import { from } from 'rxjs';
@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
signUpDisplay=true;
ngOnInit()
 {
   // Checking if user is signed in or not to display/ hide log in + sign up buttons
  if (localStorage.getItem("currentUser") === null) {
    this.signUpDisplay= false;
  }
}

}
