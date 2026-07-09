import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Request } from '../../classes/Request';
import { RequestStatus } from '../../classes/RequestStatus';

@Injectable({
  providedIn: 'root',
})
export class RequestService {
  arr_requests: Array<Request> = []
  url='https://localhost:7161/api/Requests';

  constructor(private http: HttpClient) { }

  getAllRequests():Observable<Array<Request>>{
    return this.http.get<Array<Request>>(this.url);
  }


  addRequest(request: Request): Observable<Request> {

    return this.http.post<Request>(this.url, request);
  }
  
  deleteRequest(id: string): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }

  updateRequest(request: Request): Observable<Request> {
    return this.http.put<Request>(this.url, request);
  }

  getRequestById(id: string): Observable<Request> {
    return this.http.get<Request>(this.url + '/' + id);
  }

getRequestsByLocationAndCategory(lat: number, lng: number, radius: number, categoryId: string | null = null): Observable<Array<Request>> {
  let params = new HttpParams()
    .set('lat', lat)
    .set('lng', lng)
    .set('radiusInMeters', radius);
   
    if (categoryId) {
      params = params.set('categoryId', categoryId);
    }

  return this.http.get<Array<Request>>(`${this.url}/nearby-requests`, { params });
}

updateStatus(id: string, status: RequestStatus): Observable<boolean> {
  return this.http.patch<boolean>(`${this.url}/${id}/status`, status, {
    headers: { 'Content-Type': 'application/json' }
  });
}

getRequestsByUserId(userId: string): Observable<Array<Request>> {
  return this.http.get<Array<Request>>(`${this.url}/user/${userId}`);
}
}
