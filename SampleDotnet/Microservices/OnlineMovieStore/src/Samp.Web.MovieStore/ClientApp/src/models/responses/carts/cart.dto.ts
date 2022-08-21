import { CartItemDto } from "./cart-item.dto";

export interface CartDto {
  id: string;
  status: string;
  items: CartItemDto[];
}
