import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiClientService } from '../api-client.service';
import { ResponseModel } from '../../models/responses/response-model';
import { TokenDto } from '../../models/responses/identity/token.dto';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  constructor(private http: HttpClient
    , private api: ApiClientService
  ) { }

  login(username: string, password: string): Promise<ResponseModel<TokenDto>> {
    return this.api.post<TokenDto>('/token', {
      username,
      password,
      grant_type: 'password'
    }, null, 'application/x-www-form-urlencoded');
  }

  register(username: string, email: string, password: string): Promise<ResponseModel<any>> {
    return this.api.post<any>('/register', {
      username,
      email,
      password
    }, null, 'application/x-www-form-urlencoded');
  }
}
