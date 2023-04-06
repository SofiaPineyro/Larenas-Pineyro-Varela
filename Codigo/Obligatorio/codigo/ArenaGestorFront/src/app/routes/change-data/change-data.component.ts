import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserUpdateUserDto } from 'src/app/models/Users/UserUpdateUserDto';
import { SecurityService } from 'src/app/services/security.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-change-data',
  templateUrl: './change-data.component.html'
})
export class ChangeDataComponent implements OnInit {

  model: UserUpdateUserDto = new UserUpdateUserDto();

  constructor(private toastr: ToastrService, private userService: UserService, private securityService: SecurityService) { }

  ngOnInit(): void {
    this.securityService.GetLoggedUser().subscribe(res => {
      this.userService.GetById(res.userId).subscribe(user => {
        this.model = new UserUpdateUserDto();
        this.model.name = user.name;
        this.model.surname = user.surname;
        this.model.userId = user.userId;
        this.model.roles = user.roles;
      })
    })
  }

  Confirmar() {
    this.userService.UpdateLogged(this.model).subscribe(res => {
      this.toastr.success("Datos cambiados con éxito")
    }, error => {
      this.toastr.error(error.error);
    })
  }

}
