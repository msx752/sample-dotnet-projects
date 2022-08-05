import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ApiClientErrorHandler } from '../../../error-handlers/apiclient-error.handler';
import { MovieDto } from '../../../models/responses/movies/movie.dto';
import { MoviesApiService } from '../../../services/api/movies-api.service';

@Component({
  selector: 'search-movies',
  templateUrl: './search-movies.component.html',
})
export class SearchMoviesComponent implements OnInit, OnDestroy {
  title = 'Movies';
  @Input()
  public movies: MovieDto[] = [];
  public message: string;
  private subscriptions: Subscription[] = [];

  constructor(
    private route: ActivatedRoute
    , private apiMovies: MoviesApiService
    , private errorHandler: ApiClientErrorHandler
  ) {
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  ngOnInit(): void {
    this.subscriptions.push(this.route.queryParams.subscribe(params => {
      if (params.q) {
        this.apiMovies.search(params.q)
          .then((data) => {
            if (data.results.length > 0) {
              this.movies = data.results;
            } else {
              this.message = 'no result..';
            }
          })
          .catch((error) => {
          });
      }
    }));
  }
}
