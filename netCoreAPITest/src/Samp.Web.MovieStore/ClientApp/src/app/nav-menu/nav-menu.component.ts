import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ApiClientErrorHandler } from '../../error-handlers/apiclient-error.handler';
import { CategoryDto } from '../../models/responses/movie/category-dto';
import { MovieCatagoriesApiService } from '../../services/api/movie-category-api';
import { TokenStorageService } from '../../services/token-storage.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit, OnDestroy {
  constructor(
    private apiMovieCategories: MovieCatagoriesApiService
    , private errorHandler: ApiClientErrorHandler
    , public tokenStorage: TokenStorageService
    , private router: Router
  ) { }

  public searchInput: string;
  public categories: CategoryDto[] = [];
  private subscriptions: Subscription[] = [];

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  ngOnInit(): void {
    this.subscriptions.push(this.apiMovieCategories.GetCategories().subscribe({
      next: data => {
        if (data.results.length > 0) {
          this.categories = data.results;
        }
      },
      error: error => {
        var errStr = this.errorHandler.handle(error);
      }
    }));
  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  public search() {
    if (this.searchInput) {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
        var navigationExtras = { queryParams: { q: this.searchInput } };
        this.searchInput = '';
        this.router.navigate(['/search'], navigationExtras);
      });
    }
  }
  public moviesByCategory(url: string) {
    if (url) {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
        this.router.navigate([url])
      );
    }
  }
}
