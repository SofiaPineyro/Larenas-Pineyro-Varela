import { SecurityUserRole } from "./SecurityUserRole";

export class SecurityLoggedUser {
    userId: Number = 0;
    name: String = "";
    surname: String = "";
    email: String = "";
    roles: Array<SecurityUserRole> = new Array();
}