import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { Forecast } from '../entities/forecast';
import { ForecastService } from '../services/forecast.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-monitor-forecasts',
  templateUrl: './monitor-forecasts.component.html',
  styleUrl: './monitor-forecasts.component.css'
})
export class MonitorForecastsComponent implements AfterViewInit {
    // displayedColumns: string[] = [
    //   'Дата', 
    //   'Температура воздуха', 
    //   'Относительная влажность воздуха', 
    //   'Точка росы', 
    //   'Атмосферное давление', 
    //   'Направление ветра', 
    //   'Скорость ветра, м/с', 
    //   'Облачность',
    //   'Нижняя граница облачности',
    //   'Горизонтальная видимость',
    //   'Погодные явления'
    // ];
    displayedColumns: string[] = [
      'datetime', 
      'airTemperature', 
      'relativeAirHumidityPercent', 
      // 'dewPoint', 
      // 'atmosphericPressure', 
      // 'windDirection', 
      // 'windSpeed', 
      // 'cloudiness',
      // 'lowerCloudEdge',
      // 'horizontalVisibility',
      // 'weatherEvents'
    ];
    forecasts: Forecast[] = [];
    dataSource = new MatTableDataSource<Forecast>(this.forecasts);

    private skip: number = 0;
    private take: number = 10;

    @ViewChild(MatPaginator) paginator: MatPaginator;

    constructor(private forecastService: ForecastService) { }

    ngAfterViewInit(): void {
      this.dataSource.paginator = this.paginator;
      this.dataSource = new MatTableDataSource<Forecast>(this.forecasts);
    }

    ngOnInit() {
      this.updateForecasts();
    }

    private updateForecasts() {
      this.forecastService.get(this.skip, this.take)
          .subscribe(forecasts => {this.forecasts = forecasts; console.log(forecasts); });
    }
}
