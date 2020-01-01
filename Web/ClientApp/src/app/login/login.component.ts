import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ApiService } from '../api.service';
import { Router } from '@angular/router';
import { finalize } from 'rxjs/operators';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: User = { username: '', password: '' }

    constructor(public spinner: NgxSpinnerService, private api: ApiService, private router: Router) {
    
  }

  ngOnInit() {
  }

  onSubmit() {
      this.spinner.show();
      this.api.getDeadline(this.login)
          //.pipe(finalize(() => {
          //    this.spinner.hide();
          //}))  
          .subscribe((result: string[]) => {
          this.api.data = result;
          this.router.navigate(['/deadline'])
    }, error => console.error(error));
  }
}

class User {
  username: string;
  password: string;
}

