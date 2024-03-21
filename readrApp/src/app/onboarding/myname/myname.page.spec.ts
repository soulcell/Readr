import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MynamePage } from './myname.page';

describe('MynamePage', () => {
  let component: MynamePage;
  let fixture: ComponentFixture<MynamePage>;

  beforeEach(async(() => {
    fixture = TestBed.createComponent(MynamePage);
    component = fixture.componentInstance;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
