import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';

let serverUrl = '';
const httpOptions: any = { withCredentials: true };

@Injectable()
export class BaseService {
  constructor(private http: Http) {
    if (window.location.host === 'localhost:4200') {
      serverUrl = 'http://localhost:55429/'; // VS
    }
  }

  getApiHost(): string { return serverUrl; }

  get<T>(url: string, options?: RequestOptions): Observable<T> {
    if (options) {
      return this.http.get(url, options).map<Response, T>((response: Response) => {
        return response.json() as T; }).catch((error: any) => {
        if (error.status === 302 || error.status === '302') {
          // do some thing
        } else if(error.status === 401 || error.status === '401') {
          // redirect to login page
        } else {
          return Observable.throw(new Error(error));
        }
      });
    } else {
      return this.http.get(url).map<Response, T>((response: Response) => {
        return response.json() as T; }).catch((error: any) => {
        if (error.status === 302 || error.status === '302') {
          // do some thing
        } else {
          return Observable.throw(new Error(error));
        }
      });
    }

  }

  post<T>(url: string, data: any, options?: RequestOptions): Observable<T> {
    if (options) {
      return this.http.post(url, data, httpOptions).map<Response, T>((response: Response) => {
        return  response.json() as T; }).catch((error: any) => {
        if (error.status === 302 || error.status === '302') {
          // do some thing
        } else if(error.status === 401 || error.status === '401') {
          // redirect to login page
        } else {
          return Observable.throw(new Error(error));
        }
      });
     } else {
      return this.http.post(url, data).map<Response, T>((response: Response) => {
        return  response.json() as T; }).catch((error: any) => {
        if (error.status === 302 || error.status === '302') {
          // do some thing
        } else {
          return Observable.throw(new Error(error));
        }
      });
     }

  }

}
