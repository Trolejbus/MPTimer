import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-time-worked-widget',
  templateUrl: './time-worked-widget.component.html',
  styleUrls: ['./time-worked-widget.component.scss']
})
export class TimeWorkedWidgetComponent implements OnInit {

  public data = {
    labels: ['A','B'],
    datasets: [
      {
        data: [300, 50],
        backgroundColor: [
          "#336B87",
          "#FFFFFF",
        ],
        hoverBackgroundColor: [
          "#204456",
          "#FFFFFF",
        ],
      },
    ],  
  };

  public chartOptions = {
    cutout: '80%',
    plugins: {
      legend: {
        display: false,
      },
    },
  };

  constructor() { }

  ngOnInit(): void {
  }

}
