import { Component, OnInit } from '@angular/core';
import { FormControl,  FormGroup, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FormValidators } from '@syncfusion/ej2-angular-inputs';
import { AccountModel } from '../share/model/account.model';
import { Router } from '@angular/router';
import {  HttpService } from '../share/api.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public reactForm!: FormGroup;

  public loginObj = new AccountModel();

  constructor (private api: HttpService, private router: Router) { 
    this.reactForm = new FormGroup({
      'username': new FormControl('', [FormValidators.required]),
      'password' : new FormControl('', [FormValidators.required])
    });
  }

  ngOnInit(): void {}

    get username(){
      return this.reactForm.get('username')
    }

    get password(){
      return this.reactForm.get('password');
    }

    login()
    {
      this.loginObj.UserName = this.reactForm.value.username;  
      this.loginObj.PassWord = this.reactForm.value.password;
      this.api.loginAccount(this.loginObj).subscribe(res => {
        this.router.navigate(['home'])
      })
    }
}
