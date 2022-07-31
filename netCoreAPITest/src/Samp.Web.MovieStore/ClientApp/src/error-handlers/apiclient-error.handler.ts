import { Injectable } from "@angular/core";
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, throwError } from "rxjs";
import { catchError, map } from 'rxjs/operators';
import { Router } from "@angular/router";
import { PopupService } from "../services/popup.service";
import { ResponseModel } from "../models/responses/response-model";
import { SweetAlertResult } from "sweetalert2";

@Injectable({
  providedIn: 'root'
})
export class ApiClientErrorHandler {
  constructor(
    private popupService: PopupService) {
  }

  public handle(error: any): string {
    const responseModel = error.error as ResponseModel<any>;
    var errorStrList = "";
    if (responseModel.errors) {
      errorStrList = responseModel.errors.join(",\n");
    } else {
      errorStrList = 'given url not found.';
    }
    const title = error.statusText;
    const traceIdStringFormat = 'TraceId:&nbsp;<b>' + responseModel.stats.rid + '</b>';
    if (error.status >= 500) {
      this.popupService.show({
        icon: 'error',
        title: 'Something went wrong!',
        text: errorStrList,
        footer: 'please contact with supports, ' + traceIdStringFormat,
        showClass: {
          popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
          popup: 'animate__animated animate__fadeOutUp'
        }
      });
    } else if (error.status >= 400) {
      this.popupService.show({
        icon: 'error',
        title: title,
        text: errorStrList,
        footer: traceIdStringFormat,
        showClass: {
          popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
          popup: 'animate__animated animate__fadeOutUp'
        }
      });
    } else {
      this.popupService.show({
        icon: 'error',
        title: title,
        text: errorStrList,
        footer: traceIdStringFormat,
        showClass: {
          popup: 'animate__animated animate__fadeInDown'
        },
        hideClass: {
          popup: 'animate__animated animate__fadeOutUp'
        }
      });
    }

    return errorStrList;
  }
}
