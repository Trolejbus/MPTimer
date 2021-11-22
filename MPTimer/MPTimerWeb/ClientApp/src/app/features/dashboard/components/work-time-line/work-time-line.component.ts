import { Component, Input, OnInit } from '@angular/core';
import { WorkTimeLineSectionModel } from './models';

@Component({
  selector: 'app-work-time-line',
  templateUrl: './work-time-line.component.html',
  styleUrls: ['./work-time-line.component.scss'],
})
export class WorkTimeLineComponent implements OnInit {

  public sections: WorkTimeLineSectionModel[] = [
    {
      events: [
        {
          id: 'test11',
          name: 'Agent 1 Uptime',
          color: '#336B87',
          activities: [
            {
              id: 'test1',
              from: new Date(2021, 10, 22, 4, 30),
              to: new Date(2021, 10, 22, 10, 15),
            },
            {
              id: 'test2',
              from: new Date(2021, 10, 22, 10, 45),
              to: new Date(2021, 10, 22, 20, 45),
            },
          ],
        },
        {
          id: 'test12',
          name: 'Screen Locked',
          color: 'gray',
          activities: [
            {
              id: 'test3',
              from: new Date(2021, 10, 22, 8, 15),
            },
          ],
        },
      ]
    }
  ]

  constructor() { }

  ngOnInit(): void {
  }


}
