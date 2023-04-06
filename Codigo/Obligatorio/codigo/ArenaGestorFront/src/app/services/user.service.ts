import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { UserResultUserDto } from '../models/Users/UserResultUserDto';
import { UserGetUsersDto } from '../models/Users/UserGetUsersDto';
import { UserInsertUserDto } from '../models/Users/UserInsertUserDto';
import { UserUpdateUserDto } from '../models/Users/UserUpdateUserDto';
import { UserChangePasswordDto } from '../models/Users/UserChangePasswordDto';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl: string

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiURL + "users"
  }

  Get(filter?: UserGetUsersDto): Observable<Array<UserResultUserDto>> {
    let url = this.apiUrl;

    if (filter) {
      let params = "";

      if (filter.name.length > 0) {
        params = "?name=" + filter.name
      }
      if (filter.surname.length > 0) {
        if (params.length > 0) {
          params = params + "&"
        }
        else {
          params = "?"
        }
        params = params + "surname=" + filter.surname
      }
      if (filter.email.length > 0) {
        if (params.length > 0) {
          params = params + "&"
        }
        else {
          params = "?"
        }
        params = params + "email=" + filter.email
      }
      url = url + params;
    }
    
    return this.http.get<Array<UserResultUserDto>>(url)
  }

  GetById(id: Number): Observable<UserResultUserDto> {
    return this.http.get<UserResultUserDto>(this.apiUrl + "/" + id.toString())
  }

  Insert(user: UserInsertUserDto): Observable<UserResultUserDto> {
    return this.http.post<UserResultUserDto>(this.apiUrl, user)
  }

  Update(user: UserUpdateUserDto): Observable<UserResultUserDto> {
    return this.http.put<UserResultUserDto>(this.apiUrl, user)
  }

  UpdateLogged(user: UserUpdateUserDto): Observable<UserResultUserDto> {
    return this.http.put<UserResultUserDto>(this.apiUrl + "/loggedIn", user)
  }

  Delete(id: Number) {
    return this.http.delete(this.apiUrl + "/" + id.toString())
  }

  ChangePasswordLoggedUser(user: UserChangePasswordDto) {
    return this.http.put(this.apiUrl + "/loggedInPassword", user)

  }

  ChangePassword(user: UserChangePasswordDto) {
    return this.http.put(this.apiUrl + "/password", user)
  }

}
