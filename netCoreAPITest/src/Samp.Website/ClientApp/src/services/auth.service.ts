import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

  login(username: string, password: string): Observable<any> {
    const body = new HttpParams({
      fromObject: {
        username,
        password,
        grant_type: 'password'
      }
    });
    return this.http.post(this.baseUrl + '/token'
      , body.toString()
      , { headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' }) }
    );
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
