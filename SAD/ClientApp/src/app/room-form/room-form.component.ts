import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { RoomService } from '../room.service';
import { Room } from '../room.model';
import { FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-room-form',
  templateUrl: './room-form.component.html',
  styleUrls: ['./room-form.component.css']
})
export class RoomFormComponent implements OnInit {
  form: FormGroup;
  submitted: boolean = false;

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private roomService: RoomService) {
    this.activatedRoute.params.subscribe(params => {
      if (params['id']) {
        this.roomService.getById(params['id']).subscribe(
          (result: Room) => {
            this.initForm(result);
          });
      }
      else {
        this.initForm();
      }
    })
  }

  ngOnInit() {
  }

  initForm(room?: Room) {
    this.form = new FormGroup({
      id: new FormControl(room ? room.id : ''),
      number: new FormControl(room ? room.number : '', Validators.required),
      floor: new FormControl(room ? room.floor : '', Validators.required)
    })
  }

  save() {
    this.submitted = true;
    if (this.form.valid) {
      var formValue = this.form.value;
      this.roomService.save(formValue).subscribe(result => this.router.navigateByUrl("rooms"));
    }
  }

}
