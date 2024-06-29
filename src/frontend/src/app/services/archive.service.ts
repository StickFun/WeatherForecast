import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, map, tap } from 'rxjs/operators';
import { MessageService } from './message.service';
import { of } from 'rxjs/internal/observable/of';

@Injectable({
  providedIn: 'root',
})
export class ArchiveService {
  private archiveServiceUrl = 'api/archive';

  constructor(
    private httpClient: HttpClient,
    private messageService: MessageService
  ) {}

  upload(file: File): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('0', file, file.name);

    let url = this.archiveServiceUrl;

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
