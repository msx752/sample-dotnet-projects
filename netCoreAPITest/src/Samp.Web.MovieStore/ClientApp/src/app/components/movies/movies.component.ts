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
  fetchType: string;

  public movies: MovieDto[] = [];
  constructor(
    private apiMovies: MoviesApiService
    , private errorHandler: ApiClientErrorHandler
    , private popupService: PopupService
  ) { }

  ngOnInit(): void {
  }

  public GetIndex() {
  }

  public GetHighRatings() {
    this.movies = [];
    this.apiMovies.GetHighRatings().subscribe({
      next: data => {
        this.movies = data.results;
      },
      error: error => {
        var errStr = this.errorHandler.handle(error);
      }
    });
  }

  public RecentlyAdded() {
    this.movies = [];
    this.apiMovies.RecentlyAdded().subscribe({
      next: data => {
        this.movies = data.results;
      },
      error: error => {
        var errStr = this.errorHandler.handle(error);
      }
    });
  }
}
