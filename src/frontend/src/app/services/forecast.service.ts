import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MessageService } from './message.service';
import { environment } from '../../environments/environment.development';
import { Observable, of, catchError } from 'rxjs';
import { Forecast } from '../entities/forecast';

@Injectable({
    providedIn: 'root'
})
export class ForecastService {
    private getAllForecastsEndpoint = "api/forecast"
    private serviceUrl = environment.forecastParserServiceUrl;

    constructor(
      private httpClient: HttpClient,
      private messageService: MessageService) { }

      get(skip: number, take: number): Observable<Forecast[]> {
        debugger;
        let url = new URL(this.getAllForecastsEndpoint, this.serviceUrl).toString();
        url = url.concat(`?skip=${skip}&take=${take}`);

        return this.httpClient.get<Forecast[]>(url)
          .pipe(
            catchError(this.handleError<any>('getForecasts', []))
        );
      }

      private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
          // TODO: send the error to remote logging infrastructure
          console.error(error);
    
          // TODO: better job of transforming error for user consumption
          this.log(`${operation} failed: ${error.message}`);
    
          return of(result as T);
        };
      }
    
      private log(message: string) {
        this.messageService.add(`HeroService: ${message}`);
      }
}
