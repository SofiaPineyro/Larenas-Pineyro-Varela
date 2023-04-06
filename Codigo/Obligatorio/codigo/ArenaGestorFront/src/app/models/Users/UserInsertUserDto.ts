import { UserRoleDto } from "./UserRoleDto"

export class UserInsertUserDto {
    name: String = "";
    surname: String = "";
    email: String = "";
    password: String = "";
    roles: Array<UserRoleDto> = new Array();
}