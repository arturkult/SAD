import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Room } from './room.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RoomService {
  private url = "api/room";
  private list: Room[] = [];
  private list$ = new BehaviorSubject<Room[]>([]);

  constructor(private http: HttpClient) {
    this.refresh();
  }

  public refresh() {
    this.http.get(this.url).subscribe((result: Room[]) => {
      this.list = result;
      this.list$.next(this.list);
    });
  }

  public getAll() {
    this.refresh();
    return this.list$;
  }

  public getById(id: string): Observable<Room> {
    if (!this.list.find(room => room.id === id)) {
      this.refresh();
    }
    return this.getAll().pipe(map(list => list.find(room => room.id === id)));
  }

  public save(room: Room) {
    if (room.id) {
      return this.insert(room);
    }
    return this.update(room);
  }

  public insert(room: Room) {
    return this.http.post(this.url, room);
  }

  public update(room: Room) {
    return this.http.put(this.url, room); 
  }

  public block(id: string) {
    return this.http.post(`${this.url}/block/${id}`, null);
  }
}
