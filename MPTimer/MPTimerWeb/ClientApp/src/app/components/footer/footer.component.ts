import { Component, OnInit } from '@angular/core';
import { SignalRService, SignalRStatus } from '@app/services';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  public signalRStatus$ = this.signalRService.status$;
  public SignalRStatus = SignalRStatus;

  constructor(private signalRService: SignalRService) { }

  ngOnInit(): void {
  }

  public translateSignalRStatus(status: SignalRStatus): string {
    switch (status) {
      case SignalRStatus.NotConnected:
        return "Not Connected";
      case SignalRStatus.Connecting:
        return "Connecting";
      case SignalRStatus.Connected:
        return "Connected";
      case SignalRStatus.Error:
        return "Error";
      default:
        throw new Error("Not mapped");
    }
  }

  public getStatusClass(status: SignalRStatus): string {
    switch (status) {
      case SignalRStatus.NotConnected:
        return "not-connected";
      case SignalRStatus.Connecting:
        return "connecting";
      case SignalRStatus.Connected:
        return "connected";
      case SignalRStatus.Error:
        return "error";
      default:
        throw new Error("Not mapped");
    }
  }
}
