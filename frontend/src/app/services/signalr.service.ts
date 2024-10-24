import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Observable } from 'rxjs';
import { Bun } from '../model/Bun';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: HubConnection;

  constructor() {
    this.hubConnection = new HubConnectionBuilder().withUrl('http://localhost:5224/shelf').build();
    this.startConnection();
  }

  private startConnection(): void {
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public receiveMessage(): Observable<Bun[]> {
    return new Observable<Bun[]>((observer) => {
      this.hubConnection.on('ShelfUpdate', (data: Bun[]) => {
        console.log('Message received: ', data);
        observer.next(data);
      });
    });
  }
}