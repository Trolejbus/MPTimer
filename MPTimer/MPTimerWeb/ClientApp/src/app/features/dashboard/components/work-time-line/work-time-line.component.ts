import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-work-time-line',
  templateUrl: './work-time-line.component.html',
  styleUrls: ['./work-time-line.component.scss'],
})
export class WorkTimeLineComponent implements OnInit {

  public minHour: number = 6;
  public maxHour: number = 18;
  public hourArray = Array(this.maxHour - this.minHour + 2).fill(1);
  public Arr = Array;

  constructor() { }

  ngOnInit(): void {
  }

  public isLast(index: number): boolean {
    return index >= this.maxHour - this.minHour + 2 - 1;
  }

  public getColumnWidth(): number {
    return 90 * 1 / (this.maxHour - this.minHour);
  }

}
