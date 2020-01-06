import { Component, OnInit } from '@angular/core';
import { RoomService } from '../room.service';
import { Observable } from 'rxjs';
import { Room } from '../room.model';

@Component({
  selector: 'app-room-list',
  templateUrl: './room-list.component.html',
  styleUrls: ['./room-list.component.css']
})
export class RoomListComponent implements OnInit {

  rooms$ = new Observable<Room[]>();
  constructor(private roomService: RoomService) {
    this.rooms$ = roomService.getAll();
  }

  ngOnInit() {
  }

  block(id: string) {
    this.roomService.block(id).subscribe(() => this.roomService.refresh());
  }

}
