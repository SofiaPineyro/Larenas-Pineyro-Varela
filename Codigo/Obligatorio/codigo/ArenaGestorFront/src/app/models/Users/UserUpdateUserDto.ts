import { UserRoleDto } from "./UserRoleDto";

export class UserUpdateUserDto {
    userId: Number = 0;
    name: String = "";
    surname: String = "";
    roles: Array<UserRoleDto> = new Array();
}