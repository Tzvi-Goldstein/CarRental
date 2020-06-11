import { DatePipe } from '@angular/common';
import { User } from "./../../Models/User";
import { UserService } from "./../../Services/User.service";
import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-manager-user-page",
  templateUrl: "./manager-user-page.component.html",
  styleUrls: ["./manager-user-page.component.css"],
})
export class ManagerUserPageComponent implements OnInit {
  
  getListOfUsers: any;
  listOfUsers: any;
  NewUserDisplay = true;
  userToEdit: any;
  editVisible = true;
  editUser: any;
  idPlaceholder: any = 'User Id';
  submitText: string;
  submit = 'Submit';
  update = 'Update';
  public user = new User();

  constructor(private userService: UserService, private datePipe: DatePipe) {}

  // The function hides/ displays add new user section
  DisplayNewUsersSection() {
    if(this.submitText === this.update){
      this.submitText = this.submit;
      this.user = new User();
    }else{
      this.NewUserDisplay = !this.NewUserDisplay;
    }  
    }

    ngOnInit() {
      this.userService
        .getAllUsers()
        .subscribe((response) => (this.listOfUsers = response));
      this.submitText = this.submit;
    }
 
  // Add an image to the user
  private createNewUser(){
    return this.userService.addImage(this.user.Image).subscribe((newImage) => {
      this.addUserDetails();});
     }
// Creates new user and sends info the the server
     private addUserDetails(){
    return this.userService.addNewUser(this.user).subscribe((newUser) => {
      alert('Welcome' + newUser.fullName + ' your subscription was successful');
    });
    location.reload();
     }
// Deletes existing users from the database
  private deleteUser(user: any) {
    this.userService.deleteUser(user.Tz).subscribe((deleted) => {
      this.listOfUsers = this.listOfUsers.filter(item => item.TZ !== user.TZ);
      alert('The user was erased successfuly');
      location.reload();
    },
    err=>{
      console.log('Error: ' + err);
    });
  }
// Updates existing users using put api function
  private updateUser(updatedUser: any){
    console.log("Editing function: "+ updatedUser.TZ);
    this.userService.updateUser(updatedUser).subscribe((updated)=>{
      alert("Your user has been edited successfully: "+updatedUser.TZ);
    },
    error => {
      console.log("Error: "+error);
    }
    );
  }
  // Changes the date format to make the date readable by the server
  private transformDateFormat(date: Date){
    return this.datePipe.transform(date, 'yyyy-MM-dd');
   }
  

  // Used to put the placeholder in the fields for updating
  private catchEditUser(editUser: any) {
    this.NewUserDisplay = false;
    this.user.Tz  = editUser.TZ;
    this.user.Fullname = editUser.Fullname;
    this.user.UserName = editUser.UserName;
    this.user.Password = editUser.Password;
    this.user.Birthdate = editUser.Birthdate;
    this.transformDateFormat(this.user.Birthdate);
    this.user.Gender = editUser.Gender;
    this.user.Email = editUser.Email;
    this.user.Permissions = editUser.Permissions;
    this.submitText = this.update;
    this.NewUserDisplay = false;
    this.idPlaceholder = this.user.Tz ;
  }
 // Calls the create/ update functions based on what is displayed on the action button
  private submitAction(user: any){
    if (this.submitText === this.submit){
      this.createNewUser();
    }
      // If the update is set to edit- then the form will edit form
      else {
        this.updateUser(user);
      }
    }

    // Gets the event form the image component
    private addImageToUser(file: any) {
      this.user.Image = file;
     }
}


