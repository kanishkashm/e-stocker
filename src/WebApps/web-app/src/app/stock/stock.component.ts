import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { StockService } from '../core/services/stock.service';

@Component({
  selector: 'app-stock',
  templateUrl: './stock.component.html',
  styleUrls: ['./stock.component.css']
})
export class StockComponent implements OnInit {
  constructor(private http: HttpClient, private stkService: StockService) {

  }

  ngOnInit(): void {
    this.stkService.getData("api/v1/stock")
    // this.http.get('https://localhost:7001/WeatherForecast', {
    //     responseType: "text"
    //   })
    //   // this.userService.addUser(user)
      .subscribe(x => {
        console.log("Success: res: ", x);
      }, err => {
        console.log(err.error);
      });
  }

}
