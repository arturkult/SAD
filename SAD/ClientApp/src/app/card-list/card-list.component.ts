import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Card } from '../card.model';
import { CardService } from '../card.service';

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.css']
})
export class CardListComponent implements OnInit {
  cards$: Observable<Card[]>;
  constructor(private cardService: CardService) {
    this.cards$ = cardService.getAll();
  }

  ngOnInit() {

  }

  block(userId: string) {
    this.cardService.block(userId).subscribe(() => this.cardService.refresh());
  }

}
