import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { MovieIndexViewModel } from '../../models/responses/movies/movie-index-view.model';
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
  ) { }

  ngOnInit(): void {
    this.apiMovies.getIndex()
      .then((data) => {
        if (data.results.length > 0) {
          this.movieIndexModel = data.results[0];
        }
      })
      .catch((error) => {
      });
  }
}
