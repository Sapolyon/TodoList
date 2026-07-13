import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { finalize } from 'rxjs';
import { Todo, UpdateTodoRequest } from './models/todo';
import { TodoService } from './services/todo.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  todos: Todo[] = [];
  title = '';
  description = '';
  isCompleted = false;
  editingTodoId: string | null = null;
  isLoading = false;
  isSaving = false;
  errorMessage = '';

  constructor(
    private todoService: TodoService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.cdr.detectChanges();

    this.todoService.getAll()
      .pipe(finalize(() => {
        this.isLoading = false;
        this.cdr.detectChanges();
      }))
      .subscribe({
        next: (todos) => {
          this.todos = Array.isArray(todos) ? [...todos] : [];
          this.cdr.detectChanges();
        },
        error: (error: unknown) => {
          console.error(error);
          this.errorMessage = 'Görevler yüklenirken bir hata oluştu.';
          this.cdr.detectChanges();
        }
      });
  }

  saveTodo(): void {
    if (!this.title.trim() || !this.description.trim()) {
      this.errorMessage = 'Başlık ve açıklama alanları boş bırakılamaz.';
      this.cdr.detectChanges();
      return;
    }

    this.errorMessage = '';

    if (this.editingTodoId === null) {
      this.createTodo();
      return;
    }

    this.updateTodo();
  }

  createTodo(): void {
    const todo = {
      title: this.title.trim(),
      description: this.description.trim()
    };

    this.isSaving = true;
    this.cdr.detectChanges();

    this.todoService.create(todo)
      .pipe(finalize(() => {
        this.isSaving = false;
        this.cdr.detectChanges();
      }))
      .subscribe({
        next: () => {
          this.clearForm();
          this.loadTodos();
        },
        error: (error: unknown) => {
          console.error(error);
          this.errorMessage = 'Görev eklenirken bir hata oluştu.';
          this.cdr.detectChanges();
        }
      });
  }

  startEdit(todo: Todo): void {
    this.editingTodoId = todo.id;
    this.title = todo.title;
    this.description = todo.description;
    this.isCompleted = todo.isCompleted;
    this.errorMessage = '';
    this.cdr.detectChanges();
  }

  updateTodo(): void {
    if (this.editingTodoId === null) {
      return;
    }

    const todo: UpdateTodoRequest = {
      id: this.editingTodoId,
      title: this.title.trim(),
      description: this.description.trim(),
      isCompleted: this.isCompleted
    };

    this.isSaving = true;
    this.cdr.detectChanges();

    this.todoService.update(todo.id, todo)
      .pipe(finalize(() => {
        this.isSaving = false;
        this.cdr.detectChanges();
      }))
      .subscribe({
        next: () => {
          this.clearForm();
          this.loadTodos();
        },
        error: (error: unknown) => {
          console.error(error);
          this.errorMessage = 'Görev güncellenirken bir hata oluştu.';
          this.cdr.detectChanges();
        }
      });
  }

  toggleCompleted(todo: Todo): void {
    const updatedTodo: UpdateTodoRequest = {
      id: todo.id,
      title: todo.title,
      description: todo.description,
      isCompleted: !todo.isCompleted
    };

    this.todoService.update(todo.id, updatedTodo).subscribe({
      next: () => {
        this.todos = this.todos.map((item) =>
          item.id === todo.id
            ? { ...item, isCompleted: updatedTodo.isCompleted }
            : item
        );
        this.cdr.detectChanges();
        this.loadTodos();
      },
      error: (error: unknown) => {
        console.error(error);
        this.errorMessage = 'Görev durumu değiştirilirken bir hata oluştu.';
        this.cdr.detectChanges();
      }
    });
  }

  deleteTodo(id: string): void {
    this.todoService.delete(id).subscribe({
      next: () => {
        this.todos = this.todos.filter((todo) => todo.id !== id);

        if (this.editingTodoId === id) {
          this.clearForm();
        }

        this.cdr.detectChanges();
        this.loadTodos();
      },
      error: (error: unknown) => {
        console.error(error);
        this.errorMessage = 'Görev silinirken bir hata oluştu.';
        this.cdr.detectChanges();
      }
    });
  }

  clearForm(): void {
    this.title = '';
    this.description = '';
    this.isCompleted = false;
    this.editingTodoId = null;
    this.cdr.detectChanges();
  }

  trackByTodoId(index: number, todo: Todo): string {
    return todo.id;
  }
}
