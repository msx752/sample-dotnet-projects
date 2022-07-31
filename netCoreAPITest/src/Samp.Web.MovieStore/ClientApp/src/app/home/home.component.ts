import { Component, OnInit } from '@angular/core';
import { ApiClientErrorHandler } from '../../error-handlers/apiclient-error.handler';
import { MovieIndexViewModel } from '../../models/responses/movie/movie-index-view.model';
import { MoviesApiService } from '../../services/api/movies-api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  title = 'Home';
  public movieIndexModel: MovieIndexViewModel = { all: [], highratings: [], recentlyadded:[] };

  constructor(private apiMovies: MoviesApiService
    , private errorHandler: ApiClientErrorHandler
  ) { }

  ngOnInit(): void {
    this.apiMovies.GetIndex().subscribe({
      next: data => {
        if (data.results.length > 0) {
          this.movieIndexModel = data.results[0];
        }
      },
      error: error => {
        var errStr = this.errorHandler.handle(error);
      }
    });
  }
}
