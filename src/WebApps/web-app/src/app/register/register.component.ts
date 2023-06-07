import { Component, OnInit } from '@angular/core';
import { User } from './dto/user.dto';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  user: User = {};
  // public username: string = '';
  // password: string = '';
  // confirmPassword: string = '';
  // role: string = '';

  constructor(private http: HttpClient,) {
    this.user = {};
  }

  ngOnInit(): void {
    
  }

  async addUser() {
    console.log("User: ", this.user);
    this.http.post('https://localhost:7000/api/v1/account/register', this.user, {
        responseType: "text"
      })
      // this.userService.addUser(user)
      .subscribe(x => {
        console.log("Success: res: ", x);
      }, err => {
        console.log(err.error);
      });
  }
}
