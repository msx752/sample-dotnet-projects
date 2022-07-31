import { RatingDto } from "./rating.dto";

export interface MovieDto {
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
