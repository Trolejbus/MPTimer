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

  private nativeElement: HTMLElement;
  private additionalNodes: Node[] = [];
  private parent?: HTMLElement;

  constructor(private elRef: ElementRef) {
    this.nativeElement = this.elRef.nativeElement as HTMLElement;
  }

  ngOnInit(): void {
    this.nativeElement.classList.add('no-hover');
  }

  ngAfterViewInit(): void {
    const parent = this.nativeElement.parentElement;
    if (parent == null) {
      throw new Error("Parent cannot be null");
    }

    this.parent = parent;
    this.duplicateRowsInParent();
  }

  ngOnDestroy(): void {
    for (const node of this.additionalNodes) {
      this.parent!.removeChild(node);
    }
  }

  private duplicateRowsInParent() {
    for (let index = 0; index < this.rowsCount - 1; index++) {
      const node = this.nativeElement.cloneNode(true);
      this.additionalNodes.push(node);
      this.parent!.insertBefore(node, this.nativeElement);
    }
  }
}
