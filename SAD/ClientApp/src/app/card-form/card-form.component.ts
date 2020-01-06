import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Room } from '../room.model';
import { RoomService } from '../room.service';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { UserService } from '../user.service';
import { User } from '../user.model';
import { map } from 'rxjs/operators';
import { CardRoom } from '../card-room.model';
import { Card } from '../card.model';
import { CardService } from '../card.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-card-form',
  templateUrl: './card-form.component.html',
  styleUrls: ['./card-form.component.css']
})
export class CardFormComponent implements OnInit {
  rooms$: Observable<Room[]>;
  form: FormGroup;
  users$: Observable<User[]>;
  users: User[] = [];
  allowedRooms: CardRoom[] = [];
  rooms: Room[];

  constructor(private roomService: RoomService,
    private userService: UserService,
    private cardService: CardService,
    private route: ActivatedRoute,
    private router: Router) {
    this.getRooms();
    this.users$ = userService.getAll();
    //this.users$.subscribe();
    this.initForm();
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.cardService.getById(params['id']).subscribe(card => {
          if (card) {
            this.initForm(card);
            this.allowedRooms = card.allowedRooms;
            this.getRooms();
          }
        });
      }
    });
  }

  getRooms() {
    this.rooms$ = this.roomService.getAll().pipe(
      map((result: Room[]) => result.filter(room => !this.allowedRooms.find(allowedRoom => allowedRoom.roomId === room.id))),
      map(result => {
        this.rooms = result;
        return result;
      })
    );
  }

  initForm(card?: Card) {
    this.form = new FormGroup({
      id: new FormControl(card ? card.id : ''),
      serialNumber: new FormControl(card ? card.serialNumber : '', Validators.required),
      cardOwnerId: new FormControl(card ? card.cardOwnerId : null, Validators.required)
    });
  }

  save() {
    if (this.form.valid) {
      var value = <Card>this.form.value;
      value.allowedRooms = this.allowedRooms;
      this.cardService.save(value).subscribe(() => {
        this.router.navigateByUrl("cards");
      });
    }
  }

  addRoom(id) {
    var newRoom = new CardRoom();
    newRoom.roomId = id;
    newRoom.roomNumber = this.rooms.find(room => room.id === id).number;
    this.allowedRooms.unshift(newRoom);
    this.getRooms();
  }

  removeRoom(id) {
    this.allowedRooms = this.allowedRooms.filter(room => room.roomId !== id);
    this.getRooms();
  }

  compareSelectOptions = (a: string, b: string) => {
    return (a != null && b != null && a === b)
  };
}
