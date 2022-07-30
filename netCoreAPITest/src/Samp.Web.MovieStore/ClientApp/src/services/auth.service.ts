import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiClientService } from '../services/apiclient.service';
import { ResponseModel } from '../models/responses/response-model';
import { TokenDto } from '../models/responses/identity/token.dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string
    , private api: ApiClientService
  ) { }

  login(username: string, password: string): Observable<ResponseModel<TokenDto>> {
    return this.api.post<TokenDto>('/token', {
      username,
      password,
      grant_type: 'password'
    }, null, 'application/x-www-form-urlencoded');
  }

  register(username: string, email: string, password: string): Observable<ResponseModel<any>> {
    return this.api.post<any>('/register', {
      username,
      email,
      password
    }, null, 'application/x-www-form-urlencoded');
  }
}
