import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UploadArchiveComponent } from './upload-archive/upload-archive.component';
import { MonitorForecastsComponent } from './monitor-forecasts/monitor-forecasts.component';
import { HomeWeatherForecastComponent } from './home-weather-forecast/home-weather-forecast.component';

const routes: Routes = [
  { path: '', component: HomeWeatherForecastComponent },
  { path: 'upload', component: UploadArchiveComponent },
  { path: 'monitor', component: MonitorForecastsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
