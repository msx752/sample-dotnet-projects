import { Component, Input, OnInit } from '@angular/core';
import { ApiClientErrorHandler } from '../../error-handlers/apiclient-error.handler';
import { CategoryDto } from '../../models/responses/movie/category-dto';
import { MovieCatagoriesApiService } from '../../services/api/movie-category-api';
import { TokenStorageService } from '../../services/token-storage.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  constructor(
    private apiMovieCategories: MovieCatagoriesApiService
    , private errorHandler: ApiClientErrorHandler
    , public tokenStorage: TokenStorageService
  ) { }

  public categories: CategoryDto[] = [];

  ngOnInit(): void {
    this.apiMovieCategories.GetCategories().subscribe({
      next: data => {
        if (data.results.length > 0) {
          this.categories = data.results;
        }
      },
      error: error => {
        var errStr = this.errorHandler.handle(error);
      }
    });
  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
