import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterEvent } from '@angular/router';
import { filter, Subscription } from 'rxjs';
import { ApiClientErrorHandler } from '../../../error-handlers/apiclient-error.handler';
import { MovieDto } from '../../../models/responses/movie/movie.dto';
import { MoviesApiService } from '../../../services/api/movies-api.service';

@Component({
  selector: 'movies-by-category',
  templateUrl: './movies-by-category.component.html',
})
export class MoviesByCategoryComponent implements OnInit, OnDestroy {
  title = 'Movies';
  categoryid: number;
  @Input()
  public movies: MovieDto[];
  private subscriptions: Subscription[] = [];
  public message: string;

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  constructor(private apiMovies: MoviesApiService
    , private errorHandler: ApiClientErrorHandler
    , private route: ActivatedRoute
    , private router: Router
  ) { }

  ngOnInit(): void {
    this.subscriptions.push(this.route.params.subscribe(params => {
      this.categoryid = +params['categoryid'];

      if (!this.movies) {
        this.subscriptions.push(this.apiMovies.GetFilteredByCategoryId(this.categoryid).subscribe({
          next: data => {
            if (data.results.length > 0) {
              this.movies = data.results;
            } else {
              this.message = 'no result..';
            }
          },
          error: error => {
            var errStr = this.errorHandler.handle(error);
          }
        }));
      }
    }));
  }
}
