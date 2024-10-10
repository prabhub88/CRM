import { Component,OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {AuthenticationService} from '../../Services/authentication.service';
@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  loginUser: string;

  constructor(private authenticationService: AuthenticationService, private router: Router) { }

  ngOnInit() {
    const user = this.authenticationService.currentUserValue.subscribe(
      (x) => {
        this.loginUser = x.userId;
      }
    );
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/']);
  }
}
