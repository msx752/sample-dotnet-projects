import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryDto } from '../../models/responses/movies/category-dto';
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
    this.apiMovieCategories.getCategories()
      .then((data) => {
        if (data.results.length > 0) {
          this.categories = data.results;
        }
      })
      .catch((error) => {
      });
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
