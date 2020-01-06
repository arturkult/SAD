import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../user.model';
import { UserService } from '../user.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users$ = new Observable<User[]>();

  constructor(private userService: UserService) {
    this.users$ = userService.getAll();
  }

  ngOnInit() {
  }

  block(userId: string) {
    this.userService.block(userId).subscribe(() => this.userService.refresh());
  }
}
