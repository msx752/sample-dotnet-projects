import { DirectorDto } from "./director.dto";
import { MovieDto } from "./movie.dto";

export interface MovieDirectorDto {
  movieid: string;
  //movie: MovieDto;
  directorid: string;
  director: DirectorDto;
}
