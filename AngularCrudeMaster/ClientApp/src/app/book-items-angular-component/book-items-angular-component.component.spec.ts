import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookItemsAngularComponentComponent } from './book-items-angular-component.component';

describe('BookItemsAngularComponentComponent', () => {
  let component: BookItemsAngularComponentComponent;
  let fixture: ComponentFixture<BookItemsAngularComponentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BookItemsAngularComponentComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookItemsAngularComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
