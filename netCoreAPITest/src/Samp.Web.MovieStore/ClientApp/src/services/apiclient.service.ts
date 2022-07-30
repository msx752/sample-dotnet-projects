import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenStorageService } from '../services/token-storage.service';

@Injectable({
  providedIn: 'root'
})
export class ApiClientService {
  constructor(private http: HttpClient
    , @Inject('BASE_URL') private baseUrl: string
    , private tokenStorageService: TokenStorageService
  ) { }

  public get(
    resource: string
    , headers?: HttpHeaders
    , contentType: string = 'application/json'
  ): Observable<any> {
    this.configureHeaders(headers, contentType);

    var requestUrl = this.configureResource(resource);
    return this.http.get(requestUrl, { headers: headers });
  }

  public post(
    resource: string
    , body: any
    , headers?: HttpHeaders
    , contentType: string = 'application/json'
  ): Observable<any> {
    this.configureHeaders(headers, contentType);

    var requestUrl = this.configureResource(resource);
    return this.http.post(requestUrl, this.configureBody(body), { headers: headers });
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

    headers.set('Content-Type', contentType);
    headers.set('Authorization', 'Bearer ' + this.tokenStorageService.getToken());
  }

  private configureBody(body: any) {
    var params = new HttpParams({ fromObject: body });;

    return params.toString();
  }
}
