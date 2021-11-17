import { ChangeDetectionStrategy, Component, ElementRef, Input, OnInit } from '@angular/core';

@Component({
  // tslint:disable-next-line: component-selector
  selector: '[app-table-empty-message]',
  templateUrl: './table-empty-message.component.html',
  styleUrls: ['./table-empty-message.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TableEmptyMessageComponent implements OnInit {

  @Input()
  public columnsCount = 1;
  @Input()
  public title: string = "No records to display";
  @Input()
  public message?: string;

  private nativeElement = this.elRef.nativeElement as HTMLElement;

  constructor(private elRef: ElementRef) { }

  ngOnInit(): void {
    this.nativeElement.classList.add('no-hover');
  }

}
