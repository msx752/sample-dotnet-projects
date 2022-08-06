import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartDto } from '../../models/responses/carts/cart.dto';
import { CartApiService } from '../../services/api/cart-api.service';
import { SessionStateService } from '../../services/session-state.service';

@Component({
  selector: 'basket',
  templateUrl: './basket.component.html',
})
export class BasketComponent implements OnInit {
  title = 'Basket';
  public basket: CartDto;
  public calculatedTotalPrice: number = 0;
  constructor(
    private cartApi: CartApiService
    , private sessionState: SessionStateService
    , private router: Router

  ) {
  }
  ngOnInit(): void {
    this.calculatedTotalPrice = 0;
    if (this.sessionState.isLoggedIn()) {
      this.cartApi.getCart()
        .then((data) => {
          if (data.results && data.results.length > 0) {
            this.basket = data.results[0];
            this.basket.items.forEach((item) => {
              this.calculatedTotalPrice += item.salesprice;
            });
          } else {
            this.basket = null;
          }
        })
        .catch(() => {
          this.basket = null;
        });
    } else {
      this.router.navigate(['/']);
    }
  }
  public movieById(url: string) {
    if (url) {
      this.router.navigate([url]);
    }
  }
  public removeFromBasket(cartItem: string) {
    this.cartApi.deleteCartItem(this.sessionState.getCartId(), cartItem).then(() => {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
        this.router.navigate(['/basket'])
      );
    });
  }
}
