import { Component, Input, OnInit, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { ApiClientErrorHandler } from '../../../error-handlers/apiclient-error.handler';
import { MovieDto } from '../../../models/responses/movies/movie.dto';
import { MoviesApiService } from '../../../services/api/movies-api.service';
import { PopupService } from '../../../services/popup.service';

@Component({
  selector: 'movies',
  templateUrl: './movies.component.html',
  styleUrls: ['./movies.component.css']
})
export class MoviesComponent implements OnInit {
  @Input()
  public movies: MovieDto[] = [];
  @Input()
  public id: string = '';
  @Input()
  public class: string = '';
  @Input()
  public header: string = '';

  constructor(
    private apiMovies: MoviesApiService
    , private errorHandler: ApiClientErrorHandler
    , private popupService: PopupService
    , private renderer: Renderer2
    , private router: Router
  ) { }

  ngOnInit(): void {
  }

  public cardBlockMouseover(event) {
    //console.log("show");
    var element = this.renderer.parentNode(event);
    var viewDetail = element.getElementsByClassName("view-detail")[0];
    if (viewDetail) {
      viewDetail.style.display = "block";
    }
  }
  public cardBlockMouseout(event) {
    //console.log("hide");
    var element = this.renderer.parentNode(event);
    var viewDetail = element.getElementsByClassName("view-detail")[0];
    if (viewDetail) {
      viewDetail.style.display = "none";
    }
  }

  drawStars(starIndex: number, averagerating: number): string {
    var n1 = (averagerating / 100);
    var n2 = (n1 / 2);
    var n3 = (n2 * 10);
    var stars = Math.round(n3);
    if (starIndex <= stars) {
      return 'fa fa-star star-checked';
    } else {
      return 'fa fa-star ';
    }
  }
  public movieById(url: string) {
    if (url) {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
        this.router.navigate([url])
      );
    }
  }
}
