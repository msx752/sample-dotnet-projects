import { Component, Input, OnInit } from '@angular/core';
import { ApiClientErrorHandler } from '../../../error-handlers/apiclient-error.handler';
import { MovieDto } from '../../../models/responses/movie/movie.dto';
import { MoviesApiService } from '../../../services/api/movies-api.service';
import { PopupService } from '../../../services/popup.service';

@Component({
  selector: 'movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {
  @Input()
  public movies: MovieDto[] = [];
  @Input()
  public id: string = '';
  @Input()
  public class: string = '';
  @Input()
  public header: string = '';

  constructor(
    private apiMovies: MoviesApiService
    , private errorHandler: ApiClientErrorHandler
    , private popupService: PopupService
  ) { }

  ngOnInit(): void {
  }
}
