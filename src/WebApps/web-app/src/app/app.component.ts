import { Component, OnInit } from '@angular/core';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'web-app';
  public isUserAuthenticated: boolean = false;

  constructor(private _authService: AuthService) {

  }

  ngOnInit(): void {
    this._authService.loginChanged.subscribe((res) => {
      console.log('loginChanged: ', res);
      this.isUserAuthenticated = res;
    });
  }

  public login = () => {
    this._authService.login();
  };

  public logout = () => {
    this._authService.logout();
  };
}
