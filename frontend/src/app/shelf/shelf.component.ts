import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../services/signalr.service';
import { Bun } from '../model/Bun';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-shelf',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './shelf.component.html',
  styleUrl: './shelf.component.css'
})
export class ShelfComponent implements OnInit {
  public buns: Bun[] = [];

  constructor(private signalRService: SignalRService) {}

  ngOnInit(): void {
    this.signalRService.receiveMessage().subscribe((data) => {
      this.buns = data;
    });
  }
}
