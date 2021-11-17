import { AfterViewInit, ChangeDetectionStrategy, Component, ElementRef, Input, OnDestroy, OnInit } from '@angular/core';

@Component({
  // tslint:disable-next-line: component-selector
  selector: '[app-table-skeleton]',
  templateUrl: './table-skeleton.component.html',
  styleUrls: ['./table-skeleton.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TableSkeletonComponent implements OnInit, AfterViewInit, OnDestroy {

  @Input()
  public rowsCount = 5;
  @Input()
  public columnsCount = 1;
  public Arr = Array;

  private nativeElement = this.elRef.nativeElement as HTMLElement;
  private additionalNodes: Node[] = [];

  constructor(private elRef: ElementRef) { }

  ngOnInit(): void {
    this.nativeElement.classList.add('no-hover');
  }

  ngAfterViewInit(): void {
    this.duplicateRowsInParent();
  }

  ngOnDestroy(): void {
    const parent = this.nativeElement.parentElement;
    for (const node of this.additionalNodes) {
      parent?.removeChild(node);
    }
  }

  private duplicateRowsInParent() {
    const parent = this.nativeElement.parentElement;
    for (let index = 0; index < this.rowsCount - 1; index++) {
      const node = this.nativeElement.cloneNode(true);
      this.additionalNodes.push(node);
      parent?.insertBefore(node, this.nativeElement);
    }
  }
}
