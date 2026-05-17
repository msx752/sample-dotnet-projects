import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenStorageService } from './token-storage.service';
import { ResponseModel } from '../models/responses/response-model';
import { PopupService } from './popup.service';
import { Constants } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class ApiClientService {
  constructor(private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string
    , private tokenStorageService: TokenStorageService
    , private popupService: PopupService
  ) { }

  public get<T>(
    resource: string
    , headers?: HttpHeaders
    , contentType: string = Constants.applicationjson
  ): Promise<ResponseModel<T>> {
    var _headers = this.configureHeaders(headers, contentType);
    var url = this.configureResource(resource);

    return this.handleResponse<T>(
      this.http.get<ResponseModel<T>>(url, { headers: _headers })
    );
  }

  public post<T>(
    resource: string
    , body: any
    , headers?: HttpHeaders
    , contentType: string = Constants.applicationjson
  ): Promise<ResponseModel<T>> {
    var _headers = this.configureHeaders(headers, contentType);
    var _body = this.configureBody(body, contentType);
    var url = this.configureResource(resource);

    return this.handleResponse<T>(
      this.http.post<ResponseModel<T>>(url, _body, { headers: _headers })
    );
  }

  public delete<T>(
    resource: string
    , headers?: HttpHeaders
    , contentType: string = Constants.applicationjson
  ): Promise<ResponseModel<T>> {
    var _headers = this.configureHeaders(headers, contentType);
    var url = this.configureResource(resource);

    return this.handleResponse<T>(
      this.http.delete<ResponseModel<T>>(url, { headers: _headers })
    );
  }

  private configureResource(resource: string) {
    if (!resource.startsWith("/"))
      resource = '/' + resource;

    return this.baseUrl + resource;
  }

  private configureHeaders(
    headers?: HttpHeaders
    , contentType: string = Constants.applicationjson
  ) {
    if (!headers)
      headers = new HttpHeaders();

    headers = headers.set('Content-Type', contentType);
    headers = headers.set('Authorization', 'Bearer ' + this.tokenStorageService.getToken());

    return headers;
  }

  private configureBody(body: any, contentType: string): any {
    if (!body) {
      return null;
    }

    if (contentType == Constants.applicationjson) {
      return JSON.stringify(body);
    } else if (contentType == Constants.xwwwfromurlencoded) {
      return new HttpParams({ fromObject: body }).toString();
    } else {
      throw new Error(`Content type not implemented: ${contentType}`);
    }
  }

  private handleResponse<T>(httpRequest: Observable<ResponseModel<T>>) {
    var promise = new Promise<ResponseModel<T>>((resolve, reject) => {
      var sub = httpRequest.subscribe({
        next: data => {
          sub.unsubscribe();
          resolve(data);
        },
        error: error => {
          var errStr = this.handleError(error);
          reject(errStr);
        }
      });
    });

    return promise;
  }

  private handleError(error: any): string {
    const responseModel = error.error as ResponseModel<any>;
    const errorStrList = responseModel?.errors?.join(",\n") || 'Given URL not found.';
    const traceId = responseModel?.stats?.rid || 'unknown';
    const traceIdFormat = `TraceId:&nbsp;<b>${traceId}</b>`;

    // Handle 401 Unauthorized - clear token and redirect to login
    if (error.status === 401) {
      this.tokenStorageService.logout();
      this.popupService.showDialog(
        'Session Expired',
        'Your session has expired. Please login again.',
        'warning',
        'Login',
        undefined,
        () => { window.location.href = '/login'; }
      );
      return errorStrList;
    }

    const isServerError = error.status >= 500;
    const title = isServerError ? 'Something went wrong!' : error.statusText;
    const footer = isServerError ? `Please contact support, ${traceIdFormat}` : traceIdFormat;

    this.popupService.show({
      icon: 'error',
      title: title,
      text: errorStrList,
      footer: footer,
      showClass: { popup: 'animate__animated animate__fadeInDown' },
      hideClass: { popup: 'animate__animated animate__fadeOutUp' }
    });

    return errorStrList;
  }
}
