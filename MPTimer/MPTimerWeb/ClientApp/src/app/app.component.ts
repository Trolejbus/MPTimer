import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastService, SignalRService } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: [
    ToastService,
    MessageService,
    ConfirmationService,
  ],
})
export class AppComponent implements OnInit {

  constructor(
    private toastService: ToastService,
    private messageService: MessageService,
    private signalRService: SignalRService,
  ) {
    this.toastService.showToast$.subscribe(m => this.messageService.add(m));
  }

  ngOnInit() {
    this.signalRService.init();
  }
}
