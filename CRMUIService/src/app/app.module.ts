import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {provideHttpClient,HTTP_INTERCEPTORS, withInterceptors} from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './Authentication/login/login.component';
import { ViewCustomerComponent } from './Customers/View/view-customer/view-customer.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MaterialModule } from './material/material.module';
import { AddDialogComponent } from './Customers/Dialogs/Add/add.dialog/add.dialog.component';
import { EditDialogComponent } from './Customers/Dialogs/Edit/edit.dialog/edit.dialog.component';
import { DeleteDialogComponent } from './Customers/Dialogs/Delete/delete.dialog/delete.dialog.component';
import { DataService } from './Customers/Services/data.service';
import { LayoutComponent } from './layout/layout.component';
import { HeaderComponent } from './layout/header/header.component';
import { MenubarComponent } from './layout/menubar/menubar.component';
import {jwtnewInterceptor} from '../app/jwtnew.interceptor';
import {jwtInterceptor} from '../app/Helpers/jwt.interceptor';
import {errorInterceptor} from '../app/Helpers/error.interceptor';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ViewCustomerComponent,
    AddDialogComponent,
    EditDialogComponent,
    DeleteDialogComponent,
    LayoutComponent,
    HeaderComponent,
    MenubarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MaterialModule,
    FormsModule
  ],
 
  providers: [
    provideHttpClient(withInterceptors(
      [jwtnewInterceptor]

    )),

    { provide: HTTP_INTERCEPTORS, useClass: jwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: errorInterceptor, multi: true },
    provideAnimationsAsync(),
    DataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
