import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonitorForecastsComponent } from './monitor-forecasts.component';

describe('MonitorForecastsComponent', () => {
  let component: MonitorForecastsComponent;
  let fixture: ComponentFixture<MonitorForecastsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MonitorForecastsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MonitorForecastsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
