import { Component, Input, OnInit, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
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
    , private renderer: Renderer2
    , private router: Router
  ) { }

  ngOnInit(): void {
  }

  public cardBlockMouseover(event: EventTarget | null): void {
    if (!event) return;
    const element = this.renderer.parentNode(event);
    const viewDetail = element?.getElementsByClassName("view-detail")[0] as HTMLElement;
    if (viewDetail) {
      viewDetail.style.display = "block";
    }
  }

  public cardBlockMouseout(event: EventTarget | null): void {
    if (!event) return;
    const element = this.renderer.parentNode(event);
    const viewDetail = element?.getElementsByClassName("view-detail")[0] as HTMLElement;
    if (viewDetail) {
      viewDetail.style.display = "none";
    }
  }

  drawStars(starIndex: number, averagerating: number): string {
    const stars = Math.round(averagerating / 20); // 0-100 → 0-5 stars
    return starIndex <= stars ? 'fa fa-star star-checked' : 'fa fa-star';
  }
  public movieById(url: string) {
    if (url) {
      this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
        this.router.navigate([url])
      );
    }
  }
}
