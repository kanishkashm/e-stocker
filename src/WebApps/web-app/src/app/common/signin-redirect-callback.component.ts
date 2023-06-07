import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-signin-redirect-callback',
  template: '<div></div>'
})
export class SigninRedirectCallbackComponent implements OnInit {

  constructor(private _authService: AuthService, private _router: Router) { }
  ngOnInit(): void {
    console.log("SigninRedirectCallbackComponent ===> ngOnInit");
    this._authService.finishLogin().then((_) => {
      console.log("SigninRedirectCallbackComponent ===> ngOnInit ===> finishLogin");
      this._router.navigate(['/'], { replaceUrl: true });
    });
  }

}