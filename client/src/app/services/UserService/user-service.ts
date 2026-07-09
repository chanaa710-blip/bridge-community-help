import { Injectable } from '@angular/core';
import { User } from '../../classes/User';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { UserRegister } from '../../classes/UserRegister';
import { AuthResponse } from '../../classes/AuthResponse';

const TOKEN_KEY = 'token_bridge';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  arr_users: Array<User> = []
  currentUser: any = null
  url = 'https://localhost:7161/api/User';

  constructor(private http: HttpClient) {
    this.loadUserFromStorage();
  }

  private loadUserFromStorage() {
    const savedUser = localStorage.getItem('user_bridge');
    if (savedUser) {
      this.currentUser = JSON.parse(savedUser);
    }
  }

  saveUserToStorage(user: User) {
    this.currentUser = user;
    localStorage.setItem('user_bridge', JSON.stringify(user));
  }

  saveAuthToStorage(auth: AuthResponse) {
    this.currentUser = auth.user;
    localStorage.setItem('user_bridge', JSON.stringify(auth.user));
    localStorage.setItem(TOKEN_KEY, auth.token);
  }

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  clearUser() {
    this.currentUser = null;
    localStorage.removeItem('user_bridge');
    localStorage.removeItem(TOKEN_KEY);
  }

  getAllUsers(): Observable<Array<User>> {
    return this.http.get<Array<User>>(this.url);
  }


  addUser(user: User): Observable<User> {
    return this.http.post<User>(this.url, user);
  }

  deleteUser(id: string): Observable<void> {
    return this.http.delete<void>(`${this.url}/${id}`);
  }

  updateUser(user: User): Observable<User> {
    return this.http.put<User>(this.url, user);
  }

  getUserById(id: string): Observable<User> {
    return this.http.get<User>(this.url + '/' + id);
  }

  login(email: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.url}/login?email=${email}&password=${password}`, {});
  }

  register(userRegister: UserRegister): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.url}/register`, userRegister);
  }

  getUsersInRadius(lat: number, lng: number, radius: number): Observable<Array<User>> {
    return this.http.get<Array<User>>(`${this.url}/nearby-users?lat=${lat}&lng=${lng}&radiusInMeters=${radius}`);
  }

  updateLocation(id: string, lat: number, lng: number): Observable<any> {
  const body = { 
    id: id, 
    lat: lat, 
    lng: lng 
  };
  return this.http.put<any>(`${this.url}/update-location`, body);
}

updateProfile(user: User) {
  const payload = {
    id: user.id,
    name: user.name,
    phone: user.phone
  };
  return this.http.put<User>(`${this.url}/update-profile`, payload);
}
}