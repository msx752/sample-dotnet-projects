import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientService } from '../apiclient.service';
import { ResponseModel } from '../../models/responses/response-model';
import { CategoryDto } from '../../models/responses/movies/category-dto';

@Injectable({
  providedIn: 'root'
})
export class MovieCatagoriesApiService {
  constructor(private api: ApiClientService
  ) { }

  getCategories(): Promise<ResponseModel<CategoryDto>> {
    return this.api.get<CategoryDto>('/Movies/Categories');
  }
}
