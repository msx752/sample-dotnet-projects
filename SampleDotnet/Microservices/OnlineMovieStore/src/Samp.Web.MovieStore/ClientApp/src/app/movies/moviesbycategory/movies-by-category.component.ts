import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterEvent } from '@angular/router';
import { Subscription } from 'rxjs';
import { MovieDto } from '../../../models/responses/movies/movie.dto';
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
    , private route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.subscriptions.push(this.route.params.subscribe(params => {
      this.categoryid = +params['categoryid'];

      if (!this.movies) {
        this.apiMovies.getFilteredByCategoryId(this.categoryid)
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
