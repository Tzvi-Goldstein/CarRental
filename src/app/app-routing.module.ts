import { LoginComponent } from './login/login.component';
import { RentPriceComponent } from './GuestArea/rent-price/rent-price.component';
import { SignUpComponent } from './GuestArea/sign-up/sign-up.component';
import { ManagerOrderPageComponent } from './ManagerArea/manager-order-page/manager-order-page.component';
import { CarModelComponent } from './ManagerArea/car-model/car-model.component';
import { CarinventoryComponent } from './ManagerArea/carinventory/carinventory.component';
import { ManagerUserPageComponent } from './ManagerArea/manager-user-page/manager-user-page.component';
import { ManagerPageComponent } from './ManagerArea/manager-page/manager-page.component';
import { HomePageComponent } from './GuestArea/home-page/home-page.component';
import { NgModule, Component } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReturningCarComponent } from './WorkerArea/returning-car/returning-car.component';
import { DatePipe } from '@angular/common';
import { CarChoiceComponent } from './GuestArea/car-choice/car-choice.component';
import { AuthGuard } from './auth/auth.guard';
import { Roles } from './Models/Role';


const routes: Routes = [
  {path:"HomePage",component:HomePageComponent},
  {path:"ManagerPage",component:ManagerPageComponent,
  canActivate: [AuthGuard], 
        data: { roles: [Roles.Admin] } 
      },
  {path:"MangerUserPage",component:ManagerUserPageComponent,
  canActivate: [AuthGuard], 
  data: { roles: [Roles.Admin] } 
},
  {path:"ManageCars",component:CarinventoryComponent, 
  canActivate: [AuthGuard], 
  data: { roles: [Roles.Admin] } 
},
  {path:"ReturningCars",component:ReturningCarComponent,
  canActivate: [AuthGuard], 
  data: { roles: [Roles.Worker, Roles.Admin] } 
},
  {path:"CarModels", component:CarModelComponent,
  canActivate: [AuthGuard], 
  data: { roles: [Roles.Admin] } 
},
  {path:"ManagerRentals",component:ManagerOrderPageComponent,
  canActivate: [AuthGuard], 
  data: { roles: [Roles.Admin] } 
},
  {path:"SignUpUser",component:SignUpComponent},
  {path:"CarChoice",component:CarChoiceComponent,
  canActivate: [AuthGuard], 
  data: { roles: [Roles.Admin, Roles.Worker, Roles.User] } 
},
  {path:"RentPrice/:id",component:RentPriceComponent,
  canActivate: [AuthGuard], 
  data: { roles: [Roles.Admin, Roles.Worker, Roles.User] } },
  {path:"Login",component:LoginComponent}, 
  {path:"**",redirectTo:"HomePage", pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers:[DatePipe]
})
export class AppRoutingModule { }
