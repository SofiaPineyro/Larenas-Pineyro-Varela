import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { ToastrService } from 'ngx-toastr';
import { RolesResultDto } from 'src/app/models/Roles/RolesResultDto';
import { UserUpdateUserDto } from 'src/app/models/Users/UserUpdateUserDto';
import { RoleService } from 'src/app/services/role.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  templateUrl: './user-form.component.html'
})
export class UserUpdateComponent implements OnInit {

  mode: String = "Actualizar";
  model: UserUpdateUserDto = new UserUpdateUserDto()
  email: String = "";
  password: String = "";
  
  dropdownSettings: IDropdownSettings = {};
  roleList: Array<RolesResultDto> = new Array()

  constructor(private roleService: RoleService, private service: UserService, private toastr: ToastrService, private router: Router, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.service.GetById(params["id"]).subscribe(user => {
        this.model.name = user.name
        this.model.surname = user.surname
        this.model.userId = user.userId
        this.model.roles = user.roles
      })
    })
    this.dropdownSettings = {
      idField: 'roleId',
      textField: 'name',
      enableCheckAll: false,
    };
    this.roleService.GetUserRoles().subscribe(res => { this.roleList = res })
  }

  Confirmar() {
    this.service.Update(this.model).subscribe(res => {
      this.toastr.success("Usuario actualizado correctamente", "Éxito")
      this.router.navigate(["/administracion/usuarios"])
    },
      err => {
        this.toastr.error(err.error, "Error")
      })
  }
}
