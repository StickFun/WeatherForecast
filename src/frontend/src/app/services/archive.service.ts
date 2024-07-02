import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { MessageService } from './message.service';
import { of } from 'rxjs/internal/observable/of';
import { environment } from '../../environments/environment.development';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ArchiveService {
  private postArchiveEndpoint = 'api/archive';
  private serviceUrl = environment.forecastParserServiceUrl;

  constructor(
    private httpClient: HttpClient,
    private messageService: MessageService
  ) {}

  upload(file: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);

    let url = new URL(this.postArchiveEndpoint, this.serviceUrl).toString();

    return this.httpClient.post(url, formData).pipe(
      catchError(this.handleError<any>('uploadArchive', []))
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
