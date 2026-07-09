import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Request } from '../../classes/Request';
import { Category } from '../../classes/Category';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
arr_categories: Array<Category> = []
url='https://localhost:7161/api/Categories';

constructor(private http: HttpClient) { }

getAllCategories(): Observable<Category[]> {
  return this.http.get<Category[]>(this.url);
}

getById(id: string): Observable<Category> {
  return this.http.get<Category>(`${this.url}/${id}`);
}

getRequestsByCategoryId(id: string): Observable<Request[]> {
  return this.http.get<Request[]>(`${this.url}/${id}/requests`);
}

add(category: Category): Observable<string> {
  return this.http.post<string>(this.url, category);
}

update(category: Category): Observable<number> {
  return this.http.put<number>(this.url, category);
}

delete(id: string): Observable<number> {
  return this.http.delete<number>(`${this.url}/${id}`);
}
}
