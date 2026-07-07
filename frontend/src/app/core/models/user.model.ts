export type UserRole = 'SuperAdmin' | 'Manager';

export interface User {
  id: number;
  name: string;
  mobileNumber: string;
  role: UserRole;
  isActive: boolean;
}

export interface LoginResponse {
  token: string;
  user: User;
}

export interface CreateUserRequest {
  name: string;
  mobileNumber: string;
  role: UserRole;
}

export interface UpdateUserRequest {
  name: string;
  mobileNumber: string;
  isActive: boolean;
}
