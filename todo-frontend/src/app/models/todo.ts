export interface Todo {
  id: string;
  title: string;
  description: string;
  isCompleted: boolean;
  createdAt?: string;
}

export interface CreateTodoRequest {
  title: string;
  description: string;
}

export interface UpdateTodoRequest {
  id: string;
  title: string;
  description: string;
  isCompleted: boolean;
}