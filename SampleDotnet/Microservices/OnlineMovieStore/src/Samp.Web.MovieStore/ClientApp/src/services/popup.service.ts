import { Injectable, Inject } from '@angular/core';
import { SweetAlertIcon, SweetAlertOptions } from 'sweetalert2';
import Swal from 'sweetalert2/dist/sweetalert2.js';

@Injectable({
  providedIn: 'root'
})
export class PopupService {
  constructor() { }

  public showError(title: string, htmlMessage: string) {
    return Swal.fire(title, htmlMessage, 'error');
  }

  public showSuccess(title: string, htmlMessage: string) {
    return Swal.fire(title, htmlMessage, 'success');
  }

  public showWarning(title: string, htmlMessage: string) {
    return Swal.fire(title, htmlMessage, 'warning');
  }

  public showDailog(title: string
    , text: string
    , icon: SweetAlertIcon = 'warning'
    , confirmButtonText: string = 'Okay'
    , cancelButtonText?: string
    , onConfirm?
    , onCancel?
  ) {
    return this.show({
      title: title,
      text: text,
      icon: icon,
      showCancelButton: !cancelButtonText ? false : true,
      confirmButtonText: confirmButtonText,
      cancelButtonText: cancelButtonText,
    },
      function (result) {
        if (result.value) {
          if (onConfirm)
            onConfirm(result);
        } else if (result.dismiss === Swal.DismissReason.cancel) {
          if (onCancel && cancelButtonText)
            onCancel(result);
        }
      });
  }

  public show(options: SweetAlertOptions
    , onResult?
  ) {
    if (!options.confirmButtonText)
      options.confirmButtonText = 'Okay';

    return Swal.fire(options)
      .then((result) => {
        if (onResult)
          onResult(result);
      });
  }
}
