using UserInfo.Models;

namespace UserInfo.Api.Models
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
        User Add(User newUser);
        User Delete(int id);
        User Update(User updateUser);
    }
}
