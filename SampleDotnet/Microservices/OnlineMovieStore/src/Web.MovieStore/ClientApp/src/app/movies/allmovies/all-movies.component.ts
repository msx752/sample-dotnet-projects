import { Component, Input, OnInit } from '@angular/core';
import { MovieDto } from '../../../models/responses/movies/movie.dto';

@Component({
  selector: 'all-movies',
  templateUrl: './all-movies.component.html',
})
export class AllMoviesComponent {
  title = 'Movies';
  @Input()
  public movies: MovieDto[] = [];

  constructor() { }
}
