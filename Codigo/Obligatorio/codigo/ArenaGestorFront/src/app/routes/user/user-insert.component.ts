import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ToastrService } from 'ngx-toastr';
import { RolesResultDto } from 'src/app/models/Roles/RolesResultDto';
import { UserInsertUserDto } from 'src/app/models/Users/UserInsertUserDto';
import { RoleService } from 'src/app/services/role.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  templateUrl: './user-form.component.html'
})
export class UserInsertComponent implements OnInit {

  mode: String = "Insertar";
  model: UserInsertUserDto = new UserInsertUserDto()
  email: String = "";
  password: String = "";

  dropdownSettings: IDropdownSettings = {};
  roleList: Array<RolesResultDto> = new Array()

  constructor(private roleService: RoleService, private service: UserService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.dropdownSettings = {
      idField: 'roleId',
      textField: 'name',
      enableCheckAll: false,
    };
    this.roleService.GetUserRoles().subscribe(res => { this.roleList = res })
  }

  Confirmar() {
    this.model.email = this.email;
    this.model.password = this.password;
    this.service.Insert(this.model).subscribe(res => {
      this.toastr.success("Usuario agregado correctamente", "Éxito")
      this.router.navigate(["/administracion/usuarios"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }
}
