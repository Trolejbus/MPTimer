import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TableEmptyMessageComponent } from './table-empty-message.component';

describe('TableEmptyMessageComponent', () => {
  let component: TableEmptyMessageComponent;
  let fixture: ComponentFixture<TableEmptyMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TableEmptyMessageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TableEmptyMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
