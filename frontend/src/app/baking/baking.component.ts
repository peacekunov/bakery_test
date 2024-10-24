import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { WebApiService } from '../services/webapi.service';

@Component({
  selector: 'app-baking',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './baking.component.html',
  styleUrl: './baking.component.css'
})
export class BakingComponent {
  constructor(private webApiService: WebApiService) {}

  bakingForm = new FormGroup({
    bunCount: new FormControl(0, [Validators.required, Validators.min(1), Validators.max(1000)])
  });

  bake() {
    if(this.bakingForm.valid)
    {
      this.webApiService.startBakery(this.bakingForm.value.bunCount!);
    }
  }
}
