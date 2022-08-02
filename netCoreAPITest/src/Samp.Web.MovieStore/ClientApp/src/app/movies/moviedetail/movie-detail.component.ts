import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterEvent } from '@angular/router';
import { filter, Subscription } from 'rxjs';
import { ApiClientErrorHandler } from '../../../error-handlers/apiclient-error.handler';
import { MovieDto } from '../../../models/responses/movie/movie.dto';
import { MoviesApiService } from '../../../services/api/movies-api.service';
import { TokenStorageService } from '../../../services/token-storage.service';

@Component({
  selector: 'movie-detail',
  templateUrl: './movie-detail.component.html',
})
export class MovieDetailComponent implements OnInit, OnDestroy {
  title = '';
  movieid: string;
  public movie: MovieDto;
  private subscriptions: Subscription[] = [];
  public categories: string = "todo";
  public directors: string = "todo";
  public writers: string = "todo";

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  constructor(private apiMovies: MoviesApiService
    , private errorHandler: ApiClientErrorHandler
    , public tokenStorage: TokenStorageService
    , private route: ActivatedRoute
    , private router: Router
  ) { }

  ngOnInit(): void {
    this.subscriptions.push(this.route.params.subscribe(params => {
      this.movieid = params['movieid'];

        this.subscriptions.push(this.apiMovies.GetById(this.movieid).subscribe({
          next: data => {
            if (data.results.length > 0) {
              this.movie = data.results[0];
            }
          },
          error: error => {
            var errStr = this.errorHandler.handle(error);
          }
        }));
    }));
  }
}
