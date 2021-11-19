import { Injectable } from "@angular/core";
import * as signalR from "@aspnet/signalr";
import { environment } from "../../environments/environment";
import { BehaviorSubject, Observable, Subject } from "rxjs";

@Injectable({ providedIn: 'root' })
export class SignalRService {

    private hubConnection: signalR.HubConnection;
    private status = new BehaviorSubject<SignalRStatus>(SignalRStatus.NotConnected);

    public status$ = this.status.asObservable();

    constructor() {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(`${environment.backendUrl}/Agent`)
            .build();
    }

    public init(): void {        
        this.status.next(SignalRStatus.Connecting);
        this.hubConnection
            .start()
            .then(() => { this.status.next(SignalRStatus.Connected); })
            .catch(err => {
                console.log('Error while starting connection: ', err);
                this.status.next(SignalRStatus.Error);
            });
        this.hubConnection.onclose(() => { this.status.next(SignalRStatus.NotConnected )});
    }
    
    public on$<T>(methodName: string): Observable<T> {
        const subject = new Subject<T>();
        this.hubConnection.on(methodName, (p) => subject.next(p));
        return subject;
    }
}

export enum SignalRStatus {
    Connected = 1,
    Connecting,
    NotConnected,
    Error,
}