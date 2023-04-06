import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { UserChangePasswordDto } from 'src/app/models/Users/UserChangePasswordDto';
import { UserService } from 'src/app/services/user.service';

@Component({
  templateUrl: './change-password.component.html'
})
export class ChangePasswordComponent implements OnInit {

  model: UserChangePasswordDto = new UserChangePasswordDto();
  newPasswordConfirm: String = "";

  constructor(private toastr: ToastrService, private userService: UserService) { }

  ngOnInit(): void {
  }

  Confirmar() {
    if (this.model.newPassword.length == 0 || this.newPasswordConfirm.length == 0 || this.model.oldPassword.length == 0) {
      this.toastr.error("No debes dejar campos vacíos");
      return;
    }
    if (this.model.newPassword != this.newPasswordConfirm) {
      this.toastr.error("Las contraseñas no coinciden");
      return;
    }
    this.userService.ChangePasswordLoggedUser(this.model).subscribe(res => {
      this.toastr.success("Contraseña cambiada con éxito")
      this.model = new UserChangePasswordDto();
    }, error => {
      this.toastr.error(error.error);
    })
  }

}
