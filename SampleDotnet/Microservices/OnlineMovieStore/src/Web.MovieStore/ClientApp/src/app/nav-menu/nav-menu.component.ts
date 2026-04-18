import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
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

  public searchInput: string = '';
  public categories: CategoryDto[] = [];
  public menuItems: MenuItem[] = [];
  public cartCount: string = '0';
  private subscriptions: Subscription[] = [];

  ngOnDestroy(): void {
    this.subscriptions.forEach(sub => sub.unsubscribe());
  }

  ngOnInit(): void {
    this.buildMenuItems();
    this.apiMovieCategories.getCategories()
      .then((data) => {
        if (data.results.length > 0) {
          this.categories = data.results;
          this.buildMenuItems();
        }
      })
      .catch((error) => {
        console.error('Failed to load categories:', error);
      });
  }

  private buildMenuItems(): void {
    const categoryItems: MenuItem[] = this.categories.map(cat => ({
      label: cat.name,
      icon: 'pi pi-tag',
      command: () => this.moviesByCategory('/category/' + cat.id)
    }));

    this.menuItems = [
      {
        label: 'Home',
        icon: 'pi pi-home',
        routerLink: ['/']
      },
      {
        label: 'Categories',
        icon: 'pi pi-list',
        items: categoryItems.length > 0 ? categoryItems : [{ label: 'Loading...', disabled: true }]
      },
      {
        label: 'High Ratings',
        icon: 'pi pi-star',
        routerLink: ['/highratings']
      },
      {
        label: 'Recently Added',
        icon: 'pi pi-clock',
        routerLink: ['/recentlyadded']
      }
    ];
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
