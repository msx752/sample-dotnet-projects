import { MovieDto } from "./movie.dto";
import { WriterDto } from "./writer.dto";

export interface MovieWriterDto {
  movieid: string;
  //movie: MovieDto;
  writerid: string;
  writer: WriterDto;
}
