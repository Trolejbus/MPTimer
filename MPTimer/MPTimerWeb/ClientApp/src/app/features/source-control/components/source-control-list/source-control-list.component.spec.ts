import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SourceControlListComponent } from './source-control-list.component';

describe('SourceControlListComponent', () => {
  let component: SourceControlListComponent;
  let fixture: ComponentFixture<SourceControlListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SourceControlListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SourceControlListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
