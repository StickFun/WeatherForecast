import { Component } from '@angular/core';
import { Forecast } from '../entities/forecast';
import { ForecastService } from '../services/forecast.service';

@Component({
  selector: 'app-monitor-forecasts',
  templateUrl: './monitor-forecasts.component.html',
  styleUrl: './monitor-forecasts.component.css'
})
export class MonitorForecastsComponent {
    forecasts: Forecast[] = [];

    private skip: number = 0;
    private take: number = 10;

    constructor(private forecastService: ForecastService) { }

    ngOnInit() {
      this.updateForecasts();
    }

    private updateForecasts() {
      this.forecastService.get(this.skip, this.take)
          .subscribe(forecasts => {this.forecasts = forecasts; console.log(forecasts); });
    }
}
