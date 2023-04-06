using ArenaGestor.APIContracts.Users;
using Microsoft.AspNetCore.Mvc;

namespace ArenaGestor.APIContracts
{
    public interface IUsersAppService
    {
        IActionResult GetUserById(int userId);
        IActionResult GetUsers(UserGetUsersDto userFilter = null);
        IActionResult PostUser(UserInsertUserDto insertUser);
        IActionResult PutUser(UserUpdateUserDto updateUser);
        IActionResult PutUserLoggedIn(UserUpdateUserDto updateUser);
        IActionResult PutUserPassword(UserChangePasswordDto newPassword);
        IActionResult PutUserLoggedInPassword(UserChangePasswordDto newPassword);
        IActionResult DeleteUser(int userId);
    }
}
