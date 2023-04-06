import { UserRoleDto } from "./UserRoleDto";

export class UserResultUserDto {
    userId: Number = 0;
    name: String = "";
    surname: String = "";
    email: String = "";
    roles: Array<UserRoleDto> = new Array();
}