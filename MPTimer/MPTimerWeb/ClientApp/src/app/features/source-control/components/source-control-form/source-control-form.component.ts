import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AgentDto, AgentService } from '@app/features/agents';
import { ToastService } from '@app/services';
import { FormUtils, GuidUtils } from '@app/shared';
import { SelectItem } from 'primeng/api';
import { Observable, of, Subscription } from 'rxjs';
import { filter, map, switchMap } from 'rxjs/operators';
import { SourceControlDto } from '../../models';
import { SourceControlService } from '../../services';

@Component({
  selector: 'app-source-control-form',
  templateUrl: './source-control-form.component.html',
  styleUrls: ['./source-control-form.component.scss'],
  providers: [
    FormUtils,
  ]
})
export class SourceControlFormComponent implements OnInit {

  public agents$: Observable<SelectItem<string>[]> = this.agentService.entities$.pipe(
    map(agents => agents.map(agent => ({
      label: agent.name,
      value: agent.id
    }))),
  )

  public formGroup: FormGroup;
  public loading: boolean = false;
  private subscriptions = new Subscription();

  constructor(
    public form: FormUtils,
    private sourceControlService: SourceControlService,
    private agentService: AgentService,
    private router: Router,
    private toastService: ToastService,
    fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
  ) {
    this.formGroup = fb.group({
      id: [],
      name: [, Validators.required],
      path: [, Validators.required],
      agentId: [, Validators.required],
    });
    form.setForm(this.formGroup);
  }

  ngOnInit(): void {
    this.agentService.getAll();
    this.subscriptions.add(this.activatedRoute.params
      .pipe(
        map(p => p.id),
        switchMap(v => v != null ? this.sourceControlService.getByKey(v) : of(null)),
      ).subscribe(p => {
        if (p == null) {
          return;
        }

        this.formGroup.setValue(p);
      }));
  }

  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

  public isEditing(): boolean {
    return this.formGroup.get('id')?.value != null;
  }

  public onSubmit(): void {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.invalid) {
      return;
    }

    this.formGroup.disable();
    this.loading = true;
    const value: SourceControlDto = this.formGroup.value;
    const call$ = value.id == null ? this.sourceControlService.add({ ...value, id: GuidUtils.newGuid() }) : this.sourceControlService.update(value);
    call$.subscribe(() => {
      this.loading = false;
      this.formGroup.enable();
      this.router.navigate(["source-controls/list"]);
      this.toastService.success("Successfully added new Source Control", "Source Control");
    });
  }

}
