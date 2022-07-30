import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiClientService } from '../services/apiclient.service';
import { ResponseModel } from '../models/response-model';
import { TokenDto } from '../models/responses/identity/token-dto.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string
    , private apiClient: ApiClientService
  ) { }

  login(username: string, password: string): Observable<ResponseModel<TokenDto>> {
    return this.apiClient.post<ResponseModel<TokenDto>>('/token', {
      username,
      password,
      grant_type: 'password'
    }, null, 'application/x-www-form-urlencoded');
  }

  register(username: string, email: string, password: string): Observable<ResponseModel<any>> {
    const body = new HttpParams({
      fromObject: {
        username,
        email,
        password
      }
    });
    return this.http.post<ResponseModel<any>>(this.baseUrl + '/register'
      , body.toString()
      , { headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' }) }
    );
  }
}
