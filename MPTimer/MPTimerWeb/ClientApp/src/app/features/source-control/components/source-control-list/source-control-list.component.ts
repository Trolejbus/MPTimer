import { Component, OnInit } from '@angular/core';
import { ToastService } from '@app/services';
import { ConfirmationService } from 'primeng/api';
import { combineLatest } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { SourceControlDto, SourceControlStateDto } from '../../models';
import { SourceControlService } from '../../services';

@Component({
  selector: 'app-source-control-list',
  templateUrl: './source-control-list.component.html',
  styleUrls: ['./source-control-list.component.scss']
})
export class SourceControlListComponent implements OnInit {

  public sourceControls$ = this.sourceControlService.sourceControls$;
  public loading$ = this.sourceControlService.loading$;
  public vm$ = combineLatest([
    this.sourceControls$.pipe(startWith(null)),
    this.loading$,
  ]).pipe(
    map(([sourceControls, loading]) => ({ sourceControls, loading })),
  );
  
  constructor(
    private sourceControlService: SourceControlService,
    private confirmationService: ConfirmationService,
    private toastService: ToastService,
  ) {
  }

  ngOnInit() {
    this.sourceControlService.getAll();
  }

  public remove(sourceControl: SourceControlDto): void {
    this.confirmationService.confirm({
      message: `Are you sure that you want to remove ${sourceControl.name}?`,
      accept: () => {
        this.sourceControlService.delete(sourceControl)
          .subscribe(() => {
            this.toastService.success(`Source control ${sourceControl.name} has been successfully removed.`, "Source Control");
          });
      }
    });
  }

  public getActiveBranch(sourceControl: SourceControlDto): string | null {
    return sourceControl.statuses.find(s => s.to == null)?.branchName ?? null;
  }
}
