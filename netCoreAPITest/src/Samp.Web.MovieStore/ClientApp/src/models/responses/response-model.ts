export interface BaseResponseModel {
  Errors: string[];
  Stats: ResponseStatModel;
}

export interface ResponseStatModel {
  RId: string;
  ElapsedMilliseconds: string;
  Offset: number;
}

export interface ResponseModel<T> extends BaseResponseModel {
  Results: T[];
}
