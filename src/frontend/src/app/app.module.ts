import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeWeatherForecastComponent } from './home-weather-forecast/home-weather-forecast.component';
import { MonitorForecastsComponent } from './monitor-forecasts/monitor-forecasts.component';
import { UploadArchiveComponent } from './upload-archive/upload-archive.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeWeatherForecastComponent,
    MonitorForecastsComponent,
    UploadArchiveComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
