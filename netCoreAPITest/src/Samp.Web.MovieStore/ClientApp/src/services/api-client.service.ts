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
    if (contentType == Constants.applicationjson) {
      return JSON.stringify(body);
    } else if (contentType == Constants.xwwwfromurlencoded) {
      return new HttpParams({ fromObject: body }).toString();
    } else {
      throw "not implemented " + contentType;
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
    var errorStrList = "";
    if (responseModel.errors) {
      errorStrList = responseModel.errors.join(",\n");
    } else {
      errorStrList = 'given url not found.';
    }
    const title = error.statusText;
    const traceIdStringFormat = 'TraceId:&nbsp;<b>' + responseModel.stats.rid + '</b>';
    if (error.status >= 500) {
      this.popupService.show({
        icon: 'error',
        title: 'Something went wrong!',
        text: errorStrList,
        footer: 'please contact with supports, ' + traceIdStringFormat,
        showClass: {
          popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
          popup: 'animate__animated animate__fadeOutUp'
        }
      });
    } else if (error.status >= 400) {
      this.popupService.show({
        icon: 'error',
        title: title,
        text: errorStrList,
        footer: traceIdStringFormat,
        showClass: {
          popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
          popup: 'animate__animated animate__fadeOutUp'
        }
      });
    } else {
      this.popupService.show({
        icon: 'error',
        title: title,
        text: errorStrList,
        footer: traceIdStringFormat,
        showClass: {
          popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
          popup: 'animate__animated animate__fadeOutUp'
        }
      });
    }

    return errorStrList;
  }
}
