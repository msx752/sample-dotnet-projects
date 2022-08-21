import { CategoryDto } from "./category-dto";
import { MovieDto } from "./movie.dto";

export interface MovieCategoryDto {
  movieid: string;
  //movie: MovieDto;
  categoryid: number;
  category: CategoryDto;
}
