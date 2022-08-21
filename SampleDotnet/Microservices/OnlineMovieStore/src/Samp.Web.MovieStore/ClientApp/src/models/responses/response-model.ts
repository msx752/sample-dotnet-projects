export interface BaseResponseModel {
  errors: string[];
  stats: ResponseStatModel;
}

export interface ResponseStatModel {
  rid: string;
  elapsedmilliseconds: string;
  offset: number;
}

export interface ResponseModel<T> extends BaseResponseModel {
  results: T[];
}
