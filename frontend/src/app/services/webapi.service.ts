import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WebApiService {

  constructor(private httpClient: HttpClient) { }

  startBakery(count: number) {
    this.httpClient.post('http://localhost:5224/start', { count }).subscribe();
  }
}
