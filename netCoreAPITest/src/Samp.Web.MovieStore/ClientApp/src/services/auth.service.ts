import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiClientService } from '../services/apiclient.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string
    , private apiClient: ApiClientService
  ) { }

  login(username: string, password: string): Observable<any> {

    return this.apiClient.post('/token', {
      username,
      password,
      grant_type: 'password'
    }, null, 'application/x-www-form-urlencoded');

  }

  register(username: string, email: string, password: string): Observable<any> {
    const body = new HttpParams({
      fromObject: {
        username,
        email,
        password
      }
    });
    return this.http.post(this.baseUrl + '/register'
      , body.toString()
      , { headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' }) }
    );
  }
}
