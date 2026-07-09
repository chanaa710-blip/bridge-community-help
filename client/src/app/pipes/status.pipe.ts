import { Pipe, PipeTransform } from '@angular/core';
import { RequestStatus } from '../classes/RequestStatus';

@Pipe({
  name: 'statusText',
  standalone: true
})
export class StatusPipe implements PipeTransform {
  transform(value: RequestStatus): string {
    switch (value) {
      case RequestStatus.Open: return 'פתוחה';
      case RequestStatus.InProgress: return 'בתהליך';
      case RequestStatus.Closed: return 'סגורה';
      case RequestStatus.Canceled: return 'מבוטלת';
      default: return 'לא ידוע';
    }
  }
}