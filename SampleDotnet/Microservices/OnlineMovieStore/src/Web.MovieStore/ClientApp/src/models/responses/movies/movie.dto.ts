import { MovieCategoryDto } from "./movie-category.dto";
import { MovieDirectorDto } from "./movie-director.dto";
import { MovieWriterDto } from "./movie-writer.dto";
import { RatingDto } from "./rating.dto";

export interface MovieDto {
  categories: MovieCategoryDto[];
  moviedirectors: MovieDirectorDto[];
  moviewriters: MovieWriterDto[];
  id: string;
  title: string;
  runtimeminutes: number;
  startyear: number;
  description: string;
  rating: RatingDto;
  type: string;
  usdprice: number;
  itemdatabase: string;
}
