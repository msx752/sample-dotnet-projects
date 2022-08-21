import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TransactionDto } from '../../models/responses/payment/transaction';
import { PaymentService } from '../../services/api/payment-api.service';
import { SessionStateService } from '../../services/session-state.service';

@Component({
  selector: 'payment-history',
  templateUrl: './payment-history.component.html',
})
export class PaymentHistoryComponent implements OnInit {
  title = 'Payment History';
  public transactions: TransactionDto[] = [];
  constructor(
    private paymentApi: PaymentService
    , private sessionState: SessionStateService
    , private router: Router

  ) {
  }
  ngOnInit(): void {
    this.transactions = [];
    if (this.sessionState.isLoggedIn()) {
      this.paymentApi.getPaymentHistory()
        .then((data) => {
          if (data.results && data.results.length > 0) {
            this.transactions = data.results;
          } else {
            this.transactions = [];
          }
        })
        .catch((error) => {
          this.transactions = [];
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
}
