import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login.component';
import { MoviesComponent } from './components/movies/movies.component';
import { HighRatingsComponent } from './movies/highratings/high-ratings.component';
import { RecentlyAddedComponent } from './movies/recentlyadded/recently-added.component';
import { AllMoviesComponent } from './movies/allmovies/all-movies.component';
import { SearchMoviesComponent } from './movies/searchmovies/search-movies.component';
import { MoviesByCategoryComponent } from './movies/moviesbycategory/movies-by-category.component';
import { MovieDetailComponent } from './movies/moviedetail/movie-detail.component';
import { BasketComponent } from './basket/basket.component';
import { PaymentHistoryComponent } from './paymenthistory/payment-history.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    MoviesComponent,
    HighRatingsComponent,
    RecentlyAddedComponent,
    AllMoviesComponent,
    SearchMoviesComponent,
    MoviesByCategoryComponent,
    MovieDetailComponent,
    BasketComponent,
    PaymentHistoryComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'highratings', component: HighRatingsComponent },
      { path: 'recentlyadded', component: RecentlyAddedComponent },
      { path: 'category/:categoryid', component: MoviesByCategoryComponent, },
      { path: 'movie/:movieid', component: MovieDetailComponent, },
      { path: 'search', component: SearchMoviesComponent, },
      { path: 'login', component: LoginComponent },
      { path: 'basket', component: BasketComponent },
      { path: 'payment-history', component: PaymentHistoryComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
