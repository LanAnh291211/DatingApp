using DatingApp.API.Databases;
using DatingApp.API.Databases.Entities;

namespace DatingApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username || u.Email == username);
        }

        public List<User> GetUsers()
        {
            return _context.Users.Take(25).ToList();
        }

        public void UpdateUser(User user)
        {
            var updatedUser = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (updatedUser is null) return;
            updatedUser.Username = user.Username;
            updatedUser.Email = user.Email;
            _context.SaveChanges();
        }
    }
}