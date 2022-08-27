import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  getPublicContent(): Observable<any> {
    return this.http.get(this.baseUrl + '/users/public', { responseType: 'text' });
  }

  getUserContent(): Observable<any> {
    return this.http.get(this.baseUrl + '/users/current', { responseType: 'text' });
  }
}
