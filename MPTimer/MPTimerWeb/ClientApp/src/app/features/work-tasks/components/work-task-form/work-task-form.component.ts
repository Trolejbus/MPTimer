import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '@app/services';
import { FormUtils, GuidUtils } from '@app/shared';
import { Observable, Subscription } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
import { WorkTaskDto } from '../../models';
import { WorkTaskService } from '../../services';

@Component({
  selector: 'app-work-task-form',
  templateUrl: './work-task-form.component.html',
  styleUrls: ['./work-task-form.component.scss'],
  providers: [
    FormUtils,
  ]
})
export class WorkTaskFormComponent implements OnInit, OnDestroy {

  public formGroup: FormGroup;
  public loading: boolean = false;
  private subscriptions = new Subscription();

  constructor(
    public form: FormUtils,
    private service: WorkTaskService,
    private router: Router,
    private toastService: ToastService,
    fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
  ) {
    this.formGroup = fb.group({
      id: [],
      name: [, Validators.required],
    });
    form.setForm(this.formGroup);
  }

  ngOnInit(): void {
    this.subscriptions.add(this.activatedRoute.params
      .pipe(
        tap(p => console.log(p)),
        map(p => p.id),
        switchMap(v => this.service.getByKey(v)),
      ).subscribe(p => {
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
    const value: WorkTaskDto = this.formGroup.value;
    const call$ = value.id == null ? this.service.add({ ...value, id: GuidUtils.newGuid() }) : this.service.update(value);
    call$.subscribe(() => {
      this.loading = false;
      this.formGroup.enable();
      this.router.navigate(["work-tasks/list"]);
      this.toastService.success("Successfully added new Work Task", "Work Task");
    });
  }

}
