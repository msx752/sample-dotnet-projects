import { UserDto } from "./user.dto";

export interface TokenDto {
  access_token: string;
  ExpiresAt: string;
  refresh_token: string;
  RefreshTokenExpiresAt: string;
  User: UserDto;
}
