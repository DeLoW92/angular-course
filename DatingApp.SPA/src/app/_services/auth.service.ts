import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Response } from '@angular/http';
import 'rxjs/add/operator/map';


@Injectable()
export class AuthService {
  baseUrl = 'http://localhost:5000/api/auth/';
  userToken: any;

  constructor(private http: HttpClient) {}

  login(model: any) {

    return this.http.post<HttpResponse<any>>(this.baseUrl + 'login', model, {
      headers: this.requestOptions() }).map( response => {
        const hdr = response['tokenString'];
        if (hdr) {localStorage.setItem('token', hdr); }
    });

  }


  register(model: any) {
    return this.http.post(this.baseUrl + 'register', model, {headers: this.requestOptions()});
  }

  private requestOptions() {
    const headers = new HttpHeaders({ 'Content-type': 'application/json' });
    return headers;
  }
}
