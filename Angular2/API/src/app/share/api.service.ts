import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AccountModel } from "./model/account.model"


@Injectable({
    providedIn: 'root'
})

export class HttpService{
    

    constructor(private http: HttpClient){}

    loginAccount(data: AccountModel) : Observable<AccountModel>
    {
       return this.http.post<AccountModel>('https://localhost:44314/api/Account/authenticate', data)
    }

    createAccount(data: AccountModel) : Observable<AccountModel>
    {
        return this.http.post<AccountModel>('https://localhost:44314/api/Account/createaccount', data);
    }
}