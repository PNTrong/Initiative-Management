import { BaseService } from 'app/services/base.service';
import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';

import 'rxjs/add/operator/map'
import { Observable } from 'rxjs/Observable';


let serverUrl = '';

@Injectable()
export class AuthenticationService {
    token: string;
    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http) {
        // set token if saved in local storage
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        this.token = currentUser && currentUser.token;
        if (window.location.host === 'localhost:4200') {
            serverUrl = 'http://localhost:55429/'; // VS
          }
    }

    login(username: string, password: string): Observable<boolean> {

        let data = 'grant_type=password&username=' + username + '&password=' + password;

        // Creates header for post requests.
        this.headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        this.options = new RequestOptions({ headers: this.headers });


        let url = serverUrl + 'oauth/token'

        return this.http.post(url, data, this.options).map((response: Response) => {

                // login successful if there's a jwt token in the response
                let token = response.json() && response.json().token;

                if (token) {
                    // set token property
                    this.token = token;

                    // store username and jwt token in local storage to keep user logged in between page refreshes
                    localStorage.setItem('currentUser', JSON.stringify({ username: username, token: token }));

                    // return true to indicate successful login
                    return true;
                } else {
                    // return false to indicate failed login
                    return false;
                }
            });
    }

    logout(): void {
        // clear token remove user from local storage to log user out
        this.token = null;
        localStorage.removeItem('currentUser');
    }
}
