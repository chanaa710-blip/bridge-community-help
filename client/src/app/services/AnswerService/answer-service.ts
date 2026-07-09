import { Injectable } from '@angular/core';
import { Answer } from '../../classes/Answer';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AnswerService {
  arr_answers: Array<Answer> = []
  url='https://localhost:7161/api/Answer';

  constructor(private http: HttpClient) { }

  getAllAnswers():Observable<Array<Answer>>{
    return this.http.get<Array<Answer>>(this.url);
  }


  addAnswer(answer: Answer): Observable<Answer> {
    return this.http.post<Answer>(this.url, answer);
  }
  
  deleteAnswer(id: string): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }

  updateAnswer(answer: Answer): Observable<Answer> {
    return this.http.put<Answer>(this.url, answer);
  }

  getAnswerById(id: string): Observable<Answer> {
    return this.http.get<Answer>(this.url + '/' + id);
  }

  getAnswersByRequestId(requestId: string): Observable<Array<Answer>> {
    return this.http.get<Array<Answer>>(`${this.url}/request/${requestId}`);
  }

  getAnswersByUserId(userId: string): Observable<Array<Answer>> {
    return this.http.get<Array<Answer>>(`${this.url}/user/${userId}`);
  }
}
