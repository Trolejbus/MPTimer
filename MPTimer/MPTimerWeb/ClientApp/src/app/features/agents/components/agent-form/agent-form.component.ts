import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastService } from '@app/services';
import { FormUtils, GuidUtils } from '@app/shared';
import { SelectItem } from 'primeng/api';
import { of, Subscription } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { AgentTypeTranslator } from '../../utils';
import { AgentType } from '../../enums';
import { AgentDto } from '../../models';
import { AgentService } from '../../services';

@Component({
  selector: 'app-agent-form',
  templateUrl: './agent-form.component.html',
  styleUrls: ['./agent-form.component.scss'],
  providers: [
    FormUtils,
  ],
})
export class AgentFormComponent implements OnInit {

  public types: SelectItem<AgentType>[] = [
    { label: AgentTypeTranslator.translate(AgentType.DesktopApp), value: AgentType.DesktopApp },
  ];

  public formGroup: FormGroup;
  public loading: boolean = false;
  private subscriptions = new Subscription();

  constructor(
    public form: FormUtils,
    private service: AgentService,
    private router: Router,
    private toastService: ToastService,
    fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
  ) {
    this.formGroup = fb.group({
      id: [],
      name: [, Validators.required],
      type: [AgentType.DesktopApp, Validators.required],
    });
    form.setForm(this.formGroup);
  }

  ngOnInit(): void {
    this.subscriptions.add(this.activatedRoute.params
      .pipe(
        map(p => p.id),
        switchMap(v => v != null ? this.service.getByKey(v) : of(null)),
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
    const value: AgentDto = this.formGroup.value;
    const call$ = value.id == null ? this.service.add({ ...value, id: GuidUtils.newGuid() }) : this.service.update(value);
    call$.subscribe(() => {
      this.loading = false;
      this.formGroup.enable();
      this.router.navigate(["agents/list"]);
      this.toastService.success("Successfully added new Agent", "Agent");
    });
  }

}
