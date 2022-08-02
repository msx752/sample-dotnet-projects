import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientService } from '../apiclient.service';
import { ResponseModel } from '../../models/responses/response-model';
import { MovieDto } from '../../models/responses/movie/movie.dto';
import { MovieIndexViewModel } from '../../models/responses/movie/movie-index-view.model';

@Injectable({
  providedIn: 'root'
})
export class MoviesApiService {
  constructor(private api: ApiClientService
  ) { }

  GetIndex(): Observable<ResponseModel<MovieIndexViewModel>> {
    return this.api.get<MovieIndexViewModel>('/Movies');
  }

  GetHighRatings(): Observable<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/HighRatings');
  }

  GetRecentlyAdded(): Observable<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/RecentlyAdded');
  }

  GetById(id: string): Observable<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/' + id);
  }

  Search(search: string): Observable<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/Search?query=' + search);
  }

  GetFilteredByCategoryId(id: number): Observable<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/CategoryBy/' + id);
  }
}
