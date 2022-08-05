import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientService } from '../api-client.service';
import { ResponseModel } from '../../models/responses/response-model';
import { MovieDto } from '../../models/responses/movies/movie.dto';
import { MovieIndexViewModel } from '../../models/responses/movies/movie-index-view.model';

@Injectable({
  providedIn: 'root'
})
export class MoviesApiService {
  constructor(private api: ApiClientService
  ) { }

  getIndex(): Promise<ResponseModel<MovieIndexViewModel>> {
    return this.api.get<MovieIndexViewModel>('/Movies');
  }

  getHighRatings(): Promise<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/HighRatings');
  }

  getRecentlyAdded(): Promise<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/RecentlyAdded');
  }

  getById(id: string): Promise<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/' + id);
  }

  search(search: string): Promise<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/Search?query=' + search);
  }

  getFilteredByCategoryId(id: number): Promise<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/CategoryBy/' + id);
  }
}
