import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, from, switchMap } from 'rxjs';
import { AuthService } from './auth.service';
import { Constants } from '../const';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {

  constructor(private _authService: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log()
    if(req.url.startsWith(Constants.apiRoot)){
      console.log("intercept if");
      // return from(
      //   this._authService.getAccessToken()
      //   .then(token => {
      //     const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
      //     const authRequest = req.clone({ headers });
      //     const a = next.handle(authRequest).toPromise();
      //     return next.handle(authRequest).toPromise() as HttpEvent<any>;
      //   })
      // );
      return from(this._authService.getAccessToken())
      .pipe(
        switchMap(token => {
          const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
          const authRequest = req.clone({ headers });
          return next.handle(authRequest)
        })
      );
    }
    else {
      console.log("intercept else");
      return next.handle(req);
    }
  }
}
