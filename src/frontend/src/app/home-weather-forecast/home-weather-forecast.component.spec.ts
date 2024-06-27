import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeWeatherForecastComponent } from './home-weather-forecast.component';

describe('HomeWeatherForecastComponent', () => {
  let component: HomeWeatherForecastComponent;
  let fixture: ComponentFixture<HomeWeatherForecastComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HomeWeatherForecastComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(HomeWeatherForecastComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
