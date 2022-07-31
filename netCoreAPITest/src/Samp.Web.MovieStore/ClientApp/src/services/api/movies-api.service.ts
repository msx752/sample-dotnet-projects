import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiClientService } from '../apiclient.service';
import { ResponseModel } from '../../models/responses/response-model';
import { TokenDto } from '../../models/responses/identity/token.dto';
import { MovieDto } from '../../models/responses/movie/movie.dto';

@Injectable({
  providedIn: 'root'
})
export class MoviesApiService {
  constructor(private api: ApiClientService
  ) { }

  GetIndex(): Observable<ResponseModel<MovieDto>> {
    return this.api.get<any>('/Movies/Index');
  }

  GetHighRatings(): Observable<ResponseModel<MovieDto>> {
    return this.api.get<any>('/Movies/HighRatings');
  }

  RecentlyAdded(): Observable<ResponseModel<MovieDto>> {
    return this.api.get<any>('/Movies/RecentlyAdded');
  }

  GetById(id: number): Observable<ResponseModel<MovieDto>> {
    return this.api.get<any>('/Movies/' + id);
  }

  Search(search: string): Observable<ResponseModel<MovieDto>> {
    return this.api.get<any>('/Movies/Search?query=' + search);
  }

  GetFilteredByCategoryId(id: number): Observable<ResponseModel<MovieDto>> {
    return this.api.get<any>('/Movies/CategoryBy/' + id);
  }
}
