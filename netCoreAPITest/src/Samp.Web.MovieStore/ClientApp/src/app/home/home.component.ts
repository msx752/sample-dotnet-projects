import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ApiClientErrorHandler } from '../../error-handlers/apiclient-error.handler';
import { MovieIndexViewModel } from '../../models/responses/movie/movie-index-view.model';
import { MoviesApiService } from '../../services/api/movies-api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy {
  title = 'Home';
  public movieIndexModel: MovieIndexViewModel = { all: [], highratings: [], recentlyadded: [] };
  private subscriptions: Subscription[] = [];

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  constructor(private apiMovies: MoviesApiService
    , private errorHandler: ApiClientErrorHandler
  ) { }

  ngOnInit(): void {
    this.subscriptions.push(this.apiMovies.GetIndex().subscribe({
      next: data => {
        if (data.results.length > 0) {
          this.movieIndexModel = data.results[0];
        }
      },
      error: error => {
        var errStr = this.errorHandler.handle(error);
      }
    }));
  }
}
