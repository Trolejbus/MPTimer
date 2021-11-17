import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ToastService } from '@app/services';
import { ConfirmationService } from 'primeng/api';
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

  constructor(
    private workTaskService: WorkTaskService,
    private confirmationService: ConfirmationService,
    private toastService: ToastService,
  ) {
    this.workTasks$ = workTaskService.entities$;
    this.loading$ = workTaskService.loading$;
  }

  ngOnInit(): void {
    this.workTaskService.getAll();
  }

  public remove(workTask: WorkTaskDto): void {
    this.confirmationService.confirm({
      message: `Are you sure that you want to remove ${workTask.name}?`,
      accept: () => {
        this.workTaskService.delete(workTask)
          .subscribe(() => {
            this.toastService.success(`Work Task ${workTask.name} has been successfully removed.`, "Work Task");
          });
      }
    });
  }

}
