import { Component, Input, OnInit } from '@angular/core';
import { MovieDto } from '../../../models/responses/movie/movie.dto';

@Component({
  selector: 'all-movies',
  templateUrl: './allmovies.component.html',
})
export class AllMoviesComponent {
  title = 'Movies';
  @Input()
  public movies: MovieDto[] = [];

  constructor() { }
}
