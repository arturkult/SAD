import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { Card } from './card.model';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CardService {
  url = "api/card";
  cards: Card[] = [];
  cards$ = new BehaviorSubject<Card[]>(this.cards);
  constructor(private http: HttpClient) {
    this.refresh();
  }

  refresh() {
    this.http.get<Card[]>(this.url).subscribe(result => {
      this.cards = result;
      this.cards$.next(this.cards);
    })
  }

  getAll() {
    this.refresh();
    return this.cards$;
  }

  getById(id: string) {
    if (!this.cards.find(card => card.id === id)) {
      this.refresh();
    }
    return this.cards$.pipe(map(result => result.find(card => card.id === id)));
  }

  public save(entity: Card) {
    if (entity.id) {
      return this.update(entity);
    }
    else {
      return this.insert(entity);
    }
  }

  insert(entity: Card) {
    return this.http.post(this.url, entity);
  }

  update(entity: Card) {
    return this.http.put(this.url, entity);
  }

  block(id: string) {
    return this.http.post(`${this.url}/block/${id}`, null);
  }
}
