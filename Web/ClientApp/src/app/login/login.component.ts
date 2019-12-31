import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApiService } from '../api.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public deadlines: string[];
  login: User = { username: '', password: '' }

  constructor(public spinner: NgxSpinnerService, private api: ApiService) {
    
  }

  ngOnInit() {
  }

  onSubmit() {
    console.log("submited");
    this.api.getDeadline(this.login).subscribe((result: string[]) => {
      this.deadlines = result;
    }, error => console.error(error));
  }
}

class User {
  username: string;
  password: string;
}

