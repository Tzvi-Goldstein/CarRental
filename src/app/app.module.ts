import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomePageComponent } from './GuestArea/home-page/home-page.component';
import { ManagerPageComponent } from './ManagerArea/manager-page/manager-page.component';
import { CarinventoryComponent } from './ManagerArea/carinventory/carinventory.component';
import { ManagerUserPageComponent } from './ManagerArea/manager-user-page/manager-user-page.component';
import { ManagerOrderPageComponent } from './ManagerArea/manager-order-page/manager-order-page.component';
import { HttpClientModule} from '@angular/common/http';
import { TableOfContentComponent } from './table-of-content/table-of-content.component';
import { ReturningCarComponent } from './WorkerArea/returning-car/returning-car.component';
import { CarModelComponent } from './ManagerArea/car-model/car-model.component';
import { SignUpComponent } from './GuestArea/sign-up/sign-up.component';
import { ImageUploadComponent } from './ImageUpload/image-upload/image-upload.component';
import { CarChoiceComponent } from './GuestArea/car-choice/car-choice.component';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { RentPriceComponent } from './GuestArea/rent-price/rent-price.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    ManagerPageComponent,
    ManagerUserPageComponent,
    ManagerOrderPageComponent,
    TableOfContentComponent,
    CarinventoryComponent,
    ReturningCarComponent,
    CarModelComponent,
    SignUpComponent,
    ImageUploadComponent,
    CarChoiceComponent,
    RentPriceComponent,
    LoginComponent,
    LogoutComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    Ng2SearchPipeModule
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
