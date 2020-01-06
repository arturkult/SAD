import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { User } from '../user.model';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {
  form: FormGroup;

  constructor(private router: Router,
    private activatedRoute: ActivatedRoute,
    private userService: UserService) {
    this.activatedRoute.params.subscribe(params => {
      if (params['id']) {
        this.userService.getById(params['id']).subscribe(
          (result: User[]) => {
            this.initForm(result[0]);
          });
      }
      else {
        this.initForm();
      }
    })
  }

  ngOnInit() {
  }

  save() {
    if (this.form.valid) {
      this.userService.save(this.form.value).subscribe(
        () => {
          this.router.navigateByUrl("/users");
        },
        (error: HttpErrorResponse) => console.error(error.error));
    }
  }

  reset() {
    this.router.navigateByUrl("/users");
  }

  private initForm(user?: User) {
    this.form = new FormGroup({
      id: new FormControl(user ? user.id : null),
      email: new FormControl(user ? user.email : '', [Validators.required, Validators.email]),
      fullName: new FormControl(user ? user.fullName : '', Validators.required),
    });
  }
}
