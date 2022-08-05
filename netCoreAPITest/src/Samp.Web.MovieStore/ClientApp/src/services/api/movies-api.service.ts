import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientService } from '../apiclient.service';
import { ResponseModel } from '../../models/responses/response-model';
import { MovieDto } from '../../models/responses/movies/movie.dto';
import { MovieIndexViewModel } from '../../models/responses/movies/movie-index-view.model';

@Injectable({
  providedIn: 'root'
})
export class MoviesApiService {
  constructor(private api: ApiClientService
  ) { }

  GetIndex(): Promise<ResponseModel<MovieIndexViewModel>> {
    return this.api.get<MovieIndexViewModel>('/Movies');
  }

  GetHighRatings(): Promise<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/HighRatings');
  }

  GetRecentlyAdded(): Promise<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/RecentlyAdded');
  }

  GetById(id: string): Promise<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/' + id);
  }

  Search(search: string): Promise<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/Search?query=' + search);
  }

  GetFilteredByCategoryId(id: number): Promise<ResponseModel<MovieDto>> {
    return this.api.get<MovieDto>('/Movies/CategoryBy/' + id);
  }
}
