import { Injectable, Inject } from '@angular/core';
import { ApiClientService } from '../api-client.service';
import { ResponseModel } from '../../models/responses/response-model';
import { TransactionDto } from '../../models/responses/payment/transaction';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  constructor(private api: ApiClientService
  ) { }

  getPaymentHistory(): Promise<ResponseModel<TransactionDto>> {
    return this.api.get<TransactionDto>('/Payments/History');
  }
  postPayment(cartid: string): Promise<ResponseModel<any>> {
    return this.api.post<TransactionDto>('/Payments/Create/' + cartid, null, null);
  }
}
