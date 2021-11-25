import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SidebarComponent implements OnInit {

  private expand$ = new BehaviorSubject<boolean>(true);
  private items$: Observable<SidebarItem[]> = of([
    {
      label: 'Dashboard',
      icon: 'fa fa-chart-line',
      link: '',
    },
    {
      label: 'Work Tasks',
      icon: 'fa fa-clipboard-list',
      link: 'work-tasks',
    },
    {
      label: 'Agents',
      icon: 'fa fa-network-wired',
      link: 'agents',
    },
    {
      label: 'Source Controls',
      icon: 'fab fa-git-alt',
      link: 'source-controls',
    }
  ]);

  public vm$ = combineLatest([
    this.expand$,
    this.items$,
  ]).pipe(
    map(([expanded, items]) => ({
      expanded, items,
    })),
  );

  constructor() { }

  ngOnInit(): void {
  }

  public toggleExpand(): void {
    this.expand$.next(!this.expand$.value);
  }
}

class SidebarItem {
  public label!: string;
  public icon!: string;
  public link!: string;
}
