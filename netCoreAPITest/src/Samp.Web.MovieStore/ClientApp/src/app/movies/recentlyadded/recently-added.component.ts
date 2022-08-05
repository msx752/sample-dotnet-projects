import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ApiClientErrorHandler } from '../../../error-handlers/apiclient-error.handler';
import { MovieDto } from '../../../models/responses/movies/movie.dto';
import { MoviesApiService } from '../../../services/api/movies-api.service';

@Component({
  selector: 'recently-added-movies',
  templateUrl: './recently-added.component.html',
})
export class RecentlyAddedComponent implements OnInit, OnDestroy {
  title = 'Recently Added'
  @Input()
  public movies: MovieDto[];
  private subscriptions: Subscription[] = [];

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  constructor(private apiMovies: MoviesApiService
    , private errorHandler: ApiClientErrorHandler
  ) { }

  ngOnInit(): void {
    if (!this.movies) {
      this.apiMovies.getRecentlyAdded()
        .then((data) => {
          if (data.results.length > 0) {
            this.movies = data.results;
          }
        })
        .catch((error) => {
        });
    }
  }
}
