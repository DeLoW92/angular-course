import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {}; // Creo un elemento de cualquier tipo vacio

  constructor(private authService: AuthService) {}

  ngOnInit() {}

  login() {
// tslint:disable-next-line:no-debugger
// debugger;
    const a = this.authService.login(this.model).subscribe();
    this.authService.login(this.model).subscribe(data => {
      console.log('logged in successfully');
    }, error => {
      console.log('failed to log in.');
    });
  }

  logout() {
    this.authService.userToken = null;
    localStorage.removeItem('token');
    console.log('logged out.');
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !!token;
    // !! indica si es un bool alaaaa
  }
}
