import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateTodoRequest, Todo, UpdateTodoRequest } from '../models/todo';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private readonly apiUrl = 'https://localhost:7075/api/Todo';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Todo[]> {
    return this.http.get<Todo[]>(this.apiUrl);
  }

  getById(id: string): Observable<Todo> {
    return this.http.get<Todo>(`${this.apiUrl}/${id}`);
  }

  create(todo: CreateTodoRequest): Observable<void> {
    return this.http.post<void>(this.apiUrl, todo);
  }

  update(id: string, todo: UpdateTodoRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, todo);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
