using APIcv.DATA;
using APIcv.interfaces;
using APIcv.Models;

namespace APIcv.Repository
{
    public class UserRep : IUserRep
    {
        private Datacontext _context;
        public UserRep(Datacontext context)
        {
            _context = context;
        }

        public bool CreateUser(User user)
        {
            //change Tracker
            //Add,Update,mofidy
            //connected,disconnected
            _context.Add(user);
   
            return Save();
        }

        public User GetUser(int ID)
        {
            return _context.Users.Where(e => e.ID == ID).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(c => c.ID).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UserEXISTS(int ID)
        {
            return _context.Users.Any(c => c.ID == ID);
        }
    }
}
