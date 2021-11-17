import { Component } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ToastService } from './services';

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
export class AppComponent {
  constructor(
    private toastService: ToastService,
    private messageService: MessageService,
  ) {
    this.toastService.showToast$.subscribe(m => this.messageService.add(m));
  }
}
