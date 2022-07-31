import { MovieDto } from "./movie.dto";

export interface MovieIndexViewModel {
  highratings: MovieDto[];
  all: MovieDto[];
  recentlyadded: MovieDto[];
}
