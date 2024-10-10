import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject, Observable } from 'rxjs';
import {User} from '../Models/user.model';
import { map } from 'rxjs/operators';
import {environment} from '../Environments/environment';
@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserSubject: Subject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
       this.currentUserSubject = new Subject<User>();
       this.currentUser = this.currentUserSubject.asObservable();
  }
public get getToken():string{
   let obj= (localStorage.getItem("currentUser")) || '{}';
   var usr=JSON.parse(obj);
   console.log(usr);
   console.log('getokenva');
   if(usr && usr.token.length > 0)
    return usr.token;
else
return '' ;

}

  // Get curent user info
  public get currentUserValue():Observable<User> {
      return this.currentUserSubject;
  }

  login(username: string, password: string) {
    // Authenticate Api url
    console.log(password);
    var url = environment.config.apiBaseUrl + environment.config.loginAPI;
    const loginModel = {
        username: username,
        password: password
    };
 var usr=new User();usr.userId=username;
    return this.http.post(url,loginModel,{responseType: 'text'})
        .subscribe(
           (x)=>{
                usr.token=x;
                localStorage.setItem("currentUser",JSON.stringify(usr));
                this.currentUserSubject.next(usr);
           }
        );
               
            // store user details and jwt token in local storage to keep user logged in between page refreshes
        //    usr.userId=username;
        //    usr.userType='';
        //     localStorage.setItem("currentUserToken", jwttoken);
        //     localStorage.setItem("currentUser",JSON.stringify(usr));
        //     this.currentUserSubject.next(usr);
            
        
}

logout() {
    // remove user from local storage to log user out
    localStorage.removeItem("currentUserToken");
    this.currentUserSubject.next(new User());
}
}
