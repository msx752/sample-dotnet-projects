import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { IdentityService } from '../../services/api/identity.service';
import { PopupService } from '../../services/popup.service';
import { TokenStorageService } from '../../services/token-storage.service';
import Swal from 'sweetalert2';
import { ApiClientErrorHandler } from '../../error-handlers/apiclient-error.handler';
import { Subscription } from 'rxjs';
import { SessionStateService } from '../../services/session-state.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  form: any = {
    username: null,
    password: null
  };
  isLoggedIn = false;
  isLoginFailed = false;
  errorMessage = '';
  roles: string[] = [];
  username: string = '';
  inProgressLoginButton: boolean = false;
  private subscriptions: Subscription[] = [];

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  constructor(private identityService: IdentityService
    , private tokenStorage: TokenStorageService
    , @Inject('BASE_URL') private baseUrl: string
    , private popupService: PopupService
    , private errorHandler: ApiClientErrorHandler
    , private sessionStateService: SessionStateService
    , private tokenStorageService: TokenStorageService
  ) { }

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.username = this.tokenStorage.getUser().username;
    }
  }

  onSubmit(): void {
    this.inProgressLoginButton = true;

    const { username, password } = this.form;

    this.identityService.login(username, password).then((data) => {
      this.tokenStorage.saveToken(data.results[0].access_token);
      this.tokenStorage.saveUser(data.results[0].user);

      this.isLoginFailed = false;
      this.isLoggedIn = true;
      this.username = this.tokenStorage.getUser().username;
      this.inProgressLoginButton = false;

      this.sessionStateService.refreshUserCart().then(() => {
        this.reloadPage();
      });
    }).catch((error) => {
      this.tokenStorageService.removeCartId();
      this.isLoginFailed = true;
      this.errorMessage = error;

      this.inProgressLoginButton = false;
      this.tokenStorage.logout();
    });
  }

  reloadPage() {
    window.location.reload();
  }
}
