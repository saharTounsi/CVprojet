using APIcv.Models;

namespace APIcv.interfaces
{
    public interface IUserRep
    {
        ICollection<User> GetUsers();
        User GetUser(int ID);
        bool UserEXISTS(int ID);
        bool CreateUser(User user);
        bool Save();
    }
}
