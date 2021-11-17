import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { combineLatest, Observable } from 'rxjs';
import { WorkTaskDto } from '../../models';
import { WorkTaskService } from '../../services';

@Component({
  selector: 'app-work-task-list',
  templateUrl: './work-task-list.component.html',
  styleUrls: ['./work-task-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class WorkTaskListComponent implements OnInit {

  public workTasks$: Observable<WorkTaskDto[]>;
  public loading$: Observable<boolean>;

  constructor(private workTaskService: WorkTaskService) {
    this.workTasks$ = workTaskService.entities$;
    this.loading$ = workTaskService.loading$;
  }

  ngOnInit(): void {
    this.workTaskService.getAll();
  }

}
