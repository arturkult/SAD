import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from './user.model';
import { BehaviorSubject } from 'rxjs';
import { map, filter } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private url = "api/user";
  private users$ = new BehaviorSubject([]);
  private users: User[] = [];
  constructor(private http: HttpClient) {
    this.refresh();
  }

  refresh() {
    this.http.get<User[]>(this.url).subscribe(result => {
      this.users = result;
      this.users$.next(this.users);
    });
  };

  getAll() {
    return this.users$;
  }

  getById(id: string) {
    if (!this.users.filter(user => user.id === id)) {
      this.refresh();
    }

    return this.users$.pipe(map(result => {
      return result.filter(user => user.id === id);
    }))
  }

  save(user: User) {
    if (user.id) {
      return this.http.put(this.url, user)
        .pipe(map(result => {
          this.refresh();
        }));
    }
    return this.http.post(this.url, user)
      .pipe(map(result => {
        this.refresh();
      }));;
  }

  block(id: string) {
    return this.http.post(`${this.url}/block/${id}`, null);
  }
}
