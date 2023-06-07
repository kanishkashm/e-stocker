import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { from } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class StockService {
  constructor(private http: HttpClient, private _authService: AuthService) {}
  public getData = (route: string) => {
    return this.http.get(
      this.createCompleteRoute(route, environment.apiBaseUrl)
    );
    // return from(
    //   this._authService.getAccessToken()
    //   .then(token => {
    //     const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    //     return this.http.get(this.createCompleteRoute(route, environment.apiBaseUrl), { headers: headers }).toPromise();
    //   })
    // );
  };

  public addData = (route: string, data: any) => {
    return this.http.post(
      this.createCompleteRoute(route, environment.apiBaseUrl),
      data
    );
  };

  public editData = (route: string, data: any) => {
    return this.http.put(
      this.createCompleteRoute(route, environment.apiBaseUrl),
      data
    );
  };

  private createCompleteRoute = (route: string, envAddress: string) => {
    return `${envAddress}/${route}`;
  };
}
