import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenStorageService } from '../services/token-storage.service';
import { ResponseModel } from '../models/responses/response-model';
import { ApiClientErrorHandler } from '../error-handlers/apiclient-error.handler';

@Injectable({
  providedIn: 'root'
})
export class ApiClientService {
  constructor(private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string
    , private tokenStorageService: TokenStorageService
    , private errorHandler: ApiClientErrorHandler
  ) { }

  public get<T>(
    resource: string
    , headers?: HttpHeaders
    , contentType: string = 'application/json'
  ): Promise<ResponseModel<T>> {
    var _headers = this.configureHeaders(headers, contentType);
    var url = this.configureResource(resource);

    var promise = new Promise<ResponseModel<T>>((resolve, reject) => {
      var sub = this.http.get<ResponseModel<T>>(url, { headers: _headers }).subscribe({
        next: data => {
          sub.unsubscribe();
          resolve(data);
        },
        error: error => {
          var errStr = this.errorHandler.handle(error);
          reject(errStr);
        }
      });
    });

    return promise;
  }

  public post<T>(
    resource: string
    , body: any
    , headers?: HttpHeaders
    , contentType: string = 'application/json'
  ): Promise<ResponseModel<T>> {
    var _headers = this.configureHeaders(headers, contentType);
    var _body = this.configureBody(body);
    var url = this.configureResource(resource);

    var promise = new Promise<ResponseModel<T>>((resolve, reject) => {
      var sub = this.http.post<ResponseModel<T>>(url, _body, { headers: _headers }).subscribe({
        next: data => {
          sub.unsubscribe();
          resolve(data);
        },
        error: error => {
          var errStr = this.errorHandler.handle(error);
          reject(errStr);
        }
      });
    });

    return promise;
  }

  public delete<T>(
    resource: string
    , headers?: HttpHeaders
    , contentType: string = 'application/json'
  ): Observable<ResponseModel<T>> {
    var _headers = this.configureHeaders(headers, contentType);
    var url = this.configureResource(resource);

    return this.http.delete<ResponseModel<T>>(url, { headers: _headers });
  }

  private configureResource(resource: string) {
    if (!resource.startsWith("/"))
      resource = '/' + resource;

    return this.baseUrl + resource;
  }

  private configureHeaders(
    headers?: HttpHeaders
    , contentType: string = 'application/json'
  ) {
    if (!headers)
      headers = new HttpHeaders();

    headers = headers.set('Content-Type', contentType);
    headers = headers.set('Authorization', 'Bearer ' + this.tokenStorageService.getToken());

    return headers;
  }

  private configureBody(body: any) {
    var params = new HttpParams({ fromObject: body });;

    return params.toString();
  }
}
