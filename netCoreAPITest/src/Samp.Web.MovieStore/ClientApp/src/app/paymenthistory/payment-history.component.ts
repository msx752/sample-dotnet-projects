import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CartApiService } from '../../services/api/cart-api.service';
import { SessionStateService } from '../../services/session-state.service';

@Component({
  selector: 'payment-history',
  templateUrl: './payment-history.component.html',
})
export class PaymentHistoryComponent implements OnInit {
  title = 'Payment History';

  constructor(
    private cartApi: CartApiService
    , private sessionState: SessionStateService
    , private router: Router

  ) {
  }
  ngOnInit(): void {
  }
}
