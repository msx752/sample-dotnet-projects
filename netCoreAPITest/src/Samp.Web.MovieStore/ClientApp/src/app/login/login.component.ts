import { Component, OnInit, Inject } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { PopupService } from '../../services/popup.service';
import { TokenStorageService } from '../../services/token-storage.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
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

  constructor(private authService: AuthService,
    private tokenStorage: TokenStorageService,
    @Inject('BASE_URL') private baseUrl: string,
    private popupService: PopupService
  ) { }

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.username = this.tokenStorage.getUser().Username;
    }
  }

  onSubmit(): void {

    this.inProgressLoginButton = true;

    const { username, password } = this.form;

    this.authService.login(username, password).subscribe({
      next: data => {
        this.tokenStorage.saveToken(data.results[0].accessToken);
        this.tokenStorage.saveUser(data.results[0].user);

        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.username = this.tokenStorage.getUser().Username;
        this.inProgressLoginButton = false;
        this.reloadPage();
      },
      error: err => {
        this.isLoginFailed = true;
        this.popupService.showError('Login failed', err.error.message);

        this.inProgressLoginButton = false;
      }
    });
  }

  reloadPage(): void {
    window.location.reload();
  }
}
