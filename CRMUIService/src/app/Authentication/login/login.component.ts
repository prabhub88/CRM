import { Component,signal } from '@angular/core';
import {Router} from '@angular/router';
import { first } from 'rxjs/operators';
import {AuthenticationService} from '../../Services/authentication.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  constructor(private router:Router,private authSvc:AuthenticationService){
   }
   loginFail=false;
   value = signal(0);

  username:string='';
  userpwd:string='';
  token='';
  loading=false;

  onLogin()
  {
    console.log(this.userpwd);
    this.loading=true;
    this.authSvc.login(this.username,this.userpwd)
    this.authSvc.currentUserValue.subscribe(
      (x)=>{
        console.log(x);
        if(x.token.length > 0)
          this.router.navigateByUrl('Customers');
      }
    )
      
     this.loginFail=true;
    this.loading=false;
  }
}
