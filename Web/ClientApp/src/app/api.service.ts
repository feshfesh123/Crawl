import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}
 
  getDeadline(userLogin: any) {
    return this.http.post(this.baseUrl + 'deadline', { username: userLogin.username, password: userLogin.password });
  }
}
