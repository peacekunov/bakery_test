import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BakingComponent } from './baking/baking.component';
import { ShelfComponent } from './shelf/shelf.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, BakingComponent, ShelfComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'frontend';
}
