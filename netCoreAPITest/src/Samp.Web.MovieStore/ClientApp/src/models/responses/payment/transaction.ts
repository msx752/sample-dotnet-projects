import { TransactionItemDto } from "./transaction-item.dto";

export interface TransactionDto {
  id: string;
  userid: string;
  totalcalculatedprice: string;
  transactionitems: TransactionItemDto[];
}
