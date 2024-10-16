import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Authentication/login/login.component';
import { LayoutComponent } from './layout/layout.component';
import { ViewCustomerComponent } from './Customers/View/view-customer/view-customer.component';
import { authguardGuard } from './Helpers/authguard.guard';
const routes: Routes = [
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path:'Customers',
    component:ViewCustomerComponent,
    canActivate: [authguardGuard]
  },
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
