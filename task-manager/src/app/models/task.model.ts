export interface Task {
  id?: number;
  title: string;
  description: string;
  status: 'pending' | 'completed';
  //dueDate?: Date;
  createdAt?: Date;
  updatedAt?: Date;
}