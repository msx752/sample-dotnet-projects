import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiClientService } from '../apiclient.service';
import { ResponseModel } from '../../models/responses/response-model';
import { CategoryDto } from '../../models/responses/movies/category-dto';
import { CartDto } from '../../models/responses/carts/cart.dto';
import { CartItemDto } from '../../models/responses/carts/cart-item.dto';

@Injectable({
  providedIn: 'root'
})
export class CartApiService {
  constructor(private api: ApiClientService
  ) { }

  getCart(): Promise<ResponseModel<CartDto>> {
    return this.api.get<CartDto>('/Carts');
  }

  postCartItem(cartid: string, productid: string, productdatabase: string): Promise<ResponseModel<CartItemDto>> {
    return this.api.post<CartItemDto>('/Carts/' + cartid, { productid: productid, productdatabase: productdatabase });
  }

  deleteCartItem(cartid: string, cartitemid: string): Promise<ResponseModel<any>> {
    return this.api.delete<any>('/Carts/' + cartid + '/Item/' + cartitemid);
  }
}
