import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserGetUsersDto } from 'src/app/models/Users/UserGetUsersDto';
import { UserResultUserDto } from 'src/app/models/Users/UserResultUserDto';
import { UserService } from 'src/app/services/user.service';

@Component({
  templateUrl: './user.component.html'
})
export class UserComponent implements OnInit {

  userList: Array<UserResultUserDto> = new Array()
  filter: UserGetUsersDto = new UserGetUsersDto()
  userToDelete: Number = 0;

  constructor(private toastr: ToastrService, private service: UserService, private router: Router) { }

  ngOnInit(): void {
    this.GetData()
  }

  GetData() {
    this.service.Get(this.filter).subscribe(res => {
      this.userList = res
    })
  }

  SetUserToDelete(id: Number) {
    this.userToDelete = id;
  }

  Delete() {
    this.service.Delete(this.userToDelete).subscribe(res => {
      this.toastr.success("Usuario eliminado correctamente", "Éxito")
      this.GetData();
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }

}
