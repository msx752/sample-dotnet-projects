import { UserDto } from "./user.dto";

export interface TokenDto {
  access_token: string;
  expiresat: string;
  refresh_token: string;
  refreshtokenexpiresat: string;
  user: UserDto;
}
