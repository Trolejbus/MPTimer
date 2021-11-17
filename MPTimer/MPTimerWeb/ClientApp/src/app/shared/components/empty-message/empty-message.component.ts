import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-empty-message',
  templateUrl: './empty-message.component.html',
  styleUrls: ['./empty-message.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmptyMessageComponent implements OnInit {

  @Input()
  public title!: string;
  @Input()
  public type: 'error' | 'secondary' = 'secondary';
  @Input()
  public size: 'small' | 'normal' | 'large' = 'normal';

  constructor() { }

  ngOnInit(): void {
  }

}
