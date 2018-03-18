import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({

  selector: 'app-register',
  templateUrl: './Register.component.html',
  styleUrls: ['./Register.component.css']
})
export class RegisterComponent implements OnInit {
model: any = {};
@Input()valuesFromHome: any;
@Output() cancelRegister = new EventEmitter();
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

register() {
  this.authService.register(this.model).subscribe(() => {
    console.log('Registration successfull');
  }, error => {
    console.log(error);
  });
}

cancel() {
  console.log('cancelled');
}
}
