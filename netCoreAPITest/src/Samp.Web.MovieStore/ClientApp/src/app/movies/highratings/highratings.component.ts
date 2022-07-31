import { Component, Input, OnInit } from '@angular/core';
import { ApiClientErrorHandler } from '../../../error-handlers/apiclient-error.handler';
import { MovieDto } from '../../../models/responses/movie/movie.dto';
import { MoviesApiService } from '../../../services/api/movies-api.service';

@Component({
  selector: 'high-ratings-movies',
  templateUrl: './highratings.component.html',
})
export class HighRatingsComponent implements OnInit {
  title = 'High Ratings';
  @Input()
  public movies: MovieDto[];

  constructor(private apiMovies: MoviesApiService
    , private errorHandler: ApiClientErrorHandler
  ) { }

  ngOnInit(): void {
    if (!this.movies) {
      this.apiMovies.GetHighRatings().subscribe({
        next: data => {
          if (data.results.length > 0) {
            this.movies = data.results;
          }
        },
        error: error => {
          var errStr = this.errorHandler.handle(error);
        }
      });
    }
  }
}
